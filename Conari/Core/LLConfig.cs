/*!
 * Copyright (c) 2016  Denis Kuzmin <x-3F@outlook.com> github/3F
 * Copyright (c) Conari contributors https://github.com/3F/Conari/graphs/contributors
 * Licensed under the MIT License (MIT).
 * See accompanying LICENSE.txt file or visit https://github.com/3F/Conari
*/

using System;
using System.Threading;

namespace net.r_eg.Conari.Core
{
    internal sealed class LLConfig
    {
        private const int SIG_LIM = 80_000; //ms

        /// <inheritdoc cref="IConfig.IsolateLoadingOfModule"/>
        public bool tryIsolateModule;

        /// <inheritdoc cref="IConfig.ModuleIsolationRecipe"/>
        public IModuleIsolationRecipe moduleIsolationRecipe;

        /// <inheritdoc cref="IConfig.CancelIfCantIsolate"/>
        public bool cancelIfCantIsolate;

        /// <inheritdoc cref="IConfig.Cts"/>
        public CancellationTokenSource cts;

        /// <inheritdoc cref="IConfig.LoaderSyncLimit"/>
        public int loaderSyncLimit;

        /// <inheritdoc cref="IConfig.PeImplementation"/>
        public PeImplType peImplementation;

        internal LLConfig(IConfig cfg)
        {
            if(cfg == null) throw new ArgumentNullException(nameof(cfg));

            tryIsolateModule        = cfg.IsolateLoadingOfModule;
            moduleIsolationRecipe   = cfg.ModuleIsolationRecipe;
            cancelIfCantIsolate     = cfg.CancelIfCantIsolate;
            cts                     = cfg.Cts;
            loaderSyncLimit         = cfg.LoaderSyncLimit ?? SIG_LIM;
            peImplementation        = cfg.PeImplementation;
        }

        internal LLConfig()
        {

        }
    }
}
