/*!
 * Copyright (c) 2016  Denis Kuzmin <x-3F@outlook.com> github/3F
 * Copyright (c) Conari contributors https://github.com/3F/Conari/graphs/contributors
 * Licensed under the MIT License (MIT).
 * See accompanying LICENSE.txt file or visit https://github.com/3F/Conari
*/

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using net.r_eg.Conari.Core;
using net.r_eg.Conari.Extension;
using net.r_eg.Conari.Native.Core;
using net.r_eg.Conari.Resources;
using net.r_eg.Conari.Types;

namespace net.r_eg.Conari.Native
{
    using Fields = List<Field>;

    public class NativeData: IDlrAccessor, IChain
    {
        protected Fields map = new();
        protected readonly IAccessor accessor;
        protected ChainMode chainMode = ChainMode.Fast;

        internal readonly ChainMeta meta = new();

        /// <summary>
        /// Access raw-data from the current chain to final build and other use.
        /// </summary>
        public Raw Raw => new(accessor, map, meta);

        /// <summary>
        /// Reset the whole chain. Alias to <see cref="reset(uint)"/> using default value.
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public NativeData Zero => reset(0);

        /// <summary>
        /// Access to the data via <see cref="IAccessor"/> implementations.
        /// Requires active pointer. Otherwise see <see cref="extend"/> method to continue the chain with bytes.
        /// </summary>
        public IAccessor Access => accessor;

        public dynamic DLR => Raw.DLR;

        public dynamic _ => Raw._;

        public int Size => meta.ChainSize;

        /// <summary>
        /// Build a new <see cref="NativeStruct"/> from the current chain.
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public NativeStruct Struct => new(this);

        /// <summary>
        /// Align by max size of existing types without changing of original types.
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public NativeData AlignSizeByMax => alignSizeBy(map.Max(m => m.tsize));

        /// <summary>
        /// Size of the selected types in bytes that must be considered as unmanaged types.
        /// </summary>
        /// <param name="types"></param>
        /// <returns>Native size in bytes.</returns>
        public static int SizeOf(params Type[] types) => types?.Sum(t => SizeOf(t)) ?? 0;

        /// <summary>
        /// Size of the selected type in bytes that must be considered as unmanaged type.
        /// </summary>
        /// <param name="type"></param>
        /// <returns>Native size in bytes.</returns>
        public static int SizeOf(Type type)
        {
            if(type == typeof(string) 
                || type == typeof(CharPtr)
                || type == typeof(TCharPtr)
#pragma warning disable CS0618 // Type or member is obsolete
                || type == typeof(BSTR)
#pragma warning restore CS0618 // Type or member is obsolete
                || type == typeof(WCharPtr)) 
            {
                type = typeof(IntPtr); // aligned pointer to allocated data: char* and so on.
            }

            return Marshal.SizeOf(type);
        }

