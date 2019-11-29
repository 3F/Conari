using System;
using net.r_eg.Conari;
using net.r_eg.Conari.Core;
using net.r_eg.Conari.Exceptions;
using net.r_eg.Conari.Native;
using net.r_eg.Conari.Native.Core;
using net.r_eg.Conari.Types;
using Xunit;

namespace ConariTest
{
    /// <summary>
    /// ConariX related tests
    /// </summary>
    public class XBindingTest
    {
        private const string UNLIB_DLL = @"..\UnLib.dll";

        private readonly IConfig gCfgUnlib = new Config(UNLIB_DLL, true);

        [Fact]
        public void basicTest1()
        {
            using(dynamic l = new ConariX(gCfgUnlib))
            {
                Assert.Equal(true, l.get_True<bool>());
                Assert.True(l.bind<Func<bool>>("get_True")());
                Assert.Equal(true, l.bind(Dynamic.GetMethodInfo(typeof(bool)), "get_True")
                                                    .dynamic
                                                    .Invoke(null, Array.Empty<object>()));
            }
        }

        [Fact]
        public void basicTest2()
        {
            using(dynamic l = new ConariX(gCfgUnlib))
            {
                Assert.Equal(7, l.get_Seven<ushort>());
                Assert.Equal(7, l.bind<Func<ushort>>("get_Seven")());
                Assert.Equal((ushort)7, l.bind(Dynamic.GetMethodInfo(typeof(ushort)), "get_Seven")
                                                         .dynamic
                                                         .Invoke(null, Array.Empty<object>()));
            }
        }

        [Fact]
        public void basicTest3()
        {
            using(dynamic l = new ConariX(gCfgUnlib))
            {
                string exp = "Hello World !";
                Assert.Equal(exp, l.get_HelloWorld<CharPtr>());
                Assert.Equal(exp, l.bind<Func<CharPtr>>("get_HelloWorld")());

                var dyn = l.bind(Dynamic.GetMethodInfo(typeof(CharPtr)), "get_HelloWorld");
                Assert.Equal(exp, (CharPtr)dyn.dynamic.Invoke(null, Array.Empty<object>()));
            }
        }

        [Fact]
        public void basicTest4()
        {
            Assert.Throws<WinFuncFailException>(() =>
            {
                using(dynamic l = new ConariX(gCfgUnlib))
                {
                    l.Mangling = false;
                    l.not_real_func_name<bool>();
                }
            });
        }

        [Fact]
        public void basicTest5()
        {
            Assert.Throws<WinFuncFailException>(() =>
            {
                using(dynamic l = new ConariX(gCfgUnlib))
                {
                    l.Mangling = false;
                    l.bind<Func<bool>>("not_real_func_name")();
                }
            });
        }

        [Fact]
        public void basicTest6()
        {
            Assert.Throws<WinFuncFailException>(() =>
            {
                using(dynamic l = new ConariX(gCfgUnlib))
                {
                    l.Mangling = false;
                    l.bind(Dynamic.GetMethodInfo(typeof(bool)), "not_real_func_name")
                        .dynamic
                        .Invoke(null, Array.Empty<object>());
                }
            });
        }

        [Fact]
        public void basicTest7()
        {
            using(dynamic l = new ConariX(gCfgUnlib))
            {
                Assert.Equal((UserSpecUintType)7, l.get_Seven<UserSpecUintType>());
                Assert.Equal((UserSpecUintType)7, l.bind<Func<UserSpecUintType>>("get_Seven")());

                Assert.Equal((UserSpecUintType)7, 
                                l.bind(Dynamic.GetMethodInfo(typeof(UserSpecUintType)), "get_Seven")
                                                .dynamic
                                                .Invoke(null, Array.Empty<object>()));
            }
        }

        [Fact]
        public void basicTest8()
        {
            Assert.Throws<EntryPointNotFoundException>(() =>
            {
                using(dynamic l = new ConariX(gCfgUnlib))
                {
                    l.Mangling = true;
                    l.not_real_func_name<bool>();
                }
            });
        }

        [Fact]
        public void basicTest9()
        {
            Assert.Throws<EntryPointNotFoundException>(() =>
            {
                using(dynamic l = new ConariX(gCfgUnlib))
                {
                    l.Mangling = true;
                    l.bind<Func<bool>>("not_real_func_name")();
                }
            });
        }

