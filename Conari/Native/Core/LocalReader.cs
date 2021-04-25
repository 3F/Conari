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
using System.Linq;
using net.r_eg.Conari.Types;

namespace net.r_eg.Conari.Native.Core
{
    using static Static.Members;

    public class LocalReader: NativeReaderAbstract, IPointer, INativeReader, ILocalReader
    {
        protected byte[] data;

        public int Length => data.Length;

        public override VPtr CurrentPtr { get; set; }

        public override UIntPtr readUIntPtr() => new(Is64bit ? readUInt64() : readUInt32());

        public override IntPtr readIntPtr() => new(Is64bit ? readInt64() : readInt32());

        public override UInt64 readUInt64() => atomic(_ => BitConverter.ToUInt64(getData(8), 0));

        public override Int64 readInt64() => atomic(_ => BitConverter.ToInt64(getData(8), 0));

        public override UInt32 readUInt32() => atomic(_ => BitConverter.ToUInt32(getData(4), 0));

        public override Int32 readInt32() => atomic(_ => BitConverter.ToInt32(getData(4), 0));

        public override UInt16 readUInt16() => atomic(_ => BitConverter.ToUInt16(getData(2), 0));

        public override Int16 readInt16() => atomic(_ => BitConverter.ToInt16(getData(2), 0));

        public override byte readByte() => atomic(_ => data[CurrentPtr]);

        public override sbyte readSByte() => unchecked((sbyte)readByte());

        public void extend(byte[] bytes)
        {
            if(bytes != null) data = data.Concat(bytes).ToArray();
        }

        public LocalReader(byte[] data)
            : base(VPtr.ZeroLong)
        {
            this.data = data ?? throw new ArgumentNullException(nameof(data));
        }

        protected byte[] getData(uint count) => getData(CurrentPtr, count);

        protected byte[] getData(long offset, uint count)
        {
            byte[] ret = new byte[count];
            for(long i = offset, n = offset + count; i < n; ++i)
            {
                ret[i - offset] = data[i];
            }
            return ret;
        }
    }
}
