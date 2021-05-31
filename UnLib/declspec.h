#pragma once

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