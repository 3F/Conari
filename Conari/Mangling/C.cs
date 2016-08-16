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
        public static string Decorate(string function, string[] names)
        {
            if(names == null) {
                return null;
            }
            return names.Where(actual => Check(function, actual)).FirstOrDefault();
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
