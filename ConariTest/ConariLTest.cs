using System;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using net.r_eg.Conari;
using net.r_eg.Conari.Core;
using net.r_eg.Conari.Exceptions;

namespace net.r_eg.ConariTest
{
    [TestClass]
    public class ConariLTest
    {
        private const string STUB_LIB_NAME = "__ThisIsNotRealUserLib";

        [TestMethod]
        public void loadTest1()
        {
            try {
                new ConariL((string)null);
                Assert.Fail("1");
            }
            catch(Exception ex) { Assert.IsTrue(ex.GetType() == typeof(ArgumentException), ex.GetType().ToString()); }

            try {
                new ConariL((IConfig)null);
                Assert.Fail("2");
            }
            catch(Exception ex) { Assert.IsTrue(ex.GetType() == typeof(ArgumentException), ex.GetType().ToString()); }

            try {
                new ConariL(STUB_LIB_NAME);
                Assert.Fail("3");
            }
            catch(Exception ex) { Assert.IsTrue(ex.GetType() == typeof(LoadLibException), ex.GetType().ToString()); }
            
        }

        [TestMethod]
        //[ExpectedException(typeof(ArgumentException))]
        public void loadTest2()
        {
            new ConariL(new Config("") { LazyLoading = true });

            try {
                new ConariL("");
                Assert.Fail("2");
            }
            catch(Exception ex) { Assert.IsTrue(ex.GetType() == typeof(ArgumentException), ex.GetType().ToString()); }
        }

        [TestMethod]
        public void funcNameTest1()
        {
            var l = new ConariL(new Config("") { LazyLoading = true });

            try {
                l.funcName("");
                Assert.Fail("1");
            }
            catch(Exception ex) { Assert.IsTrue(ex.GetType() == typeof(ArgumentException), ex.GetType().ToString()); }

            try {
                l.funcName(null);
                Assert.Fail("2");
            }
            catch(Exception ex) { Assert.IsTrue(ex.GetType() == typeof(ArgumentException), ex.GetType().ToString()); }
        }

        [TestMethod]
        public void funcNameTest2()
        {
            using(var l = new ConariL(
                                 new Config("") {
                                     LazyLoading = true
                                 }))
            {
                string func = "name1";

                l.Prefix = "test_";
                Assert.AreEqual($"test_{func}", l.funcName(func));

                l.Prefix = "new_";
                Assert.AreEqual($"new_{func}", l.funcName(func));
            }
        }

        [TestMethod]
        public void funcNameTest3()
        {
            using(var l = new ConariL(
                                 new Config("") {
                                     LazyLoading = true
                                 }, "test3_"))
            {
                string func = "name2";
                Assert.AreEqual($"test3_{func}", l.funcName(func));

                l.Prefix = null;
                Assert.AreEqual(func, l.funcName(func));
            }
        }

        [TestMethod]
        public void callingConvTest1()
        {
            using(var l = new ConariL(
                                 new Config("") {
                                     LazyLoading = true
                                 }))
            {
                Assert.AreEqual(CallingConvention.Cdecl, l.Convention);

                l.Convention = CallingConvention.StdCall;
                Assert.AreEqual(CallingConvention.StdCall, l.Convention);
            }
        }

        [TestMethod]
        public void callingConvTest2()
        {
            using(IConari l = new ConariL(
                                 new Config("") {
                                     LazyLoading = true
                                 }, CallingConvention.Cdecl))
            {
                Assert.AreEqual(CallingConvention.Cdecl, l.Convention);
            }
        }
    }
}
