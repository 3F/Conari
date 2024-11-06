/*!
 * Copyright (c) 2016  Denis Kuzmin <x-3F@outlook.com> github/3F
 * Copyright (c) Conari contributors https://github.com/3F/Conari/graphs/contributors
 * Licensed under the MIT License (MIT).
 * See accompanying LICENSE.txt file or visit https://github.com/3F/Conari
*/

using System;
using System.IO;
using net.r_eg.Conari.Native.Core;
using net.r_eg.Conari.PE.Hole;

namespace net.r_eg.Conari.PE
{
    /// <summary>
    /// PE32/PE32+ <see cref="NativeStream"/> implementation.
    /// </summary>
    public sealed class PEFile: PEAbstract, IPE, IDisposable
    {
        public PEFile(string file)
            :base(new QPe
            (
                new NativeStream
                (
                    new FileStream
                    (
                        !string.IsNullOrWhiteSpace(file) ? file : throw new ArgumentNullException(nameof(file)), 
                        FileMode.Open,
                        FileAccess.Read,
                        FileShare.Read
                    )
                )
            ))
        {
            FileName = file;
        }

        #region IDisposable

        private bool disposed;

        private void Dispose(bool _)
        {
            if(!disposed)
            {
                ((NativeStream)qpe.Accessor).Dispose();
                FileName = null;

                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
