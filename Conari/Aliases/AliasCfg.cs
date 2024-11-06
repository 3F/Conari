/*!
 * Copyright (c) 2016  Denis Kuzmin <x-3F@outlook.com> github/3F
 * Copyright (c) Conari contributors https://github.com/3F/Conari/graphs/contributors
 * Licensed under the MIT License (MIT).
 * See accompanying LICENSE.txt file or visit https://github.com/3F/Conari
*/

namespace net.r_eg.Conari.Aliases
{
    public struct AliasCfg: IAliasCfg
    {
        /// <summary>
        /// Avoids prefix for right operand if it's defined.
        /// </summary>
        public bool NoPrefixR
        {
            get;
            set;
        }
    }
}
