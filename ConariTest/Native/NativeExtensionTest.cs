using net.r_eg.Conari.Native;
using Xunit;

namespace ConariTest.Native
{
    using static net.r_eg.Conari.Static.Members;

    public class NativeExtensionTest
    {
        [Fact]
        public void NativeSizeTest1()
        {
            Assert.Equal(0, new long[] { }.NativeSize());

            Assert.Equal(0, ((long[])null).NativeSize());

            Assert.Equal(8, new long[] { 1 }.NativeSize());

            Assert.Equal(3, new byte[] { 1, 2, 3 }.NativeSize());

            Assert.Equal(12, new int[] { 1, 2, 3 }.NativeSize());

            Assert.Equal(12, new short[2, 3] { { 1, 2, 3 }, { 4, 5, 6 } }.NativeSize());
        }

        [Fact]
        public void NativeSizeTest2()
        {
            Assert.Equal(8, ((long)0).NativeSize());
            Assert.Equal(4, ((int)0).NativeSize());
            Assert.Equal(2, ((short)0).NativeSize());
            Assert.Equal(1, ((byte)0).NativeSize());
        }

        [Fact]
        public void NativeSizeTest3()
        {
            Assert.Equal(8, SizeOf<long>());
            Assert.Equal(4, SizeOf<int>());
            Assert.Equal(2, SizeOf<short>());
            Assert.Equal(1, SizeOf<byte>());
        }

        [Fact]
        public void NativeSizeTest4()
        {
            Assert.Equal(24, typeof(long).NativeSize(3));
            Assert.Equal(12, typeof(int).NativeSize(3));
            Assert.Equal(6, typeof(short).NativeSize(3));
            Assert.Equal(3, typeof(byte).NativeSize(3));
        }

        [Fact]
        public void NativeSizeTest5()
        {
            Assert.Equal(24, SizeOf<long>(3));
            Assert.Equal(12, SizeOf<int>(3));
            Assert.Equal(6, SizeOf<short>(3));
            Assert.Equal(3, SizeOf<byte>(3));
        }

        [Fact]
        public void NativeSizeTest6()
        {
            Assert.Equal(0, SizeOf<long>(0));
            Assert.Equal(0, typeof(long).NativeSize(0));
        }
    }
}
