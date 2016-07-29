/*
 * The MIT License (MIT)
 * 
 * Copyright (c) 2016  Denis Kuzmin <entry.reg@gmail.com>
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
using System.Runtime.InteropServices;
using System.Text;

namespace net.r_eg.Conari.Types
{
    /// <summary>
    ///   char*
    /// </summary>
    public struct CharPtr
    {
        private IntPtr ptr;

        /// <summary>
        /// Raw byte-sequence
        /// </summary>
        public byte[] Raw
        {
            get
            {
                //length - our strings always has a zero '\0' after its last character as in C.
                int len = -1;
                while(Marshal.ReadByte(ptr, ++len) != 0) { };

                if(len < 1) {
                    return new byte[0];
                }

                byte[] ret = new byte[len];
                Marshal.Copy(ptr, ret, 0, len);

                return ret;
            }
        }

        public string Ansi
        {
            get {
                return this;
            }
        }

        public string Unicode
        {
            get {
                return Marshal.PtrToStringUni(ptr);
            }
        }

        public string Utf8
        {
            get
            {
                byte[] data = Raw;

                if(data.Length < 1) {
                    return String.Empty;
                }
                return Encoding.UTF8.GetString(data, 0, data.Length);
            }
        }

        public string BSTR
        {
            get {
                return Marshal.PtrToStringBSTR(ptr);
            }
        }

        public static implicit operator string(CharPtr val)
        {
            return Marshal.PtrToStringAnsi(val.ptr);
        }

        public static implicit operator IntPtr(CharPtr val)
        {
            return val.ptr;
        }

        public static implicit operator CharPtr(IntPtr ptr)
        {
            return new CharPtr(ptr);
        }

        public CharPtr(IntPtr ptr)
        {
            this.ptr = ptr;
        }
    }
}
