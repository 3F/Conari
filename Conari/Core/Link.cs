/*!
 * Copyright (c) 2016  Denis Kuzmin <x-3F@outlook.com> github/3F
 * Copyright (c) Conari contributors https://github.com/3F/Conari/graphs/contributors
 * Licensed under the MIT License (MIT).
 * See accompanying LICENSE.txt file or visit https://github.com/3F/Conari
*/

using System;
using net.r_eg.Conari.Types;

namespace net.r_eg.Conari.Core
{
    public struct Link
    {
        /// <summary>
        /// Used module (.dll, .exe, or address)
        /// </summary>
        public string module;

        /// <summary>
        /// Points to actual isolated module if true.
        /// </summary>
        public bool isolated;

        /// <summary>
        /// Cancelled or timeout when loading.
        /// </summary>
        public bool cancelled;

        /// <summary>
        /// A handle of loaded module.
        /// </summary>
        internal IntPtr handle;

        /// <summary>
        /// An resolved file status of the used module.
        /// </summary>
        internal readonly WRef<bool> resolved;

        public bool IsActive => handle != IntPtr.Zero;

        [Obsolete("Use {module} field instead.")]
        public string LibName => module;

        public static explicit operator IntPtr(Link v) => v.handle;

        public override string ToString() => module;

        public Link(IntPtr handle, string module, bool isolated = false)
            : this()
        {
            this.handle     = handle;
            this.module     = module;
            this.isolated   = isolated;

            resolved = new WRef<bool>(false);
        }

        public Link(string module)
            : this(IntPtr.Zero, module)
        {

        }
    }
}
