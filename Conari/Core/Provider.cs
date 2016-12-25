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
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using net.r_eg.Conari.Exceptions;
using net.r_eg.Conari.Log;
using net.r_eg.Conari.Native;
using net.r_eg.Conari.Types;
using net.r_eg.Conari.Types.Methods;
using net.r_eg.Conari.WinAPI;

namespace net.r_eg.Conari.Core
{
    using CMangling = Mangling.C;
    using Method = Method<object, object>;

    public abstract class Provider: Loader, ILoader, IProvider
    {
        /// <summary>
        /// When Prefix has been changed.
        /// </summary>
        public event EventHandler<DataArgs<string>> PrefixChanged = delegate(object sender, DataArgs<string> e) { };

        /// <summary>
        /// When Convention has been changed.
        /// </summary>
        public event EventHandler<DataArgs<CallingConvention>> ConventionChanged = delegate(object sender, DataArgs<CallingConvention> e) { };

        /// <summary>
        /// When handling new non-zero ProcAddress.
        /// </summary>
        public event EventHandler<ProcAddressArgs> NewProcAddress = delegate(object sender, ProcAddressArgs e) { };

        /// <summary>
        /// Prefix for exported functions.
        /// </summary>
        public string Prefix
        {
            get {
                return _prefix;
            }
            set {
                _prefix = value;
                PrefixChanged(this, new DataArgs<string>(value));
            }
        }
        private string _prefix;

        /// <summary>
        /// How should call methods implemented in unmanaged code.
        /// </summary>
        public CallingConvention Convention
        {
            get {
                return _convention;
            }
            set {
                _convention = value;
                ConventionChanged(this, new DataArgs<CallingConvention>(value));
            }
        }
        private CallingConvention _convention;

        /// <summary>
        /// Auto name-decoration to find entry points of exported functions.
        /// </summary>
        public bool Mangling
        {
            get;
            set;
        }

        /// <summary>
        /// Access to exported variables.
        /// </summary>
        public IExVar ExVar
        {
            get {
                if(exvar == null) {
                    exvar = new ExVar(this);
                }
                return exvar;
            }
        }
        protected IExVar exvar;

        /// <summary>
        /// Additional services.
        /// </summary>
        public IProviderSvc Svc
        {
            get {
                if(svc == null) {
                    svc = new ProviderSvc(this);
                }
                return svc;
            }
        }
        private IProviderSvc svc;

        protected sealed class ProviderSvc: IProviderSvc
        {
            private Provider provider;
            
            /// <summary>
            /// Retrieves the address of an exported function or variable.
            /// </summary>
            /// <param name="lpProcName">The name of function or variable, or the function's ordinal value.</param>
            /// <returns>The address if found.</returns>
            public IntPtr getProcAddr(string lpProcName)
            {
                return provider.getProcAddress(lpProcName);
            }

            /// <summary>
            /// Prepare NativeData for active provider.
            /// </summary>
            /// <param name="lpProcName">The name of function or variable, or the function's ordinal value.</param>
            /// <returns></returns>
            public NativeData native(string lpProcName)
            {
                return getProcAddr(lpProcName).Native();
            }

            public ProviderSvc(Provider p)
            {
                provider = p;
            }
        }

        /// <summary>
        /// Binds the exported Function. Full name is required.
        /// </summary>
        /// <typeparam name="T">Type of delegate.</typeparam>
        /// <param name="lpProcName">The full name of exported function.</param>
        /// <returns>Delegate of exported function.</returns>
        public T bindFunc<T>(string lpProcName) where T : class
        {
            return getDelegate<T>(lpProcName);
        }

        /// <summary>
        /// Alias `bindFunc&lt;Action&gt;(string lpProcName)`
        /// Binds the exported Function. Full name is required.
        /// </summary>
        /// <param name="lpProcName">The full name of exported function.</param>
        /// <returns>Delegate of exported function.</returns>
        public Action bindFunc(string lpProcName)
        {
            return bindFunc<Action>(lpProcName);
        }

        /// <summary>
        /// Binds the exported Function.
        /// The main prefix will affects on this result.
        /// </summary>
        /// <typeparam name="T">Type of delegate.</typeparam>
        /// <param name="func">The name of exported function.</param>
        /// <returns>Delegate of exported function.</returns>
        public T bind<T>(string func) where T : class
        {
            return bindFunc<T>(procName(func));
        }

        /// <summary>
        /// Alias `bind&lt;Action&gt;(string func)`
        /// Binds the exported Function.
        /// The main prefix will affects on this result.
        /// </summary>
        /// <param name="func">The name of exported function.</param>
        /// <returns>Delegate of exported function.</returns>
        public Action bind(string func)
        {
            return bind<Action>(func);
        }

