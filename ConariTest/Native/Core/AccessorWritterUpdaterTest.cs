using System;
using System.Collections.Generic;
using System.IO;
using net.r_eg.Conari.Native;
using net.r_eg.Conari.Native.Core;
using net.r_eg.Conari.Types;
using Xunit;

namespace ConariTest.Native.Core
{
    public class AccessorWritterUpdaterTest
    {
        [Theory]
        [MemberData(nameof(AcsTest12))]
        public void writerAndUpdaterDzoneTest1(IAccessor acs, IDisposable obj)
        {
            using IDisposable _ = obj;
            Assert.True
            (
                acs
                .write<byte>(1, 2, 3, 4)
                .D.eq<byte>(1, 2, 3, 4).failed(false)
                .D.write<byte>(5, 6, 7, 8)
                .update<byte>(8, 7, 6, 5)
                .back<int>()
                .eq(84281096).ifFalse(_ => throw new Exception())
                .D.eq<byte>(8, 7, 6, 5)
                .check()
             );
        }

        public static IEnumerable<object[]> AcsTest1()
        {
            byte[] data = { 0, 1, 2, 3, 4, 5 };

            yield return new object[] { new LocalContent(data), null };

            Allocator alloc = new(data);
            yield return new object[] { alloc.Memory, alloc };

            NativeStream stream = new(new MemoryStream(data));
            yield return new object[] { stream, stream };
        }

        [Theory]
        [MemberData(nameof(AcsTest1))]
        public void writerAndUpdaterTest1(IAccessor acs, IDisposable obj)
        {
            using IDisposable _ = obj;
            Assert.True
            (
                acs
                .repeat<byte>(3, 0).move(0, Zone.D)
                .eq<byte>(0, 0, 0).failed(false).move(0, Zone.D)
                .write<sbyte>(-1)
                .move(-1).eq<sbyte>(-1).failed(false)
                .writeByte(2)
                .move(-1).eq<byte>(2).failed(false)
                .writeSByte(-4)
                .update<byte>(6)
                .move(-2).eq<byte>(2, 6).failed(false)
                .update<sbyte>(-7)
                .move(0, Zone.Initial)
                .eq<sbyte>(-1).eq<byte>(2).eq<sbyte>(-7).eq<byte>(3, 4)
                .move(0, Zone.Initial)
                .eq<sbyte>(-1, 2, -7, 3, 4)
                .check()
             );
        }

        public static IEnumerable<object[]> AcsTest2()
        {
            byte[] data = { 0, 0, 0, 0, 0, 0 };

            yield return new object[] { new LocalContent(data), null };

            Allocator alloc = new(data);
            yield return new object[] { alloc.Memory, alloc };

            NativeStream stream = new(new MemoryStream(data));
            yield return new object[] { stream, stream };
        }

        [Theory]
        [MemberData(nameof(AcsTest2))]
        public void writerAndUpdaterTest2(IAccessor acs, IDisposable obj)
        {
            using IDisposable _ = obj;
            Assert.True
            (
                acs
                .write<short>(-1)
                .back<short>().eq<short>(-1).ifFalse(_ => throw new Exception())
                .writeUInt16(2)
                .back<short>().eq<ushort>(2).failed(false)
                .writeInt16(-4)
                .update<ushort>(6)
                .back<short, short>().eq<ushort>(2, 6).ifFalse(_ => throw new Exception())
                .update<short>(-7)
                .move(0, Zone.Initial)
                .eq<short>(-1, 2, -7)
                .check()
             );
        }

        public static IEnumerable<object[]> AcsTest3()
        {
            byte[] data = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            yield return new object[] { new LocalContent(data), null };

            Allocator alloc = new(data);
            yield return new object[] { alloc.Memory, alloc };

            NativeStream stream = new(new MemoryStream(data));
            yield return new object[] { stream, stream };
        }

        [Theory]
        [MemberData(nameof(AcsTest3))]
        public void writerAndUpdaterTest3(IAccessor acs, IDisposable obj)
        {
            using IDisposable _ = obj;
            Assert.True
            (
                acs
                .write(-1)
                .back<int>().eq(-1).failed(false)
                .writeUInt32(2)
                .back<uint>().eq<uint>(2).failed(false)
                .writeInt32(-4)
                .update<uint>(6)
                .back<int>(2).eq<uint>(2, 6).failed(false)
                .update(-7)
                .move(0, Zone.Initial)
                .eq(-1, 2, -7)
                .check()
             );
        }

        public static IEnumerable<object[]> AcsTest4()
        {
            byte[] data = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            yield return new object[] { new LocalContent(data), null };

            Allocator alloc = new(data);
            yield return new object[] { alloc.Memory, alloc };

            NativeStream stream = new(new MemoryStream(data));
            yield return new object[] { stream, stream };
        }

