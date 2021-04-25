using System;
using net.r_eg.Conari.Types;
using Xunit;

namespace ConariTest.Types
{
    [Collection("Sequential")]
    public class TCharPtrSequentialTest
    {
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
