/*!
 * Copyright (c) 2016  Denis Kuzmin <x-3F@outlook.com> github/3F
 * Copyright (c) Conari contributors https://github.com/3F/Conari/graphs/contributors
 * Licensed under the MIT License (MIT).
 * See accompanying LICENSE.txt file or visit https://github.com/3F/Conari
*/

using System;
using System.Collections.Generic;
using System.Linq;
using net.r_eg.Conari.PE.Hole;
using net.r_eg.Conari.PE.WinNT;

namespace net.r_eg.Conari.PE
{
    public abstract class PEAbstract: IPE
    {
        internal readonly QPe qpe;
        private readonly Lazy<Export> export;

        public Characteristics Characteristics => qpe.Characteristics;

        public Magic Magic => qpe.Magic;

        public MachineTypes Machine => qpe.Machine;

        public AddrTables Addresses => qpe.Addresses;

        public IMAGE_SECTION_HEADER[] Sections => qpe.Sections;

        public IMAGE_EXPORT_DIRECTORY DExport => qpe.Export;

        public IEnumerable<string> ExportedProcNames => qpe.Names;

        public Export Export => export.Value;

        [Obsolete("Use " + nameof(ExportedProcNames))]
        public string[] ExportedProcNamesArray => ExportedProcNames.ToArray();

        public string FileName { get; protected set; }

        internal PEAbstract(QPe qpe)
        {
            this.qpe    = qpe ?? throw new ArgumentNullException(nameof(qpe));
            export      = new Lazy<Export>(() => new(qpe));
        }
    }
}
