/*!
 * Copyright (c) 2016  Denis Kuzmin <x-3F@outlook.com> github/3F
 * Copyright (c) Conari contributors https://github.com/3F/Conari/graphs/contributors
 * Licensed under the MIT License (MIT).
 * See accompanying LICENSE.txt file or visit https://github.com/3F/Conari
*/

using System;
using System.Linq;
using net.r_eg.Conari.Exceptions;
using net.r_eg.Conari.Types;

namespace net.r_eg.Conari.Native.Core
{
    using static Static.Members;

    public class LocalContent: AccessorAbstract, IPointer, IAccessor, ILocalContent
    {
        protected byte[] data;

        public int Length => data.Length;

        public override VPtr CurrentPtr { get; set; }

        public override UIntPtr readUIntPtr() => new(Is64bit ? readUInt64() : readUInt32());

        public override IntPtr readIntPtr() => new(Is64bit ? readInt64() : readInt32());

        public override UInt64 readUInt64() => BitConverter.ToUInt64(bytes(8), 0);

        public override Int64 readInt64() => BitConverter.ToInt64(bytes(8), 0);

        public override UInt32 readUInt32() => BitConverter.ToUInt32(bytes(4), 0);

        public override Int32 readInt32() => BitConverter.ToInt32(bytes(4), 0);

        public override UInt16 readUInt16() => BitConverter.ToUInt16(bytes(2), 0);

        public override Int16 readInt16() => BitConverter.ToInt16(bytes(2), 0);

        public override byte readByte() => checkRange().atomicR(_ => data[CurrentPtr]);

        public override sbyte readSByte() => unchecked((sbyte)readByte());

        public override IAccessor writeByte(byte input)
            => checkRange().atomicW<byte>(_ => { data[CurrentPtr] = input; });

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

        public void extend(params byte[] bytes)
        {
            if(bytes != null) data = data.Concat(bytes).ToArray();
        }

        public LocalContent(params byte[] data)
            : base(VPtr.ZeroLong)
        {
            this.data = data ?? throw new ArgumentNullException(nameof(data));
        }

        protected virtual LocalContent checkRange()
        {
            if(CurrentPtr < 0 || CurrentPtr >= data.Length) throw new InvalidOrUnavailableRangeException(CurrentPtr);
            return this;
        }
    }
}
