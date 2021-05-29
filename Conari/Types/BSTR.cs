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
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using net.r_eg.Conari.Extension;
using net.r_eg.Conari.Resources;

namespace net.r_eg.Conari.Types
{
    /// <summary>
    /// A BSTR (Basic string or binary string) is a composite data type that consists of a length prefix, a data string, and a terminator: <br/>
    /// * Length prefix - A 4-byte integer that contains the number of bytes in the following data string. <br/>
    ///                   It appears immediately before the first character of the data string. <br/>
    /// <br/>
    /// * Data string   - A string of Unicode characters. May contain multiple embedded null characters. <br/>
    /// * Terminator    - 2-null characters.
    /// </summary>
    [DebuggerDisplay("{dbgInfo()}")]
    [Serializable]
    [Obsolete("Use " + nameof(WCharPtr) + " together with powerful " + nameof(NativeString<WCharPtr>))]
    public struct BSTR: ISerializable
    {
        private IntPtr pointer;

        public static readonly BSTR Null;

        /// <inheritdoc cref="CharPtr.Raw"/>
        public byte[] Raw => pointer.GetStringBytes(Length);

        /// <inheritdoc cref="CharPtr.Length"/>
        public int Length => pointer.GetStringLength(2);

        /// <inheritdoc cref="CharPtr.StrLength"/>
        public int StrLength { get
        {
            int len = Length;
            if(len > 1) len /= 2;
            return len;
        }}

        public string Unicode { get
        {
            if(pointer == IntPtr.Zero) return null;
            return Marshal.PtrToStringUni(pointer);
        }}

        [Obsolete("Use pointer information instead.")]
        public static int PtrSize => IntPtr.Size;

        [NativeType]
        public static implicit operator IntPtr(BSTR v) => v.pointer;

        public static implicit operator string(BSTR v) => v.ToString();

        public static implicit operator BSTR(IntPtr ptr) => new(ptr);

        public static implicit operator BSTR(Int64 v) => new((IntPtr)v);

        public static implicit operator BSTR(Int32 v) => new((IntPtr)v);

        public static bool operator ==(BSTR a, CharPtr b) => a.Equals(b);

        public static bool operator !=(BSTR a, CharPtr b) => !(a == b);

        public static bool operator ==(BSTR a, BSTR b) => a.Equals(b);

        public static bool operator !=(BSTR a, BSTR b) => !(a == b);

        public override bool Equals(object obj)
        {
            if(obj is null || obj is not BSTR b) {
                return false;
            }

            return pointer == b.pointer
                    || ToString() == b.ToString();
        }

        public override int GetHashCode()
        {
            return 0.CalculateHashCode
            (
                pointer
            );
        }

        public override string ToString()
        {
            if(pointer == IntPtr.Zero) return null;
            return Marshal.PtrToStringBSTR(pointer);
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if(info == null) throw new ArgumentNullException(nameof(info));

            info.AddValue(nameof(pointer), pointer);
        }

        public void free()
        {
            Marshal.FreeBSTR(pointer);
            pointer = IntPtr.Zero;
        }

        public BSTR(IntPtr pointer)
        {
            if(pointer == IntPtr.Zero) throw new ArgumentOutOfRangeException(Msg.invalid_pointer);
            this.pointer = pointer;
        }

        #region DebuggerDisplay

        private string dbgInfo()
            => pointer == IntPtr.Zero ? "null"
                : $"{(string)this}    [ An {StrLength} of a BSTR at 0x{pointer.ToString("x")} ]";

        #endregion
    }
}
