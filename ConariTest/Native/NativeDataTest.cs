using net.r_eg.Conari.Native;
using Xunit;

namespace ConariTest.Native
{
    public class NativeDataTest
    {
        [Fact]
        public void localTest1()
        {
            var exp = new byte[] { 1, 2 };
            var raw = new NativeData(exp).Raw;

            Assert.Equal(2, raw.Values.Length);
            Assert.Equal(exp[0], raw.Values[0]);
            Assert.Equal(exp[1], raw.Values[1]);
        }

        [Fact]
        public void localTest2()
        {
            var exp = new byte[] { 8 };
            var raw = NativeData._(exp).Raw;

            Assert.Single(raw.Values);
            Assert.Equal(exp[0], raw.Values[0]);
        }
    }
}
