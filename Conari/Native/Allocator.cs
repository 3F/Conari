/*!
 * Copyright (c) 2016  Denis Kuzmin <x-3F@outlook.com> github/3F
 * Copyright (c) Conari contributors https://github.com/3F/Conari/graphs/contributors
 * Licensed under the MIT License (MIT).
 * See accompanying LICENSE.txt file or visit https://github.com/3F/Conari
*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using net.r_eg.Conari.Native.Core;
using net.r_eg.Conari.Types;

namespace net.r_eg.Conari.Native
{
    public class Allocator: NativeArray<byte>, ISerializable, IEnumerable<byte>, IEnumerable, IDisposable
    {
        /// <summary>
        /// Access data in unmanaged memory using <see cref="Core.Memory"/> implementation of <see cref="IAccessor"/>.
        /// </summary>
        public Memory Memory => new((IntPtr)this);

        /// <summary>
        /// Construct native chains using <see cref="NativeData"/>.
        /// </summary>
        public NativeData Native => new((IntPtr)this);

        /// <summary>
        /// Allocates new uninitialized space in unmanaged memory.
        /// </summary>
        /// <param name="size">Reserved space in bytes.</param>
        public Allocator(int size)
            : base(size)
        {

        }

        /// <summary>
        /// Allocates new space in unmanaged memory with initialization using dotnet managed environment.
        /// </summary>
        /// <param name="input">Input managed data.</param>
        public Allocator(params byte[] input)
            : base(input)
        {

        }
    }
}
