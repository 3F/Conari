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
using System.IO;
using net.r_eg.Conari.Resources;
using net.r_eg.Conari.Types;

namespace net.r_eg.Conari.Native.Core
{
    using static Static.Members;

    public class NativeStream: AccessorAbstract, IPointer, IAccessor, IDisposable
    {
        protected readonly BinaryReader stream;
        private VPtr currentPtr;

        public override VPtr CurrentPtr
        {
            get => currentPtr;
            set
            {
                currentPtr = value;

                if(stream == null) return; // base .ctor will initialize property before user stream
                if(stream.BaseStream == null) throw new EndOfStreamException(Msg.stream_disposed);

                stream.BaseStream.Position = value;
            }
        }

        public override UIntPtr readUIntPtr() => new(Is64bit ? readUInt64() : readUInt32());

        public override IntPtr readIntPtr() => new(Is64bit ? readInt64() : readInt32());

        public override UInt64 readUInt64() => atomicR(_ => stream.ReadUInt64());

        public override Int64 readInt64() => atomicR(_ => stream.ReadInt64());

        public override UInt32 readUInt32() => atomicR(_ => stream.ReadUInt32());

        public override Int32 readInt32() => atomicR(_ => stream.ReadInt32());

        public override UInt16 readUInt16() => atomicR(_ => stream.ReadUInt16());

        public override Int16 readInt16() => atomicR(_ => stream.ReadInt16());

        public override byte readByte() => atomicR(_ => stream.ReadByte());

        public override sbyte readSByte() => atomicR(_ => stream.ReadSByte());

        public override IAccessor writeByte(byte input) => atomicW<byte>(_ => writeToStream(input));

        public override IAccessor writeSByte(sbyte input) => writeByte(unchecked((byte)input));

        public override IAccessor writeInt16(short input) => write(BitConverter.GetBytes(input));

        public override IAccessor writeUInt16(ushort input) => write(BitConverter.GetBytes(input));

        public override IAccessor writeInt32(int input) => write(BitConverter.GetBytes(input));

        public override IAccessor writeUInt32(uint input) => write(BitConverter.GetBytes(input));

        public override IAccessor writeInt64(long input) => write(BitConverter.GetBytes(input));

        public override IAccessor writeUInt64(ulong input) => write(BitConverter.GetBytes(input));

        public override IAccessor writeIntPtr(IntPtr input)
            => write(BitConverter.GetBytes(Is64bit ? input.ToInt64() : input.ToInt32()));

        public override IAccessor writeUIntPtr(UIntPtr input)
            => write(BitConverter.GetBytes(Is64bit ? input.ToUInt64() : input.ToUInt32()));

        public NativeStream(Stream stream)
            : base(VPtr.ZeroLong)
        {
            if(stream == null) throw new ArgumentNullException(nameof(stream));
            if(!stream.CanSeek || !stream.CanRead) throw new ArgumentException($"{nameof(stream)} must support seeking and reading");

            this.stream = new BinaryReader(stream);
            this.stream.BaseStream.Position = CurrentPtr; // because the first call from the base class is out of sync due to abstract impl
        }

        protected virtual IAccessor writeToStream(byte data)
        {
            if(!stream.BaseStream.CanWrite) throw new NotSupportedException(Msg.stream_cant_write);
            stream.BaseStream.WriteByte(data);
            return this;
        }

        #region IDisposable

        private bool disposed;

        protected virtual void Dispose(bool _)
        {
            if(!disposed)
            {
                stream.Dispose();
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
