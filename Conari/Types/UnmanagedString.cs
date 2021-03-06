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
using net.r_eg.Conari.Resources;

namespace net.r_eg.Conari.Types
{
    [DebuggerDisplay("{Data} [ {\"0x\" + Pointer.ToString(\"X\")} ]")]
    [Obsolete("Use modern " + nameof(NativeString<WCharPtr>) + ", " + nameof(NativeString<CharPtr>))]
    public sealed class UnmanagedString: IDisposable
    {
        /// <summary>
        /// Pointer to allocated string.
        /// </summary>
        public IntPtr Pointer { get; private set; }

        /// <summary>
        /// Who is the owner for unmanaged string.
        /// </summary>
        public bool Owner { get; private set; }

        /// <summary>
        /// Access to managed or unmanaged string data.
        /// </summary>
        public string Data { get; private set; }

        public SType Type { get; private set; }

        public enum SType
        {
            Auto,
            Ansi,
            Unicode,
            BSTR
        }

        [NativeType]
        public static implicit operator IntPtr(UnmanagedString v) => v.Pointer;

        public static implicit operator CharPtr(UnmanagedString v) => new(v.Pointer);

        public static implicit operator WCharPtr(UnmanagedString v) => new(v.Pointer);

        public static implicit operator BSTR(UnmanagedString v) => new(v.Pointer);

        public UnmanagedString(string str, SType type = SType.Auto)
        {
            Data    = str ?? throw new ArgumentNullException(nameof(str));
            Type    = type;

            alloc();
        }

        public UnmanagedString(IntPtr ptr, SType type)
        {
            if(ptr == IntPtr.Zero) throw new ArgumentException(Msg.invalid_pointer);

            Type = type;
            Data = alloc(ptr);
        }

        private string alloc(IntPtr ptr)
        {
            Pointer = ptr;
            Owner   = false;

            switch(Type)
            {
                case SType.Ansi: return (CharPtr)ptr;
                case SType.Unicode: return (WCharPtr)ptr;
                case SType.BSTR: return (BSTR)ptr;
            }

            throw new NotImplementedException($"the type '{Type}' is not implemented yet.");
        }

        private void alloc()
        {
            Owner = true;

            switch(Type)
            {
                case SType.Auto: {
                    Pointer = Marshal.StringToHGlobalAuto(Data);
                    return;
                }
                case SType.Ansi: {
                    Pointer = Marshal.StringToHGlobalAnsi(Data);
                    return;
                }
                case SType.Unicode: {
                    Pointer = Marshal.StringToHGlobalUni(Data);
                    return;
                }
                case SType.BSTR: {
                    Pointer = Marshal.StringToBSTR(Data);
                    return;
                }
            }
            throw new NotImplementedException($"the type '{Type}' is not implemented yet.");
        }

        private void free(IntPtr ptr, SType type)
        {
            if(ptr == IntPtr.Zero) return;

            switch(type)
            {
                case SType.Auto:
                case SType.Ansi:
                case SType.Unicode: {
                    Marshal.FreeHGlobal(ptr);
                    break;
                }
                case SType.BSTR: {
                    Marshal.FreeBSTR(ptr);
                    break;
                }
            }
        }

        private void free(bool managed)
        {
            if(!managed && Owner) {
                free(Pointer, Type);
            }

            if(managed)
            {
                // since we still can try to get data from this offset
                Pointer = IntPtr.Zero;
                Data = null;
            }
        }

        #region IDisposable

        private bool disposed;

        private void Dispose(bool disposing)
        {
            if(!disposed)
            {
                free(managed: false);
                if(disposing) free(managed: true);

                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~UnmanagedString() => Dispose(false);

        #endregion
    }
}
