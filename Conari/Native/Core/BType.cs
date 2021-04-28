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
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using net.r_eg.Conari.Core;

namespace net.r_eg.Conari.Native.Core
{
    using TFields = List<Field>;

    public class BType: DynamicObject, IDlrAccessor
    {
        public const string DYN_EMPTY_FLD_NAME = "$-noname-$";

        protected byte[] data;

        public dynamic DLR => this;

        public dynamic _ => this;

        public TFields Fields { get; protected set; }

        public Field FirstField => Fields?.FirstOrDefault();

        public byte[] FieldsBinary { get
        {
            List<byte> ret = new();
            Fields.ForEach(f => ret.AddRange(f.toBytes()));
            return ret.ToArray();
        }}

        public Field getFieldByName(string name) => Fields.Where(f => f?.name == name).FirstOrDefault();

        /// <summary>
        /// Magic fields. Get.
        /// </summary>
        /// <param name="binder"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            if(isNoname(binder.Name)) {
                result = Fields[Int32.Parse(getNonameIndex(binder.Name))].value;
                return true;
            }

            Field f = Fields.Where(p => p.name == binder.Name).FirstOrDefault();
            if(f == null) {
                throw new ArgumentException($"The field name {binder.Name} was not found to get value.");
            }

            result = f.value;
            return true;
        }

        /// <summary>
        /// Magic fields. Set.
        /// </summary>
        /// <param name="binder"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            Field f = Fields.Where(p => p.name == binder.Name).FirstOrDefault();
            if(f == null) {
                throw new ArgumentException($"The field name {binder.Name} was not found to set value.");
            }

            f.value = value;
            return true;
        }

        /// <summary>
        /// List of magic fields.
        /// </summary>
        /// <returns></returns>
        public override IEnumerable<string> GetDynamicMemberNames()
        {
            return Fields.Select((f, i) => String.IsNullOrWhiteSpace(f.name)? formatNoname(i) : f.name);
        }

        public BType(byte[] data, TFields fields)
        {
            this.data   = data;
            Fields      = assign(fields, data);
        }

        protected virtual bool isNoname(string name)
        {
            // (name.IndexOf('<') != -1)
            return name.EndsWith(DYN_EMPTY_FLD_NAME);
        }

        protected virtual string formatNoname(int index)
        {
            return $"[#{index}]{DYN_EMPTY_FLD_NAME}";
        }

        protected virtual string getNonameIndex(string name)
        {
            int left    = 2;
            int len     = name.IndexOf(']') - left;
            return name.Substring(left, len);
        }

        protected TFields assign(TFields fields, byte[] data)
        {
            var ret = new TFields();

            if(fields == null || fields.Count < 1) {
                return fields;
            }

            BReader br = new BReader(data, 0);

            foreach(var f in fields)
            {
                var val     = new Field(f);
                val.value   = br.next(val.type, val.tsize);
                ret.Add(val);
            }

            return ret;
        }
    }
}
