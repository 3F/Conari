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

using net.r_eg.Conari.Types;

namespace net.r_eg.Conari.Native.Core
{
    public abstract class PointerAbstract: IPointer
    {
        private readonly VPtr initial;
        private VPtr region;

        public abstract VPtr CurrentPtr { get; set; }

        public VPtr InitialPtr => initial;

        public VPtr RegionPtr => region;

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

        public static implicit operator VPtr(PointerAbstract v) => v.CurrentPtr;

        protected PointerAbstract(VPtr ptr)
        {
            initial = CurrentPtr = ptr;
            shiftRegionPtr();
        }
    }
}
