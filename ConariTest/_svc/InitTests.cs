/*!
 * Copyright (c) 2016  Denis Kuzmin <x-3F@outlook.com> github/3F
 * Copyright (c) Conari contributors https://github.com/3F/Conari/graphs/contributors
 * Licensed under the MIT License (MIT).
 * See accompanying LICENSE.txt file or visit https://github.com/3F/Conari
*/

using System.IO;
using net.r_eg.Conari.Core;
using Xunit.Abstractions;
using Xunit.Sdk;

[assembly: Xunit.TestFramework
(
    nameof(ConariTest) + "." + nameof(ConariTest._svc) + "." + nameof(ConariTest._svc.InitTests),
    nameof(ConariTest)
)]

namespace ConariTest._svc
{
    using static _svc.TestHelper;

    public class InitTests: XunitTestFramework
    {
        public InitTests(IMessageSink messageSink)
            : base(messageSink)
        {
            Unset(Loader.TempDstPath)
                .Unset(TempDstPath);
        }

        private InitTests Unset(string path)
        {
            if(Directory.Exists(path)) {
                Directory.Delete(path, true);
            }
            return this;
        }
    }
}
