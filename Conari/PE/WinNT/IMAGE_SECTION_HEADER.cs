/*
 * The MIT License (MIT)
 * 
 * Copyright (c) 2016  Denis Kuzmin <entry.reg@gmail.com>
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
*/

namespace net.r_eg.Conari.PE.WinNT
{
    using BYTE  = System.Byte;
    using DWORD = System.UInt32;
    using WORD  = System.UInt16;

    /// <summary>
    /// https://msdn.microsoft.com/en-us/library/windows/desktop/ms680341.aspx
    /// /winnt.h
    /// </summary>
    public struct IMAGE_SECTION_HEADER
    {
        public const int IMAGE_SIZEOF_SHORT_NAME = 8;
        public const int IMAGE_SIZEOF_SECTION_HEADER = 40;

        /// <summary>
        /// An 8-byte, null-padded UTF-8 string. 
        /// There is no terminating null character if the string is exactly eight characters long. 
        /// </summary>
        public BYTE[] Name/*[IMAGE_SIZEOF_SHORT_NAME]*/;

        /// <summary>
        /// union { DWORD PhysicalAddress; DWORD VirtualSize; }
        /// 
        /// PhysicalAddress
        ///     The file address.
        ///     
        /// VirtualSize
        ///     The total size of the section when loaded into memory, in bytes. 
        ///     If this value is greater than the SizeOfRawData member, the section is filled with zeroes. 
        ///     This field is valid only for executable images and should be set to 0 for object files.
        /// </summary>
        public DWORD Misc;

        /// <summary>
        /// The address of the first byte of the section when loaded into memory, 
        /// relative to the image base. For object files, this is the address of the first byte 
        /// before relocation is applied.
        /// </summary>
        public DWORD VirtualAddress;

        /// <summary>
        /// The size of the initialized data on disk, in bytes. 
        /// 
        /// This value must be a multiple of the FileAlignment member of the IMAGE_OPTIONAL_HEADER structure. 
        /// If this value is less than the VirtualSize member, the remainder of the section is filled with zeroes. 
        /// If the section contains only uninitialized data, the member is zero.
        /// </summary>
        public DWORD SizeOfRawData;

        /// <summary>
        /// A file pointer to the first page within the COFF file. 
        /// 
        /// This value must be a multiple of the FileAlignment member of the IMAGE_OPTIONAL_HEADER structure. 
        /// If a section contains only uninitialized data, set this member is zero.
        /// </summary>
        public DWORD PointerToRawData;

        /// <summary>
        /// A file pointer to the beginning of the relocation entries for the section. 
        /// If there are no relocations, this value is zero.
        /// </summary>
        public DWORD PointerToRelocations;

        /// <summary>
        /// A file pointer to the beginning of the line-number entries for the section.
        /// If there are no COFF line numbers, this value is zero.
        /// </summary>
        public DWORD PointerToLinenumbers;

        /// <summary>
        /// The number of relocation entries for the section. 
        /// This value is zero for executable images.
        /// </summary>
        public WORD NumberOfRelocations;

        /// <summary>
        /// The number of line-number entries for the section.
        /// </summary>
        public WORD NumberOfLinenumbers;

        /// <summary>
        /// The characteristics of the image.
        /// </summary>
        public DWORD Characteristics;
    }
}
