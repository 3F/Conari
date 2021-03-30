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
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;
using System.Threading;
using net.r_eg.Conari.Exceptions;
using net.r_eg.Conari.Log;
using net.r_eg.Conari.PE;
using net.r_eg.Conari.WinAPI;

namespace net.r_eg.Conari.Core
{
    public abstract class Loader: ILoader, IDisposable
    {
        private const string CLLI = "CLLI-ED221AE3-F097-4AA7-AAFB-84260FEB3D2A";
        private const int SIG_LIM = 8000; //ms

        private readonly EventWaitHandle ewhSignal = new EventWaitHandle(true, EventResetMode.AutoReset, CLLI);

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

        //TODO: integration with IConfig
        private protected abstract bool ModuleIsolation { get; set; }

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
            if(string.IsNullOrWhiteSpace(lib)) {
                throw new ArgumentNullException(nameof(lib));
            }

            if(Library.IsActive) {
                throw new LoaderException($"Module '{Library.module}' should be unloaded before new loading '{lib}'.");
            }

            Library = loadLibrary(lib);

            if(Library.handle == IntPtr.Zero) {
                // TODO: clarify specific error
                throw new LoadLibException($"Failed loading '{Library.module}'. Possible incorrect architecture or missing file or its dependencies. https://github.com/3F/Conari/issues/4", true);
            }

            PE = new PEFile(Library.module);

            AfterLoad(this, new DataArgs<Link>(Library));
            return true;
        }

        protected bool load()
        {
            return load(Library.module);
        }

        protected Link loadLibrary(string lib)
        {
            var l = new Link(lib);

            if(ModuleIsolation)
            {
                ewhSignal.WaitOne(SIG_LIM);

                try
                {
                    // GetModuleHandle must be used carefully when a multithreading.
                    // There is no guarantee that the module handle remains valid for the time when it will be used.
                    // That's why we still use loadLibrary for actual module use.
                    l.handle = NativeMethods.GetModuleHandle(lib);

                    if(l.handle != IntPtr.Zero && tryIsolateModule(l, out string isolated))
                    {
                        lib = isolated;
                        l.isolated = true;
                    }
                }
                finally
                {
                    ewhSignal.Set();
                }
            }

            l.handle = loadLibraryEx(lib);
            l.module = lib;

            // resolves full name of loaded module for the case when no file extension
            if(!l.resolved && !Path.HasExtension(l.module) && GetModuleFileName(l, out string module))
            {
                l.module = module;
            }
            return l;
        }

        protected virtual IntPtr loadLibraryEx(string lib)
        {
            // It can return the same handle as for the first loaded module because of used reference count for each loading through this. 
            // see details in `IConfig.IsolateLoadingOfModule` option.
            return NativeMethods.LoadLibraryEx(lib, IntPtr.Zero, LoadLibraryFlags.LOAD_WITH_ALTERED_SEARCH_PATH);
        }

        [SuppressMessage("Design", "CA1031:Do not catch general exception types", 
            Justification = "We can only try with loadLibrary, that's why we'll just return false for any problems. Additional option to rethrow exception is possible in future but not for today's implementation."
        )]
        protected virtual bool tryIsolateModule(Link l, out string module)
        {
            try
            {
                if(!l.resolved && !Path.HasExtension(l.module) && GetModuleFileName(l, out string fname)) {
                    module = fname;
                }
                else {
                    module = l.module;
                }

                var dstDir = Path.Combine(Path.GetTempPath(), CLLI, Guid.NewGuid().ToString());
                Directory.CreateDirectory(dstDir);

                var dst = Path.Combine(dstDir, Path.GetFileName(module));
                File.Copy(module, dst, true);

                module = dst;
                return true;
            }
            catch(Exception ex)
            {
                //TODO: option to throw exception
                LSender.Send(this, $"Something went wrong when trying to isolate `{l.module}`: {ex.Message}", Message.Level.Debug);
                
                module = null;
                return false;
            }
        }

        protected virtual void discardIsolation(Link l)
        {
            try 
            {
                var dir = Path.GetDirectoryName(l.module);

                if(dir.IndexOf(CLLI) != -1) // just to be sure
                {
                    File.Delete(l.module);
                    Directory.Delete(dir, false);
                }

                LSender.Send(this, $"Discarded isolation: {l.module}", Message.Level.Trace);
            }
            catch(IOException ex) {
                // we're working with temp files, so just inform about something:
                LSender.Send(this, $"Isolation cannot be discarded for `{l.module}`: {ex.Message}", Message.Level.Debug);
            }
        }

        private protected static bool GetModuleFileName(Link l, out string fname, int buffer = 1024)
            => l.resolved.value = GetModuleFileName(l.handle, out fname, buffer);

        private protected static bool GetModuleFileName(IntPtr hModule, out string fname, int buffer = 1024)
        {
            var sb = new StringBuilder(buffer);
            uint ret = NativeMethods.GetModuleFileName(hModule, sb, (uint)buffer);

            if(ret > 0)
            {
                fname = sb.ToString();
                return true;
            }

            fname = string.Empty;
            return false;
        }

        protected bool free()
        {
            ewhSignal.Dispose();

            if(!Library.IsActive) 
            {
                LSender.Send(this, $"Dispose Library: it's not activated.", Message.Level.Trace);
                return true;
            }

            BeforeUnload(this, new DataArgs<Link>(Library));

            try 
            {
                return NativeMethods.FreeLibrary(Library.handle);
            }
            finally
            {
                AfterUnload
                (
                    this, 
                    new DataArgs<Link>(
                        new Link(Library.module)
                    )
                );

                if(PE != null) {
                    LSender.Send(this, $"Dispose PE: file ({PE.FileName})", Message.Level.Debug);
                    ((IDisposable)PE).Dispose();
                }

                if(Library.isolated) {
                    discardIsolation(Library);
                }
            }
        }

        #region IDisposable

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if(disposed) {
                return;
            }
            disposed = true;

            free();
        }

        public void Dispose()
        {
            Dispose(true);
        }

        #endregion
    }
}
