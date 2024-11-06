/*!
 * Copyright (c) 2016  Denis Kuzmin <x-3F@outlook.com> github/3F
 * Copyright (c) Conari contributors https://github.com/3F/Conari/graphs/contributors
 * Licensed under the MIT License (MIT).
 * See accompanying LICENSE.txt file or visit https://github.com/3F/Conari
*/

using System;
using net.r_eg.Conari.Native.Core;
using net.r_eg.Conari.PE.Hole;
using net.r_eg.Conari.PE.WinNT;

namespace net.r_eg.Conari.PE
{
    using static Static.Members;

    /// <summary>
    /// PE32/PE32+ <see cref="Memory"/> implementation.
    /// </summary>
    public sealed class PEMem: PEAbstract, IPE
    {
        /// <summary>
        /// Current image.
        /// </summary>
        public static Magic CurrentImage { get; }
            = Is64bit ? Magic.PE64 : Magic.PE32;

        /// <summary>
        /// Full path to current image.
        /// </summary>
        public static string CurrentImageName { get
        {
            try
            {
                return AppDomain.CurrentDomain.FriendlyName;
            }
            catch(AppDomainUnloadedException ex)
            {
                return ex.Source;
            }
        }}

        public PEMem(IntPtr addr)
            : base(new QPe(new Memory(addr)))
        {

        }
    }
}
