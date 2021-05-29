using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using net.r_eg.Conari.Core;

namespace ConariTest._svc
{
    internal static class TestHelper
    {
        public const string UNLIB_DLL = @"..\UnLib.dll";
        public const string STUB_LIB_NAME = "__ThisIsNotRealUserLib";

        public const string RXW_X32 = @".\x32\regXwild.dll";
        public const string RXW_X64 = @".\x64\regXwild.dll";

#if TEST_LIB_ARCH32
        public const string RXW_X = RXW_X32;
#else
        public const string RXW_X = RXW_X64;
#endif

        public static readonly IConfig gCfgIsolatedRxW = new Config(RXW_X, isolate: true);

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
