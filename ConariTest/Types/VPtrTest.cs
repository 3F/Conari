/*!
 * Copyright (c) 2016  Denis Kuzmin <x-3F@outlook.com> github/3F
 * Copyright (c) Conari contributors https://github.com/3F/Conari/graphs/contributors
 * Licensed under the MIT License (MIT).
 * See accompanying LICENSE.txt file or visit https://github.com/3F/Conari
*/

using System;
using net.r_eg.Conari.Types;
using Xunit;

namespace ConariTest.Types
{
    public class VPtrTest
    {
        [Fact]
        public void ctorTest1()
        {
            VPtr n1 = 145323552783335428;
            Assert.True(n1.IsLong);
            Assert.Equal(145323552783335428, (long)n1);
            Assert.Equal(IntPtr.Zero, (IntPtr)n1);
        }

        [Fact]
        public void ctorTest2()
        {
            VPtr n1 = new IntPtr(33273088);
            Assert.False(n1.IsLong);
            Assert.Equal(0, (long)n1);
            Assert.Equal(new IntPtr(33273088), (IntPtr)n1);
        }

        [Fact]
        public void cmpTest1()
        {
            VPtr n1 = 145323552783335428;
            VPtr n2 = -145323552783335428;
            VPtr n3 = 145323552783335428;

            Assert.Equal(n1, n3);
            Assert.NotEqual(n1, n2);
        }

        [Fact]
        public void cmpTest2()
        {
            VPtr n1 = new IntPtr(33273088);
            VPtr n2 = new IntPtr(-33273088);
            VPtr n3 = new IntPtr(33273088);

            Assert.Equal(n1, n3);
            Assert.NotEqual(n1, n2);
        }

        [Fact]
        public void cmpTest3()
        {
            VPtr n = new(new IntPtr(2));

            Assert.True(n > 0);
            Assert.True(n >= 2);
            Assert.False(n > 2);

            Assert.False(n < 0);
            Assert.True(n <= 2);
            Assert.False(n < 2);

            Assert.True(n == 2);
            Assert.False(n != 2);
            Assert.False(n == 0);
            Assert.True(n != 0);
        }

        [Fact]
        public void cmpTest4()
        {
            VPtr n = new(2);

            Assert.True(n > 0);
            Assert.True(n >= 2);
            Assert.False(n > 2);

            Assert.False(n < 0);
            Assert.True(n <= 2);
            Assert.False(n < 2);

            Assert.True(n == 2);
            Assert.False(n != 2);
            Assert.False(n == 0);
            Assert.True(n != 0);
        }

        [Fact]
        public void incrementTest1()
        {
            VPtr n1 = 145323552783335428;
            n1 += 2;
            ++n1;

            Assert.Equal(145323552783335431, (long)n1);

            VPtr offset = new IntPtr(7);
            n1 += offset;

            Assert.Equal(145323552783335438, (long)n1);
        }

        [Fact]
        public void incrementTest2()
        {
            VPtr n1 = new IntPtr(33273088);
            n1 += 2;
            ++n1;

            Assert.Equal(new IntPtr(33273091), (IntPtr)n1);

            long offset = 7;
            n1 += offset;

            Assert.Equal(new IntPtr(33273098), (IntPtr)n1);
        }

        [Fact]
        public void incrementTest3()
        {
            VPtr n1 = new IntPtr(0xB);
            VPtr n2 = new IntPtr(0x7FFF89EB0110);

            VPtr expected = new IntPtr(0x7FFF89EB011B);

            Assert.Equal(expected, n1 + n2);

            VPtr n3 = (long)0x7FFF89EB0110;

            Assert.Equal(expected, n1 + n3);
        }

        [Fact]
        public void incrementTest4()
        {
            VPtr n1 = new IntPtr(0xB);
            VPtr n2 = new IntPtr(-0x7FFF89EB0110);

            VPtr expected = new IntPtr(unchecked((long)0xFFFF80007614FEFB));

            Assert.Equal(expected, (VPtr)new IntPtr(-0x7FFF89EB0105));

            Assert.Equal(expected, n1 + n2);

            VPtr n3 = (long)-0x7FFF89EB0110;

            Assert.Equal(expected, n1 + n3);
        }

