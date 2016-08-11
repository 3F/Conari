/*
 * The MIT License (MIT)
 * 
 * Copyright (c) 2016  Denis Kuzmin <entry.reg@gmail.com>
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
using System.Reflection;
using System.Reflection.Emit;

namespace net.r_eg.Conari.Core
{
    public sealed class Dynamic
    {
        public const string METHOD_NAME = "Invoke";

        /// <summary>
        /// Gets MethodInfo with empty method {METHOD_NAME}.
        /// </summary>
        /// <param name="ret">The return type.</param>
        /// <param name="args">Arguments of method if exists.</param>
        /// <returns>Complete signature as method without body.</returns>
        public static MethodInfo GetMethodInfo(Type ret, params Type[] args)
        {
            return GetMethodInfo(METHOD_NAME, ret, args);
        }

        /// <summary>
        /// Gets MethodInfo with empty method.
        /// </summary>
        /// <param name="name">The name of method.</param>
        /// <param name="ret">The return type.</param>
        /// <param name="args">Arguments of method if exists.</param>
        /// <returns>Complete signature as method without body.</returns>
        public static MethodInfo GetMethodInfo(string name, Type ret, params Type[] args)
        {
            Type type = CreateEmptyType(name, ret, args);
            return type.GetMethod(name);
        }

        /// <summary>
        /// Generates empty type with default name. 
        /// </summary>
        /// <param name="ret">The return type.</param>
        /// <param name="args">Arguments if exists.</param>
        /// <returns>The type that contains signature as `{ret} {METHOD_NAME}({args})`</returns>
        public static Type CreateEmptyType(Type ret, params Type[] args)
        {
            return CreateEmptyType(METHOD_NAME, ret, args);
        }

        /// <summary>
        /// Generates empty type.
        /// </summary>
        /// <param name="name">The name of type.</param>
        /// <param name="ret">The return type.</param>
        /// <param name="args">Arguments if exists.</param>
        /// <returns>The type that contains signature as `{ret} {name}({args})`</returns>
        public static Type CreateEmptyType(string name, Type ret, params Type[] args)
        {
            if(String.IsNullOrWhiteSpace(name)) {
                throw new ArgumentException("The name cannot be null or empty.");
            }

            if(ret == null) {
                ret = typeof(void);
            }

            AssemblyName asm        = new AssemblyName("DynamicAsm");
            AssemblyBuilder dynAsm  = AppDomain.CurrentDomain.DefineDynamicAssembly(asm, AssemblyBuilderAccess.ReflectionOnly);

            ModuleBuilder module    = dynAsm.DefineDynamicModule(asm.Name);
            TypeBuilder tbuild      = module.DefineType("DynamicType", TypeAttributes.Public);

            MethodBuilder m = tbuild.DefineMethod(
                                        name,
                                        MethodAttributes.Public | MethodAttributes.Static,
                                        ret,
                                        args);

            ILGenerator il = m.GetILGenerator();
            il.Emit(OpCodes.Ret);

            return tbuild.CreateType();
        }

        public static dynamic DCast(Type type, object obj)
        {
            return typeof(Dynamic)
                        .GetMethod("Cast", BindingFlags.Public | BindingFlags.Static)
                        .MakeGenericMethod(type)
                        .Invoke(null, new[] { obj });
        }

        public static T Cast<T>(dynamic obj)
        {
            return (T)obj;
        }
    }
}
