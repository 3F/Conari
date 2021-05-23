using System;
using net.r_eg.Conari.Types;
using Xunit;

namespace ConariTest.Types
{
#pragma warning disable CS0618 // Type or member is obsolete.
    public class UnmanagedStructureTest
    {
        [Fact]
        public void allocFreeTest1()
        {
            var managed = new TVer(0, 32, 1024);

            UnmanagedStructure uv;
            using(uv = new UnmanagedStructure(managed))
            {
                Assert.NotEqual(0, uv.SizeOf);
                Assert.NotNull(uv.Managed);
            }

            Assert.Equal(IntPtr.Zero, (IntPtr)uv);
        }

        [Fact]
        public void allocFreeTest2()
        {
            var managed = new TVer(0, 32, 1024);

            UnmanagedStructure uv;
            using(uv = new UnmanagedStructure(managed))
            {
                IntPtr ptr = uv;

                using UnmanagedStructure uv2 = new(ptr, typeof(TVer));

                TVer managed2 = (TVer)uv2.Managed;

                Assert.Equal(((TVer)uv.Managed).major, managed2.major);
                Assert.Equal(((TVer)uv.Managed).minor, managed2.minor);
                Assert.Equal(((TVer)uv.Managed).patch, managed2.patch);
            }

            Assert.Equal(IntPtr.Zero, (IntPtr)uv);
        }

        [Fact]
        public void ctorTest1()
        {
            Assert.Throws<ArgumentNullException>(() => 
                new UnmanagedStructure(null)
            );
        }
    }
#pragma warning restore CS0618 // Type or member is obsolete
}
