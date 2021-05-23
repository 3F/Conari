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
    public interface IAccessorUpdater<TretChain>
    {
        /// <summary>
        /// Update T type data using the current position that was before the previous reading
        /// such after <see cref="IAccessorReader{TretChain}.read{T}()"/>.
        /// </summary>
        /// <typeparam name="T">
        ///     <see cref="UIntPtr"/>, <see cref="IntPtr"/>, <see cref="UInt64"/>, 
        ///     <see cref="Int64"/>, <see cref="UInt32"/>, <see cref="Int32"/>, 
        ///     <see cref="UInt16"/>, <see cref="Int16"/>, <see cref="SByte"/>, 
        ///     <see cref="byte"/>, <see cref="char"/>, <see cref="achar"/>,
        ///     <see cref="wchar"/>
        /// </typeparam>
        /// <param name="input"></param>
        TretChain update<T>(T input) where T : struct;

        /// <inheritdoc cref="update{T}(T)"/>
        TretChain update<T>(params T[] input) where T : struct;

        /// <summary>
        /// Update <see cref="UIntPtr"/> using the current position that was before the previous reading
        /// such after <see cref="IAccessorReader{TretChain}.read{T}()"/>.
        /// </summary>
        TretChain updateUIntPtr(UIntPtr input);

        /// <summary>
        /// Update <see cref="IntPtr"/> using the current position that was before the previous reading
        /// such after <see cref="IAccessorReader{TretChain}.read{T}()"/>.
        /// </summary>
        TretChain updateIntPtr(IntPtr input);

        /// <summary>
        /// Update <see cref="UInt64"/> using the current position that was before the previous reading
        /// such after <see cref="IAccessorReader{TretChain}.read{T}()"/>.
        /// </summary>
        TretChain updateUInt64(UInt64 input);

        /// <summary>
        /// Update <see cref="Int64"/> using the current position that was before the previous reading
        /// such after <see cref="IAccessorReader{TretChain}.read{T}()"/>.
        /// </summary>
        TretChain updateInt64(Int64 input);

        /// <summary>
        /// Update <see cref="UInt32"/> using the current position that was before the previous reading
        /// such after <see cref="IAccessorReader{TretChain}.read{T}()"/>.
        /// </summary>
        TretChain updateUInt32(UInt32 input);

        /// <summary>
        /// Update <see cref="Int32"/> using the current position that was before the previous reading
        /// such after <see cref="IAccessorReader{TretChain}.read{T}()"/>.
        /// </summary>
        TretChain updateInt32(Int32 input);

        /// <summary>
        /// Update <see cref="UInt16"/> using the current position that was before the previous reading
        /// such after <see cref="IAccessorReader{TretChain}.read{T}()"/>.
        /// </summary>
        TretChain updateUInt16(UInt16 input);

        /// <summary>
        /// Update <see cref="Int16"/> using the current position that was before the previous reading
        /// such after <see cref="IAccessorReader{TretChain}.read{T}()"/>.
        /// </summary>
        TretChain updateInt16(Int16 input);

        /// <summary>
        /// Update <see cref="byte"/> using the current position that was before the previous reading
        /// such after <see cref="IAccessorReader{TretChain}.read{T}()"/>.
        /// </summary>
        TretChain updateByte(byte input);

        /// <summary>
        /// Update as a 8 bit character using the current position that was before the previous reading
        /// such after <see cref="IAccessorReader{TretChain}.read{T}()"/>.
        /// </summary>
        TretChain updateChar(char input);

        /// <summary>
        /// Update as a 16 bit character using the current position that was before the previous reading
        /// such after <see cref="IAccessorReader{TretChain}.read{T}()"/>.
        /// </summary>
        TretChain updateWChar(char input);

        /// <summary>
        /// Update either as a 16 bit or a 8 bit character 
        /// depending on the configuration (eg. <see cref="IAccessor.set(CharType)"/> etc),
        /// at current position that was before the previous reading such after <see cref="IAccessorReader{TretChain}.read{T}()"/>.
        /// </summary>
        TretChain updateTChar(char input);

        /// <summary>
        /// Update either as a 16 bit or a 8 bit character 
        /// depending on specified type, at current position that was before the previous reading
        /// such after <see cref="IAccessorReader{TretChain}.read{T}()"/>.
        /// </summary>
        /// <remarks>It will ignore configurations such as <see cref="IAccessor.set(CharType)"/> etc.</remarks>
        /// <param name="input"></param>
        /// <param name="enc"></param>
        TretChain updateChar(char input, CharType enc);

        /// <summary>
        /// Update <see cref="sbyte"/> using the current position that was before the previous reading
        /// such after <see cref="IAccessorReader{TretChain}.read{T}()"/>.
        /// </summary>
        TretChain updateSByte(sbyte input);
    }
}
