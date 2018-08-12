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

        [TestMethod]
        public void pointerTest1()
        {
            string managed = " my string 123 ";

            UnmanagedString uns, uns2;
            using(uns = new UnmanagedString(managed, UnmanagedString.SType.Unicode))
            {
                IntPtr ptr = uns;

                using(uns2 = new UnmanagedString(ptr, uns.Type))
                {
                    Assert.AreEqual(uns.Pointer, uns2.Pointer);
                    Assert.AreEqual(uns.Type, uns2.Type);

                    Assert.AreEqual(false, uns2.Owner);
                    Assert.AreEqual(true, uns.Owner);

                    Assert.AreEqual(managed.Length, uns2.Data.Length);
                    Assert.AreEqual(managed, uns2.Data);
                }
            }

            Assert.AreEqual(IntPtr.Zero, (IntPtr)uns);
            Assert.AreEqual(IntPtr.Zero, (IntPtr)uns2);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ctorTest1()
        {
            new UnmanagedString(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ctorTest2()
        {
            new UnmanagedString(IntPtr.Zero, UnmanagedString.SType.Unicode);
        }
    }
}
