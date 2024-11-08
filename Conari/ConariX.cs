﻿/*!
 * Copyright (c) 2016  Denis Kuzmin <x-3F@outlook.com> github/3F
 * Copyright (c) Conari contributors https://github.com/3F/Conari/graphs/contributors
 * Licensed under the MIT License (MIT).
 * See accompanying LICENSE.txt file or visit https://github.com/3F/Conari
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
    /// Conari engine [DLR version]. An unmanaged memory, modules, and raw data in one-touch.
    /// </summary>
    /// <remarks>https://github.com/3F/Conari</remarks>
    public class ConariX: ConariX<TCharPtr>, IConari
    {
        /// <summary>
        /// Wrapper to use both runtime dynamic and compile type <see cref="ConariX"/>.
        /// </summary>
        /// <param name="x">Actual instance.</param>
        /// <param name="d">Use runtime dynamic type.</param>
        /// <returns>Use compile type instance.</returns>
        public static ConariX Make(ConariX x, out dynamic d)
        {
            d = x ?? throw new ArgumentNullException(nameof(x));
            return x;
        }

        /// <inheritdoc cref="ConariL(IConfig, CallingConvention, string)"/>
        public ConariX(IConfig cfg, CallingConvention conv, string prefix = null)
            : base(cfg, conv, prefix)
        {

        }

        /// <inheritdoc cref="ConariL(IConfig, string)"/>
        public ConariX(IConfig cfg, string prefix = null)
            : base(cfg, prefix)
        {

        }

        /// <inheritdoc cref="ConariL(string, CallingConvention, string)"/>
        public ConariX(string library, CallingConvention conv, string prefix = null)
            : base(library, conv, prefix)
        {

        }

        /// <inheritdoc cref="ConariL(string, string)"/>
        public ConariX(string library, string prefix = null)
            : base(library, prefix)
        {

        }

        /// <inheritdoc cref="ConariL(string, bool, string)"/>
        public ConariX(string library, bool isolate, string prefix = null)
            : base(library, isolate, prefix)
        {

        }
    }

    /// <inheritdoc cref="ConariX"/>
    public class ConariX<TCharIn>: DynamicObject, 
                                    IConari<TCharIn>, 
                                    ILoader, 
                                    IProvider, 
                                    IBinder, 
                                    IDlrAccessor, 
                                    INativeAccessor, 
                                    IStringMaker<TCharIn>, 
                                    IDisposable
        where TCharIn: struct
    {
        protected readonly ConariL<TCharIn> __l_impl;

        /// <summary>
        /// Dynamic access to exported variables.
        /// </summary>
        public dynamic V => __l_impl.ExVar.DLR;

        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            return __l_impl.DLR.TryInvokeMember(binder, args, out result);
        }

        public static explicit operator IntPtr(ConariX<TCharIn> l) => l.Library.handle;
        public static explicit operator VPtr(ConariX<TCharIn> l) => l.Library.handle;

        /// <inheritdoc cref="ConariX.Make(ConariX, out dynamic)"/>
        public static ConariX<TCharIn> Make(ConariX<TCharIn> x, out dynamic d)
        {
            d = x ?? throw new ArgumentNullException(nameof(x));
            return x;
        }

        /// <inheritdoc cref="ConariX(IConfig, CallingConvention, string)"/>
        public ConariX(IConfig cfg, CallingConvention conv, string prefix = null)
        {
            __l_impl = new ConariL<TCharIn>(cfg, conv, prefix);
        }

        /// <inheritdoc cref="ConariX(IConfig, string)"/>
        public ConariX(IConfig cfg, string prefix = null)
            : this(cfg, CallingConvention.Cdecl, prefix)
        {

        }

        /// <inheritdoc cref="ConariX(string, CallingConvention, string)"/>
        public ConariX(string lib, CallingConvention conv, string prefix = null)
            : this((Config)lib, conv, prefix)
        {

        }

        /// <inheritdoc cref="ConariX(string, string)"/>
        public ConariX(string lib, string prefix = null)
            : this((Config)lib, prefix)
        {

        }

        /// <inheritdoc cref="ConariX(string, bool, string)"/>
        public ConariX(string lib, bool isolate, string prefix = null)
            : this(new Config(lib, isolate), prefix)
        {

        }

        #region ConariL implementation

        public NativeData Native => __l_impl.Native;

        public IAccessor Memory => __l_impl.Memory;

        public INativeStringManager<TCharIn> Strings => __l_impl.Strings;

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

        public IntPtr _T(string input, int extend)
        {
            return __l_impl._T(input, extend);
        }

        public IntPtr _T(string input)
        {
            return __l_impl._T(input);
        }

        public IntPtr _T<Tin>(string input, int extend) where Tin : struct
        {
            return __l_impl._T<Tin>(input, extend);
        }

        public IntPtr _T<Tin>(string input) where Tin : struct
        {
            return __l_impl._T<Tin>(input);
        }

        public IntPtr _T(string input, int extend, out TCharIn access)
        {
            return __l_impl._T(input, extend, out access);
        }

        public IntPtr _T(string input, out TCharIn access)
        {
            return __l_impl._T(input, out access);
        }

        public IntPtr _T<Tin>(string input, int extend, out Tin access) where Tin : struct
        {
            return __l_impl._T<Tin>(input, extend, out access);
        }

        public IntPtr _T<Tin>(string input, out Tin access) where Tin : struct
        {
            return __l_impl._T<Tin>(input, out access);
        }

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
