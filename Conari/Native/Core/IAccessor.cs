/*!
 * Copyright (c) 2016  Denis Kuzmin <x-3F@outlook.com> github/3F
 * Copyright (c) Conari contributors https://github.com/3F/Conari/graphs/contributors
 * Licensed under the MIT License (MIT).
 * See accompanying LICENSE.txt file or visit https://github.com/3F/Conari
*/

using System;
using net.r_eg.Conari.Exceptions;
using net.r_eg.Conari.Types;

namespace net.r_eg.Conari.Native.Core
{
    public interface IAccessor: IPointer, IAccessorReader<IAccessor>, IAccessorWritter<IAccessor>, IAccessorUpdater<IAccessor>
    {
        /// <inheritdoc cref="Zone.D"/>
        IAccessor D { get; }

        /// <inheritdoc cref="Zone.U"/>
        IAccessor U { get; }

        /// <summary>
        /// Move to a new address using offset and specified region.
        /// </summary>
        /// <param name="offset">Offset for address. Can be negative.</param>
        /// <param name="region"></param>
        IAccessor move(long offset, Zone region = Zone.Current);

        /// <summary>
        /// Go to a new address using absolute position.
        /// </summary>
        /// <param name="addr"></param>
        IAccessor @goto(VPtr addr);

        /// <summary>
        /// Set encoding system for methods like <see cref="IAccessorReader{IAccessor}.readChar()"/>.
        /// </summary>
        IAccessor set(CharType enc);

        /// <inheritdoc cref="eq{T}(T)"/>
        IAccessor eq<T>(params T[] input) where T : struct;

        /// <summary>
        /// Verifies that input T is equal to T using <see cref="IAccessorReader{IAccessor}.read{T}()"/>.
        /// Use <see cref="check"/>, <see cref="ifTrue(Action{IAccessor})"/>,
        /// and  <see cref="ifFalse(Action{IAccessor})"/> to check the result.
        /// </summary>
        /// <typeparam name="T">Supported type for <see cref="IAccessorReader{IAccessor}.read{T}()"/></typeparam>
        /// <param name="input">To compare with <see cref="IAccessorReader{IAccessor}.read{T}()"/>.</param>
        IAccessor eq<T>(T input) where T : struct;

        /// <summary>
        /// Logical boolean for <see cref="eq{T}(T)"/> chain.
        /// </summary>
        IAccessor or();

        /// <summary>
        /// Check the result of <see cref="eq{T}(T)"/> and <see cref="or()"/> as boolean value.
        /// </summary>
        /// <returns>
        ///     True if all <see cref="eq{T}(T)"/> are true.
        ///     Or at least for one of the <see cref="or()"/> if it was applied.
        ///     Otherwise return false.
        /// </returns>
        bool check();

        /// <summary>
        /// Check the result as <see cref="check()"/> and invoke the action if it is true.
        /// </summary>
        /// <param name="act">The action to be executed if the result is true.</param>
        IAccessor ifTrue(Action<IAccessor> act);

        /// <summary>
        /// Check the result as <see cref="check()"/> and invoke the action if it is false.
        /// </summary>
        /// <param name="act">The action to be executed if the result is false.</param>
        IAccessor ifFalse(Action<IAccessor> act);

        /// <summary>
        /// Check the result as <see cref="check()"/> 
        /// and throw an <see cref="FailedCheckException"/> exception if it is equal to expected value. 
        /// </summary>
        /// <param name="when">Expected value to fail.</param>
        IAccessor failed(bool when);

        /// <summary>
        /// Rewind the chain to a specific region.
        /// </summary>
        /// <param name="region"></param>
        IAccessor rewind(Zone region = Zone.Region);

        /// <summary>
        /// Move back on T type size from the current position.
        /// </summary>
        /// <param name="count">How many times. Positive numbers only.</param>
        IAccessor back<T>(int count = 1);

        /// <summary>
        /// Move back on T1 and T2 type sizes from the current position.
        /// </summary>
        IAccessor back<T1, T2>();

        /// <summary>
        /// Move back on T1, T2, and T3 type sizes from the current position.
        /// </summary>
        IAccessor back<T1, T2, T3>();
    }
}
