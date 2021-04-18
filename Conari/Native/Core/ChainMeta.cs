﻿/*
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

namespace net.r_eg.Conari.Native.Core
{
    internal sealed class ChainMeta
    {
        private int flaggedChainSize;

        /// <summary>
        /// Produced data size in the chain before final build.
        /// </summary>
        public int ChainSize { get; private set; }

        /// <summary>
        /// Produced data size in the chain before final build between specific points.
        /// Returns 0 if delta is zero.
        /// </summary>
        public int FlaggedChainSize => ChainSize == flaggedChainSize ? 0 : flaggedChainSize;

        public int updateSize(int size, bool ignoreFlagged = false)
        {
            ChainSize += size;
            if(!ignoreFlagged) flaggedChainSize += size;
            return size;
        }

        public void resetChainSize() => ChainSize = 0;
        public void resetFlaggedChainSize() => flaggedChainSize = 0;
    }
}
