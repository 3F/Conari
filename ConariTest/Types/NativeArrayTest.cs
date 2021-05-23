using System;
using System.Collections.Generic;
using net.r_eg.Conari.Native.Core;
using net.r_eg.Conari.Types;
using Xunit;
using static net.r_eg.Conari.Static.Members;

namespace ConariTest.Types
{
    public class NativeArrayTest
    {
        public static IEnumerable<object[]> allocInputTest1()
        {
            NativeArray nr = new(1, 2, -1);
            yield return new object[] { nr, true };

            NativeArray<int> nr2 = new(nr.Length, nr);
            yield return new object[] { nr2, false };

            NativeArray<int> nr3 = new(nr.Length);
            nr3[0] = 1;
            nr3[1] = 2;
            nr3[2] = -1;
            yield return new object[] { nr3, true };
        }

        [Theory]
        [MemberData(nameof(allocInputTest1))]
        public void allocTest1(NativeArray<int> nr, bool owner)
        {
            using IDisposable _ = nr;

            Assert.Equal(3, nr.Length);
            Assert.Equal(1, nr[0]);
            Assert.Equal(2, nr[1]);
            Assert.Equal(-1, nr[2]);

            Assert.Equal(owner, nr.Owner);
            Assert.Equal(SizeOf<int>(nr.Length), nr.Size);
        }

        [Fact]
        public void allocTest2()
        {
            using NativeArray<int> nr = new(1, 2, 3, 4);

            NativeArray<int> nr2 = new(nr.Length, nr);

            nr2[0] = -1;
            nr2[1] = 0;
            nr2[2] = 1;
            nr2[3] = 2;

            Assert.Equal(4, nr2.Length);

            Assert.Equal(-1, nr[0]);
            Assert.Equal(0, nr[1]);
            Assert.Equal(1, nr[2]);
            Assert.Equal(2, nr[3]);
        }

        [Fact]
        public void allocTest3()
        {
            using NativeArray<int> nr = new(1, 2, 3, 4);

            NativeArray<int> nr2 = new(nr);

            nr2[0] = -1;
            nr2[1] = 0;
            nr2[2] = 1;
            nr2[3] = 2;

            Assert.Equal(4, nr2.Length);

            Assert.Equal(1, nr[0]);
            Assert.Equal(2, nr[1]);
            Assert.Equal(3, nr[2]);
            Assert.Equal(4, nr[3]);
        }

        [Fact]
        public void memTest1()
        {
            using NativeArray nr = new(2, -4, 6);
            Memory memory = nr;

            Assert.Equal(3, nr.Length);

            memory.read(nr.Length, out int[] r);

            Assert.Equal(2, r[0]);
            Assert.Equal(-4, r[1]);
            Assert.Equal(6, r[2]);

            Assert.Equal(r[0], nr[0]);
            Assert.Equal(r[1], nr[1]);
            Assert.Equal(r[2], nr[2]);
        }

        public static IEnumerable<object[]> ctorInputInt()
        {
            yield return new object[] { Array.Empty<int>() };
            yield return new object[] { new int[] { 1, 2, 3 } };
        }

        [Theory]
        [MemberData(nameof(ctorInputInt))]
        public void ctorTest1(int[] input)
        {
            using var nr = new NativeArray<int>(input);
            Assert.Equal(input.Length, nr.Length);
            Assert.True(nr.Owner);
            Assert.Equal(SizeOf<int>(nr.Length), nr.Size);
        }

        [Fact]
        public void ctorTest2()
        {
            Assert.Throws<ArgumentNullException>(() => new NativeArray<int>(null));
            Assert.Throws<ArgumentOutOfRangeException>(() => new NativeArray<int>(-1));
            Assert.Throws<ArgumentOutOfRangeException>(() => new NativeArray<int>(2, IntPtr.Zero));
            Assert.Throws<ArgumentOutOfRangeException>(() => new NativeArray<int>(-2, (IntPtr)1));
        }

        [Fact]
        public void ctorTest3()
        {
            Assert.Throws<ArgumentNullException>(() => new NativeArray(null));
            Assert.Throws<ArgumentOutOfRangeException>(() => new NativeArray(-1));
            Assert.Throws<ArgumentOutOfRangeException>(() => new NativeArray(2, IntPtr.Zero));
            Assert.Throws<ArgumentOutOfRangeException>(() => new NativeArray(-2, (IntPtr)1));
        }

        [Fact]
        public void ctorTest4()
        {
            using NativeArray<int> nr = new();
            Assert.Equal(0, nr.Length);
            Assert.Equal(0, nr.Size);
            Assert.True(nr.Owner);
            Assert.Throws<ArgumentOutOfRangeException>(() => nr[0]);
        }

        [Fact]
        public void cmpTest1()
        {
            using NativeArray<int> nr1 = new(4, 5, 7);
            using NativeArray<int> nr2 = new(4, 2, 7);
            using NativeArray<int> nr3 = new(4, 5, 7);

            Assert.Equal(nr1, nr3);
            Assert.True(nr1 == nr3);
            Assert.False(nr1 != nr3);

            Assert.NotEqual(nr1, nr2);
            Assert.False(nr1 == nr2);
            Assert.True(nr1 != nr2);
        }

        [Fact]
        public void cmpTest2()
        {
            using NativeArray<int> nr1 = new(4, 5, 7);

            int[] r = { 4, 5, 7 };

            Assert.Equal(nr1, r);
            Assert.True(nr1 == r);
            Assert.False(nr1 != r);

            Assert.Equal(r, nr1);
            Assert.True(r == nr1);
            Assert.False(r != nr1);
        }

        [Fact]
        public void cmpTest3()
        {
            using NativeArray<int> nr1 = new(4, 5, 7);
            using NativeArray<int> nr2 = new(nr1);

            Assert.Equal(nr1, nr2);
            Assert.True(nr1 == nr2);
            Assert.False(nr1 != nr2);
        }
    }
}
