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

    public sealed class BReader
    {
        private byte[] data;
        private int offset;

        /// <summary>
        /// To get manually the value from byte-sequence.
        /// </summary>
        /// <param name="type">The type of result value.</param>
        /// <param name="tsize">Actual size of value in byte-sequence.</param>
        /// <param name="offset">Offset at left.</param>
        /// <param name="data"></param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException"></exception>
        public static dynamic GetValue(Type type, int tsize, ref int offset, ref byte[] data)
        {
            // the System.IO.BinaryReader as variant, but we will use own implementation ...

            byte[] val  = range(ref data, offset, tsize);
            offset      += tsize;

            if(type == typeof(Byte[])) {
                return (Byte[])val;
            }

            if(type == typeof(Byte)) {
                return (Byte)val[0];
            }

            if(type == typeof(SByte)) {
                return (SByte)val[0];
            }

            if(type == typeof(Int16)) {
                return BitConverter.ToInt16(val, 0);
            }

            if(type == typeof(UInt16)) {
                return BitConverter.ToUInt16(val, 0);
            }

            if(type == typeof(Int32)) {
                return BitConverter.ToInt32(val, 0);
            }

            if(type == typeof(UInt32)) {
                return BitConverter.ToUInt32(val, 0);
            }

            if(type == typeof(Int64)) {
                return BitConverter.ToInt64(val, 0);
            }

            if(type == typeof(UInt64)) {
                return BitConverter.ToUInt64(val, 0);
            }

            if(type == typeof(IntPtr)) {
                return asIntPtr(val);
            }

            if(type == typeof(UIntPtr)) {
                return asUIntPtr(val);
            }

            if(type == typeof(Single)) {
                return BitConverter.ToSingle(val, 0);
            }

            if(type == typeof(Double)) {
                return BitConverter.ToDouble(val, 0);
            }

            if(type == typeof(Boolean)) {
                return BitConverter.ToBoolean(val, 0);
            }

            if(type == typeof(Char)) {
                return BitConverter.ToChar(val, 0);
            }

            if(type == typeof(string) 
                || type == typeof(CharPtr)
                || type == typeof(TCharPtr)
#pragma warning disable CS0618 // Type or member is obsolete
                || type == typeof(BSTR)
#pragma warning restore CS0618 // Type or member is obsolete
                || type == typeof(WCharPtr)) 
            {
                //return new Types.CharPtr((IntPtr)field.value);
                return asIntPtr(val);
            }

            throw new NotSupportedException($"The type `{type}` is not supported.");
        }

        /// <summary>
        /// Try to get the next value in byte-sequence.
        /// </summary>
        /// <param name="type">The type of value.</param>
        /// <param name="tsize">Actual size of value.</param>
        /// <param name="value">Output value.</param>
        /// <returns></returns>
        public bool tryNext(Type type, int tsize, out dynamic value)
        {
            if(offset + tsize > data.Length) {
                value = null;
                return false;
            }

            value = GetValue(type, tsize, ref offset, ref data);
            return true;
        }

        /// <summary>
        /// Gets next value in byte-sequence.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="tsize"></param>
        /// <returns></returns>
        /// <exception cref="IndexOutOfRangeException"></exception>
        public dynamic next(Type type, int tsize)
        {
            if(tryNext(type, tsize, out dynamic value)) {
                return value;
            }

            throw new IndexOutOfRangeException(
                $"The type `{type}` with selected size '{tsize}' cannot be read from byte-sequence. Offset: {offset}"
            );
        }

        /// <summary>
        /// Alias to `next(Type type, int tsize)`
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tsize"></param>
        /// <returns></returns>
        public dynamic next<T>(int tsize)
        {
            return next(typeof(T), tsize);
        }

        /// <summary>
        /// Gets next value in byte-sequence with size of type by default.
        /// </summary>
        /// <param name="type">The type of value.</param>
        /// <returns></returns>
        public dynamic next(Type type)
        {
            return next(type, NativeData.SizeOf(type));
        }

        /// <summary>
        /// Alias to `next(Type type)`
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public dynamic next<T>()
        {
            return next(typeof(T));
        }

        /// <summary>
        /// To reset position.
        /// </summary>
        /// <param name="offset"></param>
        public void reset(int offset = 0)
        {
            this.offset = offset;
        }

        public BReader(byte[] raw, int offset = 0)
        {
            data        = raw;
            this.offset = offset;
        }

        private static IntPtr asIntPtr(byte[] val)
        {
            if(Is64bit) {
                return (IntPtr)BitConverter.ToInt64(val, 0);
            }
            return (IntPtr)BitConverter.ToInt32(val, 0);
        }

        private static UIntPtr asUIntPtr(byte[] val)
        {
            if(Is64bit) {
                return (UIntPtr)BitConverter.ToUInt64(val, 0);
            }
            return (UIntPtr)BitConverter.ToUInt32(val, 0);
        }

        private static byte[] range(ref byte[] data, int offset, int len)
        {
            int idx = 0;
            byte[] ret = new byte[Math.Max(0, len)];

            len = offset + len;
            while(offset < len) {
                ret[idx++] = data[offset++];
            }
            return ret;
        }
    }
}
