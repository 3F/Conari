using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using net.r_eg.Conari;
using net.r_eg.Conari.Aliases;
using net.r_eg.Conari.Core;
using net.r_eg.Conari.Types;

namespace net.r_eg.ConariTest
{
    [TestClass]
    public class AliasesTest
    {
        private const string UNLIB_DLL = "UnLib.dll";

        [TestMethod]
        public void aliasTest1()
        {
            // bool net::r_eg::Conari::UnLib::API::getD_True(void)
            // ?getD_True@API@UnLib@Conari@r_eg@net@@YA_NXZ

            using(var l = new ConariL(UNLIB_DLL))
            {
                l.Aliases["getD_True"] = new ProcAlias("?getD_True@API@UnLib@Conari@r_eg@net@@YA_NXZ");

                Assert.AreEqual(true, l.DLR.getD_True<bool>());
                Assert.AreEqual(true, l.bind<Func<bool>>("getD_True")());
                Assert.AreEqual(true, l.bind(Dynamic.GetMethodInfo(typeof(bool)), "getD_True")
                                        .dynamic
                                        .Invoke(null, new object[0]));
            }
        }

        [TestMethod]
        public void aliasTest2()
        {
            // unsigned short net::r_eg::Conari::UnLib::API::getD_Seven(void)
            // ?getD_Seven@API@UnLib@Conari@r_eg@net@@YAGXZ

            using(var l = new ConariL(UNLIB_DLL))
            {
                l.Aliases["getD_Seven"] = new ProcAlias("?getD_Seven@API@UnLib@Conari@r_eg@net@@YAGXZ");

                Assert.AreEqual(7, l.DLR.getD_Seven<ushort>());
                Assert.AreEqual(7, l.bind<Func<ushort>>("getD_Seven")());
                Assert.AreEqual((ushort)7, l.bind(Dynamic.GetMethodInfo(typeof(ushort)), "getD_Seven")
                                                .dynamic
                                                .Invoke(null, new object[0]));
            }
        }

        [TestMethod]
        public void aliasTest3()
        {
            // char const * net::r_eg::Conari::UnLib::API::getD_HelloWorld(void)
            // ?getD_HelloWorld@API@UnLib@Conari@r_eg@net@@YAPBDXZ

            using(var l = new ConariL(UNLIB_DLL))
            {
                string exp = "Hello World !";
                l.Aliases["getD_HelloWorld"] = new ProcAlias("?getD_HelloWorld@API@UnLib@Conari@r_eg@net@@YAPBDXZ");

                Assert.AreEqual(exp, l.DLR.getD_HelloWorld<CharPtr>());
                Assert.AreEqual(exp, l.bind<Func<CharPtr>>("getD_HelloWorld")());

                var dyn = l.bind(Dynamic.GetMethodInfo(typeof(CharPtr)), "getD_HelloWorld");
                Assert.AreEqual(exp, (CharPtr)dyn.dynamic.Invoke(null, new object[0]));
            }
        }

        [TestMethod]
        public void multiAliasTest1()
        {
            using(var l = new ConariL(UNLIB_DLL))
            {
                l.Aliases["getD_True"]  = "?getD_True@API@UnLib@Conari@r_eg@net@@YA_NXZ";
                l.Aliases["getFlag"]    = l.Aliases["getD_True"];

                Assert.AreEqual(true, l.DLR.getD_True<bool>());
                Assert.AreEqual(true, l.DLR.getFlag<bool>());
                Assert.AreEqual(true, l.bind<Func<bool>>("getFlag")());
                Assert.AreEqual(true, l.bind(Dynamic.GetMethodInfo(typeof(bool)), "getFlag")
                                        .dynamic
                                        .Invoke(null, new object[0]));
            }
        }

        [TestMethod]
        public void multiAliasTest2()
        {
            using(var l = new ConariL(UNLIB_DLL))
            {
                l.Aliases["d"]  = "?getD_True@API@UnLib@Conari@r_eg@net@@YA_NXZ";
                l.Aliases["a"] = l.Aliases["b"] = l.Aliases["c"] = l.Aliases["d"];

                Assert.AreEqual(true, l.DLR.a<bool>());
                Assert.AreEqual(true, l.DLR.b<bool>());
                Assert.AreEqual(true, l.DLR.c<bool>());
                Assert.AreEqual(true, l.DLR.d<bool>());
            }
        }
    }
}
