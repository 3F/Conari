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

namespace net.r_eg.Conari
{
    [SuppressMessage("Microsoft.Design", "CA1063:ImplementIDisposableCorrectly", Justification = "Bug. False positive. The IDisposable is already implemented correctly !")]
    public class ConariL: Provider, IConari, ILoader, IProvider, IBinder/*, IDisposable*/
    {
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
        }

        /// <summary>
        /// The Conari with StdCall - When the callee cleans the stack.
        /// </summary>
        /// <param name="cfg">The Conari configuration.</param>
        /// <param name="prefix">Optional prefix to use via `bind&lt;&gt;`</param>
        public ConariL(IConfig cfg, string prefix = null)
            : this(cfg, CallingConvention.StdCall, prefix)
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
                throw new ArgumentException("Configuration cannot be null.");
            }

            if(cfg.LazyLoading) {
                Library = new Link(cfg.LibName);
            }
            else {
                load(cfg.LibName);
            }
        }
    }
}
