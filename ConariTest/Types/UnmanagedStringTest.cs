using System;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using net.r_eg.Conari.Extension;
using net.r_eg.Conari.Types;

namespace net.r_eg.ConariTest.Types
{
    [TestClass]
    public class UnmanagedStringTest
    {
        [TestMethod]
        public void allocFreeTest1()
        {
            string managed = " my string 123 ";

            UnmanagedString uns;
            using(uns = new UnmanagedString(managed, UnmanagedString.SType.Ansi))
            {
                CharPtr actual = uns;
                Assert.AreEqual(managed.Length, ((string)actual).Length);
                Assert.AreEqual(UnmanagedString.SType.Ansi, uns.Type);
            }

            Assert.AreEqual(IntPtr.Zero, (IntPtr)uns);
        }

        [TestMethod]
        public void allocFreeTest2()
        {
            string managed = " my string 123 ";

            UnmanagedString uns;
            using(uns = new UnmanagedString(managed, UnmanagedString.SType.Unicode)) {
                WCharPtr actual = uns;
                Assert.AreEqual(managed.Length, ((string)actual).Length);
                Assert.AreEqual(UnmanagedString.SType.Unicode, uns.Type);
            }

            Assert.AreEqual(IntPtr.Zero, (IntPtr)uns);
        }

        [TestMethod]
        public void allocFreeTest3()
        {
            string managed = " my string 123 ";

            UnmanagedString uns;
            using(uns = new UnmanagedString(managed, UnmanagedString.SType.BSTR)) {
                BSTR actual = uns;
                Assert.AreEqual(managed.Length, ((string)actual).Length);
                Assert.AreEqual(UnmanagedString.SType.BSTR, uns.Type);
            }

            Assert.AreEqual(IntPtr.Zero, (IntPtr)uns);
        }
    }
}
