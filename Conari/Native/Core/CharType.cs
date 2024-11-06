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
    public enum CharType
    {
        /// <summary>
        /// 8 bit unsigned character.
        /// </summary>
        OneByte,

        /// <summary>
        /// 16 bit unsigned character.
        /// </summary>
        TwoByte,

        /// <inheritdoc cref="OneByte"/>
        Ascii = OneByte,

        /// <inheritdoc cref="TwoByte"/>
        Unicode = TwoByte,
    }
}
