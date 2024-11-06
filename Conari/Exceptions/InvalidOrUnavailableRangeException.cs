/*!
 * Copyright (c) 2016  Denis Kuzmin <x-3F@outlook.com> github/3F
 * Copyright (c) Conari contributors https://github.com/3F/Conari/graphs/contributors
 * Licensed under the MIT License (MIT).
 * See accompanying LICENSE.txt file or visit https://github.com/3F/Conari
*/

using System;
using net.r_eg.Conari.Extension;
using net.r_eg.Conari.Resources;
using net.r_eg.Conari.Types;

namespace net.r_eg.Conari.Exceptions
{
    [Serializable]
    public class InvalidOrUnavailableRangeException: CommonException
    {
        public InvalidOrUnavailableRangeException(VPtr address)
            : base(Msg.invalid_or_unavailable_range_at_0.Format(address.ToString()))
        {

        }
    }
}
