/*
 * The MIT License (MIT)
 * 
 * Copyright (c) 2016-2018  Denis Kuzmin < entry.reg@gmail.com > :: github.com/3F
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Conari"), to deal
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
using System.Runtime.InteropServices;

namespace net.r_eg.Conari.Extension
{
    public static class IntPtrExtension
    {
        /// <summary>
        /// Get length of null-based string.
        /// </summary>
        /// <param name="ptr">Pointer to unmanaged string.</param>
        /// <param name="nulsize">Count of a zero '\0' after last character.</param>
        /// <returns></returns>
        public static int GetStringLength(this IntPtr ptr, uint nulsize = 1)
        {
            if(ptr == IntPtr.Zero) {
                return -1;
            }

            int nul = (int)Math.Max(1, nulsize);

            Func<int, bool> isNul = delegate(int ofs)
            {
                if(nul == 8) {
                    return (Marshal.ReadInt64(ptr, ofs) == 0);
                }

                if(nul == 4) {
                    return (Marshal.ReadInt32(ptr, ofs) == 0);
                }

                if(nul == 2) {
                    return (Marshal.ReadInt16(ptr, ofs) == 0);
                }

                for(int i = 0; i < nul; ++i) {
                    if(Marshal.ReadByte(ptr, ofs + i) != 0) {
                        return false;
                    }
                }
                return true;
            };

            int len = 0;
            while(!isNul(len)) { len += nul; }

            return len;
        }

        /// <summary>
        /// Byte-sequence of unmanaged string.
        /// </summary>
        /// <param name="ptr">Pointer to unmanaged string.</param>
        /// <param name="length">Length of string in bytes.</param>
        /// <returns></returns>
        public static byte[] GetStringBytes(this IntPtr ptr, int length)
        {
            if(length < 1 || ptr == IntPtr.Zero) {
                return new byte[0];
            }

            byte[] ret = new byte[length];
            Marshal.Copy(ptr, ret, 0, length);

            return ret;
        }
    }
}
