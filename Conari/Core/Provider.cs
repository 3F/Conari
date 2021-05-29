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
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using net.r_eg.Conari.Aliases;
using net.r_eg.Conari.Exceptions;
using net.r_eg.Conari.Extension;
using net.r_eg.Conari.Log;
using net.r_eg.Conari.Native;
using net.r_eg.Conari.PE;
using net.r_eg.Conari.Resources;
using net.r_eg.Conari.Types.Methods;
using net.r_eg.Conari.WinAPI;
using net.r_eg.DotNet.System.Reflection.Emit;

namespace net.r_eg.Conari.Core
{
    using Method = Method<object, object>;

    public abstract class Provider: Loader, ILoader, IProvider
    {
        public event EventHandler<DataArgs<string>> PrefixChanged = delegate(object sender, DataArgs<string> e) { };

        public event EventHandler<DataArgs<CallingConvention>> ConventionChanged = delegate(object sender, DataArgs<CallingConvention> e) { };

        public event EventHandler<ProcAddressArgs> NewProcAddress = delegate(object sender, ProcAddressArgs e) { };

        protected PAddrCache<Delegate> xDelegates = new();
        protected PAddrCache<TDyn> xTDyn = new();

        protected bool _cache = true;
        protected IExVar exvar;
        private string _prefix;
        private CallingConvention _convention = CallingConvention.Cdecl;
        private bool _mangling;

        private readonly AliasDict aliasDict;

        public bool Cache
        {
            get => _cache;
            set
            {
                if(!value) {
                    xDelegates.Clear();
                    xTDyn.Clear();
                }
                _cache = value;
            }
        }

        public string Prefix
        {
            get => _prefix;
            set
            {
                _prefix = value;
                PrefixChanged(this, new DataArgs<string>(value));
            }
        }

        public CallingConvention Convention
        {
            get => _convention;
            set
            {
                _convention = value;
                ConventionChanged(this, new DataArgs<CallingConvention>(value));
            }
        }

        public bool Mangling
        {
            get => _mangling;
            set
            {
                if(LLCfg?.peImplementation == PeImplType.Disabled) throw new NotSupportedException(Msg.activate_pe);
                _mangling = value;
            }
        }

        public Dictionary<string, ProcAlias> Aliases => aliasDict?.Aliases;

        public IExVar ExVar { get
        {
            if(exvar == null) {
                exvar = new ExVar(this);
            }
            return exvar;
        }}

        public IProviderSvc Svc { get; }

        #region IProviderSvc

        protected sealed class ProviderSvc: IProviderSvc
        {
            private readonly Provider provider;

            public IntPtr getProcAddr(LpProcName lpProcName) => provider.getProcAddress(lpProcName);

            public NativeData native(LpProcName lpProcName) => getProcAddr(lpProcName).Native();

            public NativeData native(string lpProcName, bool prefix = false) => native(procName(lpProcName, prefix));

            public LpProcName procName(string lpProcName, bool prefix) => provider.procName(lpProcName, prefix);

            public string tryAlias(string name) => provider.tryAlias(procName(name, true));

            public ProviderSvc(Provider p) => provider = p;
        }

        #endregion

        public T bindFunc<T>(string lpProcName) where T : class
        {
            return getDelegate<T>(procName(lpProcName, false));
        }

        public Action bindFunc(string lpProcName) => bindFunc<Action>(lpProcName);

        public T bind<T>(string func) where T : class
        {
            return getDelegate<T>(procName(func, true));
        }

        public Action bind(string func) => bind<Action>(func);

        public TDyn bind(MethodInfo mi, string name) => wire(mi, procName(name, false));

        public TDyn bind(MethodInfo mi, string name, CallingConvention conv)
        {
            return wire(mi, procName(name, false), conv);
        }

        public Method bindFunc(string lpProcName, Type ret, params Type[] args)
        {
            return bindFunc<object>(lpProcName, ret, args);
        }

        public Method<T, object> bindFunc<T>(string lpProcName, Type ret, params Type[] args)
        {
            return bind<T>(procName(lpProcName, false), ret, args);
        }

