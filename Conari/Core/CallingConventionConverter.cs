/*!
 * Copyright (c) 2016  Denis Kuzmin <x-3F@outlook.com> github/3F
 * Copyright (c) Conari contributors https://github.com/3F/Conari/graphs/contributors
 * Licensed under the MIT License (MIT).
 * See accompanying LICENSE.txt file or visit https://github.com/3F/Conari
*/

using System.Runtime.InteropServices;

namespace net.r_eg.Conari.Core
{
    /// <summary>
    /// For internal m_signature processing with unmanaged EmitCalli.
    /// See Provider.
    /// </summary>
    internal struct CallingConventionConverter
    {
        public static MdSigCallingConvention GetValue(CallingConvention conv)
        {
            switch(conv)
            {
                case CallingConvention.Cdecl: {
                    return MdSigCallingConvention.C;
                }

                case CallingConvention.Winapi:
                case CallingConvention.StdCall: {
                    return MdSigCallingConvention.StdCall;
                }

                case CallingConvention.ThisCall: {
                    return MdSigCallingConvention.ThisCall;
                }

                case CallingConvention.FastCall: {
                    return MdSigCallingConvention.FastCall;
                }
            }

            return MdSigCallingConvention.Default;
        }

        public static byte GetMdSigCallingConventionAsByte(CallingConvention conv) => (byte)GetValue(conv);
    }
}
