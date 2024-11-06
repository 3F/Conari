/*!
 * Copyright (c) 2016  Denis Kuzmin <x-3F@outlook.com> github/3F
 * Copyright (c) Conari contributors https://github.com/3F/Conari/graphs/contributors
 * Licensed under the MIT License (MIT).
 * See accompanying LICENSE.txt file or visit https://github.com/3F/Conari
*/

using System;
using System.Collections.Generic;
using net.r_eg.Conari.PE.WinNT;

namespace net.r_eg.Conari.PE
{
    public interface IPE
    {
        /// <summary>
        /// Attributes of the image.
        /// </summary>
        Characteristics Characteristics { get; }

        /// <summary>
        /// Magic word from optional header.
        /// </summary>
        Magic Magic { get; }

        /// <summary>
        /// Target architecture of the image.
        /// </summary>
        MachineTypes Machine { get; }

        /// <summary>
        /// Known addresses of the tables.
        /// </summary>
        AddrTables Addresses { get; }

        /// <summary>
        /// Get available sections.
        /// https://msdn.microsoft.com/en-us/library/windows/desktop/ms680341.aspx
        /// /winnt.h
        /// </summary>
        IMAGE_SECTION_HEADER[] Sections { get; }

        /// <summary>
        /// Get IMAGE_EXPORT_DIRECTORY record.
        /// WinNT IMAGE_OPTIONAL_HEADER - IMAGE_DATA_DIRECTORY[IMAGE_DIRECTORY_ENTRY_EXPORT]
        /// </summary>
        IMAGE_EXPORT_DIRECTORY DExport { get; }

        /// <summary>
        /// Receives full names of all available exported functions or variables from ExportDirectory 
        /// (WinNT OPTIONAL_HEADER).
        /// </summary>
        IEnumerable<string> ExportedProcNames { get; }

        /// <summary>
        /// Export Address Table + Export Name Table + Export Ordinal Table.
        /// </summary>
        Export Export { get; }

        /// <summary>
        /// Full names of all available exported functions or variables from ExportDirectory 
        /// (WinNT OPTIONAL_HEADER).
        /// </summary>
        [Obsolete("Use " + nameof(ExportedProcNames))]
        string[] ExportedProcNamesArray { get; }

        /// <summary>
        /// Active pe-file.
        /// </summary>
        string FileName { get; }
    }
}
