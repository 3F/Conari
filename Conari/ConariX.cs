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
using System.Collections.Generic;
using System.Dynamic;
using System.Reflection;
using System.Runtime.InteropServices;
using net.r_eg.Conari.Aliases;
using net.r_eg.Conari.Core;
using net.r_eg.Conari.Log;
using net.r_eg.Conari.Native;
using net.r_eg.Conari.Native.Core;
using net.r_eg.Conari.PE;
using net.r_eg.Conari.Types;
using net.r_eg.Conari.Types.Methods;

namespace net.r_eg.Conari
{
    /// <summary>
    /// Conari engine [DLR version]. An unmanaged memory, modules, and raw data in one touch.
    /// https://github.com/3F/Conari
    /// </summary>
    public class ConariX: DynamicObject, IConari, ILoader, IProvider, IBinder, IDlrAccessor, INativeAccessor, IDisposable
    {
        private readonly ConariL __l_impl;

        /// <summary>
        /// Dynamic access to exported variables.
        /// </summary>
        public dynamic V => __l_impl.ExVar.DLR;

        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            return __l_impl.DLR.TryInvokeMember(binder, args, out result);
        }

        public static explicit operator IntPtr(ConariX l) => l.Library.handle;
        public static explicit operator VPtr(ConariX l) => l.Library.handle;

        /// <summary>
        /// Initialize Conari with specific calling convention.
        /// </summary>
        /// <param name="cfg">The Conari configuration.</param>
        /// <param name="conv">How should call methods.</param>
        /// <param name="prefix">Optional prefix to use via `bind&lt;&gt;`</param>
        public ConariX(IConfig cfg, CallingConvention conv, string prefix = null)
        {
            __l_impl = new ConariL(cfg, conv, prefix);
        }

        /// <summary>
        /// Initialize Conari with Cdecl - When the stack is cleaned up by the caller, it can do vararg functions.
        /// </summary>
        /// <param name="cfg">The Conari configuration.</param>
        /// <param name="prefix">Optional prefix to use via `bind&lt;&gt;`</param>
        public ConariX(IConfig cfg, string prefix = null)
            : this(cfg, CallingConvention.Cdecl, prefix)
        {

        }

        /// <summary>
        /// Initialize Conari with specific calling convention.
        /// </summary>
        /// <param name="lib">The library.</param>
        /// <param name="conv">How should call methods.</param>
        /// <param name="prefix">Optional prefix to use via `bind&lt;&gt;`</param>
        public ConariX(string lib, CallingConvention conv, string prefix = null)
            : this((Config)lib, conv, prefix)
        {

        }

        /// <summary>
        /// Initialize Conari with the calling convention by default.
        /// </summary>
        /// <param name="lib">The library.</param>
        /// <param name="prefix">Optional prefix to use via `bind&lt;&gt;`</param>
        public ConariX(string lib, string prefix = null)
            : this((Config)lib, prefix)
        {

        }

        /// <summary>
        /// Initialize Conari with the calling convention by default.
        /// </summary>
        /// <param name="lib">The library.</param>
        /// <param name="isolate">To isolate module for a real new loading when true. Details in {IConfig.IsolateLoadingOfModule}.</param>
        /// <param name="prefix">Optional prefix to use via `bind&lt;&gt;`</param>
        public ConariX(string lib, bool isolate, string prefix = null)
            : this(new Config(lib, isolate), prefix)
        {

        }

        #region ConariL implementation

        public NativeData Native => __l_impl.Native;

        public INativeReader Memory => __l_impl.Memory;

        public IProviderDLR ConfigDLR => __l_impl.ConfigDLR;

        public dynamic DLR => __l_impl.DLR;

        public dynamic _ => __l_impl._;

        public ISender Log => __l_impl.Log;

        public dynamic __cdecl => __l_impl.__cdecl;

        public dynamic __stdcall => __l_impl.__stdcall;

        public dynamic __fastcall => __l_impl.__fastcall;

        public dynamic __vectorcall => __l_impl.__vectorcall;

        public bool Cache { get => __l_impl.Cache; set => __l_impl.Cache = value; }

        public string Prefix { get => __l_impl.Prefix; set => __l_impl.Prefix = value; }

        public virtual CallingConvention Convention { get => __l_impl.Convention; set => __l_impl.Convention = value; }

        public bool Mangling { get => __l_impl.Mangling; set => __l_impl.Mangling = value; }

        public Dictionary<string, ProcAlias> Aliases => __l_impl.Aliases;

        public IExVar ExVar => __l_impl.ExVar;

        public IProviderSvc Svc => __l_impl.Svc;

        public Link Library => __l_impl.Library;

        public IPE PE => __l_impl.PE;

        public event EventHandler<DataArgs<string>> PrefixChanged
        {
            add => __l_impl.PrefixChanged += value;
            remove => __l_impl.PrefixChanged -= value;
        }

        public event EventHandler<DataArgs<CallingConvention>> ConventionChanged
        {
            add => __l_impl.ConventionChanged += value;
            remove => __l_impl.ConventionChanged -= value;
        }

        public event EventHandler<ProcAddressArgs> NewProcAddress
        {
            add => __l_impl.NewProcAddress += value;
            remove => __l_impl.NewProcAddress -= value;
        }

        public event EventHandler<DataArgs<Link>> BeforeUnload
        {
            add => __l_impl.BeforeUnload += value;
            remove => __l_impl.BeforeUnload -= value;
        }

        public event EventHandler<DataArgs<Link>> AfterUnload
        {
            add => __l_impl.AfterUnload += value;
            remove => __l_impl.AfterUnload -= value;
        }

        public event EventHandler<DataArgs<Link>> AfterLoad
        {
            add => __l_impl.AfterLoad += value;
            remove => __l_impl.AfterLoad -= value;
        }

        public string procName(string name) => __l_impl.procName(name);

        public IntPtr addr(LpProcName item) => __l_impl.addr(item);

        public T bindFunc<T>(string lpProcName) where T : class
        {
            return __l_impl.bindFunc<T>(lpProcName);
        }

        public Action bindFunc(string lpProcName)
        {
            return __l_impl.bindFunc(lpProcName);
        }

        public T bind<T>(string func) where T : class
        {
            return __l_impl.bind<T>(func);
        }

        public Action bind(string func)
        {
            return __l_impl.bind(func);
        }

        public TDyn bind(MethodInfo mi, string name)
        {
            return __l_impl.bind(mi, name);
        }

        public TDyn bind(MethodInfo mi, string name, CallingConvention conv)
        {
            return __l_impl.bind(mi, name, conv);
        }

        public Method<object, object> bindFunc(string lpProcName, Type ret, params Type[] args)
        {
            return __l_impl.bindFunc(lpProcName, ret, args);
        }

        public Method<T, object> bindFunc<T>(string lpProcName, Type ret, params Type[] args)
        {
            return __l_impl.bindFunc<T>(lpProcName, ret, args);
        }

        public Method<object, object> bind(string func, Type ret, params Type[] args)
        {
            return __l_impl.bind(func, ret, args);
        }

        public Method<T, object> bind<T>(string func, Type ret, params Type[] args)
        {
            return __l_impl.bind<T>(func, ret, args);
        }

        public void free(IntPtr ptr)
        {
            __l_impl.free(ptr);
        }

        #endregion

        #region IDisposable

        private bool disposed;

        protected virtual void Dispose(bool _)
        {
            if(!disposed)
            {
                __l_impl?.Dispose();
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
