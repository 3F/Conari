/*!
 * Copyright (c) 2016  Denis Kuzmin <x-3F@outlook.com> github/3F
 * Copyright (c) Conari contributors https://github.com/3F/Conari/graphs/contributors
 * Licensed under the MIT License (MIT).
 * See accompanying LICENSE.txt file or visit https://github.com/3F/Conari
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
