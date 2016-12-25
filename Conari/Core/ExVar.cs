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
using net.r_eg.Conari.Native;

namespace net.r_eg.Conari.Core
{
    public class ExVar: IExVar
    {
        private IProvider provider;

        /// <summary>
        /// Gets value from exported Variable. Full name is required.
        /// </summary>
        /// <typeparam name="T">The type of variable.</typeparam>
        /// <param name="lpProcName">The full name of exported variable.</param>
        /// <returns>The value from exported variable.</returns>
        public T getVar<T>(string lpProcName)
        {
            return getField<T>(lpProcName).value;
        }

        /// <summary>
        /// Gets value from exported Variable.
        /// The main prefix will affects on this result.
        /// </summary>
        /// <typeparam name="T">The type of variable.</typeparam>
        /// <param name="variable">The name of exported variable.</param>
        /// <returns>The value from exported variable.</returns>
        public T get<T>(string variable)
        {
            return getVar<T>(provider.procName(variable));
        }

        /// <summary>
        /// Get field with native data from export table.
        /// Uses type for information about data.
        /// </summary>
        /// <param name="type">To consider it as this type.</param>
        /// <param name="name">The name of record.</param>
        /// <returns></returns>
        public Native.Core.Field getField(Type type, string name)
        {
            return getField(
                name,
                provider
                    .Svc
                    .native(name)
                    .t(type, name)
            );
        }

        /// <summary>
        /// Alias to `getField(Type type, string name)`
        /// 
        /// Get field with native data from export table.
        /// Uses type for information about data.
        /// </summary>
        /// <typeparam name="T">To consider it as T type.</typeparam>
        /// <param name="name">The name of record.</param>
        /// <returns></returns>
        public Native.Core.Field getField<T>(string name)
        {
            return getField(typeof(T), name);
        }

        /// <summary>
        /// Get field with native data from export table.
        /// Uses size of unspecified unmanaged type in bytes. 
        /// To calculate it from managed types, see: `NativeData.SizeOf`
        /// </summary>
        /// <param name="size">The size of raw-data in bytes.</param>
        /// <param name="name">The name of record.</param>
        /// <returns></returns>
        public Native.Core.Field getField(int size, string name)
        {
            return getField(
                name,
                provider
                    .Svc
                    .native(name)
                    .t(size, name)
            );
        }

        public ExVar(IProvider p)
        {
            if(p == null) {
                throw new ArgumentException("Provider cannot be null.");
            }
            provider = p;
        }

        protected Native.Core.Field getField(string name, NativeData n)
        {
            return n.Raw.Type.getFieldByName(name);
        }
    }
}