        /// <inheritdoc cref="SizeOf(Type)"/>
        /// <remarks>Alias to <see cref="SizeOf(Type)"/></remarks>
        public static int SizeOf<T>() => SizeOf(typeof(T));

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
        /// Assigns (if it was null before) or Overrides custom field names for already specified fields in the chain.
        /// </summary>
        /// <param name="offset">Offset from the start of the chain.</param>
        /// <param name="names">The names, starting from specified offset.</param>
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
                    $"offset {offset} is not allowed. The names to assign: {names.Length} / chain: {map.Count}"
                );
            }

            return this;
        }

        /// <summary>
        /// Assigns new fields for each presented name.
        /// </summary>
        /// <typeparam name="T">Data type of the new field.</typeparam>
        /// <param name="names">Field names.</param>
        public NativeData assign<T>(params string[] names) => align<T>(names?.Length ?? 0, names);

        /// <inheritdoc cref="assign(string[])"/>
        /// <remarks>Alias to <see cref="assign(string[])"/>.</remarks>
        public NativeData f<T>(params string[] names) => assign<T>(names);

        /// <summary>
        /// Use offset to the data.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="index">The zero-based position of the data to be read.</param>
        /// <param name="names">Optional assigned names.</param>
        /// <returns></returns>
        public NativeData ofs<T>(int index, params string[] names)
        {
            align<T>(index);
            return f<T>(names);
        }

        /// <summary>
        /// To reset the chain.
        /// </summary>
        /// <param name="after">
        ///     Absolute position from the start of the chain. 
        ///     Eg.: count of the mapped fields such from `.t` and related.
        /// </param>
        public NativeData reset(uint after = 0)
        {
            map = new Fields(
                map.Take(unchecked((int)after))
            );
            meta.resetChainSize();

            int totalChainSize = map.Sum(x => x.tsize);

            if(meta.FlaggedChainSize > totalChainSize) {
                meta.resetFlaggedChainSize();
            }
            accessor.resetRegionPtr();

            meta.updateSize(totalChainSize, ignoreFlagged: true);
            return this;
        }

        /// <summary>
        /// Extends local data using additional bytes.
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public NativeData extend(params byte[] bytes)
        {
            if(bytes == null) throw new ArgumentNullException(nameof(bytes));
            if(accessor is not LocalContent) throw new NotSupportedException($"Current chain was initialized as a `{accessor}` but this requires {nameof(LocalContent)} based accessor");

            ((ILocalContent)accessor).extend(bytes);
            return this;
        }

        /// <summary>
        /// Set new chain mode.
        /// </summary>
        /// <param name="def"></param>
        /// <returns></returns>
        public NativeData mode(ChainMode def)
        {
            chainMode = def;
            return this;
        }

        /// <summary>
        /// An additional way to start the chain with specific <see cref="Zone"/>.
        /// </summary>
        /// <param name="seek"></param>
        public NativeData renew(Zone seek = Zone.Current)
        {
            switch(seek)
            {
                case Zone.Current:  { accessor.shiftRegionPtr(); break; }
                case Zone.Region:   { accessor.resetRegionPtr(); break; }
                case Zone.Initial:  { accessor.resetPtr(); break; }
                default: throw new NotImplementedException();
            }
            return reset();
        }

        /// <inheritdoc cref="renew(Zone)"/>
        /// <param name="position">Returns current position after renew chain.</param>
        /// <param name="seek"></param>
        public NativeData renew(out VPtr position, Zone seek = Zone.Current)
        {
            renew(seek);
            position = Access.CurrentPtr;
            return this;
        }

        /// <inheritdoc cref="Raw.build()"/>
        /// <remarks>Alias to <see cref="Raw.build()"/></remarks>
        public BType build() => Raw.build();

        /// <remarks>Chained <see cref="build()"/></remarks>
        /// <inheritdoc cref="build()"/>
        public NativeData build(out dynamic result)
        {
            result = build();
            return renew();
        }

        /// <summary>
        /// Mark new region in the chain.
        /// </summary>
        /// <returns></returns>
        public NativeData region()
        {
            meta.resetFlaggedChainSize();
            return this;
        }

        /// <inheritdoc cref="region()"/>
        /// <param name="current">Produced data size in the chain to the current point.</param>
        public NativeData region(out int current)
        {
            current = meta.ChainSize;
            return region();
        }

        /// <inheritdoc cref="region()"/>
        /// <param name="addr">Intended address in memory to the current data in the chain.</param>
        public NativeData region(out VPtr addr)
        {
            addr = accessor.getPtrFrom(meta.ChainSize);
            return region();
        }

        /// <summary>
        /// To add complex type from header file (.h)
        /// </summary>
        /// <param name="file">Full path to header file.</param>
        /// <param name="typedef">The name of declared type that should be loaded.</param>
        /// <returns></returns>
        public NativeData h(string file, string typedef)
        {
            throw new NotImplementedException(Msg.not_yet_impl);
        }

        public NativeData t(Type type, string name = null) { return _a(track(name, type)); }
        public NativeData t(Type[] types, params string[] names) { return _a(track(names, types)); }
        public NativeData t(int size, string name = null) { return _a(track(name, size)); }

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

#if F_PREDEFINED_NATIVEDATA_T32

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

#endif

        public static explicit operator Memory(NativeData v) => (Memory)v.accessor;
        public static explicit operator LocalContent(NativeData v) => (LocalContent)v.accessor;
        public static explicit operator NativeStream(NativeData v) => (NativeStream)v.accessor;
        public static implicit operator VPtr(NativeData v) => v.accessor.CurrentPtr;

        /// <param name="ptr">pointer to data structure.</param>
        public NativeData(IntPtr ptr)
            : this(ptr != IntPtr.Zero ? new Memory(ptr) : throw new ArgumentException(Msg.arg_pointer_zero))
        {

        }

        public NativeData(IAccessor accessor)
        {
            this.accessor = accessor ?? throw new ArgumentNullException(nameof(accessor));
            accessor.shiftRegionPtr();
        }

        /// <param name="bytes">local raw data.</param>
        public NativeData(byte[] bytes)
            : this(new LocalContent(bytes))
        {

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
            addField(new Field(type, size) {
                name = fieldName(name)
            });

            return meta.updateSize(size);
        }

        protected virtual int track(string name, int size)
        {
            addField(new Field(typeof(byte[]), size) {
                name = fieldName(name)
            });

            return meta.updateSize(size);
        }

        protected virtual string fieldName(string name)
        {
            if(name == null) {
                return null;
            }

            // TODO: now all fields contains IsValidForDLR property, and probably this is more flexible way...
            //       If use flags ('Sys | Core | Spec' or something else), we need to extract context that is more overhead.
            //       thus, currently we'll leave it as is.

            //string rule = "^[a-zA-Z_][a-zA-Z_0-9]*$";
            //if(!Regex.Match(name, rule).Success) {
            //    throw new ArgumentException($"The name `{name}` is not valid as field name. Rule: {rule}");
            //}

            return name;
        }

        protected void addField(Field fld)
        {
            if(fld.name == null)
            {
                map.Add(fld);
                return;
            }

            switch(chainMode)
            {
                case ChainMode.Fast: break;
                case ChainMode.Updating:
                {
                    map.Where(f => f.name == fld.name).ForEach(f => f.name = null);
                    break;
                }
                case ChainMode.Exception:
                {
                    if(map.Any(f => f.name == fld.name)) {
                        throw new ArgumentException(Msg.field_0_defined_mode_1.Format($"{fld.name}", $"{chainMode}"));
                    } 
                    break;
                }
            }

            map.Add(fld);
            return;
        }

        private NativeData _a(int _)
        {
            // ...
            return this;
        }
    }
}
