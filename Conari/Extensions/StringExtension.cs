/*!
 * Copyright (c) 2016  Denis Kuzmin <x-3F@outlook.com> github/3F
 * Copyright (c) Conari contributors https://github.com/3F/Conari/graphs/contributors
 * Licensed under the MIT License (MIT).
 * See accompanying LICENSE.txt file or visit https://github.com/3F/Conari
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
