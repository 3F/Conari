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

        private byte[] local;

        private int offset;
        private int length;

        /// <summary>
        /// Final byte-sequence from values (via pointer or local data).
        /// </summary>
        public byte[] Values
        {
            get
            {
                if(IsLocal) {
                    return local;
                }
                return Iter.ToArray();
            }
        }

        /// <summary>
        /// Access to byte-sequence from values (via pointer or local data).
        /// </summary>
        public IEnumerable<byte> Iter
        {
            get
            {
                if(IsLocal) {
                    return local.AsEnumerable();
                }
                return read(pointer, offset, length);
            }
        }

        /// <summary>
        /// Generates dynamic type for current data.
        /// /+DLR
        /// </summary>
        public BType Type
        {
            get {
                return new BType(Values, fields);
            }
        }

        private bool IsLocal
        {
            get {
                return (pointer == IntPtr.Zero);
            }
        }

        public Raw(IntPtr ptr, Fields fields)
            : this(ptr, fields.Sum(f => f.tsize), 0)
        {
            this.fields = fields;
        }

        public Raw(IntPtr ptr, int length, int offset = 0)
        {
            if(ptr == IntPtr.Zero) {
                throw new ArgumentException("The pointer cannot be zero.");
            }
            pointer = ptr;

            this.length = length;
            this.offset = offset;
        }

        public Raw(byte[] bytes, Fields fields)
        {
            if(bytes == null) {
                throw new ArgumentException("Array of bytes cannot be null.");
            }

            local       = bytes;
            this.fields = fields;
        }

        public Raw(byte[] bytes)
            : this(bytes, null)
        {

        }

        private IEnumerable<byte> read(IntPtr ptr, int offset, int len)
        {
            for(int i = offset; i < len; ++i) {
                yield return Marshal.ReadByte(ptr, i);
            }
        }
    }
}