        ///// <summary>
        ///// Binds the exported Function via MethodInfo.
        ///// </summary>
        ///// <param name="mi">Prepared signature.</param>
        ///// <param name="prefix">Add prefix to function name from IProvider.Prefix if true.</param>
        ///// <returns>Complete information to create delegates or to invoke methods.</returns>
        //public TDyn bind(MethodInfo mi, bool prefix = false)
        //{
        //    string func = prefix ? procName(mi.Name) : mi.Name;
        //    return wire(mi, func);
        //}

        /// <summary>
        /// Binds the exported Function via MethodInfo and an specific name.
        /// </summary>
        /// <param name="mi">Prepared signature.</param>
        /// <param name="name">Valid function name.</param>
        /// <returns>Complete information to create delegates or to invoke methods.</returns>
        public TDyn bind(MethodInfo mi, string name)
        {
            return wire(mi, name);
        }

        /// <summary>
        /// Binds the exported Function via MethodInfo, an specific name and CallingConvention.
        /// </summary>
        /// <param name="mi">Prepared signature.</param>
        /// <param name="name">Valid function name.</param>
        /// <param name="conv">How it should be called. It overrides only for current method.</param>
        /// <returns>Complete information to create delegates or to invoke methods.</returns>
        public TDyn bind(MethodInfo mi, string name, CallingConvention conv)
        {
            return wire(mi, name, conv);
        }

        /// <summary>
        /// Alias `bindFunc&lt;object&gt;(string lpProcName, Type ret, params Type[] args)`
        /// Binds the exported function.
        /// </summary>
        /// <param name="lpProcName">The full name of exported function.</param>
        /// <param name="ret">The type of return value.</param>
        /// <param name="args">The type of arguments.</param>
        /// <returns>Delegate of exported function.</returns>
        public Method bindFunc(string lpProcName, Type ret, params Type[] args)
        {
            return bindFunc<object>(lpProcName, ret, args);
        }

        /// <summary>
        /// Binds the exported function.
        /// </summary>
        /// <typeparam name="T">The return type for new Delegate should be as T type.</typeparam>
        /// <param name="lpProcName">The full name of exported function.</param>
        /// <param name="ret">The type of return value.</param>
        /// <param name="args">The type of arguments.</param>
        /// <returns>Delegate of exported function.</returns>
        public Method<T, object> bindFunc<T>(string lpProcName, Type ret, params Type[] args)
        {
            MethodInfo mi   = Dynamic.GetMethodInfo(lpProcName, ret, args);
            TDyn dyn        = bind(mi, lpProcName, Convention);

            return delegate (object[] _args) {
                return (T)dyn.dynamic.Invoke(null, _args);
            };
        }

        /// <summary>
        /// Alias `bind&lt;object&gt;(string func, Type ret, params Type[] args)`
        /// Binds the exported C API Function.
        /// </summary>
        /// <param name="func">The name of exported C API function.</param>
        /// <param name="ret">The type of return value.</param>
        /// <param name="args">The type of arguments.</param>
        /// <returns>Delegate of exported function.</returns>
        public Method bind(string func, Type ret, params Type[] args)
        {
            return bind<object>(func, ret, args);
        }

        /// <summary>
        /// Binds the exported C API Function.
        /// </summary>
        /// <typeparam name="T">The return type for new Delegate should be as T type.</typeparam>
        /// <param name="func">The name of exported C API function.</param>
        /// <param name="ret">The type of return value.</param>
        /// <param name="args">The type of arguments.</param>
        /// <returns>Delegate of exported function.</returns>
        public Method<T, object> bind<T>(string func, Type ret, params Type[] args)
        {
            return bindFunc<T>(procName(func), ret, args);
        }

        /// <summary>
        /// Returns full lpProcName with main prefix etc.
        /// </summary>
        /// <param name="name">Exported function or variable name.</param>
        public virtual string procName(string name)
        {
            if(String.IsNullOrWhiteSpace(name)) {
                throw new ArgumentException("The function name cannot be empty or null.");
            }
            return $"{Prefix}{name}";
        }

        /// <summary>
        /// To free memory from the heap allocated from the unmanaged memory.
        /// </summary>
        /// <param name="ptr">The address of the memory to be freed.</param>
        public void free(IntPtr ptr)
        {
            throw new NotImplementedException("Not yet implemented. Please use it from unmanaged code.");
        }

