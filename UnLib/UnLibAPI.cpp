#include "stdafx.h"
#include "UnLib.h"
#include "UnLibAPI.h"

namespace NS_UNLIB_API_
{
    using namespace std;

    /* basic */

    LIBAPI bool get_True()
    {
        return true;
    }

    LIBAPI bool get_False()
    {
        return false;
    }

    LIBAPI unsigned short int get_Seven()
    {
        return 7;
    }

    LIBAPI const char* get_HelloWorld()
    {
        return "Hello World !";
    }

    int _varSevenData = 7;
    LIBAPI int get_VarSeven()
    {
        return _varSevenData;
    }

    LIBAPI void set_VarSeven(int v)
    {
        _varSevenData = v;
    }

    LIBAPI void reset_VarSeven()
    {
        _varSevenData = -1;
    }

    /* naming */

    LIBAPI int GetMagicNum()
    {
        return -1;
    }

    LIBAPI int apiprefix_GetMagicNum()
    {
        return 4;
    }

    /* mangling */

    LIBAPI_CPP bool getD_True()
    {
        return get_True();
    }

    LIBAPI_CPP bool getD_True(bool flag)
    {
        return flag ? get_True() : get_False();
    }

    LIBAPI_CPP unsigned short int getD_Seven()
    {
        return get_Seven();
    }

    LIBAPI_CPP const char* getD_HelloWorld()
    {
        return get_HelloWorld();
    }

    LIBAPI unsigned short int __stdcall get_SevenStdCall()
    {
        return 7;
    }

    LIBAPI unsigned short int __fastcall get_SevenFastCall()
    {
        return 7;
    }

    LIBAPI unsigned short int __vectorcall get_SevenVectorCall()
    {
        return 7;
    }

    /* Exported-Variables */

    LIBAPI DWORD ADDR_SPEC                  = 0x00001CE8;
    LIBAPI bool apiprefix_GFlag             = false;
    LIBAPI_CPP const char* eVariableTest    = "Hello World!";


    /* echo */

    LIBAPI bool get_BoolVal(bool v)
    {
        return v;
    }

    LIBAPI int get_IntVal(int v)
    {
        return v;
    }

    LIBAPI void* get_GPtrVal(void* pdata)
    {
        return pdata;
    }

    LIBAPI string* get_StringPtrVal(string* str)
    {
        return str;
    }

    LIBAPI wstring* get_WStringPtrVal(wstring* wstr)
    {
        return wstr;
    }

    LIBAPI const char* get_CharPtrVal(const char* str)
    {
        return str;
    }

    LIBAPI const wchar_t* get_WCharPtrVal(const wchar_t* wstr)
    {
        return wstr;
    }

    LIBAPI const BSTR* get_BSTRVal(const BSTR* bstr)
    {
        return bstr;
    }

    LIBAPI_CPP string get_StringVal(string str)
    {
        return str;
    }

    LIBAPI_CPP wstring get_WStringVal(wstring wstr)
    {
        return wstr;
    }


    /* complex */

    std::shared_ptr<TSpec> g_sharedTSpec; // to check DLR, delegates and MI
    LIBAPI TSpec* get_TSpec()
    {
        if (g_sharedTSpec != nullptr) {
            return g_sharedTSpec.get();
        }

        auto s = std::make_shared<TSpec>();

        s->a    = 2;
        s->b    = 4;
        s->name = "Conari";

        g_sharedTSpec = s; //+1

        return s.get();
    }

    TSpecB* g_TSpecB;
    LIBAPI TSpecB* get_TSpecB_A_ptr()
    {
        TSpecB* B = new TSpecB();

        B->d = true;
        B->s = new TSpecA();
        B->s->a = 4;
        B->s->b = -8;

        // ! do not use the [delete] operator for TSpecA / TSpecB 
        //   it will be tested with IMem.free or other later !

        g_TSpecB = B;
        return B;
    }

    LIBAPI TSpecB* get_g_TSpecB()
    {
        return g_TSpecB;
    }

    /* strings */

    LIBAPI bool get_CharPtrCmpRef(const char& str1, const char* str2)
    {
        return str1 == *str2;
    }

    LIBAPI bool get_WCharPtrCmpRef(const wchar_t& wstr1, const wchar_t* wstr2)
    {
        return wstr1 == *wstr2;
    }

    LIBAPI bool get_StringPtrCmpRef(const string& str1, const string* str2)
    {
        return lstrcmpA((LPCSTR)&str1, ((LPCSTR)str2)) == 0;
    }

    LIBAPI bool get_WStringPtrCmpRef(const wstring& wstr1, const wstring* wstr2)
    {
        return lstrcmpW((LPCWSTR)&wstr1, ((LPCWSTR)wstr2)) == 0;
    }

    /* types */

    LIBAPI bool chkTypeTVer(TVer t, int major, int minor, int patch)
    {
        return t.major == major && t.minor == minor && t.patch == patch;
    }

    LIBAPI bool chkTypeRefTVer(TVer& t, int major, int minor, int patch)
    {
        return t.major == major && t.minor == minor && t.patch == patch;
    }

    /* service */

    LIBSVC void svcFreeAll()
    {
        g_sharedTSpec.reset();
        delete g_TSpecB->s; // TSpecB->TSpecA
        delete g_TSpecB;    // TSpecB
        // ...
    }

    // do not use `free(void* ptr)`: __scrt_dllmain_uninitialize_c() & __CRTDECL operator delete(void* block, size_t)
    LIBSVC void svcFree(void* ptr)
    {
        delete ptr;
    }

    LIBSVC void svcFreeArr(void* ptr)
    {
        delete[] ptr;
    }
}
_NS_UNLIB_API