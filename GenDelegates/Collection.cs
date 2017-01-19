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
using System.Linq;

namespace net.r_eg.GenDelegates
{
    public static class Collection
    {
        private static Gen gen = new Gen();

        public enum Modifiers
        {
            Out,
            Ref
        }

        public enum TAccess
        {
            Public,
            Internal,
            Protected,
            Private,
            PrivateHidden
        }

        public static IEnumerable<string> fmt(IEnumerable<string> sig, TAccess type, string postfix = "", string prefix = "")
        {
            if(type == TAccess.PrivateHidden) {
                return sig.Select(s => $"{prefix}{s}{postfix}");
            }

            string acc = type.ToString().ToLower();
            return sig.Select(s => $"{prefix}{acc} {s}{postfix}");
        }

        public static IEnumerable<string> getV1(string name, int maxgroups, Modifiers mod, bool hasResult)
        {
            var ret     = new List<string>();
            string LF   = Environment.NewLine;
            string sMod = mod.ToString().ToLower();

            for(int i = 1; i <= maxgroups; ++i)
            {
                if(i > 1) {
                    ret.Add($"{LF}/* {i} param-group */{LF}");
                }

                var sig = gen.signatures(name, i, sMod, hasResult);
                ret.AddRange(fmt(sig, TAccess.Public, LF));
            }

            return ret;
        }
    }
}
