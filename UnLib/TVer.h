#pragma once
/*!
 * Copyright (c) 2016  Denis Kuzmin <x-3F@outlook.com> github/3F
 * Copyright (c) Conari contributors https://github.com/3F/Conari/graphs/contributors
 * Licensed under the MIT License (MIT).
 * See accompanying LICENSE.txt file or visit https://github.com/3F/Conari
*/

class TVer
{
public:
    int major;
    int minor;
    int patch;

    TVer(int major, int minor, int patch)
        : major(major), minor(minor), patch(patch)
    {

    }
};

template<typename T>
class TVerTpl: public TVer
{

};