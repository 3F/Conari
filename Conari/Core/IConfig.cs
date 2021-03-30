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

using System.Threading;

namespace net.r_eg.Conari.Core
{
    public interface IConfig
    {
        /// <summary>
        /// Module (.dll, .exe, or address).
        /// </summary>
        string Module { get; set; }

        /// <summary>
        /// To use `commit` methods for end calling.
        /// </summary>
        bool TransactionStrategy { get; set; }

        /// <summary>
        /// To load library only when required.
        /// </summary>
        bool LazyLoading { get; set; }

        /// <summary>
        /// To cache dynamic types.
        /// </summary>
        bool CacheDLR { get; set; }

        /// <summary>
        /// Auto name-decoration to find entry points of exported proc.
        /// </summary>
        bool Mangling { get; set; }

        /// <summary>
        /// https://github.com/3F/Conari/issues/15
        /// Windows will prevent new loading and return the same handle as for the first loaded module due to used reference count for each trying to load the same module (dll or exe).
        /// Actual new loading and its new handle is possible when reference count is less than 1.
        /// 
        /// Through Conari this means each decrementing when disposing is processed on implemented such as ConariL object.
        /// That is, each new instance will increase total reference count by +1 and each disposing will decrease it by -1.
        /// But it can produce the problem not only in multithreading but even between third processes.
        /// 
        /// This option will isolate module for a real new loading even if it was already loaded somewhere else.
        /// </summary>
        bool IsolateLoadingOfModule { get; set; }

        /// <summary>
        /// For not null value overrides the logic for how the module will be isolated according <see cref="IsolateLoadingOfModule"/> option.
        /// </summary>
        IModuleIsolationRecipe ModuleIsolationRecipe { get; set; }

        /// <summary>
        /// Cancel processing in loader if module can't be isolated.
        /// </summary>
        bool CancelIfCantIsolate { get; set; }

        /// <summary>
        /// Signals to cancel active operations as soon as possible.
        /// </summary>
        CancellationTokenSource Cts { get; set; }

        /// <summary>
        /// Limit in milliseconds for how long to wait signals when synchronization of threads (processes) in <see cref="Loader"/>.
        /// Use <see cref="Timeout.Infinite"/> (-1) to wait indefinitely.
        /// Use null for default value (<see cref="LLConfig.SIG_LIM"/>).
        /// </summary>
        int? LoaderSyncLimit { get; set; }
    }
}
