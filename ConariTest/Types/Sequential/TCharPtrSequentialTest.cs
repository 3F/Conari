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
            Assert.False(tch.IsWideChar);
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
            Assert.True(tch.IsWideChar);
            Assert.Equal(22, tch.Length);
            Assert.Equal(11, tch.StrLength);
        }

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
    }
}
