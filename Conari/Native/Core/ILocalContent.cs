﻿/*!
 * Copyright (c) 2016  Denis Kuzmin <x-3F@outlook.com> github/3F
 * Copyright (c) Conari contributors https://github.com/3F/Conari/graphs/contributors
 * Licensed under the MIT License (MIT).
 * See accompanying LICENSE.txt file or visit https://github.com/3F/Conari
*/

namespace net.r_eg.Conari.Native.Core
{
    public interface ILocalContent: IAccessor
    {
        /// <summary>
        /// The length of the readable data.
        /// </summary>
        int Length { get; }

        /// <summary>
        /// Extends local data using additional bytes.
        /// </summary>
        /// <param name="bytes"></param>
        void extend(byte[] bytes);
    }
}
