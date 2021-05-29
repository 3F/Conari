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
    }
}
