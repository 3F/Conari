using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using net.r_eg.Conari.Native.Core;

namespace net.r_eg.ConariTest.Native.Core
{
    [TestClass]
    public class RawTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ctorTest1()
        {
            new Raw(IntPtr.Zero, 4);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ctorTest2()
        {
            new Raw((byte[])null);
        }

        [TestMethod]
        public void ctorTest3()
        {
            var exp = new byte[] { 1, 2 };
            var raw = new Raw(exp);

            Assert.AreEqual(2, raw.Values.Length);
            Assert.AreEqual(exp[0], raw.Values[0]);
            Assert.AreEqual(exp[1], raw.Values[1]);

            var actualIter = raw.Iter.ToArray();

            Assert.AreEqual(2, actualIter.Length);
            Assert.AreEqual(exp[0], actualIter[0]);
            Assert.AreEqual(exp[1], actualIter[1]);
        }
    }
}
