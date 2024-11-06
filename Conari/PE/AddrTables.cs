/*!
 * Copyright (c) 2016  Denis Kuzmin <x-3F@outlook.com> github/3F
 * Copyright (c) Conari contributors https://github.com/3F/Conari/graphs/contributors
 * Licensed under the MIT License (MIT).
 * See accompanying LICENSE.txt file or visit https://github.com/3F/Conari
*/

using net.r_eg.Conari.Types;

namespace net.r_eg.Conari.PE
{
    public sealed class AddrTables
    {
        public VPtr IMAGE_DOS_HEADER { get; internal set; }

        public VPtr IMAGE_NT_HEADERS { get; internal set; }

        public VPtr IMAGE_FILE_HEADER { get; internal set; }

        public VPtr IMAGE_OPTIONAL_HEADER { get; internal set; }

        public VPtr IMAGE_EXPORT_DIRECTORY { get; internal set; }

        internal AddrTables(VPtr mz)
        {
            IMAGE_DOS_HEADER = mz;
        }
    }
}
