/*
 * The MIT License (MIT)
 *
 * Copyright (c) 2016-2021  Denis Kuzmin <x-3F@outlook.com> github/3F
 * Copyright (c) Conari contributors https://github.com/3F/Conari/graphs/contributors
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
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

        internal LLConfig(IConfig cfg)
        {
            if(cfg == null) throw new ArgumentNullException(nameof(cfg));

            tryIsolateModule        = cfg.IsolateLoadingOfModule;
            moduleIsolationRecipe   = cfg.ModuleIsolationRecipe;
            cancelIfCantIsolate     = cfg.CancelIfCantIsolate;
            cts                     = cfg.Cts;
            loaderSyncLimit         = cfg.LoaderSyncLimit ?? SIG_LIM;
        }

        internal LLConfig()
        {

        }
    }
}