        [Theory]
        [MemberData(nameof(AcsTest4))]
        public void writerAndUpdaterTest4(IAccessor acs, IDisposable obj)
        {
            using IDisposable _ = obj;
            Assert.True
            (
                acs
                .write<long>(-1)
                .back<long>().eq<long>(-1).failed(false)
                .writeUInt64(2)
                .back<ulong>().eq<ulong>(2).failed(false)
                .writeInt64(-4)
                .update<ulong>(6)
                .back<ulong, long>().eq<ulong>(2, 6).failed(false)
                .update<long>(-7)
                .move(0, Zone.Initial)
                .eq<long>(-1, 2, -7)
                .check()
             );
        }

        public static IEnumerable<object[]> AcsTest5()
        {
            byte[] data = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            yield return new object[] { new LocalContent(data), null };

            Allocator alloc = new(data);
            yield return new object[] { alloc.Memory, alloc };

            NativeStream stream = new(new MemoryStream(data));
            yield return new object[] { stream, stream };
        }

        [Theory]
        [MemberData(nameof(AcsTest5))]
        public void writerAndUpdaterTest5(IAccessor acs, IDisposable obj)
        {
            using IDisposable _ = obj;
            Assert.True
            (
                acs
                .write(new IntPtr(-1))
                .back<IntPtr>().eq(new IntPtr(-1)).failed(false)
                .writeUIntPtr(new UIntPtr(2))
                .back<IntPtr>().eq(new UIntPtr(2)).failed(false)
                .writeIntPtr(new IntPtr(-4))
                .update(new UIntPtr(6))
                .back<IntPtr>(2).eq(new UIntPtr(2), new UIntPtr(6)).failed(false)
                .update(new IntPtr(-7))
                .move(0, Zone.Initial)
                .eq(new IntPtr(-1), new IntPtr(2), new IntPtr(-7))
                .check()
             );
        }

        public static IEnumerable<object[]> AcsTest6()
        {
            byte[] data = { 0, 0, 0 };

            yield return new object[] { new LocalContent(data), null };

            Allocator alloc = new(data);
            yield return new object[] { alloc.Memory, alloc };

            NativeStream stream = new(new MemoryStream(data));
            yield return new object[] { stream, stream };
        }

        [Theory]
        [MemberData(nameof(AcsTest6))]
        public void writerAndUpdaterTest6(IAccessor acs, IDisposable obj)
        {
            using IDisposable _ = obj;
            Assert.True
            (
                acs
                .write('3')
                .back<char>().eq('3').failed(false)
                .writeChar('9')
                .back<char>().eq('9').ifFalse(_ => throw new Exception())
                .writeChar('0')
                .update('6')
                .back<char>(2).eq('9', '6').failed(false)
                .update('7')
                .move(0, Zone.Initial)
                .eq('3', '9', '7')
                .move(0, Zone.Initial)
                .eq<byte>(0x33, 0x39, 0x37)
                .check()
             );
        }

        public static IEnumerable<object[]> AcsTest7()
        {
            byte[] data = { 0, 0, 0, 0, 0, 0 };

            yield return new object[] { new LocalContent(data), null };

            Allocator alloc = new(data);
            yield return new object[] { alloc.Memory, alloc };

            NativeStream stream = new(new MemoryStream(data));
            yield return new object[] { stream, stream };
        }

        [Theory]
        [MemberData(nameof(AcsTest7))]
        public void writerAndUpdaterTest7(IAccessor acs, IDisposable obj)
        {
            using IDisposable _ = obj;
            Assert.True
            (
                acs
                .writeWChar('3')
                .back<wchar>().eq<wchar>('3').ifFalse(_ => throw new Exception())
                .writeWChar('9')
                .back<wchar>().eq<wchar>('9').failed(false)
                .writeWChar('0')
                .updateWChar('6')
                .back<wchar, wchar>().eq<wchar>('9', '6').failed(false)
                .updateWChar('7')
                .D.eq<wchar>('3', '9', '7')
                .D.eq<byte>(0x33, 0x00, 0x39, 0x00, 0x37, 0x00)
                .check()
             );
        }

        public static IEnumerable<object[]> AcsTest8()
        {
            byte[] data = { 0, 0, 0, 0, 0, 0 };

            yield return new object[] { new LocalContent(data), null };

            Allocator alloc = new(data);
            yield return new object[] { alloc.Memory, alloc };

            NativeStream stream = new(new MemoryStream(data));
            yield return new object[] { stream, stream };
        }

        [Theory]
        [MemberData(nameof(AcsTest8))]
        public void writerAndUpdaterTest8(IAccessor acs, IDisposable obj)
        {
            using IDisposable _ = obj;
            Assert.True
            (
                acs
                .set(CharType.TwoByte)
                .writeTChar('3')
                .back<char>().eq('3').failed(false)
                .writeTChar('9')
                .back<char>().eq('9').failed(false)
                .writeTChar('0')
                .updateTChar('6')
                .back<char>(2).eq('9', '6').failed(false)
                .updateTChar('7')
                .move(0, Zone.Initial)
                .eq('3', '9', '7')
                .move(0, Zone.Initial)
                .eq<byte>(0x33, 0x00, 0x39, 0x00, 0x37, 0x00)
                .check()
             );
        }

