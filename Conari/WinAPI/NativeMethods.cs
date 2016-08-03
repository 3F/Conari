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
using System.Runtime.InteropServices;

namespace net.r_eg.Conari.WinAPI
{
    internal static class NativeMethods
    {
        /// <summary>
        /// Loads the specified module into the address space of the calling process.
        /// https://msdn.microsoft.com/en-us/library/windows/desktop/ms684175.aspx
        /// 
        ///  The system maintains a per-process reference count on all loaded modules. 
        ///  * Calling LoadLibrary increments the reference count. 
        ///  * Calling the FreeLibrary (see below) or FreeLibraryAndExitThread function decrements the reference count. 
        ///  The system unloads a module when its reference count reaches zero or when the process terminates (regardless of the reference count).
        /// </summary>
        /// <param name="lpFileName">
        ///     The name of the module. This can be either a library module (a .dll file) or an executable module (an .exe file).
        /// </param>
        /// <returns>A handle that can be used in GetProcAddress to get the address of a DLL function.</returns>
        [DllImport("kernel32", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern IntPtr LoadLibrary([MarshalAs(UnmanagedType.LPWStr)] string lpFileName);

        /// <summary>
        /// Loads the specified module into the address space of the calling process. The specified module may cause other modules to be loaded.
        /// https://msdn.microsoft.com/en-us/library/windows/desktop/ms684179.aspx
        /// 
        /// The behavior of this function is identical to the LoadLibrary function without flags.
        /// </summary>
        /// <param name="lpFileName">
        ///     A string that specifies the file name of the module to load. 
        ///     The module can be a library module (a .dll file) or an executable module (an .exe file).
        /// </param>
        /// <param name="hFile">This parameter is reserved for future use. It must be NULL.</param>
        /// <param name="dwFlags">The action to be taken when loading the module. Use from LoadLibraryFlags.</param>
        /// <returns></returns>
        [DllImport("kernel32", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern IntPtr LoadLibraryEx([MarshalAs(UnmanagedType.LPWStr)]string lpFileName, IntPtr hFile, uint dwFlags);

        /// <summary>
        /// Retrieves the address of an exported function or variable from the specified dynamic-link library (DLL).
        /// https://msdn.microsoft.com/en-us/library/windows/desktop/ms683212.aspx
        /// </summary>
        /// <param name="hModule">A handle to the DLL module that contains the function or variable.</param>
        /// <param name="lpProcName">
        ///     The function or variable name, or the function's ordinal value. 
        ///     If this parameter is an ordinal value, it must be in the low-order word; 
        ///     the high-order word must be zero.
        /// </param>
        /// <returns>the address of the exported function or variable if true.</returns>
        [DllImport("kernel32", SetLastError = true, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true, ExactSpelling = true /* without postfix A/W */)]
        public static extern IntPtr GetProcAddress(IntPtr hModule, [MarshalAs(UnmanagedType.LPStr)] string lpProcName);

        /// <summary>
        /// Frees the loaded dynamic-link library (DLL) module and, if necessary, decrements its reference count.
        /// When the reference count reaches zero, the module is unloaded from the address space of the calling process and the handle is no longer valid.
        /// https://msdn.microsoft.com/en-us/library/windows/desktop/ms683152.aspx
        /// </summary>
        /// <param name="hModule">A handle to the loaded library module.</param>
        /// <returns>If the function succeeds, the return value is nonzero.</returns>
        [DllImport("kernel32", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool FreeLibrary(IntPtr hModule);
    }
}
