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
using System.Reflection;

namespace net.r_eg.Conari.Core
{
    public interface IDynamic
    {
        /// <summary>
        /// To cache dynamic types by default with similar signatures:
        ///     `{return_type} name( [{argument_types}] )`
        /// </summary>
        bool UseCache { get; set; }

        /// <summary>
        /// To reset all cached types.
        /// </summary>
        void resetCache();

        /// <summary>
        /// To cache dynamic types via signature of method.
        /// </summary>
        /// <param name="mi">The instance of MethodInfo.</param>
        /// <returns>false value if this signature is already exists or cannot be cached.</returns>
        bool cache(MethodInfo mi);

        /// <summary>
        /// Extract all valuable types from MethodInfo.
        /// </summary>
        /// <param name="mi"></param>
        /// <returns>The array of types that can be used for MICache containers etc.</returns>
        Type[] getKeyTypes(MethodInfo mi);

        /// <summary>
        /// Gets MethodInfo with empty method.
        /// </summary>
        /// <param name="ret">The return type.</param>
        /// <param name="args">Arguments of method if exists.</param>
        /// <returns>Complete signature as method without body.</returns>
        MethodInfo getMethodInfo(Type ret = null, params Type[] args);

        /// <summary>
        /// Gets MethodInfo with empty method.
        /// </summary>
        /// <param name="cache">Try to find same types from cache. The name cannot be actual if true.</param>
        /// <param name="ret">The return type.</param>
        /// <param name="args">Arguments of method if exists.</param>
        /// <returns>Complete signature as method without body.</returns>
        MethodInfo getMethodInfo(bool cache, Type ret = null, params Type[] args);

        /// <summary>
        /// Gets MethodInfo with empty method.
        /// </summary>
        /// <param name="name">The name of method.</param>
        /// <param name="ret">The return type.</param>
        /// <param name="args">Arguments of method if exists.</param>
        /// <returns>Complete signature as method without body.</returns>
        MethodInfo getMethodInfo(string name, Type ret = null, params Type[] args);

        /// <summary>
        /// Gets MethodInfo with empty method.
        /// </summary>
        /// <param name="name">The name of method.</param>
        /// <param name="cache">Try to find same types from cache. The name cannot be actual if true.</param>
        /// <param name="ret">The return type.</param>
        /// <param name="args">Arguments of method if exists.</param>
        /// <returns>Complete signature as method without body.</returns>
        MethodInfo getMethodInfo(string name, bool cache, Type ret = null, params Type[] args);
    }
}
