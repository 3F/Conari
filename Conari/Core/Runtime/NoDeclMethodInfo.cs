/*!
 * Copyright (c) 2016  Denis Kuzmin <x-3F@outlook.com> github/3F
 * Copyright (c) Conari contributors https://github.com/3F/Conari/graphs/contributors
 * Licensed under the MIT License (MIT).
 * See accompanying LICENSE.txt file or visit https://github.com/3F/Conari
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
