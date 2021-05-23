using System;
using System.IO;
using ConariTest._svc;
using net.r_eg.Conari;
using net.r_eg.Conari.Native;
using net.r_eg.Conari.Native.Core;
using net.r_eg.Conari.PE.WinNT;
using net.r_eg.Conari.Types;
using Xunit;

namespace ConariTest.Native
{
    using DWORD = UInt32;
    using LONG  = Int32;
    using WORD  = UInt16;
    using static _svc.TestHelper;

    public class NativeDataTest
    {
        [Fact]
        public void peMemTest1()
        {
            using var l = new ConariL(RXW_X64);

            var native = ((IntPtr)l).Native();
            var memory = native.Access;

            native.Access.move(0x3C, Zone.Initial);
            var e_lfanew = memory.read<LONG>();

            memory.move(e_lfanew, Zone.Initial);
            char[] sig = memory.bytes<char>(4);

            Assert.Equal('P', sig[0]);
            Assert.Equal('E', sig[1]);
            Assert.Equal('\0', sig[2]);
            Assert.Equal('\0', sig[3]);
        }

        [Fact]
        public void peMemTest2()
        {
            using var l = new ConariL(RXW_X64);
            var native = _rxw_x64_pe_init(l);

            dynamic ifh =
                native.renew()
                .t<DWORD>()
                .f<WORD>("Machine", "NumberOfSections")
                .align<DWORD>(3)
                .f<WORD>("SizeOfOptionalHeader", "Characteristics")
                .region()
                .t<WORD>("Magic")
                .build();

            _rxw_x64_nt_header_asserts(ifh);
        }

        [Fact]
        public void peMemTest3()
        {
            using var l = new ConariL(RXW_X64);
            var native = _rxw_x64_pe_init(l);

            dynamic ifh =
                native.renew()
                .t<DWORD, WORD, WORD>(null, "Machine", "NumberOfSections")
                .align<DWORD>(3)
                .f<WORD>("SizeOfOptionalHeader", "Characteristics")
                .region()
                .t<WORD>("Magic")
                .reset(6)
                .t<WORD, WORD>("SizeOfOptionalHeader", "Characteristics")
                .region()
                .t<WORD>("Magic")
                .Raw.Type;

            _rxw_x64_nt_header_asserts(ifh);
        }

        [Fact]
        public void peMemTest4()
        {
            using var l = new ConariL(RXW_X64);
            var native = _rxw_x64_pe_init(l);

            IntPtr init = native.Access.CurrentPtr;
            dynamic ifh =
                native.renew()
                .t<DWORD>()
                .f<WORD>("Machine", "NumberOfSections")
                .region(out int ofs1)
                .region(out VPtr ptr1)
                .align<DWORD>(3)
                .f<WORD>("SizeOfOptionalHeader", "Characteristics")
                .region(out VPtr ptr2)
                .region(out int ofs2)
                .t<WORD>("Magic")
                .Raw.build();

            Assert.Equal((VPtr)init + sizeof(DWORD) + sizeof(WORD) * 2, ptr1);
            Assert.Equal(sizeof(DWORD) + sizeof(WORD) * 2, ofs1);
            Assert.Equal((VPtr)init + sizeof(DWORD) + sizeof(WORD) * 2 + sizeof(DWORD) * 3 + sizeof(WORD) * 2, ptr2);
            Assert.Equal(sizeof(DWORD) + sizeof(WORD) * 2 + sizeof(DWORD) * 3 + sizeof(WORD) * 2, ofs2);

            _rxw_x64_nt_header_asserts(ifh);
        }

