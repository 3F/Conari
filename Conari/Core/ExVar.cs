/*
 * The MIT License (MIT)
 * 
 * Copyright (c) 2016-2017  Denis Kuzmin <entry.reg@gmail.com>
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Conari"), to deal
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
using net.r_eg.Conari.Extension;
using net.r_eg.Conari.Native;

namespace net.r_eg.Conari.Core
{
    using DefaultType = Int32;

    public class ExVar: DynamicObject, IExVar
    {
        private IProvider provider;

        /// <summary>
        /// Access to dynamic features like getting exported-variables at runtime.
        /// </summary>
        public dynamic DLR
        {
            get {
                return this;
            }
        }

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
        /// Alias to `getVar&lt;T&gt;(string lpProcName)`
        /// Gets value from exported Variable. Full name is required.
        /// </summary>
        /// <param name="lpProcName">The full name of exported variable.</param>
        /// <returns>The value from exported variable.</returns>
        public DefaultType getVar(string lpProcName)
        {
            return getVar<DefaultType>(lpProcName);
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
        /// Alias to `get&lt;T&gt;(string variable)`
        /// 
        /// Gets value from exported Variable.
        /// The main prefix will affects on this result.
        /// </summary>
        /// <param name="variable">The name of exported variable.</param>
        /// <returns>The value from exported variable.</returns>
        public DefaultType get(string variable)
        {
            return get<DefaultType>(variable);
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

        /// <summary>
        /// Magic properties. Get.
        /// </summary>
        /// <param name="binder"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            result = getFieldDLR(typeof(DefaultType), binder.Name);
            return true;
        }

        /// <summary>
        /// Magic properties. Set.
        /// </summary>
        /// <param name="binder"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            throw new NotImplementedException("Not yet implemented.");
        }

        /// <summary>
        /// Magic methods. Invoking.
        /// </summary>
        /// <![CDATA[
        ///     `name<return_type>()`
        /// ]]>
        /// <param name="binder"></param>
        /// <param name="args"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            if(args?.Length > 0) {
                throw new ArgumentException("Arguments are not allowed for this method.");
            }
            Type[] generic = getGenericArgTypes(binder).ToArray();

            if(generic.Length > 1) {
                throw new ArgumentException("No more than one type (as a return type) allowed for this method.");
            }

            result = getFieldDLR(
                (generic.Length < 1) ? typeof(DefaultType) : generic[0],
                binder.Name
            );
            return true;
        }

        /// <summary>
        /// List of magic properties.
        /// </summary>
        /// <returns></returns>
        public override IEnumerable<string> GetDynamicMemberNames()
        {
            // TODO: the exported variables are free from decorations but '@' still may be used if it's not a C linkage
            // For example: LIBAPI_CPP const char* eVariableTest
            //         ->   ?eVariableTest@API@UnLib@Conari@r_eg@net@@3PBDB
            return ((ILoader)provider).PE.ExportedProcNames/*.Where(p => p.IndexOfAny(new[] { '@' }) == -1)*/;
        }

        public ExVar(IProvider p)
        {
            if(p == null) {
                throw new ArgumentException("Provider cannot be null.");
            }
            provider = p;
        }

        protected dynamic getFieldDLR(Type type, string variable)
        {
            return getField(type, provider.procName(variable)).value;
        }

        protected Native.Core.Field getField(string name, NativeData n)
        {
            var ret = n.Raw.Type.getFieldByName(name);
            if(ret == null) {
                throw new KeyNotFoundException($"Field('{name}') was not found inside Raw.Type.");
            }
            return ret;
        }

        private IEnumerable<Type> getGenericArgTypes(InvokeMemberBinder binder)
        {
            // FIXME: avoid access to private members
            return binder
                    .GetPropertyValue("Microsoft.CSharp.RuntimeBinder.ICSharpInvokeOrInvokeMemberBinder.TypeArguments", true)
                    as IEnumerable<Type>;
        }
    }
}
