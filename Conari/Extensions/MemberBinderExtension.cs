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
