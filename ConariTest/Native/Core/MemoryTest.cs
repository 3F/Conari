using System;
using ConariTest._svc;
using net.r_eg.Conari;
using net.r_eg.Conari.Exceptions;
using net.r_eg.Conari.Native;
using net.r_eg.Conari.Native.Core;
using net.r_eg.Conari.PE.WinNT;
using net.r_eg.Conari.Types;
using Xunit;

namespace ConariTest.Native.Core
{
    using DWORD = UInt32;
    using LONG  = Int32;
    using WORD  = UInt16;
    using static _svc.TestHelper;

    public class MemoryTest
    {
        [Fact]
        public void peMemNativeChainTest1()
        {
            using ConariL l = new(RXW_X64);

            l.Memory
            .move(0x3C, Zone.D)
            .read(out LONG e_lfanew)
            .move(e_lfanew, Zone.D)
            .eq('P', 'E', '\0', '\0')
            .ifFalse(_ => throw new PECorruptDataException())
            .Native()
            .f<WORD>("Machine", "NumberOfSections")
            .align<DWORD>(3)
            .t<WORD, WORD>("SizeOfOptionalHeader", "Characteristics")
            .region()
            .t<WORD>("Magic") // start IMAGE_OPTIONAL_HEADER offset 0 (0x108)
            .build(out dynamic ifh)
            .Access
            .move(ifh.SizeOfOptionalHeader == 0xF0 ? 0x6C : 0x5C)
            .read(out DWORD NumberOfRvaAndSizes)
            .Native() // DataDirectory[IMAGE_DIRECTORY_ENTRY_EXPORT]
            .t<DWORD>("VirtualAddress")
            .t<DWORD>("Size")
            .build(out dynamic idd)
            .Access.move(8 * (NumberOfRvaAndSizes - 1));

            Assert.Equal(6, ifh.NumberOfSections);
            Assert.Equal
            (
                Characteristics.IMAGE_FILE_LARGE_ADDRESS_AWARE
                    | Characteristics.IMAGE_FILE_DLL
                    | Characteristics.IMAGE_FILE_EXECUTABLE_IMAGE,
                (Characteristics)ifh.Characteristics
            );
            Assert.Equal(MachineTypes.IMAGE_FILE_MACHINE_AMD64, (MachineTypes)ifh.Machine);
            Assert.Equal(Magic.PE64, (Magic)ifh.Magic);
            Assert.Equal(0xF0, ifh.SizeOfOptionalHeader);
        }

