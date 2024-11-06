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
    public class LoadLibException: WinFuncFailException
    {
        public LoadLibException(string message, bool getError)
            : base(message, getError)
        {

        }

        public LoadLibException(string message)
            : base(message, false)
        {

        }
    }
}
