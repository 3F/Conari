/*
 * The MIT License (MIT)
 * 
 * Copyright (c) 2016  Denis Kuzmin <entry.reg@gmail.com>
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
*/

using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using Microsoft.CSharp.RuntimeBinder;
using net.r_eg.Conari.Extension;
using net.r_eg.Conari.Log;
using net.r_eg.Conari.Types;

namespace net.r_eg.Conari.Core
{
    public sealed class ProviderDLR: DynamicObject, IProviderDLR
    {
        private IProvider provider;

        /// <summary>
        /// Access to used IDynamic object.
        /// </summary>
        public IDynamic DynCfg
        {
            get {
                return Dynamic._;
            }
        }

        /// <summary>
        /// To use cache for dynamic types etc.
        /// </summary>
        public bool Cache
        {
            get {
                return DynCfg.UseCache;
            }
            set {
                DynCfg.UseCache = value;
            }
        }

        /// <summary>
        /// Current Convention for all dynamic methods.
        /// </summary>
        public CallingConvention Convention
        {
            get;
            private set;
        }

        /// <summary>
        /// To use information about types from CallingContext if it's possible.
        /// This should automatically:
        ///     * Detect all ByRef&amp; types.
        ///     * Bind all null-values for any reference-types that pushed with out/ref modifier.
        /// </summary>
        public bool UseCallingContext
        {
            get;
            set;
        } = true;

        /// <summary>
        /// To use ByRef&amp; (reference-types) for all sent types.
        /// </summary>
        public bool UseByRef
        {
            get;
            set;
        }

        /// <summary>
        /// Magic methods. Invoking.
        /// </summary>
        /// <![CDATA[
        ///     `[result =] name<return_type>([{argument_types}])`
        /// ]]>
        /// <param name="binder"></param>
        /// <param name="args"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            Type[] tArgs;

            try {
                tArgs = getArgTypes(binder, args);
            }
            catch(ArgumentException ex) {
                LSender.Send(this, $"Problem with arguments. DLR for '{binder.Name}': {ex.Message}", Message.Level.Warn);
                tArgs = args.Select(a => a.GetType()).ToArray();
            }

            Type[] tGeneric = getGenericArgTypes(binder).ToArray();
            MethodInfo mi   = getmi(binder.Name, tGeneric, tArgs);
            TDyn dyn        = provider.bind(mi, provider.procName(binder.Name), Convention);

            // Boxing types, for example: NullType -> null -> NullType

            object[] unboxed = unbox(args);
            result = Dynamic.DCast(dyn.returnType, dyn.dynamic.Invoke(null, unboxed));
            boxing(unboxed, args);

            return true;
        }

        public ProviderDLR(IProvider provider, CallingConvention conv)
        {
            if(provider == null) {
                throw new ArgumentException("Provider cannot be null.");
            }

            this.provider   = provider;
            Convention      = conv;
        }

        private object[] unbox(object[] args)
        {
            return args.Select(a => (a == null)
                                    ? null
                                    : (
                                          a.GetType()
                                              .GetInterface(typeof(IBoxed).FullName) != null
                                      )
                                      ? ((IBoxed)a).Data
                                      : a
                              )
                              .ToArray();
        }

        private void boxing(object[] from, object[] to)
        {
            if(from.Length != to.Length) {
                throw new ArgumentException($"boxing for types: length mismatch {from.Length} != {to.Length}");
            }

            for(int i = 0; i < to.Length; ++i)
            {
                if(to[i] == null) {
                    to[i] = from[i];
                    continue;
                }

                if(to[i].GetType().GetInterface(typeof(IBoxed).FullName) != null) {
                    ((IBoxed)to[i]).Data = from[i];
                }
            }
        }

        private MethodInfo getmi(string name, Type[] generic, Type[] args)
        {
            if(generic?.Length > 1) {
                throw new ArgumentException("Allowed only one type (as a return type) for this generic method.");
            }

            return Dynamic.GetMethodInfo(
                                name,
                                (generic == null || generic.Length < 1) ? null : generic[0],
                                args
                           );
        }

        private Type[] getArgTypes(InvokeMemberBinder binder, object[] args)
        {
            // NOTE: the args does not contain information about reference-types, that is IsByRef == false for any case

            Type[] tArgs = null;

            if(UseCallingContext) {
                LSender.Send(this, "Trying to get types from CallingContext", Message.Level.Trace);
                tArgs = getTypesFromCallingContext(binder);
            }

            if(tArgs == null)
            {
                LSender.Send(this, $"UseCallingContext == {UseCallingContext} && tArgs == null", Message.Level.Trace);
                tArgs = args.Select((a, i) => 
                                        (a == null) 
                                            ? typeof(object)
                                            : (a.GetType().GetInterface(typeof(INullType).FullName) != null)
                                                    ? ((INullType)a).GenericType // null-value via NullType<IData> -> IData
                                                                    //.E<Type>(() => { args[i] = null; })
                                                    : a.GetType()
                                   )
                                   .ToArray();
            }

            if(!UseCallingContext)
            {
                CSharpArgumentInfoFlags[] aflags = getArgFlags(binder);
                LSender.Send(this, $"CSharpArgumentInfoFlags: {aflags.Length} == {tArgs.Length}", Message.Level.Debug);

                if(aflags.Length == tArgs.Length)
                {
                    tArgs = aflags.Select((f, i) =>
                                             ((f & (CSharpArgumentInfoFlags.IsRef | CSharpArgumentInfoFlags.IsOut)) != 0)
                                                   ? tArgs[i].MakeByRefType()
                                                   : tArgs[i]
                                         )
                                         .ToArray();
                }
            }

            if(UseByRef) {
                LSender.Send(this, $"Make ByRef& types for all arguments.", Message.Level.Trace);
                tArgs = tArgs.Select(a => a.IsByRef ? a : a.MakeByRefType()).ToArray();
            }

            if(args.Length != tArgs.Length) {
                throw new ArgumentException($"getArgTypes: length mismatch ({args.Length} == {tArgs.Length})");
            }

            return tArgs;
        }

        private IEnumerable<Type> getGenericArgTypes(InvokeMemberBinder binder)
        {
            // FIXME: avoid access to private members
            return binder
                    .GetPropertyValue("Microsoft.CSharp.RuntimeBinder.ICSharpInvokeOrInvokeMemberBinder.TypeArguments", true)
                    as IEnumerable<Type>;
        }

        private Type[] getTypesFromCallingContext(InvokeMemberBinder binder)
        {
            // FIXME: avoid access to private members

            // +ICSharpInvokeOrInvokeMemberBinder.CallingContext
            var cache = binder.GetFieldValue("Cache", true) as Dictionary<Type, object>;
            Type type = cache?.FirstOrDefault().Key;

            return type?.GetMethod("Invoke")?
                            .GetParameters()
                            .Skip(2) // service args
                            .Select(t => t.ParameterType)
                            .ToArray();
        }

        private CSharpArgumentInfoFlags[] getArgFlags(InvokeMemberBinder binder)
        {
            // FIXME: avoid access to private members

            var info = binder
                        .GetPropertyValue("Microsoft.CSharp.RuntimeBinder.ICSharpInvokeOrInvokeMemberBinder.ArgumentInfo", true)
                        as IEnumerable<CSharpArgumentInfo>;

            return info
                    .Select(i => (CSharpArgumentInfoFlags)i.GetPropertyValue("Flags", true))
                    .Skip(1) // service args
                    .ToArray();
        }
    }
}
