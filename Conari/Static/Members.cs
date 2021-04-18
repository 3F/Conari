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
using net.r_eg.Conari.Native;

namespace net.r_eg.Conari.Static
{
    public static class Members
    {
        /// <summary>
        /// 32-bit or 64-bit addressing in the current process?
        /// </summary>
        public static bool Is64bit => IntPtr.Size == sizeof(Int64);

        /// <inheritdoc cref="NativeData.SizeOf(Type)"/>
        public static int SizeOf<T>() => NativeData.SizeOf(typeof(T));

#if NET40

        public static T[] EmptyArray<T>() => _EmptyArray<T>.value;

        private static class _EmptyArray<T>
        {
            public static readonly T[] value = new T[0];
        }

#else

        public static T[] EmptyArray<T>() => Array.Empty<T>();

#endif

    }
}
