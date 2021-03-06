﻿/*
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
    /// C-string (\0-terminated) that was based on 8-bit characters.
    /// </summary>
    [DebuggerDisplay("{DbgInfo}")]
    [Serializable]
    [StructLayout(LayoutKind.Explicit)]
    public struct CharPtr: IPtr, ISerializable
    {
        [FieldOffset(0)]
        private readonly IntPtr pointer;

        public static readonly CharPtr Null;

        /// <summary>
        /// Access to byte-sequence.
        /// </summary>
        public byte[] Raw => pointer.GetStringBytes(Length);

        /// <summary>
        /// The length of used chars needed to represent a C-string (\0-terminated).
        /// </summary>
        public int Length => pointer.GetStringLength();

        /// <summary>
        /// Length of string.
        /// </summary>
        public int StrLength => Length;

        public string Utf8 { get
        {
            if(pointer == IntPtr.Zero) return null;

            if(Length < 1) return string.Empty;

            return Encoding.UTF8.GetString(Raw, 0, Length);
        }}

        public IntPtr AddressPtr => this;

        [Obsolete("Use pointer information instead.")]
        public static int PtrSize => IntPtr.Size;

        [NativeType]
        public static implicit operator IntPtr(CharPtr v) => v.pointer;

        public static implicit operator string(CharPtr v) => v.ToString();

        public static implicit operator CharPtr(IntPtr ptr) => new(ptr);

        public static explicit operator CharPtr(Int64 v) => new((IntPtr)v);

        public static explicit operator CharPtr(Int32 v) => new((IntPtr)v);

        public static bool operator ==(CharPtr a, WCharPtr b) => a.Equals(b);

        public static bool operator !=(CharPtr a, WCharPtr b) => !(a == b);

        public static bool operator ==(CharPtr a, CharPtr b) => a.Equals(b);

        public static bool operator !=(CharPtr a, CharPtr b) => !(a == b);

        public override bool Equals(object obj)
        {
            if(obj is null || obj is not CharPtr b) {
                return false;
            }

#if F_NATIVE_STRING_CMP_STRICT

            return pointer == b.pointer
                    || ToString() == b.ToString();

#else
            return ToString() == b.ToString();
#endif
        }

        public override int GetHashCode()
        {
            return 0.CalculateHashCode
            (
                pointer
            );
        }

        public override string ToString() => Marshal.PtrToStringAnsi(pointer);

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if(info == null) throw new ArgumentNullException(nameof(info));

            info.AddValue(nameof(pointer), pointer);
        }

        public CharPtr(IntPtr pointer)
        {
            if(pointer == IntPtr.Zero) throw new ArgumentOutOfRangeException(Msg.invalid_pointer);
            this.pointer = pointer;
        }

        #region DebuggerDisplay

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string DbgInfo
            => pointer == IntPtr.Zero 
                ? "<nullptr>"
                : $"{(string)this}    [ An {StrLength} of a 8-bit characters at 0x{pointer.ToString("x")} ]";

        #endregion
    }
}
