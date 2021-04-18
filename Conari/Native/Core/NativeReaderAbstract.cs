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
    using static Static.Members;

    public abstract class NativeReaderAbstract: PointerAbstract, INativeReader
    {
        #region abstract reading

        public abstract char readChar();
        public abstract byte readByte();
        public abstract sbyte readSByte();
        public abstract Int16 readInt16();
        public abstract UInt16 readUInt16();
        public abstract Int32 readInt32();
        public abstract UInt32 readUInt32();
        public abstract Int64 readInt64();
        public abstract UInt64 readUInt64();
        public abstract IntPtr readIntPtr();
        public abstract UIntPtr readUIntPtr();

        #endregion

        /// <summary>
        /// Move to a new address using offset and specified region.
        /// </summary>
        /// <param name="offset">Offset for address. Can be negative.</param>
        /// <param name="region"></param>
        public virtual INativeReader move(long offset, SeekPosition region = SeekPosition.Current)
        {
            if(region == SeekPosition.Initial) {
                resetPtr();
            }
            else if(region == SeekPosition.Region) {
                resetRegionPtr();
            }
            upPtr(offset);
            return this;
        }

        /// <summary>
        /// Read unsigned bytes, signed bytes, or chars using current possition.
        /// </summary>
        /// <typeparam name="T"><see cref="byte"/>, <see cref="sbyte"/>, <see cref="char"/></typeparam>
        /// <param name="length">Length of data.</param>
        /// <returns></returns>
        public T[] bytes<T>(int length) where T: struct
        {
            Type t = typeof(T);
            if(typeof(char) != t && typeof(byte) != t && typeof(sbyte) != t)
            {
                throw new NotSupportedException($"Only byte, sbyte, and char types are allowed.");
            }

            var ret = new T[length];
            for(int i = 0; i < length; ++i)
            {
                if(typeof(byte) == t)  { ret[i] = (dynamic)readByte(); continue; }
                if(typeof(sbyte) == t) { ret[i] = (dynamic)readSByte(); continue; }
                if(typeof(char) == t)  { ret[i] = (dynamic)Convert.ToChar(readByte()); continue; }
            }

            return ret;
        }

        /// <inheritdoc cref="bytes&lt;T&gt;(int)"/>
        /// <param name="length">Length of data.</param>
        /// <param name="def">Optional definition of the input length in the chain.</param>
        public T[] bytes<T>(int length, out int def) where T : struct
        {
            def = length;
            return bytes<T>(length);
        }

        /// <inheritdoc cref="bytes&lt;T&gt;(int)"/>
        public byte[] bytes(int length) => bytes<byte>(length);

        /// <inheritdoc cref="bytes(int)"/>
        /// <param name="length">Length of data.</param>
        /// <param name="def">Optional definition of the input length in the chain.</param>
        public byte[] bytes(int length, out int def)
        {
            def = length;
            return bytes(length);
        }

        /// <summary>
        /// Read T type data using current possition.
        /// </summary>
        /// <typeparam name="T">
        ///     <see cref="UIntPtr"/>, <see cref="IntPtr"/>, <see cref="UInt64"/>, 
        ///     <see cref="Int64"/>, <see cref="UInt32"/>, <see cref="Int32"/>, 
        ///     <see cref="UInt16"/>, <see cref="Int16"/>, <see cref="SByte"/>, 
        ///     <see cref="byte"/>, <see cref="char"/>
        /// </typeparam>
        /// <returns></returns>
        public T read<T>() where T: struct
        {
            Type t = typeof(T);

            if(typeof(UIntPtr) == t) return (dynamic)readUIntPtr();
            if(typeof(IntPtr) == t) return (dynamic)readIntPtr();
            if(typeof(UInt64) == t) return (dynamic)readUInt64();
            if(typeof(Int64) == t) return (dynamic)readInt64();
            if(typeof(UInt32) == t) return (dynamic)readUInt32();
            if(typeof(Int32) == t) return (dynamic)readInt32();
            if(typeof(UInt16) == t) return (dynamic)readUInt16();
            if(typeof(Int16) == t) return (dynamic)readInt16();
            if(typeof(SByte) == t) return (dynamic)readSByte();
            if(typeof(byte) == t) return (dynamic)readByte();
            if(typeof(char) == t) return (dynamic)readChar();

            throw new NotSupportedException();
        }

        public INativeReader read<T>(out T readed) where T : struct
        {
            readed = read<T>();
            return this;
        }

        public INativeReader read<T>(int length, out T[] readed) where T : struct
        {
            readed = bytes<T>(length);
            return this;
        }

        public INativeReader next<T>(ref T readed) where T : struct
        {
            readed = read<T>();
            return this;
        }

        public INativeReader bytes<T>(int length, ref T[] readed) where T : struct
        {
            readed = bytes<T>(length);
            return this;
        }

        public NativeReaderAbstract(VPtr ptr)
            : base(ptr)
        {

        }

        protected T atomic<T>(Func<int, T> act)
        {
            int size = SizeOf<T>();
            try
            {
                return act.Invoke(size);
            }
            finally
            {
                upPtr(size);
            }
        }
    }
}
