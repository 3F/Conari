#pragma once
#ifndef _UNLIB_TYPEDEFS_
#define _UNLIB_TYPEDEFS_

#include "stdafx.h"

namespace net::r_eg::Conari::UnLib
{

#ifdef UNLIB_UNICODE

    typedef std::wstring tstring;
    #define __toxlower towlower

    typedef wchar_t TCHAR;
    #ifndef _T
        #define _T(x)  L ## x
    #endif // !_T

#else

    typedef std::string tstring;
    #define __toxlower tolower

    typedef char TCHAR;
    #ifndef _T
        #define _T(x)  x
    #endif // !_T

#endif

    typedef size_t udiff_t;
    typedef ptrdiff_t diff_t;

    typedef unsigned int flagmeta_t;
    typedef unsigned int flagcfg_t;
    typedef unsigned char flagshort_t;

#if _UNLIB_FEATURE_MATCH_MAP
    typedef std::vector<udiff_t> matchmap_t;
#endif

}

#endif // _UNLIB_TYPEDEFS_