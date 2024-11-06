/*!
 * Copyright (c) 2016  Denis Kuzmin <x-3F@outlook.com> github/3F
 * Copyright (c) Conari contributors https://github.com/3F/Conari/graphs/contributors
 * Licensed under the MIT License (MIT).
 * See accompanying LICENSE.txt file or visit https://github.com/3F/Conari
*/

#include "stdafx.h"
#include "UnLib.h"
#include "common.h"

namespace net::r_eg::Conari::UnLib::mem
{
    using namespace std;

    LIBAPI bool updateTCharPtr(TCHAR* input, const TCHAR* word)
    {
        if(input == nullptr)
        {
            return false;
        }

        tstring _word = tstring(word);

        if(strlen(input) < _word.length())
        {
            return false;
        }

        std::copy(_word.begin(), _word.end(), input);
        input[_word.length()] = _T('\0');

        return true;
    }
}