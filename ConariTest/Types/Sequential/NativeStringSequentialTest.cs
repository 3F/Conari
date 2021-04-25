using System;
using net.r_eg.Conari;
using net.r_eg.Conari.Types;
using Xunit;

namespace ConariTest.Types.Sequential
{
    using static _svc.TestHelper;

    [Collection("Sequential")]
    public class NativeStringSequentialTest
    {
        [Fact]
        public void strTest1()
        {
            TCharPtr.__Unicode = false;

            using dynamic l = new ConariX(RXW_X64);
            using var data = new NativeString<TCharPtr>("Hello {p}!", 2);

            using NativeString<TCharPtr> filter = new("{p}");
            using NativeString<TCharPtr> replacement = new("world");

            Assert.True(l.replace<bool>((IntPtr)data, (IntPtr)filter, (IntPtr)replacement));
            Assert.Equal("Hello world!", (TCharPtr)data);

            Assert.True(data.Owner);
            Assert.True(filter.Owner);
            Assert.True(replacement.Owner);
        }
    }
}
