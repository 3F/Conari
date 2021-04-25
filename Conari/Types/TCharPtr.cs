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
using System.Runtime.Serialization;
using net.r_eg.Conari.Extension;
using net.r_eg.Conari.Resources;

namespace net.r_eg.Conari.Types
{
    /// <summary>
    /// Runtime-based type. <see cref="CharPtr"/> or <see cref="WCharPtr"/>.
    /// </summary>
    [DebuggerDisplay("{dbgInfo()}")]
    [Serializable]
    public struct TCharPtr: ISerializable
    {
        private static bool? _unicode;

        private readonly WCharPtr wdata;
        private readonly CharPtr data;

        public static readonly TCharPtr Null;

        public static bool Unicode
        {
            get
            {
                if(_unicode == null) _unicode = false;
                return _unicode == true;
            }
            set
            {
                if(_unicode != null) throw new NotSupportedException(Msg.configure_once);
                _unicode = value;
            }
        }

        /// <inheritdoc cref="CharPtr.Raw"/>
        public byte[] Raw => IsWideChar ? wdata.Raw : data.Raw;

        /// <inheritdoc cref="CharPtr.Length"/>
        public int Length => IsWideChar ? wdata.Length : data.Length;

        /// <inheritdoc cref="CharPtr.StrLength"/>
        public int StrLength => IsWideChar ? wdata.StrLength : data.StrLength;

        public bool IsWideChar => wdata != WCharPtr.Null;

        [NativeType]
        public static implicit operator IntPtr(TCharPtr v) => v.IsWideChar ? v.wdata : v.data;
        public static implicit operator string(TCharPtr v) => v.IsWideChar ? v.wdata : v.data;

        public static implicit operator TCharPtr(WCharPtr ptr) => new(ptr);
        public static implicit operator TCharPtr(CharPtr ptr) => new(ptr);

        public static implicit operator WCharPtr(TCharPtr v)
        {
            if(!v.IsWideChar) throw new NotSupportedException();
            return v.wdata;
        }

        public static implicit operator CharPtr(TCharPtr v)
        {
            if(v.IsWideChar) throw new NotSupportedException();
            return v.data;
        }

        public static bool operator ==(TCharPtr a, TCharPtr b) => a.Equals(b);

        public static bool operator !=(TCharPtr a, TCharPtr b) => !(a == b);

        public override bool Equals(object obj)
        {
            if(obj is null || obj is not TCharPtr b) {
                return false;
            }

            return wdata == b.wdata
                    && data == b.data;
        }

        public override int GetHashCode()
        {
            return 0.CalculateHashCode
            (
                wdata,
                data
            );
        }

        public override string ToString() => this;

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if(info == null) throw new ArgumentNullException(nameof(info));

            info.AddValue(nameof(wdata), wdata);
            info.AddValue(nameof(data), data);
        }

        public TCharPtr(WCharPtr data)
            : this()
        {
            wdata = data;
        }

        public TCharPtr(CharPtr data)
            : this()
        {
            this.data = data;
        }

        #region unit-tests

        /// <remarks>Synchronize context before any use!</remarks>
        internal static bool? __Unicode { get => _unicode; set => _unicode = value; }

        #endregion

        #region DebuggerDisplay

        private string dbgInfo()
        {
            string cbit;
            IntPtr ptr;

            if(IsWideChar)
            {
                cbit    = "16-bit";
                ptr     = wdata;
            }
            else
            {
                cbit    = "8-bit";
                ptr     = data;
            }

            return $"{(string)this}    [ An {StrLength} of a {cbit} characters at 0x{ptr.ToString("x")} ]";
        }

        #endregion
    }
}
