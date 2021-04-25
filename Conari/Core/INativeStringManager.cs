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
using net.r_eg.Conari.Types;

namespace net.r_eg.Conari.Core
{
    public interface INativeStringManager: IStringMaker, INativeStringManager<TCharPtr>
    {

    }

    public interface INativeStringManager<T>: IStringMaker<T>, IDisposable
        where T: struct
    {
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