        public static IEnumerable<object[]> AcsTest9()
        {
            byte[] data = { 0, 0, 0 };

            yield return new object[] { new LocalContent(data), null };

            Allocator alloc = new(data);
            yield return new object[] { alloc.Memory, alloc };

            NativeStream stream = new(new MemoryStream(data));
            yield return new object[] { stream, stream };
        }

        [Theory]
        [MemberData(nameof(AcsTest9))]
        public void writerAndUpdaterTest9(IAccessor acs, IDisposable obj)
        {
            using IDisposable _ = obj;
            Assert.True
            (
                acs
                .set(CharType.OneByte)
                .writeTChar('3')
                .back<char>().eq('3').failed(false)
                .writeTChar('9')
                .back<char>().eq('9').failed(false)
                .writeTChar('0')
                .updateTChar('6')
                .back<char>(2).eq('9', '6').failed(false)
                .updateTChar('7')
                .move(0, Zone.Initial)
                .eq('3', '9', '7')
                .move(0, Zone.Initial)
                .eq<byte>(0x33, 0x39, 0x37)
                .check()
             );
        }

        public static IEnumerable<object[]> AcsTest10()
        {
            byte[] data = { 0, 0, 0, 0, 0, 0 };

            yield return new object[] { new LocalContent(data), null };

            Allocator alloc = new(data);
            yield return new object[] { alloc.Memory, alloc };

            NativeStream stream = new(new MemoryStream(data));
            yield return new object[] { stream, stream };
        }

        [Theory]
        [MemberData(nameof(AcsTest10))]
        public void writerAndUpdaterTest10(IAccessor acs, IDisposable obj)
        {
            using IDisposable _ = obj;
            Assert.True
            (
                acs
                .set(CharType.Ascii)
                .writeChar('3', CharType.Unicode)
                .back<wchar>().eq<wchar>('3').failed(false)
                .writeChar('9', CharType.Unicode)
                .back<wchar>().eq<wchar>('9').failed(false)
                .writeChar('0', CharType.Unicode)
                .updateChar('6', CharType.Unicode)
                .back<wchar>(2).eq<wchar>('9', '6').failed(false)
                .updateChar('7', CharType.Unicode)
                .D.eq<wchar>('3', '9', '7')
                .move(0, Zone.Initial)
                .eq<byte>(0x33, 0x00, 0x39, 0x00, 0x37, 0x00)
                .check()
             );
        }

        public static IEnumerable<object[]> AcsTest11()
        {
            byte[] data = { 0, 0, 0 };

            yield return new object[] { new LocalContent(data), null };

            Allocator alloc = new(data);
            yield return new object[] { alloc.Memory, alloc };

            NativeStream stream = new(new MemoryStream(data));
            yield return new object[] { stream, stream };
        }

        [Theory]
        [MemberData(nameof(AcsTest11))]
        public void writerAndUpdaterTest11(IAccessor acs, IDisposable obj)
        {
            using IDisposable _ = obj;
            Assert.True
            (
                acs
                .set(CharType.Unicode)
                .writeChar('3', CharType.Ascii)
                .back<achar>().eq<achar>('3').ifFalse(_ => throw new Exception())
                .writeChar('9', CharType.Ascii)
                .back<achar>().eq<achar>('9').ifFalse(_ => throw new Exception())
                .writeChar('0', CharType.Ascii)
                .updateChar('6', CharType.Ascii)
                .back<achar>(2).eq<achar>('9', '6').ifFalse(_ => throw new Exception())
                .updateChar('7', CharType.Ascii)
                .move(0, Zone.Initial)
                .eq<achar>('3', '9', '7')
                .D.eq<byte>(0x33, 0x39, 0x37)
                .check()
             );
        }

        public static IEnumerable<object[]> AcsTest12()
        {
            byte[] data = { 0, 0, 0, 0, 0, 0 };

            yield return new object[] { new LocalContent(data), null };

            Allocator alloc = new(data);
            yield return new object[] { alloc.Memory, alloc };

            NativeStream stream = new(new MemoryStream(data));
            yield return new object[] { stream, stream };
        }

        [Theory]
        [MemberData(nameof(AcsTest12))]
        public void writerAndUpdaterTest13(IAccessor acs, IDisposable obj)
        {
            using IDisposable _ = obj;
            acs
            .write<byte>(5)
            .write(-4)
            .update(6)
            .back<int, byte>().eq<byte>(5).eq(6).ifFalse(_ => throw new Exception())
            .move(0, Zone.D)
            .write<byte>(7)
            .update<byte>(8)
            .back<byte>().eq<byte>(8).eq(6).ifFalse(_ => throw new Exception());
        }
    }
}