        public Method bind(string func, Type ret, params Type[] args)
        {
            return bind<object>(func, ret, args);
        }

        public Method<T, object> bind<T>(string func, Type ret, params Type[] args)
        {
            return bind<T>(procName(func, true), ret, args);
        }

        public virtual string procName(string name)
        {
            if(string.IsNullOrWhiteSpace(name)) {
                throw new ArgumentNullException(nameof(name));
            }
            return $"{Prefix}{name}";
        }

        public IntPtr addr(LpProcName item) => Svc.getProcAddr(item);

        public void free(IntPtr ptr) => throw new NotImplementedException(Msg.not_yet_impl);

        protected Provider()
        {
            Svc         = new ProviderSvc(this);
            aliasDict   = new AliasDict(this);
        }

        /// <param name="lpProcName">The name of exported function.</param>
        /// <returns>The address of the exported function.</returns>
        protected IntPtr getProcAddress(LpProcName lpProcName)
        {
            if(Disposed) throw new CommonException($"{GetType()} was disposed.");

            if(!Library.IsActive && !load())
            {
                if(Library.cancelled) throw new LoadCancelledException(Library.module);
                throw new LoaderException($"Module `{Library}` is not loaded or corrupted.");
            }

            return getProcAddress
            (
                Library.handle,
                tryAlias(lpProcName), 
                Mangling
            );
        }

        protected IntPtr getProcAddress(IntPtr hModule, string lpProcName, bool mangling = true)
        {
            if(string.IsNullOrWhiteSpace(lpProcName)) {
                throw new ArgumentException(Msg.arg_0_empty_or_null.Format(nameof(lpProcName)));
            }

            IntPtr pAddr = NativeMethods.GetProcAddress(hModule, lpProcName);
            if(pAddr != IntPtr.Zero) {
                NewProcAddress(this, new ProcAddressArgs(pAddr, hModule, lpProcName));
                return pAddr;
            }

            string msgerr = Msg.proc_not_found.Format(lpProcName);
            if(!mangling) {
                throw new WinFuncFailException(msgerr, true);
            }

            // C-rules

            LSender.Send(this, $"{msgerr} {Msg.trying_decorate_with_0.Format(nameof(Conari.Mangling.C))}", Message.Level.Warn);

            IPE pe = ((ILoader)this).PE ?? throw new NotSupportedException(Msg.activate_pe);
            var proc = Conari.Mangling.C.Decorate(lpProcName, pe.Export.Names);
            if(proc == null)
            {
                throw new EntryPointNotFoundException(
                    $"{msgerr} {Msg.mangling_0_does_not_help.Format(nameof(Conari.Mangling.C))}"
                );
            }

            return getProcAddress(hModule, proc, false);
        }

        /// <typeparam name="T">The return type for new Delegate must be as T type.</typeparam>
        /// <param name="lpProcName"></param>
        /// <param name="ret">The type for return value.</param>
        /// <param name="args">Argument types.</param>
        /// <returns>Invokable delegate.</returns>
        protected Method<T, object> bind<T>(LpProcName lpProcName, Type ret, params Type[] args)
        {
            MethodInfo mi   = Dynamic.GetMethodInfo((string)lpProcName, ret, args);
            TDyn dyn        = wire(mi, lpProcName, Convention);

            return delegate(object[] _args) {
                return (T)dyn.dynamic.Invoke(null, _args);
            };
        }

        protected LpProcName procName(string lpProcName, bool prefix) => new
        (
            lpProcName, 
            prefix ? procName(lpProcName) : null
        );

        protected T getDelegate<T>(LpProcName lpProcName) where T : class
        {
            return getDelegate<T>(getProcAddress(lpProcName));
        }

        protected T getDelegate<T>(IntPtr ptr) where T : class
        {
            return getDelegate<T>(ptr, Convention);
        }

