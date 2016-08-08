/*
 * The MIT License (MIT)
 * 
 * Copyright (c) 2016  Denis Kuzmin <entry.reg@gmail.com>
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

namespace net.r_eg.Conari.Native.Core
{
    using TFields = List<Field>;

    public class BType: DynamicObject
    {
        public const string DYN_EMPTY_FLD_NAME = "$-noname-$";

        protected byte[] data;

        public TFields Fields
        {
            get;
            protected set;
        }

        public byte[] FieldsBinary
        {
            get
            {
                var ret = new List<byte>();

                foreach(var f in Fields) {
                    byte[] raw = BitConverter.GetBytes(f.value);
                    ret.AddRange(raw);
                }

                return ret.ToArray();
            }
        }

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

        protected TFields assign(TFields fields, byte[] data)
        {
            var ret = new TFields();

            if(fields == null || fields.Count < 1) {
                return fields;
            }

            int offset = 0;
            foreach(var f in fields)
            {
                var val = new Field(f);
                setValue(val, ref offset, ref data);
                ret.Add(val);
            }

            return ret;
        }

        protected void setValue(Field field, ref int offset, ref byte[] data)
        {
            // the BinaryReader as variant...

            byte[] val  = range(ref data, offset, field.tsize);
            offset      += field.tsize;

            if(field.type == typeof(Byte)) {
                field.value = (Byte)val[0];
                return;
            }

            if(field.type == typeof(SByte)) {
                field.value = (SByte)val[0];
                return;
            }

            if(field.type == typeof(Int16)) {
                field.value = BitConverter.ToInt16(val, 0);
                return;
            }

            if(field.type == typeof(UInt16)) {
                field.value = BitConverter.ToUInt16(val, 0);
                return;
            }

            if(field.type == typeof(Int32)) {
                field.value = BitConverter.ToInt32(val, 0);
                return;
            }

            if(field.type == typeof(UInt32)) {
                field.value = BitConverter.ToUInt32(val, 0);
                return;
            }

            if(field.type == typeof(Int64)) {
                field.value = BitConverter.ToInt64(val, 0);
                return;
            }

            if(field.type == typeof(UInt64)) {
                field.value = BitConverter.ToUInt64(val, 0);
                return;
            }

            if(field.type == typeof(IntPtr))
            {
                if(IntPtr.Size == sizeof(Int64)) {
                    field.value = BitConverter.ToInt64(val, 0);
                }
                else {
                    field.value = BitConverter.ToInt32(val, 0);
                }

                return;
            }

            if(field.type == typeof(UIntPtr))
            {
                if(UIntPtr.Size == sizeof(UInt64)) {
                    field.value = BitConverter.ToUInt64(val, 0);
                }
                else {
                    field.value = BitConverter.ToUInt32(val, 0);
                }

                return;
            }

            if(field.type == typeof(Single)) {
                field.value = BitConverter.ToSingle(val, 0);
                return;
            }

            if(field.type == typeof(Double)) {
                field.value = BitConverter.ToDouble(val, 0);
                return;
            }

            if(field.type == typeof(Boolean)) {
                field.value = BitConverter.ToBoolean(val, 0);
                return;
            }

            if(field.type == typeof(Char)) {
                field.value = BitConverter.ToChar(val, 0);
                return;
            }

            if(field.type == typeof(String))
            {
                if(IntPtr.Size == sizeof(Int64)) {
                    field.value = BitConverter.ToInt64(val, 0);
                }
                else {
                    field.value = BitConverter.ToInt32(val, 0);
                }
                
                //field.value = new Types.CharPtr((IntPtr)field.value);
                return;
            }
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

        private byte[] range(ref byte[] data, int offset, int len)
        {
            int idx = 0;
            byte[] ret = new byte[Math.Max(0, len)];

            len = offset + len;
            while(offset < len) {
                ret[idx++] = data[offset++];
            }
            return ret;
        }
    }
}