        [Fact]
        public void peMemTest5()
        {
            using var l = new ConariL(RXW_X64);
            var native = _rxw_x64_pe_init(l);

            native.renew()
            .t<DWORD>()
            .f<WORD>("Machine", "NumberOfSections")
            .build(out dynamic ifh1)
            .align<DWORD>(3)
            .f<WORD>("SizeOfOptionalHeader", "Characteristics")
            .Raw.build(out dynamic ifh2)
            .t<WORD>("Magic")
            .build(out dynamic ioh);


            Assert.Equal(MachineTypes.IMAGE_FILE_MACHINE_AMD64, (MachineTypes)ifh1.Machine);
            Assert.Equal(6, ifh1.NumberOfSections);

            Assert.Equal(0xF0, ifh2.SizeOfOptionalHeader);

            Assert.Equal(Magic.PE64, (Magic)ioh.Magic);
        }

        [Fact]
        public void peMemTest6()
        {
            using var l = new ConariL(RXW_X64);

            l.Native.Access
                .move(0x3C, Zone.D)
                .move(l.Memory.read<LONG>(), Zone.D);

            l.Native.renew()
                .t<DWORD>()
                .f<WORD>("Machine", "NumberOfSections")
                .align<DWORD>(3)
                .f<WORD>("SizeOfOptionalHeader")
                .build(out dynamic ifh);

            Assert.Equal(MachineTypes.IMAGE_FILE_MACHINE_AMD64, (MachineTypes)ifh.Machine);
            Assert.Equal(6, ifh.NumberOfSections);
            Assert.Equal(0xF0, ifh.SizeOfOptionalHeader);
        }

        [Fact]
        public void peMemFlaggedChainTest1()
        {
            using var l = new ConariL(RXW_X64);
            var native = _rxw_x64_pe_init(l);

            VPtr ofs0 = native.Access.CurrentPtr;

            dynamic ifh =
                native.renew()
                .t<DWORD>()
                .align<WORD>(2)
                .align<DWORD>(3)
                .align<WORD>(2)
                .build();

            Assert.Equal(VPtr.MakePtr(24), native.Access.CurrentPtr - ofs0);

            native.Access.move(0x6C);

            dynamic idd = native.renew(out VPtr ofs1)
                .t<DWORD>("VirtualAddress")
                .t<DWORD>("Size")
                .build();

            Assert.Equal(VPtr.MakePtr(8), native.Access.CurrentPtr - ofs1);
        }

        [Fact]
        public void peMemFlaggedChainTest2()
        {
            using var l = new ConariL(RXW_X64);
            var native = _rxw_x64_pe_init(l);

            dynamic ifh =
                native.renew(out VPtr ofs0)
                .t<DWORD>()
                .align<WORD>(2)
                .align<DWORD>(3)
                .align<WORD>(2)
                .region()
                .t<WORD>("Magic")
                .build();

            Assert.Equal(VPtr.MakePtr(24), native.Access.CurrentPtr - ofs0);

            native.Access.move(0x6C);

            dynamic idd = native.renew(out VPtr ofs1)
                .t<DWORD>("VirtualAddress")
                .t<DWORD>("Size")
                .region(out VPtr d2)
                .t<DWORD>("VirtualAddress")
                .t<DWORD>("Size")
                .build();

            Assert.Equal(VPtr.MakePtr(8), native.Access.CurrentPtr - ofs1);
            Assert.Equal(VPtr.MakePtr(8), native.Access.CurrentPtr - ofs1);
        }
        
        [Fact]
        public void peStreamTest1()
        {
            using var stream = new NativeStream(new FileStream(RXW_X64, FileMode.Open, FileAccess.Read, FileShare.Read));

            var native = new NativeData(stream);

            native.Access
                .move(0x3C, Zone.D)
                .move(native.Access.read<LONG>(), Zone.Initial);


            dynamic ifh =
                native.renew()
                .t<DWORD>(null)
                .f<WORD>("Machine", "NumberOfSections")
                .align<DWORD>(3)
                .f<WORD>("SizeOfOptionalHeader", "Characteristics")
                .region()
                .t<WORD>("Magic")
                .build();


            _rxw_x64_nt_header_asserts(ifh);
        }

