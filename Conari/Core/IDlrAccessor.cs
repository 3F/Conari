/*!
 * Copyright (c) 2016  Denis Kuzmin <x-3F@outlook.com> github/3F
 * Copyright (c) Conari contributors https://github.com/3F/Conari/graphs/contributors
 * Licensed under the MIT License (MIT).
 * See accompanying LICENSE.txt file or visit https://github.com/3F/Conari
*/

namespace net.r_eg.Conari.Core
{
    public interface IDlrAccessor
    {
        /// <summary>
        /// Provides dynamic features at runtime.
        /// </summary>
        dynamic DLR { get; }

        /// <summary>
        /// Alias to <see cref="DLR"/>.
        /// </summary>
        dynamic _ { get; }
    }
}
