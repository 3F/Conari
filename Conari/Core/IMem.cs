/*!
 * Copyright (c) 2016  Denis Kuzmin <x-3F@outlook.com> github/3F
 * Copyright (c) Conari contributors https://github.com/3F/Conari/graphs/contributors
 * Licensed under the MIT License (MIT).
 * See accompanying LICENSE.txt file or visit https://github.com/3F/Conari
*/

using System;

namespace net.r_eg.Conari.Core
{
    public interface IMem
    {
        /// <summary>
        /// To free memory from the heap allocated from the unmanaged memory.
        /// </summary>
        /// <param name="ptr">The address of the memory to be freed.</param>
        void free(IntPtr ptr);
    }
}
