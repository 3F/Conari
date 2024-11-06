/*!
 * Copyright (c) 2016  Denis Kuzmin <x-3F@outlook.com> github/3F
 * Copyright (c) Conari contributors https://github.com/3F/Conari/graphs/contributors
 * Licensed under the MIT License (MIT).
 * See accompanying LICENSE.txt file or visit https://github.com/3F/Conari
*/

using System;
using net.r_eg.Conari.Core;
using net.r_eg.Conari.Log;
using net.r_eg.Conari.Types;

namespace net.r_eg.Conari
{
    public interface IConari: IConari<TCharPtr>
    {

    }

    public interface IConari<TCharIn>: IBinder, IProvider, ILoader, INativeAccessor, IStringMaker<TCharIn>, IDisposable
        where TCharIn: struct
    {
        /// <summary>
        /// Access to used string manager.
        /// </summary>
        INativeStringManager<TCharIn> Strings { get; }

        /// <summary>
        /// Access to available configuration data of dynamic DLR.
        /// </summary>
        IProviderDLR ConfigDLR { get; }

        /// <summary>
        /// Dynamic features such as invoking of the exported functions 
        /// and getting exported variables at runtime.
        /// </summary>
        dynamic DLR { get; }

        /// <summary>
        /// Access to logger and its events.
        /// </summary>
        ISender Log { get; }

        /// <summary>
        /// DLR Features with `__cdecl` calling convention.
        /// </summary>
        dynamic __cdecl { get; }

        /// <summary>
        /// DLR Features with `__stdcall` calling convention.
        /// </summary>
        dynamic __stdcall { get; }

        /// <summary>
        /// DLR Features with `__fastcall` calling convention.
        /// </summary>
        dynamic __fastcall { get; }

        /// <summary>
        /// DLR Features with `__vectorcall` calling convention.
        /// </summary>
        dynamic __vectorcall { get; }
    }
}
