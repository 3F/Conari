using System;
using ConariTest._svc;
using net.r_eg.Conari;
using net.r_eg.Conari.Native;
using net.r_eg.Conari.Native.Core;
using net.r_eg.Conari.Types;
using Xunit;

namespace ConariTest.Native.Core
{
    using static _svc.TestHelper;
    using LONG = Int32;

    public class MemoryTest
    {
        [Fact]
        public void peMemTest1()
        {
            using var l = new ConariL(RXW_X64);
            Memory memory = new((IntPtr)l);

            memory.move(0x3C, SeekPosition.Initial);
            var e_lfanew = memory.read<LONG>();

            memory.move(e_lfanew, SeekPosition.Initial);
            char[] sig = memory.bytes<char>(4);

            Assert.Equal('P', sig[0]);
            Assert.Equal('E', sig[1]);
            Assert.Equal('\0', sig[2]);
            Assert.Equal('\0', sig[3]);
        }

        [Fact]
        public void memTest2()
        {
            using var l = new ConariL(RXW_X64);

            NativeData native   = new((IntPtr)l);
            Memory memory       = new((IntPtr)l);

            Assert.Equal((VPtr)l, memory.CurrentPtr);

            Assert.Equal(native.Reader.CurrentPtr, memory.CurrentPtr);
            Assert.Equal(native.Reader.InitialPtr, memory.InitialPtr);
            Assert.Equal(native.Reader.RegionPtr, memory.RegionPtr);
        }

        [Fact]
        public void memTest3()
        {
            using var l = new ConariL(RXW_X64);
            Memory memory = new((IntPtr)l);

            VPtr initial = (VPtr)l;
            byte[] bytes = memory.bytes(2);
            Assert.Equal(0x4D, bytes[0]);
            Assert.Equal(0x5A, bytes[1]);

            Assert.Equal(memory.CurrentPtr, memory.getAddr(2));

            Assert.Equal(new VPtr(initial, +2), memory.CurrentPtr);
            Assert.Equal(initial, memory.InitialPtr);
            Assert.Equal(initial, memory.RegionPtr);

            VPtr _mz = memory.CurrentPtr;

            memory.resetRegionPtr();
            Assert.Equal(initial, memory.CurrentPtr);

            memory.resetPtr();
            Assert.Equal(initial, memory.CurrentPtr);

            Assert.Equal(_mz, memory.getPtrFrom(2));

            char[] chars = memory.bytes<char>(2);
            Assert.Equal('M', chars[0]);
            Assert.Equal('Z', chars[1]);
            Assert.Equal(new VPtr(initial, +2), memory.CurrentPtr);
        }

        [Fact]
        public void memTest4()
        {
            using var l = new ConariL(RXW_X64);
            Memory memory = new((IntPtr)l);

            VPtr initial = (VPtr)l;
            Assert.Equal(new VPtr(initial, +2), memory.move(2).CurrentPtr);
            Assert.Equal(initial, memory.upPtr(-2));

            Assert.Equal(0x5A4D, memory.readInt16());
        }

        [Fact]
        public void memTest5()
        {
            using var alloc = new Allocator(new byte[]{ 0, 0x31, 2, 3, 4, 5, 6, 7, 8, 9 });
            Memory memory = new(alloc.ptr);

            Assert.Equal(0, memory.readByte());
            Assert.Equal('1', memory.readChar());

            VPtr ptr = memory.CurrentPtr;
            Assert.Equal(ptr, memory.shiftRegionPtr());
            Assert.Equal(ptr, memory.resetRegionPtr());

            Assert.Equal(650777868590383874u, memory.readUInt64());
            memory.upPtr(-4);
            Assert.Equal(151521030u, memory.readUInt32());
        }

