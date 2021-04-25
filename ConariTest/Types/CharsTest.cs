using System;
using net.r_eg.Conari.Types;
using Xunit;

namespace ConariTest.Types
{
    public class CharsTest
    {
        [Fact]
        public void wcharTest1()
        {
            string str1 = "あいうえお ;";
            string str2 = "あいうえお .";

            using UnmanagedString uns = new(str1, UnmanagedString.SType.Unicode);

            WCharPtr ch = uns;
            Assert.Equal("あいうえお ;", ch);
            Assert.Equal(7, ch.StrLength);
            Assert.Equal(14, ch.Length);

            using UnmanagedString uns2 = new(str1, UnmanagedString.SType.Unicode);
            WCharPtr ch2 = uns2;
            WCharPtr ch3 = uns;
            Assert.Equal(ch2, ch);
            Assert.Equal(ch3, ch);

            using UnmanagedString uns3 = new(str2, UnmanagedString.SType.Unicode);
            WCharPtr ch4 = uns3;
            Assert.NotEqual(ch4, ch);
        }

        [Fact]
        public void wcharCtorTest1()
        {
            using UnmanagedString uns = new(String.Empty, UnmanagedString.SType.Unicode);

            WCharPtr ch = uns;
            Assert.NotEqual(WCharPtr.Null, ch);
            Assert.Equal(WCharPtr.Null, new WCharPtr());
            Assert.Equal(0, ch.StrLength);
            Assert.Equal(0, ch.Length);

            Assert.Throws<ArgumentOutOfRangeException>(() => (WCharPtr)IntPtr.Zero);
        }

        [Fact]
        public void wcharCtorTest2()
        {
            using UnmanagedString uns = new(" ", UnmanagedString.SType.Unicode);

            WCharPtr ch = uns;
            Assert.NotEqual(WCharPtr.Null, ch);
            Assert.Equal(1, ch.StrLength);
            Assert.Equal(2, ch.Length);
        }

        [Fact]
        public void charTest1()
        {
            string str1 = "test of chars ;";
            string str2 = "test of chars .";

            using UnmanagedString uns = new(str1, UnmanagedString.SType.Ansi);

            CharPtr ch = uns;
            Assert.Equal("test of chars ;", ch);
            Assert.Equal(15, ch.StrLength);
            Assert.Equal(15, ch.Length);

            using UnmanagedString uns2 = new(str1, UnmanagedString.SType.Ansi);
            CharPtr ch2 = uns2;
            CharPtr ch3 = uns;
            Assert.Equal(ch2, ch);
            Assert.Equal(ch3, ch);

            using UnmanagedString uns3 = new(str2, UnmanagedString.SType.Ansi);
            CharPtr ch4 = uns3;
            Assert.NotEqual(ch4, ch);
        }

        [Fact]
        public void charCtorTest1()
        {
            using UnmanagedString uns = new(String.Empty, UnmanagedString.SType.Ansi);

            CharPtr ch = uns;
            Assert.NotEqual(CharPtr.Null, ch);
            Assert.Equal(CharPtr.Null, new CharPtr());
            Assert.Equal(0, ch.StrLength);
            Assert.Equal(0, ch.Length);

            Assert.Throws<ArgumentOutOfRangeException>(() => (CharPtr)IntPtr.Zero);
        }

        [Fact]
        public void charCtorTest2()
        {
            using UnmanagedString uns = new(" ", UnmanagedString.SType.Ansi);

            CharPtr ch = uns;
            Assert.NotEqual(CharPtr.Null, ch);
            Assert.Equal(1, ch.StrLength);
            Assert.Equal(1, ch.Length);
        }

        [Fact]
        public void ucharTest1()
        {
            string str1 = "Hello, ... !";

            using UnmanagedString uns1 = new(str1, UnmanagedString.SType.Unicode);
            TCharPtr ch1 = (WCharPtr)uns1;

            using UnmanagedString uns2 = new(str1, UnmanagedString.SType.Ansi);
            TCharPtr ch2 = (CharPtr)uns2;

            Assert.Equal(str1, ch1);
            Assert.True(ch1.IsWideChar);
            Assert.Equal(24, ch1.Length);
            Assert.Equal(12, ch1.StrLength);
            Assert.Equal((WCharPtr)uns1, (WCharPtr)ch1);
            Assert.Throws<NotSupportedException>(() => (CharPtr)ch1);

            Assert.Equal(str1, ch2);
            Assert.False(ch2.IsWideChar);
            Assert.Equal(12, ch2.Length);
            Assert.Equal(12, ch2.StrLength);
            Assert.Equal((CharPtr)uns2, (CharPtr)ch2);
            Assert.Throws<NotSupportedException>(() => (WCharPtr)ch2);
        }

        [Fact]
        public void diffCharsTest1()
        {
            Assert.True(WCharPtr.Null == new WCharPtr());
            Assert.False(WCharPtr.Null != new WCharPtr());
            Assert.False(WCharPtr.Null == CharPtr.Null);
            Assert.True(WCharPtr.Null != CharPtr.Null);

            Assert.True(CharPtr.Null == new CharPtr());
            Assert.False(CharPtr.Null != new CharPtr());
            Assert.False(CharPtr.Null == WCharPtr.Null);
            Assert.True(CharPtr.Null != WCharPtr.Null);

            Assert.True(TCharPtr.Null == new TCharPtr());
            Assert.False(TCharPtr.Null != new TCharPtr());

            Assert.True(TCharPtr.Null == (TCharPtr)CharPtr.Null);
            Assert.True(TCharPtr.Null == (TCharPtr)WCharPtr.Null);

            Assert.False(TCharPtr.Null != (TCharPtr)CharPtr.Null);
            Assert.False(TCharPtr.Null != (TCharPtr)WCharPtr.Null);
        }
    }
}
