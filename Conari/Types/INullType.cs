/*!
 * Copyright (c) 2016  Denis Kuzmin <x-3F@outlook.com> github/3F
 * Copyright (c) Conari contributors https://github.com/3F/Conari/graphs/contributors
 * Licensed under the MIT License (MIT).
 * See accompanying LICENSE.txt file or visit https://github.com/3F/Conari
*/

using System;

namespace net.r_eg.Conari.Types
{
    public interface INullType
    {
        /// <summary>
        /// To get/set encapsulated data.
        /// </summary>
        dynamic Data { get; set; }

        /// <summary>
        /// To get base type.
        /// </summary>
        Type GenericType { get; }
    }
}
