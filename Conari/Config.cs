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
using net.r_eg.Conari.Core;
using net.r_eg.Conari.Resources;

namespace net.r_eg.Conari
{
    public class Config: IConfig
    {
        public string Module { get; set; }

        public bool TransactionStrategy
        {
            get => throw new NotImplementedException(Msg.not_yet_impl);
            set => throw new NotImplementedException(Msg.not_yet_impl);
        }

        public bool LazyLoading { get; set; }

        public virtual bool Cache { get; set; } = true;

        public virtual bool CacheDLR { get; set; } = true;

        public virtual bool Mangling { get; set; } = true;

        public bool IsolateLoadingOfModule { get; set; }

        public IModuleIsolationRecipe ModuleIsolationRecipe { get; set; }

        public bool CancelIfCantIsolate { get; set; }

        public CancellationTokenSource Cts { get; set; }

        public int? LoaderSyncLimit { get; set; }

        public virtual PeImplType PeImplementation { get; set; } = PeImplType.Memory;

        [Obsolete("Use {Module} property instead.")]
        public string LibName => Module;

        public static explicit operator string(Config cfg) => cfg?.Module;

        public static explicit operator Config(string lib) => new(lib);

        /// <param name="module">Initialize <see cref="Module"/>.</param>
        /// <param name="isolate">Initialize <see cref="IsolateLoadingOfModule"/>.</param>
        public Config(string module, bool isolate = false)
        {
            Module = module;
            IsolateLoadingOfModule = isolate;
        }

        public Config()
        {

        }
    }
}
