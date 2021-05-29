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

using System.Reflection.Emit;
using System.Runtime.InteropServices;
using net.r_eg.Conari.Core.Runtime;
using net.r_eg.Conari.Types;

namespace net.r_eg.Conari.Core
{
    public interface IProviderDLR
    {
        /// <summary>
        /// Access to used <see cref="IDynamic"/> object.
        /// </summary>
        IDynamic DynCfg { get; }

        /// <summary>
        /// To use cache for dynamic types etc.
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
        /// Forced use ByRef&amp; (reference-types) for all sent types.
        /// </summary>
        bool UseByRef { get; set; }

        /// <summary>
        /// Additional arguments at the end of calling.
        /// Can be applied only if varargs.
        /// null value will disable this.
        /// </summary>
        /// <remarks>Useful in avoiding any optional values. Eg. (1, 2, 3, [0, 0])</remarks>
        object[] TrailingArgs { get; set; }

        /// <summary>
        /// An additional buffer to process ref strings (ByRef&amp;).
        /// Factor is how many times to increase.
        /// </summary>
        /// <remarks>Eg. 0 - do not use buffer; 0.5 - half of input; 1 - same to input; 2 - twice...</remarks>
        float RefModifiableStringBuffer { get; set; }

        /// <summary>
        /// Use <see cref="AssemblyBuilder"/> + <see cref="TypeBuilder"/> + <see cref="MethodBuilder"/> to generate methods signatures.
        /// Otherwise use lightweight <see cref="NoDeclMethodInfo"/> wrapper.
        /// </summary>
        bool SignaturesViaTypeBuilder { get; set; }

        /// <summary>
        /// It can help better understand what are you trying to call without specified casting.
        /// </summary>
        /// <remarks>Affects performance.</remarks>
        bool TryEvaluateContext { get; set; }

        /// <summary>
        /// Collect information about all input <see cref="INativeString"/> and delegate control.
        /// </summary>
        bool ManageNativeStrings { get; set; }

        /// <summary>
        /// Control of boxing for input data.
        /// </summary>
        /// <remarks>Affects performance.</remarks>
        BoxingType BoxingControl { get; set; }
    }
}
