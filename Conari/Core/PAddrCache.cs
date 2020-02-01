/*
 * The MIT License (MIT)
 *
 * Copyright (c) 2016-2020  Denis Kuzmin < x-3F@outlook.com > GitHub/3F
 * Copyright (c) Conari contributors: https://github.com/3F/Conari/graphs/contributors
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
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace net.r_eg.Conari.Core
{
    [Serializable]
    public sealed class PAddrCache<T>: Dictionary<PAddrCache<T>.Key, T>
    {
        public struct Key
        {
            public readonly IntPtr addr;
            public CallingConvention convention;

            public Key(IntPtr ptr, CallingConvention conv)
            {
                addr = ptr;
                convention = conv;
            }
        }

        private class EqTypeArrayComparer: IEqualityComparer<Key>
        {
            public bool Equals(Key a, Key b)
            {
                if(a.convention != b.convention) {
                    return false;
                }

                if(a.addr != b.addr) {
                    return false;
                }

                return true;
            }

            public int GetHashCode(Key obj)
            {
                Func<int, int, int> polynom = delegate(int r, int x) {
                    unchecked {
                        return (r << 5) + r ^ x;
                    }
                };

                int h = 0;
                h = polynom(h, obj.addr.GetHashCode());
                h = polynom(h, obj.convention.GetHashCode());

                return h;
            }
        }

        public PAddrCache()
            : base(new EqTypeArrayComparer())
        {

        }

        private PAddrCache(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {

        }
    }
}
