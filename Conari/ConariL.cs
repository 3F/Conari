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
using System.Runtime.InteropServices;
using net.r_eg.Conari.Core;
using net.r_eg.Conari.Log;
using net.r_eg.Conari.Native;
using net.r_eg.Conari.Native.Core;
using net.r_eg.Conari.Resources;
using net.r_eg.Conari.Types;

namespace net.r_eg.Conari
{
    /// <summary>
    /// Conari engine. An unmanaged memory, modules, and raw data in one-touch.
    /// </summary>
    /// <remarks>https://github.com/3F/Conari</remarks>
    public class ConariL: ConariL<TCharPtr>, IConari
    {
        /// <summary>
        /// Wrapper to use both runtime dynamic (using access to <see cref="ConariL{TCharIn}.DLR"/>) 
        /// and compile type <see cref="ConariL"/>.
        /// </summary>
        /// <param name="l">Actual instance.</param>
        /// <param name="d">Use runtime dynamic type.</param>
        /// <returns>Use compile type instance.</returns>
        public static ConariL Make(ConariL l, out dynamic d)
        {
            d = (l ?? throw new ArgumentNullException(nameof(l))).DLR;
            return l;
        }

        /// <summary>
        /// Initialize Conari engine using the Cdecl calling convention.
        /// </summary>
        /// <remarks>*Cdecl - When the stack is cleaned up by the caller, it can do vararg functions.</remarks>
        /// <inheritdoc cref="ConariL(string, CallingConvention, string)"/>
        public ConariL(string library, string prefix = null)
            : base(library, prefix)
        {

        }

        /// <summary>
        /// Initialize Conari with specific calling convention.
        /// </summary>
        /// <param name="library">The library. Either full path or name with / without file extension.</param>
        /// <param name="conv">Use the calling convention.</param>
        /// <param name="prefix">Optional prefix to use via <see cref="Provider.bind{T}(string)"/> and related.</param>
        public ConariL(string library, CallingConvention conv, string prefix = null)
            : base(library, conv, prefix)
        {

        }

        /// <param name="library">The library. Either full path or name with / without file extension.</param>
        /// <param name="isolate">To isolate module for a real new loading when true. Details in <see cref="IConfig.IsolateLoadingOfModule"/>.</param>
        /// <param name="prefix">Optional prefix to use via <see cref="Provider.bind{T}(string)"/> and related.</param>
        /// <inheritdoc cref="ConariL(string, string)"/>
        public ConariL(string library, bool isolate, string prefix = null)
            : base(library, isolate, prefix)
        {

        }

        /// <param name="cfg">Use <see cref="IConfig"/> configuration.</param>
        /// <param name="prefix">Optional prefix to use via <see cref="Provider.bind{T}(string)"/> and related.</param>
        /// <inheritdoc cref="ConariL(string, string)"/>
        public ConariL(IConfig cfg, string prefix = null)
            : base(cfg, prefix)
        {

        }

        /// <param name="cfg">Use <see cref="IConfig"/> configuration.</param>
        /// <param name="conv">Use the calling convention.</param>
        /// <param name="prefix">Optional prefix to use via <see cref="Provider.bind{T}(string)"/> and related.</param>
        /// <inheritdoc cref="ConariL(string, CallingConvention, string)"/>
        public ConariL(IConfig cfg, CallingConvention conv, string prefix = null)
            : base(cfg, conv, prefix)
        {

        }
    }

