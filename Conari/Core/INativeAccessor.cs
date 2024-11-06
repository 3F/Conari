/*!
 * Copyright (c) 2016  Denis Kuzmin <x-3F@outlook.com> github/3F
 * Copyright (c) Conari contributors https://github.com/3F/Conari/graphs/contributors
 * Licensed under the MIT License (MIT).
 * See accompanying LICENSE.txt file or visit https://github.com/3F/Conari
*/

using net.r_eg.Conari.Native;
using net.r_eg.Conari.Native.Core;

namespace net.r_eg.Conari.Core
{
    public interface INativeAccessor
    {
        /// <summary>
        /// Access to unmanaged and binary data through flexible chains.
        /// </summary>
        NativeData Native { get; }

        /// <summary>
        /// Raw access to unmanaged memory.
        /// </summary>
        IAccessor Memory { get; }
    }
}
