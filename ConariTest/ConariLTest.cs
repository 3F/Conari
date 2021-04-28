﻿using System;
using System.Runtime.InteropServices;
using net.r_eg.Conari;
using net.r_eg.Conari.Core;
using net.r_eg.Conari.Exceptions;
using net.r_eg.Conari.Types;
using Xunit;

namespace ConariTest
{
    using static _svc.TestHelper;

    public class ConariLTest
    {
        [Fact]
        public void MakeTest1()
        {
            using var l = ConariL.Make(new(RXW_X), out dynamic d);

            Assert.NotEmpty(l.PE.Export.Names);
            Assert.Contains("versionString", l.PE.Export.Names);
            string ver = d.versionString<CharPtr>();
            Assert.NotNull(ver);

            Version v = new(ver);
            Assert.True(v.Major >= 1);
            Assert.True(v.Minor >= 4);
        }

        [Fact]
        public void loadTest1()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new ConariL((string)null)
            );

            Assert.Throws<ArgumentNullException>(() =>
                new ConariL((IConfig)null)
            );

            Assert.Throws<LoadLibException>(() =>
                new ConariL(STUB_LIB_NAME)
            );
        }

        [Fact]
        public void loadTest2()
        {
            new ConariL(new Config("") { LazyLoading = true }).Dispose();

            Assert.Throws<ArgumentNullException>(() =>
                new ConariL("")
            );
        }

        [Fact]
        public void funcNameTest1()
        {
            var l = new ConariL(new Config("") { LazyLoading = true });

            Assert.Throws<ArgumentNullException>(() =>
                l.procName("")
            );

            Assert.Throws<ArgumentNullException>(() =>
                l.procName(null)
            );

            l.Dispose();
        }

        [Fact]
        public void funcNameTest2()
        {
            using(var l = new ConariL(
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
            using(var l = new ConariL(
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
            using(var l = new ConariL(
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
            using(IConari l = new ConariL(
                                 new Config("") {
                                     LazyLoading = true
                                 }, CallingConvention.Cdecl))
            {
                Assert.Equal(CallingConvention.Cdecl, l.Convention);
            }
        }
    }
}
