using System;
using net.r_eg.Conari.Types;
using Xunit;

namespace ConariTest.Types
{
    public class UnmanagedStringTest
    {
        [Fact]
        public void allocFreeTest1()
        {
            string managed = " my string 123 ";

            UnmanagedString uns;
            using(uns = new UnmanagedString(managed, UnmanagedString.SType.Ansi))
            {
                CharPtr actual = uns;
                Assert.Equal(managed.Length, ((string)actual).Length);
                Assert.Equal(UnmanagedString.SType.Ansi, uns.Type);
            }

            Assert.Equal(IntPtr.Zero, (IntPtr)uns);
        }

        [Fact]
        public void allocFreeTest2()
        {
            string managed = " my string 123 ";

            UnmanagedString uns;
            using(uns = new UnmanagedString(managed, UnmanagedString.SType.Unicode)) {
                WCharPtr actual = uns;
                Assert.Equal(managed.Length, ((string)actual).Length);
                Assert.Equal(UnmanagedString.SType.Unicode, uns.Type);
            }

            Assert.Equal(IntPtr.Zero, (IntPtr)uns);
        }

        [Fact]
        public void allocFreeTest3()
        {
            string managed = " my string 123 ";

            UnmanagedString uns;
            using(uns = new UnmanagedString(managed, UnmanagedString.SType.BSTR)) {
                BSTR actual = uns;
                Assert.Equal(managed.Length, ((string)actual).Length);
                Assert.Equal(UnmanagedString.SType.BSTR, uns.Type);
            }

            Assert.Equal(IntPtr.Zero, (IntPtr)uns);
        }

        [Fact]
        public void pointerTest1()
        {
            string managed = " my string 123 ";

            UnmanagedString uns, uns2;
            using(uns = new UnmanagedString(managed, UnmanagedString.SType.Unicode))
            {
                IntPtr ptr = uns;

                using(uns2 = new UnmanagedString(ptr, uns.Type))
                {
                    Assert.Equal(uns.Pointer, uns2.Pointer);
                    Assert.Equal(uns.Type, uns2.Type);

                    Assert.False(uns2.Owner);
                    Assert.True(uns.Owner);

                    Assert.Equal(managed.Length, uns2.Data.Length);
                    Assert.Equal(managed, uns2.Data);
                }
            }

            Assert.Equal(IntPtr.Zero, (IntPtr)uns);
            Assert.Equal(IntPtr.Zero, (IntPtr)uns2);
        }

        [Fact]
        public void ctorTest1()
        {
            Assert.Throws<ArgumentNullException>(() => 
                new UnmanagedString(null)
            );
        }

        [Fact]
        public void ctorTest2()
        {
            Assert.Throws<ArgumentException>(() =>
                new UnmanagedString(IntPtr.Zero, UnmanagedString.SType.Unicode)
            );
        }
    }
}
