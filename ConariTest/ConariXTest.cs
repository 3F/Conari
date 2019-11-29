using System;
using System.Runtime.InteropServices;
using net.r_eg.Conari;
using net.r_eg.Conari.Core;
using net.r_eg.Conari.Exceptions;
using Xunit;

namespace ConariTest
{
    public class ConariXTest
    {
        private const string STUB_LIB_NAME = "__ThisIsNotRealUserLib";

        [Fact]
        public void loadTest1()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new ConariX((string)null)
            );

            Assert.Throws<ArgumentNullException>(() =>
                new ConariX((IConfig)null)
            );

            Assert.Throws<LoadLibException>(() =>
                new ConariX(STUB_LIB_NAME)
            );
        }

        [Fact]
        public void loadTest2()
        {
            new ConariX(new Config("") { LazyLoading = true }).Dispose();

            Assert.Throws<ArgumentNullException>(() =>
                new ConariX("")
            );
        }

        [Fact]
        public void funcNameTest1()
        {
            dynamic l = new ConariX(new Config("") { LazyLoading = true });

            Assert.Throws<ArgumentException>(() =>
                l.procName("")
            );

            Assert.Throws<ArgumentException>(() =>
                l.procName(null)
            );

            ((IDisposable)l).Dispose();
        }

        [Fact]
        public void funcNameTest2()
        {
            using(dynamic l = new ConariX(
                                 new Config("") {
                                     LazyLoading = true
                                 }))
            {
                string func = "name1";

                l.Prefix = "test_";
                Assert.Equal($"test_{func}", l.procName(func));

                l.Prefix = "new_";
                Assert.Equal($"new_{func}", l.procName(func));
            }
        }

        [Fact]
        public void funcNameTest3()
        {
            using(dynamic l = new ConariX(
                                 new Config("") {
                                     LazyLoading = true
                                 }, "test3_"))
            {
                string func = "name2";
                Assert.Equal($"test3_{func}", l.procName(func));

                l.Prefix = null;
                Assert.Equal(func, l.procName(func));
            }
        }

        [Fact]
        public void callingConvTest1()
        {
            using(dynamic l = new ConariX(
                                 new Config("") {
                                     LazyLoading = true
                                 }))
            {
                Assert.Equal(CallingConvention.Cdecl, l.Convention);

                l.Convention = CallingConvention.StdCall;
                Assert.Equal(CallingConvention.StdCall, l.Convention);
            }
        }

        [Fact]
        public void callingConvTest2()
        {
            using(IConari l = new ConariX(
                                 new Config("") {
                                     LazyLoading = true
                                 }, CallingConvention.Cdecl))
            {
                Assert.Equal(CallingConvention.Cdecl, l.Convention);
            }
        }
    }
}
