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
    public sealed class ProviderDLR<TCharIn>: DynamicObject, IProviderDLR
        where TCharIn : struct
    {
        private static readonly Type TypeVoid       = typeof(void);
        private static readonly Type TypeDefault    = typeof(object);
        private static readonly Type TypeCharIn     = typeof(TCharIn);
        private static readonly Type TypeString     = typeof(string);

        private readonly IProvider provider;
        private readonly Lazy<NativeStringManager<TCharIn>> strings;

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

        public float RefModifiableStringBuffer { get; set; } = BufferedString<TCharIn>.BUF;

        private INativeStringManager<TCharIn> Strings => strings.Value;

        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] input, out object result)
        {
            collectNativeStrings(input);

            object[] args = addTail(input);

            Type[] tArgs;
            _RefInfo[] refi;
            try
            {
                refi    = getRefInfo(binder).ToArray();
                tArgs   = getArgTypes(binder, args, refi);
            }
            catch(ArgumentException ex)
            {
                LSender.Send(this, Msg.dlr_args_unknown_problems_0_1.Format($"{binder.Name}'", $"{ex.Message}"), Message.Level.Warn);
                tArgs   = args.Select(a => a?.GetType() ?? TypeDefault).ToArray();
                refi    = null;
            }

            Type[] tGeneric = AdaptRetTypes(binder.GetGenericArgTypes()).ToArray();

            TDyn dyn = provider.bind
            (
                Getmi(binder.Name, tGeneric, tArgs),
                provider.Svc.tryAlias(binder.Name),
                Convention
            );

            // Boxing types, for example: NullType -> null -> NullType

            object[] unboxed = unboxing(args, refi);

            object odv = dyn.dynamic.Invoke(null, unboxed);

            if(dyn.returnType == TypeVoid) // points to generic type in func<returnType>()
            {
                result = odv; // return 'as is' due to unspecified behaviour from user space^
            }
            else
            {
                result = Dynamic.DCast(dyn.returnType, odv);
            }

            boxing(unboxed, input);
            return true;
        }

        public ProviderDLR(IProvider provider, CallingConvention conv, Lazy<NativeStringManager<TCharIn>> strings)
        {
            this.provider   = provider ?? throw new ArgumentNullException(nameof(provider));
            this.strings    = strings ?? throw new ArgumentNullException(nameof(strings));
            Convention      = conv;
        }

        private static IEnumerable<Type> AdaptRetTypes(IEnumerable<Type> input) 
            => input.Select(t => t == TypeString ? TypeCharIn : t);

        private static IEnumerable<Type> AdaptArgTypes(object[] args)
            => args.Select(a =>
                (a == null)
                    ? TypeDefault
                    : a is string
                        ? TypeCharIn
                        : a is IMarshalableGeneric mr
                            ? mr.MarshalableType
                            : a is INullType nt
                                ? nt.GenericType
                                : a.GetType());

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

        private void collectNativeStrings(object[] args)
        {
            foreach(object a in args ?? throw new ArgumentNullException(nameof(args)))
            {
                if(a is INativeString ns && ns.UseManager)
                {
                    if(ns.Disposed || TypeCharIn != ns.MarshalableType) throw new NotSupportedException();
                    Strings.add((NativeString<TCharIn>)a);
                }
            }
        }

        private object[] unboxing(object[] args, _RefInfo[] refi)
            => args
                .Select(a => a is IBoxed boxed ? boxed.Data : a)
                .Select((a, i) => a is string str ? makeCstr(str, refi, i) : a)
                .Select(a => a is IMarshalableGeneric mr ? Dynamic.DCast(mr.MarshalableType, a) : a)
                .ToArray();

        private NativeString<TCharIn> makeCstr(string str, _RefInfo[] refi, int idx)
        {
            if(refi == null || refi.Length <= idx || !refi[idx].isRef) {
                return Strings.cstr(str, 0);
            }
            return Strings.cstr(str, str.RelativeLength(RefModifiableStringBuffer));
        }

        private void boxing(object[] from, object[] to)
        {
            if(from.Length < to.Length) {
                throw new ArgumentException(Msg.incorrect_args_length_0_1.Format($"{from.Length}", $"{to.Length}"));
            }

            for(int i = 0; i < to.Length; ++i)
            {
                if(to[i] is IBoxed boxed)
                {
                    boxed.Data = from[i];
                }
                else if(to[i] is string)
                {
                    to[i] = from[i].ToString();
                    Strings.release(((IPtr)from[i]).AddressPtr);
                }
                else
                {
                    // update everything since ByRef& updated value is possible
                    to[i] = from[i];
                }
            }
        }

        private object[] addTail(object[] args)
        {
            if(args == null) throw new ArgumentNullException(nameof(args));
            if(TrailingArgs == null || Convention != CallingConvention.Cdecl || TrailingArgs.Length < 1) return args;

            return args.Concat(TrailingArgs).ToArray();
        }

        private Type[] getArgTypes(InvokeMemberBinder binder, object[] args, _RefInfo[] refi)
        {
            // we're trying to resolve context because initial args may not provide info about reference-types (ByRef&)

            Type[] ret = AdaptArgTypes(args).ToArray();

            if(ret.Length < refi.Length) {
                throw new ArgumentException(Msg.incorrect_args_length_0_1.Format($"{ret.Length}", $"{refi.Length}"));
            }

            refi.ForEach((c, i) =>
            {
                if((c.isRef || UseByRef) && ret[i] != TypeCharIn)
                {
                    ret[i] = c.type ?? ret[i].MakeByRefType();
                    refi[i].isRef = true;
                }
            });

            return ret;
        }

        private IEnumerable<_RefInfo> getRefInfo(InvokeMemberBinder binder)
        {
            if(UseCallingContext) {
                return GetTypesFromCallingContext(binder).Select(c => new _RefInfo(c, c.IsByRef));
            }

            CSharpArgumentInfoFlags fByRef = CSharpArgumentInfoFlags.IsRef | CSharpArgumentInfoFlags.IsOut;
            return GetArgFlags(binder).Select(f => new _RefInfo((f & fByRef) != 0));
        }

        private sealed class _RefInfo
        {
            public readonly Type type;
            public bool isRef;

            public _RefInfo(bool isRef) => this.isRef = isRef;

            public _RefInfo(Type type, bool isRef)
                : this(isRef)
            {
                this.type = type;
            }
        }
    }
}
