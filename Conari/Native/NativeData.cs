/*
 * The MIT License (MIT)
 * 
 * Copyright (c) 2016  Denis Kuzmin <entry.reg@gmail.com>
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
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using net.r_eg.Conari.Native.Core;

namespace net.r_eg.Conari.Native
{
    using Fields = List<Field>;

    public class NativeData
    {
        protected Fields map = new Fields();
        protected byte[] local;
        private IntPtr pointer;

        /// <summary>
        /// Get raw-data of complex native structure.
        /// </summary>
        /// <returns></returns>
        public Raw Raw
        {
            get
            {
                if(pointer == IntPtr.Zero) {
                    return new Raw(local, map);
                }
                return new Raw(pointer, map);
            }
        }

        /// <summary>
        /// To reset chain in zero.
        /// </summary>
        public NativeData Zero
        {
            get {
                reset();
                return this;
            }
        }

        /// <summary>
        /// Align by max size of existing types without changing of original types.
        /// </summary>
        public NativeData AlignSizeByMax
        {
            get {
                return alignSizeBy(map.Max(m => m.tsize));
            }
        }

        /// <summary>
        /// Gets size of selected types in bytes that are should be considered as unmanaged types.
        /// </summary>
        /// <param name="types"></param>
        /// <returns>the size in bytes.</returns>
        public static int SizeOf(params Type[] types)
        {
            int sum = 0;
            foreach(Type t in types) {
                sum += SizeOf(t);
            }
            return sum;
        }
        
        /// <summary>
        /// Gets size of selected type in bytes that's should be considered as unmanaged type.
        /// </summary>
        /// <param name="type"></param>
        /// <returns>the size in bytes.</returns>
        public static int SizeOf(Type type)
        {
            if(type == typeof(string)) {
                type = typeof(IntPtr); // i.e. we will use pointer to allocated data, like char* etc.
            }

            return Marshal.SizeOf(type);
        }

        /// <summary>
        /// Alias to `int SizeOf(Type type)`
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static int SizeOf<T>()
        {
            return SizeOf(typeof(T));
        }

        /// <summary>
        /// Align size by specific type.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="count">the count of types that should be in chain.</param>
        /// <returns></returns>
        public static int AlignSizeBy(Type type, int count)
        {
            if(count < 1) {
                throw new ArgumentException("Count of types should be >= 1");
            }
            return SizeOf(type) * count;
        }

        /// <summary>
        /// Alias to get instance: `new NativeData(IntPtr)`
        /// </summary>
        /// <param name="ptr">pointer to data structure.</param>
        public static NativeData _(IntPtr ptr)
        {
            return new NativeData(ptr);
        }

        /// <summary>
        /// Alias to get instance: `new NativeData(byte[])`
        /// </summary>
        /// <param name="bytes">local raw data.</param>
        public static NativeData _(byte[] bytes)
        {
            return new NativeData(bytes);
        }

        /// <summary>
        /// Align the chain by specific type at the right.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="count">the count of T that should be in chain.</param>
        /// <param name="names">Optional assigned names.</param>
        /// <returns></returns>
        public NativeData align<T>(int count, params string[] names)
        {
            for(int i = 0; i < count; ++i)
            {
                string name = (i < names.Length) ? names[i] : null;
                track(name, typeof(T));
            }

            return this;
        }

        /// <summary>
        /// Align by specific size without changing of original types.
        /// </summary>
        public NativeData alignSizeBy(int size)
        {
            map.ForEach(m => m.tsize = size);
            return this;
        }

        /// <summary>
        /// To assign custom names.
        /// </summary>
        /// <param name="offset">Initial offset for present data with.</param>
        /// <param name="names">The names, starting from offset.</param>
        /// <returns></returns>
        public NativeData assign(int offset, params string[] names)
        {
            for(int i = 0; i < names.Length; ++i)
            {
                offset += i;

                if(offset >= 0 && offset < map.Count) {
                    map[offset].name = fieldName(names[i]);
                    continue;
                }

                throw new ArgumentException(
                    $"offset {offset} is not available. The names to assign: {names.Length} / chain: {map.Count}"
                );
            }

            return this;
        }

        /// <summary>
        /// To reset chain.
        /// </summary>
        /// <param name="pos">absolute position.</param>
        public void reset(uint pos = 0)
        {
            map = new Fields(
                map.Take((int)pos)
            );
        }

        /// <summary>
        /// To add complex type from header file (.h)
        /// </summary>
        /// <param name="file">Full path to header file.</param>
        /// <param name="typedef">The name of declared type that should be loaded.</param>
        /// <returns></returns>
        public NativeData h(string file, string typedef)
        {
            throw new NotImplementedException("Not yet implemented.");
        }

        public NativeData t<T>(string name = null) { return _a(track(name, typeof(T))); }
        public NativeData t<T, T2>(params string[] names) { return _a(track(names, typeof(T), typeof(T2))); }
        public NativeData t<T, T2, T3>(params string[] names) { return _a(track(names, typeof(T), typeof(T2), typeof(T3))); }
        public NativeData t<T, T2, T3, T4>(params string[] names) { return _a(track(names, typeof(T), typeof(T2), typeof(T3), typeof(T4))); }
        public NativeData t<T, T2, T3, T4, T5>(params string[] names) { return _a(track(names, typeof(T), typeof(T2), typeof(T3), typeof(T4), typeof(T5))); }
        public NativeData t<T, T2, T3, T4, T5, T6>(params string[] names) { return _a(track(names, typeof(T), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6))); }
        public NativeData t<T, T2, T3, T4, T5, T6, T7>(params string[] names) { return _a(track(names, typeof(T), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7))); }
        public NativeData t<T, T2, T3, T4, T5, T6, T7, T8>(params string[] names) { return _a(track(names, typeof(T), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8))); }
        public NativeData t<T, T2, T3, T4, T5, T6, T7, T8, T9>(params string[] names) { return _a(track(names, typeof(T), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9))); }
        public NativeData t<T, T2, T3, T4, T5, T6, T7, T8, T9, T10>(params string[] names) { return _a(track(names, typeof(T), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10))); }
        public NativeData t<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(params string[] names) { return _a(track(names, typeof(T), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11))); }
        public NativeData t<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(params string[] names) { return _a(track(names, typeof(T), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12))); }
        public NativeData t<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(params string[] names) { return _a(track(names, typeof(T), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13))); }
        public NativeData t<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(params string[] names) { return _a(track(names, typeof(T), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13), typeof(T14))); }
        public NativeData t<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(params string[] names) { return _a(track(names, typeof(T), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13), typeof(T14), typeof(T15))); }
        public NativeData t<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(params string[] names) { return _a(track(names, typeof(T), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13), typeof(T14), typeof(T15), typeof(T16))); }
        public NativeData t<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17>(params string[] names) { return _a(track(names, typeof(T), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13), typeof(T14), typeof(T15), typeof(T16), typeof(T17))); }
        public NativeData t<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18>(params string[] names) { return _a(track(names, typeof(T), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13), typeof(T14), typeof(T15), typeof(T16), typeof(T17), typeof(T18))); }
        public NativeData t<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19>(params string[] names) { return _a(track(names, typeof(T), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13), typeof(T14), typeof(T15), typeof(T16), typeof(T17), typeof(T18), typeof(T19))); }
        public NativeData t<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20>(params string[] names) { return _a(track(names, typeof(T), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13), typeof(T14), typeof(T15), typeof(T16), typeof(T17), typeof(T18), typeof(T19), typeof(T20))); }
        public NativeData t<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21>(params string[] names) { return _a(track(names, typeof(T), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13), typeof(T14), typeof(T15), typeof(T16), typeof(T17), typeof(T18), typeof(T19), typeof(T20), typeof(T21))); }
        public NativeData t<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22>(params string[] names) { return _a(track(names, typeof(T), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13), typeof(T14), typeof(T15), typeof(T16), typeof(T17), typeof(T18), typeof(T19), typeof(T20), typeof(T21), typeof(T22))); }
        public NativeData t<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23>(params string[] names) { return _a(track(names, typeof(T), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13), typeof(T14), typeof(T15), typeof(T16), typeof(T17), typeof(T18), typeof(T19), typeof(T20), typeof(T21), typeof(T22), typeof(T23))); }
        public NativeData t<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24>(params string[] names) { return _a(track(names, typeof(T), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13), typeof(T14), typeof(T15), typeof(T16), typeof(T17), typeof(T18), typeof(T19), typeof(T20), typeof(T21), typeof(T22), typeof(T23), typeof(T24))); }
        public NativeData t<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25>(params string[] names) { return _a(track(names, typeof(T), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13), typeof(T14), typeof(T15), typeof(T16), typeof(T17), typeof(T18), typeof(T19), typeof(T20), typeof(T21), typeof(T22), typeof(T23), typeof(T24), typeof(T25))); }
        public NativeData t<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26>(params string[] names) { return _a(track(names, typeof(T), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13), typeof(T14), typeof(T15), typeof(T16), typeof(T17), typeof(T18), typeof(T19), typeof(T20), typeof(T21), typeof(T22), typeof(T23), typeof(T24), typeof(T25), typeof(T26))); }
        public NativeData t<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27>(params string[] names) { return _a(track(names, typeof(T), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13), typeof(T14), typeof(T15), typeof(T16), typeof(T17), typeof(T18), typeof(T19), typeof(T20), typeof(T21), typeof(T22), typeof(T23), typeof(T24), typeof(T25), typeof(T26), typeof(T27))); }
        public NativeData t<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28>(params string[] names) { return _a(track(names, typeof(T), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13), typeof(T14), typeof(T15), typeof(T16), typeof(T17), typeof(T18), typeof(T19), typeof(T20), typeof(T21), typeof(T22), typeof(T23), typeof(T24), typeof(T25), typeof(T26), typeof(T27), typeof(T28))); }
        public NativeData t<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29>(params string[] names) { return _a(track(names, typeof(T), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13), typeof(T14), typeof(T15), typeof(T16), typeof(T17), typeof(T18), typeof(T19), typeof(T20), typeof(T21), typeof(T22), typeof(T23), typeof(T24), typeof(T25), typeof(T26), typeof(T27), typeof(T28), typeof(T29))); }
        public NativeData t<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30>(params string[] names) { return _a(track(names, typeof(T), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13), typeof(T14), typeof(T15), typeof(T16), typeof(T17), typeof(T18), typeof(T19), typeof(T20), typeof(T21), typeof(T22), typeof(T23), typeof(T24), typeof(T25), typeof(T26), typeof(T27), typeof(T28), typeof(T29), typeof(T30))); }
        public NativeData t<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31>(params string[] names) { return _a(track(names, typeof(T), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13), typeof(T14), typeof(T15), typeof(T16), typeof(T17), typeof(T18), typeof(T19), typeof(T20), typeof(T21), typeof(T22), typeof(T23), typeof(T24), typeof(T25), typeof(T26), typeof(T27), typeof(T28), typeof(T29), typeof(T30), typeof(T31))); }
        public NativeData t<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32>(params string[] names) { return _a(track(names, typeof(T), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13), typeof(T14), typeof(T15), typeof(T16), typeof(T17), typeof(T18), typeof(T19), typeof(T20), typeof(T21), typeof(T22), typeof(T23), typeof(T24), typeof(T25), typeof(T26), typeof(T27), typeof(T28), typeof(T29), typeof(T30), typeof(T31), typeof(T32))); }

        /// <param name="ptr">pointer to data structure.</param>
        public NativeData(IntPtr ptr)
        {
            pointer = ptr;
        }

        /// <param name="bytes">local raw data.</param>
        public NativeData(byte[] bytes)
        {
            local = bytes;
        }

        protected int track(string[] names, params Type[] types)
        {
            int sum = 0;
            for(int i = 0; i < types.Length; ++i)
            {
                string name = (i < names.Length) ? names[i] : null;
                sum += track(name, types[i]);
            }

            return sum;
        }

        protected virtual int track(string name, Type type)
        {
            int size = SizeOf(type);
            map.Add(new Field(type, size) { name = fieldName(name) });

            return size;
        }

        protected virtual string fieldName(string name)
        {
            if(name == null) {
                return null;
            }

            string rule = "^[a-zA-Z_][a-zA-Z_0-9]*$";
            if(!Regex.Match(name, rule).Success) {
                throw new ArgumentException($"The name `{name}` is not valid as field name. Rule: {rule}");
            }
            return name;
        }

        private NativeData _a(int len)
        {
            // ...
            return this;
        }
    }
}
