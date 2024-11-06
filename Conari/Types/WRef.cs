/*!
 * Copyright (c) 2016  Denis Kuzmin <x-3F@outlook.com> github/3F
 * Copyright (c) Conari contributors https://github.com/3F/Conari/graphs/contributors
 * Licensed under the MIT License (MIT).
 * See accompanying LICENSE.txt file or visit https://github.com/3F/Conari
*/

namespace net.r_eg.Conari.Types
{
    /// <summary>
    /// Reference type wrapper for use inside value type data.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal sealed class WRef<T> 
    {
        public T value;

        public static implicit operator T(WRef<T> obj)
        {
            return (obj == default) ? default : obj.value;
        }

        public WRef(T value)
        {
            this.value = value;
        }

        public WRef()
        {

        }
    }
}
