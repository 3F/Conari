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
    [DebuggerDisplay(" = {val} [ ActualSize: {ActualSize} ]")]
    public struct float_t
    {
        public const int SIZE_SINGLE = sizeof(Single);
        public const int SIZE_DOUBLE = sizeof(Double);

        public const Double MAX = Double.MaxValue;
        public const Double MIN = Double.MinValue;

        private readonly Double val;

        public int ActualSize
        {
            get;
            private set;
        }

        public static implicit operator Double(float_t number)
        {
            return number.val;
        }

        public static implicit operator Single(float_t number)
        {
            // To check infinity use the IsInfinity methods
            return (Single)number.val;
        }

        public static implicit operator float_t(Double number)
        {
            return new float_t(number, SIZE_DOUBLE);
        }

        public static implicit operator float_t(Single number)
        {
            return new float_t(number, SIZE_SINGLE);
        }

        public float_t(Double number, int size)
        {
            val         = number;
            ActualSize  = size;
        }

        public float_t(Double number)
            : this(number, SIZE_DOUBLE)
        {

        }

        public float_t(Single number)
            : this(number, SIZE_SINGLE)
        {

        }
    }
}
