/*!
 * Copyright (c) 2016  Denis Kuzmin <x-3F@outlook.com> github/3F
 * Copyright (c) Conari contributors https://github.com/3F/Conari/graphs/contributors
 * Licensed under the MIT License (MIT).
 * See accompanying LICENSE.txt file or visit https://github.com/3F/Conari
*/

namespace net.r_eg.Conari.Types.Methods
{
    /// <typeparam name="TRes">The type of return value.</typeparam>
    /// <typeparam name="T">The type of arguments.</typeparam>
    /// <param name="args">Argument list.</param>
    /// <returns>Return value.</returns>
    public delegate TRes Method<out TRes, T>(params T[] args);
}
