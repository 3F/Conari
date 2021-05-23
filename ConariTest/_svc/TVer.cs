namespace ConariTest._svc
{
    internal struct TVer
    {
        public int major;
        public int minor;
        public int patch;

        public TVer(int major, int minor, int patch)
        {
            this.major = major;
            this.minor = minor;
            this.patch = patch;
        }
    }
}
