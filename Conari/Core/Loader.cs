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
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using net.r_eg.Conari.Exceptions;
using net.r_eg.Conari.Extension;
using net.r_eg.Conari.Log;
using net.r_eg.Conari.PE;
using net.r_eg.Conari.Resources;
using net.r_eg.Conari.Types;
using net.r_eg.Conari.WinAPI;

namespace net.r_eg.Conari.Core
{
    public abstract class Loader: ILoader, IDisposable
    {
        private const string CLLI = "CLLI-ED221AE3-F097-4AA7-AAFB-84260FEB3D2A";

        private readonly EventWaitHandle ewhSignal = new(true, EventResetMode.AutoReset, CLLI);

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

        private protected abstract LLConfig LLCfg { get; set; }

        /// <summary>
        /// Active library.
        /// </summary>
        public Link Library { get; protected set; }

        /// <summary>
        /// PE32/PE32+ features.
        /// </summary>
        public IPE PE { get; protected set; }

        internal static string TempDstPath => Path.Combine(Path.GetTempPath(), CLLI);

        internal bool Disposed => disposed;

        public static explicit operator IntPtr(Loader l) => l.Library.handle;
        public static explicit operator VPtr(Loader l) => l.Library.handle;

        /// <summary>
        /// Loads library into the address space.
        /// </summary>
        /// <param name="lib">The name of the library.</param>
        /// <returns></returns>
        protected bool load(string lib)
        {
            if(string.IsNullOrWhiteSpace(lib)) throw new ArgumentNullException(nameof(lib));

            if(Library.IsActive) {
                throw new LoaderException($"Module '{Library}' should be unloaded before new loading '{lib}'.");
            }

            Library = loadLibrary(lib);
            if(Library.cancelled)
            {
                LSender.Send(this, $"The load of the `{Library}` was cancelled.", Message.Level.Debug);
                return false;
            }

            if(Library.handle == IntPtr.Zero)
            {
                if(File.Exists(Library.module)) //TODO: it can also be in PATH, system32, ... +optional extension
                {
                    using var _pe = new PEFile(Library.module);
                    if(_pe.Magic != PEMem.CurrentImage) throw new ArchitectureMismatchException(_pe);
                }
                throw new LoadLibException($"Failed loading '{Library}'. Possible incorrect architecture or missing file or its dependencies. https://github.com/3F/Conari/issues/4", true);
            }
            
            PE = getPeInstance(LLCfg.peImplementation, Library);

            AfterLoad(this, new DataArgs<Link>(Library));
            return true;
        }

        protected bool load() => load(Library.module);