        [Fact]
        public void basicTest10()
        {
            Assert.Throws<EntryPointNotFoundException>(() =>
            {
                using(dynamic l = new ConariX(gCfgUnlib))
                {
                    l.Mangling = true;
                    l.bind(Dynamic.GetMethodInfo(typeof(bool)), "not_real_func_name")
                        .dynamic
                        .Invoke(null, Array.Empty<object>());
                }
            });
        }

        [Fact]
        public void basicTest11()
        {
            using(dynamic l = new ConariX(gCfgUnlib))
            {
                Assert.Equal(false, l.get_False<bool>());
                Assert.False(l.bind<Func<bool>>("get_False")());
                Assert.Equal(false, l.bind(Dynamic.GetMethodInfo(typeof(bool)), "get_False")
                                                    .dynamic
                                                    .Invoke(null, Array.Empty<object>()));
            }
        }

        [Fact]
        public void basicTest12()
        {
            using(dynamic l = new ConariX(UNLIB_DLL, true))
            {
                Assert.Equal(7, l.bindFunc<int>("get_VarSeven", typeof(int))());
                Assert.Null(l.bind("set_VarSeven", typeof(void), typeof(int))(5));
                Assert.Equal(5, l.bind<int>("get_VarSeven", typeof(int))());
                Assert.Null(l.bind("reset_VarSeven", null)());
                Assert.Equal(-1, (int)l.bind("get_VarSeven", typeof(int))());
            }
        }

        [Fact]
        public void basicTest13()
        {
            using(dynamic l = new ConariX(gCfgUnlib))
            {
                Assert.Equal(7, l.bind<Func<int>>("get_VarSeven")());

                l.bind<Action<int>>("set_VarSeven")(5);
                Assert.Equal(5, l.bind<int>("get_VarSeven", typeof(int))());

                l.bind("reset_VarSeven")();
                Assert.Equal(-1, l.bind<Func<int>>("get_VarSeven")());
            }
        }

        [Fact]
        public void basicTest14()
        {
            using(dynamic l = new ConariX(gCfgUnlib))
            {
                Assert.Equal(7, l.get_VarSeven<int>());
                Assert.Equal(null, l.set_VarSeven(5));
                Assert.Equal(5, l.get_VarSeven<int>());
                Assert.Equal(null ,l.reset_VarSeven());
                Assert.Equal(-1, l.get_VarSeven<int>());
            }
        }

        [Fact]
        public void basicTest15()
        {
            using(dynamic l = new ConariX(gCfgUnlib))
            {
                Assert.Equal(7, l.bind(Dynamic.GetMethodInfo(typeof(int)), "get_VarSeven")
                                        .dynamic
                                        .Invoke(null, null));

                Assert.Null(l.bind(Dynamic.GetMethodInfo(typeof(void), typeof(int)), "set_VarSeven")
                                        .dynamic
                                        .Invoke(null, new object[] { 5 }));

                Assert.Equal(5, l.bind(Dynamic.GetMethodInfo(typeof(int)), "get_VarSeven")
                                        .dynamic
                                        .Invoke(null, Array.Empty<object>()));

                Assert.Null(l.bind(Dynamic.GetMethodInfo(null), "reset_VarSeven")
                                        .dynamic
                                        .Invoke(null, null));

                Assert.Equal(-1, l.bind(Dynamic.GetMethodInfo(typeof(int)), "get_VarSeven")
                                        .dynamic
                                        .Invoke(null, null));
            }
        }

        [Fact]
        public void cacheTest1()
        {
            using(dynamic l = new ConariX(gCfgUnlib))
            {
                Assert.Equal(7, l.get_VarSeven<int>());
                Assert.Equal(null, l.set_VarSeven(1235));
                Assert.Equal(1235, l.get_VarSeven<int>());
                Assert.Equal(null, l.set_VarSeven(-44));
                Assert.Equal(-44, l.get_VarSeven<int>());
            }
        }

