/*!
 * Copyright (c) 2016  Denis Kuzmin <x-3F@outlook.com> github/3F
 * Copyright (c) Conari contributors https://github.com/3F/Conari/graphs/contributors
 * Licensed under the MIT License (MIT).
 * See accompanying LICENSE.txt file or visit https://github.com/3F/Conari
*/

using System;
using System.IO;
using net.r_eg.Conari;
using net.r_eg.Conari.Core;
using Xunit;

namespace ConariTest.Core
{
    using static _svc.TestHelper;

    public class ModuleIsolationRecipeTest
    {
        /// <summary>
        /// Test succeeds if user isolation has been performed at least once.
        /// See also <see cref="MtaTest"/>
        /// </summary>
        [Fact]
        public async void userTest1()
        {
            _Recipe1 rcp = new();

            IConfig cfg = new Config(UNLIB_DLL, true)
            {
                ModuleIsolationRecipe = rcp
            };

            bool isolated = false;
            await MtaRun(() => 
            {
                if(isolated) return;
                using dynamic l = new ConariX(cfg);

                if(!l.Library.cancelled)
                {
                    l.get_True<bool>();
                }

                if(rcp.Destination != null)
                {
                    cfg.Cts.Cancel();
                    isolated = true;
                }

            }, cfg, 10_000);

            Assert.True(isolated);
        }

        /// <summary>
        /// Test succeeds if user isolation has been performed at least once.
        /// See also <see cref="MtaTest"/>
        /// </summary>
        [Fact]
        public async void userTest2()
        {
            IConfig cfg = new Config(UNLIB_DLL, true)
            {
                ModuleIsolationRecipe = new _Recipe0(),
                CancelIfCantIsolate = true
            };

            bool cancelled = false;
            await MtaRun(() => 
            {
                if(cancelled) return;
                using dynamic l = new ConariX(cfg);

                Assert.False(l.Library.isolated);
                if(l.Library.cancelled)
                {
                    cancelled = true;
                }
                else
                {
                    l.get_True<bool>();
                }

            }, cfg, 10_000);
            Assert.True(cancelled);
        }

        private class _Recipe0: IModuleIsolationRecipe
        {
            public bool isolate(Link l, out string module)
            {
                throw new NotImplementedException();
            }

            public bool discard(Link l)
            {
                throw new NotImplementedException();
            }
        }

        private class _Recipe1: IModuleIsolationRecipe
        {
            public string DstDir { get; private set; }
            public string Destination { get; private set; }

            public bool isolate(Link l, out string module)
            {
                DstDir = Path.Combine(TempDstPath, Guid.NewGuid().ToString());
                Directory.CreateDirectory(DstDir);

                Destination = Path.Combine(DstDir, Path.GetFileName(l.module));
                File.Copy(l.module, Destination, true);

                module = Destination;
                return true;
            }

            public bool discard(Link l)
            {
                var dir = Path.GetDirectoryName(l.module);

                if(dir.IndexOf(CLLI_TEST) != -1)
                {
                    File.Delete(l.module);
                    Directory.Delete(dir, false);
                    return true;
                }
                return false;
            }
        }
    }
}