        /// <param name="lpProcName">The name of exported function.</param>
        /// <returns>The address of the exported function.</returns>
        protected IntPtr getProcAddress(string lpProcName)
        {
            if(!Library.IsActive && !load()) {
                throw new LoaderException($"The handle of library is zero. Last loaded library: '{Library.LibName}'");
            }

            return getProcAddress(Library.Handle, lpProcName, Mangling);
        }

        protected IntPtr getProcAddress(IntPtr hModule, string lpProcName, bool mangling = true)
        {
            if(String.IsNullOrWhiteSpace(lpProcName)) {
                throw new ArgumentException($"lpProcName is empty or null.");
            }

            IntPtr pAddr = NativeMethods.GetProcAddress(hModule, lpProcName);
            if(pAddr != IntPtr.Zero) {
                NewProcAddress(this, new ProcAddressArgs(pAddr, hModule, lpProcName));
                return pAddr;
            }

            string msgerr = $"The entry point '{lpProcName}' was not found.";
            if(!mangling) {
                throw new WinFuncFailException(msgerr, true);
            }

            // C-rules

            LSender.Send(this, $"{msgerr} Trying to decorate with C rules.", Message.Level.Warn);

            var func = CMangling.Decorate(lpProcName, ((ILoader)this).ExportFunctionNames);
            if(func == null) {
                throw new EntryPointNotFoundException(
                    $"{msgerr} The `Mangling.C` does not help. Check a correct name manually. Related issue: https://github.com/3F/Conari/issues/3"
                );
            }

            return getProcAddress(hModule, func, false);
        }

        protected T getDelegate<T>(string lpProcName) where T : class
        {
            return getDelegate<T>(getProcAddress(lpProcName));
        }

        protected T getDelegate<T>(IntPtr ptr) where T : class
        {
            return getDelegate<T>(ptr, Convention);
        }

        protected T getDelegate<T>(IntPtr ptr, CallingConvention conv) where T : class
        {
            MethodInfo m    = typeof(T).GetMethod("Invoke");
            TDyn type       = wire(m, ptr, conv);

            return type.dynamic.CreateDelegate(type.declaringType) as T;
        }

        protected TDyn wire(MethodInfo mi, string lpProcName)
        {
            return wire(mi, lpProcName, Convention);
        }

        protected TDyn wire(MethodInfo mi, string lpProcName, CallingConvention conv)
        {
            return wire(mi, getProcAddress(lpProcName), conv);
        }

        protected TDyn wire(MethodInfo mi, IntPtr ptr)
        {
            return wire(mi, ptr, Convention);
        }

        protected TDyn wire(MethodInfo mi, IntPtr ptr, CallingConvention conv)
        {
            Type[] mParams = mi.GetParameters()
                               .Select(p => p.ParameterType)
                               .ToArray();

            DynamicMethod dyn = new DynamicMethod(
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
                il.Emit(OpCodes.Ldc_I8, ptr.ToInt64()); //64bit ptr
            }
            else {
                il.Emit(OpCodes.Ldc_I4, ptr.ToInt32()); //32bit ptr
            }

            il.EmitCalli(OpCodes.Calli, conv, fixTypes(convRetType(mi.ReturnType)), mParams);
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

        /// <summary>
        /// to support of implicit conversions
        /// </summary>
        /// <param name="origin">Base type</param>
        /// <returns></returns>
        protected Type convRetType(Type origin)
        {
            Type found = typeByNativeAttr(origin);
            if(found != null) {
                return found;
            }

            var methods = implicitConvFor(origin).Where(t => t.ReturnType != origin);
            foreach(var m in methods)
            {
                // we will use the first conversion
                if(m.ReturnType.Namespace != typeof(int).Namespace) {
                    return convRetType(m.ReturnType);
                }
                return m.ReturnType;
            }

            return origin;
        }

        protected Type typeByNativeAttr(Type origin)
        {
            // check for the entire struct
            if(origin.GetCustomAttributes(false)
                .Any(a => a.GetType() == typeof(NativeTypeAttribute)))
            {
                return origin;
            }

            // check for implicit conversions
            foreach(MethodInfo m in implicitConvFor(origin))
            {
                if(Attribute.GetCustomAttributes(m)
                    .Any(a => a.GetType() == typeof(NativeTypeAttribute)))
                {
                    return m.ReturnType;
                }
            }

            return null;
        }

        protected virtual IEnumerable<MethodInfo> implicitConvFor(Type type)
        {
            return type.GetMember("op_Implicit", BindingFlags.Public | BindingFlags.Static)
                       .Select(t => (MethodInfo)t);
        }
    }
}
