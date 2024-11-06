#pragma once
/*!
 * Copyright (c) 2016  Denis Kuzmin <x-3F@outlook.com> github/3F
 * Copyright (c) Conari contributors https://github.com/3F/Conari/graphs/contributors
 * Licensed under the MIT License (MIT).
 * See accompanying LICENSE.txt file or visit https://github.com/3F/Conari
*/

#include "UnLib.h"

namespace net::r_eg::Conari::UnLib
{

    struct TSpec
    {
        BYTE a;
        int b;
        char* name;
    };

    struct TSpecA
    {
        int a;
        int b;
    };

    struct TSpecB
    {
        bool d;
        TSpecA* s;
    };

    struct TSpecC
    {
        bool d;
        TSpecA s;
    };


}