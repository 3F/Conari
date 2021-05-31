#pragma once

#include "UnLib.h"

namespace net::r_eg::Conari::UnLib::common
{
    using namespace std;

    /* ref */

    LIBAPI void addRefVal(int a, int b, int* result);
    LIBAPI int retAddRefVal(int a, int b, int* result);

    /* cdecl varargs */

    LIBAPI void vararg2(int* a, int* b, int c = 0, int d = 0);
    LIBAPI int vararg2ret(int* a, int* b, int c = 0, int d = 0);

    /* basic */

    LIBAPI bool get_True();
    LIBAPI bool get_False();
    LIBAPI unsigned short int get_Seven();
    LIBAPI const char* get_HelloWorld();
    LIBAPI int get_VarSeven();
    LIBAPI void set_VarSeven(int v);
    LIBAPI void reset_VarSeven();

    /* naming */

    LIBAPI int GetMagicNum();
    LIBAPI int apiprefix_GetMagicNum();


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

    LIBAPI string* get_StringPtrVal(string* str);
    LIBAPI wstring* get_WStringPtrVal(wstring* wstr);
    LIBAPI const char* get_CharPtrVal(const char* str);
    LIBAPI const wchar_t* get_WCharPtrVal(const wchar_t* wstr);
    LIBAPI const BSTR* get_BSTRVal(const BSTR* bstr);

    LIBAPI_CPP string get_StringVal(string str);
    LIBAPI_CPP wstring get_WStringVal(wstring wstr);

    /* complex */

    LIBAPI TSpec* get_TSpec();
    LIBAPI TSpecB* get_TSpecB_A_ptr();
    LIBAPI TSpecB* get_g_TSpecB();

    //LIBAPI TSpec* get_TSpec(BYTE a, int b, const char* name);
    //LIBAPI TSpecC* get_TSpecC_A_val();

    //LIBAPI TSpecA get_TSpecA();
    //LIBAPI TSpecA get_TSpecA(int a, int b);

    /* strings */

    LIBAPI bool get_CharPtrCmpRef(const char& str1, const char* str2);
    LIBAPI bool get_WCharPtrCmpRef(const wchar_t& wstr1, const wchar_t* wstr2);

    LIBAPI bool get_StringPtrCmpRef(const string& str1, const string* str2);
    LIBAPI bool get_WStringPtrCmpRef(const wstring& wstr1, const wstring* wstr2);


    /* types */

    LIBAPI bool chkTypeTVer(TVer t, int major, int minor, int patch);
    LIBAPI bool chkTypeRefTVer(TVer& t, int major, int minor, int patch);

    // TODO: byte-packing etc. for support of template/generic types. ~TVerTpl<char>
    //LIBAPI_CPP bool chkWordForString(string str);
    //LIBAPI_CPP bool get_WStringVal(wstring wstr);

    /* service */

    LIBSVC void svcFreeAll();
    LIBSVC void svcFree(void* ptr);
    LIBSVC void svcFreeArr(void* ptr);
}