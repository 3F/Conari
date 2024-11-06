/*!
 * Copyright (c) 2016  Denis Kuzmin <x-3F@outlook.com> github/3F
 * Copyright (c) Conari contributors https://github.com/3F/Conari/graphs/contributors
 * Licensed under the MIT License (MIT).
 * See accompanying LICENSE.txt file or visit https://github.com/3F/Conari
*/

namespace net.r_eg.Conari.PE.WinNT
{
    using WORD = System.UInt16;

    /// <summary>
    /// IMAGE_OPTIONAL_HEADER 
    ///     WORD Magic; 0x108
    /// </summary>
    public enum Magic: WORD
    {
        /// <summary>
        /// PE32 image.
        /// </summary>
        PE32 = 0x10B,

        /// <summary>
        /// PE32+ image.
        /// </summary>
        PE64 = 0x20B,

        /// <summary>
        /// The file is a ROM image.
        /// </summary>
        ROM = 0x107,
    }
}
