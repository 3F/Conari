/*!
 * Copyright (c) 2016  Denis Kuzmin <x-3F@outlook.com> github/3F
 * Copyright (c) Conari contributors https://github.com/3F/Conari/graphs/contributors
 * Licensed under the MIT License (MIT).
 * See accompanying LICENSE.txt file or visit https://github.com/3F/Conari
*/

using net.r_eg.Conari.Extension;
using Xunit;

namespace ConariTest.Extensions
{
    public class StringExtensionTest
    {
        [Fact]
        public void RelativeLengthTest1()
        {
            string str = "012345";
            Assert.Equal(3, str.RelativeLength(0.5f));
            Assert.Equal(6, str.RelativeLength(1));
            Assert.Equal(0, str.RelativeLength(0));
            Assert.Equal(1, str.RelativeLength(0.1f));
            Assert.Equal(1, str.RelativeLength(0.01f));
            Assert.Equal(9, str.RelativeLength(1.5f));
            Assert.Equal(10, str.RelativeLength(1.6f));
            Assert.Equal(0, ((string)null).RelativeLength(1.5f));
            Assert.Equal(0, string.Empty.RelativeLength(1.5f));
        }
    }
}
