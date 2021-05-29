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
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using net.r_eg.Conari.Extension;

namespace net.r_eg.Conari.Core
{
    public sealed class PAddrCache<T>: ConcurrentDictionary<PAddrCache<T>.Key, T>
    {
        public T this[IntPtr addr, CallingConvention conv, Type sig, int hash = 0]
        {
            get => this[k(addr, conv, sig, hash)];
            set => this[k(addr, conv, sig, hash)] = value;
        }

        internal Key k(IntPtr addr, CallingConvention conv, Type sig, int hash = 0)
                => new(addr, conv, sig, hash);

        public struct Key
        {
            public IntPtr addr;
            public CallingConvention convention;
            public Type signature;
            public int hash;

            public Key(IntPtr ptr, CallingConvention conv, Type signature, int hash = 0)
            {
                addr = ptr;
                convention = conv;
                this.signature = signature;
                this.hash = hash;
            }
        }

        private class EqTypeArrayComparer: IEqualityComparer<Key>
        {
            public bool Equals(Key a, Key b)
                => (a.addr == b.addr)
                    && (a.convention == b.convention)
                    && (a.hash == b.hash)
                    && (a.signature == b.signature);

            public int GetHashCode(Key obj) => 0.CalculateHashCode
            (
                obj.addr,
                obj.convention,
                obj.hash,
                obj.signature
            );
        }

        internal PAddrCache()
            : base(new EqTypeArrayComparer())
        {

        }
    }
}
