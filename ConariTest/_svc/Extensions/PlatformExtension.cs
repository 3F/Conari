
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
