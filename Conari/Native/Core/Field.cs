/*
 * The MIT License (MIT)
 *
 * Copyright (c) 2016-2019  Denis Kuzmin < entry.reg@gmail.com > GitHub/3F
 * Copyright (c) Conari contributors: https://github.com/3F/Conari/graphs/contributors
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
using System.Text.RegularExpressions;

namespace net.r_eg.Conari.Native.Core
{
    public sealed class Field
    {
        public Type type;

        /// <summary>
        /// Used size of type.
        /// </summary>
        public int tsize;

        /// <summary>
        /// Field name.
        /// </summary>
        public string name;

        /// <summary>
        /// The value of field.
        /// </summary>
        public dynamic value;

        /// <summary>
        /// User object for any purpose.
        /// </summary>
        public object user;

        /// <summary>
        /// Checks the correct name for DLR features.
        /// </summary>
        public bool IsValidForDLR
        {
            get
            {
                if(String.IsNullOrWhiteSpace(name)) {
                    return false;
                }
                return Regex.Match(name, "^[a-zA-Z_][a-zA-Z_0-9]*$").Success;
            }
        }

        /// <summary>
        /// Get bytes from current field.
        /// </summary>
        /// <returns></returns>
        public byte[] toBytes()
        {
            return BitConverter.GetBytes(value);
        }

        public Field(Type type, int tsize)
        {
            this.type   = type;
            this.tsize  = tsize;
        }

        public Field(Field f)
            : this(f.type, f.tsize)
        {
            name    = f.name;
            value   = f.value;
        }
    }
}
