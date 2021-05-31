#pragma once

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