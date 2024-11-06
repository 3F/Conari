/*!
 * Copyright (c) 2016  Denis Kuzmin <x-3F@outlook.com> github/3F
 * Copyright (c) Conari contributors https://github.com/3F/Conari/graphs/contributors
 * Licensed under the MIT License (MIT).
 * See accompanying LICENSE.txt file or visit https://github.com/3F/Conari
*/

namespace net.r_eg.Conari.PE.WinNT
{
    using DWORD = System.UInt32;
    using WORD  = System.UInt16;

    /// <summary>
    /// Export Format
    /// /winnt.h
    /// </summary>
    public struct IMAGE_EXPORT_DIRECTORY
    {
        public DWORD Characteristics;
        public DWORD TimeDateStamp;
        public WORD MajorVersion;
        public WORD MinorVersion;
        public DWORD Name;
        public DWORD Base;
        public DWORD NumberOfFunctions;
        public DWORD NumberOfNames;
        public DWORD AddressOfFunctions;     // RVA from base of image
        public DWORD AddressOfNames;         // RVA from base of image
        public DWORD AddressOfNameOrdinals;  // RVA from base of image
    }
}