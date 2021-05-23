using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using net.r_eg.Conari.Core;

namespace ConariTest._svc
{
    internal sealed class Allocator: IDisposable
    {
        public readonly IntPtr ptr;

        public Allocator(params byte[] data)
        {
            if(data == null) throw new ArgumentNullException(nameof(data));

            ptr = Marshal.AllocHGlobal(data.Length);

            for(int i = 0; i < data.Length; ++i)
            {
                Marshal.WriteByte(IntPtr.Add(ptr, i), data[i]);
            }
        }

        #region IDisposable

        private bool disposed;

        private void Dispose(bool _)
        {
            if(!disposed)
            {
                Marshal.FreeHGlobal(ptr);
                disposed = true;
            }
        }

        ~Allocator()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
