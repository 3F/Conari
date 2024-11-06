/*!
 * Copyright (c) 2016  Denis Kuzmin <x-3F@outlook.com> github/3F
 * Copyright (c) Conari contributors https://github.com/3F/Conari/graphs/contributors
 * Licensed under the MIT License (MIT).
 * See accompanying LICENSE.txt file or visit https://github.com/3F/Conari
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
