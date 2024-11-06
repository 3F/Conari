/*!
 * Copyright (c) 2016  Denis Kuzmin <x-3F@outlook.com> github/3F
 * Copyright (c) Conari contributors https://github.com/3F/Conari/graphs/contributors
 * Licensed under the MIT License (MIT).
 * See accompanying LICENSE.txt file or visit https://github.com/3F/Conari
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
