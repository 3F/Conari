using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using net.r_eg.Conari;
using net.r_eg.Conari.Core;
using net.r_eg.Conari.Exceptions;
using net.r_eg.Conari.Native;
using net.r_eg.Conari.Native.Core;
using net.r_eg.Conari.Types;

namespace net.r_eg.ConariTest
{
    [TestClass]
    public class BindingTest
    {
        private const string UNLIB_DLL = "UnLib.dll";

        [TestMethod]
        public void basicTest1()
        {
            using(var l = new ConariL(UNLIB_DLL))
            {
                Assert.AreEqual(true, l.DLR.get_True<bool>());
                Assert.AreEqual(true, l.bind<Func<bool>>("get_True")());
                Assert.AreEqual(true, l.bind(Dynamic.GetMethodInfo(typeof(bool)), "get_True")
                                                    .dynamic
                                                    .Invoke(null, new object[0]));
            }
        }

        [TestMethod]
        public void basicTest2()
        {
            using(var l = new ConariL(UNLIB_DLL))
            {
                Assert.AreEqual(7, l.DLR.get_Seven<ushort>());
                Assert.AreEqual(7, l.bind<Func<ushort>>("get_Seven")());
                Assert.AreEqual((ushort)7, l.bind(Dynamic.GetMethodInfo(typeof(ushort)), "get_Seven")
                                                         .dynamic
                                                         .Invoke(null, new object[0]));
            }
        }

        [TestMethod]
        public void basicTest3()
        {
            using(var l = new ConariL(UNLIB_DLL))
            {
                string exp = "Hello World !";
                Assert.AreEqual(exp, l.DLR.get_HelloWorld<CharPtr>());
                Assert.AreEqual(exp, l.bind<Func<CharPtr>>("get_HelloWorld")());

                var dyn = l.bind(Dynamic.GetMethodInfo(typeof(CharPtr)), "get_HelloWorld");
                Assert.AreEqual(exp, (CharPtr)dyn.dynamic.Invoke(null, new object[0]));
            }
        }

        [TestMethod]
        [ExpectedException(typeof(WinFuncFailException))]
        public void basicTest4()
        {
            using(var l = new ConariL(UNLIB_DLL))
            {
                l.DLR.not_real_func_name<bool>();
            }
        }

        [TestMethod]
        [ExpectedException(typeof(WinFuncFailException))]
        public void basicTest5()
        {
            using(var l = new ConariL(UNLIB_DLL))
            {
                l.bind<Func<bool>>("not_real_func_name")();
            }
        }

        [TestMethod]
        [ExpectedException(typeof(WinFuncFailException))]
        public void basicTest6()
        {
            using(var l = new ConariL(UNLIB_DLL))
            {
                l.bind(Dynamic.GetMethodInfo(typeof(bool)), "not_real_func_name")
                    .dynamic
                    .Invoke(null, new object[0]);
            }
        }

        [TestMethod]
        public void basicTest7()
        {
            using(var l = new ConariL(UNLIB_DLL))
            {
                Assert.AreEqual((UserSpecUintType)7, l.DLR.get_Seven<UserSpecUintType>());
                Assert.AreEqual((UserSpecUintType)7, l.bind<Func<UserSpecUintType>>("get_Seven")());

                Assert.AreEqual((UserSpecUintType)7, 
                                l.bind(Dynamic.GetMethodInfo(typeof(UserSpecUintType)), "get_Seven")
                                                .dynamic
                                                .Invoke(null, new object[0]));
            }
        }

        //----

        [TestMethod]
        public void decoratedTest1()
        {
            // bool net::r_eg::Conari::UnLib::API::getD_True(void)
            // ?getD_True@API@UnLib@Conari@r_eg@net@@YA_NXZ

            using(var l = new ConariL(UNLIB_DLL))
            {
                Assert.AreEqual(true, l.bind<Func<bool>>("?getD_True@API@UnLib@Conari@r_eg@net@@YA_NXZ")());
                Assert.AreEqual(true, l.bind(Dynamic.GetMethodInfo(typeof(bool)), "?getD_True@API@UnLib@Conari@r_eg@net@@YA_NXZ")
                                                    .dynamic
                                                    .Invoke(null, new object[0]));
            }
        }

