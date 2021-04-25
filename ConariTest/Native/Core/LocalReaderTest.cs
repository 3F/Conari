using System;
using net.r_eg.Conari.Native;
using net.r_eg.Conari.Native.Core;
using Xunit;

namespace ConariTest.Native.Core
{
    public class LocalReaderTest
    {
        [Fact]
        public void charBitsTest1()
        {
            new byte[] { 0x30, 0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37, 0x38, 0x39 }
            .Native().Reader
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
            byte[] data = { 0x30, 0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37, 0x38, 0x39 };
            INativeReader reader = data.Native().Reader;

            Assert.Equal('㄰', reader.readWChar());
            Assert.Equal('2', reader.readTChar());
            Assert.Equal('㐳', reader.readTChar(CharType.TwoByte));
            Assert.Equal('5', reader.readTChar(CharType.OneByte));

            reader.set(CharType.Unicode);
            Assert.Equal('6', reader.readChar());
            Assert.Equal('㠷', reader.readTChar());

            reader.set(CharType.Ascii);
            Assert.Equal('9', reader.readTChar());
        }

        [Fact]
        public void cmpCharsTest1()
        {
            byte[] seq = { (byte)'M', (byte)'Z', 0, (byte)'P', (byte)'E', 0, 0 };

            Assert.True
            (
                seq.Native().Reader
                .eq("MZ\0".ToCharArray())
                .ifFalse(_ => throw new ArgumentException())
                .eq("PE\0\0".ToCharArray())
                .check()
            );
        }
    }
}
