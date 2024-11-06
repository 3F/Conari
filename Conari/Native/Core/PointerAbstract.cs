/*!
 * Copyright (c) 2016  Denis Kuzmin <x-3F@outlook.com> github/3F
 * Copyright (c) Conari contributors https://github.com/3F/Conari/graphs/contributors
 * Licensed under the MIT License (MIT).
 * See accompanying LICENSE.txt file or visit https://github.com/3F/Conari
*/

using System;
using System.Runtime.Serialization;
using net.r_eg.Conari.Types;

namespace net.r_eg.Conari.Native.Core
{
    [Serializable]
    public abstract class PointerAbstract: IPointer, ISerializable
    {
        private readonly VPtr initial;
        private VPtr region;

        public abstract VPtr CurrentPtr { get; set; }

        public VPtr InitialPtr => initial;

        public VPtr RegionPtr => region;

        public static implicit operator VPtr(PointerAbstract v) => v.CurrentPtr;

        public VPtr resetPtr() => region = CurrentPtr = InitialPtr;

        public VPtr shiftRegionPtr() => region = CurrentPtr;

        public VPtr resetRegionPtr() => CurrentPtr = RegionPtr;

        public VPtr getPtrFrom(long offset) => CurrentPtr + offset;

        public VPtr getAddr(long offset) => InitialPtr + offset;

        public VPtr upPtr(ref VPtr ptr)
        {
            ptr += 1;
            CurrentPtr += 1;
            return CurrentPtr;
        }

        public VPtr upPtr(long offset = 1) => CurrentPtr += offset;

        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if(info == null) throw new ArgumentNullException(nameof(info));

            info.AddValue(nameof(initial), initial);
            info.AddValue(nameof(region), region);
        }

        protected PointerAbstract(VPtr ptr)
        {
            initial = CurrentPtr = ptr;
            shiftRegionPtr();
        }
    }
}
