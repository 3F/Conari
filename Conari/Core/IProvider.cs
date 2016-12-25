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
using System.Runtime.InteropServices;

namespace net.r_eg.Conari.Core
{
    public interface IProvider: IBinder, IMem
    {
        /// <summary>
        /// When Prefix has been changed.
        /// </summary>
        event EventHandler<DataArgs<string>> PrefixChanged;

        /// <summary>
        /// When Convention has been changed.
        /// </summary>
        event EventHandler<DataArgs<CallingConvention>> ConventionChanged;

        /// <summary>
        /// When handling new non-zero ProcAddress.
        /// </summary>
        event EventHandler<ProcAddressArgs> NewProcAddress;

        /// <summary>
        /// Prefix for exported functions.
        /// </summary>
        string Prefix { get; set; }

        /// <summary>
        /// How should call methods implemented in unmanaged code.
        /// </summary>
        CallingConvention Convention { get; set; }

        /// <summary>
        /// Auto name-decoration to find entry points of exported functions.
        /// </summary>
        bool Mangling { get; set; }

        /// <summary>
        /// Access to exported variables.
        /// </summary>
        IExVar ExVar { get; }

        /// <summary>
        /// Additional services.
        /// </summary>
        IProviderSvc Svc { get; }

        /// <summary>
        /// Returns full lpProcName with main prefix etc.
        /// </summary>
        /// <param name="name">Exported function or variable name.</param>
        string procName(string name);
    }
}
