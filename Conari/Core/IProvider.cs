/*!
 * Copyright (c) 2016  Denis Kuzmin <x-3F@outlook.com> github/3F
 * Copyright (c) Conari contributors https://github.com/3F/Conari/graphs/contributors
 * Licensed under the MIT License (MIT).
 * See accompanying LICENSE.txt file or visit https://github.com/3F/Conari
*/

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using net.r_eg.Conari.Aliases;

namespace net.r_eg.Conari.Core
{
    public interface IProvider: IBinder, IMem
    {
        /// <summary>
        /// When Prefix has been changed.
        /// </summary>
        event EventHandler<DataArgs<string>> PrefixChanged;

        /// <summary>
        /// When Convention has been changed.
        /// </summary>
        event EventHandler<DataArgs<CallingConvention>> ConventionChanged;

        /// <summary>
        /// When handling new non-zero ProcAddress.
        /// </summary>
        event EventHandler<ProcAddressArgs> NewProcAddress;

        /// <summary>
        /// To cache delegates, generated methods, etc.
        /// </summary>
        bool Cache { get; set; }

        /// <summary>
        /// Prefix for exported functions.
        /// </summary>
        string Prefix { get; set; }

        /// <summary>
        /// How should call methods implemented in unmanaged code.
        /// </summary>
        CallingConvention Convention { get; set; }

        /// <summary>
        /// Auto name-decoration to find entry points of exported functions.
        /// </summary>
        bool Mangling { get; set; }

        /// <summary>
        /// The aliases for exported-functions and variables.
        /// </summary>
        Dictionary<string, ProcAlias> Aliases { get; }

        /// <summary>
        /// Access to exported variables.
        /// </summary>
        IExVar ExVar { get; }

        /// <summary>
        /// Additional services.
        /// </summary>
        IProviderSvc Svc { get; }

        /// <summary>
        /// Returns full lpProcName with main prefix etc.
        /// </summary>
        /// <param name="name">Exported function or variable name.</param>
        string procName(string name);

        /// <summary>
        /// Returns address of the specific item such streams std::cin etc.
        /// Related: https://github.com/3F/Conari/issues/17
        /// </summary>
        IntPtr addr(LpProcName item);
    }
}
