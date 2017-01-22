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
        public void aliasTest4()
        {
            using(var l = new ConariL(UNLIB_DLL))
            {
                UInt32 expected = 0x00001CE8;

                l.Aliases["addr"]
                    = l.Aliases["_"]
                    = "ADDR_SPEC";

                Assert.AreEqual(expected, (UInt32)l.ExVar.DLR.addr, "1");
                Assert.AreEqual(expected, l.ExVar.DLR._<UInt32>(), "2");
                Assert.AreEqual(expected, (UInt32)l.ExVar.get("addr"), "3");
                Assert.AreEqual(expected, l.ExVar.get<UInt32>("addr"), "4");
                Assert.AreEqual(expected, (UInt32)l.ExVar.getVar("_"), "5");
                Assert.AreEqual(expected, l.ExVar.getVar<UInt32>("_"), "6");

                Assert.AreEqual(expected, l.ExVar.getField<UInt32>("addr").value, "7");
                Assert.AreEqual(expected, l.ExVar.getField(typeof(UInt32), "_").value, "8");
            }
        }

        [TestMethod]
        public void aliasPrefixTest1()
        {
            using(var l = new ConariL(UNLIB_DLL, "apiprefix_"))
            {
                bool expected = false;

                l.Aliases["GF"] = l.Aliases["apiprefix_GF"] = "GFlag";

                Assert.AreEqual(expected, l.ExVar.DLR.GF<bool>(), "1");
                Assert.AreEqual(expected, l.ExVar.get<bool>("GF"), "2");
                Assert.AreEqual(expected, l.ExVar.getVar<bool>("apiprefix_GF"), "3");

                Assert.AreEqual(expected, l.ExVar.getField<bool>("apiprefix_GF").value, "4");
                Assert.AreEqual(expected, l.ExVar.getField(typeof(bool), "apiprefix_GF").value, "5");
            }
        }

        [TestMethod]
        public void aliasPrefixTest2()
        {
            using(var l = new ConariL(UNLIB_DLL, "apiprefix_"))
            {
                bool expected = false;

                var pa = new ProcAlias("apiprefix_GFlag", new AliasCfg() { NoPrefixR = true });
                l.Aliases["GF"] = l.Aliases["apiprefix_GF"] = pa;

                Assert.AreEqual(expected, l.ExVar.DLR.GF<bool>(), "1");
                Assert.AreEqual(expected, l.ExVar.get<bool>("GF"), "2");
                Assert.AreEqual(expected, l.ExVar.getVar<bool>("apiprefix_GF"), "3");

                Assert.AreEqual(expected, l.ExVar.getField<bool>("apiprefix_GF").value, "4");
                Assert.AreEqual(expected, l.ExVar.getField(typeof(bool), "apiprefix_GF").value, "5");
            }
        }

        [TestMethod]
        public void aliasPrefixTest3()
        {
            using(var l = new ConariL(UNLIB_DLL, "apiprefix_"))
            {
                bool expected = false;

                var pa = new ProcAlias("apiprefix_GFlag", new AliasCfg() { NoPrefixR = true });
                l.Aliases["GF"] = l.Aliases["apiprefix_GF"] = pa;
                
                Assert.AreEqual(expected, l.ExVar.get<bool>("apiprefix_GF"), "1");
                Assert.AreEqual(expected, l.ExVar.getVar<bool>("GF"), "2");
                Assert.AreEqual(expected, l.ExVar.getField<bool>("GF").value, "3");
                Assert.AreEqual(expected, l.ExVar.getField(typeof(bool), "GF").value, "4");
            }
        }

        [TestMethod]
        public void aliasPrefixTest4()
        {
            using(var l = new ConariL(UNLIB_DLL))
            {
                l.Prefix = "apiprefix_";

                l.Aliases["HelloWorld"] = new ProcAlias(
                    "?getD_HelloWorld@API@UnLib@Conari@r_eg@net@@YAPBDXZ", 
                    new AliasCfg() { NoPrefixR = true }
                );

                string exp = "Hello World !";

                Assert.AreEqual(exp, l.DLR.HelloWorld<CharPtr>());

                l.Prefix = "";
                Assert.AreEqual(exp, l.DLR.HelloWorld<CharPtr>());
            }
        }

        [TestMethod]
        public void aliasPrefixTest5()
        {
            using(var l = new ConariL(UNLIB_DLL))
            {
                var pa = new ProcAlias("apiprefix_GFlag", new AliasCfg() { NoPrefixR = true });
                l.Aliases["one"] = l.Aliases["two"] = pa;

                bool exp = false;

                l.Prefix = "apiprefix_";
                Assert.AreEqual(exp, l.ExVar.DLR.one<bool>());

                l.Prefix = "";
                Assert.AreEqual(exp, l.ExVar.DLR.one<bool>());
            }
        }

        [TestMethod]
        public void aliasPrefixTest6()
        {
            using(var l = new ConariL(UNLIB_DLL))
            {
                l.Aliases["GMN1"] = new ProcAlias(
                    "GetMagicNum", 
                    new AliasCfg() { NoPrefixR = true }
                );

                l.Aliases["GMN2"] = new ProcAlias(
                    "GetMagicNum",
                    new AliasCfg() { NoPrefixR = false }
                );

                l.Prefix = "apiprefix_";
                Assert.AreEqual(-1, l.DLR.GMN1<int>());
                Assert.AreEqual(4, l.DLR.GMN2<int>());

                l.Prefix = "";
                Assert.AreEqual(-1, l.DLR.GMN1<int>());
                Assert.AreEqual(-1, l.DLR.GMN2<int>());
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
