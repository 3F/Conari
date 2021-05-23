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
using System.Linq;
using System.Reflection;
using net.r_eg.Conari.Types;

namespace net.r_eg.Conari.Extension
{
    using static Static.Members;

    public static class TypeExtension
    {
        internal static Type TypeDeepByNativeAttr(this Type origin)
        {
            Type found = TypeByNativeAttr(origin);
            if(found != null) return found;

            //TODO: review
            foreach(var m in origin.GetImplicitConversions().Where(t => t.ReturnType != origin))
            {
                // we will use the first conversion
                if(m.ReturnType.Namespace != typeof(int).Namespace) {
                    return TypeDeepByNativeAttr(m.ReturnType);
                }
                return m.ReturnType;
            }

            return origin;
        }

        internal static Type TypeByNativeAttr(this Type origin)
        {
            // entire struct or class definition

            if(origin.GetCustomAttributes(false).Any(a => a is NativeTypeAttribute)) return origin;

            // through conversions

            foreach(MethodInfo m in origin.GetImplicitConversions())
                if(Attribute.GetCustomAttributes(m).Any(a => a is NativeTypeAttribute)) return m.ReturnType;

            foreach(MethodInfo m in origin.GetExplicitConversions())
                if(Attribute.GetCustomAttributes(m).Any(a => a is NativeTypeAttribute)) return m.ReturnType;

            return null;
        }

        internal static IEnumerable<MethodInfo> GetImplicitConversions(this Type type)
            => GetTypeConversions(type, "op_Implicit");

        internal static IEnumerable<MethodInfo> GetExplicitConversions(this Type type) 
            => GetTypeConversions(type, "op_Explicit");

        private static IEnumerable<MethodInfo> GetTypeConversions(Type type, string name)
            => type?
                    .GetMember(name, BindingFlags.Public | BindingFlags.Static)
                    .Select(t => (MethodInfo)t)
                ??
                EmptyArray<MethodInfo>();
    }
}
