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


    /* decorated */

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

    LIBAPI std::string get_StringVal(std::string str)
    {
        return str;
    }

    LIBAPI std::wstring get_WStringVal(std::wstring wstr)
    {
        return wstr;
    }


    /* complex */


}
_NS_UNLIB_API