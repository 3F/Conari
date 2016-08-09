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

namespace net.r_eg.Conari.Types
{
    [DebuggerDisplay("{managed} [ {\"0x\" + pointer.ToString(\"X\")} ]")]
    public sealed class UnmanagedString: IDisposable
    {
        private string managed;

        /// <summary>
        /// Pointer to our allocated string.
        /// </summary>
        private IntPtr pointer;

        public SType Type
        {
            get;
            private set;
        }

        public enum SType
        {
            Auto,
            Ansi,
            Unicode,
            BSTR
        }

        public static implicit operator IntPtr(UnmanagedString val)
        {
            return val.pointer;
        }

        public static implicit operator CharPtr(UnmanagedString val)
        {
            return new CharPtr(val.pointer);
        }

        public static implicit operator WCharPtr(UnmanagedString val)
        {
            return new WCharPtr(val.pointer);
        }

        public static implicit operator BSTR(UnmanagedString val)
        {
            return new BSTR(val.pointer);
        }

        public UnmanagedString(string str, SType type = SType.Auto)
        {
            managed     = str;
            Type        = type;

            alloc();
        }

        private void alloc()
        {
            switch(Type)
            {
                case SType.Auto: {
                    pointer = Marshal.StringToHGlobalAuto(managed);
                    return;
                }
                case SType.Ansi: {
                    pointer = Marshal.StringToHGlobalAnsi(managed);
                    return;
                }
                case SType.Unicode: {
                    pointer = Marshal.StringToHGlobalUni(managed);
                    return;
                }
                case SType.BSTR: {
                    pointer = Marshal.StringToBSTR(managed);
                    return;
                }
            }
            throw new NotImplementedException($"the type '{Type}' is not yet implemented.");
        }

        private void free()
        {
            switch(Type)
            {
                case SType.Auto:
                case SType.Ansi:
                case SType.Unicode: {
                    Marshal.FreeHGlobal(pointer);
                    break;
                }
                case SType.BSTR: {
                    Marshal.FreeBSTR(pointer);
                    break;
                }
            }

            // but we still can try to get data from this offset :)
            pointer = IntPtr.Zero;
            managed = null;
        }

        #region IDisposable

        // To detect redundant calls
        private bool disposed = false;

        // Do not change this code. Put cleanup code in Dispose(bool disposing)
        public void Dispose()
        {
            Dispose(true);

            // To suppress only if the finalizer is overridden ! ~UnmanagedString()
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if(disposed) {
                return;
            }
            disposed = true;

            free();
        }

        // Do not change this code. Put cleanup code in Dispose(bool disposing)
        ~UnmanagedString()
        {
            Dispose(false);
        }

        #endregion
    }
}