        protected T getDelegate<T>(IntPtr ptr, CallingConvention conv) where T : class
        {
            if(!Cache) {
                return getDelegateNoCache<T>(ptr, conv) as T;
            }

            Type sig = typeof(T);

            var key = xDelegates.k(ptr, conv, sig);
            if(!xDelegates.ContainsKey(key)) {
                xDelegates[key] = getDelegateNoCache(sig, ptr, conv);
            }
            return xDelegates[key] as T;
        }

        protected Delegate getDelegateNoCache<T>(IntPtr ptr, CallingConvention conv) where T : class
            => getDelegateNoCache(typeof(T), ptr, conv);

        protected Delegate getDelegateNoCache(Type sig, IntPtr ptr, CallingConvention conv)
        {
            MethodInfo m    = sig.GetMethod("Invoke");
            TDyn type       = wire(m, ptr, conv);

            return type.dynamic.CreateDelegate(m.DeclaringType);
        }

        protected TDyn wire(MethodInfo mi, LpProcName lpProcName)
        {
            return wire(mi, lpProcName, Convention);
        }

        protected TDyn wire(MethodInfo mi, LpProcName lpProcName, CallingConvention conv)
        {
            return wire(mi, getProcAddress(lpProcName), conv);
        }

        protected TDyn wire(MethodInfo mi, IntPtr ptr)
        {
            return wire(mi, ptr, Convention);
        }

        protected TDyn wire(MethodInfo mi, IntPtr ptr, CallingConvention conv)
        {
            if(!Cache) {
                return wireNoCache(mi, ptr, conv);
            }

            var key = xTDyn.k(ptr, conv, mi.DeclaringType);
            if(!xTDyn.ContainsKey(key)) {
                xTDyn[key] = wireNoCache(mi, ptr, conv);
            }
            return xTDyn[key];
        }

        protected TDyn wireNoCache(MethodInfo mi, IntPtr ptr, CallingConvention conv)
        {
            Type[] mParams = mi.GetParameters()
                               .Select(p => p.ParameterType)
                               .ToArray();

            DynamicMethod dyn = new
            (
                mi.Name, 
                mi.ReturnType, 
                mParams, 
                typeof(Delegate), 
                skipVisibility: true
            );

            ILGenerator il = dyn.GetILGenerator();
            /*
              Calli - Stack behavior:
              https://msdn.microsoft.com/en-us/library/system.reflection.emit.opcodes.calli.aspx

                1. Method arguments are pushed onto the stack.
                2. The method entry pointer is pushed onto the stack.
                3. Method arguments and the entry pointer are popped from the stack. 
                   The call to the method is performed. When complete, a return value is generated by the callee method and sent to the caller.
                4. The return value is pushed onto the stack.
            */

            for(int i = 0; i < mParams.Length; ++i) {
                il.Emit(OpCodes.Ldarg, i);
            }

            if(IntPtr.Size == sizeof(Int64)) {
                il.Emit(OpCodes.Ldc_I8, ptr.ToInt64());
            }
            else {
                il.Emit(OpCodes.Ldc_I4, ptr.ToInt32());
            }

            // https://github.com/3F/Conari/issues/6
            Type tret = fixTypes(mi.ReturnType.TypeDeepByNativeAttr());

            il.EmitCalliStd(conv, tret, mParams);

            il.Emit(OpCodes.Ret);

            return new TDyn()
            {
                dynamic         = dyn,
                method          = mi,
                args            = mParams,
                returnType      = mi.ReturnType,
                declaringType   = mi.DeclaringType,
                convention      = conv
            };
        }

        /// <summary>
        /// Fixes for specific types like a bool -> I1 etc.
        /// https://github.com/3F/Conari/issues/6
        /// </summary>
        /// <param name="origin">Base type</param>
        /// <returns></returns>
        protected virtual Type fixTypes(Type origin)
        {
            if(origin == typeof(bool)) {
                return typeof(byte);
            }

            return origin;
        }

        private string tryAlias(LpProcName lpProcName)
        {
            return (aliasDict == null) ? (string)lpProcName : aliasDict.use(lpProcName);
        }
    }
}
