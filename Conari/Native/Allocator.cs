/*
 * The MIT License (MIT)
 *
 * Copyright (c) 2016-2021  Denis Kuzmin <x-3F@outlook.com> github/3F
 * Copyright (c) Conari contributors https://github.com/3F/Conari/graphs/contributors
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
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
