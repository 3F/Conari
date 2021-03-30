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
        [Fact]
        public async void userTest1()
        {
            _Recipe1 rcp = new();

            IConfig cfg = new Config(UNLIB_DLL, true)
            {
                ModuleIsolationRecipe = rcp
            };

            bool isolated = false;
            await mtaRun(() => 
            {
                using(var l = new ConariX(cfg))
                {
                    if(rcp.Destination != null)
                    {
                        cfg.Cts.Cancel();
                        isolated = true;
                    }
                }
            }, cfg);

            Assert.True(isolated);
        }

        [Fact]
        public async void userTest2()
        {
            IConfig cfg = new Config(UNLIB_DLL, true)
            {
                ModuleIsolationRecipe = new _Recipe0(),
                CancelIfCantIsolate = true
            };

            bool cancelled = false;
            await mtaRun(() => 
            {
                using(var l = new ConariX(cfg))
                {
                    Assert.False(l.Library.isolated);
                    if(l.Library.cancelled)
                    {
                        cancelled = true;
                    }
                }

            }, cfg);
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
            public const string CLLI = "CLLI-TEST-2301F37A-5F7D-45B7-9AED-ABC3988D953F";

            public string DstDir { get; private set; }
            public string Destination { get; private set; }

            public bool isolate(Link l, out string module)
            {
                DstDir = Path.Combine(Path.GetTempPath(), CLLI, Guid.NewGuid().ToString());
                Directory.CreateDirectory(DstDir);

                Destination = Path.Combine(DstDir, Path.GetFileName(l.module));
                File.Copy(l.module, Destination, true);

                module = Destination;
                return true;
            }

            public bool discard(Link l)
            {
                var dir = Path.GetDirectoryName(l.module);

                if(dir.IndexOf(CLLI) != -1)
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
