/*!
 * Copyright (c) 2016  Denis Kuzmin <x-3F@outlook.com> github/3F
 * Copyright (c) Conari contributors https://github.com/3F/Conari/graphs/contributors
 * Licensed under the MIT License (MIT).
 * See accompanying LICENSE.txt file or visit https://github.com/3F/Conari
*/

using System.Collections.Generic;
using net.r_eg.Conari.Extension;
using Xunit;

namespace ConariTest.Extensions
{
    public class MathExtensionTest
    {
        [Fact]
        public void CalculateHashCodeTest1()
        {
            int hash = 0.CalculateHashCode(typeof(int), typeof(long));

            Assert.NotEqual(0.CalculateHashCode(), hash);
            Assert.NotEqual(0.CalculateHashCode(typeof(long)), hash);
            Assert.NotEqual(0.CalculateHashCode(typeof(long), typeof(int)), hash);
            Assert.Equal(0.CalculateHashCode(typeof(int), typeof(long)), hash);
            Assert.NotEqual(0.CalculateHashCode(typeof(long), typeof(int)), hash);
        }

        [Fact]
        public void CalculateHashCodeTest2()
        {
            int hash = 0.CalculateHashCode(12, 24);

            Assert.NotEqual(0.CalculateHashCode(), hash);
            Assert.Equal(0.CalculateHashCode(12, 24), hash);
            Assert.NotEqual(0.CalculateHashCode(24, 12), hash);

            Assert.NotEqual(0.CalculateHashCode(null, 12), hash);

            Assert.Equal(0.CalculateHashCode(12).CalculateHashCode(24), hash);
            Assert.NotEqual(0.CalculateHashCode(24).CalculateHashCode(12), hash);
        }

        [Fact]
        public void CalculateHashCodeTest3()
        {
            int initial = 1274;
            List<int> a = [7, 15, 3, 0, 9461];

            Assert.Equal(initial, initial.CalculateHashCode(null));
            Assert.Equal(initial, initial.CalculateHashCode());

            Assert.Equal(initial.CalculateHashCode(a), initial.CalculateHashCode(new int[] { 7, 15, 3, 0, 9461 }));
            Assert.Equal(initial.CalculateHashCode(a), initial.CalculateHashCode(7, 15, 3, 0, 9461));

            List<int> b = [7, 15, 4, 0, 9461];
            Assert.NotEqual(initial.CalculateHashCode(a), initial.CalculateHashCode(b));
            Assert.NotEqual(initial.CalculateHashCode(a), 1273.CalculateHashCode(a));
        }
    }
}
