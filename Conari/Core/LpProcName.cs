/*!
 * Copyright (c) 2016  Denis Kuzmin <x-3F@outlook.com> github/3F
 * Copyright (c) Conari contributors https://github.com/3F/Conari/graphs/contributors
 * Licensed under the MIT License (MIT).
 * See accompanying LICENSE.txt file or visit https://github.com/3F/Conari
*/

using System.Diagnostics;

namespace net.r_eg.Conari.Core
{
    [DebuggerDisplay("{(string)this}")]
    public struct LpProcName
    {
        public string origin;
        public string prefixed;

        public static explicit operator string(LpProcName proc)
        {
            return proc.prefixed ?? proc.origin;
        }

        public static implicit operator LpProcName(string proc) => new(proc);

        public LpProcName(string origin, string prefixed = null)
        {
            this.origin     = origin;
            this.prefixed   = prefixed;
        }
    }
}