        [TestMethod]
        public void decoratedTest2()
        {
            // unsigned short net::r_eg::Conari::UnLib::API::getD_Seven(void)
            // ?getD_Seven@API@UnLib@Conari@r_eg@net@@YAGXZ
            using(var l = new ConariL(UNLIB_DLL))
            {
                Assert.AreEqual(7, l.bind<Func<ushort>>("?getD_Seven@API@UnLib@Conari@r_eg@net@@YAGXZ")());
                Assert.AreEqual((ushort)7, l.bind(Dynamic.GetMethodInfo(typeof(ushort)), "?getD_Seven@API@UnLib@Conari@r_eg@net@@YAGXZ")
                                                         .dynamic
                                                         .Invoke(null, new object[0]));
            }
        }

        [TestMethod]
        public void decoratedTest3()
        {
            // char const * net::r_eg::Conari::UnLib::API::getD_HelloWorld(void)
            // ?getD_HelloWorld@API@UnLib@Conari@r_eg@net@@YAPBDXZ
            using(var l = new ConariL(UNLIB_DLL))
            {
                string exp = "Hello World !";
                Assert.AreEqual(exp, l.bind<Func<CharPtr>>("?getD_HelloWorld@API@UnLib@Conari@r_eg@net@@YAPBDXZ")());

                var dyn = l.bind(Dynamic.GetMethodInfo(typeof(CharPtr)), "?getD_HelloWorld@API@UnLib@Conari@r_eg@net@@YAPBDXZ");
                Assert.AreEqual(exp, (CharPtr)dyn.dynamic.Invoke(null, new object[0]));
            }
        }

        /// <summary>
        /// get_CharPtrVal
        /// </summary>
        [TestMethod]
        public void echoTest1()
        {
            using(var l = new ConariL(UNLIB_DLL))
            {
                string exp = "my string-123 !";

                using(var uns = new UnmanagedString(exp, UnmanagedString.SType.Ansi))
                {
                    CharPtr chrptr = uns;

                    Assert.AreEqual(exp, l.DLR.get_CharPtrVal<CharPtr>(chrptr));
                    Assert.AreEqual(exp, l.bind<Func<CharPtr, CharPtr>>("get_CharPtrVal")(chrptr));

                    var dyn = l.bind(Dynamic.GetMethodInfo(typeof(CharPtr), typeof(CharPtr)), "get_CharPtrVal");
                    Assert.AreEqual(exp, (CharPtr)dyn.dynamic.Invoke(null, new object[] { chrptr }));
                }
            }
        }

        /// <summary>
        /// get_WCharPtrVal
        /// </summary>
        [TestMethod]
        public void echoTest2()
        {
            using(var l = new ConariL(UNLIB_DLL))
            {
                string exp = "my string-123 !";

                using(var uns = new UnmanagedString(exp, UnmanagedString.SType.Unicode))
                {
                    WCharPtr wchrptr = uns;

                    Assert.AreEqual(exp, l.DLR.get_WCharPtrVal<WCharPtr>(wchrptr));
                    Assert.AreEqual(exp, l.bind<Func<WCharPtr, WCharPtr>>("get_WCharPtrVal")(wchrptr));

                    var dyn = l.bind(Dynamic.GetMethodInfo(typeof(WCharPtr), typeof(WCharPtr)), "get_WCharPtrVal");
                    Assert.AreEqual(exp, (WCharPtr)dyn.dynamic.Invoke(null, new object[] { wchrptr }));
                }
            }
        }

        /// <summary>
        /// get_BSTRVal
        /// </summary>
        [TestMethod]
        public void echoTest3()
        {
            using(var l = new ConariL(UNLIB_DLL))
            {
                string exp = "my string-123 !";

                using(var uns = new UnmanagedString(exp, UnmanagedString.SType.BSTR))
                {
                    BSTR bstr = uns;

                    Assert.AreEqual(exp, l.DLR.get_BSTRVal<BSTR>(bstr));
                    Assert.AreEqual(exp, l.bind<Func<BSTR, BSTR>>("get_BSTRVal")(bstr));

                    var dyn = l.bind(Dynamic.GetMethodInfo(typeof(BSTR), typeof(BSTR)), "get_BSTRVal");
                    Assert.AreEqual(exp, (BSTR)dyn.dynamic.Invoke(null, new object[] { bstr }));
                }
            }
        }

