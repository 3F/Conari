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
using net.r_eg.Conari.Types;
using net.r_eg.Conari.WinAPI;

namespace net.r_eg.Conari.Core
{
    public abstract class Provider: Loader, ILoader, IProvider
    {
        /// <summary>
        /// Prefix for exported functions.
        /// </summary>
        public string Prefix
        {
            get;
            set;
        }

        /// <summary>
        /// How should call methods implemented in unmanaged code.
        /// </summary>
        public CallingConvention Convention
        {
            get;
            set;
        }

        /// <summary>
        /// Binds the exported function.
        /// </summary>
        /// <typeparam name="T">Type of delegate.</typeparam>
        /// <param name="lpProcName">The full name of exported function.</param>
        /// <returns>Delegate of exported function.</returns>
        public T bindFunc<T>(string lpProcName) where T : class
        {
            return getDelegate<T>(lpProcName);
        }

        /// <summary>
        /// Binds the exported C API Function.
        /// </summary>
        /// <typeparam name="T">Type of delegate.</typeparam>
        /// <param name="func">The name of exported C API function.</param>
        /// <returns>Delegate of exported function.</returns>
        public T bind<T>(string func) where T : class
        {
            return bindFunc<T>(funcName(func));
        }

        /// <summary>
        /// Binds the exported Function via MethodInfo.
        /// </summary>
        /// <param name="mi">Prepared signature.</param>
        /// <param name="prefix">Add prefix to function name from IProvider.Prefix if true.</param>
        /// <returns>Complete information to create delegates or to invoke methods.</returns>
        public TDyn bind(MethodInfo mi, bool prefix = false)
        {
            string func = prefix ? funcName(mi.Name) : mi.Name;
            return wire(mi, func);
        }

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
        /// Returns full name of exported function.
        /// </summary>
        /// <param name="name">short function name.</param>
        public virtual string funcName(string name)
        {
            if(String.IsNullOrWhiteSpace(name)) {
                throw new ArgumentException("The function name cannot be empty or null.");
            }
            return $"{Prefix}{name}";
        }

        /// <param name="lpProcName">The name of exported function.</param>
        /// <returns>The address of the exported function.</returns>
        protected IntPtr getProcAddress(string lpProcName)
        {
            if(!Library.IsActive && !load()) {
                throw new LoaderException($"The handle of library is zero. Last loaded library: '{Library.LibName}'");
            }

            IntPtr pAddr = NativeMethods.GetProcAddress(Library.Handle, lpProcName);
            if(pAddr == IntPtr.Zero) {
                throw new WinFuncFailException($"The entry point '{lpProcName}' was not found.", true);
            }

            return pAddr;
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
            return wire(mi, getProcAddress(lpProcName));
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

            il.EmitCalli(OpCodes.Calli, conv, convRetType(mi.ReturnType), mParams);
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
