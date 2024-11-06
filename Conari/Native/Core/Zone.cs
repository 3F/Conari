/*!
 * Copyright (c) 2016  Denis Kuzmin <x-3F@outlook.com> github/3F
 * Copyright (c) Conari contributors https://github.com/3F/Conari/graphs/contributors
 * Licensed under the MIT License (MIT).
 * See accompanying LICENSE.txt file or visit https://github.com/3F/Conari
*/

using System;

namespace net.r_eg.Conari.Native.Core
{
    [Serializable]
    public enum Zone
    {
        /// <summary>
        /// Initial state of the used position in a specified container.
        /// </summary>
        Initial,

        /// <summary>
        /// The beginning of active area in container.
        /// </summary>
        Region,

        /// <summary>
        /// Current position in a specific region.
        /// </summary>
        Current,

        /// <inheritdoc cref="Initial"/>
        D = Initial,

        /// <inheritdoc cref="Region"/>
        U = Region,

        /// <inheritdoc cref="Current"/>
        V = Current,
    }
}
