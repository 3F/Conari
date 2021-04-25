using net.r_eg.Conari.Extension;
using Xunit;

namespace ConariTest.Extensions
{
    public class StringExtensionTest
    {
        [Fact]
        public void PercentageLengthTest1()
        {
            string str = "012345";
            Assert.Equal(3, str.PercentageLength(0.5f));
            Assert.Equal(6, str.PercentageLength(1));
            Assert.Equal(0, str.PercentageLength(0));
            Assert.Equal(1, str.PercentageLength(0.1f));
            Assert.Equal(1, str.PercentageLength(0.01f));
            Assert.Equal(9, str.PercentageLength(1.5f));
            Assert.Equal(10, str.PercentageLength(1.6f));
            Assert.Equal(0, ((string)null).PercentageLength(1.5f));
            Assert.Equal(0, string.Empty.PercentageLength(1.5f));
        }
    }
}
