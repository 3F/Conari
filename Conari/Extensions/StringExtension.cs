﻿/*
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

namespace net.r_eg.Conari.Extension
{
    public static class StringExtension
    {
        /// <inheritdoc cref="Do{T}(string, out T, int?)"/>
        public static NativeString<CharPtr> Do(this string input, out CharPtr result)
            => Do<CharPtr>(input, out result, null);

        /// <inheritdoc cref="Do{T}(string, out T, int?)"/>
        public static NativeString<WCharPtr> Do(this string input, out WCharPtr result)
            => Do<WCharPtr>(input, out result, null);

        /// <inheritdoc cref="Do{T}(string, out T, int?)"/>
        public static NativeString<TCharPtr> Do(this string input, out TCharPtr result)
            => Do<TCharPtr>(input, out result, null);

        /// <inheritdoc cref="Do{T}(string, out T, int?)"/>
        public static NativeString<CharPtr> Do(this string input, out CharPtr result, int extend)
            => Do<CharPtr>(input, out result, extend);

        /// <inheritdoc cref="Do{T}(string, out T, int?)"/>
        public static NativeString<WCharPtr> Do(this string input, out WCharPtr result, int extend)
            => Do<WCharPtr>(input, out result, extend);

        /// <inheritdoc cref="Do{T}(string, out T, int?)"/>
        public static NativeString<TCharPtr> Do(this string input, out TCharPtr result, int extend)
            => Do<TCharPtr>(input, out result, extend);

        /// <summary>
        /// Do modifiable string to catch the result.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <param name="result">Updated string value.</param>
        /// <param name="extend">Use an additional buffer.</param>
        /// <returns></returns>
        internal static NativeString<T> Do<T>(this string input, out T result, int? extend) where T: struct
        {
            NativeString<T> ns = new(input, extend ?? input.RelativeLength(BufferedString<T>.BUF)) {
                UseManager = true
            };
            result = (dynamic)ns;
            return ns;
        }

        internal static int RelativeLength(this string input, float percent)
            => (int)Math.Ceiling((input?.Length ?? 0) * percent);
    }
}
