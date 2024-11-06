/*!
 * Copyright (c) 2016  Denis Kuzmin <x-3F@outlook.com> github/3F
 * Copyright (c) Conari contributors https://github.com/3F/Conari/graphs/contributors
 * Licensed under the MIT License (MIT).
 * See accompanying LICENSE.txt file or visit https://github.com/3F/Conari
*/

using System;

namespace net.r_eg.Conari.Exceptions
{
    [Serializable]
    public class PECorruptDataException: CommonException
    {
        public PECorruptDataException(string message)
            : base(message)
        {

        }

        public PECorruptDataException()
            : this("Incorrect or damaged PE-format.")
        {

        }
    }
}
