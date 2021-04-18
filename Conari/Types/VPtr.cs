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
using System.Diagnostics;
using System.Runtime.Serialization;
using net.r_eg.Conari.Extension;

namespace net.r_eg.Conari.Types
{
    /// <summary>
    /// Variable long pointer.
    /// </summary>
    [DebuggerDisplay("{(IsLong ? \"long: \" : \"IntPtr: \") + ToString()}")]
    [Serializable]
    public struct VPtr: ISerializable
    {
        private readonly IntPtr vIntPtr;
        private readonly long vLong;

        public static readonly VPtr Zero;
        public static readonly VPtr ZeroLong = new(0);

        public bool IsLong { get; private set; }

        public static implicit operator IntPtr(VPtr n) => n.vIntPtr;
        public static implicit operator long(VPtr n) => n.vLong;

        public static implicit operator VPtr(IntPtr n) => new(n);
        public static implicit operator VPtr(long n) => new(n);

        public static VPtr operator +(VPtr input, int offset)
        {
            if(input.IsLong) return input.vLong + offset;
            return input.vIntPtr + offset;
        }

        public static VPtr operator +(VPtr input, VPtr offset)
        {
            if(input.IsLong) return input.vLong + GetLongOfs(offset);
            return Calc(input.vIntPtr, GetLongOfs(offset), plus: true);
        }

        public static VPtr operator +(VPtr input, IntPtr offset) => input + new VPtr(offset);

        public static VPtr operator +(VPtr input, long offset) => input + new VPtr(offset);

        public static VPtr operator ++(VPtr input) => input += 1;

        public static VPtr operator -(VPtr input, int offset)
        {
            if(input.IsLong) return input.vLong - offset;
            return input.vIntPtr - offset;
        }

        public static VPtr operator -(VPtr input, VPtr offset)
        {
            if(input.IsLong) return input.vLong - GetLongOfs(offset);
            return Calc(input.vIntPtr, GetLongOfs(offset), plus: false);
        }

        public static VPtr operator -(VPtr input, IntPtr offset) => input - new VPtr(offset);

        public static VPtr operator -(VPtr input, long offset) => input - new VPtr(offset);

        public static VPtr operator --(VPtr input) => input -= 1;

        public static VPtr Add(VPtr input, VPtr offset) => input + offset;

        public static VPtr Add(VPtr input, IntPtr offset) => input + offset;

        public static VPtr Add(VPtr input, long offset) => input + offset;

        public static VPtr Add(VPtr input, int offset) => input + offset;

        public static VPtr Sub(VPtr input, VPtr offset) => input - offset;

        public static VPtr Sub(VPtr input, IntPtr offset) => input - offset;

        public static VPtr Sub(VPtr input, long offset) => input - offset;

        public static VPtr Sub(VPtr input, int offset) => input - offset;

        public static VPtr MakePtr(long value) => new(new IntPtr(value));

        public static VPtr MakeLong(long value) => new(value);

        public static VPtr MakePtr(IntPtr value) => new(value);

        public static VPtr MakeLong(IntPtr value) => new(value.ToInt64());

        public static bool operator ==(VPtr a, VPtr b) => a.Equals(b);

        public static bool operator !=(VPtr a, VPtr b) => !(a == b);

        public override bool Equals(object obj)
        {
            if(obj is null || !(obj is VPtr)) {
                return false;
            }

            var b = (VPtr)obj;

            return vIntPtr == b.vIntPtr
                    && vLong == b.vLong
                    && IsLong == b.IsLong;
        }

        public override int GetHashCode()
        {
            return 0.CalculateHashCode
            (
                vIntPtr,
                vLong,
                IsLong
            );
        }

        public override string ToString() => IsLong ? vLong.ToString() : "0x" + vIntPtr.ToString("X");

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if(info == null) throw new ArgumentNullException(nameof(info));

            info.AddValue(nameof(vIntPtr), vIntPtr);
            info.AddValue(nameof(vLong), vLong);
            info.AddValue(nameof(IsLong), IsLong);
        }

        public VPtr(IntPtr input, int offset)
            : this(input)
        {
            vIntPtr = input + offset;
        }

        public VPtr(long input, int offset)
            : this(input)
        {
            vLong = input + offset;
        }

        public VPtr(VPtr origin, int offset)
            : this()
        {
            if(origin.IsLong)
            {
                vLong = origin.vLong + offset;
            }
            else
            {
                vIntPtr = origin.vIntPtr + offset;
            }
        }

        public VPtr(IntPtr input)
            : this()
        {
            vIntPtr = input;
        }

        public VPtr(long input)
            : this()
        {
            vLong = input;
            IsLong = true;
        }

        private static VPtr Calc(IntPtr input, long offset, bool plus)
        {
            int inc = offset < 0 ? int.MinValue : int.MaxValue;

            IntPtr ret = input;
            long sum = 0;
            while(true)
            {
                long next = unchecked(sum + inc);
                if((inc < 0 && next >= sum) || (inc > 0 && next < sum)) break; // overflow

                sum = next;
                if((inc < 0 && sum <= offset) || (inc > 0 && sum >= offset)) break;

                ret = plus ? ret + inc : ret - inc;
            }

            inc -= (int)(sum - offset);
            return plus ? ret + inc : ret - inc;
        }

        private static long GetLongOfs(VPtr input) 
            => input.IsLong ? input.vLong : input.vIntPtr.ToInt64();
    }
}