    /// <inheritdoc cref="ConariL"/>
    public class ConariL<TCharIn>: Provider, 
                                    IConari<TCharIn>, 
                                    ILoader, 
                                    IProvider, 
                                    IBinder, 
                                    IDlrAccessor, 
                                    INativeAccessor, 
                                    IStringMaker<TCharIn>, 
                                    IDisposable
        where TCharIn: struct
    {
        protected IConfig config;

        protected readonly Lazy<NativeStringManager<TCharIn>> strings = new(() => new());

        protected Lazy<dynamic> _dlrCdecl, _dlrStd, _dlrFast, _dlrVector;

        public INativeStringManager<TCharIn> Strings => strings.Value;

        public IProviderDLR ConfigDLR => (IProviderDLR)DLR;

        public dynamic DLR { get; protected set; }

        public dynamic _ => DLR;

        /// <summary>
        /// Access to unmanaged and binary data through flexible chains.
        /// </summary>
        public NativeData Native { get; protected set; }

        /// <summary>
        /// Raw access to unmanaged memory.
        /// </summary>
        public IAccessor Memory { get; protected set; }

        /// <remarks>Alias to <see cref="ExVar"/></remarks>
        /// <inheritdoc cref="ExVar"/>
        public IExVar V => ExVar;

        /// <summary>
        /// Access to logger and its events.
        /// </summary>
        public ISender Log => LSender._;

        private protected override LLConfig LLCfg { get; set; }

        /// <summary>
        /// DLR Features using `__cdecl` calling convention.
        /// </summary>
        public dynamic __cdecl => _dlrCdecl.Value;

        /// <summary>
        /// DLR Features using `__stdcall` calling convention.
        /// </summary>
        public dynamic __stdcall => _dlrStd.Value;

        /// <summary>
        /// DLR Features using `__fastcall` calling convention.
        /// </summary>
        public dynamic __fastcall => _dlrFast.Value;

        /// <summary>
        /// DLR Features using `__vectorcall` calling convention.
        /// https://msdn.microsoft.com/en-us/library/dn375768.aspx
        /// </summary>
        public dynamic __vectorcall => _dlrVector.Value;

        /// <inheritdoc cref="ConariL.Make(ConariL, out dynamic)"/>
        public static ConariL<TCharIn> Make(ConariL<TCharIn> l, out dynamic d)
        {
            d = (l ?? throw new ArgumentNullException(nameof(l))).DLR;
            return l;
        }

        public IntPtr _T(string input, int extend) => Strings._T(input, extend);

        public IntPtr _T(string input) => Strings._T(input);

        public IntPtr _T<Tin>(string input, int extend) where Tin : struct
            => Strings._T<Tin>(input, extend);

        public IntPtr _T<Tin>(string input) where Tin : struct
            => Strings._T<Tin>(input);

        public IntPtr _T(string input, int extend, out TCharIn access)
            => Strings._T(input, extend, out access);

        public IntPtr _T(string input, out TCharIn access)
            => Strings._T(input, out access);

        public IntPtr _T<Tin>(string input, int extend, out Tin access) where Tin : struct
            => Strings._T<Tin>(input, extend, out access);

        public IntPtr _T<Tin>(string input, out Tin access) where Tin : struct
            => Strings._T<Tin>(input, out access);

        /// <inheritdoc cref="ConariL(IConfig, CallingConvention, string)"/>
        public ConariL(IConfig cfg, CallingConvention conv, string prefix = null)
        {
            Prefix      = prefix;
            Convention  = conv;

            init(cfg);
            ConventionChanged += onConventionChanged;
        }

        /// <inheritdoc cref="ConariL(IConfig, string)"/>
        public ConariL(IConfig cfg, string prefix = null)
            : this(cfg, CallingConvention.Cdecl, prefix)
        {

        }

        /// <inheritdoc cref="ConariL(string, CallingConvention, string)"/>
        public ConariL(string library, CallingConvention conv, string prefix = null)
            : this((Config)library, conv, prefix)
        {

        }

        /// <inheritdoc cref="ConariL(string, string)"/>
        public ConariL(string library, string prefix = null)
            : this((Config)library, prefix)
        {

        }

        /// <inheritdoc cref="ConariL(string, bool, string)"/>
        public ConariL(string library, bool isolate, string prefix = null)
            : this(new Config(library, isolate), prefix)
        {

        }

        protected void init(IConfig cfg)
        {
            config      = cfg ?? throw new ArgumentNullException(nameof(cfg));
            Mangling    = cfg.Mangling;
            LLCfg       = new(cfg);

            if(cfg.LazyLoading) {
                Library = new Link(cfg.Module);
            }
            else {
                load(cfg.Module);
            }

            Memory = new Memory((VPtr)this);
            Native = new NativeData(Memory);

            DLR = newDLR(Convention);

            _dlrCdecl   = new Lazy<dynamic>(() => newDLR(CallingConvention.Cdecl));
            _dlrStd     = new Lazy<dynamic>(() => newDLR(CallingConvention.StdCall));
            _dlrFast    = new Lazy<dynamic>(() => newDLR(CallingConvention.FastCall));
            _dlrVector  = new Lazy<dynamic>(() => throw new NotSupportedException(Msg.NotYetImpl));
        }

        protected override void Dispose(bool disposing)
        {
            if(strings.IsValueCreated) {
                strings.Value.Dispose();
            }
            base.Dispose(disposing);
        }

        private dynamic newDLR(CallingConvention conv)
        {
            return new ProviderDLR(this, conv) {
                Cache = config.CacheDLR
            };
        }

        private void onConventionChanged(object sender, DataArgs<CallingConvention> e)
        {
            DLR = newDLR(e.Data);
            LSender.Send(sender, $"DLR has been updated with new CallingConvention: {e.Data}", Message.Level.Info);
        }
    }
}
