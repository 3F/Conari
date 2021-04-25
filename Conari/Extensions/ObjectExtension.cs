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
using System.Reflection;

namespace net.r_eg.Conari.Extension
{
    using Act = KeyValuePair<bool, object>;
    using static Static.Members;

    public static class ObjectExtension
    {
        /// <summary>
        /// Alias to `.Invoke(name, BindingFlags.Public | BindingFlags.Instance[, args])`
        /// Invoke specific public method from unspecified object.
        /// </summary>
        /// <param name="obj">Unspecified object.</param>
        /// <param name="name">The name of non-static public method from object.</param>
        /// <param name="args">Optional arguments.</param>
        /// <returns>The result from invoked method.</returns>
        public static object Invoke(this Object obj, string name, params object[] args)
        {
            return Invoke(obj, name, BindingFlags.Public /*| BindingFlags.NonPublic*/ | BindingFlags.Instance, args);
        }

        /// <summary>
        /// Invoke specific method from unspecified object.
        /// </summary>
        /// <param name="obj">Unspecified object.</param>
        /// <param name="name">The name of method from object.</param>
        /// <param name="flags">Control binding.</param>
        /// <param name="args">Optional arguments.</param>
        /// <returns>The result from invoked method.</returns>
        /// <exception cref="EntryPointNotFoundException">When the selected method with specific arguments was not found.</exception>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="TargetParameterCountException"></exception>
        /// <exception cref="MethodAccessException"></exception>
        public static object Invoke(this Object obj, string name, BindingFlags flags, params object[] args)
        {
            return Exec(() =>
            {
                MethodInfo mi = obj.GetType()
                                    .GetMethod(name, flags);

                return new Act(
                    mi != null,
                    mi?.Invoke(obj, args ?? EmptyArray<object>())
                );
            },
            obj, name);
        }

        /// <summary>
        /// Get field value from unspecified object.
        /// </summary>
        /// <param name="obj">Unspecified object.</param>
        /// <param name="name">The name of field from object.</param>
        /// <param name="nonPublic">Is a non-public ?</param>
        /// <param name="isStatic">Is a static ?</param>
        /// <returns>The value from selected field.</returns>
        public static object GetFieldValue(this Object obj, string name, bool nonPublic = false, bool isStatic = false)
        {
            return GetFieldValue(obj, name, DefaultFlags(nonPublic, isStatic));
        }

        /// <summary>
        /// Get field value from unspecified object.
        /// </summary>
        /// <param name="obj">Unspecified object.</param>
        /// <param name="name">The name of field from object.</param>
        /// <param name="flags">Control binding.</param>
        /// <returns>The value from selected field.</returns>
        /// <exception cref="EntryPointNotFoundException">When the selected field was not found.</exception>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="FieldAccessException"></exception>
        public static object GetFieldValue(this Object obj, string name, BindingFlags flags)
        {
            return Exec(() => 
            {
                FieldInfo fi = obj.GetType()
                                    .GetField(name, flags);

                return new Act(fi != null, fi?.GetValue(obj));
            }, 
            obj, name);
        }

        internal static bool TryGetFieldValue(this object obj, out object value, string name, bool nonPublic = false)
        {
            FieldInfo fi = obj?.GetType().GetField(name, DefaultFlags(nonPublic, false));

            if(fi == null)
            {
                value = null;
                return false;
            }
            
            value = fi.GetValue(obj);
            return true;
        }

        /// <summary>
        /// Get property value from unspecified object.
        /// </summary>
        /// <param name="obj">Unspecified object.</param>
        /// <param name="name">The name of property from object.</param>
        /// <param name="nonPublic">Is a non-public ?</param>
        /// <param name="isStatic">Is a static ?</param>
        /// <returns>The value from selected property.</returns>
        public static object GetPropertyValue(this Object obj, string name, bool nonPublic = false, bool isStatic = false)
        {
            return GetPropertyValue(obj, name, DefaultFlags(nonPublic, isStatic));
        }

        internal static bool TryGetPropertyValue(this object obj, out object value, string name, bool nonPublic = false)
        {
            PropertyInfo pi = obj?.GetType().GetProperty(name, DefaultFlags(nonPublic, false));

            if(pi == null)
            {
                value = null;
                return false;
            }

            value = pi.GetValue(obj, null);
            return true;
        }

        /// <summary>
        /// Get property value from unspecified object.
        /// </summary>
        /// <param name="obj">Unspecified object.</param>
        /// <param name="name">The name of property from object.</param>
        /// <param name="flags">Control binding.</param>
        /// <returns>The value from selected property.</returns>
        /// <exception cref="EntryPointNotFoundException">When the selected property was not found.</exception>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="MethodAccessException"></exception>
        public static object GetPropertyValue(this Object obj, string name, BindingFlags flags)
        {
            return Exec(() => 
            {
                PropertyInfo pi = obj.GetType()
                                        .GetProperty(name, flags);

                return new Act(pi != null, pi?.GetValue(obj, null));
            }, 
            obj, name);
        }

        /// <summary>
        /// Execute action on value in the chain separately from result.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="act"></param>
        /// <returns>Input value to continue the chain.</returns>
        public static T E<T>(this T obj, Action act) => E(obj, _=> act());

        /// <summary>
        /// Execute action on value in the chain separately from result.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="act"></param>
        /// <returns>Input value to continue the chain.</returns>
        public static T E<T>(this T obj, Action<T> act)
        {
            act?.Invoke(obj);
            return obj;
        }

        /// <summary>
        /// Perform an action on the value in the chain according to the condition.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="condition">Must be true to execute action.</param>
        /// <param name="act">Executed when the result of the condition is true.</param>
        /// <returns>Input value to continue the chain.</returns>
        public static T If<T>(this T obj, Func<T, bool> condition, Action<T> act)
        {
            if(condition?.Invoke(obj) == true)
            {
                act?.Invoke(obj);
            }
            return obj;
        }

        private static BindingFlags DefaultFlags(bool nonPublic = false, bool isStatic = false)
        {
            var flags = BindingFlags.Public | ((isStatic) ? BindingFlags.Static : BindingFlags.Instance);
            if(nonPublic) {
                flags |= BindingFlags.NonPublic;
            }
            return flags;
        }

        private static object Exec(Func<Act> action, Object obj, string name)
        {
            if(obj == null || name == null) {
                throw new ArgumentNullException();
            }

            Act a = action();
            if(a.Key) {
                return a.Value;
            }

            throw new EntryPointNotFoundException(
                $"'{name}' was not found in '{obj.GetType().FullName}'. Check the access, type of member, and correct name."
            );
        }
    }
}
