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
using System.Diagnostics;

namespace net.r_eg.Conari.Types
{
    using static Static.Members;

    [DebuggerDisplay("(int_t) = {(long)val} [ IntPtr.Size: {System.IntPtr.Size} ]")]
    public struct ptrdiff_t
    {
        private int_t val;

        public static implicit operator int_t(ptrdiff_t number)
        {
            return number.val;
        }

        public static implicit operator IntPtr(ptrdiff_t number)
        {
            if(number.val.ActualSize == int_t.SIZE_I64) {
                return new IntPtr((Int64)number.val);
            }
            return new IntPtr((Int32)number.val);
        }

        public static implicit operator ptrdiff_t(int_t number)
        {
            return new ptrdiff_t(number);
        }

        public static implicit operator ptrdiff_t(IntPtr ptr)
        {
            if(Is64bit) {
                return new ptrdiff_t(ptr.ToInt64());
            }
            return new ptrdiff_t(ptr.ToInt32());
        }

        public static implicit operator long(ptrdiff_t number)
        {
            return number.val;
        }

        public static implicit operator ptrdiff_t(long number)
        {
            return new ptrdiff_t(number);
        }

        public static implicit operator int(ptrdiff_t number)
        {
            return number.val;
        }

        // we also use this to initialize the int_t as the int type
        public static implicit operator ptrdiff_t(int number)
        {
            return new ptrdiff_t(number);
        }

        public ptrdiff_t(int_t number)
        {
            val = number;
        }
    }
}
