#pragma once

#include "UnLib.h"

namespace NS_UNLIB_API_
{

    /* basic */

    LIBAPI bool get_True();
    LIBAPI bool get_False();
    LIBAPI unsigned short int get_Seven();
    LIBAPI const char* get_HelloWorld();


    /* mangling */

    LIBAPI_CPP bool getD_True();
    LIBAPI_CPP unsigned short int getD_Seven();
    LIBAPI_CPP const char* getD_HelloWorld();

    LIBAPI unsigned short int __stdcall get_SevenStdCall();
    LIBAPI unsigned short int __fastcall get_SevenFastCall();
    LIBAPI unsigned short int __vectorcall get_SevenVectorCall();

    /* echo */

    LIBAPI bool get_BoolVal(bool v);
    LIBAPI int get_IntVal(int v);
    LIBAPI void* get_GPtrVal(void* pdata);

    LIBAPI std::string* get_StringPtrVal(std::string* str);
    LIBAPI std::wstring* get_WStringPtrVal(std::wstring* wstr);
    LIBAPI const char* get_CharPtrVal(const char* str);
    LIBAPI const wchar_t* get_WCharPtrVal(const wchar_t* wstr);
    LIBAPI const BSTR* get_BSTRVal(const BSTR* bstr);

    LIBAPI_CPP std::string get_StringVal(std::string str);
    LIBAPI_CPP std::wstring get_WStringVal(std::wstring wstr);

    /* complex */

    LIBAPI TSpec* get_TSpec();
    LIBAPI TSpecB* get_TSpecB_A_ptr();
    LIBAPI TSpecB* get_g_TSpecB();

    //LIBAPI TSpec* get_TSpec(BYTE a, int b, const char* name);
    //LIBAPI TSpecC* get_TSpecC_A_val();

    //LIBAPI TSpecA get_TSpecA();
    //LIBAPI TSpecA get_TSpecA(int a, int b);


    /* service */

    LIBSVC void svcFreeAll();
    LIBSVC void svcFree(void* ptr);
    LIBSVC void svcFreeArr(void* ptr);
}
_NS_UNLIB_API