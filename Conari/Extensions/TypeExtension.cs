/*!
 * Copyright (c) 2016  Denis Kuzmin <x-3F@outlook.com> github/3F
 * Copyright (c) Conari contributors https://github.com/3F/Conari/graphs/contributors
 * Licensed under the MIT License (MIT).
 * See accompanying LICENSE.txt file or visit https://github.com/3F/Conari
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
