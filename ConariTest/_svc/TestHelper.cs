/*!
 * Copyright (c) 2016  Denis Kuzmin <x-3F@outlook.com> github/3F
 * Copyright (c) Conari contributors https://github.com/3F/Conari/graphs/contributors
 * Licensed under the MIT License (MIT).
 * See accompanying LICENSE.txt file or visit https://github.com/3F/Conari
*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using net.r_eg.Conari;

namespace ConariTest._svc
{
    using static net.r_eg.Conari.Static.Members;

    internal static class TestHelper
    {
        public const string UNLIB_DLL = @"..\UnLib.dll";
        public const string STUB_LIB_NAME = "__ThisIsNotRealUserLib";

        public const string RXW_X32 = @"x32\regXwild.dll";
        public const string RXW_X64 = @"x64\regXwild.dll";

        public static readonly string regXwildDll = Path.GetFullPath(Path.Combine
        (
            AppDomain.CurrentDomain.BaseDirectory,
            Is64bit ? RXW_X64 : RXW_X32
        ));

        public static readonly IConfig gCfgIsolatedRxW = new Config(regXwildDll, isolate: true);

        public const string CLLI_TEST = "CLLI-TEST-2301F37A-5F7D-45B7-9AED-ABC3988D953F";

        internal static string TempDstPath => Path.Combine(Path.GetTempPath(), CLLI_TEST);

        internal static async Task MtaRun(Action act, IConfig cfg, int limit
#if DEBUG
            = 100
#else
            = 300
#endif
            )
        {
            using CancellationTokenSource cts = new();
            cfg.Cts = cts;

            List<Task> tasks = new(limit / 10);

            int i = 0;
            for(; i <= limit; ++i)
            {
                Task t = new(act);

                if(i == limit || cts.IsCancellationRequested) break;

                tasks.Add(t);
                t.Start();
            }

            await Task.WhenAll(tasks.ToArray());
        }
    }
}