        [Fact]
        public void memTest6()
        {
            using var alloc = new Allocator(new byte[]{ 0x70, 0xDE, 0x01, 0xAC, 0x04, 0xCB, 0x70, 0xDE, 0x93, 0x12, 0x74, 0x94 });
            Memory memory = new(alloc.ptr);

            Assert.Equal(-2418209778971845008, memory.readInt64());

            memory.resetPtr();
            Assert.Equal(-1409163664, memory.readInt32());

            memory.shiftRegionPtr();

            memory.upPtr(-4);
            memory.upPtr(6);
            memory.move(2, SeekPosition.Current);
            Assert.Equal(-109, memory.readSByte());

            memory.resetRegionPtr();

#if TEST_MEM_LOAD_X32

            Assert.Equal(new IntPtr(-563033340), memory.readIntPtr());

            memory.resetRegionPtr();
            Assert.Equal(new UIntPtr(3731933956), memory.readUIntPtr());

#else

            Assert.Equal(new IntPtr(-7749548632496354556), memory.readIntPtr());

            memory.resetRegionPtr();
            Assert.Equal(new UIntPtr(10697195441213197060), memory.readUIntPtr());

#endif
        }

        [Fact]
        public void memTest7()
        {
            using var alloc = new Allocator(new byte[]{ 0x4D, 0x00, 0x4B, 0x04, 0xFC, 0x25, 0x41, 0x00, 
                                                        0x4B, 0x04, 0xFE, 0x25, 0x43, 0x00, 0x4B, 0x04, 
                                                        0x00, 0x26, 0x55, 0x00, 0x4B, 0x04, 0x02, 0x26, 
                                                        0x47, 0x00, 0x4B, 0x04, 0x04, 0x26, 0x49, 0x00, 
                                                        0x4B, 0x04, 0x06, 0x26, 0x57, 0x00, 0x4B, 0x04, 
                                                        0x08, 0x26, 0x4D, 0x00, 0x4F, 0x04, 0x0C });
            Memory memory = new(alloc.ptr);

            Assert.Equal('M', memory.read<char>());
            Assert.Equal(0, memory.read<byte>());
            Assert.Equal(75, memory.read<sbyte>());
            Assert.Equal(-1020, memory.read<short>());
            Assert.Equal(16677, memory.read<ushort>());
            Assert.Equal(-33273088, memory.read<int>());
            Assert.Equal(1258308389u, memory.read<uint>());
            Assert.Equal(145323552783335428, memory.read<long>());
            Assert.Equal(5270904830368433958u, memory.read<ulong>());

#if TEST_MEM_LOAD_X32

            Assert.Equal(new IntPtr(100944640), memory.read<IntPtr>());
            Assert.Equal(new UIntPtr(1258313510), memory.read<UIntPtr>());

#else

            Assert.Equal(new IntPtr(5404415373665913600), memory.read<IntPtr>());
            Assert.Equal(new UIntPtr(865903891074910212), memory.read<UIntPtr>());

#endif
        }

        [Fact]
        public void memTest8()
        {
            using var alloc = new Allocator(new byte[]{ 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 1 });
            Memory memory = new(alloc.ptr);

            sbyte v1    = -9;
            byte[] v2   = Array.Empty<byte>();

            Assert.Equal(0, memory.readByte());

            memory
                .read<byte>(2, out byte[] r1)
                .read<byte>(out byte r2)
                .next<sbyte>(ref v1)
                .read<short>(out short r3)
                .bytes<byte>(2, ref v2);


            Assert.Equal((byte)1, r1[0]);
            Assert.Equal((byte)2, r1[1]);
            Assert.Equal((byte)3, r2);
            Assert.Equal((sbyte)4, v1);
            Assert.Equal((short)1541, r3);
            Assert.Equal((byte)7, v2[0]);
            Assert.Equal((byte)8, v2[1]);

            Assert.Equal((ushort)265, memory.readUInt16());
        }

        [Fact]
        public void memTest9()
        {
            using var l = new ConariL(RXW_X64);

            VPtr initial = (VPtr)l;
            Assert.Equal(new VPtr(initial, +2), l.Memory.move(2).CurrentPtr);
            Assert.Equal(initial, l.Memory.upPtr(-2));

            Assert.Equal(0x5A4D, l.Memory.readInt16());
        }
    }
}
