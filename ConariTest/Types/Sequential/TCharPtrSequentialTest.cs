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
