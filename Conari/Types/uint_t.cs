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
    /// <summary>
    /// unsigned int 
    /// /or/ 
    /// unsigned __int64 (unsigned long long)
    /// </summary>
    [DebuggerDisplay(" = {val} [ ActualSize: {ActualSize} ]")]
    public struct uint_t
    {
        public const int SIZE_IU64  = sizeof(UInt64);
        public const int SIZE_IU32  = sizeof(UInt32);
        public const int SIZE_IU16  = sizeof(UInt16);
        public const int SIZE_IU8   = sizeof(Byte);

        public const UInt64 MAX = UInt64.MaxValue;
        public const UInt64 MIN = UInt64.MinValue;

        private readonly UInt64 val;

        public int ActualSize
        {
            get;
            private set;
        }

        public static implicit operator UInt64(uint_t number)
        {
            return number.val;
        }

        /// <exception cref="OverflowException">number is greater than Int32.MaxValue</exception>
        public static implicit operator UInt32(uint_t number)
        {
            return Convert.ToUInt32(number.val);
        }

        /// <exception cref="OverflowException">number is greater than Int16.MaxValue</exception>
        public static implicit operator UInt16(uint_t number)
        {
            return Convert.ToUInt16(number.val);
        }

        /// <exception cref="OverflowException">number is greater than Byte.MaxValue</exception>
        public static implicit operator Byte(uint_t number)
        {
            return Convert.ToByte(number.val);
        }

        /// <exception cref="OverflowException">number is greater than int_t.MAX</exception>
        public static explicit operator int_t(uint_t number)
        {
            return new int_t(number);
        }

        public static implicit operator uint_t(UInt64 number)
        {
            return new uint_t(number, SIZE_IU64);
        }

        public static implicit operator uint_t(UInt32 number)
        {
            return new uint_t(number, SIZE_IU32);
        }

        public static implicit operator uint_t(UInt16 number)
        {
            return new uint_t(number, SIZE_IU16);
        }

        public static implicit operator uint_t(Byte number)
        {
            return new uint_t(number, SIZE_IU8);
        }

        public uint_t(UInt64 number, int size)
        {
            val         = number;
            ActualSize  = size;
        }

        public uint_t(UInt64 number)
            : this(number, SIZE_IU64)
        {

        }

        public uint_t(UInt32 number)
            : this(number, SIZE_IU32)
        {

        }

        public uint_t(UInt16 number)
            : this(number, SIZE_IU16)
        {

        }

        public uint_t(Byte number)
            : this(number, SIZE_IU8)
        {

        }

        /// <exception cref="OverflowException">number is less than uint_t.MIN</exception>
        public uint_t(int_t number)
            : this(Convert.ToUInt64(number), number.ActualSize)
        {

        }
    }
}
