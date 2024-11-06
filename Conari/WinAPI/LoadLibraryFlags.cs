/*!
 * Copyright (c) 2016  Denis Kuzmin <x-3F@outlook.com> github/3F
 * Copyright (c) Conari contributors https://github.com/3F/Conari/graphs/contributors
 * Licensed under the MIT License (MIT).
 * See accompanying LICENSE.txt file or visit https://github.com/3F/Conari
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
