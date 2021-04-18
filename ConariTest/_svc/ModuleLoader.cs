using System;
using net.r_eg.Conari.WinAPI;

namespace ConariTest._svc
{
    internal sealed class ModuleLoader: IDisposable
    {
        public readonly IntPtr ptr;

        public static implicit operator IntPtr(ModuleLoader l) => l.ptr;

        public ModuleLoader(string file)
        {
            ptr = NativeMethods.LoadLibrary(file ?? throw new ArgumentNullException(nameof(file)));
        }

        #region IDisposable

        private bool disposed;

        private void Dispose(bool _)
        {
            if(!disposed)
            {
                NativeMethods.FreeLibrary(ptr);
                disposed = true;
            }
        }

        ~ModuleLoader()
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
