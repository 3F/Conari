using System;
using net.r_eg.Conari;
using net.r_eg.Conari.Extension;
using net.r_eg.Conari.Types;
using net.r_eg.Conari.Types.Action.Out;
using net.r_eg.Conari.Types.Action.Ref;
using net.r_eg.Conari.Types.Func.Out;
using net.r_eg.Conari.Types.Func.Ref;
using Xunit;

namespace ConariTest
{
    using static _svc.TestHelper;

    public class BindingContextTest
    {
        [Fact]
        public void exTest1()
        {
            using dynamic l = new ConariX(RXW_X);

            string data = "number = 888;";
            Assert.True(l.replace<bool>(ref data, "+??;", "2034;"));
            Assert.Equal("number = 2034;", data);
        }

        [Fact]
        public void dlrStringTest1()
        {
            using dynamic l = new ConariX(RXW_X);

            bool found = l.replace<bool>
            (
                "Hello {p}".Do(out TCharPtr result),
                "{p}",
                "world!"
            );

            Assert.True(found);
            Assert.Equal("Hello world!", result);
        }

        [Fact]
        public void dlrStringTest2()
        {
            using dynamic l = new ConariX<CharPtr>(RXW_X);

            bool found = l.replace<bool>
            (
                "Hello {p}".Do(out CharPtr result),
                "{p}",
                "world!"
            );

            Assert.True(found);
            Assert.Equal("Hello world!", result);
        }

        [Fact]
        public void dlrStringTest3()
        {
            using dynamic l = new ConariX(UNLIB_DLL);

            string result = l.get_CharPtrVal<string>("Hi!");

            Assert.Equal("Hi!", result);
        }

        [Fact]
        public void dlrStringTest4()
        {
            using(dynamic l = new ConariX<WCharPtr>(RXW_X))
            {
                Assert.Throws<NotSupportedException>(() => l.replace<bool>("".Do(out CharPtr r), "", ""));
                Assert.Throws<NotSupportedException>(() => l.replace<bool>("".Do(out TCharPtr r), "", ""));
            }

            using(dynamic l = new ConariX<CharPtr>(RXW_X))
            {
                Assert.Throws<NotSupportedException>(() => l.replace<bool>("".Do(out WCharPtr r), "", ""));
                Assert.Throws<NotSupportedException>(() => l.replace<bool>("".Do(out TCharPtr r), "", ""));
            }

            using(dynamic l = new ConariX<TCharPtr>(RXW_X))
            {
                Assert.Throws<NotSupportedException>(() => l.replace<bool>("".Do(out CharPtr r), "", ""));
                Assert.Throws<NotSupportedException>(() => l.replace<bool>("".Do(out WCharPtr r), "", ""));
            }
        }

        [Fact]
        public void dlrRefStringTest1()
        {
            using dynamic l = new ConariX(RXW_X);

            string str = "Hello {p}";
            bool found = l.replace<bool>
            (
                ref str,
                "{p}",
                "world!"
            );

            Assert.True(found);
            Assert.Equal("Hello world!", str);
        }

        [Fact]
        public void lambdaStringTest1()
        {
            using ConariL l = new(RXW_X);

            Assert.True(l.bind<Func<TCharPtr, string, string, bool>>("replace")
            (
                l._T("Hello {p}", out TCharPtr result),
                "{p}",
                "world!"
            ));
            Assert.Equal("Hello world!", result);

            Assert.True(l.bind<Func<IntPtr, string, string, bool>>("replace")
            (
                l._T("Hello {p}", out TCharPtr result2),
                "{p}",
                "there!"
            ));
            Assert.Equal("Hello there!", result2);
        }

        [Fact]
        public void lambdaStringTest2()
        {
            using ConariL<CharPtr> l = new(RXW_X);

            bool found = l.bind<Func<IntPtr, string, string, bool>>("replace")
            (
                "Hello {p}".Do(out CharPtr result),
                "{p}",
                "world!"
            );

            Assert.True(found);
            Assert.Equal("Hello world!", result);
        }

