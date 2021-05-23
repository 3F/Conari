using System;
using net.r_eg.Conari.Types;
using Xunit;

namespace ConariTest.Types.Sequential
{
    [Collection("Sequential")]
    public class TCharPtrSequentialTest
    {
        [Fact]
        public void charTest1()
        {
            TCharPtr.__Unicode = false;

            using var data = new NativeString<TCharPtr>("Hello world");

            TCharPtr tch = data;
            CharPtr ch  = data;

            Assert.Equal(tch, ch);

            Assert.False(TCharPtr.Unicode);
            Assert.Equal(11, tch.Length);
            Assert.Equal(11, tch.StrLength);
        }

        [Fact]
        public void wcharTest1()
        {
            TCharPtr.__Unicode = true;

            using var data = new NativeString<TCharPtr>("Hello world");

            TCharPtr tch = data;
            WCharPtr ch = data;

            Assert.Equal(tch, ch);

            Assert.True(TCharPtr.Unicode);
            Assert.Equal(22, tch.Length);
            Assert.Equal(11, tch.StrLength);
        }

#if F_UCHAR_IMPL

        //NOTE: L-175. This is possible only for UCharPtr since TCharPtr (wdata & data) at the same 0 position now.

        [Fact]
        public void ucharTest1()
        {
            string str1 = "Hello, ... !";

            TCharPtr.__Unicode = true;
            using NativeString<WCharPtr> uns1 = new(str1);
            TCharPtr ch1 = (WCharPtr)uns1;

            TCharPtr.__Unicode = false;
            using NativeString<CharPtr> uns2 = new(str1);
            TCharPtr ch2 = (CharPtr)uns2;

            Assert.Equal(str1, ch1);
            Assert.Equal(24, ch1.Length);
            Assert.Equal(12, ch1.StrLength);
            Assert.Equal((WCharPtr)uns1, (WCharPtr)ch1);
            Assert.Throws<NotSupportedException>(() => (CharPtr)ch1);

            Assert.Equal(str1, ch2);
            Assert.Equal(12, ch2.Length);
            Assert.Equal(12, ch2.StrLength);
            Assert.Equal((CharPtr)uns2, (CharPtr)ch2);
            Assert.Throws<NotSupportedException>(() => (WCharPtr)ch2);
        }

#endif

        [Fact]
        public void ctorTest1()
        {
            TCharPtr.__Unicode = null;

            TCharPtr.Unicode = true;
            Assert.Throws<NotSupportedException>(() => TCharPtr.Unicode = true);
            Assert.Throws<NotSupportedException>(() => TCharPtr.Unicode = false);

            TCharPtr.__Unicode = null;

            TCharPtr.Unicode = false;
            Assert.Throws<NotSupportedException>(() => TCharPtr.Unicode = true);
            Assert.Throws<NotSupportedException>(() => TCharPtr.Unicode = false);
        }

        [Fact]
        public void castTest1()
        {
            TCharPtr.__Unicode = true;
            using NativeString<TCharPtr> ns1 = new("str");

            Assert.Equal("str", (WCharPtr)(TCharPtr)ns1);
            Assert.Throws<NotSupportedException>(() => (CharPtr)(TCharPtr)ns1);

            string str = (string)ns1;
            TCharPtr.__Unicode = false;
            using NativeString<TCharPtr> ns2 = new(str);

            Assert.Equal("str", (CharPtr)(TCharPtr)ns2);
            Assert.Throws<NotSupportedException>(() => (WCharPtr)(TCharPtr)ns2);
        }

        [Fact]
        public void castTest2()
        {
            TCharPtr.__Unicode = true;
            Assert.True(TCharPtr.Null == (TCharPtr)WCharPtr.Null);
            Assert.False(TCharPtr.Null != (TCharPtr)WCharPtr.Null);

            TCharPtr.__Unicode = false;
            Assert.True(TCharPtr.Null == (TCharPtr)CharPtr.Null);
            Assert.False(TCharPtr.Null != (TCharPtr)CharPtr.Null);
        }

        [Fact]
        public void castTest3()
        {
            TCharPtr.__Unicode = false;
            Assert.Throws<NotSupportedException>(() => (TCharPtr)WCharPtr.Null);
            Assert.Throws<NotSupportedException>(() => (TCharPtr)WCharPtr.Null);

            TCharPtr.__Unicode = true;
            Assert.Throws<NotSupportedException>(() => (TCharPtr)CharPtr.Null);
            Assert.Throws<NotSupportedException>(() => (TCharPtr)CharPtr.Null);
        }
    }
}