        protected Link loadLibrary(string lib)
        {
            Link l = new(lib);
            try
            {
                WaitHandle[] handles = (LLCfg.cts == null) ? new[] { ewhSignal } 
                                                           : new[] { ewhSignal, LLCfg.cts.Token.WaitHandle };

                if(WaitHandle.WaitAny(handles, LLCfg.loaderSyncLimit) != 0)
                {
                    if(!LLCfg.cts.IsCancellationRequested)
                    {
                        LSender.Send(this, Msg.sync_limit_0.Format($"{LLCfg.loaderSyncLimit}"), Message.Level.Warn);
                    }

                    l.cancelled = true;
                    return l;
                }

                if(LLCfg.tryIsolateModule)
                {
                    // GetModuleHandle must be used carefully when a multithreading.
                    // There is no guarantee that the module handle remains valid for the time when it will be used.
                    // That's why we still use loadLibrary for actual module use.
                    l.handle = NativeMethods.GetModuleHandle(lib);

                    if(l.handle != IntPtr.Zero)
                    {
                        if(tryIsolateModule(l, out string isolated))
                        {
                            lib         = isolated;
                            l.isolated  = true;
                            l.handle    = IntPtr.Zero;
                        }
                        else if(LLCfg.cancelIfCantIsolate)
                        {
                            l.cancelled = true;
                            l.handle    = IntPtr.Zero;
                            return l;
                        }
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
            finally
            {
                if(!l.cancelled || (l.handle == IntPtr.Zero && LLCfg.cancelIfCantIsolate))
                    ewhSignal.Set();
            }
        }

        protected virtual IntPtr loadLibraryEx(string lib)
        {
            // It can return the same handle as for the first loaded module because of used reference count for each loading through this. 
            // see details in `IConfig.IsolateLoadingOfModule` option.
            return NativeMethods.LoadLibraryEx(lib, IntPtr.Zero, LoadLibraryFlags.LOAD_WITH_ALTERED_SEARCH_PATH);
        }

        protected bool tryIsolateModule(Link l, out string module)
        {
            try
            {
                if(LLCfg.moduleIsolationRecipe != null)
                {
                    return LLCfg.moduleIsolationRecipe.isolate(l, out module);
                }

                if(!l.resolved && !Path.HasExtension(l.module) && GetModuleFileName(l, out string fname)) {
                    module = fname;
                }
                else {
                    module = l.module;
                }

                var dstDir = Path.Combine(TempDstPath, Guid.NewGuid().ToString());
                Directory.CreateDirectory(dstDir);

                var dst = Path.Combine(dstDir, Path.GetFileName(module));
                File.Copy(module, dst, true);

                module = dst;
                return true;
            }
            catch(Exception ex)
            {
                module = null;
                LSender.Send
                (
                    this, 
                    $"`{l.module}` cannot be isolated (user's recipe `{LLCfg.moduleIsolationRecipe?.GetType().FullName}`): {ex.Message}",
                    Message.Level.Debug
                );
                return false;
            }
        }

        protected bool tryDiscardIsolation(Link l)
        {
            try
            {
                if(LLCfg.moduleIsolationRecipe != null)
                {
                    return LLCfg.moduleIsolationRecipe.discard(l);
                }

                string dir = Path.GetDirectoryName(l.module);

                if(dir.IndexOf(CLLI, StringComparison.OrdinalIgnoreCase) == -1)
                {
                    throw new DirectoryNotFoundException($"`{dir}` is not part of `{CLLI}`");
                }

                File.Delete(l.module);
                Directory.Delete(dir, false);

                LSender.Send(this, $"Discarded isolation: {l.module}", Message.Level.Trace);
                return true;
            }
            catch(Exception ex)
            {
                LSender.Send
                (
                    this,
                    $"Isolation cannot be discarded for `{l.module}` (user's recipe `{LLCfg.moduleIsolationRecipe?.GetType().FullName}`): {ex.Message}",
                    Message.Level.Debug
                );
                return false;
            }
        }

        protected IPE getPeInstance(PeImplType type, Link l)
        {
            switch(type)
            {
                case PeImplType.Default: // 1.5+ Memory
                case PeImplType.Memory: return new PEMem(l.handle);
                case PeImplType.NativeStream: return new PEFile(l.module);
            }
            throw new NotImplementedException();
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

        private bool free(bool disposing)
        {
            if(!Library.IsActive || Library.cancelled) 
            {
                LSender.Send(this, $"Ignored disposing. `{Library}` is not activated or has been cancelled.", Message.Level.Trace);
                return true;
            }

            if(!ewhSignal.WaitOne(LLCfg.loaderSyncLimit))
            {
                LSender.Send(this, Msg.sync_limit_0.Format($"{LLCfg.loaderSyncLimit}"), Message.Level.Warn);
            }

            if(disposing) BeforeUnload(this, new DataArgs<Link>(Library));

            bool ret = false;
            try 
            {
                ret = NativeMethods.FreeLibrary(Library.handle);
                return ret;
            }
            finally
            {
                ewhSignal.Set();

                if(disposing)
                AfterUnload
                (
                    this, 
                    new
                    (
                        ret ? new Link(Library.module) : Library
                    )
                );

                if(disposing && PE != null && PE is IDisposable pestream)
                {
                    LSender.Send(this, $"Dispose PE file `{PE.FileName}`", Message.Level.Trace);
                    pestream.Dispose();
                }

                if(Library.isolated)
                {
                    tryDiscardIsolation(Library);
                }

                if(disposing) ewhSignal.Dispose();
            }
        }

        #region IDisposable

        private bool disposed;

        protected virtual void Dispose(bool disposing)
        {
            if(!disposed)
            {
                if(!free(disposing))
                {
                    LSender.Send
                    (
                        this,
                        $"`{Library}` ({Library.handle}) cannot be disposed due to {Marshal.GetLastWin32Error()}",
                        Message.Level.Warn
                    );
                }
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~Loader() => Dispose(false);

        #endregion
    }
}
