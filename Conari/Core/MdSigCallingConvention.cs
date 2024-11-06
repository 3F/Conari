/*!
 * Copyright (c) 2016  Denis Kuzmin <x-3F@outlook.com> github/3F
 * Copyright (c) Conari contributors https://github.com/3F/Conari/graphs/contributors
 * Licensed under the MIT License (MIT).
 * See accompanying LICENSE.txt file or visit https://github.com/3F/Conari
*/

namespace net.r_eg.Conari.Core
{
    /// <summary>
    /// For internal m_signature processing with unmanaged EmitCalli.
    /// See Provider.
    /// </summary>
    internal enum MdSigCallingConvention: byte
    {
        Default         = 0x00,

        //
        C               = 0x01,
        StdCall         = 0x02,
        ThisCall        = 0x03,
        FastCall        = 0x04,

        //
        Vararg          = 0x05,
        Field           = 0x06,
        LocalSig        = 0x07,
        Property        = 0x08,
        Unmgd           = 0x09,
        GenericInst     = 0x0A,
        Max             = 0x0B,

        //
        Generics        = 0x10,
        HasThis         = 0x20,
        ExplicitThis    = 0x40,
        CallConvMask    = 0x0F,
    }
}
