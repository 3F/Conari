/*!
 * Copyright (c) 2016  Denis Kuzmin <x-3F@outlook.com> github/3F
 * Copyright (c) Conari contributors https://github.com/3F/Conari/graphs/contributors
 * Licensed under the MIT License (MIT).
 * See accompanying LICENSE.txt file or visit https://github.com/3F/Conari
*/

using System;
using System.Runtime.InteropServices;
using net.r_eg.Conari.Core;

namespace net.r_eg.Conari.Accessors.WinAPI
{
    /// <summary>
    /// user32 via Conari engine [DLR version]: 
    /// https://github.com/3F/Conari
    /// https://docs.microsoft.com/en-us/windows/win32/api/winuser/
    /// </summary>
    public sealed class User32: ConariX
    {
        private const CallingConvention __v_conv = CallingConvention.Winapi;
        private const string __v_module = "user32";

        public override CallingConvention Convention
        {
            get => __v_conv;
            set => throw new NotSupportedException();
        }

        /// <summary>
        /// Initialize user32 via Conari engine.
        /// </summary>
        /// <param name="cfg">Custom configuration. Module cannot be overridden.</param>
        public User32(IConfig cfg)
            : base(DefConf(cfg), __v_conv, null)
        {

        }

        /// <summary>
        /// Initialize user32 via Conari engine.
        /// </summary>
        public User32()
            : this(DefConf())
        {

        }

        private static IConfig DefConf(IConfig cfg = null)
        {
            if(cfg == null) {
                cfg = new Config();
            }

            cfg.Module = __v_module;
            return cfg;
        }
    }
}
