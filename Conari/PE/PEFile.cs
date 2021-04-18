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
using System.Linq;
using net.r_eg.Conari.PE.Hole;
using net.r_eg.Conari.PE.WinNT;

namespace net.r_eg.Conari.PE
{
    using static Static.Members;

    public sealed class PEFile: IPE, IDisposable
    {
        private readonly ExportDirectory exdir;
        private readonly Lazy<Export> _export;

        /// <summary>
        /// Attributes of the image.
        /// </summary>
        public Characteristics Characteristics => exdir.Characteristics;

        /// <summary>
        /// Magic word from optional header.
        /// </summary>
        public Magic Magic => exdir.Magic;

        /// <summary>
        /// Target architecture of the image.
        /// </summary>
        public MachineTypes Machine => exdir.Machine;

        /// <summary>
        /// Get available sections.
        /// https://msdn.microsoft.com/en-us/library/windows/desktop/ms680341.aspx
        /// /winnt.h
        /// </summary>
        public IMAGE_SECTION_HEADER[] Sections => exdir.Sections;

        /// <summary>
        /// Get IMAGE_EXPORT_DIRECTORY record.
        /// WinNT IMAGE_OPTIONAL_HEADER - IMAGE_DATA_DIRECTORY[IMAGE_DIRECTORY_ENTRY_EXPORT]
        /// </summary>
        public IMAGE_EXPORT_DIRECTORY DExport => exdir.Export;

        /// <summary>
        /// Receives full names of all available exported functions or variables from ExportDirectory 
        /// (WinNT OPTIONAL_HEADER).
        /// </summary>
        public IEnumerable<string> ExportedProcNames => exdir.Names;

        /// <summary>
        /// Export Address Table + Export Name Table + Export Ordinal Table.
        /// </summary>
        public Export Export => _export.Value;

        /// <summary>
        /// Full names of all available exported functions or variables from ExportDirectory 
        /// (WinNT OPTIONAL_HEADER).
        /// </summary>
        [Obsolete("Use " + nameof(ExportedProcNames))]
        public string[] ExportedProcNamesArray => ExportedProcNames.ToArray();

        /// <summary>
        /// Active pe-file.
        /// </summary>
        public string FileName { get; private set; }

        /// <summary>
        /// Current image.
        /// </summary>
        public Magic Current { get; } 
            = Is64bit ? Magic.PE64 : Magic.PE32;

        /// <summary>
        /// Full path to current image.
        /// </summary>
        public string CurrentImageName { get; private set; }

        public PEFile(string file)
        {
            if(string.IsNullOrWhiteSpace(file)) throw new ArgumentNullException(nameof(file));

            exdir       = new ExportDirectory(file);
            FileName    = file;

            _export = new Lazy<Export>(() => new(exdir));

            try
            {
                CurrentImageName = AppDomain.CurrentDomain.FriendlyName;
            }
            catch(AppDomainUnloadedException ex)
            {
                CurrentImageName = ex.Source;
            }
        }

        private void free()
        {
            exdir.Dispose();
            FileName = null;
        }

        #region IDisposable

        private bool disposed;

        private void Dispose(bool _)
        {
            if(!disposed)
            {
                free();
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
