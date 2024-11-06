/*!
 * Copyright (c) 2016  Denis Kuzmin <x-3F@outlook.com> github/3F
 * Copyright (c) Conari contributors https://github.com/3F/Conari/graphs/contributors
 * Licensed under the MIT License (MIT).
 * See accompanying LICENSE.txt file or visit https://github.com/3F/Conari
*/

using System;
using net.r_eg.Conari;
using net.r_eg.Conari.Types;
using Xunit;

namespace ConariTest.Types.Sequential
{
    using static _svc.TestHelper;

    [Collection("Sequential")]
    public class NativeStringSequentialTest
    {
        [Fact]
        public void strTest1()
        {
            TCharPtr.__Unicode = false;

            using dynamic l = new ConariX(regXwildDll);
            using var data = new NativeString<TCharPtr>("Hello {p}!", 2);

            using NativeString<TCharPtr> filter = new("{p}");
            using NativeString<TCharPtr> replacement = new("world");

            Assert.True(l.replace<bool>((IntPtr)data, (IntPtr)filter, (IntPtr)replacement));
            Assert.Equal("Hello world!", (TCharPtr)data);

            Assert.True(data.Owner);
            Assert.True(filter.Owner);
            Assert.True(replacement.Owner);
        }

        [Fact]
        public void strTest2()
        {
            TCharPtr.__Unicode = false;

            using dynamic l = new ConariX(regXwildDll);
            using var data = new NativeString<TCharPtr>("Hello {p}!", 2);

            using NativeString<TCharPtr> filter = new("{p}");
            using NativeString<TCharPtr> replacement = new("world");

            Assert.True(l.replace<bool>(data, filter, replacement));
            Assert.Equal("Hello world!", (TCharPtr)data);
        }

        [Fact]
        public void strTest3()
        {
            TCharPtr.__Unicode = false;

            using var l = new ConariL(regXwildDll);
            using var data = new NativeString<TCharPtr>("Hello {p}!", 2);

            using NativeString<TCharPtr> filter = new("{p}");
            using NativeString<TCharPtr> replacement = new("world");

            Assert.True(l.bind<Func<TCharPtr, TCharPtr, TCharPtr, bool>>("replace")(data, filter, replacement));
            Assert.Equal("Hello world!", (TCharPtr)data);
        }

        [Fact]
        public void strTest4()
        {
            using dynamic l = new ConariX(regXwildDll);
            using var data = new NativeString<CharPtr>("Hello {p}!", 2);

            using NativeString<CharPtr> filter = new("{p}");
            using NativeString<CharPtr> replacement = new("world");

            Assert.True(l.replace<bool>(data, filter, replacement));
            Assert.Equal("Hello world!", (CharPtr)data);
        }

        [Fact]
        public void strTest5()
        {
            using var l = new ConariL(regXwildDll);
            using var data = new NativeString<CharPtr>("Hello {p}!", 2);

            using NativeString<CharPtr> filter = new("{p}");
            using NativeString<CharPtr> replacement = new("world");

            Assert.True(l.bind<Func<CharPtr, CharPtr, CharPtr, bool>>("replace")(data, filter, replacement));
            Assert.Equal("Hello world!", (CharPtr)data);
        }
    }
}
