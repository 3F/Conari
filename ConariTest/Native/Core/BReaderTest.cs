using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using net.r_eg.Conari.Native.Core;

namespace net.r_eg.ConariTest.Native.Core
{
    [TestClass]
    public class BReaderTest
    {
        [TestMethod]
        public void nextValTest1()
        {
            var br = new BReader(new byte[] {
                5, 0, 0, 0, // Int32 = 5
                7, 0, 0, 0, // Int32 = 7
                31          // SByte = 31
            });

            Assert.AreEqual(5, br.next(typeof(Int32), 4));
            Assert.AreEqual(7, br.next(typeof(Int32), 4));
            Assert.AreEqual(31, br.next(typeof(sbyte), 1));
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void nextValTest2()
        {
            var br = new BReader(new byte[] {
                5, 0, 0, 0, // Int32 = 5
                31          // SByte = 31
            });

            br.next(typeof(Int32), 4);
            br.next(typeof(Int32), 4);
        }

        [TestMethod]
        public void nextValTest3()
        {
            var br = new BReader(new byte[] {
                5, 0, 0, 0, // Int32 = 5
            });

            Assert.AreEqual(5, br.next(typeof(Int32), 4));
            br.reset();
            Assert.AreEqual(5, br.next(typeof(Int32), 4));

            dynamic value;
            Assert.AreEqual(false, br.tryNext(typeof(Int32), 4, out value));
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void GetValTest1()
        {
            int offset  = 0;
            byte[] data = new byte[] { 1, 2, 3, 4 };
            BReader.GetValue(typeof(UserSpecUintType), 1, ref offset, ref data);
        }
    }
}