        [Fact]
        public void cacheTest2()
        {
            using(dynamic l = new ConariX(gCfgUnlib))
            {
                Assert.Equal(7, l.bindFunc<int>("get_VarSeven", typeof(int))());
                Assert.Null(l.bind("set_VarSeven", typeof(void), typeof(int))(1024));
                Assert.Equal(1024, l.bind<int>("get_VarSeven", typeof(int))());
                Assert.Null(l.bind("set_VarSeven", typeof(void), typeof(int))(-4096));
                Assert.Equal(-4096, l.bind<int>("get_VarSeven", typeof(int))());
            }
        }

        [Fact]
        public void cacheTest3()
        {
            using(dynamic l = new ConariX(gCfgUnlib))
            {
                Assert.Equal(7, l.bindFunc<Func<int>>("get_VarSeven")());

                l.bind<Action<int>>("set_VarSeven")(1024);
                Assert.Equal(1024, l.bind<Func<int>>("get_VarSeven")());
                
                l.bind<Action<int>>("set_VarSeven")(-4096);
                Assert.Equal(-4096, l.bind<Func<int>>("get_VarSeven")());
            }
        }

        [Fact]
        public void cacheTest4()
        {
            /*
             *  MethodInfo m = typeof(T).GetMethod("Invoke"); - local
                TDyn type = ...from cache
                type.dynamic.CreateDelegate(...)
                   - type.declaringType - failed from another cached TDyn
                   - m.DeclaringType - should be ok

                see `T getDelegate<T>(IntPtr ptr, CallingConvention conv) where T : class`
            */

            using(dynamic l = new ConariX(gCfgUnlib))
            {
                Assert.Equal(7, l.get_Seven<ushort>());
                Assert.Equal(7, l.bind<Func<ushort>>("get_Seven")());

                Assert.Equal(7, l.get_Seven<ushort>());
                Assert.Equal(7, l.bind<Func<ushort>>("get_Seven")());
            }

            using(dynamic l = new ConariX(gCfgUnlib))
            {
                Assert.Equal(7, l.bind<Func<ushort>>("get_Seven")());
                Assert.Equal(7, l.get_Seven<ushort>());

                Assert.Equal(7, l.bind<Func<ushort>>("get_Seven")());
                Assert.Equal(7, l.get_Seven<ushort>());
            }
        }

        [Fact]
        public void namingTest1()
        {
            using(dynamic l = new ConariX(UNLIB_DLL, true, "apiprefix_"))
            {
                Assert.Equal(4, l.GetMagicNum<int>());

                Assert.Equal(4, l.bind<Func<int>>("GetMagicNum")());
                Assert.Equal(-1, l.bindFunc<Func<int>>("GetMagicNum")());

                Assert.Equal(-1, l.bind(Dynamic.GetMethodInfo(typeof(int)), "GetMagicNum").dynamic.Invoke(null, null));

                Assert.Equal(-1, l.bindFunc<int>("GetMagicNum", typeof(int))());
                Assert.Equal(4, l.bind<int>("GetMagicNum", typeof(int))());
            }
        }

        [Fact]
        public void manglingTest1()
        {
            // bool net::r_eg::Conari::UnLib::API::getD_True(void)
            // ?getD_True@API@UnLib@Conari@r_eg@net@@YA_NXZ

            using(dynamic l = new ConariX(gCfgUnlib))
            {
                Assert.True(l.bind<Func<bool>>("?getD_True@API@UnLib@Conari@r_eg@net@@YA_NXZ")());
                Assert.Equal(true, l.bind(Dynamic.GetMethodInfo(typeof(bool)), "?getD_True@API@UnLib@Conari@r_eg@net@@YA_NXZ")
                                                    .dynamic
                                                    .Invoke(null, Array.Empty<object>()));
            }
        }

        [Fact]
        public void manglingTest2()
        {
            // unsigned short net::r_eg::Conari::UnLib::API::getD_Seven(void)
            // ?getD_Seven@API@UnLib@Conari@r_eg@net@@YAGXZ
            using(dynamic l = new ConariX(gCfgUnlib))
            {
                Assert.Equal(7, l.bind<Func<ushort>>("?getD_Seven@API@UnLib@Conari@r_eg@net@@YAGXZ")());
                Assert.Equal((ushort)7, l.bind(Dynamic.GetMethodInfo(typeof(ushort)), "?getD_Seven@API@UnLib@Conari@r_eg@net@@YAGXZ")
                                                         .dynamic
                                                         .Invoke(null, Array.Empty<object>()));
            }
        }

