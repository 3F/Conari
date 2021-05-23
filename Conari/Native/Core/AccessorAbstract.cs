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
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Text;
using net.r_eg.Conari.Exceptions;
using net.r_eg.Conari.Extension;
using net.r_eg.Conari.Types;

namespace net.r_eg.Conari.Native.Core
{
    using static Static.Members;

    [Serializable]
    public abstract class AccessorAbstract: PointerAbstract, IAccessor, ISerializable
    {
        protected CharType charType = CharType.OneByte;

        private bool? equaled;
        private VPtr firstEq;
        private bool hasTrueForOr;

        #region abstract reading

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

        #region abstract writing

        public abstract IAccessor writeByte(byte input);
        public abstract IAccessor writeSByte(sbyte input);
        public abstract IAccessor writeInt16(Int16 input);
        public abstract IAccessor writeUInt16(UInt16 input);
        public abstract IAccessor writeInt32(Int32 input);
        public abstract IAccessor writeUInt32(UInt32 input);
        public abstract IAccessor writeInt64(Int64 input);
        public abstract IAccessor writeUInt64(UInt64 input);
        public abstract IAccessor writeIntPtr(IntPtr input);
        public abstract IAccessor writeUIntPtr(UIntPtr input);

        #endregion

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public IAccessor D => move(0, Zone.D);

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public IAccessor U => move(0, Zone.U);

        /// <summary>
        /// Move to a new address using offset and specified region.
        /// </summary>
        /// <param name="offset">Offset for address. Can be negative.</param>
        /// <param name="region"></param>
        public virtual IAccessor move(long offset, Zone region = Zone.Current)
        {
            rewind(region);
            upPtr(offset);
            return this;
        }

