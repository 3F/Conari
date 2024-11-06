/*!
 * Copyright (c) 2016  Denis Kuzmin <x-3F@outlook.com> github/3F
 * Copyright (c) Conari contributors https://github.com/3F/Conari/graphs/contributors
 * Licensed under the MIT License (MIT).
 * See accompanying LICENSE.txt file or visit https://github.com/3F/Conari
*/

using System;
using System.Runtime.Serialization;
using net.r_eg.Conari.Extension;

namespace net.r_eg.Conari.Types
{
    [Serializable]
    public sealed class BufferedString<T>: NativeString<T>, ISerializable, IDisposable
        where T : struct
    {
        internal const float BUF = 2.5f;

        public BufferedString(string str)
            : base(str, str.RelativeLength(BUF))
        {

        }

        public BufferedString(int buffer = 0xFF)
            : base(buffer)
        {

        }

        public BufferedString(string str, int extend)
            : base(str, extend)
        {

        }

        public BufferedString(IntPtr pointer)
            : base(pointer, (int)Math.Ceiling((pointer == IntPtr.Zero ? 0 : GetLengthFromPtr(pointer)) * BUF))
        {

        }

        public BufferedString(IntPtr pointer, int extend)
            : base(pointer, extend)
        {

        }
    }
}
