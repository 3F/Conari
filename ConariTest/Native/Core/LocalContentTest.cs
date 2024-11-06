/*!
 * Copyright (c) 2016  Denis Kuzmin <x-3F@outlook.com> github/3F
 * Copyright (c) Conari contributors https://github.com/3F/Conari/graphs/contributors
 * Licensed under the MIT License (MIT).
 * See accompanying LICENSE.txt file or visit https://github.com/3F/Conari
*/

using System;
using net.r_eg.Conari.Exceptions;
using net.r_eg.Conari.Native;
using net.r_eg.Conari.Native.Core;
using Xunit;

namespace ConariTest.Native.Core
{
    public class LocalContentTest
    {
        [Fact]
        public void charBitsTest1()
        {
            new byte[] { 0x30, 0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37, 0x38, 0x39 }
            .Access()
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
            IAccessor acs = data.Native().Access;

            Assert.Equal('㄰', acs.readWChar());
            Assert.Equal('2', acs.readChar());
            Assert.Equal('㐳', acs.readChar(CharType.TwoByte));
            Assert.Equal('5', acs.readChar(CharType.OneByte));

            acs.set(CharType.Unicode);
            Assert.Equal('6', acs.readAChar());
            Assert.Equal('㠷', acs.readChar());

            acs.set(CharType.Ascii);
            Assert.Equal('9', acs.readChar());
        }

        [Fact]
        public void cmpCharsTest1()
        {
            byte[] seq = { (byte)'M', (byte)'Z', 0, (byte)'P', (byte)'E', 0, 0 };

            Assert.True
            (
                seq.Access()
                .eq("MZ\0".ToCharArray())
                .ifFalse(_ => throw new ArgumentException())
                .eq("PE\0\0".ToCharArray())
                .check()
            );
        }

        [Fact]
        public void ctorTest1()
        {
            Assert.Throws<ArgumentNullException>(() => new LocalContent(null));

            Assert.Throws<InvalidOrUnavailableRangeException>(() => new LocalContent().readInt32());
            Assert.Throws<InvalidOrUnavailableRangeException>(() => new LocalContent(4).readInt16());

            Assert.Equal(1026, new LocalContent(2, 4).readInt16());
        }
    }
}
