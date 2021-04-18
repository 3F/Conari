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
using System.Linq;

namespace net.r_eg.Conari.Native.Core
{
    using Fields = List<Field>;

    public sealed class Raw: IEnumerable<byte>, IEnumerable
    {
        private readonly Fields fields;

        private readonly long offset;
        private readonly long length;

        private readonly INativeReader reader;
        private readonly ChainMeta meta;

        public IEnumerator<byte> GetEnumerator() => Iter.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => Iter.GetEnumerator();

        /// <summary>
        /// Final byte-sequence from values (via pointer or local data).
        /// </summary>
        public byte[] Values => Iter.ToArray();

        /// <summary>
        /// Access to byte-sequence from specified values via pointer or local data.
        /// </summary>
        public IEnumerable<byte> Iter { get
        {
            reader.move(offset, SeekPosition.Region);

            for(long i = offset; i < length; ++i)
            {
                yield return reader.readByte();
            }
        }}

        /// <inheritdoc cref="build()"/>
        /// <remarks>Alias to <see cref="build()"/></remarks>
        public BType Type => build();

        /// <summary>
        /// <see cref="build()"/> and access <see cref="BType.DLR"/>.
        /// </summary>
        /// <remarks>Alias to <see cref="BType.DLR"/></remarks>
        public dynamic DLR => build().DLR;

        private int FlaggedChainSize => meta?.FlaggedChainSize ?? 0;

        /// <summary>
        /// Generates dynamic type using prepared byte-sequence.
        /// </summary>
        public BType build()
        {
            try
            {
                return new(Values, fields);
            }
            finally
            {
                reader.upPtr(-FlaggedChainSize);
            }
        }

        /// <remarks>Chained <see cref="build()"/></remarks>
        /// <inheritdoc cref="build()"/>
        public NativeData build(out dynamic result)
        {
            result = build();
            return reader.Native();
        }

        public Raw(INativeReader reader, long length, long offset = 0)
        {
            this.reader = reader ?? throw new ArgumentNullException(nameof(reader));
            this.length = length;
            this.offset = offset;
        }

        public Raw(byte[] bytes, Fields fields)
        {
            reader      =  new LocalReader(bytes ?? throw new ArgumentNullException(nameof(bytes)));
            this.fields = fields;
            length      = bytes.Length;
        }

        public Raw(INativeReader reader, Fields fields)
            : this(reader, fields.Sum(f => f.tsize), 0)
        {
            this.fields = fields;
        }

        public Raw(byte[] bytes)
            : this(bytes, null)
        {

        }

        internal Raw(INativeReader reader, Fields fields, ChainMeta meta)
            : this(reader, fields)
        {
            this.meta = meta;

            if(length < 1 && reader is LocalReader)
            {
                length = ((ILocalReader)reader).Length;
            }
        }
    }
}
