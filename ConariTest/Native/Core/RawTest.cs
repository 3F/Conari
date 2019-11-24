using System;
using System.Linq;
using net.r_eg.Conari.Native.Core;
using Xunit;

namespace ConariTest.Native.Core
{
    public class RawTest
    {
        [Fact]
        public void ctorTest1()
        {
            Assert.Throws<ArgumentException>(() => 
                new Raw(IntPtr.Zero, 4)
            );
        }

        [Fact]
        public void ctorTest2()
        {
            Assert.Throws<ArgumentException>(() => 
                new Raw((byte[])null)
            );
        }

        [Fact]
        public void ctorTest3()
        {
            var exp = new byte[] { 1, 2 };
            var raw = new Raw(exp);

            Assert.Equal(2, raw.Values.Length);
            Assert.Equal(exp[0], raw.Values[0]);
            Assert.Equal(exp[1], raw.Values[1]);

            var actualIter = raw.Iter.ToArray();

            Assert.Equal(2, actualIter.Length);
            Assert.Equal(exp[0], actualIter[0]);
            Assert.Equal(exp[1], actualIter[1]);
        }
    }
}
