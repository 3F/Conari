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
using System.Diagnostics;
using System.Runtime.InteropServices;
using net.r_eg.Conari.Extension;

namespace net.r_eg.Conari.Types
{
    /// <summary>
    /// A BSTR (Basic string or binary string) is a string data type that is used by COM, Automation, and Interop functions.
    /// https://msdn.microsoft.com/en-us/library/windows/desktop/ms221069(v=vs.85).aspx
    /// 
    /// A BSTR is a composite data type that consists of a length prefix, a data string, and a terminator:
    /// * Length prefix - A 4-byte integer that contains the number of bytes in the following data string. 
    ///                   It appears immediately before the first character of the data string. 
    /// 
    /// * Data string   - A string of Unicode characters. May contain multiple embedded null characters.
    /// * Terminator    - 2-null characters.
    /// </summary>
    [DebuggerDisplay("{(string)this} [ {\"0x\" + ptr.ToString(\"X\")} Length: {Length} ]")]
    public struct BSTR
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

        public string Unicode
        {
            get
            {
                if(ptr == IntPtr.Zero) {
                    return null;
                }
                return Marshal.PtrToStringUni(ptr);
            }
        }

        [NativeType]
        public static implicit operator IntPtr(BSTR val)
        {
            return val.ptr;
        }

        public static implicit operator string(BSTR val)
        {
            if(val.ptr == IntPtr.Zero) {
                return null;
            }
            return Marshal.PtrToStringBSTR(val.ptr);
        }

        public static implicit operator BSTR(IntPtr ptr)
        {
            return new BSTR(ptr);
        }

        public static implicit operator BSTR(Int32 val)
        {
            return new BSTR((IntPtr)val);
        }

        /// <summary>
        /// Frees a BSTR using the COM SysFreeString function:
        /// https://msdn.microsoft.com/en-us/Library/ms221481.aspx
        /// </summary>
        public void free()
        {
            Marshal.FreeBSTR(ptr);
            ptr = IntPtr.Zero;
        }

        public BSTR(IntPtr ptr)
        {
            this.ptr = ptr;
        }
    }
}
