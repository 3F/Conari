/*!
 * Copyright (c) 2016  Denis Kuzmin <x-3F@outlook.com> github/3F
 * Copyright (c) Conari contributors https://github.com/3F/Conari/graphs/contributors
 * Licensed under the MIT License (MIT).
 * See accompanying LICENSE.txt file or visit https://github.com/3F/Conari
*/

using System;
using net.r_eg.Conari.PE;

namespace net.r_eg.Conari.Core
{
    public interface ILoader
    {
        /// <summary>
        /// Before unloading a library.
        /// </summary>
        event EventHandler<DataArgs<Link>> BeforeUnload;

        /// <summary>
        /// When library has been unloaded.
        /// </summary>
        event EventHandler<DataArgs<Link>> AfterUnload;

        /// <summary>
        /// When library has been loaded.
        /// </summary>
        event EventHandler<DataArgs<Link>> AfterLoad;

        /// <summary>
        /// Active library.
        /// </summary>
        Link Library { get; }

        /// <summary>
        /// PE32/PE32+ features.
        /// </summary>
        IPE PE { get; }
    }
}