        [Fact]
        public void manglingTest3()
        {
            // char const * net::r_eg::Conari::UnLib::API::getD_HelloWorld(void)
            // x86: ?getD_HelloWorld@API@UnLib@Conari@r_eg@net@@YAPBDXZ
            // x64: ?getD_HelloWorld@API@UnLib@Conari@r_eg@net@@YAPEBDXZ
            using(dynamic l = new ConariX(gCfgUnlib))
            {
                string xfun;
                if(IntPtr.Size == sizeof(Int64)) {
                    xfun = "?getD_HelloWorld@API@UnLib@Conari@r_eg@net@@YAPEBDXZ";
                }
                else {
                    xfun = "?getD_HelloWorld@API@UnLib@Conari@r_eg@net@@YAPBDXZ";
                }

                string exp = "Hello World !";
                Assert.Equal(exp, l.bind<Func<CharPtr>>(xfun)());

                var dyn = l.bind(Dynamic.GetMethodInfo(typeof(CharPtr)), xfun);
                Assert.Equal(exp, (CharPtr)dyn.dynamic.Invoke(null, Array.Empty<object>()));
            }
        }

        /// <summary>
        /// unsigned short int __stdcall get_SevenStdCall()
        /// </summary>
        [Fact]
        public void manglingTest4()
        {
            using(dynamic l = new ConariX(gCfgUnlib))
            {
                l.Mangling = true;

                Assert.Equal(7, l.get_SevenStdCall<ushort>());
                Assert.Equal(7, l.bind<Func<ushort>>("get_SevenStdCall")());
                Assert.Equal((ushort)7, l.bind(Dynamic.GetMethodInfo(typeof(ushort)), "get_SevenStdCall")
                                                         .dynamic
                                                         .Invoke(null, Array.Empty<object>()));
            }
        }

        /// <summary>
        /// unsigned short int __fastcall get_SevenFastCall();
        /// </summary>
        [Fact]
        public void manglingTest5()
        {
            using(dynamic l = new ConariX(gCfgUnlib))
            {
                l.Mangling = true;

                Assert.Equal(7, l.get_SevenFastCall<ushort>());
                Assert.Equal(7, l.bind<Func<ushort>>("get_SevenFastCall")());
                Assert.Equal((ushort)7, l.bind(Dynamic.GetMethodInfo(typeof(ushort)), "get_SevenFastCall")
                                                         .dynamic
                                                         .Invoke(null, Array.Empty<object>()));
            }
        }

        /// <summary>
        /// unsigned short int __vectorcall get_SevenVectorCall();
        /// </summary>
        [Fact]
        public void manglingTest6()
        {
            using(dynamic l = new ConariX(gCfgUnlib))
            {
                l.Mangling = true;

                Assert.Equal(7, l.get_SevenVectorCall<ushort>());
                Assert.Equal(7, l.bind<Func<ushort>>("get_SevenVectorCall")());
                Assert.Equal((ushort)7, l.bind(Dynamic.GetMethodInfo(typeof(ushort)), "get_SevenVectorCall")
                                                         .dynamic
                                                         .Invoke(null, Array.Empty<object>()));
            }
        }

        /// <summary>
        /// unsigned short int __stdcall get_SevenStdCall()
        /// x86: _get_SevenStdCall@0
        /// x64: get_SevenStdCall
        /// </summary>
        [Fact]
        public void manglingTest7()
        {
            using dynamic l = new ConariX(gCfgUnlib)
            {
                Mangling = false
            };

            if(IntPtr.Size == sizeof(Int64))
            {
                Assert.Equal((ushort)7, l.get_SevenStdCall<ushort>());
                return;
            }

            Assert.Throws<WinFuncFailException>(() =>
            {
                l.get_SevenStdCall<ushort>();
            });
        }

