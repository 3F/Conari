/*!
 * Copyright (c) 2016  Denis Kuzmin <x-3F@outlook.com> github/3F
 * Copyright (c) Conari contributors https://github.com/3F/Conari/graphs/contributors
 * Licensed under the MIT License (MIT).
 * See accompanying LICENSE.txt file or visit https://github.com/3F/Conari
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
