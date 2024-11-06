/*!
 * Copyright (c) 2016  Denis Kuzmin <x-3F@outlook.com> github/3F
 * Copyright (c) Conari contributors https://github.com/3F/Conari/graphs/contributors
 * Licensed under the MIT License (MIT).
 * See accompanying LICENSE.txt file or visit https://github.com/3F/Conari
*/

using System;
using net.r_eg.Conari.Exceptions;
using net.r_eg.Conari.Types;

namespace net.r_eg.Conari.Core
{
    public interface INativeStringManager: IStringMaker, INativeStringManager<TCharPtr>
    {

    }

    public interface INativeStringManager<T>: IStringMaker<T>, IDisposable
        where T: struct
    {
        /// <inheritdoc cref="add{Tin}(NativeString{Tin})"/>
        void add(NativeString<T> input);

        /// <summary>
        /// Manage a <see cref="NativeString{Tin}"/> instance.
        /// </summary>
        /// <typeparam name="Tin">Supported type for <see cref="NativeString{T}"/>.</typeparam>
        /// <exception cref="AbandonedPointerException"/>
        void add<Tin>(NativeString<Tin> input) where Tin : struct;

        /// <inheritdoc cref="cstr{Tin}(string, int, out Tin)"/>
        NativeString<T> cstr(string input, int extend);

        /// <inheritdoc cref="cstr{Tin}(string, int, out Tin)"/>
        NativeString<T> cstr(string input);

        /// <inheritdoc cref="cstr{Tin}(string, int, out Tin)"/>
        NativeString<Tin> cstr<Tin>(string input, int extend) where Tin : struct;

        /// <inheritdoc cref="cstr{Tin}(string, int, out Tin)"/>
        NativeString<Tin> cstr<Tin>(string input) where Tin : struct;

        /// <inheritdoc cref="cstr{Tin}(string, int, out Tin)"/>
        NativeString<T> cstr(string input, int extend, out T access);

        /// <inheritdoc cref="cstr{Tin}(string, int, out Tin)"/>
        NativeString<T> cstr(string input, out T access);

        /// <summary>
        /// Allocate a new C-string using <see cref="NativeString{T}"/>.
        /// </summary>
        /// <remarks>
        /// It does not require disposing beyond of the <see cref="INativeStringManager{T}"/> implementation.
        /// <br/><see cref="TCharPtr"/> is the default type.
        /// </remarks>
        /// <typeparam name="Tin">Supported type for <see cref="NativeString{T}"/>.</typeparam>
        /// <param name="input">Managed a Unicode UTF-16 string.</param>
        /// <param name="extend">Allocate additional buffer in bytes. Do not include a null terminator.</param>
        /// <param name="access">Referencing a new allocated C-string using processed type.</param>
        /// <returns>Access to a <see cref="NativeString{T}"/> instance.</returns>
        NativeString<Tin> cstr<Tin>(string input, int extend, out Tin access) where Tin : struct;

        /// <inheritdoc cref="cstr{Tin}(string, int, out Tin)"/>
        NativeString<Tin> cstr<Tin>(string input, out Tin access) where Tin : struct;

        /// <summary>
        /// Release all stored strings if any.
        /// It will NOT dispose current object. Only frees memory from old strings.
        /// </summary>
        INativeStringManager<T> release();

        /// <summary>
        /// Release specified stored string.
        /// </summary>
        /// <param name="pointer">Pointer to allocated C-string in memory.</param>
        INativeStringManager<T> release(IntPtr pointer);
    }
}
