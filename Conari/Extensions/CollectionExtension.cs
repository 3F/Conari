/*!
 * Copyright (c) 2016  Denis Kuzmin <x-3F@outlook.com> github/3F
 * Copyright (c) Conari contributors https://github.com/3F/Conari/graphs/contributors
 * Licensed under the MIT License (MIT).
 * See accompanying LICENSE.txt file or visit https://github.com/3F/Conari
*/

using System;
using System.Collections.Generic;

namespace net.r_eg.Conari.Extension
{
    public static class CollectionExtension
    {
        /// <summary>
        /// Foreach in Linq manner.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <param name="act">The action that should be executed for each item.</param>
        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> items, Action<T> act)
        {
            if(items == null || act == null) return items;
            return items.ForEach((x, i) => act(x));
        }

        /// <summary>
        /// Foreach in Linq manner.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <param name="act">The action that should be executed for each item.</param>
        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> items, Action<T, long> act)
        {
            if(items == null || act == null) return items;

            long n = 0;
            foreach(var item in items)
            {
                act.Invoke(item, n++);
            }
            return items;
        }
    }
}
