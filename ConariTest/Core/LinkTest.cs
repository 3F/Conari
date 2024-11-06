/*!
 * Copyright (c) 2016  Denis Kuzmin <x-3F@outlook.com> github/3F
 * Copyright (c) Conari contributors https://github.com/3F/Conari/graphs/contributors
 * Licensed under the MIT License (MIT).
 * See accompanying LICENSE.txt file or visit https://github.com/3F/Conari
*/

using System;
using net.r_eg.Conari.Core;
using Xunit;

namespace ConariTest.Core
{
    public class LinkTest
    {
        [Fact]
        public void ctorTest1()
        {
            var l = new Link();

            Assert.Equal(IntPtr.Zero, l.handle);
            Assert.False(l.resolved);
            Assert.False(l.isolated);
            Assert.False(l.IsActive);
            Assert.Null(l.module);
        }

        [Fact]
        public void ctorTest2()
        {
            string lib      = "test.dll";
            IntPtr handle   = (IntPtr)1;

            var l = new Link(handle, lib, true); 

            Assert.Equal(handle, l.handle);
            Assert.True(l.IsActive);
            Assert.Equal(lib, l.module);
            Assert.False(l.resolved);
            Assert.True(l.isolated);
        }

        [Fact]
        public void ctorTest3()
        {
            string lib = "test.dll";

            var l = new Link(lib);

            Assert.Equal(IntPtr.Zero, l.handle);
            Assert.False(l.IsActive);
            Assert.Equal(lib, l.module);
            Assert.False(l.resolved);
            Assert.False(l.isolated);
        }
    }
}
