/*!
 * Copyright (c) 2016  Denis Kuzmin <x-3F@outlook.com> github/3F
 * Copyright (c) Conari contributors https://github.com/3F/Conari/graphs/contributors
 * Licensed under the MIT License (MIT).
 * See accompanying LICENSE.txt file or visit https://github.com/3F/Conari
*/

namespace ConariTest._svc.Extensions
{
    internal static class PlatformExtension
    {
#if !NETCORE
        internal static bool Contains(this string str, string value, System.StringComparison comparisonType)
        {
            if(str == null || value == null) return false;
            return str.IndexOf(value, comparisonType) != -1;
        }
#endif
    }
}
