#pragma once

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