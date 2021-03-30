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
using net.r_eg.Conari.Types;

namespace net.r_eg.Conari.Core
{
    public struct Link
    {
        /// <summary>
        /// Used module (.dll, .exe, or address)
        /// </summary>
        public string module;

        /// <summary>
        /// Points to actual isolated module if true.
        /// </summary>
        public bool isolated;

        /// <summary>
        /// A handle of loaded module.
        /// </summary>
        internal IntPtr handle;

        /// <summary>
        /// An resolved file status of the used module.
        /// </summary>
        internal readonly WRef<bool> resolved;

        public bool IsActive => handle != IntPtr.Zero;

        [Obsolete("Use {module} field instead.")]
        public string LibName => module;

        public Link(IntPtr handle, string module, bool isolated = false)
            : this()
        {
            this.handle     = handle;
            this.module     = module;
            this.isolated   = isolated;

            resolved = new WRef<bool>(false);
        }

        public Link(string module)
            : this(IntPtr.Zero, module)
        {

        }
    }
}
