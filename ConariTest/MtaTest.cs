using System;
using net.r_eg.Conari;
using net.r_eg.Conari.Core;
using Xunit;

namespace ConariTest
{
    using static _svc.TestHelper;

    public class MtaTest
    {
        [Fact]
        public async void mtaTest1()
        {
            IConfig cfg = new Config(UNLIB_DLL, true);

            await mtaRun(() => 
            {
                using(dynamic l = new ConariX(cfg))
                {
                    l.set_VarSeven(2048);
                    Assert.Equal(2048, l.get_VarSeven<int>());

                    l.set_VarSeven(-8192);
                    Assert.Equal(-8192, l.get_VarSeven<int>());

                    l.bind<Action<int>>("set_VarSeven")(2048);
                    Assert.Equal(2048, l.bind<Func<int>>("get_VarSeven")());

                    l.bind<Action<int>>("set_VarSeven")(-8192);
                    Assert.Equal(-8192, l.bind<Func<int>>("get_VarSeven")());
                }
            }, 
            cfg);
        }
    }
}
