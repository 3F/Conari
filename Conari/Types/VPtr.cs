﻿/*!
 * Copyright (c) 2016  Denis Kuzmin <x-3F@outlook.com> github/3F
 * Copyright (c) Conari contributors https://github.com/3F/Conari/graphs/contributors
 * Licensed under the MIT License (MIT).
 * See accompanying LICENSE.txt file or visit https://github.com/3F/Conari
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

        public static bool operator >(VPtr a, long b) => a.IsLong ? a.vLong > b : a.vIntPtr.ToInt64() > b;

        public static bool operator <(VPtr a, long b) => a.IsLong ? a.vLong < b : a.vIntPtr.ToInt64() < b;

        public static bool operator >=(VPtr a, long b) => a.IsLong ? a.vLong >= b : a.vIntPtr.ToInt64() >= b;

        public static bool operator <=(VPtr a, long b) => a.IsLong ? a.vLong <= b : a.vIntPtr.ToInt64() <= b;

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

        public static bool operator ==(VPtr a, VPtr b)
        {
            if(a.IsLong && b.IsLong) return a.vLong == b.vLong;

            if(a.IsLong) return a.vLong == b.vIntPtr.ToInt64();
            if(b.IsLong) return b.vLong == a.vIntPtr.ToInt64();

            return a.Equals(b);
        }

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

        public override string ToString() => IsLong ? vLong.ToString() : "0x" + vIntPtr.ToString("x");

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
