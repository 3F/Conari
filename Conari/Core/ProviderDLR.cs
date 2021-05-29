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
using net.r_eg.Conari.Resources;
using net.r_eg.Conari.Types;

namespace net.r_eg.Conari.Core
{
    using RefInfo = Dictionary<int, Type>; // arg index -> Type

    public sealed class ProviderDLR<TCharIn>: DynamicObject, IProviderDLR
        where TCharIn : struct
    {
        private static readonly Type tVoid      = typeof(void);
        private static readonly Type tDefault   = typeof(object);
        private static readonly Type tCharIn    = typeof(TCharIn);
        private static readonly Type tString    = typeof(string);
        private static readonly Type tIntPtr    = typeof(IntPtr);

        private readonly IProvider provider;
        private readonly Lazy<NativeStringManager<TCharIn>> strings;

        private DynamicOptions options;

        public IDynamic DynCfg => Dynamic._;

        public bool Cache
        {
            get => (options & DynamicOptions.Cache) != 0;
            set
            {
                if(value) options |= DynamicOptions.Cache;
                else options &= ~DynamicOptions.Cache;
            }
        }

        public CallingConvention Convention { get; private set; }

        public bool UseCallingContext { get; set; } = true;

        public bool UseByRef { get; set; }

        public object[] TrailingArgs { get; set; } = new object[] { (long)0, (long)0 };

        public float RefModifiableStringBuffer { get; set; } = BufferedString<TCharIn>.BUF;

        public bool SignaturesViaTypeBuilder // 1.4 and less used TypeBuilder
        {
            get => (options & DynamicOptions.GenerateUsingBuilder) != 0;
            set
            {
                if(value) options |= DynamicOptions.GenerateUsingBuilder;
                else options &= ~DynamicOptions.GenerateUsingBuilder;
            }
        }

        public bool TryEvaluateContext { get; set; } = true;

        public bool ManageNativeStrings { get; set; } = true;

        public BoxingType BoxingControl { get; set; } = BoxingType.UnboxingAndBoxing;

        private INativeStringManager<TCharIn> Strings => strings.Value;

        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] input, out object result)
        {
            object[] args;
            if(TrailingArgs == null || Convention != CallingConvention.Cdecl || TrailingArgs.Length < 1)
            {
                args = input;
            }
            else
            {
                args = addTail(input);
            }

            if(ManageNativeStrings) collectNativeStrings(args);

            IEnumerable<Type> tArgs;
            RefInfo refi;

            if(TryEvaluateContext)
            {
                refi = new(args.Length + /*bucket*/1);
                fillRefInfo(refi, binder);
                tArgs = AdaptArgTypes(args, refi, UseByRef);
            }
            else
            {
                tArgs = AdaptArgTypes(args);
                refi = null;
            }

            IEnumerable<Type> tGeneric = AdaptRetTypes(binder.GetGenericArgTypes());

            TDyn dyn = provider.bind
            (
                getmi(binder.Name, tGeneric, tArgs),
                provider.Svc.tryAlias(binder.Name),
                Convention
            );

            object[] unboxed;

            if((BoxingControl & BoxingType.Unboxing) != 0)
            {
                unboxed = unboxing(args, refi);
            }
            else
            {
                unboxed = args;
            }

            object odv = dyn.dynamic.Invoke(null, unboxed);

            if(dyn.returnType == tVoid) // points to generic type in func<returnType>()
            {
                result = odv; // return 'as is' due to unspecified behavior from user space^
            }
            else
            {
                result = Dynamic.DCast(dyn.returnType, odv);
            }

            if((BoxingControl & BoxingType.Boxing) != 0)
            {
                boxing(unboxed, input);
            }
            return true;
        }

        public ProviderDLR(IProvider provider, CallingConvention conv, Lazy<NativeStringManager<TCharIn>> strings)
        {
            this.provider   = provider ?? throw new ArgumentNullException(nameof(provider));
            this.strings    = strings ?? throw new ArgumentNullException(nameof(strings));
            Convention      = conv;
        }

        private static IEnumerable<Type> AdaptRetTypes(IEnumerable<Type> input) 
            => input.Select(t => t == tString ? tCharIn : t);

        private static IEnumerable<Type> AdaptArgTypes(IEnumerable<object> args)
            => args.Select
            (a => (a == null) ? tDefault
                : a is string
                    ? tCharIn
                    : a is IMarshalableGeneric mr
                        ? RevealMarshalableGenericType(mr.MarshalableType)
                        : a is INullType nt
                            ? nt.GenericType : a.GetType());

        private static IEnumerable<Type> AdaptArgTypes(IEnumerable<object> args, RefInfo refi, bool useByRef)
            => AdaptArgTypes(args)
            .Select
            ((t, i) => refi?.ContainsKey(i) == true
                        ? SetRef(t, refi[i]) 
                        : useByRef 
                            ? SetRef(t, null) : t);

        private static Type SetRef(Type input, Type refi)
        {
            if(input == tCharIn) return input;
            return refi ?? input.MakeByRefType();
        }

        private static Type RevealMarshalableGenericType(Type input)
        {
            if(input.GetInterface(typeof(IPtr).FullName) == null) {
                return tIntPtr;
            }
            return input; // such as CharPtr, WCharPtr, etc.
        }

        private static object RevealMarshalableValue(IMarshalableGeneric mr, object input)
        {
            if(mr.MarshalableType.GetInterface(typeof(IPtr).FullName) == null) {
                return mr.AddressPtr;
            }
            return Dynamic.DCast(mr.MarshalableType, input); // such as NativeString<T>, etc.
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

        private MethodInfo getmi(string name, IEnumerable<Type> generic, IEnumerable<Type> args)
        {
            if(generic?.ElementAtOrDefault(1) != null) { //TODO: we can actually improve this but do we really need it?
                throw new NotSupportedException(Msg.dlr_only_one_type_allowed);
            }

            return Dynamic.GetMethodInfo
            (
                options,
                name,
                generic.ElementAtOrDefault(0),
                args.ToArray()
            );
        }

        private void collectNativeStrings(object[] args)
        {
            foreach(object a in args ?? throw new ArgumentNullException(nameof(args)))
            {
                if(a is INativeString ns && ns.UseManager)
                {
                    if(ns.Disposed || tCharIn != ns.MarshalableType) throw new NotSupportedException();
                    Strings.add((NativeString<TCharIn>)a);
                }
            }
        }

        private object[] unboxing(object[] args, RefInfo refi)
        {
            object[] r = new object[args.Length];
            for(int i = 0; i < args.Length; ++i)
            {
                r[i] = args[i];

                if(r[i] is IBoxed boxed) r[i] = boxed.Data;
                if(r[i] is string str) r[i] = makeCstr(str, refi, i);
                if(r[i] is IMarshalableGeneric mr) r[i] = RevealMarshalableValue(mr, r[i]);
            }
            return r;
        }

        private NativeString<TCharIn> makeCstr(string str, RefInfo refi, int idx)
        {
            if(refi?.ContainsKey(idx) == true) {
                return Strings.cstr(str, 0);
            }
            return Strings.cstr(str, str.RelativeLength(RefModifiableStringBuffer));
        }

        private void boxing(object[] src, object[] dst)
        {
            if(src.Length < dst.Length) {
                throw new ArgumentException(Msg.incorrect_args_length_0_1.Format($"{src.Length}", $"{dst.Length}"));
            }

            for(int i = 0; i < dst.Length; ++i)
            {
                if(dst[i] is IBoxed boxed)
                {
                    boxed.Data = src[i];
                }
                else if(dst[i] is string)
                {
                    dst[i] = src[i].ToString(); // update since ByRef& is possible  

                    // TODO: L-176. It seem may produce AccessViolationException in some tests,
                    // such as BindingContextTest (dlrStringTest3(), lambdaStringTest2()) ...
                    // Strings.release(((IPtr)src[i]).AddressPtr);
                }
                else
                {
                    // update everything since ByRef& updated value is possible
                    dst[i] = src[i];
                }
            }
        }

        private object[] addTail(object[] args)
        {
            object[] ret = new object[args.Length + TrailingArgs.Length];

            args.CopyTo(ret, 0);
            TrailingArgs.CopyTo(ret, args.Length);
            return ret;
        }

        private void fillRefInfo(RefInfo input, InvokeMemberBinder binder)
        {
            int idx = 0;
            if(UseCallingContext)
            {
                foreach(Type t in GetTypesFromCallingContext(binder))
                {
                    if(t.IsByRef) input[idx] = t;
                    ++idx;
                }
                return;
            }

            CSharpArgumentInfoFlags fByRef = CSharpArgumentInfoFlags.IsRef | CSharpArgumentInfoFlags.IsOut;
            foreach(var f in GetArgFlags(binder))
            {
                if((f & fByRef) != 0) input[idx] = null;
                ++idx;
            }
        }
    }
}
