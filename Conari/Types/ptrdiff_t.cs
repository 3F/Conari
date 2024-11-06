/*!
 * Copyright (c) 2016  Denis Kuzmin <x-3F@outlook.com> github/3F
 * Copyright (c) Conari contributors https://github.com/3F/Conari/graphs/contributors
 * Licensed under the MIT License (MIT).
 * See accompanying LICENSE.txt file or visit https://github.com/3F/Conari
*/

using System;
using System.Diagnostics;

namespace net.r_eg.Conari.Types
{
    using static Static.Members;

    [DebuggerDisplay("(int_t) = {(long)val} [ IntPtr.Size: {System.IntPtr.Size} ]")]
    public struct ptrdiff_t
    {
        private int_t val;

        public static implicit operator int_t(ptrdiff_t number)
        {
            return number.val;
        }

        public static implicit operator IntPtr(ptrdiff_t number)
        {
            if(number.val.ActualSize == int_t.SIZE_I64) {
                return new IntPtr((Int64)number.val);
            }
            return new IntPtr((Int32)number.val);
        }

        public static implicit operator ptrdiff_t(int_t number)
        {
            return new ptrdiff_t(number);
        }

        public static implicit operator ptrdiff_t(IntPtr ptr)
        {
            if(Is64bit) {
                return new ptrdiff_t(ptr.ToInt64());
            }
            return new ptrdiff_t(ptr.ToInt32());
        }

        public static implicit operator long(ptrdiff_t number)
        {
            return number.val;
        }

        public static implicit operator ptrdiff_t(long number)
        {
            return new ptrdiff_t(number);
        }

        public static implicit operator int(ptrdiff_t number)
        {
            return number.val;
        }

        // we also use this to initialize the int_t as the int type
        public static implicit operator ptrdiff_t(int number)
        {
            return new ptrdiff_t(number);
        }

        public ptrdiff_t(int_t number)
        {
            val = number;
        }
    }
}
