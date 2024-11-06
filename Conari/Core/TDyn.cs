/*!
 * Copyright (c) 2016  Denis Kuzmin <x-3F@outlook.com> github/3F
 * Copyright (c) Conari contributors https://github.com/3F/Conari/graphs/contributors
 * Licensed under the MIT License (MIT).
 * See accompanying LICENSE.txt file or visit https://github.com/3F/Conari
*/

using System;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices;

namespace net.r_eg.Conari.Core
{
    public struct TDyn
    {
        public DynamicMethod dynamic;

        public MethodInfo method;

        public Type[] args;

        public Type returnType;

        public CallingConvention convention;
    }
}
