/*!
 * Copyright (c) 2016  Denis Kuzmin <x-3F@outlook.com> github/3F
 * Copyright (c) Conari contributors https://github.com/3F/Conari/graphs/contributors
 * Licensed under the MIT License (MIT).
 * See accompanying LICENSE.txt file or visit https://github.com/3F/Conari
*/

using System;
using System.Runtime.InteropServices;
using net.r_eg.Conari;
using net.r_eg.Conari.Types;
using Xunit;
using static net.r_eg.Conari.Static.Members;

namespace ConariTest.Types
{
    using static _svc.TestHelper;

    public class NativeStructTest
    {
        [Fact]
        public void allocTest1()
        {
            using var c = ConariL.Make(new(gCfgIsolatedRxW), out dynamic l);

            using var u = NativeStruct.Make.f<UIntPtr>("start", "end").Struct;

            Assert.True(l.match<bool>(c._T("0123456"), c._T("234"), EngineOptions.F_MATCH_RESULT, u));

            dynamic v = u.Access;

            Assert.Equal((UIntPtr)2, v.start);
            Assert.Equal((UIntPtr)5, v.end);

            Assert.True(l.match<bool>(c._T("0123456"), c._T("1*5"), EngineOptions.F_MATCH_RESULT, (IntPtr)u));

            v = u.Access;

            Assert.Equal((UIntPtr)1, v.start);
            Assert.Equal((UIntPtr)6, v.end);
        }

        [Fact]
        public void allocTest2()
        {
            using var c = ConariL.Make(new(gCfgIsolatedRxW), out dynamic l);
            using var u = new NativeStruct();

            Assert.True(l.match<bool>(c._T("0123456"), c._T("234"), EngineOptions.F_MATCH_RESULT, u));

            u.Native
                .f<UIntPtr>("start", "end")
                .build(out dynamic mres);

            Assert.Equal((UIntPtr)2, mres.start);
            Assert.Equal((UIntPtr)5, mres.end);
        }

        [Fact]
        public void allocTest3()
        {
            using var c = ConariL.Make(new(gCfgIsolatedRxW), out dynamic l);

            using var u = new NativeStruct<MatchResult>();

            Assert.True(l.match<bool>(c._T("system"), c._T("syStem"), EngineOptions.F_ICASE | EngineOptions.F_MATCH_RESULT, u));
            Assert.Equal(n(0), u.Data.start);
            Assert.Equal(n(6), u.Data.end);

            Assert.False(l.match<bool>(c._T("system"), c._T("1"), EngineOptions.F_NONE, (IntPtr)u));
            Assert.Equal(MatchResult.npos, u.read().Data.start);

            Assert.True(l.matchOfs<bool>(c._T("number_str = '+12'"), c._T("str"), n(5), EngineOptions.F_NONE, u));
            Assert.Equal(MatchResult.npos, u.read().Data.start);

            Assert.True(l.matchOfs<bool>(c._T("number_str = '+12'"), c._T("str"), n(5), EngineOptions.F_MATCH_RESULT, (IntPtr)u));
            u.read();
            Assert.Equal(n(7), u.Data.start);
            Assert.Equal(n(10), u.Data.end);

            Assert.False(l.matchOfs<bool>(c._T("number_str = '+12'"), c._T("str"), n(8), EngineOptions.F_NONE, u));
            Assert.Equal(MatchResult.npos, u.read().Data.start);

        }

        #region decl struct way

        private static UIntPtr n(nuint v) => v;

        [StructLayout(LayoutKind.Sequential)]
        private struct MatchResult
        {
            public static readonly UIntPtr npos = new(Is64bit ? ulong.MaxValue : uint.MaxValue);

            public UIntPtr start;
            public UIntPtr end;
        }

        [Flags]
        private enum EngineOptions: uint
        {
            F_NONE = 0,

            F_ICASE = 0x001,

            F_MATCH_RESULT = 0x002,
        }

        #endregion
    }
}
