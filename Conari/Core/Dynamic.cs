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
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace net.r_eg.Conari.Core
{
    public sealed class Dynamic: IDynamic
    {
        /// <summary>
        /// Default name for new methods.
        /// </summary>
        public const string METHOD_NAME = "Invoke";

        private static readonly Lazy<Dynamic> _this = new Lazy<Dynamic>(() => new Dynamic());

        private static readonly Lazy<ModuleBuilder> module = new Lazy<ModuleBuilder>(() => 
        {
            AssemblyName asm = new AssemblyName("__dynamic_asm_Conari");

#if NETCORE

            AssemblyBuilder dynAsm = AssemblyBuilder.DefineDynamicAssembly(asm, AssemblyBuilderAccess.RunAndCollect);
#else
            AssemblyBuilder dynAsm = AppDomain.CurrentDomain.DefineDynamicAssembly(asm, AssemblyBuilderAccess.RunAndCollect);
#endif

            return dynAsm.DefineDynamicModule(asm.Name);
        });

        private readonly MICache micache = new MICache();

        public static IDynamic _ => _this.Value;

        /// <summary>
        /// To cache dynamic types by default with similar signatures:
        ///     `{return type} name( [{argument types}] )`
        /// </summary>
        public bool UseCache
        {
            get;
            set;
        } = true;

        /// <summary>
        /// To reset all cached types.
        /// </summary>
        public void resetCache()
        {
            micache?.Clear();
        }

        /// <summary>
        /// To cache dynamic types via signature of method.
        /// </summary>
        /// <param name="mi">The instance of MethodInfo.</param>
        /// <returns>false value if this signature is already exists or cannot be cached.</returns>
        public bool cache(MethodInfo mi)
        {
            var key = getKeyTypes(mi);

            if(!micache.ContainsKey(key)) {
                micache[key] = mi;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Extract all valuable types from MethodInfo.
        /// </summary>
        /// <param name="mi"></param>
        /// <returns>The array of types that can be used for MICache containers etc.</returns>
        public Type[] getKeyTypes(MethodInfo mi)
        {
            if(mi == null) {
                throw new ArgumentNullException("MethodInfo cannot be null.");
            }

            return combine(
                        mi.ReturnType, 
                        mi.GetParameters()
                                .Select(p => p.ParameterType)
                                .ToArray()
            );
        }

        /// <summary>
        /// Gets MethodInfo with empty method.
        /// </summary>
        /// <param name="ret">The return type.</param>
        /// <param name="args">Arguments of method if exists.</param>
        /// <returns>Complete signature as method without body.</returns>
        public MethodInfo getMethodInfo(Type ret = null, params Type[] args)
        {
            return getMethodInfo(UseCache, ret, args);
        }

        /// <summary>
        /// Gets MethodInfo with empty method.
        /// </summary>
        /// <param name="cache">Try to find same types from cache. The name cannot be actual if true.</param>
        /// <param name="ret">The return type.</param>
        /// <param name="args">Arguments of method if exists.</param>
        /// <returns>Complete signature as method without body.</returns>
        public MethodInfo getMethodInfo(bool cache, Type ret = null, params Type[] args)
        {
            return getMethodInfo(METHOD_NAME, cache, ret, args);
        }

        /// <summary>
        /// Gets MethodInfo with empty method.
        /// </summary>
        /// <param name="name">The name of method.</param>
        /// <param name="ret">The return type.</param>
        /// <param name="args">Arguments of method if exists.</param>
        /// <returns>Complete signature as method without body.</returns>
        public MethodInfo getMethodInfo(string name, Type ret = null, params Type[] args)
        {
            return getMethodInfo(name, UseCache, ret, args);
        }

        /// <summary>
        /// Gets MethodInfo with empty method.
        /// </summary>
        /// <param name="name">The name of method.</param>
        /// <param name="cache">Try to find same types from cache. The name cannot be actual if true.</param>
        /// <param name="ret">The return type.</param>
        /// <param name="args">Arguments of method if exists.</param>
        /// <returns>Complete signature as method without body.</returns>
        public MethodInfo getMethodInfo(string name, bool cache, Type ret = null, params Type[] args)
        {
            if(String.IsNullOrWhiteSpace(name)) {
                name = METHOD_NAME;
            }
            ret = NullV(ret);

            if(!cache) {
                return getmi(name, ret, args);
            }

            var key = combine(ret, args);

            if(!micache.ContainsKey(key)) {
                micache[key] = getmi(name, ret, args);
            }

            return micache[key];
        }

        /// <summary>
        /// Alias to `IDynamic.getMethodInfo(Type ret, params Type[] args)`
        /// Gets MethodInfo with empty method.
        /// </summary>
        /// <param name="ret">The return type.</param>
        /// <param name="args">Arguments of method if exists.</param>
        /// <returns>Complete signature as method without body.</returns>
        public static MethodInfo GetMethodInfo(Type ret = null, params Type[] args)
        {
            return _.getMethodInfo(ret, args);
        }

        /// <summary>
        /// Alias to `IDynamic.getMethodInfo(bool cache, Type ret, params Type[] args)`
        /// Gets MethodInfo with empty method.
        /// </summary>
        /// <param name="cache">Try to find same types from cache. The name cannot be actual if true.</param>
        /// <param name="ret">The return type.</param>
        /// <param name="args">Arguments of method if exists.</param>
        /// <returns>Complete signature as method without body.</returns>
        public static MethodInfo GetMethodInfo(bool cache, Type ret = null, params Type[] args)
        {
            return _.getMethodInfo(cache, ret, args);
        }

        /// <summary>
        /// Alias to `IDynamic.getMethodInfo(string name, Type ret, params Type[] args)`
        /// Gets MethodInfo with empty method.
        /// </summary>
        /// <param name="name">The name of method.</param>
        /// <param name="ret">The return type.</param>
        /// <param name="args">Arguments of method if exists.</param>
        /// <returns>Complete signature as method without body.</returns>
        public static MethodInfo GetMethodInfo(string name, Type ret = null, params Type[] args)
        {
            return _.getMethodInfo(name, ret, args);
        }

        /// <summary>
        /// Alias to `IDynamic.getMethodInfo(string name, bool cache, Type ret, params Type[] args)`
        /// Gets MethodInfo with empty method.
        /// </summary>
        /// <param name="name">The name of method.</param>
        /// <param name="cache">Try to find same types from cache. The name cannot be actual if true.</param>
        /// <param name="ret">The return type.</param>
        /// <param name="args">Arguments of method if exists.</param>
        /// <returns>Complete signature as method without body.</returns>
        public static MethodInfo GetMethodInfo(string name, bool cache, Type ret = null, params Type[] args)
        {
            return _.getMethodInfo(name, cache, ret, args);
        }

        /// <summary>
        /// Generates empty type with default name. 
        /// </summary>
        /// <param name="ret">The return type.</param>
        /// <param name="args">Arguments if exists.</param>
        /// <returns>The type that contains signature as `{ret} {METHOD_NAME}({args})`</returns>
        public static Type CreateEmptyType(Type ret = null, params Type[] args)
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
        public static Type CreateEmptyType(string name, Type ret = null, params Type[] args)
        {
            if(string.IsNullOrWhiteSpace(name)) {
                throw new ArgumentNullException(nameof(name));
            }

            TypeBuilder tbuild = module.Value.DefineType(Guid.NewGuid().ToString(), TypeAttributes.Public);

            MethodBuilder m = tbuild.DefineMethod
            (
                name,
                MethodAttributes.Public | MethodAttributes.Static,
                NullV(ret),
                args
            );

            ILGenerator il = m.GetILGenerator();
            il.Emit(OpCodes.Ret);

#if NETCORE
            return tbuild.CreateTypeInfo()?.AsType();
#else
            return tbuild.CreateType();
#endif
        }

        public static dynamic DCast(Type type, dynamic obj)
        {
            if(type == null || type == typeof(void)) {
                return null;
            }

            return typeof(Dynamic)
                        .GetMethod("Cast", BindingFlags.Public | BindingFlags.Static)
                        .MakeGenericMethod(type)
                        .Invoke(null, new[] { obj });
        }

        public static T Cast<T>(dynamic obj)
        {
            return (T)obj;
        }

        public Dynamic()
        {

        }

        private static Type NullV(Type type)
        {
            if(type == null) {
                return typeof(void);
            }
            return type;
        }

        private MethodInfo getmi(string name, Type ret, params Type[] args)
        {
            Type type = CreateEmptyType(name, ret, args);
            return type.GetMethod(name);
        }

        private Type[] combine(Type ret, params Type[] args)
        {
            ret = NullV(ret);

            if(args == null || args.Length < 1) {
                return new Type[] { ret };
            }

            var key = new Type[args.Length + 1];
            key[0]  = ret;
            args.CopyTo(key, 1);

            return key;
        }
    }
}
