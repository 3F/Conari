/*!
 * Copyright (c) 2016  Denis Kuzmin <x-3F@outlook.com> github/3F
 * Copyright (c) Conari contributors https://github.com/3F/Conari/graphs/contributors
 * Licensed under the MIT License (MIT).
 * See accompanying LICENSE.txt file or visit https://github.com/3F/Conari
*/

using net.r_eg.Conari.Types;

namespace ConariTest._svc
{
    internal struct UserSpecUintType
    {
        private readonly uint val;

        [NativeType]
        public static implicit operator uint(UserSpecUintType number) => number.val;

        public static implicit operator UserSpecUintType(uint number) => new(number);

        public UserSpecUintType(uint number) => val = number;
    }
}
