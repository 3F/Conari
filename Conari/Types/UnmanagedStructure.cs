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
    [DebuggerDisplay("[ {\"0x\" + pointer.ToString(\"X\")} ] SizeOf: {SizeOf}")]
    public sealed class UnmanagedStructure: IDisposable
    {
        /// <summary>
        /// Pointer to unmanaged memory where will placed structure.
        /// </summary>
        private IntPtr pointer;

        /// <summary>
        /// Managed structure.
        /// </summary>
        public dynamic Managed
        {
            get;
            private set;
        }

        public int SizeOf
        {
            get {
                return Marshal.SizeOf(Managed);
            }
        }

        [NativeType]
        public static implicit operator IntPtr(UnmanagedStructure val)
        {
            return val.pointer;
        }

        public static object ConvertToManaged(IntPtr ptr, Type type)
        {
            return Marshal.PtrToStructure(ptr, type);
        }

        public UnmanagedStructure(dynamic obj)
        {
            if(obj == null) {
                throw new ArgumentNullException("UnmanagedStructure cannot be null.");
            }
            Managed = obj;

            alloc();
        }

        public UnmanagedStructure(IntPtr ptr, Type type)
        {
            Managed = alloc(ptr, type);
        }

        private void alloc()
        {
            pointer = Marshal.AllocHGlobal(SizeOf);

            // copy to unmanaged memory
            Marshal.StructureToPtr(Managed, pointer, false);
        }

        private dynamic alloc(IntPtr ptr, Type type)
        {
            pointer = ptr;
            return ConvertToManaged(ptr, type);
        }

        private void free()
        {
            Marshal.FreeHGlobal(pointer);

            // but we still can try to get data from this offset :)
            pointer = IntPtr.Zero;
            Managed = null;
        }

        #region IDisposable

        // To detect redundant calls
        private bool disposed = false;

        // Do not change this code. Put cleanup code in Dispose(bool disposing)
        public void Dispose()
        {
            Dispose(true);

            // To suppress only if the finalizer is overridden ! ~UnmanagedStructure()
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
        ~UnmanagedStructure()
        {
            Dispose(false);
        }

        #endregion
    }
}
