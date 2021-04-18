using System;
using net.r_eg.Conari.Core;
using Xunit;

namespace ConariTest.Core
{
    public class MICacheTest
    {
        [Fact]
        public void ComparerTest1()
        {
            var cache = new MICache();

            Assert.Empty(cache);

            Type[] types1 = new Type[] { typeof(int), typeof(int) };
            Type[] types2 = new Type[] { typeof(int), typeof(bool) };
            Type[] types3 = new Type[] { typeof(bool), typeof(int) };
            Type[] types4 = new Type[] { typeof(bool), typeof(bool) };

            cache[types1] = null;
            cache[types2] = null;
            cache[types3] = null;
            cache[types4] = null;

            Assert.Equal(4, cache.Count);

            Assert.True(cache.ContainsKey(types1));
            Assert.True(cache.ContainsKey(types2));
            Assert.True(cache.ContainsKey(types3));
            Assert.True(cache.ContainsKey(types4));

            Assert.False(cache.ContainsKey(new[] { typeof(int), typeof(Int64) }));
            Assert.False(cache.ContainsKey(new[] { typeof(void), typeof(int) }));
            Assert.False(cache.ContainsKey(new[] { typeof(int) }));
            Assert.False(cache.ContainsKey(Array.Empty<Type>()));
        }
    }
}
