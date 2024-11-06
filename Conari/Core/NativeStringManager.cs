/*!
 * Copyright (c) 2016  Denis Kuzmin <x-3F@outlook.com> github/3F
 * Copyright (c) Conari contributors https://github.com/3F/Conari/graphs/contributors
 * Licensed under the MIT License (MIT).
 * See accompanying LICENSE.txt file or visit https://github.com/3F/Conari
*/

using System;
using System.Collections.Concurrent;
using net.r_eg.Conari.Exceptions;
using net.r_eg.Conari.Extension;
using net.r_eg.Conari.Types;

namespace net.r_eg.Conari.Core
{
    public class NativeStringManager: NativeStringManager<TCharPtr>, 
                                        IStringMaker, 
                                        IStringMaker<TCharPtr>, 
                                        INativeStringManager, 
                                        INativeStringManager<TCharPtr>, 
                                        IDisposable
    {

    }

    public class NativeStringManager<T>: IStringMaker<T>, INativeStringManager<T>, IDisposable
        where T: struct
    {
        protected readonly ConcurrentDictionary<IntPtr, IDisposable> strings = new();

        protected virtual float DefaultBuffer => 0.8f;

        public void add(NativeString<T> input) => addString(input);

        public void add<Tin>(NativeString<Tin> input) where Tin : struct
            => addString(input);

        public IntPtr _T(string input, int extend) => cstr<T>(input, extend);

        public IntPtr _T(string input) => cstr<T>(input);

        public IntPtr _T<Tin>(string input, int extend) where Tin : struct
            => cstr<Tin>(input, extend);

        public IntPtr _T<Tin>(string input) where Tin : struct
            => cstr<Tin>(input);

        public IntPtr _T(string input, int extend, out T access) 
            => cstr<T>(input, extend, out access);

        public IntPtr _T(string input, out T access) 
            => cstr<T>(input, input.RelativeLength(DefaultBuffer), out access);

        public IntPtr _T<Tin>(string input, int extend, out Tin access) where Tin : struct
            => cstr<Tin>(input, extend, out access);

        public IntPtr _T<Tin>(string input, out Tin access) where Tin : struct
            => cstr<Tin>(input, input.RelativeLength(DefaultBuffer), out access);

        public NativeString<T> cstr(string input, int extend) => cstr<T>(input, extend);

        public NativeString<T> cstr(string input) => cstr<T>(input);

        public NativeString<Tin> cstr<Tin>(string input, int extend) where Tin : struct
        {
            NativeString<Tin> ns = new(input, extend);
            addString(ns);
            return ns;
        }

        public NativeString<Tin> cstr<Tin>(string input) where Tin : struct
        {
            NativeString<Tin> ns = new(input);
            addString(ns);
            return ns;
        }

        public NativeString<T> cstr(string input, int extend, out T access) 
            => cstr<T>(input, extend, out access);

        public NativeString<T> cstr(string input, out T access) 
            => cstr<T>(input, input.RelativeLength(DefaultBuffer), out access);

        public NativeString<Tin> cstr<Tin>(string input, int extend, out Tin access) where Tin : struct
        {
            NativeString<Tin> ns = new(input, extend);
            addString(ns);
            access = (dynamic)ns;
            return ns;
        }

        public NativeString<Tin> cstr<Tin>(string input, out Tin access) where Tin : struct
            => cstr<Tin>(input, input.RelativeLength(DefaultBuffer), out access);

        public INativeStringManager<T> release()
        {
            strings.ForEach(s => { s.Value.Dispose(); strings[s.Key] = null; });
            strings.Clear();
            return this;
        }

        public INativeStringManager<T> release(IntPtr pointer)
        {
            if(strings.ContainsKey(pointer))
            {
                strings[pointer].Dispose();
                strings[pointer] = null;
                strings.TryRemove(pointer, out _);
            }
            return this;
        }

        protected virtual void addString<Tin>(NativeString<Tin> ns) where Tin : struct
        {
            if(!strings.TryAdd(ns, ns))
            {
                throw new AbandonedPointerException(ns);
            }
        }

        #region IDisposable

        private bool disposed;

        protected virtual void Dispose(bool _)
        {
            if(!disposed)
            {
                release();
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
