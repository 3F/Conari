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
using System.Diagnostics;

namespace net.r_eg.Conari.Types
{
    [DebuggerDisplay(" = {val} [ ActualSize: {ActualSize} ]")]
    public struct float_t
    {
        public const int SIZE_SINGLE = sizeof(Single);
        public const int SIZE_DOUBLE = sizeof(Double);

        public const Double MAX = Double.MaxValue;
        public const Double MIN = Double.MinValue;

        private Double val;

        public int ActualSize
        {
            get;
            private set;
        }

        public static implicit operator Double(float_t number)
        {
            return number.val;
        }

        public static implicit operator Single(float_t number)
        {
            // To check infinity use the IsInfinity methods
            return (Single)number.val;
        }

        public static implicit operator float_t(Double number)
        {
            return new float_t(number, SIZE_DOUBLE);
        }

        public static implicit operator float_t(Single number)
        {
            return new float_t(number, SIZE_SINGLE);
        }

        public float_t(Double number, int size)
        {
            val         = number;
            ActualSize  = size;
        }

        public float_t(Double number)
            : this(number, SIZE_DOUBLE)
        {

        }

        public float_t(Single number)
            : this(number, SIZE_SINGLE)
        {

        }
    }
}
