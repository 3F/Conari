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
using System.Runtime.Serialization;
using net.r_eg.Conari.Extension;

namespace net.r_eg.Conari.Types
{
    [Serializable]
    public sealed class BufferedString<T>: NativeString<T>, ISerializable, IDisposable
        where T : struct
    {
        internal const float BUF = 2.5f;

        public BufferedString(string str)
            : base(str, str.RelativeLength(BUF))
        {

        }

        public BufferedString(int buffer = 0xFF)
            : base(buffer)
        {

        }

        public BufferedString(string str, int extend)
            : base(str, extend)
        {

        }

        public BufferedString(IntPtr pointer)
            : base(pointer, (int)Math.Ceiling((pointer == IntPtr.Zero ? 0 : GetLengthFromPtr(pointer)) * BUF))
        {

        }

        public BufferedString(IntPtr pointer, int extend)
            : base(pointer, extend)
        {

        }
    }
}
