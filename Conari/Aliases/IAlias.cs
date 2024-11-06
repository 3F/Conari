/*!
 * Copyright (c) 2016  Denis Kuzmin <x-3F@outlook.com> github/3F
 * Copyright (c) Conari contributors https://github.com/3F/Conari/graphs/contributors
 * Licensed under the MIT License (MIT).
 * See accompanying LICENSE.txt file or visit https://github.com/3F/Conari
*/

namespace net.r_eg.Conari.Aliases
{
    public interface IAlias
    {
        /// <summary>
        /// The final name.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Configuration of alias.
        /// </summary>
        IAliasCfg Cfg { get; }
    }
}
