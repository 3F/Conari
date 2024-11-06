/*!
 * Copyright (c) 2016  Denis Kuzmin <x-3F@outlook.com> github/3F
 * Copyright (c) Conari contributors https://github.com/3F/Conari/graphs/contributors
 * Licensed under the MIT License (MIT).
 * See accompanying LICENSE.txt file or visit https://github.com/3F/Conari
*/

using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using net.r_eg.Conari.Types;

namespace net.r_eg.Conari.Native.Core
{
    using static Static.Members;

    [Serializable]
    public class Memory: AccessorAbstract, IPointer, IAccessor, ISerializable
    {
        public override VPtr CurrentPtr { get; set; }

        public static explicit operator VPtr(Memory m) => m.CurrentPtr;
        public static implicit operator Memory(VPtr ptr) => new(ptr);

        public static explicit operator IntPtr(Memory m) => m.CurrentPtr;
        public static implicit operator Memory(IntPtr ptr) => new(ptr);

        public override UIntPtr readUIntPtr() => new(Is64bit ? readUInt64() : readUInt32());

        public override IntPtr readIntPtr() => atomicR(_ => Marshal.ReadIntPtr(CurrentPtr));

        public override UInt64 readUInt64() => BitConverter.ToUInt64(bytes(SizeOf<UInt64>()), 0);

        public override Int64 readInt64() => atomicR(_ => Marshal.ReadInt64(CurrentPtr));

        public override UInt32 readUInt32() => BitConverter.ToUInt32(bytes(SizeOf<UInt32>()), 0);

        public override Int32 readInt32() => atomicR(_ => Marshal.ReadInt32(CurrentPtr));

        public override UInt16 readUInt16() => BitConverter.ToUInt16(bytes(SizeOf<UInt16>()), 0);

        public override Int16 readInt16() => atomicR(_ => Marshal.ReadInt16(CurrentPtr));

        public override byte readByte() => atomicR(_ => Marshal.ReadByte(CurrentPtr));

        public override sbyte readSByte() => unchecked((sbyte)readByte());

        public override IAccessor writeByte(byte input)
            => atomicW<byte>(_ => Marshal.WriteByte(CurrentPtr, input));

        public override IAccessor writeSByte(sbyte input) => writeByte(unchecked((byte)input));

        public override IAccessor writeInt16(short input) 
            => atomicW<short>(_ => Marshal.WriteInt16(CurrentPtr, input));

        public override IAccessor writeUInt16(ushort input) 
            => write(BitConverter.GetBytes(input));

        public override IAccessor writeInt32(int input) 
            => atomicW<int>(_ => Marshal.WriteInt32(CurrentPtr, input));

        public override IAccessor writeUInt32(uint input)
            => write(BitConverter.GetBytes(input));

        public override IAccessor writeInt64(long input) 
            => atomicW<long>(_ => Marshal.WriteInt64(CurrentPtr, input));

        public override IAccessor writeUInt64(ulong input)
            => write(BitConverter.GetBytes(input));

        public override IAccessor writeIntPtr(IntPtr input)
            => atomicW<IntPtr>(_ => Marshal.WriteIntPtr(CurrentPtr, input));

        public override IAccessor writeUIntPtr(UIntPtr input)
            => write(BitConverter.GetBytes(Is64bit ? input.ToUInt64() : input.ToUInt32()));

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue(nameof(CurrentPtr), CurrentPtr);
        }

        public Memory(VPtr ptr)
            : base(ptr)
        {

        }
    }
}
