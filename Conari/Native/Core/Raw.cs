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
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace net.r_eg.Conari.Native.Core
{
    using Fields = List<Field>;

    public sealed class Raw
    {
        private IntPtr pointer;
        private Fields fields;

        private int offset;
        private int length;

        public byte[] Values
        {
            get {
                return Iter.ToArray();
            }
        }

        public IEnumerable<byte> Iter
        {
            get {
                return read(pointer, offset, length);
            }
        }

        public BType Type
        {
            get {
                return new BType(Values, fields);
            }
        }

        public Raw(IntPtr ptr, Fields fields)
            : this(ptr, fields.Sum(f => f.tsize), 0)
        {
            this.fields = fields;
        }

        public Raw(IntPtr ptr, int length, int offset = 0)
        {
            pointer = ptr;

            this.length = length;
            this.offset = offset;
        }

        private IEnumerable<byte> read(IntPtr ptr, int offset, int len)
        {
            for(int i = offset; i < len; ++i) {
                yield return Marshal.ReadByte(ptr, i);
            }
        }
    }
}
