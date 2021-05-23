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

namespace net.r_eg.Conari.Native.Core
{
    public interface IAccessorReader<TretChain>
    {
        /// <summary>
        /// Read unsigned bytes, signed bytes, or chars using current possition.
        /// </summary>
        /// <typeparam name="T"><see cref="byte"/>, <see cref="sbyte"/>, <see cref="char"/></typeparam>
        /// <param name="length">Length of data.</param>
        /// <returns></returns>
        T[] bytes<T>(int length) where T : struct;

        /// <inheritdoc cref="bytes{T}(int)"/>
        /// <param name="length">Length of data.</param>
        /// <param name="def">Optional definition of the input length in the chain.</param>
        T[] bytes<T>(int length, out int def) where T : struct;

        /// <inheritdoc cref="bytes{T}(int)"/>
        byte[] bytes(int length);

        /// <inheritdoc cref="bytes(int)"/>
        /// <param name="length">Length of data.</param>
        /// <param name="def">Optional definition of the input length in the chain.</param>
        byte[] bytes(int length, out int def);

        /// <summary>
        /// Read T type data using current possition.
        /// </summary>
        /// <typeparam name="T">
        ///     <see cref="UIntPtr"/>, <see cref="IntPtr"/>, <see cref="UInt64"/>, 
        ///     <see cref="Int64"/>, <see cref="UInt32"/>, <see cref="Int32"/>, 
        ///     <see cref="UInt16"/>, <see cref="Int16"/>, <see cref="SByte"/>, 
        ///     <see cref="byte"/>, <see cref="char"/>, <see cref="achar"/>,
        ///     <see cref="wchar"/>
        /// </typeparam>
        /// <returns></returns>
        T read<T>() where T : struct;

        /// <inheritdoc cref="read{T}(int, out T[])"/>
        TretChain read<T>(out T readed) where T : struct;

        /// <inheritdoc cref="read{T}()"/>
        /// <remarks>Chained <see cref="read{T}()"/>.</remarks>
        /// <param name="length">The length of the elements to read.</param>
        /// <param name="readed">The result of the reading.</param>
        TretChain read<T>(int length, out T[] readed) where T : struct;

        /// <inheritdoc cref="read{T}(out T)"/>
        TretChain next<T>(ref T readed) where T : struct;

        /// <inheritdoc cref="read{T}(int, out T[])"/>
        TretChain bytes<T>(int length, ref T[] readed) where T : struct;

        UIntPtr readUIntPtr();

        IntPtr readIntPtr();

        UInt64 readUInt64();

        Int64 readInt64();

        UInt32 readUInt32();

        Int32 readInt32();

        UInt16 readUInt16();

        Int16 readInt16();

        byte readByte();

        /// <summary>
        /// Read as a 8 bit character.
        /// </summary>
        char readAChar();

        /// <summary>
        /// Read as a 16 bit character.
        /// </summary>
        char readWChar();

        /// <summary>
        /// Read either as a 16 bit or a 8 bit character 
        /// depending on the configuration (eg. <see cref="IAccessor.set(CharType)"/> etc).
        /// </summary>
        char readChar();

        /// <summary>
        /// Read either as a 16 bit or a 8 bit character 
        /// depending on specified type.
        /// </summary>
        /// <remarks>It will ignore configurations such as <see cref="IAccessor.set(CharType)"/> etc.</remarks>
        /// <param name="type"></param>
        char readChar(CharType type);

        sbyte readSByte();
    }
}
