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

    [DebuggerDisplay("(uint_t) = {(ulong)val} [ UIntPtr.Size: {System.UIntPtr.Size} ]")]
    public struct size_t
    {
        private uint_t val;

        public static implicit operator uint_t(size_t number)
        {
            return number.val;
        }

        public static implicit operator UIntPtr(size_t number)
        {
            if(number.val.ActualSize == uint_t.SIZE_IU64) {
                return new UIntPtr((UInt64)number.val);
            }
            return new UIntPtr((UInt32)number.val);
        }

        public static implicit operator size_t(uint_t number)
        {
            return new size_t(number);
        }

        public static implicit operator size_t(UIntPtr ptr)
        {
            if(Is64bit) {
                return new size_t(ptr.ToUInt64());
            }
            return new size_t(ptr.ToUInt32());
        }

        public static implicit operator long(size_t number)
        {
            return number.val;
        }

        public static implicit operator size_t(ulong number)
        {
            return new size_t(number);
        }

        public static implicit operator int(size_t number)
        {
            return number.val;
        }

        // we also use this to initialize the uint_t as the uint type
        public static implicit operator size_t(uint number)
        {
            return new size_t((uint_t)number);
        }

        public size_t(uint_t number)
        {
            val = number;
        }
    }
}
