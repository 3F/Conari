/*!
 * Copyright (c) 2016  Denis Kuzmin <x-3F@outlook.com> github/3F
 * Copyright (c) Conari contributors https://github.com/3F/Conari/graphs/contributors
 * Licensed under the MIT License (MIT).
 * See accompanying LICENSE.txt file or visit https://github.com/3F/Conari
*/

using System;
using System.Text.RegularExpressions;

namespace net.r_eg.Conari.Native.Core
{
    public sealed class Field
    {
        public Type type;

        /// <summary>
        /// Native size for <see cref="type"/>.
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