        [Fact]
        public void lambdaStringTest3()
        {
            using(ConariL l = new(UNLIB_DLL))
            {
                TCharPtr result = l.bind<Func<TCharPtr, TCharPtr>>("get_CharPtrVal")(l._T("Hi!"));
                Assert.Equal("Hi!", result);
            }

            using(ConariL<CharPtr> l = new(UNLIB_DLL))
            {
                CharPtr result = l.bind<Func<CharPtr, CharPtr>>("get_CharPtrVal")(l._T("Hi!"));
                Assert.Equal("Hi!", result);
            }
        }

        [Fact]
        public void lambdaStringTest4()
        {
            using(ConariL l = new(UNLIB_DLL))
            {
                string result = l.bind<Func<TCharPtr, TCharPtr>>("get_CharPtrVal")(l._T("Hello!"));
                Assert.Equal("Hello!", result);
            }

            using(ConariL<CharPtr> l = new(UNLIB_DLL))
            {
                string result = l.bind<Func<CharPtr, CharPtr>>("get_CharPtrVal")(l._T("Hello!"));
                Assert.Equal("Hello!", result);
            }
        }

        [Fact]
        public void contextTest1()
        {
            using ConariL l = new(RXW_X, true);
            l._.replace<bool>
            (
                l._T("Hello {p}", out CharPtr result1), 
                l._T("{p}"), 
                l._T("world!")
            );
            Assert.Equal("Hello world!", result1);

            using dynamic x = new ConariX(RXW_X, true);
            x.replace<bool>
            (
                (IntPtr)l._T("Hello {p}", out CharPtr result2), 
                (IntPtr)l._T("{p}"), 
                (IntPtr)l._T("world!")
            );
            Assert.Equal(result1, result2);

            using dynamic x2 = new ConariX(RXW_X, true);
            x2.replace<bool>
            (
                l._T("Hello {p}", out CharPtr result3), 
                l._T("{p}"),
                l._T("world!")
            );
            Assert.Equal(result1, result3);
        }

        [Fact]
        public void contextTest2()
        {
            using ConariX l = ConariX.Make(new ConariX(RXW_X, true), out dynamic x);
            x.replace<bool>
            (
                l._T("Hello {p}", out CharPtr result), 
                l._T("{p}"),
                l._T("world!")
            );
            Assert.Equal("Hello world!", result);
        }

        [Fact]
        public void contextTest3()
        {
            using dynamic l = new ConariX(RXW_X);
            l.replace<bool>
            (
                l._T("Hello {p}", out CharPtr result),
                l._T("{p}"),
                l._T("world!")
            );
            Assert.Equal("Hello world!", result);
        }

        [Fact]
        public void contextTest4()
        {
            using dynamic l = new ConariX(RXW_X);

            Assert.False(l.searchEssC<bool>((IntPtr)l._T("123"), (IntPtr)l._T("4"), true));
            Assert.True(l.searchEssC<bool>((IntPtr)l._T("123"), (IntPtr)l._T("2"), true));

            Assert.False(l.searchEssC<bool>(l._T("123"), l._T("4"), true));
            Assert.True(l.searchEssC<bool>(l._T("123"), l._T("2"), true));
        }

        [Fact]
        public void contextTest5()
        {
            using dynamic l = new ConariX(RXW_X);

            Assert.False(l.searchEssC<bool>(l._T("123"), l._T("4"), true));
            Assert.True(l.searchEssC<bool>(l._T("123"), l._T("2"), true));

            Assert.False(l.searchEssC<bool>((IntPtr)l._T("123"), (IntPtr)l._T("4"), true));
            Assert.True(l.searchEssC<bool>((IntPtr)l._T("123"), (IntPtr)l._T("2"), true));
        }

        [Fact]
        public void refTest1()
        {
            using ConariL l = new(UNLIB_DLL);

            l.bind<ActionOut3<int, int, int>>("addRefVal")(10, 4, out int result);
            Assert.Equal(14, result);
        }

        [Fact]
        public void refTest2()
        {
            using ConariL l = new(UNLIB_DLL);

            int result = 0;
            l.bind<ActionRef3<int, int, int>>("addRefVal")(5, 7, ref result);
            Assert.Equal(12, result);
        }

