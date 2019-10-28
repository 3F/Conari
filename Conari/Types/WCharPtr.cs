/*
 * The MIT License (MIT)
 *
 * Copyright (c) 2016-2019  Denis Kuzmin < entry.reg@gmail.com > GitHub/3F
 * Copyright (c) Conari contributors: https://github.com/3F/Conari/graphs/contributors
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
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using net.r_eg.Conari.Extension;

namespace net.r_eg.Conari.Types
{
    [DebuggerDisplay("{(string)this} [ {\"0x\" + ptr.ToString(\"X\")} Length: {Length} ]")]
    public struct WCharPtr
    {
        private IntPtr ptr;

        /// <summary>
        /// Raw byte-sequence
        /// </summary>
        public byte[] Raw
        {
            get {
                return ptr.GetStringBytes(Length);
            }
        }

        public int Length
        {
            get {
                return ptr.GetStringLength(2);
            }
        }

        public string BigEndian
        {
            get
            {
                if(ptr == IntPtr.Zero) {
                    return null;
                }

                if(Length < 1) {
                    return String.Empty;
                }

                return Encoding.BigEndianUnicode.GetString(Raw, 0, Length);
            }
        }

        [NativeType]
        public static implicit operator IntPtr(WCharPtr val)
        {
            return val.ptr;
        }

        public static implicit operator string(WCharPtr val)
        {
            if(val.ptr == IntPtr.Zero) {
                return null;
            }
            return Marshal.PtrToStringUni(val.ptr);
        }

        public static implicit operator WCharPtr(IntPtr ptr)
        {
            return new WCharPtr(ptr);
        }

        public static implicit operator WCharPtr(Int32 val)
        {
            return new WCharPtr((IntPtr)val);
        }

        public WCharPtr(IntPtr ptr)
        {
            this.ptr = ptr;
        }
    }
}
