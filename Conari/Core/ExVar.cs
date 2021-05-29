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
using net.r_eg.Conari.Extension;
using net.r_eg.Conari.Native;
using net.r_eg.Conari.Resources;

namespace net.r_eg.Conari.Core
{
    using DefaultType = Int32;

    public class ExVar: DynamicObject, IExVar, IDlrAccessor
    {
        private readonly IProvider provider;

        /// <summary>
        /// Access to dynamic features like getting exported-variables at runtime.
        /// </summary>
        public dynamic DLR => this;

        public dynamic _ => this;

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
        /// Alias to <see cref="getVar{T}(string)"/>
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
            return getField(typeof(T), provider.Svc.procName(variable, true)).value;
        }

        /// <summary>
        /// Alias to <see cref="get{T}(string)"/>
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
            return getField(type, provider.Svc.procName(name, false));
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
            return getField(size, provider.Svc.procName(name, false));
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
            throw new NotImplementedException(Msg.not_yet_impl);
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
            Type[] generic = binder.GetGenericArgTypes().ToArray();

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
            return ((ILoader)provider).PE?.Export.Names ?? throw new NotSupportedException(Msg.activate_pe);
        }

        public ExVar(IProvider provider)
        {
            this.provider = provider ?? throw new ArgumentNullException(nameof(provider));
        }

        protected dynamic getFieldDLR(Type type, string variable)
        {
            return getField(
                type,
                provider.Svc.tryAlias(variable)
            )
            .value;
        }

        protected Native.Core.Field getField(string name, NativeData n)
        {
            var ret = n.Raw.Type.getFieldByName(name);
            if(ret == null) {
                throw new KeyNotFoundException($"Field('{name}') was not found inside Raw.Type.");
            }
            return ret;
        }

        protected Native.Core.Field getField(Type type, LpProcName lpProcName)
        {
            return provider
                    .Svc
                    .native(lpProcName)
                    .t(type)
                    .Raw.Type.FirstField;
        }

        protected Native.Core.Field getField(int size, LpProcName lpProcName)
        {
            return provider
                    .Svc
                    .native(lpProcName)
                    .t(size)
                    .Raw.Type.FirstField;
        }
    }
}