        public virtual IAccessor @goto(VPtr addr)
        {
            CurrentPtr = addr;
            shiftRegionPtr();
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

        /// <inheritdoc cref="bytes{T}(int)"/>
        /// <param name="length">Length of data.</param>
        /// <param name="def">Optional definition of the input length in the chain.</param>
        public T[] bytes<T>(int length, out int def) where T : struct
        {
            def = length;
            return bytes<T>(length);
        }

        /// <inheritdoc cref="bytes{T}(int)"/>
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
        ///     <see cref="byte"/>, <see cref="char"/>, <see cref="achar"/>,
        ///     <see cref="wchar"/>
        /// </typeparam>
        /// <returns></returns>
        public T read<T>() where T: struct
        {
            Type t = typeof(T);

            if(typeof(byte) == t) return (dynamic)readByte();

            if(typeof(char) == t) return (dynamic)readChar();
            if(typeof(achar) == t) return (dynamic)readAChar();
            if(typeof(wchar) == t) return (dynamic)readWChar();

            if(typeof(Int32) == t) return (dynamic)readInt32();
            if(typeof(Int16) == t) return (dynamic)readInt16();
            if(typeof(Int64) == t) return (dynamic)readInt64();

            if(typeof(UInt32) == t) return (dynamic)readUInt32();
            if(typeof(UInt16) == t) return (dynamic)readUInt16();
            if(typeof(UInt64) == t) return (dynamic)readUInt64();

            if(typeof(IntPtr) == t) return (dynamic)readIntPtr();
            if(typeof(UIntPtr) == t) return (dynamic)readUIntPtr();

            if(typeof(SByte) == t) return (dynamic)readSByte();

            throw new NotSupportedException();
        }

        public IAccessor read<T>(out T readed) where T : struct
        {
            readed = read<T>();
            return this;
        }

        public IAccessor read<T>(int length, out T[] readed) where T : struct
        {
            var ret = new T[length];
            for(int i = 0; i < length; ++i)
            {
                ret[i] = read<T>();
            }

            readed = ret;
            return this;
        }

        public IAccessor next<T>(ref T readed) where T : struct
        {
            readed = read<T>();
            return this;
        }

        public IAccessor bytes<T>(int length, ref T[] readed) where T : struct
        {
            readed = bytes<T>(length);
            return this;
        }

        public virtual char readAChar() => Convert.ToChar(readByte());

        public virtual char readWChar() => Convert.ToChar(readUInt16());

        public virtual char readChar() => readChar(charType);

        public virtual char readChar(CharType enc) 
            => (enc == CharType.OneByte) ? readAChar() : readWChar();

        #region IAccessorWritter<IAccessor>

        public IAccessor write<T>(params T[] input) where T : struct
        {
            if(input == null) throw new ArgumentNullException(nameof(input));
            input.ForEach(v => write(v));
            return this;
        }

        public IAccessor write<T>(T input) where T : struct
        {
            Type t = typeof(T);

            if(typeof(byte) == t) return writeByte((dynamic)input);

            if(typeof(char) == t) return writeTChar((dynamic)input);
            if(typeof(achar) == t) return writeChar((dynamic)input);
            if(typeof(wchar) == t) return writeWChar((dynamic)input);

            if(typeof(Int32) == t) return writeInt32((dynamic)input);
            if(typeof(Int16) == t) return writeInt16((dynamic)input);
            if(typeof(Int64) == t) return writeInt64((dynamic)input);

            if(typeof(UInt32) == t) return writeUInt32((dynamic)input);
            if(typeof(UInt16) == t) return writeUInt16((dynamic)input);
            if(typeof(UInt64) == t) return writeUInt64((dynamic)input);

            if(typeof(IntPtr) == t) return writeIntPtr((dynamic)input);
            if(typeof(UIntPtr) == t) return writeUIntPtr((dynamic)input);

            if(typeof(SByte) == t) return writeSByte((dynamic)input);

            throw new NotSupportedException();
        }

        public IAccessor repeat<T>(int count, T input) where T : struct
        {
            for(int i = 0; i < count; ++i) write(input);
            return this;
        }

        public virtual IAccessor writeChar(char input)
        {
            write(getCharBytes(input));
            return this;
        }

        public virtual IAccessor writeWChar(char input)
        {
            write(getWideCharBytes(input));
            return this;
        }

        public virtual IAccessor writeTChar(char input) => writeChar(input, charType);

        public virtual IAccessor writeChar(char input, CharType enc)
            => (enc == CharType.OneByte) ? writeChar(input) : writeWChar(input);

        #endregion

        #region IAccessorUpdater<IAccessor>

        public IAccessor update<T>(params T[] input) where T : struct
        {
            if(input == null) throw new ArgumentNullException(nameof(input));

            back<T>(input.Length);
            input.ForEach(v => write(v));
            return this;
        }

        public IAccessor update<T>(T input) where T : struct
        {
            Type t = typeof(T);

            if(typeof(byte) == t) return updateByte((dynamic)input);

            if(typeof(char) == t) return updateTChar((dynamic)input);
            if(typeof(achar) == t) return updateChar((dynamic)input);
            if(typeof(wchar) == t) return updateWChar((dynamic)input);

            if(typeof(Int32) == t) return updateInt32((dynamic)input);
            if(typeof(Int16) == t) return updateInt16((dynamic)input);
            if(typeof(Int64) == t) return updateInt64((dynamic)input);

            if(typeof(UInt32) == t) return updateUInt32((dynamic)input);
            if(typeof(UInt16) == t) return updateUInt16((dynamic)input);
            if(typeof(UInt64) == t) return updateUInt64((dynamic)input);

            if(typeof(IntPtr) == t) return updateIntPtr((dynamic)input);
            if(typeof(UIntPtr) == t) return updateUIntPtr((dynamic)input);

            if(typeof(SByte) == t) return updateSByte((dynamic)input);

            throw new NotSupportedException();
        }

        public virtual IAccessor updateChar(char input)
        {
            update(getCharBytes(input));
            return this;
        }

        public virtual IAccessor updateWChar(char input)
        {
            update(getWideCharBytes(input));
            return this;
        }

        public virtual IAccessor updateTChar(char input) => updateChar(input, charType);

        public virtual IAccessor updateChar(char input, CharType enc)
            => (enc == CharType.OneByte) ? updateChar(input) : updateWChar(input);

        public virtual IAccessor updateByte(byte input) => back<byte>().writeByte(input);

        public virtual IAccessor updateSByte(sbyte input) => back<sbyte>().writeSByte(input);

        public virtual IAccessor updateInt16(short input) => back<short>().writeInt16(input);

        public virtual IAccessor updateUInt16(ushort input) => back<ushort>().writeUInt16(input);

        public virtual IAccessor updateInt32(int input) => back<int>().writeInt32(input);

        public virtual IAccessor updateUInt32(uint input) => back<uint>().writeUInt32(input);

        public virtual IAccessor updateInt64(long input) => back<long>().writeInt64(input);

        public virtual IAccessor updateUInt64(ulong input) => back<ulong>().writeUInt64(input);

        public virtual IAccessor updateIntPtr(IntPtr input) => back<IntPtr>().writeIntPtr(input);

        public virtual IAccessor updateUIntPtr(UIntPtr input) => back<UIntPtr>().writeUIntPtr(input);

        #endregion

        public IAccessor set(CharType enc)
        {
            charType = enc;
            return this;
        }

        public IAccessor eq<T>(params T[] input) where T : struct
        {
            input?.ForEach(x => eq(x));
            return this;
        }

        public IAccessor eq<T>(T input) where T: struct
        {
            if(hasTrueForOr || equaled == false)
            {
                upPtr(getDataSize<T>());
                return this;
            }

            if(equaled == null) firstEq = CurrentPtr;

            equaled = input.Equals(read<T>());
            return this;
        }

        public IAccessor or()
        {
            if(equaled != true)
            {
                CurrentPtr  = firstEq;
                equaled     = null;
            }
            else hasTrueForOr = true;
            return this;
        }

        public bool check()
        {
            bool ret        = equaled != false;
            equaled         = null;
            hasTrueForOr    = false;
            return ret;
        }

        public IAccessor ifTrue(Action<IAccessor> act)
        {
            if(check()) act?.Invoke(this);
            return this;
        }

        public IAccessor ifFalse(Action<IAccessor> act)
        {
            if(!check()) act?.Invoke(this);
            return this;
        }

        public IAccessor failed(bool when)
        {
            if(check() == when) throw new FailedCheckException(when);
            return this;
        }

        public IAccessor rewind(Zone region = Zone.Region)
        {
            if(region == Zone.Initial) {
                resetPtr();
            }
            else if(region == Zone.Region) {
                resetRegionPtr();
            }
            return this;
        }

        public IAccessor back<T>(int count = 1) => move(-(getDataSize<T>() * Math.Max(0, count)));

        public IAccessor back<T1, T2>() => move(-(getDataSize<T1>() + getDataSize<T2>()));

        public IAccessor back<T1, T2, T3>() => move(-(getDataSize<T1>() + getDataSize<T2>() + getDataSize<T3>()));

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            info.AddValue(nameof(charType), charType);
            info.AddValue(nameof(equaled), equaled);
            info.AddValue(nameof(firstEq), firstEq);
            info.AddValue(nameof(hasTrueForOr), hasTrueForOr);
        }

        public AccessorAbstract(VPtr ptr)
            : base(ptr)
        {

        }

        protected IAccessor atomicW<T>(Action<int> act)
        {
            atomicR<T>(x => { act(x); return default; });
            return this;
        }

        protected T atomicR<T>(Func<int, T> act)
        {
            int size = getDataSize<T>();
            T ret = act(size);

            upPtr(size);
            return ret;
        }

        protected virtual int getDataSize<T>()
        {
            Type input = typeof(T);

            if(input == typeof(wchar)) return 2;
            if(input == typeof(achar)) return 1;
            if(input == typeof(char))
            {
                return charType switch
                {
                    CharType.TwoByte => 2,
                    CharType.OneByte => 1,
                    _ => throw new NotImplementedException(),
                };
            }
            return SizeOf<T>();
        }

        protected virtual byte[] getCharBytes(char input)
        {
            return Encoding.ASCII.GetBytes(new[] { input });
        }

        protected virtual byte[] getWideCharBytes(char input)
        {
            return Encoding.Unicode.GetBytes(new[] { input });
        }
    }
}
