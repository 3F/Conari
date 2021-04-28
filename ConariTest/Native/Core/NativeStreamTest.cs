using System;
using System.IO;
using net.r_eg.Conari.Native.Core;
using Xunit;

namespace ConariTest.Native.Core
{
    using static _svc.TestHelper;

    public class NativeStreamTest
    {
        [Fact]
        public void charBitsTest1()
        {
            using var stream = new NativeStream(new FileStream(RXW_X, FileMode.Open, FileAccess.Read, FileShare.Read));

            stream
            .eq('M')
            .set(CharType.TwoByte)
            .eq('遚')
            .set(CharType.OneByte)
            .eq<byte>(0)
            .rewind()
            .set(CharType.Unicode)
            .eq('婍')
            .rewind()
            .set(CharType.Ascii)
            .rewind()
            .eq('M', 'Z')
            .ifFalse(_ => throw new ArgumentException());
        }

        [Fact]
        public void charBitsTest2()
        {
            using var stream = new NativeStream(new FileStream(RXW_X, FileMode.Open, FileAccess.Read, FileShare.Read));

            Assert.Equal('婍', stream.readWChar());
            Assert.Equal('M', stream.rewind().readTChar());
            Assert.Equal('遚', stream.readTChar(CharType.TwoByte));
            Assert.Equal('M', stream.rewind().readTChar(CharType.OneByte));

            stream.set(CharType.Unicode);
            Assert.Equal('Z', stream.readChar());
            Assert.Equal('婍', stream.rewind().readTChar());

            stream.set(CharType.Ascii);
            Assert.Equal('M', stream.rewind().readTChar());
        }
    }
}