        /// <summary>
        /// unsigned short int __stdcall get_SevenStdCall()
        /// x86: _get_SevenStdCall@0
        /// x64: get_SevenStdCall
        /// </summary>
        [Fact]
        public void manglingTest8()
        {
            using dynamic l = new ConariX(gCfgUnlib)
            {
                Mangling = false
            };

            var xfun = l.bind<Func<ushort>>("get_SevenStdCall");

            if(IntPtr.Size == sizeof(Int64))
            {
                Assert.Equal((ushort)7, xfun());
                return;
            }

            Assert.Throws<WinFuncFailException>(() =>
            {
                xfun();
            });
        }

        /// <summary>
        /// unsigned short int __stdcall get_SevenStdCall()
        /// x86: _get_SevenStdCall@0
        /// x64: get_SevenStdCall
        /// </summary>
        [Fact]
        public void manglingTest9()
        {
            using dynamic l = new ConariX(gCfgUnlib)
            {
                Mangling = false
            };

            var xfun = l.bind(Dynamic.GetMethodInfo(typeof(ushort)), "get_SevenStdCall")
                        .dynamic;

            if(IntPtr.Size == sizeof(Int64))
            {
                Assert.Equal((ushort)7, xfun.Invoke(null, Array.Empty<object>()));
                return;
            }

            Assert.Throws<WinFuncFailException>(() =>
            {
                xfun.Invoke(null, Array.Empty<object>());
            });
        }

        /// <summary>
        /// get_CharPtrVal
        /// </summary>
        [Fact]
        public void echoTest1()
        {
            using(dynamic l = new ConariX(gCfgUnlib))
            {
                string exp = "my string-123 !";

                using(var uns = new UnmanagedString(exp, UnmanagedString.SType.Ansi))
                {
                    CharPtr chrptr = uns;

                    Assert.Equal(exp, l.get_CharPtrVal<CharPtr>(chrptr));
                    Assert.Equal(exp, l.bind<Func<CharPtr, CharPtr>>("get_CharPtrVal")(chrptr));

                    var dyn = l.bind(Dynamic.GetMethodInfo(typeof(CharPtr), typeof(CharPtr)), "get_CharPtrVal");
                    Assert.Equal(exp, (CharPtr)dyn.dynamic.Invoke(null, new object[] { chrptr }));
                }
            }
        }

        /// <summary>
        /// get_WCharPtrVal
        /// </summary>
        [Fact]
        public void echoTest2()
        {
            using(dynamic l = new ConariX(gCfgUnlib))
            {
                string exp = "my string-123 !";

                using(var uns = new UnmanagedString(exp, UnmanagedString.SType.Unicode))
                {
                    WCharPtr wchrptr = uns;

                    Assert.Equal(exp, l.get_WCharPtrVal<WCharPtr>(wchrptr));
                    Assert.Equal(exp, l.bind<Func<WCharPtr, WCharPtr>>("get_WCharPtrVal")(wchrptr));

                    var dyn = l.bind(Dynamic.GetMethodInfo(typeof(WCharPtr), typeof(WCharPtr)), "get_WCharPtrVal");
                    Assert.Equal(exp, (WCharPtr)dyn.dynamic.Invoke(null, new object[] { wchrptr }));
                }
            }
        }

        /// <summary>
        /// get_BSTRVal
        /// </summary>
        [Fact]
        public void echoTest3()
        {
            using(dynamic l = new ConariX(gCfgUnlib))
            {
                string exp = "my string-123 !";

                using(var uns = new UnmanagedString(exp, UnmanagedString.SType.BSTR))
                {
                    BSTR bstr = uns;

                    Assert.Equal(exp, l.get_BSTRVal<BSTR>(bstr));
                    Assert.Equal(exp, l.bind<Func<BSTR, BSTR>>("get_BSTRVal")(bstr));

                    var dyn = l.bind(Dynamic.GetMethodInfo(typeof(BSTR), typeof(BSTR)), "get_BSTRVal");
                    Assert.Equal(exp, (BSTR)dyn.dynamic.Invoke(null, new object[] { bstr }));
                }
            }
        }

