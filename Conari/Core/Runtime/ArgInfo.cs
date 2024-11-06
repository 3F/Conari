/*!
 * Copyright (c) 2016  Denis Kuzmin <x-3F@outlook.com> github/3F
 * Copyright (c) Conari contributors https://github.com/3F/Conari/graphs/contributors
 * Licensed under the MIT License (MIT).
 * See accompanying LICENSE.txt file or visit https://github.com/3F/Conari
*/

using System;
using System.Reflection;

namespace net.r_eg.Conari.Core.Runtime
{
    internal class ArgInfo: ParameterInfo
    {
        protected readonly Type init;

        public override Type ParameterType => init;

        public override string Name => init.Name;

        public ArgInfo(Type type)
        {
            init = type ?? throw new ArgumentNullException(nameof(type));
        }
    }
}
