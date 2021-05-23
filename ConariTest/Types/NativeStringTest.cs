using System;
using net.r_eg.Conari;
using net.r_eg.Conari.Types;
using Xunit;

namespace ConariTest.Types
{
    using static _svc.TestHelper;

    public class NativeStringTest
    {
        [Fact]
        public void allocTest1()
        {
            using dynamic l = new ConariX(RXW_X);

            using BufferedString<CharPtr> data  = new("Hello {p}!");
            using BufferedString<CharPtr> data2 = new(data.ToString());

            Assert.True(data.Owner);

            using NativeString<CharPtr> filter = new("{p}");
            using NativeString<CharPtr> replacement = new("world");

            Assert.True(l.replace<bool>((IntPtr)data, (IntPtr)filter, (IntPtr)replacement));
            Assert.Equal("Hello world!", (CharPtr)data);

            ((ConariX)l).Cache = false; // TODO:

            Assert.True(l.replace<bool>(data2, filter, (CharPtr)replacement));
            Assert.Equal(data2, data);
        }

        [Fact]
        public void allocTest2()
        {
            using dynamic l = new ConariX(RXW_X);
            using var data = new NativeString<CharPtr>(6);

            Assert.True(data.Owner);

            using NativeString<CharPtr> filter = new("*");
            using NativeString<CharPtr> replacement = new("Hello!");

            Assert.True(l.replace<bool>((IntPtr)data, (IntPtr)filter, (IntPtr)replacement));
            Assert.Equal("Hello!", (CharPtr)data);
        }

        [Fact]
        public void updateTest1()
        {
            using var data = new NativeString<CharPtr>("Hello world!", 10);
            using var data2 = new NativeString<CharPtr>(data);

            Assert.True(data.Owner);
            Assert.False(data2.Owner);

            Assert.Equal("Hello!", (CharPtr)data2.update("Hello!"));
            Assert.Equal("Hello word!", (CharPtr)data2.update("Hello word!"));

            Assert.Throws<ArgumentOutOfRangeException>(() => data2.update("Hello world!!")); // since we don't actually check the end of region.
        }

        [Fact]
        public void updateTest2()
        {
            using var data = new NativeString<CharPtr>("Hello!", 6);
            Assert.Equal("Hello world!", (CharPtr)data.update("Hello world!"));
            Assert.Equal("Hello word!", (CharPtr)data.update("Hello word!"));

            Assert.Throws<ArgumentOutOfRangeException>(() => data.update("Hello world!!"));
        }

        [Fact]
        public void updateTest3()
        {
            using var data = new NativeString<CharPtr>("Hello world!");
            using var dataE = new NativeString<CharPtr>(data);

            Assert.True(data.Owner);
            Assert.False(dataE.Owner);

            dataE.update("Hello!");
            Assert.Equal("Hello!", (CharPtr)data);

            dataE.update("Hello word!");
            Assert.Equal("Hello word!", (CharPtr)data);
        }

        [Fact]
        public void copyTest1()
        {
            using var data = new NativeString<CharPtr>("Hello!");
            using var dataE = new NativeString<CharPtr>(data, 6);

            Assert.Equal("Hello!", (CharPtr)dataE);
            dataE.update("Hello world!");
            Assert.Equal("Hello world!", (CharPtr)dataE);
            Assert.Equal("Hello!", (CharPtr)data);
        }

        [Fact]
        public void copyTest2()
        {
            using var data = new NativeString<CharPtr>("Hello");
            using var data2 = data + " " + "world";

            Assert.True("Hello world" == data2);

            using NativeString<CharPtr> msg = data2.add(", and you!");

            Assert.True(msg == "Hello world, and you!");
            Assert.NotEqual(data, msg);

            using NativeString<CharPtr> msg2 = msg + string.Empty;
            Assert.Equal(msg2, msg);
            Assert.True(msg2 == msg);
            Assert.False(msg2 != msg);

            using NativeString<CharPtr> msg3 = msg + " ";
            Assert.NotEqual(msg3, msg);
            Assert.False(msg3 == msg);
            Assert.True(msg3 != msg);
        }

        [Fact]
        public void ctorTest1()
        {
            Assert.Throws<NotSupportedException>(() => new NativeString<WCharPtr>(null));
            Assert.Throws<ArgumentOutOfRangeException>(() => new NativeString<WCharPtr>(IntPtr.Zero));

            Assert.Throws<NotSupportedException>(() => new NativeString<CharPtr>(null));
            Assert.Throws<ArgumentOutOfRangeException>(() => new NativeString<CharPtr>(IntPtr.Zero));

            Assert.Throws<NotSupportedException>(() => new NativeString<TCharPtr>(null));
            Assert.Throws<ArgumentOutOfRangeException>(() => new NativeString<TCharPtr>(IntPtr.Zero));
        }

        [Fact]
        public void gtypeTest1()
        {
            Assert.Throws<NotImplementedException>(() => {
                using var data = new NativeString<size_t>("");
            });
        }
    }
}
