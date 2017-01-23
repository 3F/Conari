using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using net.r_eg.Conari.Core;

namespace net.r_eg.ConariTest.Core
{
    [TestClass]
    public class MICacheTest
    {
        [TestMethod]
        public void ComparerTest1()
        {
            var cache = new MICache();

            Assert.AreEqual(0, cache.Count);

            Type[] types1 = new Type[] { typeof(int), typeof(int) };
            Type[] types2 = new Type[] { typeof(int), typeof(bool) };
            Type[] types3 = new Type[] { typeof(bool), typeof(int) };
            Type[] types4 = new Type[] { typeof(bool), typeof(bool) };

            cache[types1] = null;
            cache[types2] = null;
            cache[types3] = null;
            cache[types4] = null;

            Assert.AreEqual(4, cache.Count);

            Assert.AreEqual(true, cache.ContainsKey(types1));
            Assert.AreEqual(true, cache.ContainsKey(types2));
            Assert.AreEqual(true, cache.ContainsKey(types3));
            Assert.AreEqual(true, cache.ContainsKey(types4));
            Assert.AreEqual(false, cache.ContainsKey(new[] { typeof(int), typeof(Int64) }));
            Assert.AreEqual(false, cache.ContainsKey(new[] { typeof(void), typeof(int) }));
            Assert.AreEqual(false, cache.ContainsKey(new[] { typeof(int) }));
            Assert.AreEqual(false, cache.ContainsKey(new Type[0]));
        }
    }
}
