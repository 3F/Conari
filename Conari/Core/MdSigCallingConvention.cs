/*
 * The MIT License (MIT)
 *
 * Copyright (c) 2016-2020  Denis Kuzmin < x-3F@outlook.com > GitHub/3F
 * Copyright (c) Conari contributors: https://github.com/3F/Conari/graphs/contributors
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

namespace net.r_eg.Conari.Core
{
    /// <summary>
    /// For internal m_signature processing with unmanaged EmitCalli.
    /// See Provider.
    /// </summary>
    internal enum MdSigCallingConvention: byte
    {
        Default         = 0x00,

        //
        C               = 0x01,
        StdCall         = 0x02,
        ThisCall        = 0x03,
        FastCall        = 0x04,

        //
        Vararg          = 0x05,
        Field           = 0x06,
        LocalSig        = 0x07,
        Property        = 0x08,
        Unmgd           = 0x09,
        GenericInst     = 0x0A,
        Max             = 0x0B,

        //
        Generics        = 0x10,
        HasThis         = 0x20,
        ExplicitThis    = 0x40,
        CallConvMask    = 0x0F,
    }
}
