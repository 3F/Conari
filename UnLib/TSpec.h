#pragma once

#include "UnLib.h"

namespace NS_UNLIB_
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
_NS_UNLIB