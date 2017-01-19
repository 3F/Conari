/*
 * The MIT License (MIT)
 * 
 * Copyright (c) 2016-2017  Denis Kuzmin <entry.reg@gmail.com>
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Conari"), to deal
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
using System.Linq;
using net.r_eg.Conari.Exceptions;
using net.r_eg.Conari.Log;
using net.r_eg.Conari.Native;
using net.r_eg.Conari.Native.Core;
using net.r_eg.Conari.PE.WinNT;

namespace net.r_eg.Conari.PE.Hole
{
    using BYTE  = System.Byte;
    using DWORD = System.UInt32;
    using LONG  = System.Int32;
    using WORD  = System.UInt16;

    /// <summary>
    /// PE32/PE32+ files. Works with records from ExportDirectory: 
    /// WinNT IMAGE_OPTIONAL_HEADER - IMAGE_DATA_DIRECTORY[IMAGE_DIRECTORY_ENTRY_EXPORT]
    /// </summary>
    internal class ExportDirectory: IDisposable
    {
        protected BinaryReader reader;
        protected TExInfo exInfo;

        protected struct TExInfo
        {
            // VirtualAddress of DataDirectory[ENTRY_EXPORT]
            public DWORD VirtualAddress;

            // The Size of DataDirectory[ENTRY_EXPORT]
            public DWORD Size;

            // The number of sections
            public WORD NumberOfSections;
        }

        public IMAGE_SECTION_HEADER[] Sections
        {
            get;
            protected set;
        }

        public IMAGE_EXPORT_DIRECTORY DExport
        {
            get;
            protected set;
        }

        public IEnumerable<string> Names
        {
            get
            {
                if(DExport.NumberOfNames < 1 || DExport.AddressOfNames < 1) {
                    LSender.Send(
                        this, 
                        "The export functions was not found. The NumberOfNames or AddressOfNames < 1", 
                        Message.Level.Info
                    );
                    yield break;
                }

                var stream = reader.BaseStream;

                DWORD offset = rva2Offset(DExport.AddressOfNames);
                stream.Seek(offset, SeekOrigin.Begin);

                // addresses of function names

                DWORD[] names = new DWORD[DExport.NumberOfNames];
                for(DWORD i = 0; i < DExport.NumberOfNames; ++i) {
                    names[i] = rva2Offset(reader.ReadUInt32());
                }

                // null-terminated names

                foreach(DWORD addr in names)
                {
                    stream.Seek(addr, SeekOrigin.Begin);

                    char c;
                    string str = "";
                    while((c = reader.ReadChar()) != '\0') {
                        str += c;
                    }

                    yield return str;
                }
            }
        }

        public static string[] GetNames(string pefile)
        {
            using(var ef = new ExportDirectory(pefile)) {
                return ef.Names.ToArray();
            }
        }

        public static string[] TryGetNames(string pefile)
        {
            try {
                return GetNames(pefile);
            }
            catch(Exception ex) {
                LSender.Send<ExportDirectory>(new Message($"GetNames('{pefile}') threw an exception.", ex));
                return null;
            }
        }

        public ExportDirectory(string pefile)
        {
            reader = new BinaryReader(
                new FileStream(pefile, FileMode.Open, FileAccess.Read, FileShare.Read)
            );

            init();
        }

        protected void init()
        {
            exInfo = addrOfExport(lfanew());

            if(exInfo.VirtualAddress < 1 || exInfo.Size < 1) {
                return;
            }

            Sections = getSectionHeaders(exInfo.NumberOfSections);
            DExport  = getExports(exInfo.VirtualAddress);
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

        protected TExInfo addrOfExport(LONG e_lfanew)
        {
            var stream = reader.BaseStream;

            /* IMAGE_NT_HEADERS */
            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms680336.aspx

            stream.Seek(e_lfanew, SeekOrigin.Begin);

            char[] sig = new char[4];
            reader.Read(sig, 0, sig.Length); // A 4-byte signature identifying the file as a PE image

            // The bytes are "PE\0\0"
            if(sig[0] != 'P' || sig[1] != 'E' || sig[2] != '\0' || sig[3] != '\0') {
                throw new PECorruptDataException();
            }

            /* IMAGE_FILE_HEADER */
            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms680313.aspx

            byte[] IMAGE_FILE_HEADER = new byte[20];
            reader.Read(IMAGE_FILE_HEADER, 0, IMAGE_FILE_HEADER.Length);

            dynamic ifh = NativeData
                            ._(IMAGE_FILE_HEADER)
                            .t<WORD, WORD>(null, "NumberOfSections")
                            .align<DWORD>(3)
                            .t<WORD, WORD>("SizeOfOptionalHeader")
                            .Raw.Type;

            /* IMAGE_OPTIONAL_HEADER */
            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms680339.aspx

            // move to NumberOfRvaAndSizes

            if(ifh.SizeOfOptionalHeader == 0xE0) { // IMAGE_OPTIONAL_HEADER32
                stream.Seek(0x5C, SeekOrigin.Current);
            }
            else if(ifh.SizeOfOptionalHeader == 0xF0) { // IMAGE_OPTIONAL_HEADER64
                stream.Seek(0x6C, SeekOrigin.Current);
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

            dynamic idd = NativeData
                            ._(DIRECTORY_EXPORT)
                            .t<DWORD>("VirtualAddress")
                            .t<DWORD>("Size")
                            .Raw.Type;
            
            // to the end of directories
            stream.Seek(8 * (NumberOfRvaAndSizes - 1), SeekOrigin.Current);

            return new TExInfo() {
                VirtualAddress = idd.VirtualAddress,
                Size = idd.Size,
                NumberOfSections = ifh.NumberOfSections,
            };
        }

        protected IMAGE_SECTION_HEADER[] getSectionHeaders(int count)
        {
            /* IMAGE_SECTION_HEADER */
            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms680341.aspx

            if(count < 1) {
                return new IMAGE_SECTION_HEADER[0];
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

        #region IDisposable

        // To detect redundant calls
        private bool disposed = false;

        // To correctly implement the disposable pattern.
        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if(disposed) {
                return;
            }
            disposed = true;

            //...
            reader.Dispose();
        }

        #endregion
    }
}
