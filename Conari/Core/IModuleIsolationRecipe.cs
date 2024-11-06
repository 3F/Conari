/*!
 * Copyright (c) 2016  Denis Kuzmin <x-3F@outlook.com> github/3F
 * Copyright (c) Conari contributors https://github.com/3F/Conari/graphs/contributors
 * Licensed under the MIT License (MIT).
 * See accompanying LICENSE.txt file or visit https://github.com/3F/Conari
*/

namespace net.r_eg.Conari.Core
{
    public interface IModuleIsolationRecipe
    {
        /// <summary>
        /// Isolate module and its depedencies before loading. 
        /// See also <see cref="IConfig.IsolateLoadingOfModule"/> and <see cref="IConfig.ModuleIsolationRecipe"/> related options.
        /// </summary>
        /// <param name="l">Accessing module information.</param>
        /// <param name="module">Final module for loading.</param>
        /// <returns>Success of implemented isolation. It will use the original module if false; And will require <see cref="discard"/> processing if true.</returns>
        bool isolate(Link l, out string module);

        /// <summary>
        /// Discards isolation if it was applied before.
        /// </summary>
        /// <param name="l">Accessing module information.</param>
        /// <returns>Success (reserved)</returns>
        bool discard(Link l);
    }
}
