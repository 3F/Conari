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
using System.Collections.Generic;
using net.r_eg.Conari.Core;
using net.r_eg.Conari.Log;

namespace net.r_eg.Conari.Aliases
{
    internal sealed class AliasDict
    {
        private readonly Provider provider;

        /// <summary>
        /// The aliases for exported-functions and variables.
        /// </summary>
        public Dictionary<string, ProcAlias> Aliases { get; } = new();

        /// <summary>
        /// Try to use alias.
        /// </summary>
        /// <param name="lpProcName"></param>
        /// <returns></returns>
        public string use(LpProcName lpProcName)
        {
            if(Aliases == null || lpProcName.origin == null
                || !Aliases.ContainsKey(lpProcName.origin))
            {
                return (string)lpProcName;
            }

            IAlias als = Aliases[lpProcName.origin];
            string ret;

            if(als.Cfg != null && als.Cfg.NoPrefixR) {
                ret = als.Name;
            }
            else {
                ret = provider.procName(als.Name);
            }

            LSender.Send(this, $"Use alias '{ret}' instead of '{(string)lpProcName}'", Message.Level.Info);
            return ret;
        }

        public AliasDict(Provider provider)
        {
            this.provider = provider ?? throw new ArgumentNullException(nameof(provider));
        }
    }
}
