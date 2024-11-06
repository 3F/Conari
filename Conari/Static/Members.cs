/*!
 * Copyright (c) 2016  Denis Kuzmin <x-3F@outlook.com> github/3F
 * Copyright (c) Conari contributors https://github.com/3F/Conari/graphs/contributors
 * Licensed under the MIT License (MIT).
 * See accompanying LICENSE.txt file or visit https://github.com/3F/Conari
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

        /// <inheritdoc cref="NativeExtension.NativeSize(Type)"/>
        public static int SizeOf<T>() => typeof(T).NativeSize();

        /// <inheritdoc cref="NativeExtension.NativeSize(Type, int)"/>
        public static int SizeOf<T>(int length) => typeof(T).NativeSize(length);

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
