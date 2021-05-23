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
using net.r_eg.Conari.Native.Core;

namespace net.r_eg.Conari.Native
{
    public static class NativeExtension
    {
        /// <summary>
        /// Accessing data via <see cref="Memory"/>.
        /// </summary>
        /// <param name="pointer">Some region.</param>
        /// <returns></returns>
        public static IAccessor Access(this IntPtr pointer) => new Memory(pointer);

        /// <summary>
        /// Accessing data via <see cref="LocalContent"/>.
        /// </summary>
        /// <param name="bytes">Some local raw data.</param>
        /// <returns></returns>
        public static IAccessor Access(this byte[] bytes) => new LocalContent(bytes);

        /// <summary>
        /// Initialize <see cref="NativeData"/> via pointer.
        /// </summary>
        /// <param name="pointer">Some region.</param>
        /// <returns></returns>
        public static NativeData Native(this IntPtr pointer) => new(pointer);

        /// <summary>
        /// Initialize <see cref="NativeData"/> via <see cref="IAccessor"/> implementation.
        /// </summary>
        /// <param name="accessor"></param>
        /// <returns></returns>
        public static NativeData Native(this IAccessor accessor) => new(accessor);

        /// <summary>
        /// Initialize <see cref="NativeData"/> via local bytes sequence.
        /// </summary>
        /// <param name="bytes">Some local raw data.</param>
        /// <returns></returns>
        public static NativeData Native(this byte[] bytes) => new(bytes);

        /// <summary>
        /// Get the size in bytes of the selected managed type to be treated as an unmanaged.
        /// </summary>
        /// <remarks>Alias to <see cref="NativeData.SizeOf(Type)"/>.</remarks>
        /// <param name="type"></param>
        /// <param name="length">Length of elements.</param>
        public static int NativeSize(this Type type, int length) => NativeSize(type) * Math.Max(0, length);

        /// <inheritdoc cref="NativeSize(Type, int)"/>
        public static int NativeSize(this Type type) => NativeData.SizeOf(type);

        /// <summary>
        /// Get the size in bytes of the selected managed object to be stored as an unmanaged.
        /// </summary>
        /// <param name="input">Input managed data. Can be array.</param>
        public static int NativeSize(this object input)
        {
            if(input == null) return 0;

            Type t = input.GetType();
            if(t.IsArray)
            {
                return t.GetElementType().NativeSize() * ((Array)input).Length;
            }
            return t.NativeSize();
        }
    }
}
