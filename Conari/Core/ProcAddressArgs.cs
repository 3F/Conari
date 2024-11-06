/*!
 * Copyright (c) 2016  Denis Kuzmin <x-3F@outlook.com> github/3F
 * Copyright (c) Conari contributors https://github.com/3F/Conari/graphs/contributors
 * Licensed under the MIT License (MIT).
 * See accompanying LICENSE.txt file or visit https://github.com/3F/Conari
*/

using System;

namespace net.r_eg.Conari.Core
{
    [Serializable]
    public class ProcAddressArgs: EventArgs
    {
        /// <summary>
        /// The address of the exported function or variable.
        /// </summary>
        public IntPtr PAddr
        {
            get;
            protected set;
        }

        /// <summary>
        /// A handle of used module.
        /// </summary>
        public IntPtr Handle
        {
            get;
            protected set;
        }

        /// <summary>
        /// The function or variable name, or the function's ordinal value.
        /// 
        /// If this parameter is an ordinal value, it must be in the low-order word;
        /// the high-order word must be zero.
        /// </summary>
        public string LPProcName
        {
            get;
            protected set;
        }

        public ProcAddressArgs(IntPtr pAddr, IntPtr handle, string lpProcName)
        {
            PAddr       = pAddr;
            Handle      = handle;
            LPProcName  = lpProcName;
        }
    }
}
