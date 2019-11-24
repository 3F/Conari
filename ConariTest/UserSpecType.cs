using net.r_eg.Conari.Types;

namespace ConariTest
{
    struct UserSpecUintType
    {
        private readonly uint val;

        [NativeType]
        public static implicit operator uint(UserSpecUintType number)
        {
            return number.val;
        }

        public static implicit operator UserSpecUintType(uint number)
        {
            return new UserSpecUintType(number);
        }

        public UserSpecUintType(uint number)
        {
            val = number;
        }
    }
}
