/*!
 * Copyright (c) 2016  Denis Kuzmin <x-3F@outlook.com> github/3F
 * Copyright (c) Conari contributors https://github.com/3F/Conari/graphs/contributors
 * Licensed under the MIT License (MIT).
 * See accompanying LICENSE.txt file or visit https://github.com/3F/Conari
*/

using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using net.r_eg.Conari.Resources;

namespace net.r_eg.Conari.Types
{
    [DebuggerDisplay("[ {\"0x\" + Pointer.ToString(\"X\")} ] SizeOf: {SizeOf}")]
    [Obsolete("Use modern " + nameof(NativeStruct))]
    public sealed class UnmanagedStructure: IDisposable
    {
        /// <summary>
        /// Pointer to allocated unmanaged structure in memory.
        /// </summary>
        public IntPtr Pointer { get; private set; }

        /// <summary>
        /// Who is the owner. True indicates its own allocating.
        /// </summary>
        public bool Owner { get; private set; }

        /// <summary>
        /// Managed structure.
        /// </summary>
        public dynamic Managed { get; private set; }

        public int SizeOf => Marshal.SizeOf(Managed);

        [NativeType]
        public static implicit operator IntPtr(UnmanagedStructure v) => v.Pointer;

        public static object ConvertToManaged(IntPtr ptr, Type type)
        {
            return Marshal.PtrToStructure(ptr, type);
        }

        public UnmanagedStructure(dynamic obj)
        {
            Managed = obj ?? throw new ArgumentNullException(nameof(obj));

            alloc();
        }

        public UnmanagedStructure(IntPtr ptr, Type type)
        {
            if(ptr == IntPtr.Zero) throw new ArgumentOutOfRangeException(Msg.invalid_pointer);

            Managed = alloc(ptr, type);
        }

        private void alloc()
        {
            Pointer = Marshal.AllocHGlobal(SizeOf);
            Owner   = true;

            Marshal.StructureToPtr(Managed, Pointer, true);
        }

        private dynamic alloc(IntPtr ptr, Type type)
        {
            Pointer = ptr;
            Owner   = false;

            return ConvertToManaged(ptr, type);
        }

        private void free(bool managed)
        {
            if(!managed && Owner)
            {
                Marshal.DestroyStructure(Pointer, Managed.GetType());
                Marshal.FreeHGlobal(Pointer);
            }

            if(managed)
            {
                // since we still can try to get data from this offset
                Pointer = IntPtr.Zero;
                Managed = null;
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

        ~UnmanagedStructure() => Dispose(false);

        #endregion
    }
}
