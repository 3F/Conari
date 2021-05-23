/*
 * The MIT License (MIT)
 *
 * Copyright (c) 2016-2021  Denis Kuzmin <x-3F@outlook.com> github/3F
 * Copyright (c) Conari contributors https://github.com/3F/Conari/graphs/contributors
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

using System;
using System.Collections.Generic;
using net.r_eg.Conari.Exceptions;
using net.r_eg.Conari.Log;
using net.r_eg.Conari.Native;
using net.r_eg.Conari.Native.Core;
using net.r_eg.Conari.PE.WinNT;

namespace net.r_eg.Conari.PE.Hole
{
    using BYTE  = Byte;
    using DWORD = UInt32;
    using LONG  = Int32;
    using WORD  = UInt16;
    using static Static.Members;

    // FIXME: QPe was based on old logic and its enumerations are not thread safe

    /// <summary>
    /// PE32/PE32+ Memory + Streams implementation.
    /// </summary>
    /// <remarks>Enumerations are not thread safe.</remarks>
    internal class QPe
    {
        protected DWORD vaExportDir;

        public IAccessor Accessor { get; protected set; }

        /// <summary>
        /// Attributes of the image.
        /// </summary>
        public Characteristics Characteristics { get; protected set; }

        /// <summary>
        /// Magic word from optional header.
        /// </summary>
        public Magic Magic { get; protected set; }

        /// <summary>
        /// Target architecture of the image.
        /// </summary>
        public MachineTypes Machine { get; protected set; }

        /// <summary>
        /// Known addresses of some structures.
        /// </summary>
        public AddrTables Addresses { get; protected set; }

        public IMAGE_SECTION_HEADER[] Sections { get; protected set; }

        public IMAGE_EXPORT_DIRECTORY Export { get; protected set; }

        public IEnumerable<string> Names { get
        {
            seek(Export.AddressOfNames);

            DWORD[] names = new DWORD[Export.NumberOfNames];

            for(DWORD i = 0; i < Export.NumberOfNames; ++i) {
                names[i] = rva2Offset(Accessor.read<DWORD>());
            }

            // null-terminated names
            foreach(DWORD addr in names)
            {
                Accessor.move(addr, Zone.D);

                char c;
                string str = string.Empty;
                while((c = Accessor.readChar()) != '\0') {
                    str += c;
                }

                yield return str;
            }
        }}

        internal IEnumerable<DWORD> AddressesOfProc { get
        {
            seek(Export.AddressOfFunctions);

            for(DWORD i = 0; i < Export.NumberOfFunctions; ++i) {
                yield return Accessor.read<DWORD>();
            }
        }}

        internal IEnumerable<WORD> Ordinals { get
        {
            seek(Export.AddressOfNameOrdinals);

            for(DWORD i = 0; i < Export.NumberOfNames; ++i) {
                yield return Accessor.read<WORD>();
            }
        }}

        public QPe(IAccessor accessor)
        {
            Accessor    = accessor ?? throw new ArgumentNullException(nameof(accessor));
            Addresses   = new AddrTables(Accessor.InitialPtr);

            var numberOfSections = initialize(lfanew());

            Sections    = getSectionHeaders(numberOfSections);
            Export      = getExports(vaExportDir);
        }

        protected LONG lfanew()
        {
            /* IMAGE_DOS_HEADER - winnt.h */
            Accessor.move(0x3C/*e_lfanew*/, Zone.D);

            // File address of the new pe header
            return Accessor.read<LONG>();
        }

        protected WORD initialize(LONG e_lfanew)
        {
            /* IMAGE_NT_HEADERS */
            Addresses.IMAGE_NT_HEADERS = Accessor.getAddr(e_lfanew);

            Accessor.move(e_lfanew, Zone.D)
                .eq('P', 'E', '\0', '\0')
                .ifFalse(_ => throw new PECorruptDataException());

            /* IMAGE_FILE_HEADER */
            Addresses.IMAGE_FILE_HEADER = Accessor.CurrentPtr;

            Accessor.Native()
                .f<WORD>("Machine", "NumberOfSections")
                .align<DWORD>(3)
                .t<WORD>("SizeOfOptionalHeader")
                .t<WORD>("Characteristics")
                .region()
                .t<WORD>("Magic") // start IMAGE_OPTIONAL_HEADER offset 0 (0x108)
                .build(out dynamic ifh);

            /* IMAGE_OPTIONAL_HEADER - move to NumberOfRvaAndSizes */
            Addresses.IMAGE_OPTIONAL_HEADER = Accessor.CurrentPtr;

            if(ifh.SizeOfOptionalHeader == 0xE0) { // IMAGE_OPTIONAL_HEADER32
                Accessor.move(0x5C);
            }
            else if(ifh.SizeOfOptionalHeader == 0xF0) { // IMAGE_OPTIONAL_HEADER64
                Accessor.move(0x6C);
            }
            else {
                // also known 0 for object files
                throw new PECorruptDataException($"SizeOfOptionalHeader: {ifh.SizeOfOptionalHeader}");
            }

            DWORD NumberOfRvaAndSizes = Accessor.read<DWORD>(); // The number of directory entries.

            /* IMAGE_DATA_DIRECTORY DataDirectory[IMAGE_NUMBEROF_DIRECTORY_ENTRIES] */

            Accessor.Native() // DataDirectory[IMAGE_DIRECTORY_ENTRY_EXPORT]
                .t<DWORD>("VirtualAddress")
                .t<DWORD>("Size")
                .build(out dynamic idd);

            // to the end of directory entries
            Accessor.move(8 * (NumberOfRvaAndSizes - 1));

            {
                vaExportDir         = idd.VirtualAddress;
                Characteristics     = (Characteristics)ifh.Characteristics;
                Machine             = (MachineTypes)ifh.Machine;
                Magic               = (Magic)ifh.Magic;
            }

            return ifh.NumberOfSections;
        }

        protected IMAGE_SECTION_HEADER[] getSectionHeaders(int count)
        {
            if(count < 1) return EmptyArray<IMAGE_SECTION_HEADER>();

            var sections = new IMAGE_SECTION_HEADER[count];
            for(int i = 0; i < count; ++i)
            {
                Accessor
                .bytes<BYTE>
                (
                    IMAGE_SECTION_HEADER.IMAGE_SIZEOF_SHORT_NAME, 
                    ref sections[i].Name
                )
                .next<DWORD>(ref sections[i].Misc)
                .next<DWORD>(ref sections[i].VirtualAddress)
                .next<DWORD>(ref sections[i].SizeOfRawData)
                .next<DWORD>(ref sections[i].PointerToRawData)
                .next<DWORD>(ref sections[i].PointerToRelocations)
                .next<DWORD>(ref sections[i].PointerToLinenumbers)
                .next<WORD> (ref sections[i].NumberOfRelocations)
                .next<WORD> (ref sections[i].NumberOfLinenumbers)
                .next<DWORD>(ref sections[i].Characteristics);
            }

            return sections;
        }

        protected IMAGE_EXPORT_DIRECTORY getExports(DWORD virtualAddress)
        {
            if(virtualAddress < 1) return new IMAGE_EXPORT_DIRECTORY();

            DWORD offset = rva2Offset(virtualAddress);
            Accessor.move(offset, Zone.D);

            Addresses.IMAGE_EXPORT_DIRECTORY = Accessor.CurrentPtr;

            return new IMAGE_EXPORT_DIRECTORY()
            {
                Characteristics         = Accessor.read<DWORD>(),
                TimeDateStamp           = Accessor.read<DWORD>(),
                MajorVersion            = Accessor.read<WORD>(),
                MinorVersion            = Accessor.read<WORD>(),
                Name                    = Accessor.read<DWORD>(),
                Base                    = Accessor.read<DWORD>(),
                NumberOfFunctions       = Accessor.read<DWORD>(),
                NumberOfNames           = Accessor.read<DWORD>(),
                AddressOfFunctions      = Accessor.read<DWORD>(),
                AddressOfNames          = Accessor.read<DWORD>(),
                AddressOfNameOrdinals   = Accessor.read<DWORD>(),
            };
        }

        protected DWORD rva2Offset(DWORD rva)
        {
            if(!Accessor.CurrentPtr.IsLong) return rva;

            foreach(var s in Sections) {
#if TRACE
                LSender.Send(this, $"VA: {s.VirtualAddress}; Size: {s.SizeOfRawData}; RVA: {rva}", Message.Level.Trace);
#endif
                if((rva >= s.VirtualAddress) && (rva < s.VirtualAddress + s.SizeOfRawData)) {
                    return (rva - s.VirtualAddress) + s.PointerToRawData;
                }
            }

            throw new ArgumentOutOfRangeException($"Incorrect RVA: {rva}");
        }

        private void seek(DWORD rva)
        {
            DWORD offset = rva2Offset(rva);
            Accessor.move(offset, Zone.D);
        }
    }
}
