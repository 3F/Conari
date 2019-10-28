/*
 * The MIT License (MIT)
 *
 * Copyright (c) 2016-2019  Denis Kuzmin < entry.reg@gmail.com > GitHub/3F
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
using System.Linq;

namespace net.r_eg.GenDelegates
{
    public class Gen
    {
        protected struct TSig
        {
            // generic type
            public string sT;

            // argument list
            public string sP;

            // result type
            public string sR;

            // signature name
            public string sN;
        }

        /// <param name="name">The name of delegate.</param>
        /// <param name="args">Count of arguments.</param>
        /// <param name="modifier">Modifier of arguments, e.g.: out / ref</param>
        /// <param name="hasResult">void or generic type.</param>
        /// <returns>List of all possible signatures with combinations as `C1 + C2 + .. Cn` Where C = n! / (m! * (n - m)!)</returns>
        public List<string> signatures(string name, int args, string modifier, bool hasResult)
        {
            var ret = new List<string>();

            foreach(int[] sig in calc(args))
            {
                TSig _ = defTypesAndArgs(sig, args, modifier, hasResult);

                // result type

                _.sR = hasResult ? "TRes" : "void";

                // name

                string postfix = (args != sig.Length) ? String.Join("_", sig) : String.Empty;
                _.sN = name + postfix;

                // final line
                ret.Add($"delegate {_.sR} {_.sN}<{_.sT}>({_.sP});");
            }

            return ret;
        }
        
        /// <summary>
        /// Calculate combinations like = n! / (m! * (n - m)!)
        /// </summary>
        /// <param name="n"></param>
        /// <param name="m"></param>
        /// <returns>Available combinations via positive index, starting from 1.</returns>
        public List<int[]> calc(int n, int m)
        {
            var _seq = new List<int>();
            var comb = new List<int[]>();

            calc(comb, _seq, n, m);

            return comb;
        }

        /// <summary>
        /// Calculate combinations like = C1 + C2 + .. Cn
        /// Where C = n! / (m! * (n - m)!)
        /// </summary>
        /// <param name="n">max n elements</param>
        /// <returns>Available combinations via positive index, starting from 1.</returns>
        public List<int[]> calc(int n)
        {
            var comb = new List<int[]>();

            for(int m = 1; m <= n; ++m) {
                comb.AddRange(calc(n, m));
            }
            return comb;
        }

        protected virtual TSig defTypesAndArgs(int[] sig, int args, string modifier, bool hasResult)
        {
            var sigT = new List<string>();
            var sigP = new List<string>();

            for(int i = 1; i <= args; ++i)
            {
                string t = $"T{i}";
                string p = $"p{i}";

                if(sig.Contains(i)) {
                    sigT.Add(t);
                    sigP.Add($"{modifier} {t} {p}");
                    continue;
                }

                sigT.Add($"in {t}");
                sigP.Add($"{t} {p}");
            }

            if(hasResult) {
                sigT.Add($"out TRes");
            }

            return new TSig() {
                sT = String.Join(", ", sigT),
                sP = String.Join(", ", sigP)
            };
        }

        /// <summary>
        ///  C = n! / (m! * (n - m)!)
        /// </summary>
        /// <param name="comb">final combinations</param>
        /// <param name="seq">constructed pair</param>
        /// <param name="n">n or max elements</param>
        /// <param name="pair">m or length of pair per seq</param>
        /// <param name="left">starting from 1 by default</param>
        protected void calc(List<int[]> comb, List<int> seq, int n, int pair, int left = 1)
        {
            for(int i = left; i <= n; ++i)
            {
                seq.Add(i);

                if(pair > 1)
                {
                    --pair;
                    calc(comb, seq, n, pair, i + 1);
                    ++pair;

                    seq.RemoveAt(seq.Count - 1);
                    continue;
                }

                // release the combination
                comb.Add(seq.ToArray());
                seq.RemoveAt(seq.Count - 1);
            }
        }
    }
}
