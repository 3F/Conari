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
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;

namespace net.r_eg.Conari.Core
{
    [Serializable]
    public sealed class MICache: Dictionary<Type[], MethodInfo>
    {
        private class EqTypeArrayComparer: IEqualityComparer<Type[]>
        {
            public bool Equals(Type[] x, Type[] y)
            {
                if(x == null || y == null) {
                    return false;
                }

                if(x.Length != y.Length) {
                    return false;
                }

                for(int i = 0; i < x.Length; ++i) {
                    if(x[i] != y[i]) {
                        return false;
                    }
                }
                return true;
            }

            public int GetHashCode(Type[] obj)
            {
                int h = 0;
                for(int i = 0; i < obj.Length; ++i) {
                    unchecked {
                        h = (h << 5) + h ^ obj[i].GetHashCode();
                    }
                }

                return h;
            }
        }

        public MICache()
            : base(new EqTypeArrayComparer())
        {

        }

        private MICache(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {

        }
    }
}