        /// <summary>
        /// get_StringPtrVal
        /// </summary>
        [Fact]
        public void echoTest4()
        {
            using(dynamic l = new ConariX(gCfgUnlib))
            {
                string exp = "my string-123 !";

                using(var uns = new UnmanagedString(exp, UnmanagedString.SType.Ansi))
                {
                    CharPtr chrptr = uns;

                    Assert.Equal(exp, l.get_StringPtrVal<CharPtr>(chrptr));
                    Assert.Equal(exp, l.bind<Func<CharPtr, CharPtr>>("get_StringPtrVal")(chrptr));

                    var dyn = l.bind(Dynamic.GetMethodInfo(typeof(CharPtr), typeof(CharPtr)), "get_StringPtrVal");
                    Assert.Equal(exp, (CharPtr)dyn.dynamic.Invoke(null, new object[] { chrptr }));
                }
            }
        }

        /// <summary>
        /// get_WStringPtrVal
        /// </summary>
        [Fact]
        public void echoTest5()
        {
            using(dynamic l = new ConariX(gCfgUnlib))
            {
                string exp = "my string-123 !";

                using(var uns = new UnmanagedString(exp, UnmanagedString.SType.Unicode))
                {
                    WCharPtr wchrptr = uns;

                    Assert.Equal(exp, l.get_WStringPtrVal<WCharPtr>(wchrptr));
                    Assert.Equal(exp, l.bind<Func<WCharPtr, WCharPtr>>("get_WStringPtrVal")(wchrptr));

                    var dyn = l.bind(Dynamic.GetMethodInfo(typeof(WCharPtr), typeof(WCharPtr)), "get_WStringPtrVal");
                    Assert.Equal(exp, (WCharPtr)dyn.dynamic.Invoke(null, new object[] { wchrptr }));
                }
            }
        }

        /// <summary>
        /// get_BoolVal
        /// </summary>
        [Fact]
        public void echoTest6()
        {
            using(dynamic l = new ConariX(gCfgUnlib))
            {
                Assert.Equal(false, l.get_BoolVal<bool>(false));
                Assert.False(l.bind<Func<bool, bool>>("get_BoolVal")(false));
                Assert.Equal(false, l.bind(Dynamic.GetMethodInfo(typeof(bool), typeof(bool)), "get_BoolVal")
                                                    .dynamic
                                                    .Invoke(null, new object[1] { false }));

                Assert.Equal(true, l.get_BoolVal<bool>(true));
                Assert.True(l.bind<Func<bool, bool>>("get_BoolVal")(true));
                Assert.Equal(true, l.bind(Dynamic.GetMethodInfo(typeof(bool), typeof(bool)), "get_BoolVal")
                                                    .dynamic
                                                    .Invoke(null, new object[1] { true }));
            }
        }

        /// <summary>
        /// get_IntVal
        /// </summary>
        [Fact]
        public void echoTest7()
        {
            using(dynamic l = new ConariX(gCfgUnlib))
            {
                Assert.Equal(0, l.get_IntVal<int>(0));
                Assert.Equal(-456, l.bind<Func<int, int>>("get_IntVal")(-456));
                Assert.Equal(1024, l.bind(Dynamic.GetMethodInfo(typeof(int), typeof(int)), "get_IntVal")
                                                    .dynamic
                                                    .Invoke(null, new object[1] { 1024 }));
            }
        }

        [Fact]
        public void complexTest1()
        {
            using(dynamic l = new ConariX(gCfgUnlib))
            {
                IntPtr ptr1 = l.get_TSpec<IntPtr>();
                IntPtr ptr2 = l.bind<Func<IntPtr>>("get_TSpec")();

                var dyn     = l.bind(Dynamic.GetMethodInfo(typeof(IntPtr)), "get_TSpec");
                IntPtr ptr3 = (IntPtr)dyn.dynamic.Invoke(null, Array.Empty<object>());

                Assert.NotEqual(IntPtr.Zero, ptr1);
                Assert.True(ptr1 == ptr2 && ptr2 == ptr3);

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
                                    .t<int, int>("a", "b")
                                    .t<CharPtr>("name");
                                    //.AlignSizeByMax; // byte = 4 / int = 4 / ptrx64 = 8

                byte[] bytes    = TSpecPtr.Raw.Values;
                dynamic dlr     = TSpecPtr.Raw.Type;
                var fields      = TSpecPtr.Raw.Type.Fields;

                Assert.Equal(3, fields.Count);

                int expA        = 2;
                int expB        = 4;
                string expName  = "Conari";

                // a
                Assert.Equal("a", fields[0].name);
                Assert.Equal(NativeData.SizeOf<int>(), fields[0].tsize);
                Assert.Equal(typeof(int), fields[0].type);
                Assert.Equal(expA, fields[0].value);

                // b
                Assert.Equal("b", fields[1].name);
                Assert.Equal(NativeData.SizeOf<int>(), fields[1].tsize);
                Assert.Equal(typeof(int), fields[1].type);
                Assert.Equal(expB, fields[1].value);

                // name
                Assert.Equal("name", fields[2].name);
                Assert.Equal(CharPtr.PtrSize, fields[2].tsize);
                Assert.Equal(typeof(CharPtr), fields[2].type);
                Assert.Equal(expName, (CharPtr)fields[2].value);

                // DLR
                Assert.Equal(expA, dlr.a);
                Assert.Equal(expB, dlr.b);
                Assert.Equal(expName, (CharPtr)dlr.name);

                // byte-seq
                var br = new BReader(bytes);
                Assert.Equal(expA, br.next<int>(NativeData.SizeOf<int>()));
                Assert.Equal(expB, br.next<int>(NativeData.SizeOf<int>()));
                Assert.Equal(expName, (CharPtr)br.next<CharPtr>(NativeData.SizeOf<CharPtr>()));
            }
        }

