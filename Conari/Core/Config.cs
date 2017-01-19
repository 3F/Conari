/*
 * The MIT License (MIT)
 * 
 * Copyright (c) 2016-2017  Denis Kuzmin <entry.reg@gmail.com>
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Conari"), to deal
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

namespace net.r_eg.Conari.Core
{
    public struct Config: IConfig
    {
        /// <summary>
        /// The library.
        /// </summary>
        public string LibName
        {
            get;
            set;
        }

        /// <summary>
        /// To use `commit` methods for end calling.
        /// TODO:
        /// </summary>
        public bool TransactionStrategy
        {
            get;
            set;
        }

        /// <summary>
        /// To load library only when it required.
        /// </summary>
        public bool LazyLoading
        {
            get;
            set;
        }

        /// <summary>
        /// To cache dynamic types.
        /// </summary>
        public bool CacheDLR
        {
            get;
            set;
        }

        /// <summary>
        /// Auto name-decoration to find entry points of exported functions.
        /// </summary>
        public bool Mangling
        {
            get;
            set;
        }

        public static explicit operator String(Config cfg)
        {
            return cfg.LibName;
        }

        public static explicit operator Config(String lib)
        {
            return new Config(lib);
        }

        /// <param name="lib">The library.</param>
        public Config(string lib)
            : this()
        {
            LibName     = lib;
            CacheDLR    = true;
            Mangling    = true;
        }
    }
}
