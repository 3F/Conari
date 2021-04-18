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
using System.IO;
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

    /// <summary>
    /// PE32/PE32+ implementation.
    /// </summary>
    internal class ExportDirectory: IDisposable
    {
        protected readonly BinaryReader reader;

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
        /// VirtualAddress of DataDirectory[ENTRY_EXPORT]
        /// </summary>
        public DWORD VAExportDir { get; protected set; }

        /// <summary>
        /// The Size of DataDirectory[ENTRY_EXPORT]
        /// </summary>
        public DWORD SizeExportDir { get; protected set; }

        public WORD NumberOfSections { get; protected set; }

        public IMAGE_SECTION_HEADER[] Sections { get; protected set; }

        public IMAGE_EXPORT_DIRECTORY Export { get; protected set; }

        public IEnumerable<string> Names { get
        {
            if(Export.NumberOfNames < 1 || Export.AddressOfNames < 1)
            {
                LSender.Send
                (
                    this, 
                    "The export functions was not found. The NumberOfNames or AddressOfNames < 1", 
                    Message.Level.Info
                );
                yield break;
            }

            seek(Export.AddressOfNames);

            // addresses of function names

            DWORD[] names = new DWORD[Export.NumberOfNames];
            for(DWORD i = 0; i < Export.NumberOfNames; ++i) {
                names[i] = rva2Offset(reader.ReadUInt32());
            }

            // null-terminated names

            foreach(DWORD addr in names)
            {
                reader.BaseStream.Seek(addr, SeekOrigin.Begin);

                char c;
                string str = string.Empty;
                while((c = reader.ReadChar()) != '\0') {
                    str += c;
                }

                yield return str;
            }
        }}

        internal IEnumerable<DWORD> AddressesOfProc { get
        {
            seek(Export.AddressOfFunctions);

            for(DWORD i = 0; i < Export.NumberOfFunctions; ++i) {
                yield return reader.ReadUInt32();
            }
        }}

        internal IEnumerable<WORD> Ordinals { get
        {
            seek(Export.AddressOfNameOrdinals);

            for(DWORD i = 0; i < Export.NumberOfNames; ++i) {
                yield return reader.ReadUInt16();
            }
        }}

        public ExportDirectory(string pefile)
        {
            reader = new BinaryReader(
                new FileStream(pefile, FileMode.Open, FileAccess.Read, FileShare.Read)
            );

            initialize(lfanew());

            Sections    = getSectionHeaders(NumberOfSections);
            Export      = getExports(VAExportDir);
        }

        protected LONG lfanew()
        {
            /* IMAGE_DOS_HEADER - winnt.h */

            var stream = reader.BaseStream;

            // 0x3c LONG e_lfanew;
            stream.Seek(0x3C, SeekOrigin.Begin);

            // File address of new exe header
            LONG e_lfanew = reader.ReadInt32();

            if(e_lfanew < stream.Length) {
                return e_lfanew;
            }
            throw new PECorruptDataException();
        }

        protected void initialize(LONG e_lfanew)
        {
            var stream = reader.BaseStream;

            /* IMAGE_NT_HEADERS */
            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms680336.aspx

            stream.Seek(e_lfanew, SeekOrigin.Begin);

            char[] sig = new char[4];
            reader.Read(sig, 0, sig.Length); // A 4-byte signature identifying the file as a PE image

            if(sig[0] != 'P' || sig[1] != 'E' || sig[2] != '\0' || sig[3] != '\0') {
                throw new PECorruptDataException();
            }

            /* IMAGE_FILE_HEADER */
            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms680313.aspx

            const short _OFS_IOH = 2; //TODO: WORD for Magic from IMAGE_OPTIONAL_HEADER
            byte[] IMAGE_FILE_HEADER = new byte[/*WORD×4 + DWORD×3 =*/20 + _OFS_IOH];
            reader.Read(IMAGE_FILE_HEADER, 0, IMAGE_FILE_HEADER.Length);

            dynamic ifh = IMAGE_FILE_HEADER
                            .Native()
                            .t<WORD, WORD>("Machine", "NumberOfSections")
                            .align<DWORD>(3)
                            .t<WORD>("SizeOfOptionalHeader")
                            .t<WORD>("Characteristics")
                            .t<WORD>("Magic") //NOTE: actually this is part of IMAGE_OPTIONAL_HEADER offset 0 (0x108)
                            .Raw.Type;

            /* IMAGE_OPTIONAL_HEADER */
            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms680339.aspx

            // move to NumberOfRvaAndSizes (Optional Header)

            if(ifh.SizeOfOptionalHeader == 0xE0) { // IMAGE_OPTIONAL_HEADER32
                stream.Seek(0x5C - _OFS_IOH, SeekOrigin.Current);
            }
            else if(ifh.SizeOfOptionalHeader == 0xF0) { // IMAGE_OPTIONAL_HEADER64
                stream.Seek(0x6C - _OFS_IOH, SeekOrigin.Current);
            }
            else {
                // also known 0 for object files
                throw new PECorruptDataException($"SizeOfOptionalHeader: {ifh.SizeOfOptionalHeader}");
            }

            DWORD NumberOfRvaAndSizes = reader.ReadUInt32(); // The number of directory entries.

            /* IMAGE_DATA_DIRECTORY DataDirectory[IMAGE_NUMBEROF_DIRECTORY_ENTRIES];
                
                winnt.h Directory Entries:

                    #define IMAGE_DIRECTORY_ENTRY_EXPORT          0   // Export Directory
                    #define IMAGE_DIRECTORY_ENTRY_IMPORT          1   // Import Directory
                    #define IMAGE_DIRECTORY_ENTRY_RESOURCE        2   // Resource Directory
                    ...
            */
            /* DataDirectory[IMAGE_DIRECTORY_ENTRY_EXPORT] */
            // IMAGE_DATA_DIRECTORY struct: https://msdn.microsoft.com/en-us/library/windows/desktop/ms680305.aspx

            byte[] DIRECTORY_EXPORT = new byte[8];
            reader.Read(DIRECTORY_EXPORT, 0, DIRECTORY_EXPORT.Length);

            dynamic idd = DIRECTORY_EXPORT
                            .Native()
                            .t<DWORD>("VirtualAddress")
                            .t<DWORD>("Size")
                            .Raw.Type;
            
            // to the end of directories
            stream.Seek(8 * (NumberOfRvaAndSizes - 1), SeekOrigin.Current);

            {
                VAExportDir         = idd.VirtualAddress;
                SizeExportDir       = idd.Size;
                NumberOfSections    = ifh.NumberOfSections;
                Characteristics     = (Characteristics)ifh.Characteristics;
                Machine             = (MachineTypes)ifh.Machine;
                Magic               = (Magic)ifh.Magic;
            }
        }

        protected IMAGE_SECTION_HEADER[] getSectionHeaders(int count)
        {
            /* IMAGE_SECTION_HEADER */
            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms680341.aspx

            if(count < 1) {
                return EmptyArray<IMAGE_SECTION_HEADER>();
            }

            byte[] data = new byte[count * IMAGE_SECTION_HEADER.IMAGE_SIZEOF_SECTION_HEADER];
            reader.Read(data, 0, data.Length);

            var br          = new BReader(data);
            var sections    = new IMAGE_SECTION_HEADER[count];

            for(int i = 0; i < count; ++i)
            {
                sections[i].Name                    = br.next<BYTE[]>(8);
                sections[i].Misc                    = br.next<DWORD>();
                sections[i].VirtualAddress          = br.next<DWORD>();
                sections[i].SizeOfRawData           = br.next<DWORD>();
                sections[i].PointerToRawData        = br.next<DWORD>();
                sections[i].PointerToRelocations    = br.next<DWORD>();
                sections[i].PointerToLinenumbers    = br.next<DWORD>();
                sections[i].NumberOfRelocations     = br.next<WORD>();
                sections[i].NumberOfLinenumbers     = br.next<WORD>();
                sections[i].Characteristics         = br.next<DWORD>();
            }

            return sections;
        }

        protected IMAGE_EXPORT_DIRECTORY getExports(DWORD virtualAddress)
        {
            if(virtualAddress < 1) return new IMAGE_EXPORT_DIRECTORY();
            var stream = reader.BaseStream;

            DWORD offset = rva2Offset(virtualAddress);
            stream.Seek(offset, SeekOrigin.Begin);

            byte[] data = new byte[IMAGE_EXPORT_DIRECTORY.SIZEOF_EXPORT_DIRECTORY];
            reader.Read(data, 0, data.Length);

            var br = new BReader(data);
            return new IMAGE_EXPORT_DIRECTORY()
            {
                Characteristics         = br.next<DWORD>(),
                TimeDateStamp           = br.next<DWORD>(),
                MajorVersion            = br.next<WORD>(),
                MinorVersion            = br.next<WORD>(),
                Name                    = br.next<DWORD>(),
                Base                    = br.next<DWORD>(),
                NumberOfFunctions       = br.next<DWORD>(),
                NumberOfNames           = br.next<DWORD>(),
                AddressOfFunctions      = br.next<DWORD>(),
                AddressOfNames          = br.next<DWORD>(),
                AddressOfNameOrdinals   = br.next<DWORD>(),
            };
        }

        protected DWORD rva2Offset(DWORD rva)
        {
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
            reader.BaseStream.Seek(offset, SeekOrigin.Begin);
        }

        #region IDisposable

        private bool disposed;

        protected virtual void Dispose(bool _)
        {
            if(!disposed)
            {
                reader.Dispose();
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
