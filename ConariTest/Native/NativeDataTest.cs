using Microsoft.VisualStudio.TestTools.UnitTesting;
using net.r_eg.Conari.Native;

namespace net.r_eg.ConariTest.Native
{
    [TestClass]
    public class NativeDataTest
    {
        [TestMethod]
        public void localTest1()
        {
            var exp = new byte[] { 1, 2 };
            var raw = new NativeData(exp).Raw;

            Assert.AreEqual(2, raw.Values.Length);
            Assert.AreEqual(exp[0], raw.Values[0]);
            Assert.AreEqual(exp[1], raw.Values[1]);
        }

        [TestMethod]
        public void localTest2()
        {
            var exp = new byte[] { 8 };
            var raw = NativeData._(exp).Raw;

            Assert.AreEqual(1, raw.Values.Length);
            Assert.AreEqual(exp[0], raw.Values[0]);
        }
    }
}
