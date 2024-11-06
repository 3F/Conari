/*!
 * Copyright (c) 2016  Denis Kuzmin <x-3F@outlook.com> github/3F
 * Copyright (c) Conari contributors https://github.com/3F/Conari/graphs/contributors
 * Licensed under the MIT License (MIT).
 * See accompanying LICENSE.txt file or visit https://github.com/3F/Conari
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
