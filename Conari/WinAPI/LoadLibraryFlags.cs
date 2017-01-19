/*
 * The MIT License (MIT)
 * 
 * Copyright (c) 2016-2017  Denis Kuzmin <entry.reg@gmail.com>
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Conari"), to deal
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

namespace net.r_eg.Conari.WinAPI
{
    /// <summary>
    /// Possible actions for loading the module with LoadLibraryEx function.
    /// https://msdn.microsoft.com/en-us/library/windows/desktop/ms684179.aspx
    /// </summary>
    internal struct LoadLibraryFlags
    {
        /// <summary>
        /// If this value is used and lpFileName specifies an absolute path, 
        /// the system uses the alternate file search strategy discussed in the Remarks section to find 
        /// associated executable modules that the specified module causes to be loaded. 
        /// If this value is used and lpFileName specifies a relative path, the behavior is undefined.
        /// 
        /// If this value is not used, or if lpFileName does not specify a path, 
        /// the system uses the standard search strategy discussed in the Remarks section to find 
        /// associated executable modules that the specified module causes to be loaded.
        /// 
        /// This value cannot be combined with any LOAD_LIBRARY_SEARCH flag.
        /// </summary>
        public const uint LOAD_WITH_ALTERED_SEARCH_PATH = 0x00000008;
    }
}
