/*!
 * Copyright (c) 2016  Denis Kuzmin <x-3F@outlook.com> github/3F
 * Copyright (c) Conari contributors https://github.com/3F/Conari/graphs/contributors
 * Licensed under the MIT License (MIT).
 * See accompanying LICENSE.txt file or visit https://github.com/3F/Conari
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
    [StructLayout(LayoutKind.Explicit)]
    public struct WCharPtr: IPtr, ISerializable
    {
        [FieldOffset(0)]
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

        public IntPtr AddressPtr => this;

        [Obsolete("Use pointer information instead.")]
        public static int PtrSize => IntPtr.Size;

        [NativeType]
        public static implicit operator IntPtr(WCharPtr v) => v.pointer;

        public static implicit operator string(WCharPtr v) => v.ToString();

        public static implicit operator WCharPtr(IntPtr ptr) => new(ptr);

        public static explicit operator WCharPtr(Int64 v) => new((IntPtr)v);

        public static explicit operator WCharPtr(Int32 v) => new((IntPtr)v);

        public static bool operator ==(WCharPtr a, CharPtr b) => a.Equals(b);

        public static bool operator !=(WCharPtr a, CharPtr b) => !(a == b);

        public static bool operator ==(WCharPtr a, WCharPtr b) => a.Equals(b);

        public static bool operator !=(WCharPtr a, WCharPtr b) => !(a == b);

        public override bool Equals(object obj)
        {
            if(obj is null || obj is not WCharPtr b) {
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

        public override string ToString() => Marshal.PtrToStringUni(pointer);

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

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string DbgInfo
            => pointer == IntPtr.Zero 
                ? "<nullptr>"
                : $"{(string)this}    [ An {StrLength} of a 16-bit characters at 0x{pointer.ToString("x")} ]";

        #endregion
    }
}
