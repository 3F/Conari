/*
 * The MIT License (MIT)
 * 
 * Copyright (c) 2016  Denis Kuzmin <entry.reg@gmail.com>
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
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using net.r_eg.Conari.Core;
using net.r_eg.Conari.Log;

namespace net.r_eg.Conari
{
    [SuppressMessage("Microsoft.Design", "CA1063:ImplementIDisposableCorrectly", Justification = "Bug. False positive. The IDisposable is already implemented correctly !")]
    public class ConariL: Provider, IConari, ILoader, IProvider, IBinder, IDisposable
    {
        protected IConfig config;

        /// <summary>
        /// Access to available configuration data of dynamic DLR.
        /// </summary>
        public IProviderDLR ConfigDLR
        {
            get {
                return (IProviderDLR)DLR;
            }
        }

        /// <summary>
        /// Provides dynamic features like adding 
        /// and invoking of new exported-functions at runtime.
        /// </summary>
        public dynamic DLR
        {
            get;
            protected set;
        }

        /// <summary>
        /// Access to logger and its events.
        /// </summary>
        public ISender Log
        {
            get {
                return LSender._;
            }
        }

        /// <summary>
        /// DLR Features with `__cdecl` calling convention.
        /// </summary>
        public dynamic __cdecl
        {
            get
            {
                if(_dlrCdecl == null) {
                    _dlrCdecl = newDLR(CallingConvention.Cdecl);
                }
                return _dlrCdecl;
            }
        }
        protected dynamic _dlrCdecl;

        /// <summary>
        /// DLR Features with `__stdcall` calling convention.
        /// </summary>
        public dynamic __stdcall
        {
            get
            {
                if(_dlrStd == null) {
                    _dlrStd = newDLR(CallingConvention.StdCall);
                }
                return _dlrStd;
            }
        }
        protected dynamic _dlrStd;

        /// <summary>
        /// DLR Features with `__fastcall` calling convention.
        /// </summary>
        public dynamic __fastcall
        {
            get {
                if(_dlrFast == null) {
                    _dlrFast = newDLR(CallingConvention.FastCall);
                }
                return _dlrFast;
            }
        }
        protected dynamic _dlrFast;

        /// <summary>
        /// DLR Features with `__vectorcall` calling convention.
        /// https://msdn.microsoft.com/en-us/library/dn375768.aspx
        /// </summary>
        public dynamic __vectorcall
        {
            get {
                throw new NotSupportedException("Not yet implemented.");
            }
        }

        /// <summary>
        /// The Conari with specific calling convention.
        /// </summary>
        /// <param name="cfg">The Conari configuration.</param>
        /// <param name="conv">How should call methods.</param>
        /// <param name="prefix">Optional prefix to use via `bind&lt;&gt;`</param>
        public ConariL(IConfig cfg, CallingConvention conv, string prefix = null)
        {
            Prefix      = prefix;
            Convention  = conv;

            init(cfg);
            ConventionChanged += onConventionChanged;
        }

        /// <summary>
        /// The Conari with Cdecl - When the stack is cleaned up by the caller, it can do vararg functions.
        /// </summary>
        /// <param name="cfg">The Conari configuration.</param>
        /// <param name="prefix">Optional prefix to use via `bind&lt;&gt;`</param>
        public ConariL(IConfig cfg, string prefix = null)
            : this(cfg, CallingConvention.Cdecl, prefix)
        {

        }
        
        /// <summary>
        /// The Conari with specific calling convention.
        /// </summary>
        /// <param name="lib">The library.</param>
        /// <param name="conv">How should call methods.</param>
        /// <param name="prefix">Optional prefix to use via `bind&lt;&gt;`</param>
        public ConariL(string lib, CallingConvention conv, string prefix = null)
            : this((Config)lib, conv, prefix)
        {

        }

        /// <summary>
        /// The Conari with the calling convention by default.
        /// </summary>
        /// <param name="lib">The library.</param>
        /// <param name="prefix">Optional prefix to use via `bind&lt;&gt;`</param>
        public ConariL(string lib, string prefix = null)
            : this((Config)lib, prefix)
        {

        }

        protected void init(IConfig cfg)
        {
            if(cfg == null) {
                throw new ArgumentNullException("Configuration cannot be null.");
            }

            config      = cfg;
            Mangling    = cfg.Mangling;

            if(cfg.LazyLoading) {
                Library = new Link(cfg.LibName);
            }
            else {
                load(cfg.LibName);
            }

            DLR = newDLR(Convention);
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
