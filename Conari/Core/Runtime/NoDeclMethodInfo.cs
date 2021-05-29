/*
 * The MIT License (MIT)
 *
 * Copyright (c) 2016-2021  Denis Kuzmin <x-3F@outlook.com> github/3F
 * Copyright (c) Conari contributors https://github.com/3F/Conari/graphs/contributors
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
*/

using System;
using System.Globalization;
using System.Reflection;

namespace net.r_eg.Conari.Core.Runtime
{
    internal sealed class NoDeclMethodInfo: MethodInfo
    {
        internal const string DEF_NAME = "Invoke";

        private readonly Type returnType;
        private readonly ParameterInfo[] arguments;

        public override string Name { get; } = DEF_NAME;

        public override Type ReturnType => returnType;

        public override ParameterInfo[] GetParameters() => arguments;

        public override Type DeclaringType { get; } // keep it null to invalidate full declaration

        public NoDeclMethodInfo(Type returnType, params ParameterInfo[] arguments)
        {
            this.returnType = returnType;
            this.arguments  = arguments;
        }

        public NoDeclMethodInfo(string name, Type returnType, params ParameterInfo[] arguments)
            : this(returnType, arguments)
        {
            Name = name;
        }

        #region  NotImplemented

        public override ICustomAttributeProvider ReturnTypeCustomAttributes => throw new NotImplementedException();

        public override RuntimeMethodHandle MethodHandle => throw new NotImplementedException();

        public override MethodAttributes Attributes => throw new NotImplementedException();

        public override Type ReflectedType => throw new NotImplementedException();

        public override MethodInfo GetBaseDefinition()
        {
            throw new NotImplementedException();
        }

        public override object[] GetCustomAttributes(bool inherit)
        {
            throw new NotImplementedException();
        }

        public override object[] GetCustomAttributes(Type attributeType, bool inherit)
        {
            throw new NotImplementedException();
        }

        public override MethodImplAttributes GetMethodImplementationFlags()
        {
            throw new NotImplementedException();
        }

        public override object Invoke(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public override bool IsDefined(Type attributeType, bool inherit)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
