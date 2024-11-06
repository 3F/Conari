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

namespace net.r_eg.Conari.PE
{
    using DWORD = UInt32;
    using WORD  = UInt16;

    public sealed class Export
    {
        private readonly DWORD[] addresses;
        private readonly string[] names;
        private readonly WORD[] ordinals;

        public IEnumerable<DWORD> Addresses => addresses;

        public IEnumerable<string> Names => names;

        public IEnumerable<WORD> NameOrdinals => ordinals;

        public DWORD getAddressOf(string name)
        {
            for(DWORD i = 0; i < names.Length; ++i)
            {
                if(name == names[i]) return addresses[ordinals[i]];
            }
            return 0;
        }

        public IntPtr getAddressOf(string name, IntPtr loaded)
        {
            return IntPtr.Add(loaded, Convert.ToInt32(getAddressOf(name)));
        }

        internal Export(QPe qpe)
        {
            if(qpe == null) throw new ArgumentNullException(nameof(qpe));

            names       = qpe.Names.ToArray();
            ordinals    = qpe.Ordinals.ToArray();
            addresses   = qpe.AddressesOfProc.ToArray();
        }
    }
}
