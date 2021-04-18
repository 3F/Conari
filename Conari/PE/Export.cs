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
