/*
 * The MIT License (MIT)
 *
 * Copyright (c) 2016-2020  Denis Kuzmin < x-3F@outlook.com > GitHub/3F
 * Copyright (c) Conari contributors: https://github.com/3F/Conari/graphs/contributors
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

namespace net.r_eg.Conari.Native
{
    public static class NativeExtension
    {
        /// <summary>
        /// To work with native data via pointer.
        /// </summary>
        /// <param name="ptr">pointer to data structure.</param>
        /// <returns></returns>
        public static NativeData Native(this IntPtr ptr)
        {
            return NativeData._(ptr);
        }

        /// <summary>
        /// To work with native data via byte-array.
        /// </summary>
        /// <param name="bytes">local raw data.</param>
        /// <returns></returns>
        public static NativeData Native(this byte[] bytes)
        {
            return NativeData._(bytes);
        }

        /// <summary>
        /// Alias to `NativeData.SizeOf ...`
        /// Gets size of selected type in bytes that's should be considered as unmanaged type.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static int NativeSize(this Type type)
        {
            return NativeData.SizeOf(type);
        }
    }
}
