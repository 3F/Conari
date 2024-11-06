/*!
 * Copyright (c) 2016  Denis Kuzmin <x-3F@outlook.com> github/3F
 * Copyright (c) Conari contributors https://github.com/3F/Conari/graphs/contributors
 * Licensed under the MIT License (MIT).
 * See accompanying LICENSE.txt file or visit https://github.com/3F/Conari
*/

using System;
using System.Reflection;

namespace net.r_eg.Conari.Core
{
    public interface IDynamic
    {
        /// <summary>
        /// Configure generation globally.
        /// </summary>
        DynamicOptions Options { get; set; }

        /// <summary>
        /// To reset all cached types.
        /// </summary>
        void resetCache();

        /// <summary>
        /// To cache specific type using <see cref="MethodInfo"/> signature information.
        /// </summary>
        /// <param name="mi">The instance of <see cref="MethodInfo"/>.</param>
        /// <returns>false value if this signature is already exists or cannot be cached.</returns>
        bool cache(MethodInfo mi);

        /// <summary>
        /// Gets empty <see cref="MethodInfo"/> using <see cref="DynamicOptions"/> options
        /// and specific name.
        /// </summary>
        /// <param name="cfg">Configure generation.</param>
        /// <param name="name">The name for method.</param>
        /// <param name="ret">The return type.</param>
        /// <param name="args">Arguments of method if exists.</param>
        /// <returns>An complete method signature without body.</returns>
        MethodInfo getMethodInfo(DynamicOptions cfg, string name, Type ret = null, params Type[] args);

        /// <summary>
        /// Gets empty <see cref="MethodInfo"/> using <see cref="DynamicOptions"/> options
        /// and default name.
        /// </summary>
        /// <inheritdoc cref="getMethodInfo(DynamicOptions, string, Type, Type[])"/>
        MethodInfo getMethodInfo(DynamicOptions cfg, Type ret = null, params Type[] args);

        /// <summary>
        /// Gets empty <see cref="MethodInfo"/> using <see cref="DynamicOptions.Default"/> option
        /// and default name.
        /// </summary>
        /// <inheritdoc cref="getMethodInfo(DynamicOptions, string, Type, Type[])"/>
        MethodInfo getMethodInfo(Type ret = null, params Type[] args);

        /// <summary>
        /// Gets empty <see cref="MethodInfo"/> using <see cref="DynamicOptions.Default"/> option
        /// and specific name.
        /// </summary>
        /// <inheritdoc cref="getMethodInfo(DynamicOptions, string, Type, Type[])"/>
        MethodInfo getMethodInfo(string name, Type ret = null, params Type[] args);

        #region Obsolete

        /// <summary>
        /// Cache all new generated types.
        /// </summary>
        [Obsolete("Use DynamicOptions to configure caching.")]
        bool UseCache { get; set; }

        /// <summary>
        /// Extract all valuable types from <see cref="MethodInfo"/>.
        /// </summary>
        /// <param name="mi"></param>
        /// <returns>The array of types that can be used for some containers.</returns>
        [Obsolete("Use Dynamic.Hash() methods instead.")]
        Type[] getKeyTypes(MethodInfo mi);

        /// <inheritdoc cref="getMethodInfo(DynamicOptions, string, Type, Type[])"/>
        [Obsolete("Use DynamicOptions to configure caching.")]
        MethodInfo getMethodInfo(string name, bool cache, Type ret = null, params Type[] args);

        /// <inheritdoc cref="getMethodInfo(DynamicOptions, Type, Type[])"/>
        [Obsolete("Use DynamicOptions to configure caching.")]
        MethodInfo getMethodInfo(bool cache, Type ret = null, params Type[] args);

        #endregion
    }
}
