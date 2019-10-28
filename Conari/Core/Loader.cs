/*
 * The MIT License (MIT)
 *
 * Copyright (c) 2016-2019  Denis Kuzmin < entry.reg@gmail.com > GitHub/3F
 * Copyright (c) Conari contributors: https://github.com/3F/Conari/graphs/contributors
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
using net.r_eg.Conari.Exceptions;
using net.r_eg.Conari.Log;
using net.r_eg.Conari.PE;
using net.r_eg.Conari.WinAPI;

namespace net.r_eg.Conari.Core
{
    public abstract class Loader: ILoader, IDisposable
    {
        /// <summary>
        /// Before unloading a library.
        /// </summary>
        public event EventHandler<DataArgs<Link>> BeforeUnload = delegate(object sender, DataArgs<Link> e) { };

        /// <summary>
        /// When library has been unloaded.
        /// </summary>
        public event EventHandler<DataArgs<Link>> AfterUnload = delegate(object sender, DataArgs<Link> e) { };

        /// <summary>
        /// When library has been loaded.
        /// </summary>
        public event EventHandler<DataArgs<Link>> AfterLoad = delegate(object sender, DataArgs<Link> e) { };

        /// <summary>
        /// Active library.
        /// </summary>
        public Link Library
        {
            get;
            protected set;
        }

        /// <summary>
        /// PE32/PE32+ features.
        /// </summary>
        public IPE PE
        {
            get;
            protected set;
        }

        /// <summary>
        /// Loads library into the address space.
        /// </summary>
        /// <param name="lib">The name of the library.</param>
        /// <returns></returns>
        protected bool load(string lib)
        {
            if(Library.IsActive) {
                throw new LoaderException($"The library '{Library.LibName}' should be unloaded before new loading '{lib}'.");
            }

            if(String.IsNullOrWhiteSpace(lib)) {
                throw new ArgumentException("The library name cannot be null or empty.", "lib");
            }

            Library = loadLibrary(lib);

            if(Library.Handle == IntPtr.Zero) {
                throw new LoadLibException($"Failed loading '{Library.LibName}': Check used architecture or existence of file. https://github.com/3F/Conari/issues/4", true);
            }

            PE = new PEFile(Library.LibName);

            AfterLoad(this, new DataArgs<Link>(Library));
            return true;
        }

        protected bool load()
        {
            return load(Library.LibName);
        }

        protected Link loadLibrary(string lib)
        {
            IntPtr hModule = NativeMethods.LoadLibraryEx(lib, IntPtr.Zero, LoadLibraryFlags.LOAD_WITH_ALTERED_SEARCH_PATH);
            return new Link(hModule, lib);
        }

        protected bool free()
        {
            if(!Library.IsActive) {
                LSender.Send(this, $"Dispose Library: it's not activated.", Message.Level.Trace);
                return true;
            }

            BeforeUnload(this, new DataArgs<Link>(Library));

            try {
                return NativeMethods.FreeLibrary(Library.Handle);
            }
            finally
            {
                AfterUnload(
                    this, 
                    new DataArgs<Link>(
                        new Link(Library.LibName)
                    )
                );

                if(PE != null) {
                    LSender.Send(this, $"Dispose PE: file ({PE.FileName})", Message.Level.Debug);
                    ((IDisposable)PE).Dispose();
                }
            }
        }

        #region IDisposable

        // To detect redundant calls
        private bool disposed = false;

        // To correctly implement the disposable pattern. /CA1063
        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if(disposed) {
                return;
            }
            disposed = true;

            //...
            free();
        }

        #endregion
    }
}
