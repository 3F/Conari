/*!
 * Copyright (c) 2016  Denis Kuzmin <x-3F@outlook.com> github/3F
 * Copyright (c) Conari contributors https://github.com/3F/Conari/graphs/contributors
 * Licensed under the MIT License (MIT).
 * See accompanying LICENSE.txt file or visit https://github.com/3F/Conari
*/

using System;
using net.r_eg.Conari.Native;

namespace net.r_eg.Conari.Core
{
    public interface IProviderSvc
    {
        /// <summary>
        /// Retrieves the address of an exported function or variable.
        /// </summary>
        /// <param name="lpProcName">The name of function or variable, or the function's ordinal value.</param>
        /// <returns>The address if found.</returns>
        IntPtr getProcAddr(LpProcName lpProcName);

        /// <summary>
        /// Prepare NativeData for active provider.
        /// </summary>
        /// <param name="lpProcName">The name of function or variable, or the function's ordinal value.</param>
        /// <returns></returns>
        NativeData native(LpProcName lpProcName);

        /// <summary>
        /// Prepare NativeData for active provider.
        /// </summary>
        /// <param name="lpProcName">The name of function or variable, or the function's ordinal value.</param>
        /// <param name="prefix">Add prefix if used.</param>
        /// <returns></returns>
        NativeData native(string lpProcName, bool prefix = false);

        /// <summary>
        /// Extracts LpProcName.
        /// </summary>
        /// <param name="lpProcName">Original lpProcName.</param>
        /// <param name="prefix">Add prefix if used.</param>
        /// <returns></returns>
        LpProcName procName(string lpProcName, bool prefix);

        /// <summary>
        /// Try to get alias.
        /// </summary>
        /// <param name="name">Possible alias name.</param>
        /// <returns></returns>
        string tryAlias(string name);
    }
}
