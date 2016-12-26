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

using System.Collections.Generic;
using net.r_eg.Conari.PE.WinNT;

namespace net.r_eg.Conari.PE
{
    public interface IPE
    {
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
        /// Full names of all available exported functions or variables from ExportDirectory 
        /// (WinNT OPTIONAL_HEADER).
        /// </summary>
        string[] ExportedProcNamesArray { get; }

        /// <summary>
        /// Active pe-file.
        /// </summary>
        string FileName { get; }
    }
}
