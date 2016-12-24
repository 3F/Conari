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
using System.Reflection;
using System.Runtime.InteropServices;
using net.r_eg.Conari.Types.Methods;

namespace net.r_eg.Conari.Core
{
    using Method = Method<object, object>;

    public interface IBinder
    {
        /// <summary>
        /// Binds the exported Function. Full name is required.
        /// </summary>
        /// <typeparam name="T">Type of delegate.</typeparam>
        /// <param name="lpProcName">The full name of exported function.</param>
        /// <returns>Delegate of exported function.</returns>
        T bindFunc<T>(string lpProcName) where T : class;

        /// <summary>
        /// Alias `bindFunc&lt;Action&gt;(string lpProcName)`
        /// Binds the exported Function. Full name is required.
        /// </summary>
        /// <param name="lpProcName">The full name of exported function.</param>
        /// <returns>Delegate of exported function.</returns>
        Action bindFunc(string lpProcName);

        /// <summary>
        /// Binds the exported Function.
        /// The main prefix will affects on this result.
        /// </summary>
        /// <typeparam name="T">Type of delegate.</typeparam>
        /// <param name="func">The name of exported function.</param>
        /// <returns>Delegate of exported function.</returns>
        T bind<T>(string func) where T : class;

        /// <summary>
        /// Alias `bind&lt;Action&gt;(string func)`
        /// Binds the exported Function.
        /// The main prefix will affects on this result.
        /// </summary>
        /// <param name="func">The name of exported function.</param>
        /// <returns>Delegate of exported function.</returns>
        Action bind(string func);

        ///// <summary>
        ///// Binds the exported Function via MethodInfo.
        ///// </summary>
        ///// <param name="mi">Prepared signature with valid function name.</param>
        ///// <param name="prefix">Add prefix to function name from IProvider.Prefix if true.</param>
        ///// <returns>
        /////     Complete information to: 
        /////     * create delegates ~ `dyn.CreateDelegate(type.declaringType) as T`
        /////     * or to invoke methods ~ `dyn.Invoke(null, mParams)`
        ///// </returns>
        //TDyn bind(MethodInfo mi, bool prefix = false);

        /// <summary>
        /// Binds the exported Function via MethodInfo and an specific name.
        /// Note: 
        ///     It's recommended as a more efficient, 
        ///     because it allows caching of all MethodInfo for the same signatures but different function names.
        /// 
        ///     Use IProvider.procName() to same control of IProvider.Prefix if needed.
        /// </summary>
        /// <param name="mi">Prepared signature.</param>
        /// <param name="name">Valid function name.</param>
        /// <returns>Complete information to create delegates or to invoke methods.</returns>
        TDyn bind(MethodInfo mi, string name);

        /// <summary>
        /// Binds the exported Function via MethodInfo, an specific name and CallingConvention.
        /// </summary>
        /// <param name="mi">Prepared signature.</param>
        /// <param name="name">Valid function name.</param>
        /// <param name="conv">How it should be called. It overrides only for current method.</param>
        /// <returns>Complete information to create delegates or to invoke methods.</returns>
        TDyn bind(MethodInfo mi, string name, CallingConvention conv);

        /// <summary>
        /// Alias `bindFunc&lt;object&gt;(string lpProcName, Type ret, params Type[] args)`
        /// Binds the exported function.
        /// </summary>
        /// <param name="lpProcName">The full name of exported function.</param>
        /// <param name="ret">The type of return value.</param>
        /// <param name="args">The type of arguments.</param>
        /// <returns>Delegate of exported function.</returns>
        Method bindFunc(string lpProcName, Type ret, params Type[] args);

        /// <summary>
        /// Binds the exported function.
        /// </summary>
        /// <typeparam name="T">The return type for new Delegate should be as T type.</typeparam>
        /// <param name="lpProcName">The full name of exported function.</param>
        /// <param name="ret">The type of return value.</param>
        /// <param name="args">The type of arguments.</param>
        /// <returns>Delegate of exported function.</returns>
        Method<T, object> bindFunc<T>(string lpProcName, Type ret, params Type[] args);

        /// <summary>
        /// Alias `bind&lt;object&gt;(string func, Type ret, params Type[] args)`
        /// Binds the exported C API Function.
        /// </summary>
        /// <param name="func">The name of exported C API function.</param>
        /// <param name="ret">The type of return value.</param>
        /// <param name="args">The type of arguments.</param>
        /// <returns>Delegate of exported function.</returns>
        Method bind(string func, Type ret, params Type[] args);

        /// <summary>
        /// Binds the exported C API Function.
        /// </summary>
        /// <typeparam name="T">The return type for new Delegate should be as T type.</typeparam>
        /// <param name="func">The name of exported C API function.</param>
        /// <param name="ret">The type of return value.</param>
        /// <param name="args">The type of arguments.</param>
        /// <returns>Delegate of exported function.</returns>
        Method<T, object> bind<T>(string func, Type ret, params Type[] args);
    }
}
