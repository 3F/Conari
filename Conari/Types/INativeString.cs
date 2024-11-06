/*!
 * Copyright (c) 2016  Denis Kuzmin <x-3F@outlook.com> github/3F
 * Copyright (c) Conari contributors https://github.com/3F/Conari/graphs/contributors
 * Licensed under the MIT License (MIT).
 * See accompanying LICENSE.txt file or visit https://github.com/3F/Conari
*/

namespace net.r_eg.Conari.Types
{
    public interface INativeString: IMarshalableGeneric
    {
        /// <summary>
        /// For supported places use a manager to release resources.
        /// </summary>
        bool UseManager { get; set; }

        /// <summary>
        /// Who is the owner. True indicates its own allocating.
        /// </summary>
        bool Owner { get; }

        /// <summary>
        /// Indicates disposed state.
        /// </summary>
        bool Disposed { get; }
    }
}
