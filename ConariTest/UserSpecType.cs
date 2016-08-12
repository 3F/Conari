using net.r_eg.Conari.Types;

namespace net.r_eg.ConariTest
{
    struct UserSpecUintType
    {
        private uint val;

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
