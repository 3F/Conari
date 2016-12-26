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

using System;
using System.Collections.Generic;
using System.Linq;
using net.r_eg.Conari.PE.Hole;
using net.r_eg.Conari.PE.WinNT;

namespace net.r_eg.Conari.PE
{
    public sealed class PEFile: IPE, IDisposable
    {
        private ExportDirectory exdir;

        /// <summary>
        /// Get available sections.
        /// https://msdn.microsoft.com/en-us/library/windows/desktop/ms680341.aspx
        /// /winnt.h
        /// </summary>
        public IMAGE_SECTION_HEADER[] Sections
        {
            get {
                return exdir.Sections;
            }
        }

        /// <summary>
        /// Get IMAGE_EXPORT_DIRECTORY record.
        /// WinNT IMAGE_OPTIONAL_HEADER - IMAGE_DATA_DIRECTORY[IMAGE_DIRECTORY_ENTRY_EXPORT]
        /// </summary>
        public IMAGE_EXPORT_DIRECTORY DExport
        {
            get {
                return exdir.DExport;
            }
        }

        /// <summary>
        /// Receives full names of all available exported functions or variables from ExportDirectory 
        /// (WinNT OPTIONAL_HEADER).
        /// </summary>
        public IEnumerable<string> ExportedProcNames
        {
            get {
                return exdir.Names;
            }
        }

        /// <summary>
        /// Full names of all available exported functions or variables from ExportDirectory 
        /// (WinNT OPTIONAL_HEADER).
        /// </summary>
        public string[] ExportedProcNamesArray
        {
            get {
                return ExportedProcNames.ToArray();
            }
        }

        /// <summary>
        /// Active pe-file.
        /// </summary>
        public string FileName
        {
            get;
            private set;
        }

        public PEFile(string file)
        {
            if(String.IsNullOrWhiteSpace(file)) {
                throw new ArgumentException("PE file cannot be null or empty.");
            }

            exdir       = new ExportDirectory(file);
            FileName    = file;
        }

        private void free()
        {
            exdir.Dispose();
            FileName = null;
        }

        #region IDisposable

        // To detect redundant calls
        private bool disposed = false;

        // To correctly implement the disposable pattern.
        public void Dispose()
        {
            Dispose(true);
        }

        private void Dispose(bool disposing)
        {
            if(disposed) {
                return;
            }
            disposed = true;

            free();
        }

        #endregion
    }
}
