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

using System.Runtime.InteropServices;

namespace net.r_eg.Conari.Core
{
    public interface IProviderDLR
    {
        /// <summary>
        /// To cache dynamic types with similar signatures:
        ///     `{return_type} name( [{argument_types}] )`
        /// </summary>
        bool Cache { get; set; }

        /// <summary>
        /// Current Convention for all dynamic methods.
        /// </summary>
        CallingConvention Convention { get; }

        /// <summary>
        /// To use information about types from CallingContext if it's possible.
        /// This should automatically:
        ///     * Detect all ByRef&amp; types.
        ///     * Bind all null-values for any reference-types that pushed with out/ref modifier.
        /// </summary>
        bool UseCallingContext { get; set; }

        /// <summary>
        /// To use ByRef&amp; (reference-types) for all sent types.
        /// </summary>
        bool UseByRef { get; set; }
    }
}