        [Fact]
        public void incrementTest5()
        {
            VPtr n1 = new IntPtr(-0xB);
            VPtr n2 = new IntPtr(0x7FFF89EB0110);

            VPtr expected = new IntPtr(0x7FFF89EB0105);

            Assert.Equal(expected, n1 + n2);

            VPtr n3 = (long)0x7FFF89EB0110;

            Assert.Equal(expected, n1 + n3);
        }

        [Fact]
        public void incrementTest6()
        {
            VPtr n1 = new IntPtr(-0xB);
            VPtr n2 = new IntPtr(-0x7FFF89EB0110);

            VPtr expected = new IntPtr(unchecked((long)0xFFFF80007614FEE5));

            Assert.Equal(expected, (VPtr)new IntPtr(-0x7FFF89EB011B));

            Assert.Equal(expected, n1 + n2);

            VPtr n3 = (long)-0x7FFF89EB0110;

            Assert.Equal(expected, n1 + n3);
        }

        [Fact]
        public void decrementTest1()
        {
            VPtr n1 = 145323552783335428;
            n1 -= 2;
            n1--;

            Assert.Equal(145323552783335425, (long)n1);

            VPtr offset = new IntPtr(7);
            n1 -= offset;

            Assert.Equal(145323552783335418, (long)n1);
        }

        [Fact]
        public void decrementTest2()
        {
            VPtr n1 = new IntPtr(33273088);
            n1 -= 2;
            n1--;

            Assert.Equal(new IntPtr(33273085), (IntPtr)n1);

            long offset = 7;
            n1 -= offset;

            Assert.Equal(new IntPtr(33273078), (IntPtr)n1);
        }

        [Fact]
        public void decrementTest3()
        {
            VPtr n1 = new IntPtr(0x7FFF89EB011B);
            VPtr n2 = new IntPtr(0x7FFF89EB0110);

            VPtr expected = new IntPtr(0xB);

            Assert.Equal(expected, n1 - n2);

            VPtr n3 = (long)0x7FFF89EB0110;

            Assert.Equal(expected, n1 - n3);
        }

        [Fact]
        public void decrementTest4()
        {
            VPtr n1 = new IntPtr(0xB);
            VPtr n2 = new IntPtr(-0x7FFF89EB0110);

            VPtr expected = new IntPtr(0x7FFF89EB011B);

            Assert.Equal(expected, n1 - n2);

            VPtr n3 = (long)-0x7FFF89EB0110;

            Assert.Equal(expected, n1 - n3);
        }

        [Fact]
        public void decrementTest5()
        {
            VPtr n1 = new IntPtr(-0x7FFF89EB011B);
            VPtr n2 = new IntPtr(0x7FFF89EB0110);

            VPtr expected = new IntPtr(unchecked((long)0xFFFF0000EC29FDD5));
            Assert.Equal(expected, (VPtr)new IntPtr(-0xFFFF13D6022B));

            Assert.Equal(expected, n1 - n2);

            VPtr n3 = (long)0x7FFF89EB0110;

            Assert.Equal(expected, n1 - n3);
        }

        [Fact]
        public void decrementTest6()
        {
            VPtr n1 = new IntPtr(-0xB);
            VPtr n2 = new IntPtr(-0x7FFF89EB0110);

            VPtr expected = new IntPtr(0x7FFF89EB0105);

            Assert.Equal(expected, n1 - n2);

            VPtr n3 = (long)-0x7FFF89EB0110;

            Assert.Equal(expected, n1 - n3);
        }

        [Fact]
        public void zeroTest1()
        {
            VPtr n1 = new(33273088);
            VPtr n2 = VPtr.Zero;
            VPtr n3 = new();

            Assert.NotEqual(n1, n2);
            Assert.Equal(VPtr.Zero, n2);
            Assert.NotEqual(VPtr.Zero, n1);
            Assert.Equal(VPtr.Zero, n3);
        }

        [Fact]
        public void zeroTest2()
        {
            VPtr n1 = new(0);
            VPtr n2 = new(IntPtr.Zero);
            VPtr n3 = VPtr.Zero;
            VPtr n4 = 0;

            Assert.NotEqual(n1, n2);
            Assert.NotEqual(n1, n3);
            Assert.Equal(n1, n4);
            Assert.Equal(n2, n3);

            Assert.Equal(VPtr.ZeroLong, n1);
            Assert.NotEqual(VPtr.Zero, VPtr.ZeroLong);
        }
    }
}
