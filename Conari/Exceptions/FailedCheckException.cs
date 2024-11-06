/*!
 * Copyright (c) 2016  Denis Kuzmin <x-3F@outlook.com> github/3F
 * Copyright (c) Conari contributors https://github.com/3F/Conari/graphs/contributors
 * Licensed under the MIT License (MIT).
 * See accompanying LICENSE.txt file or visit https://github.com/3F/Conari
*/

using System;
using net.r_eg.Conari.Extension;
using net.r_eg.Conari.Resources;

namespace net.r_eg.Conari.Exceptions
{
    [Serializable]
    public class FailedCheckException: CommonException
    {
        public FailedCheckException(bool result)
            : this(Msg.failed_check_result_0.Format($"{result}"))
        {

        }

        public FailedCheckException(string message)
            : base(message)
        {

        }
    }
}
