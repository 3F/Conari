/*!
 * Copyright (c) 2016  Denis Kuzmin <x-3F@outlook.com> github/3F
 * Copyright (c) Conari contributors https://github.com/3F/Conari/graphs/contributors
 * Licensed under the MIT License (MIT).
 * See accompanying LICENSE.txt file or visit https://github.com/3F/Conari
*/

using System;

namespace net.r_eg.Conari.Types
{
    /// <summary>
    /// ASCII character type alias.
    /// </summary>
    /// <remarks>Note: <see cref="char"/> may point either to <see cref="achar"/> or <see cref="wchar"/>.</remarks>
    [Serializable]
    public struct achar
    {
        private readonly char data;

        public static implicit operator char(achar v) => v.data;
        public static implicit operator achar(char v) => new(v);

        public achar(char input)
        {
            data = input;
        }
    }
}
