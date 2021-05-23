/*
 * The MIT License (MIT)
 *
 * Copyright (c) 2016-2021  Denis Kuzmin <x-3F@outlook.com> github/3F
 * Copyright (c) Conari contributors https://github.com/3F/Conari/graphs/contributors
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
using net.r_eg.Conari.Resources;
using net.r_eg.Conari.Types;

namespace net.r_eg.Conari.Core
{
    public sealed class ProviderDLR: DynamicObject, IProviderDLR
    {
        private readonly IProvider provider;

        public IDynamic DynCfg => new Dynamic();

        public bool Cache
        {
            get => DynCfg.UseCache;
            set => DynCfg.UseCache = value;
        }

        public CallingConvention Convention { get; private set; }

        public bool UseCallingContext { get; set; } = true;

        public bool UseByRef { get; set; }

        public object[] TrailingArgs { get; set; } = new object[] { (long)0, (long)0 };

        /// <summary>
        /// Magic method. Invoking.
        /// </summary>
        /// <remarks>`[result =] name&lt;return_type&gt;([{argument_types}])`</remarks>
        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] input, out object result)
        {
            object[] args = addTail(input);
            Type[] tArgs;

            try
            {
                tArgs = getArgTypes(binder, args);
            }
            catch(ArgumentException ex)
            {
                LSender.Send(this, Msg.dlr_args_unknown_problems_0_1.Format($"{binder.Name}'", $"{ex.Message}"), Message.Level.Warn);
                tArgs = args.Select(a => a?.GetType() ?? typeof(object)).ToArray();
            }

            Type[] tGeneric = binder.GetGenericArgTypes().ToArray();

            TDyn dyn = provider.bind
            (
                Getmi(binder.Name, tGeneric, tArgs),
                provider.Svc.tryAlias(binder.Name),
                Convention
            );

            // Boxing types, for example: NullType -> null -> NullType

            object[] unboxed = Unboxing(args);

            object odv = dyn.dynamic.Invoke(null, unboxed);

            if(dyn.returnType == typeof(void)) // points to generic type in func<returnType>()
            {
                result = odv; // return 'as is' due to unspecified behaviour from user space^
            }
            else
            {
                result = Dynamic.DCast(dyn.returnType, odv);
            }

            Boxing(unboxed, input);
            return true;
        }

        public ProviderDLR(IProvider provider, CallingConvention conv)
        {
            this.provider   = provider ?? throw new ArgumentNullException(nameof(provider));
            Convention      = conv;
        }

        private static object[] Unboxing(object[] args)
            => args
                .Select(a => a is IBoxed boxed ? boxed.Data : a)
                .Select(a => a is IMarshalableGeneric mr ? Dynamic.DCast(mr.MarshalableType, a) : a)
                .ToArray();

        private static void Boxing(object[] from, object[] to)
        {
            if(from.Length < to.Length) {
                throw new ArgumentException(Msg.incorrect_args_length_0_1.Format($"{from.Length}", $"{to.Length}"));
            }

            for(int i = 0; i < to.Length; ++i)
            {
                // update everything since ByRef& updated value is possible

                if(to[i] is IBoxed boxed)
                {
                    boxed.Data = from[i];
                }
                else
                {
                    to[i] = from[i];
                }
            }
        }

        private static IEnumerable<Type> GetTypesFromCallingContext(InvokeMemberBinder binder)
            => binder
                .GetCallingContext()?
                .GetMethod("Invoke")?
                .GetParameters()
                .Skip(2) // service args
                .Select(t => t.ParameterType);

        private static IEnumerable<CSharpArgumentInfoFlags> GetArgFlags(InvokeMemberBinder binder)
            => binder
                .GetArgInfo()
                .Select(i => (CSharpArgumentInfoFlags)i.GetPropertyValue("Flags", true))
                .Skip(1); // service args

        private static MethodInfo Getmi(string name, Type[] generic, Type[] args)
        {
            if(generic?.Length > 1) { //TODO: we can actually improve this but do we really need it?
                throw new NotSupportedException(Msg.dlr_only_one_type_allowed);
            }

            return Dynamic.GetMethodInfo(
                name,
                (generic == null || generic.Length < 1) ? null : generic[0],
                args
            );
        }

        private object[] addTail(object[] args)
        {
            if(args == null) throw new ArgumentNullException(nameof(args));
            if(TrailingArgs == null || Convention != CallingConvention.Cdecl || TrailingArgs.Length < 1) return args;

            return args.Concat(TrailingArgs).ToArray();
        }

        private Type[] getArgTypes(InvokeMemberBinder binder, object[] args)
        {
            var tArgs = args.Select(a =>
                (a == null)
                    ? typeof(object)
                    : a is IMarshalableGeneric mr 
                        ? mr.MarshalableType 
                        : a is INullType nt 
                            ? nt.GenericType // null-value via NullType<IData> -> IData
                            : a.GetType()
            );

            if(UseByRef) return tArgs.Select(a => a.MakeByRefType()).ToArray();

            // we're trying to resolve context because args may not provide info about reference-types (ByRef&)

            Type[] ret = tArgs.ToArray();

            if(UseCallingContext)
            {
                Type[] tContext = GetTypesFromCallingContext(binder).ToArray();
                if(ret.Length < tContext.Length) {
                    throw new ArgumentException(Msg.incorrect_args_length_0_1.Format($"{ret.Length}", $"{tContext.Length}"));
                }

                tContext.ForEach((c, i) => { if(c.IsByRef) { ret[i] = c; } });
                return ret;
            }

            CSharpArgumentInfoFlags[] aflags = GetArgFlags(binder).ToArray();
            if(ret.Length < aflags.Length) {
                throw new ArgumentException(Msg.incorrect_args_length_0_1.Format($"{ret.Length}", $"{aflags.Length}"));
            }

            CSharpArgumentInfoFlags fByRef = CSharpArgumentInfoFlags.IsRef | CSharpArgumentInfoFlags.IsOut;
            aflags.ForEach((f, i) =>
            {
                if((f & fByRef) != 0) {
                    ret[i] = ret[i].MakeByRefType();
                }
            });

            return ret;
        }
    }
}
