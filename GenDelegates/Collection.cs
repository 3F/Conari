/*!
 * Copyright (c) 2016  Denis Kuzmin <x-3F@outlook.com> github/3F
 * Copyright (c) Conari contributors https://github.com/3F/Conari/graphs/contributors
 * Licensed under the MIT License (MIT).
 * See accompanying LICENSE.txt file or visit https://github.com/3F/Conari
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
