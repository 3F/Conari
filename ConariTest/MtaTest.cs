/*!
 * Copyright (c) 2016  Denis Kuzmin <x-3F@outlook.com> github/3F
 * Copyright (c) Conari contributors https://github.com/3F/Conari/graphs/contributors
 * Licensed under the MIT License (MIT).
 * See accompanying LICENSE.txt file or visit https://github.com/3F/Conari
*/

using System;
using net.r_eg.Conari;
using net.r_eg.Conari.Core;
using Xunit;

namespace ConariTest
{
    using static _svc.TestHelper;

    public class MtaTest
    {
        /// <summary>
        /// The test is considered successful if there were no collisions during the entire isolation period.
        /// </summary>
        [Fact]
        public async void mtaTest1()
        {
            IConfig cfg = new Config(UNLIB_DLL, true);

            await MtaRun(() => 
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
