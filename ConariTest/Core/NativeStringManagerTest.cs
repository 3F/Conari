using System;
using System.Collections.Concurrent;
using net.r_eg.Conari;
using net.r_eg.Conari.Core;
using net.r_eg.Conari.Types;
using Xunit;

namespace ConariTest.Core
{
    using static _svc.TestHelper;

    public class NativeStringManagerTest
    {
        [Fact]
        public void lxTest1()
        {
            using ConariL l = new(gCfgIsolatedRxW);
            Assert.True
            (
                l._.replace<bool>
                (
                    l._T("number = 888;", out CharPtr result),
                    l._T("+??;"), l._T("2034;")
                )
            );
            Assert.Equal("number = 2034;", result);
        }

        [Fact]
        public void lxTest2()
        {
            using dynamic l = new ConariX(gCfgIsolatedRxW);

            Assert.True(l.replace<bool>((IntPtr)l._T("Hello {p}", out CharPtr result), (IntPtr)l._T("{p}"), (IntPtr)l._T("world!")));
            Assert.Equal("Hello world!", result);
        }

        [Fact]
        public void allocTest1()
        {
            using ConariL l = new(gCfgIsolatedRxW);
            using NativeStringManager<CharPtr> nsm = new();

            Assert.True(l._.replace<bool>(nsm._T("Hello {p}", out CharPtr result), nsm._T("{p}"), nsm._T("world!")));
            Assert.Equal("Hello world!", result);
        }

        [Fact]
        public void allocTest2()
        {
            using _NSM<CharPtr> nsm = new();
            nsm._T("str1");
            nsm._T("str2", out CharPtr str2);
            nsm._T("str3", 0x1F, out CharPtr str3);

            Assert.Equal(3, nsm.collection.Count);

            nsm.cstr("str1");
            nsm.cstr("str2", out CharPtr cstr2);
            nsm.cstr("str3", 0x1F, out CharPtr cstr3);

            Assert.Equal(6, nsm.collection.Count);

            Assert.Equal(str2, cstr2);
            Assert.Equal("str2", cstr2);
            Assert.NotEqual((IntPtr)str2, (IntPtr)cstr2);

            Assert.True(nsm.collection.TryGetValue(str2, out _));
            nsm.release(str2);
            Assert.False(nsm.collection.TryGetValue(str2, out _));
            Assert.Equal(5, nsm.collection.Count);


            Assert.True(nsm.collection.TryGetValue(cstr3, out _));
            nsm.release(cstr3);
            Assert.False(nsm.collection.TryGetValue(cstr3, out _));
            Assert.Equal(4, nsm.collection.Count);

            nsm.release();
            Assert.Empty(nsm.collection);
        }

        [Fact]
        public void allocTest3()
        {
            using _NSM<CharPtr> nsm = new();

            IntPtr addr1 = nsm._T<WCharPtr>("Hello", 0);
            IntPtr addr2 = nsm._T<WCharPtr>("Hello");
            IntPtr addr3 = nsm.cstr<WCharPtr>("world", 1);

            IntPtr addr4 = nsm.cstr("Hello", 0);
            IntPtr addr5 = nsm._T("world", 0);

            Assert.Equal(5, nsm.collection.Count);

            Assert.Equal((WCharPtr)addr1, (CharPtr)addr4);
            Assert.Equal((WCharPtr)addr3, (CharPtr)addr5);
            Assert.NotEqual(addr1, addr2);
        }

        [Fact]
        public void allocTest4()
        {
            using _NSM<WCharPtr> nsm = new();
            nsm._T<CharPtr>("str1");
            nsm._T<CharPtr>("str2", out CharPtr str2);
            nsm._T<CharPtr>("str3", 0x1F, out CharPtr str3);

            Assert.Equal(3, nsm.collection.Count);

            nsm.cstr<CharPtr>("str1");
            nsm.cstr<CharPtr>("str2", out CharPtr cstr2);
            nsm.cstr<CharPtr>("str3", 0x1F, out CharPtr cstr3);

            Assert.Equal(6, nsm.collection.Count);

            Assert.Equal(str2, cstr2);
            Assert.Equal("str2", cstr2);
            Assert.NotEqual((IntPtr)str2, (IntPtr)cstr2);

            Assert.True(nsm.collection.TryGetValue(str2, out _));
            nsm.release(str2);
            Assert.False(nsm.collection.TryGetValue(str2, out _));
            Assert.Equal(5, nsm.collection.Count);


            Assert.True(nsm.collection.TryGetValue(cstr3, out _));
            nsm.release(cstr3);
            Assert.False(nsm.collection.TryGetValue(cstr3, out _));
            Assert.Equal(4, nsm.collection.Count);

            nsm.release();
            Assert.Empty(nsm.collection);
        }

        [Fact]
        public void allocTest5()
        {
            using _NSM<WCharPtr> nsm = new();

            IntPtr addr1 = nsm._T<CharPtr>("Hello", 0);
            IntPtr addr2 = nsm._T<CharPtr>("Hello");
            IntPtr addr3 = nsm.cstr<CharPtr>("world", 1);

            IntPtr addr4 = nsm.cstr("Hello", 0);
            IntPtr addr5 = nsm._T("world", 0);

            Assert.Equal(5, nsm.collection.Count);

            Assert.Equal((CharPtr)addr1, (WCharPtr)addr4);
            Assert.Equal((CharPtr)addr3, (WCharPtr)addr5);
            Assert.NotEqual(addr1, addr2);
        }

        private sealed class _NSM<T>: NativeStringManager<T>
            where T: struct
        {
            public ConcurrentDictionary<IntPtr, IDisposable> collection => strings;
        }
    }
}