        [Fact]
        public void refTest3()
        {
            using ConariL l = new(UNLIB_DLL);

            l.bind<ActionOut3<int, int, int>>("addRefVal")(10, 4, out int result1);
            Assert.Equal(14, result1);

            int result2 = 0;
            l.bind<ActionRef3<int, int, int>>("addRefVal")(5, 7, ref result2);
            Assert.Equal(12, result2);

            l.bind<ActionOut3<int, int, int>>("addRefVal")(2, 2, out int result3);
            Assert.Equal(4, result3);
        }

        [Fact]
        public void retRefTest1()
        {
            using ConariL l = new(UNLIB_DLL);

            Assert.Equal(0, l.bind<FuncOut3<int, int, int, int>>("retAddRefVal")(10, 4, out int result));
            Assert.Equal(14, result);
        }

        [Fact]
        public void retRefTest2()
        {
            using ConariL l = new(UNLIB_DLL);

            int result = 3;
            Assert.Equal(3, l.bind<FuncRef3<int, int, int, int>>("retAddRefVal")(5, 7, ref result));
            Assert.Equal(12, result);
        }

        [Fact]
        public void retRefTest3()
        {
            using ConariL l = new(UNLIB_DLL);

            Assert.Equal(0, l.bind<FuncOut3<int, int, int, int>>("retAddRefVal")(10, 4, out int result1));
            Assert.Equal(14, result1);

            int result2 = 4;
            Assert.Equal(4, l.bind<FuncRef3<int, int, int, int>>("retAddRefVal")(5, 7, ref result2));
            Assert.Equal(12, result2);

            Assert.Equal(0, l.bind<FuncOut3<int, int, int, int>>("retAddRefVal")(2, 2, out int result3));
            Assert.Equal(4, result3);
        }

        [Fact]
        public void refDlrTest1()
        {
            using dynamic l = new ConariX(UNLIB_DLL);

            l.addRefVal(100, 40, out int result);
            Assert.Equal(140, result);
        }

        [Fact]
        public void refDlrTest2()
        {
            using dynamic l = new ConariX(UNLIB_DLL);

            int result = 5;
            l.addRefVal(50, 70, ref result);
            Assert.Equal(120, result);
        }

        [Fact]
        public void refDlrTest3()
        {
            using dynamic l = new ConariX(UNLIB_DLL);

            l.addRefVal(100, 40, out int result1);
            Assert.Equal(140, result1);

            int result2 = 5;
            l.addRefVal(50, 70, ref result2);
            Assert.Equal(120, result2);

            l.addRefVal(20, 20, out int result3);
            Assert.Equal(40, result3);
        }

        [Fact]
        public void retRefDlrTest1()
        {
            using dynamic l = new ConariX(UNLIB_DLL);

            Assert.Equal(0, l.retAddRefVal<int>(100, 40, out int result));
            Assert.Equal(140, result);
        }

        [Fact]
        public void retRefDlrTest2()
        {
            using dynamic l = new ConariX(UNLIB_DLL);

            int result = 5;
            Assert.Equal(5, l.retAddRefVal<int>(50, 70, ref result));
            Assert.Equal(120, result);
        }

        [Fact]
        public void retRefDlrTest3()
        {
            using dynamic l = new ConariX(UNLIB_DLL);

            Assert.Equal(0, l.retAddRefVal<int>(100, 40, out int result1));
            Assert.Equal(140, result1);

            int result2 = 5;
            Assert.Equal(5, l.retAddRefVal<int>(50, 70, ref result2));
            Assert.Equal(120, result2);

            Assert.Equal(0, l.retAddRefVal<int>(20, 20, out int result3));
            Assert.Equal(40, result3);
        }

        [Fact]
        public void updateStringTest1()
        {
            using dynamic l = new ConariX(UNLIB_DLL);

            Assert.True(l.updateTCharPtr<bool>("Hello !".Do(out TCharPtr str), "Conari!"));
            Assert.Equal("Conari!", str);

            Assert.False(l.updateTCharPtr<bool>("Hi!", "Conari!"));
        }
    }
}
