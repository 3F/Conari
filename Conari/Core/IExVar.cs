/*!
 * Copyright (c) 2016  Denis Kuzmin <x-3F@outlook.com> github/3F
 * Copyright (c) Conari contributors https://github.com/3F/Conari/graphs/contributors
 * Licensed under the MIT License (MIT).
 * See accompanying LICENSE.txt file or visit https://github.com/3F/Conari
*/

using System;

namespace net.r_eg.Conari.Core
{
    using DefaultType = Int32;

    public interface IExVar: IDlrAccessor
    {
        /// <summary>
        /// Gets value from exported Variable. Full name is required.
        /// </summary>
        /// <typeparam name="T">The type of variable.</typeparam>
        /// <param name="lpProcName">The full name of exported variable.</param>
        /// <returns>The value from exported variable.</returns>
        T getVar<T>(string lpProcName);

        /// <summary>
        /// Alias to <see cref="getVar{T}(string)"/>
        /// Gets value from exported Variable. Full name is required.
        /// </summary>
        /// <param name="lpProcName">The full name of exported variable.</param>
        /// <returns>The value from exported variable.</returns>
        DefaultType getVar(string lpProcName);

        /// <summary>
        /// Gets value from exported Variable.
        /// The main prefix will affects on this result.
        /// </summary>
        /// <typeparam name="T">The type of variable.</typeparam>
        /// <param name="variable">The name of exported variable.</param>
        /// <returns>The value from exported variable.</returns>
        T get<T>(string variable);

        /// <summary>
        /// Alias to <see cref="get{T}(string)"/>
        /// 
        /// Gets value from exported Variable.
        /// The main prefix will affects on this result.
        /// </summary>
        /// <param name="variable">The name of exported variable.</param>
        /// <returns>The value from exported variable.</returns>
        DefaultType get(string variable);

        /// <summary>
        /// Get field with native data from export table.
        /// Uses type for information about data.
        /// </summary>
        /// <param name="type">To consider it as this type.</param>
        /// <param name="name">The name of record.</param>
        /// <returns></returns>
        Native.Core.Field getField(Type type, string name);

        /// <summary>
        /// Alias to `getField(Type type, string name)`
        /// 
        /// Get field with native data from export table.
        /// Uses type for information about data.
        /// </summary>
        /// <typeparam name="T">To consider it as T type.</typeparam>
        /// <param name="name">The name of record.</param>
        /// <returns></returns>
        Native.Core.Field getField<T>(string name);

        /// <summary>
        /// Get field with native data from export table.
        /// Uses size of unspecified unmanaged type in bytes. 
        /// To calculate it from managed types, see: `NativeData.SizeOf`
        /// </summary>
        /// <param name="size">The size of raw-data in bytes.</param>
        /// <param name="name">The name of record.</param>
        /// <returns></returns>
        Native.Core.Field getField(int size, string name);
    }
}
