/*!
 * Copyright (c) 2016  Denis Kuzmin <x-3F@outlook.com> github/3F
 * Copyright (c) Conari contributors https://github.com/3F/Conari/graphs/contributors
 * Licensed under the MIT License (MIT).
 * See accompanying LICENSE.txt file or visit https://github.com/3F/Conari
*/

using System;
using net.r_eg.Conari.Types;

namespace net.r_eg.Conari.Core
{
    public interface IStringMaker: IStringMaker<TCharPtr>
    {

    }

    public interface IStringMaker<T>: IDisposable
        where T: struct
    {
        /// <inheritdoc cref="_T{Tin}(string, int, out Tin)"/>
        IntPtr _T(string input, int extend);

        /// <inheritdoc cref="_T{Tin}(string, int, out Tin)"/>
        IntPtr _T(string input);

        /// <inheritdoc cref="_T{Tin}(string, int, out Tin)"/>
        IntPtr _T<Tin>(string input, int extend) where Tin : struct;

        /// <inheritdoc cref="_T{Tin}(string, int, out Tin)"/>
        IntPtr _T<Tin>(string input) where Tin : struct;

        /// <inheritdoc cref="_T{Tin}(string, int, out Tin)"/>
        IntPtr _T(string input, int extend, out T access);

        /// <inheritdoc cref="_T{Tin}(string, int, out Tin)"/>
        IntPtr _T(string input, out T access);

        /// <summary>
        /// Return pointer to a new allocated C-string using <see cref="NativeString{T}"/>.
        /// </summary>
        /// <remarks><see cref="TCharPtr"/> is the default type.</remarks>
        /// <typeparam name="Tin">Supported type for <see cref="NativeString{T}"/>.</typeparam>
        /// <param name="input">Managed a Unicode UTF-16 string.</param>
        /// <param name="extend">Allocate additional buffer in bytes. Do not include a null terminator.</param>
        /// <param name="access">Referencing a new allocated C-string using processed type.</param>
        IntPtr _T<Tin>(string input, int extend, out Tin access) where Tin : struct;

        /// <inheritdoc cref="_T{Tin}(string, int, out Tin)"/>
        IntPtr _T<Tin>(string input, out Tin access) where Tin : struct;
    }
}
