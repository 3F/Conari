#pragma once

#ifdef UNLIB_AS_DLL

    #if defined(UNLIB_EXPORT)

        #define LIBAPI_CPP  __declspec(dllexport) // will decorated like: ?getSeven@API@UnLib@Conari@r_eg@net@@YAGXZ
                                                  // e.g.: unsigned short net::r_eg::Conari::UnLib::API::getSeven(void)
        #define LIBAPI  extern "C" __declspec(dllexport) // will undecorated like from C compiler: getSeven()

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