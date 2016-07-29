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

namespace net.r_eg.Conari.Types
{
    public struct int_t
    {
        public const int SIZE_I64   = sizeof(Int64);
        public const int SIZE_I32   = sizeof(Int32);
        public const int SIZE_I16   = sizeof(Int16);
        public const int SIZE_I8    = sizeof(SByte);

        public const Int64 MAX = Int64.MaxValue;
        public const Int64 MIN = Int64.MinValue;

        private Int64 val;

        public int ActualSize
        {
            get;
            private set;
        }

        public static implicit operator Int64(int_t number)
        {
            return number.val;
        }

        /// <exception cref="OverflowException">number is greater than Int32.MaxValue or less than Int32.MinValue</exception>
        public static implicit operator Int32(int_t number)
        {
            return Convert.ToInt32(number.val);
        }

        /// <exception cref="OverflowException">number is greater than Int16.MaxValue or less than Int16.MinValue</exception>
        public static implicit operator Int16(int_t number)
        {
            return Convert.ToInt16(number.val);
        }

        /// <exception cref="OverflowException">number is greater than SByte.MaxValue or less than SByte.MinValue</exception>
        public static implicit operator SByte(int_t number)
        {
            return Convert.ToSByte(number.val);
        }

        /// <exception cref="OverflowException">number is less than uint_t.MIN</exception>
        public static explicit operator uint_t(int_t number)
        {
            return new uint_t(number);
        }

        public static implicit operator int_t(Int64 number)
        {
            return new int_t(number, SIZE_I64);
        }

        public static implicit operator int_t(Int32 number)
        {
            return new int_t(number, SIZE_I32);
        }

        public static implicit operator int_t(Int16 number)
        {
            return new int_t(number, SIZE_I16);
        }

        public static implicit operator int_t(SByte number)
        {
            return new int_t(number, SIZE_I8);
        }

        public int_t(Int64 number, int size)
        {
            val         = number;
            ActualSize  = size;
        }

        public int_t(Int64 number)
            : this(number, SIZE_I64)
        {

        }

        public int_t(Int32 number)
            : this(number, SIZE_I32)
        {

        }

        public int_t(Int16 number)
            : this(number, SIZE_I16)
        {

        }

        public int_t(SByte number)
            : this(number, SIZE_I8)
        {

        }

        /// <exception cref="OverflowException">number is greater than int_t.MAX</exception>
        public int_t(uint_t number)
            : this(Convert.ToInt64(number), number.ActualSize)
        {

        }
    }
}
