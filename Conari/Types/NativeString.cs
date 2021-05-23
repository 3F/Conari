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
    [DebuggerDisplay("{DbgInfo}")]
    [Serializable]
    public class NativeString<T>: ISerializable, IDisposable
        where T : struct
    {
        protected IntPtr pointer;

        /// <remarks>It does not include a null termintor.</remarks>
        protected int? allocated;

        private static readonly byte[] cNull        = { 0 };
        private static readonly byte[] cNullWide    = { 0, 0 };

        /// <summary>
        /// Who is the owner. True indicates its own allocating.
        /// </summary>
        public bool Owner { get; private set; }

        internal bool Disposed => disposed;

        protected static bool IsWCharOrTCharWide
            => (typeof(T) == typeof(WCharPtr)) 
            || (typeof(T) == typeof(TCharPtr) && TCharPtr.Unicode);

        protected static bool IsCharOrTChar
            => (typeof(T) == typeof(CharPtr)) 
            || (typeof(T) == typeof(TCharPtr) && !TCharPtr.Unicode);

        protected static byte[] NullTerm => IsWCharOrTCharWide ? cNullWide : cNull;

        [NativeType]
        public static implicit operator IntPtr(NativeString<T> v) => v.pointer;

        public static implicit operator WCharPtr(NativeString<T> v) => v.pointer;

        public static implicit operator CharPtr(NativeString<T> v) => v.pointer;

        public static explicit operator string(NativeString<T> v) => v.ToString();

        public static implicit operator TCharPtr(NativeString<T> v)
        {
            if(IsWCharOrTCharWide) return new((WCharPtr)v.pointer);
            return new((CharPtr)v.pointer);
        }

        /// <summary>
        /// Adds a new string data to a string stored at the address of the current object.
        /// </summary>
        /// <remarks>Needs disposing a new returned object later.</remarks>
        /// <param name="input"></param>
        /// <param name="newstr">New string data. Any length.</param>
        /// <returns>Returns a NEW object which MUST be disposed later.</returns>
        public static NativeString<T> operator +(NativeString<T> input, string newstr)
        {
            NativeString<T> ret = new(input, newstr.Length);

            byte[] data = GetBytes(ref newstr);
            WriteTo(((IntPtr)ret) + GetLengthFromPtr(input), ref data, ret.allocated);
            return ret;
        }

        public static bool operator ==(NativeString<T> a, string b) => a.Equals(b);

        public static bool operator !=(NativeString<T> a, string b) => !(a == b);

        public static bool operator ==(string a, NativeString<T> b) => b.Equals(a);

        public static bool operator !=(string a, NativeString<T> b) => !(b == a);

        public static bool operator ==(NativeString<T> a, NativeString<T> b) => a.Equals(b);

        public static bool operator !=(NativeString<T> a, NativeString<T> b) => !(a == b);

        public override bool Equals(object obj)
        {
            if(obj is null) return false;
            if(obj is string s) return ToString() == s.ToString();

            if(obj is not NativeString<T> b) return false;

            // NOTE: actual values can be different for pointer == b.pointer due to updating without allocation

#if F_NATIVE_STRING_CMP_STRICT

            return allocated == b.allocated
                && ToString() == b.ToString();

#else

            return ToString() == b.ToString();
#endif
        }

        public override int GetHashCode()
        {
            return 0.CalculateHashCode
            (
                pointer,
                allocated,
                Owner
            );
        }

        public override string ToString()
        {
            if(pointer == IntPtr.Zero) return null;
            return IsWCharOrTCharWide ? (WCharPtr)pointer : (CharPtr)pointer;
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if(info == null) throw new ArgumentNullException(nameof(info));

            info.AddValue(nameof(pointer), pointer);
            info.AddValue(nameof(allocated), allocated);
            info.AddValue(nameof(Owner), Owner);
        }

        /// <summary>
        /// Updates string using current addresses.
        /// This operation will NOT change or allocate a new regions in memory.
        /// </summary>
        /// <remarks>It does not require any additional disposing after updating.</remarks>
        /// <param name="newstr">New string data. The size must fit allocated memory. Otherwise, use <see cref="add"/></param>
        /// <returns>Returns SAME object just to continue chain.</returns>
        public NativeString<T> update(string newstr)
        {
            if(pointer == IntPtr.Zero) throw new NotSupportedException(Msg.invalid_pointer);

            alloc(ref newstr, 0, pointer);
            return this;
        }

        /// <summary>
        /// Adds a new string data to a string stored at the address of the current object.
        /// </summary>
        /// <remarks>! Needs disposing a new returned object later.</remarks>
        /// <param name="newstr">New string data. Any length.</param>
        /// <returns>Returns a NEW object which MUST be disposed later.</returns>
        public NativeString<T> add(string newstr) => this + newstr;

        public NativeString(string str) => pointer = alloc(ref str, 0);

        public NativeString(int buffer = 0x7F) : this(string.Empty, buffer) { }

        public NativeString(string str, int extend) => pointer = alloc(ref str, extend);

        public NativeString(IntPtr pointer)
        {
            if(pointer == IntPtr.Zero) throw new ArgumentOutOfRangeException(Msg.invalid_pointer);

            this.pointer = pointer;
            allocated = GetLengthFromPtr(pointer);
        }

        public NativeString(IntPtr pointer, int extend)
            : this((string)(IsWCharOrTCharWide ? (WCharPtr)pointer : (CharPtr)pointer), extend)
        {

        }

        protected static int GetLengthFromPtr(IntPtr input)
            => IsWCharOrTCharWide ? input.GetStringLength(2) : input.GetStringLength();

        /// <summary>
        /// Updates data at specified address using the same allocated region.
        /// It will fail if allocated region is less than required for new data.
        /// </summary>
        /// <param name="addr">Beginning of the region.</param>
        /// <param name="newstr">New string data. Do not include a null terminator it will be processed automatically depending on the T.</param>
        /// <param name="allocated">Allocated region size.</param>
        /// <returns>Initial not modified address.</returns>
        protected static IntPtr WriteTo(IntPtr addr, ref byte[] newstr, int? allocated)
        {
            byte[] _c0 = NullTerm;

            if(newstr.Length > allocated)
            {
                throw new ArgumentOutOfRangeException(Msg.data_0_beyond_allocated_1.Format($"{newstr.Length}", $"{allocated}"));
            }

            if(newstr.Length > 0)
            {
                Marshal.Copy(newstr, 0, addr, newstr.Length);
            }
            Marshal.Copy(_c0, 0, addr + newstr.Length, _c0.Length);

            return addr;
        }

        protected static byte[] GetBytes(ref string str)
        {
            if(str == null) throw new NotSupportedException(Msg.no_support_null_cstr);

            if(IsWCharOrTCharWide)
            {
                // .NET uses a 16 bit unsigned characters, aka Unicode UTF-16.
                return Encoding.Unicode.GetBytes(str); // -> str.ToCharArray() 
            }
            else if(IsCharOrTChar)
            {
                return Encoding.ASCII.GetBytes(str);
            }

            throw new NotImplementedException(Msg.only_0_supported_yet.Format($"{nameof(TCharPtr)}, {nameof(WCharPtr)}, {nameof(CharPtr)}"));
        }

        protected IntPtr alloc(ref string str, int buffer = 0)
            => alloc(ref str, buffer, IntPtr.Zero);

        protected IntPtr alloc(ref string str, int buffer, IntPtr active)
        {
            byte[] data = GetBytes(ref str);

            return WriteTo
            (
                ((active != IntPtr.Zero) ? active : alloc(data.Length + Math.Max(0, buffer))),
                ref data,
                allocated
            );
        }

        /// <param name="cb">
        /// The required number of bytes in memory for actual string data.
        /// <br/>! Do not include size for null terminator it will be calculated automatically depending on the T.
        /// </param>
        protected IntPtr alloc(int cb)
        {
            if(allocated != null) throw new NotSupportedException(Msg.cannot_realloc + $" {cb} -> {allocated}");

            allocated = Math.Max(0, cb);
            Owner = true;
            return Marshal.AllocHGlobal(allocated.Value + NullTerm.Length);
        }

        protected void release(bool disposing = true)
        {
            if(Owner)
            {
                if(pointer == IntPtr.Zero) throw new ArgumentOutOfRangeException(Msg.invalid_pointer);
                Marshal.FreeHGlobal(pointer);
                Owner = false;
            }

            if(disposing)
            {
                pointer = IntPtr.Zero;
                allocated = null;
            }
        }

        #region IDisposable

        private bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if(!disposed)
            {
                release(disposing);
                disposed = true;
            }
        }

        ~NativeString() => Dispose(false);
        
        #endregion

        #region DebuggerDisplay

        private string DbgInfo
            => pointer == IntPtr.Zero ? "null" 
                : $"{(string)this}    [ at 0x{pointer.ToString("x")} ({(Owner ? "owner" : "access")}) ]";

        #endregion
    }
}