        [Fact]
        public void peMemTest1()
        {
            using var l = new ConariL(RXW_X64);
            Memory memory = new((IntPtr)l);

            memory.move(0x3C, Zone.Initial);
            var e_lfanew = memory.read<LONG>();

            memory.move(e_lfanew, Zone.Initial);
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

            Assert.Equal(native.Access.CurrentPtr, memory.CurrentPtr);
            Assert.Equal(native.Access.InitialPtr, memory.InitialPtr);
            Assert.Equal(native.Access.RegionPtr, memory.RegionPtr);
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
            using var alloc = new Allocator(new byte[]{ 0x30, 0x31, 2, 3, 4, 5, 6, 7, 8, 9 });
            Memory memory = new(alloc.ptr);

            Assert.Equal(0x30, memory.readByte());
            Assert.Equal('1', memory.readAChar());

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
            memory.move(2, Zone.V);
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

        [Fact]
        public void peEqTest1()
        {
            using var l = new ConariL(RXW_X64);

            Assert.True
            (
                l.Memory
                .move(0x3C, Zone.D)
                .read<LONG>(out LONG e_lfanew)
                .move(e_lfanew, Zone.D)
                .eq('P').eq('E').eq('\0').eq('\0')
                .check()
            );

            Assert.False
            (
                l.Memory
                .move(e_lfanew, Zone.Initial)
                .eq('M').eq('Z')
                .check()
            );

            Assert.False
            (
                l.Memory
                .rewind(Zone.Region)
                .eq('P').eq('Z').eq('\0').eq('\0')
                .check()
            );
        }

        [Fact]
        public void peEqTest2()
        {
            using var l = new ConariL(RXW_X64);

            Assert.False
            (
                l.Memory
                .@goto(l.PE.Addresses.IMAGE_NT_HEADERS)
                .eq('P').eq('E').eq('\0').eq('\0')
                .ifFalse(_ => throw new PECorruptDataException())
                .rewind(Zone.U)
                .eq('P')
                .eq('Z')
                .eq('\0')
                .eq('\0')
                .check()
            );

            Assert.Throws<PECorruptDataException>
            (() =>
                l.Memory
                .rewind(Zone.Region)
                .eq('P').eq('Z').eq('\0').eq('\0')
                .ifFalse(_ => throw new PECorruptDataException())
            );
        }

        [Fact]
        public void peEqTest3()
        {
            using var l = new ConariL(RXW_X64);

            Assert.True
            (
                l.Memory
                .move(0x3C, Zone.Initial)
                .read<LONG>(out LONG e_lfanew)
                .move(e_lfanew, Zone.D)
                .eq('M').eq('Z')
                .or()
                .eq('P').eq('E').eq('\0').eq('\0')
                .check()
            );

            Assert.False
            (
                l.Memory
                .move(e_lfanew, Zone.D)
                .eq('M')
                .eq('Z')
                .eq('P')
                .eq('E')
                .eq('\0')
                .eq('\0')
                .check()
            );

            Assert.True
            (
                l.Memory
                .move(e_lfanew, Zone.Initial)
                .check()
            );
        }

        [Fact]
        public void peEqTest4()
        {
            using var l = new ConariL(RXW_X64);

            l.Memory
            .@goto(l.PE.Addresses.IMAGE_NT_HEADERS)
            .eq('M').eq('Z')
            .ifTrue(_ => throw new NotSupportedException())
            .or()
            .eq('P').eq('E').eq('\0').eq('\0')
            .ifFalse(_ => throw new PECorruptDataException())
            .or()
            .eq('P').eq('Z').eq('\0').eq('\0')
            .ifTrue(_ => throw new PECorruptDataException());

            Assert.Throws<PECorruptDataException>
            (() =>
                l.Memory
                .@goto(l.PE.Addresses.IMAGE_NT_HEADERS)
                .eq('P').eq('Z').eq('\0').eq('\0')
                .ifFalse(_ => throw new PECorruptDataException())
                .or()
                .eq('M').eq('Z')
                .ifTrue(_ => throw new NotSupportedException())
            );

            Assert.Throws<NotImplementedException>
            (() =>
                l.Memory
                .@goto(l.PE.Addresses.IMAGE_NT_HEADERS)
                .eq('P').eq('Z').eq('\0').eq('\0')
                .ifTrue(_ => throw new PECorruptDataException())
                .or()
                .eq('M').eq('Z')
                .ifTrue(_ => throw new NotSupportedException())
                .eq('P').eq('E')
                .ifFalse(_ => throw new NotImplementedException())
            );

            Assert.Throws<PECorruptDataException>
            (() =>
                l.Memory
                .rewind(Zone.Region)
                .eq('M').eq('Z').eq('P').eq('Z').eq('\0').eq('\0')
                .ifFalse(_ => throw new PECorruptDataException())
            );
        }

        [Fact]
        public void peEqTest5()
        {
            using var l = new ConariL(RXW_X64);

            Assert.True
            (
                l.Memory
                .@goto(l.PE.Addresses.IMAGE_NT_HEADERS)
                .eq('P', 'E', '\0', '\0')
                .check()
            );

            Assert.False
            (
                l.Memory
                .@goto(l.PE.Addresses.IMAGE_NT_HEADERS)
                .eq('P', 'E', '\0', '\0')
                .ifFalse(_ => throw new PECorruptDataException())
                .rewind(Zone.U)
                .eq('P')
                .eq('Z')
                .eq('\0')
                .eq('\0')
                .check()
            );

            Assert.True
            (
                l.Memory
                .rewind()
                .eq('M').eq('Z')
                .or()
                .eq('P', 'E').eq('\0', '\0')
                .check()
            );

            Assert.Throws<PECorruptDataException>
            (() =>
                l.Memory
                .rewind()
                .eq('M', 'Z').eq('P').eq('Z', '\0', '\0')
                .ifFalse(_ => throw new PECorruptDataException())
            );
        }

        [Fact]
        public void peEqTest6()
        {
            using var l = new ConariL(RXW_X64);

            Assert.Throws<NotImplementedException>
            (() =>
                l.Memory
                .@goto(l.PE.Addresses.IMAGE_NT_HEADERS)
                .eq('M').eq('Z')
                .ifTrue(_ => throw new NotSupportedException())
                .or()
                .eq('E').eq('P').eq('\0')
                .ifTrue(_ => throw new ArgumentException())
                .or()
                .eq('P').eq('E').eq('\0').eq('\0')
                .ifTrue(_ => throw new NotImplementedException())
            );
        }

        [Fact]
        public void peEqFailedTest1()
        {
            using var l = new ConariL(RXW_X64);

            Assert.Throws<FailedCheckException>
            (() =>
                l.Memory
                .@goto(l.PE.Addresses.IMAGE_NT_HEADERS)
                .eq('P').eq('E').eq('\0').eq('\0')
                .failed(when: true)
            );

            Assert.Throws<FailedCheckException>
            (() =>
                l.Memory
                .@goto(l.PE.Addresses.IMAGE_NT_HEADERS)
                .eq('E').eq('P').eq('\0').eq('\0')
                .failed(when: false)
            );
        }

        [Fact]
        public void charBitsTest1()
        {
            using var alloc = new Allocator(new byte[] { 0x30, 0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37, 0x38, 0x39 });
            Memory memory = new(alloc.ptr);

            memory
            .eq('0')
            .eq('1')
            .set(CharType.TwoByte)
            .eq('㌲')
            .set(CharType.OneByte)
            .eq('4')
            .set(CharType.Unicode)
            .eq('㘵')
            .set(CharType.Unicode)
            .set(CharType.Ascii)
            .eq('7')
            .ifFalse(_ => throw new ArgumentException());
        }

        [Fact]
        public void charBitsTest2()
        {
            using var alloc = new Allocator(new byte[] { 0x30, 0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37, 0x38, 0x39 });
            Memory memory = new(alloc.ptr);

            Assert.Equal('㄰', memory.readWChar());
            Assert.Equal('2', memory.readChar());
            Assert.Equal('㐳', memory.readChar(CharType.TwoByte));
            Assert.Equal('5', memory.readChar(CharType.OneByte));

            memory.set(CharType.Unicode);
            Assert.Equal('6', memory.readAChar());
            Assert.Equal('㠷', memory.readChar());

            memory.set(CharType.Ascii);
            Assert.Equal('9', memory.readChar());
        }
    }
}
