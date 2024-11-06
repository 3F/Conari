/*!
 * Copyright (c) 2016  Denis Kuzmin <x-3F@outlook.com> github/3F
 * Copyright (c) Conari contributors https://github.com/3F/Conari/graphs/contributors
 * Licensed under the MIT License (MIT).
 * See accompanying LICENSE.txt file or visit https://github.com/3F/Conari
*/

using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using Microsoft.CSharp.RuntimeBinder;
using net.r_eg.Conari.Resources;

namespace net.r_eg.Conari.Extension
{
    /// <summary>
    /// welcometothehell
    /// TODO: FIXME
    /// </summary>
    internal static class MemberBinderExtension
    {
        internal static IEnumerable<Type> GetGenericArgTypes(this InvokeMemberBinder binder)
        {
            if(binder == null) {
                throw new ArgumentNullException(nameof(binder));
            }

            // FIXME: avoid access to private members

            if(binder.TryGetPropertyValue(out object types, "TypeArguments", true) // .NET Core
                || binder.TryGetPropertyValue(out types, "Microsoft.CSharp.RuntimeBinder.ICSharpInvokeOrInvokeMemberBinder.TypeArguments", true) // .NET Framework
                || binder.TryGetFieldValue(out types, "Microsoft.CSharp.RuntimeBinder.CSharpInvokeMemberBinder.typeArguments", true)) // Mono
            {
                return types as IEnumerable<Type>;
            }

            throw new ArgumentException(Msg.failed_accessing_to_generic_types);
        }

        internal static IEnumerable<CSharpArgumentInfo> GetArgInfo(this InvokeMemberBinder binder)
        {
            if(binder == null) {
                throw new ArgumentNullException(nameof(binder));
            }

            // FIXME: avoid access to private members

            if(binder.TryGetFieldValue(out object info, "_argumentInfo", true) // .NET Core
                || binder.TryGetPropertyValue(out info, "Microsoft.CSharp.RuntimeBinder.ICSharpInvokeOrInvokeMemberBinder.ArgumentInfo", true) // .NET Framework
                || binder.TryGetFieldValue(out info, "Microsoft.CSharp.RuntimeBinder.CSharpInvokeMemberBinder.argumentInfo", true)) // Mono
            {
                return info as IEnumerable<CSharpArgumentInfo>;
            }

            throw new ArgumentException(Msg.failed_accessing_to_generic_types);
        }

        internal static Type GetCallingContext(this InvokeMemberBinder binder)
        {
            if(binder == null) {
                throw new ArgumentNullException(nameof(binder));
            }

            // FIXME: avoid access to private members

            var cache = binder.GetFieldValue("Cache", true) as Dictionary<Type, object>; // .NET Core + .NET Framework
            Type type = cache?.FirstOrDefault().Key;

            if(type != null) {
                return type;
            }

            if(binder.TryGetPropertyValue(out object context, "CallingContext", true) // .NET Core
                || binder.TryGetPropertyValue(out context, "Microsoft.CSharp.RuntimeBinder.ICSharpInvokeOrInvokeMemberBinder.CallingContext", true) // .NET Framework
                || binder.TryGetFieldValue(out context, "Microsoft.CSharp.RuntimeBinder.CSharpInvokeMemberBinder.callingContext", true)) // Mono
            {
                return context as Type;
            }

            throw new ArgumentException(Msg.failed_accessing_to_generic_types);
        }
    }
}
