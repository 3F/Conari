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
using System.Text;
using net.r_eg.Conari.Extension;
using net.r_eg.Conari.Resources;

namespace net.r_eg.Conari.Types
{
    /// <summary>
    /// C-string (\0-terminated) that was based on 16-bit characters.
    /// </summary>
    [DebuggerDisplay("{DbgInfo}")]
    [Serializable]
    public struct WCharPtr: ISerializable
    {
        private readonly IntPtr pointer;

        public static readonly WCharPtr Null;

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

        public string BigEndian { get
        {
            if(pointer == IntPtr.Zero) return null;

            if(Length < 1) return string.Empty;

            return Encoding.BigEndianUnicode.GetString(Raw, 0, Length);
        }}

        [Obsolete]
        public static int PtrSize => IntPtr.Size;

        [NativeType]
        public static implicit operator IntPtr(WCharPtr v) => v.pointer;

        public static implicit operator string(WCharPtr v) => v.ToString();

        public static implicit operator WCharPtr(IntPtr ptr) => new(ptr);

        public static implicit operator WCharPtr(Int64 v) => new((IntPtr)v);

        public static implicit operator WCharPtr(Int32 v) => new((IntPtr)v);

        public static bool operator ==(WCharPtr a, CharPtr b) => a.Equals(b);

        public static bool operator !=(WCharPtr a, CharPtr b) => !(a == b);

        public static bool operator ==(WCharPtr a, WCharPtr b) => a.Equals(b);

        public static bool operator !=(WCharPtr a, WCharPtr b) => !(a == b);

        public override bool Equals(object obj)
        {
            if(obj is null || obj is not WCharPtr b) {
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
            return Marshal.PtrToStringUni(pointer);
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if(info == null) throw new ArgumentNullException(nameof(info));

            info.AddValue(nameof(pointer), pointer);
        }

        public WCharPtr(IntPtr pointer)
        {
            if(pointer == IntPtr.Zero) throw new ArgumentOutOfRangeException(Msg.invalid_pointer);
            this.pointer = pointer;
        }

        #region DebuggerDisplay

        private string DbgInfo
            => pointer == IntPtr.Zero ? "null" 
                : $"{(string)this}    [ An {StrLength} of a 16-bit characters at 0x{pointer:x} ]";

        #endregion
    }
}
