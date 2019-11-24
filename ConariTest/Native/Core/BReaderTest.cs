using System;
using net.r_eg.Conari.Native.Core;
using Xunit;

namespace ConariTest.Native.Core
{
    public class BReaderTest
    {
        [Fact]
        public void nextValTest1()
        {
            var br = new BReader(new byte[] {
                5, 0, 0, 0, // Int32 = 5
                7, 0, 0, 0, // Int32 = 7
                31          // SByte = 31
            });

            Assert.Equal(5, br.next(typeof(Int32), 4));
            Assert.Equal(7, br.next(typeof(Int32), 4));
            Assert.Equal(31, br.next(typeof(sbyte), 1));
        }

        [Fact]
        public void nextValTest2()
        {
            Assert.Throws<IndexOutOfRangeException>(() =>
            {
                var br = new BReader(new byte[] {
                    5, 0, 0, 0, // Int32 = 5
                    31          // SByte = 31
                });

                br.next(typeof(Int32), 4);
                br.next(typeof(Int32), 4);
            });
        }

        [Fact]
        public void nextValTest3()
        {
            var br = new BReader(new byte[] {
                5, 0, 0, 0, // Int32 = 5
            });

            Assert.Equal(5, br.next(typeof(Int32), 4));
            br.reset();
            Assert.Equal(5, br.next(typeof(Int32), 4));

            Assert.False(br.tryNext(typeof(Int32), 4, out _));
        }

        [Fact]
        public void GetValTest1()
        {
            Assert.Throws<NotSupportedException>(() => 
            {
                int offset = 0;
                byte[] data = new byte[] { 1, 2, 3, 4 };
                BReader.GetValue(typeof(UserSpecUintType), 1, ref offset, ref data);
            });
        }
    }
}