        [Fact]
        public void complexTest2()
        {
            using(dynamic l = new ConariX(gCfgUnlib))
            {
                IntPtr ptr = l.get_TSpecB_A_ptr<IntPtr>();
                Assert.NotEqual(IntPtr.Zero, ptr);

                /*                
                    struct TSpecA
                    {
                        int a;
                        int b;
                    };

                    struct TSpecB
                    {
                        bool d;
                        TSpecA* s;
                    };

                    A->a = 4;
                    A->b = -8;

                    B->d = true;
                    B->s = TSpecA*;

                 */
                var TSpecBPtr = NativeData
                                    ._(ptr)
                                    .t<bool>("d")
                                    .t<IntPtr>("s")
                                    .AlignSizeByMax;

                Assert.Equal(2, TSpecBPtr.Raw.Type.Fields.Count);

                dynamic dlr = TSpecBPtr.Raw.Type;

                IntPtr addrA = dlr.s;

                Assert.Equal(true, dlr.d);
                Assert.NotEqual(IntPtr.Zero, addrA);

                // B->A

                var TSpecAPtr = NativeData
                                    ._(addrA)
                                    .align<Int32>(2, "a", "b");

                Assert.Equal(2, TSpecAPtr.Raw.Type.Fields.Count);

                dynamic s = TSpecAPtr.Raw.Type;

                Assert.Equal(4, s.a);  // B->s->a
                Assert.Equal(-8, s.b); // B->s->b

                // the test with reading memory again

                dynamic attempt2 = NativeData
                                    ._(addrA)
                                    .align<Int32>(2, "a", "b")
                                    .Raw.Type;

                Assert.Equal(4, attempt2.a);  // B->s->a
                Assert.Equal(-8, attempt2.b); // B->s->b


                // free mem

                //var dirtyA = addrA;

                //l.free(addrA);

                //dynamic hole = NativeData
                //                    ._(dirtyA)
                //                    .align<Int32>(2, "a", "b")
                //                    .Raw.Type;

                //int _a = hole.a; // ~ 0
                //int _b = hole.b; // ~ 0
            }
        }

        /// <summary>
        /// get_CharPtrCmpRef
        /// </summary>
        [Fact]
        public void stringTest1()
        {
            using(dynamic l = new ConariX(gCfgUnlib))
            {
                string exp = "mystring-123 !";

                using(var uns1 = new UnmanagedString(exp, UnmanagedString.SType.Ansi))
                using(var uns2 = new UnmanagedString(exp, UnmanagedString.SType.Ansi))
                using(var uns3 = new UnmanagedString(" " + exp, UnmanagedString.SType.Ansi))
                {
                    CharPtr chrptr  = uns1;
                    CharPtr chrptr2 = uns2;

                    Assert.Equal(true, l.get_CharPtrCmpRef<bool>(chrptr, chrptr2));
                    Assert.True(l.bind<Func<CharPtr, CharPtr, bool>>("get_CharPtrCmpRef")(chrptr, chrptr2));

                    Assert.Equal(false, l.get_CharPtrCmpRef<bool>(chrptr, (CharPtr)uns3));
                }
            }
        }