        [Fact]
        public void chainShiftsTest1()
        {
            using var alloc = new Allocator(new byte[]{ 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 0xA, 0xB, 0xC, 0xD, 0xE, 0xF });
            IntPtr ptr = alloc.ptr;

            Assert.Equal((byte)2, ptr.Native().align<byte>(3, "", "", "z").Raw.Type.DLR.z);

            Assert.Equal((byte)2, ptr.Native().ofs<byte>(2, "z").Raw.DLR.z);
            Assert.Equal((byte)2, ptr.Native().ofs<byte>(2, "z").DLR.z);

            var native = ptr.Native();

            Assert.Equal((byte)2, native.ofs<byte>(2, "a").Raw.DLR.a);
            Assert.Equal((byte)5, native.ofs<byte>(2, "b").DLR.b);
            Assert.Equal((byte)1, native.reset().ofs<byte>(1, "c").DLR.c);

            Assert.Equal((byte)6, native.ofs<byte>(4, "d").Raw.DLR.d);
            Assert.Equal((byte)9, native.renew().ofs<byte>(2, "e").DLR.e);

            Assert.Equal((byte)0xC, native.ofs<byte>(2, "f").Raw.DLR.f);
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true, ChainMode.Fast)]
        public void chainModeTest1(bool setmode, ChainMode mode = ChainMode.Fast)
        {
            using var alloc = new Allocator(new byte[]{ 0, 1, 2, 3, 4 });
            var native = setmode ? alloc.ptr.Native().mode(mode) : alloc.ptr.Native();

            Assert.Equal((byte)2, native.t<byte, byte, byte>(null, null, "z").DLR.z);
            Assert.Equal((byte)2, native.t<byte>("z").DLR.z);
            Assert.Equal((byte)2, native.t<byte>("z").t<byte>("z").DLR.z);
        }

        [Fact]
        public void chainModeTest2()
        {
            using var alloc = new Allocator(new byte[]{ 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 });
            var native = alloc.ptr.Native().mode(ChainMode.Updating);

            Assert.Equal((byte)2, native.t<byte, byte, byte>(null, null, "z").DLR.z);
            Assert.Equal((byte)3, native.t<byte>("z").DLR.z);
            Assert.Equal((byte)5, native.t<byte>("z").t<byte>("z").DLR.z);
        }

        [Fact]
        public void chainModeTest3()
        {
            using var alloc = new Allocator(new byte[]{ 0, 1, 2, 3, 4, 5, 6 });
            var native = alloc.ptr.Native().mode(ChainMode.Exception);

            Assert.Equal((byte)2, native.t<byte, byte, byte>(null, null, "z").DLR.z);

            Assert.Throws<ArgumentException>(() => native.t<byte>("z").DLR.z);

            Assert.Equal((byte)2, native.mode(ChainMode.Fast).t<byte>("z").Raw._.z);
            Assert.Equal((byte)4, native.mode(ChainMode.Updating).t<byte>("z")._.z);
        }

        [Fact]
        public void localTest1()
        {
            var exp = new byte[] { 1, 2 };
            var raw = new NativeData(exp).Raw;

            Assert.Equal(2, raw.Values.Length);
            Assert.Equal(exp[0], raw.Values[0]);
            Assert.Equal(exp[1], raw.Values[1]);
        }

        [Fact]
        public void localTest2()
        {
            var exp = new byte[] { 8 };
            var raw = exp.Native().Raw;

            Assert.Single(raw.Values);
            Assert.Equal(exp[0], raw.Values[0]);
        }

        private static NativeData _rxw_x64_pe_init(ConariL l)
        {
            l.Memory
                .move(0x3C, Zone.Initial)
                .read<LONG>(out LONG e_lfanew)
                .move(e_lfanew, Zone.Initial);

            return l.Native;
        }

        private static void _rxw_x64_nt_header_asserts(dynamic ifh)
        {
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
    }
}
