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
using System.Reflection;

namespace net.r_eg.Conari.Core
{
    public sealed class ProviderDLR: DynamicObject, IProviderDLR
    {
        private IProvider provider;
        private MIcache micache = new MIcache();

        /// <summary>
        /// To cache dynamic types with similar signatures:
        ///     `{return type} name( [{argument types}] )`
        /// </summary>
        public bool Cache
        {
            get {
                return _cache;
            }
            set {
                if(!value) {
                    micache.Clear();
                }
                _cache = value;
            }
        }
        private bool _cache = true;

        /// <summary>
        /// Magic methods. Invoking.
        /// </summary>
        /// <![CDATA[
        ///     `[result =] name<return_type>([{argument_types}])`
        /// ]]>
        /// <param name="binder"></param>
        /// <param name="args"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            var tArgs       = args.Select(a => a.GetType()).ToArray();
            var tGeneric    = getGenericArgTypes(binder).ToArray();

            MethodInfo mi   = getmi(binder.Name, tArgs, tGeneric);
            TDyn dyn        = provider.bind(mi, provider.funcName(binder.Name));
            result          = Dynamic.DCast(dyn.returnType, dyn.dynamic.Invoke(null, args));

            return true;
        }

        public ProviderDLR(IProvider provider)
        {
            if(provider == null) {
                throw new ArgumentException("Provider cannot be null.");
            }
            this.provider = provider;
        }

        private MethodInfo getmi(string name, Type[] args, Type[] generic)
        {
            if(generic.Length > 1) {
                throw new ArgumentException("Allowed only one type (as a return type) for this generic method.");
            }
            Type retType = (generic.Length < 1) ? typeof(void) : generic[0];

            if(!Cache) {
                return Dynamic.GetMethodInfo(name, retType, args);
            }

            var key = new Type[args.Length + 1];
            key[0]  = retType;
            args.CopyTo(key, 1);

            if(!micache.ContainsKey(key)) {
                micache[key] = Dynamic.GetMethodInfo(name, retType, args);
            }

            return micache[key];
        }

        private IEnumerable<Type> getGenericArgTypes(InvokeMemberBinder binder)
        {
            PropertyInfo pinf = binder
                                    .GetType()
                                    .GetProperty(
                                        "Microsoft.CSharp.RuntimeBinder.ICSharpInvokeOrInvokeMemberBinder.TypeArguments",
                                        BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public
                                    );

            return pinf.GetValue(binder, null) as IEnumerable<Type>;
        }
    }
}
