/*!
 * Copyright (c) 2016  Denis Kuzmin <x-3F@outlook.com> github/3F
 * Copyright (c) Conari contributors https://github.com/3F/Conari/graphs/contributors
 * Licensed under the MIT License (MIT).
 * See accompanying LICENSE.txt file or visit https://github.com/3F/Conari
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
        /// <param name="name">Valid function name. Full name is required.</param>
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
