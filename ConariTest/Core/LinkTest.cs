using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using net.r_eg.Conari.Core;

namespace net.r_eg.ConariTest.Core
{
    [TestClass]
    public class LinkTest
    {
        [TestMethod]
        public void defvalueTest1()
        {
            var l = new Link();

            Assert.AreEqual(IntPtr.Zero, l.Handle);
            Assert.AreEqual(false, l.IsActive);
            //Assert.AreEqual(null, l.LibName);
        }

        [TestMethod]
        public void defvalueTest2()
        {
            string lib      = "test.dll";
            IntPtr handle   = (IntPtr)1;

            var l = new Link(handle, lib); 

            Assert.AreEqual(handle, l.Handle);
            Assert.AreEqual(true, l.IsActive);
            Assert.AreEqual(lib, l.LibName);
        }
    }
}
