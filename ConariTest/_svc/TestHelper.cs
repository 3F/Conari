﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using net.r_eg.Conari.Core;

namespace ConariTest._svc
{
    internal static class TestHelper
    {
        public const string UNLIB_DLL = @".\UnLib.dll";
        public const string STUB_LIB_NAME = "__ThisIsNotRealUserLib";

        internal static async Task mtaRun(Action act, IConfig cfg, int limit = 1000)
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
