﻿/*!
 * Copyright (c) 2016  Denis Kuzmin <x-3F@outlook.com> github/3F
 * Copyright (c) Conari contributors https://github.com/3F/Conari/graphs/contributors
 * Licensed under the MIT License (MIT).
 * See accompanying LICENSE.txt file or visit https://github.com/3F/Conari
*/

using System;
using System.Runtime.InteropServices;
using net.r_eg.Conari;
using net.r_eg.Conari.Accessors.WinAPI;
using Xunit;

#if !NETCORE
using ConariTest._svc.Extensions;
#endif

namespace ConariTest.Accessors.WinAPI
{
    public class Kernel32Test
    {
        [Fact]
        public void ctorTest1()
        {
            using(dynamic kernel32 = new Kernel32())
            {
                Assert.True(kernel32.Library.IsActive);
                Assert.NotEqual(IntPtr.Zero, kernel32.Library.handle);
                Assert.False(kernel32.Library.isolated);

                Assert.NotNull(kernel32.Library.resolved);
                Assert.True(kernel32.Library.resolved.value);

                string module = (string)kernel32.Library.module;
                Assert.NotNull(module);
                Assert.True(module.Contains("kernel32", StringComparison.OrdinalIgnoreCase));
            }
        }

        [Fact]
        public void ctorTest2()
        {
            using(dynamic kernel32 = new Kernel32(new Config() 
            { 
                Module = "CustomModule.dll"
            }))
            {
                Assert.Equal(CallingConvention.Winapi, kernel32.Convention);

                Assert.True(kernel32.Library.IsActive);
                Assert.NotEqual(IntPtr.Zero, kernel32.Library.handle);
                Assert.False(kernel32.Library.isolated);

                Assert.NotNull(kernel32.Library.resolved);
                Assert.True(kernel32.Library.resolved.value);

                string module = (string)kernel32.Library.module;
                Assert.NotNull(module);
                Assert.True(module.Contains("kernel32", StringComparison.OrdinalIgnoreCase));
            }
        }

        [Fact]
        public void ctorTest3()
        {
            using(dynamic kernel32 = new Kernel32((IConfig)null))
            {
                Assert.True(kernel32.Library.IsActive);
                Assert.NotEqual(IntPtr.Zero, kernel32.Library.handle);
                Assert.False(kernel32.Library.isolated);

                Assert.NotNull(kernel32.Library.resolved);
                Assert.True(kernel32.Library.resolved.value);

                string module = (string)kernel32.Library.module;
                Assert.NotNull(module);
                Assert.True(module.Contains("kernel32", StringComparison.OrdinalIgnoreCase));
            }
        }

        [Fact]
        public void convTest1()
        {
            Assert.Throws<NotSupportedException>(() =>
            {
                using(dynamic kernel32 = new Kernel32())
                {
                    kernel32.Convention = CallingConvention.Cdecl;
                }
            });
        }
    }
}