        /// <summary>
        /// get_WCharPtrCmpRef
        /// </summary>
        [Fact]
        public void stringTest2()
        {
            using(dynamic l = new ConariX(gCfgUnlib))
            {
                string exp = "mystring-123 !";

                using(var uns1 = new UnmanagedString(exp, UnmanagedString.SType.Unicode))
                using(var uns2 = new UnmanagedString(exp, UnmanagedString.SType.Unicode))
                using(var uns3 = new UnmanagedString(" " + exp, UnmanagedString.SType.Unicode))
                {
                    WCharPtr wchrptr    = uns1;
                    WCharPtr wchrptr2   = uns2;

                    Assert.Equal(true, l.get_WCharPtrCmpRef<bool>(wchrptr, wchrptr2));
                    Assert.True(l.bind<Func<WCharPtr, WCharPtr, bool>>("get_WCharPtrCmpRef")(wchrptr, wchrptr2));

                    Assert.Equal(false, l.get_WCharPtrCmpRef<bool>(wchrptr, (WCharPtr)uns3));
                }
            }
        }

        /// <summary>
        /// get_StringPtrCmpRef
        /// </summary>
        [Fact]
        public void stringTest3()
        {
            using(dynamic l = new ConariX(gCfgUnlib))
            {
                string exp = "mystring-123 !";

                using(var uns1 = new UnmanagedString(exp, UnmanagedString.SType.Ansi))
                using(var uns2 = new UnmanagedString(exp, UnmanagedString.SType.Ansi))
                using(var uns3 = new UnmanagedString(" " + exp, UnmanagedString.SType.Ansi))
                {
                    CharPtr chrptr  = uns1;
                    CharPtr chrptr2 = uns2;

                    Assert.Equal(true, l.get_StringPtrCmpRef<bool>(chrptr, chrptr2));
                    Assert.True(l.bind<Func<CharPtr, CharPtr, bool>>("get_StringPtrCmpRef")(chrptr, chrptr2));

                    Assert.Equal(false, l.get_StringPtrCmpRef<bool>(chrptr, (CharPtr)uns3));
                }
            }
        }

        /// <summary>
        /// get_WStringPtrCmpRef
        /// </summary>
        [Fact]
        public void stringTest4()
        {
            using(dynamic l = new ConariX(gCfgUnlib))
            {
                string exp = "mystring-123 !";

                using(var uns1 = new UnmanagedString(exp, UnmanagedString.SType.Unicode))
                using(var uns2 = new UnmanagedString(exp, UnmanagedString.SType.Unicode))
                using(var uns3 = new UnmanagedString(" " + exp, UnmanagedString.SType.Unicode))
                {
                    WCharPtr chrptr  = uns1;
                    WCharPtr chrptr2 = uns2;

                    Assert.Equal(true, l.get_WStringPtrCmpRef<bool>(chrptr, chrptr2));
                    Assert.True(l.bind<Func<WCharPtr, WCharPtr, bool>>("get_WStringPtrCmpRef")(chrptr, chrptr2));

                    Assert.Equal(false, l.get_WStringPtrCmpRef<bool>(chrptr, (WCharPtr)uns3));
                }
            }
        }

        [Fact]
        public void chkTypeTVerTest1()
        {
            using(dynamic l = new ConariX(gCfgUnlib))
            {
                TVer v = new TVer(7, 0, 256);

                Assert.Equal(true, l.chkTypeTVer<bool>(v, 7, 0, 256));
                Assert.Equal(false, l.chkTypeTVer<bool>(v, 7, 1, 256));
            }
        }

        [Fact]
        public void chkTypeRefTVerTest1()
        {
            using(dynamic l = new ConariX(gCfgUnlib))
            {
                TVer v = new TVer(5, 0, 1024);

                using(var uv = new UnmanagedStructure(v))
                {
                    IntPtr ptr = uv;

                    Assert.Equal(true, l.chkTypeRefTVer<bool>(ptr, 5, 0, 1024));
                    Assert.Equal(false, l.chkTypeRefTVer<bool>(ptr, 5, 1, 1024));
                }
            }
        }
    }
}
