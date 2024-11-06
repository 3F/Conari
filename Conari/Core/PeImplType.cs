/*!
 * Copyright (c) 2016  Denis Kuzmin <x-3F@outlook.com> github/3F
 * Copyright (c) Conari contributors https://github.com/3F/Conari/graphs/contributors
 * Licensed under the MIT License (MIT).
 * See accompanying LICENSE.txt file or visit https://github.com/3F/Conari
*/

namespace net.r_eg.Conari.Core
{
    public enum PeImplType
    {
        Default,

        /// <summary>
        /// Use <see cref="Native.Core.Memory"/> implementation.
        /// </summary>
        Memory,

        /// <summary>
        /// Use <see cref="Native.Core.NativeStream"/> implementation.
        /// </summary>
        NativeStream,

        /// <summary>
        /// It will disable all related to PE features such as mangling, list of exported proc, etc.
        /// </summary>
        Disabled,
    }
}
