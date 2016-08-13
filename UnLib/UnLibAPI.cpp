#include "stdafx.h"
#include "UnLib.h"
#include "UnLibAPI.h"

namespace NS_UNLIB_API_
{

    /* basic */

    LIBAPI bool get_True()
    {
        return true;
    }

    LIBAPI unsigned short int get_Seven()
    {
        return 7;
    }

    LIBAPI const char* get_HelloWorld()
    {
        return "Hello World !";
    }


    /* mangling */

    LIBAPI_CPP bool getD_True()
    {
        return get_True();
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

    LIBAPI std::string* get_StringPtrVal(std::string* str)
    {
        return str;
    }

    LIBAPI std::wstring* get_WStringPtrVal(std::wstring* wstr)
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

    LIBAPI_CPP std::string get_StringVal(std::string str)
    {
        return str;
    }

    LIBAPI_CPP std::wstring get_WStringVal(std::wstring wstr)
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



    /* service */

    LIBSVC void freeAll()
    {
        g_sharedTSpec.reset();
        delete g_TSpecB->s; // TSpecB->TSpecA
        delete g_TSpecB;    // TSpecB
        // ...
    }

    LIBSVC void free(void* ptr)
    {
        delete ptr;
    }

    LIBSVC void free_arr(void* ptr)
    {
        delete[] ptr;
    }
}
_NS_UNLIB_API