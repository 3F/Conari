/*!
 * Copyright (c) 2016  Denis Kuzmin <x-3F@outlook.com> github/3F
 * Copyright (c) Conari contributors https://github.com/3F/Conari/graphs/contributors
 * Licensed under the MIT License (MIT).
 * See accompanying LICENSE.txt file or visit https://github.com/3F/Conari
*/

using System.Collections.Generic;
using System.Linq;

namespace net.r_eg.Conari.Mangling
{
    /*
     * https://github.com/3F/Conari/issues/3
     * 
        @get_SevenFastCall@0
        _get_SevenStdCall@0
        get_SevenVectorCall@@0

    __cdecl:
          get_Seven
        or
          _get_Seven
    */
    /// <summary>
    /// The Mangling by C rules.
    /// </summary>
    public static class C
    {
        /// <summary>
        /// Decorate function name from existing exported list.
        /// </summary>
        /// <param name="function">Undecorated function name.</param>
        /// <param name="names">The exported function names. Use PE handler.</param>
        /// <returns>found decorated function or null.</returns>
        public static string Decorate(string function, IEnumerable<string> names)
        {
            return names?.FirstOrDefault(actual => Check(function, actual));
        }

        /// <summary>
        /// Underscore character (_) is prefixed to names, 
        /// except when __cdecl functions that use C linkage are exported.
        /// e.g: _get_Seven or get_Seven
        /// </summary>
        public static bool IsCdecl(string name, string actual)
        {
            return (name == actual || "_" + name == actual);
        }

        /// <summary>
        /// Leading underscore (_) and a trailing at sign (@) followed by the number of bytes in the parameter list in decimal.
        /// e.g: _get_SevenStdCall@0
        /// </summary>
        public static bool IsStdCall(string name, string actual)
        {
            return actual.StartsWith($"_{name}@");
        }

        /// <summary>
        /// Leading and trailing at signs (@) followed by a decimal number representing the number of bytes in the parameter list.
        /// e.g.: @get_SevenFastCall@0
        /// </summary>
        public static bool IsFastCall(string name, string actual)
        {
            return actual.StartsWith($"@{name}@");
        }

        /// <summary>
        /// Two trailing at signs (@@) followed by a decimal number of bytes in the parameter list.
        /// e.g.: get_SevenVectorCall@@0
        /// </summary>
        public static bool IsVectorCall(string name, string actual)
        {
            return actual.StartsWith(name + "@");
        }

        private static bool Check(string name, string actual)
        {
            return IsStdCall(name, actual)
                        || IsCdecl(name, actual)
                        || IsFastCall(name, actual)
                        || IsVectorCall(name, actual);
        }
    }
}
