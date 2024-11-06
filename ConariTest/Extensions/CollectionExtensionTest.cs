/*!
 * Copyright (c) 2016  Denis Kuzmin <x-3F@outlook.com> github/3F
 * Copyright (c) Conari contributors https://github.com/3F/Conari/graphs/contributors
 * Licensed under the MIT License (MIT).
 * See accompanying LICENSE.txt file or visit https://github.com/3F/Conari
*/

using System;
using System.Collections.Generic;
using System.Linq;
using net.r_eg.Conari.Extension;
using Xunit;

namespace ConariTest.Extensions
{
    public class CollectionExtensionTest
    {
        public static IEnumerable<object[]> ForEachCollection1()
        {
            yield return new object[] { Array.Empty<int>() };

            yield return new object[] { null };

            yield return new object[] { new int[] { 0, 1, 2, 3, 4 } };
        }

        [Theory]
        [MemberData(nameof(ForEachCollection1))]
        public void ForEachTest1(IEnumerable<int> data)
        {
            int len = data?.Count() ?? 0;
            long idx = 0;

            data.ForEach((n, i) => Assert.Equal(idx++, i));
            Assert.Equal(len, idx);

            idx = 0;
            data.ForEach(_ => ++idx);
            Assert.Equal(len, idx);

            int v = 0;
            data.ForEach((n, i) => Assert.Equal(v++, n));

            v = 0;
            data.ForEach(n => Assert.Equal(v++, n));
        }

        [Fact]
        public void ForEachTest2()
        {
            int[] data = { 0, 1 };

            data.ForEach((Action<int>)null);
            data.ForEach((Action<int, long>)null);

            Assert.Equal(2, data.Length);
        }
    }
}
