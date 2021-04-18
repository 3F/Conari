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