        /// <summary>
        /// get_StringPtrVal
        /// </summary>
        [TestMethod]
        public void echoTest4()
        {
            using(var l = new ConariL(UNLIB_DLL))
            {
                string exp = "my string-123 !";

                using(var uns = new UnmanagedString(exp, UnmanagedString.SType.Ansi))
                {
                    CharPtr chrptr = uns;

                    Assert.AreEqual(exp, l.DLR.get_StringPtrVal<CharPtr>(chrptr));
                    Assert.AreEqual(exp, l.bind<Func<CharPtr, CharPtr>>("get_StringPtrVal")(chrptr));

                    var dyn = l.bind(Dynamic.GetMethodInfo(typeof(CharPtr), typeof(CharPtr)), "get_StringPtrVal");
                    Assert.AreEqual(exp, (CharPtr)dyn.dynamic.Invoke(null, new object[] { chrptr }));
                }
            }
        }

        /// <summary>
        /// get_WStringPtrVal
        /// </summary>
        [TestMethod]
        public void echoTest5()
        {
            using(var l = new ConariL(UNLIB_DLL))
            {
                string exp = "my string-123 !";

                using(var uns = new UnmanagedString(exp, UnmanagedString.SType.Unicode))
                {
                    WCharPtr wchrptr = uns;

                    Assert.AreEqual(exp, l.DLR.get_WStringPtrVal<WCharPtr>(wchrptr));
                    Assert.AreEqual(exp, l.bind<Func<WCharPtr, WCharPtr>>("get_WStringPtrVal")(wchrptr));

                    var dyn = l.bind(Dynamic.GetMethodInfo(typeof(WCharPtr), typeof(WCharPtr)), "get_WStringPtrVal");
                    Assert.AreEqual(exp, (WCharPtr)dyn.dynamic.Invoke(null, new object[] { wchrptr }));
                }
            }
        }

        [TestMethod]
        public void complexTest1()
        {
            using(var l = new ConariL(UNLIB_DLL))
            {
                IntPtr ptr1 = l.DLR.get_TSpec<IntPtr>();
                IntPtr ptr2 = l.bind<Func<IntPtr>>("get_TSpec")();

                var dyn     = l.bind(Dynamic.GetMethodInfo(typeof(IntPtr)), "get_TSpec");
                IntPtr ptr3 = (IntPtr)dyn.dynamic.Invoke(null, new object[0]);

                Assert.AreNotEqual(IntPtr.Zero, ptr1);
                Assert.IsTrue(ptr1 == ptr2 && ptr2 == ptr3);

                /*                
                    struct TSpec
                    {
                        BYTE a;
                        int b;
                        char* name;
                    };

                    s->a    = 2;
                    s->b    = 4;
                    s->name = "Conari";

                 */
                var TSpecPtr = NativeData
                                    ._(ptr1)
                                    .align<IntPtr>(3, "a", "b", "name");

                byte[] bytes    = TSpecPtr.Raw.Values;
                dynamic dlr     = TSpecPtr.Raw.Type;
                var fields      = TSpecPtr.Raw.Type.Fields;

                Assert.AreEqual(3, fields.Count);

                int expA        = 2;
                int expB        = 4;
                string expName  = "Conari";

                Type fxIntPtr = (IntPtr.Size == sizeof(Int64)) ? typeof(Int64) : typeof(Int32);

                // a
                Assert.AreEqual("a", fields[0].name);
                Assert.AreEqual(NativeData.SizeOf(fxIntPtr), fields[0].tsize);
                Assert.AreEqual(fxIntPtr, fields[0].type);
                Assert.AreEqual(expA, fields[0].value);

                // b
                Assert.AreEqual("b", fields[1].name);
                Assert.AreEqual(NativeData.SizeOf(fxIntPtr), fields[1].tsize);
                Assert.AreEqual(fxIntPtr, fields[1].type);
                Assert.AreEqual(expB, fields[1].value);

                // name
                Assert.AreEqual("name", fields[2].name);
                Assert.AreEqual(IntPtr.Size, fields[2].tsize);
                Assert.AreEqual(fxIntPtr, fields[2].type);
                Assert.AreEqual(expName, (CharPtr)fields[2].value);

                // DLR
                Assert.AreEqual(expA, dlr.a);
                Assert.AreEqual(expB, dlr.b);
                Assert.AreEqual(expName, (CharPtr)dlr.name);

                // byte-seq
                var br = new BReader(bytes);
                Assert.AreEqual(expA, br.next(fxIntPtr, NativeData.SizeOf(fxIntPtr)));
                Assert.AreEqual(expB, br.next(fxIntPtr, NativeData.SizeOf(fxIntPtr)));
                Assert.AreEqual(expName, (CharPtr)br.next(fxIntPtr, NativeData.SizeOf(fxIntPtr)));
            }
        }
    }
}
