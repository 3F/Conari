/*!
 * Copyright (c) 2016  Denis Kuzmin <x-3F@outlook.com> github/3F
 * Copyright (c) Conari contributors https://github.com/3F/Conari/graphs/contributors
 * Licensed under the MIT License (MIT).
 * See accompanying LICENSE.txt file or visit https://github.com/3F/Conari
*/

using System;
using System.Text;
using net.r_eg.Conari.Extension;
using net.r_eg.Conari.Native;

namespace net.r_eg.Conari.Exceptions
{
    [Serializable]
    public class AbandonedPointerException: CommonException
    {
        public AbandonedPointerException(IntPtr ptr)
            : base($"Abandoned pointer at 0x{ptr.ToString("x")}. Dump (hex): {TryDump(ptr)}")
        {

        }

        protected static string TryDump(IntPtr ptr)
        {
            try
            {
                StringBuilder sb = new();
                ptr.Access().bytes(8).ForEach(b => sb.Append($"{b:x2} "));
                return sb.Append("...").ToString();
            }
            catch(Exception) // can't be addressed anymore
            {
                return "?<...>";
            }
        }
    }
}
