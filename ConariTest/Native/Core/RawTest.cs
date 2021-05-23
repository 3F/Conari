using System;
using System.Linq;
using net.r_eg.Conari.Native;
using net.r_eg.Conari.Native.Core;
using Xunit;

namespace ConariTest.Native.Core
{
    public class RawTest
    {
        [Fact]
        public void ctorTest1()
        {
            var seq = new byte[] { 1, 2, 4, 5 };
            using Allocator exp = new(seq);
            Memory mem = exp.Memory;

            var raw = new Raw(mem, seq.Length, 1);
            byte[] final = raw.Values;

            Assert.Equal(3, final.Length);
            Assert.Equal(seq[1], final[0]);
            Assert.Equal(seq[2], final[1]);
            Assert.Equal(seq[3], final[2]);

            var itres = raw.Iter.ToArray();

            Assert.Equal(3, itres.Length);
            Assert.Equal(seq[1], itres[0]);
            Assert.Equal(seq[2], itres[1]);
            Assert.Equal(seq[3], itres[2]);
        }

        [Fact]
        public void ctorTest2()
        {
            Assert.Throws<ArgumentNullException>(() => 
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
