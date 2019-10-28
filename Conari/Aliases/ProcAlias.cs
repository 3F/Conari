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

namespace net.r_eg.Conari.Aliases
{
    public struct ProcAlias: IAlias
    {
        /// <summary>
        /// The final name.
        /// </summary>
        public string Name
        {
            get {
                return _name;
            }
            set
            {
                if(String.IsNullOrWhiteSpace(value)) {
                    throw new ArgumentException("The value cannot be null or empty.");
                }
                _name = value;
            }
        }
        private string _name;

        /// <summary>
        /// Configuration of alias.
        /// </summary>
        public IAliasCfg Cfg
        {
            get;
            private set;
        }

        public static implicit operator string(ProcAlias pa)
        {
            return pa.Name;
        }

        public static implicit operator ProcAlias(string str)
        {
            return new ProcAlias() {
                Name = str
            };
        }

        public ProcAlias(string name)
            : this()
        {
            Name = name;
        }

        public ProcAlias(string name, IAliasCfg cfg)
            : this(name)
        {
            Cfg = cfg;
        }
    }
}
