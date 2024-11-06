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
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1069:Enums values should not be duplicated", Justification = "Due to aliases for the Default items")]
    public enum DynamicOptions
    {
        NoSet,

        DefaultNoCache = NoSet,

        Default = DefaultWithCaching,

        DefaultWithCaching = DefaultNoCache | Cache,

        Cache = 0x01,

        GenerateUsingBuilder = 0x02,
    }
}
