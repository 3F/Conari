/*!
 * Copyright (c) 2016  Denis Kuzmin <x-3F@outlook.com> github/3F
 * Copyright (c) Conari contributors https://github.com/3F/Conari/graphs/contributors
 * Licensed under the MIT License (MIT).
 * See accompanying LICENSE.txt file or visit https://github.com/3F/Conari
*/

using net.r_eg.Conari.Types;

namespace net.r_eg.Conari.Native.Core
{
    public interface IPointer
    {
        /// <summary>
        /// Non-modifiable initial pointer to the container of the data.
        /// </summary>
        VPtr InitialPtr { get; }

        /// <summary>
        /// Active area in container. Must be initially equal to <see cref="InitialPtr"/>.
        /// </summary>
        VPtr RegionPtr { get; }

        /// <summary>
        /// Active pointer indicates current position inside specific region.
        /// Must be initially equal to <see cref="RegionPtr"/>.
        /// </summary>
        VPtr CurrentPtr { get; set; }

        /// <summary>
        /// Resets pointers to initial state (the beginning of the container).
        /// </summary>
        /// <returns><see cref="InitialPtr"/></returns>
        VPtr resetPtr();

        /// <summary>
        /// Shifts the beginning of the region at the current position <see cref="CurrentPtr"/>.
        /// </summary>
        /// <returns>Pointer to the beginning of the new region.</returns>
        VPtr shiftRegionPtr();

        /// <summary>
        /// Resets active pointer <see cref="CurrentPtr"/> to the beginning of the active region.
        /// </summary>
        /// <returns><see cref="RegionPtr"/></returns>
        VPtr resetRegionPtr();

        /// <summary>
        /// Get new pointer from offset using active pointer <see cref="CurrentPtr"/>.
        /// </summary>
        /// <param name="offset">Can be negative to decrement.</param>
        /// <returns>New calculated pointer based on offset to <see cref="CurrentPtr"/>.</returns>
        VPtr getPtrFrom(long offset);

        /// <summary>
        /// Get absolute address for offset using initial pointer <see cref="InitialPtr"/>.
        /// </summary>
        /// <param name="offset">Can be negative to decrement.</param>
        /// <returns>New calculated pointer based on offset to <see cref="InitialPtr"/>.</returns>
        VPtr getAddr(long offset);

        /// <summary>
        /// Increments active pointer <see cref="CurrentPtr"/> together with referenced pointer.
        /// </summary>
        /// <param name="ptr">Independent pointer to be incremented together with <see cref="CurrentPtr"/>.</param>
        /// <returns><see cref="CurrentPtr"/></returns>
        VPtr upPtr(ref VPtr ptr);

        /// <summary>
        /// Increments active pointer <see cref="CurrentPtr"/> using offset.
        /// </summary>
        /// <param name="offset">Can be negative to decrement.</param>
        /// <returns><see cref="CurrentPtr"/></returns>
        VPtr upPtr(long offset = 1);
    }
}
