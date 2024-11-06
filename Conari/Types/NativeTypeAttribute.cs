/*!
 * Copyright (c) 2016  Denis Kuzmin <x-3F@outlook.com> github/3F
 * Copyright (c) Conari contributors https://github.com/3F/Conari/graphs/contributors
 * Licensed under the MIT License (MIT).
 * See accompanying LICENSE.txt file or visit https://github.com/3F/Conari
*/

using System;

namespace net.r_eg.Conari.Types
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Struct, AllowMultiple = false)]
    public sealed class NativeTypeAttribute: Attribute
    {
        /*
        
            Use this to change priority of selecting type for unmanaged code, for example:

            ```
            [NativeType] // to say: use this type 'as is'.
            public struct MyInteger
            {
                // without any [NativeType] should be used `int_t` type.
                // however, the `int_t` follows the same rules as and `MyInteger`, and it can be the final `int` type, etc.
                public static implicit operator int_t(MyInteger number) { }
    
                [NativeType] // to say: use `long` type for unmanaged code if should be used `MyInteger` type.
                public static implicit operator long(MyInteger number) { }

            ````

         */
    }
}
