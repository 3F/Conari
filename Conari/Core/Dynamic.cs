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
using System.Collections.Concurrent;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using net.r_eg.Conari.Core.Runtime;
using net.r_eg.Conari.Extension;

namespace net.r_eg.Conari.Core
{
    using MICache = ConcurrentDictionary<int, MethodInfo>;

    public sealed class Dynamic: IDynamic
    {
        /// <summary>
        /// Default name for new methods.
        /// </summary>
        public const string METHOD_NAME = "Invoke";

        private static readonly Lazy<Dynamic> _this = new(() => new());

        private static readonly Lazy<ModuleBuilder> module = new(() => 
        {
            AssemblyName asm = new("__dynamic_asm_Conari");

#if NETCORE

            AssemblyBuilder dynAsm = AssemblyBuilder.DefineDynamicAssembly(asm, AssemblyBuilderAccess.RunAndCollect);
#else
            AssemblyBuilder dynAsm = AppDomain.CurrentDomain.DefineDynamicAssembly(asm, AssemblyBuilderAccess.RunAndCollect);
#endif

            return dynAsm.DefineDynamicModule(asm.Name);
        });

        private static readonly Type tVoid = typeof(void);

        private readonly MICache micache = new();

        public static IDynamic _ => _this.Value;

        public DynamicOptions Options { get; set; } = DynamicOptions.DefaultWithCaching;

        public void resetCache() => micache.Clear();

        public bool cache(MethodInfo mi) => micache.TryAdd(Hash(mi), mi);

        public MethodInfo getMethodInfo(DynamicOptions cfg, string name, Type ret = null, params Type[] args)
        {
            if(string.IsNullOrWhiteSpace(name)) {
                name = METHOD_NAME;
            }
            ret = NullV(ret);

            if((cfg & DynamicOptions.Cache) == 0) {
                return Getmi(cfg, name, ret, args);
            }

            var key = Hash(ret, args);

            if(!micache.TryGetValue(key, out MethodInfo mi))
            {
                MethodInfo v = Getmi(cfg, name, ret, args);
                micache.TryAdd(key, v);
                return v;
            }

            return mi;
        }

        public MethodInfo getMethodInfo(DynamicOptions cfg, Type ret = null, params Type[] args)
        {
            return getMethodInfo(cfg, METHOD_NAME, ret, args);
        }

        public MethodInfo getMethodInfo(Type ret = null, params Type[] args)
        {
            return getMethodInfo(Options, ret, args);
        }

        public MethodInfo getMethodInfo(bool cache, Type ret = null, params Type[] args)
        {
            return getMethodInfo(METHOD_NAME, cache, ret, args);
        }

        public MethodInfo getMethodInfo(string name, Type ret = null, params Type[] args)
        {
            return getMethodInfo(Options, name, ret, args);
        }

        public MethodInfo getMethodInfo(string name, bool cache, Type ret = null, params Type[] args)
        {
            return getMethodInfo
            (
                cache ? DynamicOptions.DefaultWithCaching : DynamicOptions.DefaultNoCache, 
                METHOD_NAME, 
                ret, 
                args
            );
        }

        /// <inheritdoc cref="getMethodInfo(DynamicOptions, string, Type, Type[])"/>
        public static MethodInfo GetMethodInfo(DynamicOptions cfg, string name, Type ret = null, params Type[] args)
        {
            return _.getMethodInfo(cfg, name, ret, args);
        }

        /// <inheritdoc cref="getMethodInfo(DynamicOptions, Type, Type[])"/>
        public static MethodInfo GetMethodInfo(DynamicOptions cfg, Type ret = null, params Type[] args)
        {
            return _.getMethodInfo(cfg, ret, args);
        }

        /// <inheritdoc cref="getMethodInfo(Type, Type[])"/>
        public static MethodInfo GetMethodInfo(Type ret = null, params Type[] args)
        {
            return _.getMethodInfo(ret, args);
        }

        /// <inheritdoc cref="getMethodInfo(string, Type, Type[])"/>
        public static MethodInfo GetMethodInfo(string name, Type ret = null, params Type[] args)
        {
            return _.getMethodInfo(name, ret, args);
        }

        /// <summary>
        /// Generates empty type with default name.
        /// </summary>
        /// <inheritdoc cref="CreateEmptyType(string, Type, Type[])"/>
        public static Type CreateEmptyType(Type ret = null, params Type[] args)
        {
            return CreateEmptyType(METHOD_NAME, ret, args);
        }

        /// <summary>
        /// Generates empty type with specific name.
        /// </summary>
        /// <param name="name">The name of type.</param>
        /// <param name="ret">The return type.</param>
        /// <param name="args">Arguments if exists.</param>
        /// <returns>A complete type with a compatible signature as `{return} {name}({args})`</returns>
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

        /// <summary>
        /// Hash the following types using specified algorithm.
        /// </summary>
        /// <param name="ret">The return type.</param>
        /// <param name="arguments">Optional types for arguments.</param>
        public static int Hash(Type ret = null, params Type[] arguments)
        {
            return 0.CalculateHashCode(ret).CalculateHashCode(arguments);
        }

        /// <summary>
        /// Hash the following <see cref="MethodInfo"/> using specified algorithm.
        /// </summary>
        /// <param name="mi"></param>
        public static int Hash(MethodInfo mi)
        {
            if(mi == null) throw new ArgumentNullException(nameof(mi));
            return 0.CalculateHashCode(mi.ReturnType)
                    .CalculateHashCode(mi.GetParameters().Select(a => a.ParameterType).ToArray());
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

        public static T Cast<T>(dynamic obj) => (T)obj;

        private Dynamic()
        {

        }

        private static Type NullV(Type type) => type ?? tVoid;

        private static MethodInfo Getmi(DynamicOptions cfg, string name, Type ret, params Type[] args)
        {
            if((cfg & DynamicOptions.GenerateUsingBuilder) != 0)
            {
                Type type = CreateEmptyType(name, ret, args);
                return type.GetMethod(name);
            }
            return new NoDeclMethodInfo(name, ret, args.Select(a => new ArgInfo(a)).ToArray());
        }

        #region Obsolete

        [Obsolete("Use DynamicOptions to configure caching.")]
        public bool UseCache
        {
            get => (Options & DynamicOptions.Cache) != 0;
            set
            {
                if(value) Options |= DynamicOptions.Cache;
                else Options &= ~DynamicOptions.Cache;
            }
        }

        [Obsolete("Use Dynamic.Hash() methods instead.")]
        public Type[] getKeyTypes(MethodInfo mi)
        {
            if(mi == null) throw new ArgumentNullException(nameof(mi));

            Type tRet = NullV(mi.ReturnType);

            ParameterInfo[] info = mi.GetParameters();

            Type[] key = new Type[] { tRet };

            if(info == null || info.Length < 1) return key;

            return key.Concat(info.Select(p => p.ParameterType)).ToArray();
        }

        /// <inheritdoc cref="getMethodInfo(bool, Type, Type[])"/>
        [Obsolete("Use DynamicOptions to configure caching.")]
        public static MethodInfo GetMethodInfo(bool cache, Type ret = null, params Type[] args)
        {
            return _.getMethodInfo(cache, ret, args);
        }

        /// <inheritdoc cref="getMethodInfo(string, bool, Type, Type[])"/>
        [Obsolete("Use DynamicOptions to configure caching.")]
        public static MethodInfo GetMethodInfo(string name, bool cache, Type ret = null, params Type[] args)
        {
            return _.getMethodInfo(name, cache, ret, args);
        }

        #endregion
    }
}
