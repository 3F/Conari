using net.r_eg.Conari.Types;

namespace ConariTest._svc
{
    internal struct UserSpecUintType
    {
        private readonly uint val;

        [NativeType]
        public static implicit operator uint(UserSpecUintType number) => number.val;

        public static implicit operator UserSpecUintType(uint number) => new(number);

        public UserSpecUintType(uint number) => val = number;
    }
}
