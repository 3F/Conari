using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using net.r_eg.Conari.Types;

namespace net.r_eg.ConariTest.Types
{
    [TestClass]
    public class UnmanagedStructureTest
    {
        [TestMethod]
        public void allocFreeTest1()
        {
            var managed = new TVer(0, 32, 1024);

            UnmanagedStructure uv;
            using(uv = new UnmanagedStructure(managed))
            {
                Assert.AreNotEqual(0, uv.SizeOf);
                Assert.AreNotEqual(null, uv.Managed);
            }

            Assert.AreEqual(IntPtr.Zero, (IntPtr)uv);
        }

        [TestMethod]
        public void allocFreeTest2()
        {
            var managed = new TVer(0, 32, 1024);

            UnmanagedStructure uv;
            using(uv = new UnmanagedStructure(managed))
            {
                IntPtr ptr = uv;

                var uv2 = new UnmanagedStructure(ptr, typeof(TVer));
                
                TVer managed2 = (TVer)uv2.Managed;

                Assert.AreEqual(((TVer)uv.Managed).major, managed2.major);
                Assert.AreEqual(((TVer)uv.Managed).minor, managed2.minor);
                Assert.AreEqual(((TVer)uv.Managed).patch, managed2.patch);
            }

            Assert.AreEqual(IntPtr.Zero, (IntPtr)uv);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ctorTest1()
        {
            new UnmanagedStructure(null);
        }
        
    }
}
