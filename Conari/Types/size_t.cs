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
    public struct size_t
    {
        private uint_t val;

        public static implicit operator uint_t(size_t number)
        {
            return number.val;
        }

        public static implicit operator IntPtr(size_t number)
        {
            if(number.val.ActualSize == uint_t.SIZE_IU64) {
                return new IntPtr((Int64)number.val);
            }
            return new IntPtr((Int32)number.val);
        }

        public static implicit operator size_t(uint_t number)
        {
            return new size_t(number);
        }

        public static implicit operator size_t(IntPtr ptr)
        {
            if(IntPtr.Size == uint_t.SIZE_IU64) {
                return new size_t(ptr.ToInt64());
            }
            return new size_t(ptr.ToInt32());
        }

        public static implicit operator long(size_t number)
        {
            return number.val;
        }

        public static implicit operator size_t(ulong number)
        {
            return new size_t(number);
        }

        public static implicit operator int(size_t number)
        {
            return number.val;
        }

        // we also use this to initialize the uint_t as the uint type
        public static implicit operator size_t(uint number)
        {
            return new size_t((uint_t)number);
        }

        public size_t(uint_t number)
        {
            val = number;
        }

        public size_t(int_t number)
            : this((uint_t)number)
        {

        }
    }
}
