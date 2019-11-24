using System;
using net.r_eg.Conari.Core;
using Xunit;

namespace ConariTest.Core
{
    public class LinkTest
    {
        [Fact]
        public void defvalueTest1()
        {
            var l = new Link();

            Assert.Equal(IntPtr.Zero, l.Handle);
            Assert.False(l.IsActive);
            //Assert.Equal(null, l.LibName);
        }

        [Fact]
        public void defvalueTest2()
        {
            string lib      = "test.dll";
            IntPtr handle   = (IntPtr)1;

            var l = new Link(handle, lib); 

            Assert.Equal(handle, l.Handle);
            Assert.True(l.IsActive);
            Assert.Equal(lib, l.LibName);
        }
    }
}
