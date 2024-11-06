/*!
 * Copyright (c) 2016  Denis Kuzmin <x-3F@outlook.com> github/3F
 * Copyright (c) Conari contributors https://github.com/3F/Conari/graphs/contributors
 * Licensed under the MIT License (MIT).
 * See accompanying LICENSE.txt file or visit https://github.com/3F/Conari
*/

using System;
using System.Text;
using net.r_eg.Conari.PE;
using net.r_eg.Conari.PE.WinNT;

namespace net.r_eg.Conari.Exceptions
{
    [Serializable]
    public class ArchitectureMismatchException: LoadLibException
    {
        public ArchitectureMismatchException(IPE pe, bool getError = true)
            : base(format(pe), getError)
        {

        }

        private static string format(IPE pe)
        {
            StringBuilder sb = new();
            sb.Append($"Target architecture of the image is {pe.Machine} (");

            if(pe.Characteristics.HasFlag(Characteristics.IMAGE_FILE_LARGE_ADDRESS_AWARE)) {
                sb.Append($"can ");
            }
            else {
                sb.Append($"cannot ");
            }

            sb.Append($"handle > 2-GB addresses): `{PEMem.CurrentImageName}`({PEMem.CurrentImage}) cannot process `{pe.FileName}`({pe.Magic}). ");

            sb.Append($"Possible solution https://github.com/3F/Conari/issues/4");
            return sb.ToString();
        }
    }
}
