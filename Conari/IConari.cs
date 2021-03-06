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
