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
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using net.r_eg.Conari.Exceptions;
using net.r_eg.Conari.Extension;
using net.r_eg.Conari.Native.Core;
using net.r_eg.Conari.Resources;

namespace net.r_eg.Conari.Types
{
    using static Static.Members;

    public class NativeArray: NativeArray<int>, IMarshalableGeneric, ISerializable, IEnumerable<int>, IEnumerable, IDisposable
    {
        /// <inheritdoc cref="NativeArray{T}(int)"/>
        public NativeArray(int length)
            : base(length)
        {

        }

        /// <inheritdoc cref="NativeArray{T}(T[])"/>
        public NativeArray(params int[] input)
            : base(input)
        {

        }

        /// <inheritdoc cref="NativeArray{T}(int, IntPtr)"/>
        public NativeArray(int length, IntPtr pointer)
            : base(length, pointer)
        {

        }
    }

    [DebuggerDisplay("{DbgInfo}")]
    [Serializable]
    public class NativeArray<T>: IMarshalableGeneric, ISerializable, IEnumerable<T>, IEnumerable, IDisposable
        where T : struct
    {
        protected Memory memory;

        public Type MarshalableType { get; } = typeof(T);

        public IntPtr AddressPtr => this;

        /// <summary>
        /// Length of array.
        /// </summary>
        public int Length { get; protected set; }

        /// <summary>
        /// Produced native data size in bytes.
        /// </summary>
        public int Size { get; protected set; }

        /// <summary>
        /// Who is the owner. True indicates its own allocating.
        /// </summary>
        public bool Owner { get; protected set; }

        public T this[int index]
        {
            get => validate(index).memory.move(SizeOf<T>(index), Zone.D).read<T>();
            set => validate(index).memory.move(SizeOf<T>(index), Zone.D).write(value);
        }

        [NativeType]
        public static implicit operator IntPtr(NativeArray<T> v)
        {
            if(v.disposed) throw new AbandonedPointerException(v.memory?.InitialPtr ?? IntPtr.Zero);
            return v.memory.InitialPtr;
        }

        public static implicit operator Memory(NativeArray<T> v) => new((IntPtr)v);

        public static implicit operator T[](NativeArray<T> v)
        {
            v.memory.D.read(v.Length, out T[] readed);
            return readed;
        }

        public IEnumerator<T> GetEnumerator()
        {
            IAccessor reader = memory.D;
            for(int i = 0; i< Length; ++i)
            {
                yield return reader.read<T>();
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public static bool operator ==(NativeArray<T> a, T[] b) => a.Equals(b);

        public static bool operator !=(NativeArray<T> a, T[] b) => !(a == b);

        public static bool operator ==(T[] a, NativeArray<T> b) => b.Equals(a);

        public static bool operator !=(T[] a, NativeArray<T> b) => !(b == a);

        public static bool operator ==(NativeArray<T> a, NativeArray<T> b) => a.Equals(b);

        public static bool operator !=(NativeArray<T> a, NativeArray<T> b) => !(a == b);

        public override bool Equals(object obj)
        {
            if(obj is null) return false;
            if(obj is T[] r) return memory.D.eq(r).check();

            if(obj is not NativeArray<T> b) return false;

            // NOTE: actual values can be different for pointer == b.pointer

            if(Length != b.Length || Size != b.Size)
            {
                return false;
            }

            int idx = 0;
            foreach(T v in this)
            {
                if(!v.Equals(b[idx++])) return false;
            }
            return true;
        }

        public override int GetHashCode()
        {
            return 0.CalculateHashCode
            (
                memory,
                Length,
                Size,
                Owner
            );
        }

        public override string ToString()
        {
            if(memory == null || memory.InitialPtr == VPtr.Zero) return null;
            return string.Join(", ", this.ToArray());
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if(info == null) throw new ArgumentNullException(nameof(info));

            info.AddValue(nameof(memory), memory);
            info.AddValue(nameof(Length), Length);
            info.AddValue(nameof(Size), Size);
            info.AddValue(nameof(Owner), Owner);
        }

        /// <summary>
        /// Create a new native unmanaged T array without initialization.
        /// </summary>
        /// <remarks>Array will not be initialized! To initialize, use <see cref="NativeArray{T}(T[])"/> instead.</remarks>
        /// <param name="length">Length of array.</param>
        public NativeArray(int length)
        {
            if(length < 0) throw new ArgumentOutOfRangeException(nameof(length));

            Length      = length;
            Size        = SizeOf<T>(length);
            memory      = Marshal.AllocHGlobal(Size);
            Owner       = true;
        }

        /// <summary>
        /// Create a new native unmanaged T array with initialization using dotnet managed environment.
        /// </summary>
        /// <param name="input">Input managed data.</param>
        public NativeArray(params T[] input)
            : this(input?.Length ?? throw new ArgumentNullException(nameof(input)))
        {
            memory.write(input);
        }

        /// <summary>
        /// New <see cref="NativeArray{T}"/> instance to work with already allocated unmanaged T array.
        /// </summary>
        /// <param name="length">Length of array.</param>
        /// <param name="pointer">Attach available data via valid pointer.</param>
        public NativeArray(int length, IntPtr pointer)
        {
            if(pointer == IntPtr.Zero) throw new ArgumentOutOfRangeException(Msg.invalid_pointer);
            if(length < 0) throw new ArgumentOutOfRangeException(nameof(length));

            memory  = pointer;
            Length  = length;
            Size    = SizeOf<T>(length);
        }

        protected NativeArray<T> validate(int index)
        {
            if(index < 0 || index >= Length) throw new ArgumentOutOfRangeException(nameof(index));
            return this;
        }

        protected void release(bool disposing = true)
        {
            if(Owner)
            {
                Marshal.FreeHGlobal(memory.InitialPtr);
                Owner = false;
            }

            if(disposing)
            {
                memory = null;
            }
        }
        
        #region IDisposable

        private bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if(!disposed)
            {
                release(disposing);
                disposed = true;
            }
        }

        ~NativeArray() => Dispose(false);

        #endregion

        #region DebuggerDisplay

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string DbgInfo
            => (memory == null || memory.InitialPtr == VPtr.Zero) 
                ? "<nullptr>"
                : $"[ {Size} bytes at 0x{((IntPtr)memory.InitialPtr).ToString("x")} ]";

        #endregion
    }
}
