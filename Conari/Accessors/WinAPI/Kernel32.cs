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

namespace net.r_eg.Conari.Accessors.WinAPI
{
    /// <summary>
    /// kernel32 via Conari engine [DLR version]: 
    /// https://github.com/3F/Conari
    /// https://docs.microsoft.com/en-us/windows/win32/api/
    /// </summary>
    public sealed class Kernel32: ConariX
    {
        private const CallingConvention __v_conv = CallingConvention.Winapi;
        private const string __v_module = "kernel32";

        public override CallingConvention Convention
        {
            get => __v_conv;
            set => throw new NotSupportedException();
        }

        /// <summary>
        /// Initialize kernel32 via Conari engine.
        /// </summary>
        /// <param name="cfg">Custom configuration. Module cannot be overridden.</param>
        public Kernel32(IConfig cfg)
            : base(DefConf(cfg), __v_conv, null)
        {

        }

        /// <summary>
        /// Initialize kernel32 via Conari engine.
        /// </summary>
        public Kernel32()
            : this(DefConf())
        {

        }

        private static IConfig DefConf(IConfig cfg = null)
        {
            if(cfg == null) {
                cfg = new Config();
            }

            cfg.Module = __v_module;
            return cfg;
        }
    }
}
