#pragma once
/*!
 * Copyright (c) 2016  Denis Kuzmin <x-3F@outlook.com> github/3F
 * Copyright (c) Conari contributors https://github.com/3F/Conari/graphs/contributors
 * Licensed under the MIT License (MIT).
 * See accompanying LICENSE.txt file or visit https://github.com/3F/Conari
*/

#ifdef UNLIB_AS_DLL

    #if defined(UNLIB_EXPORT)

        #define LIBAPI_CPP  __declspec(dllexport) // to decorate like ?getSeven@common@UnLib@Conari@r_eg@net@@YAGXZ
                                                  //                  unsigned short net::r_eg::Conari::UnLib::common::getSeven(void)
        #define LIBAPI  extern "C" __declspec(dllexport)

    //#elif defined(UNLIB_C_EXPORT)

    //    #define LIBAPI  extern "C" __declspec(dllexport)

    #else

        #define LIBAPI  __declspec(dllimport)
        #define LIBAPI_CPP  LIBAPI

    #endif

#else

    #define LIBAPI  extern
    #define LIBAPI_CPP  LIBAPI

#endif

#define LIBSVC LIBAPI