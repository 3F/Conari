/*!
 * Copyright (c) 2016  Denis Kuzmin <x-3F@outlook.com> github/3F
 * Copyright (c) Conari contributors https://github.com/3F/Conari/graphs/contributors
 * Licensed under the MIT License (MIT).
 * See accompanying LICENSE.txt file or visit https://github.com/3F/Conari
*/

using System;
using System.Runtime.InteropServices;

namespace net.r_eg.Conari.Extension
{
    using static Static.Members;

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
                return EmptyArray<byte>();
            }

            byte[] ret = new byte[length];
            Marshal.Copy(ptr, ret, 0, length);

            return ret;
        }
    }
}
