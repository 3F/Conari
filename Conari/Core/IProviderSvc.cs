﻿/*
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
using net.r_eg.Conari.Native;

namespace net.r_eg.Conari.Core
{
    public interface IProviderSvc
    {
        /// <summary>
        /// Retrieves the address of an exported function or variable.
        /// </summary>
        /// <param name="lpProcName">The name of function or variable, or the function's ordinal value.</param>
        /// <returns>The address if found.</returns>
        IntPtr getProcAddr(LpProcName lpProcName);

        /// <summary>
        /// Prepare NativeData for active provider.
        /// </summary>
        /// <param name="lpProcName">The name of function or variable, or the function's ordinal value.</param>
        /// <returns></returns>
        NativeData native(LpProcName lpProcName);

        /// <summary>
        /// Prepare NativeData for active provider.
        /// </summary>
        /// <param name="lpProcName">The name of function or variable, or the function's ordinal value.</param>
        /// <param name="prefix">Add prefix if used.</param>
        /// <returns></returns>
        NativeData native(string lpProcName, bool prefix = false);

        /// <summary>
        /// Extracts LpProcName.
        /// </summary>
        /// <param name="lpProcName">Original lpProcName.</param>
        /// <param name="prefix">Add prefix if used.</param>
        /// <returns></returns>
        LpProcName procName(string lpProcName, bool prefix);

        /// <summary>
        /// Try to get alias.
        /// </summary>
        /// <param name="name">Possible alias name.</param>
        /// <returns></returns>
        string tryAlias(string name);
    }
}
