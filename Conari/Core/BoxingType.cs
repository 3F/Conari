/*!
 * Copyright (c) 2016  Denis Kuzmin <x-3F@outlook.com> github/3F
 * Copyright (c) Conari contributors https://github.com/3F/Conari/graphs/contributors
 * Licensed under the MIT License (MIT).
 * See accompanying LICENSE.txt file or visit https://github.com/3F/Conari
*/

using System;

namespace net.r_eg.Conari.Core
{
    [Flags]
    public enum BoxingType
    {
        None,

        Unboxing = 0x1,

        Boxing = 0x2,

        UnboxingAndBoxing = Unboxing | Boxing,
    }
}
