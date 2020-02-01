/*
 * The MIT License (MIT)
 *
 * Copyright (c) 2016-2020  Denis Kuzmin < x-3F@outlook.com > GitHub/3F
 * Copyright (c) Conari contributors: https://github.com/3F/Conari/graphs/contributors
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

namespace net.r_eg.GenDelegates
{
    class Program
    {
        static void Main(string[] args)
        {
            /* Sample:
             * 
             ... 4:
                public delegate void MySpec1<T1, in T2, in T3, in T4>(out T1 p1, T2 p2, T3 p3, T4 p4);
                public delegate void MySpec2<in T1, T2, in T3, in T4>(T1 p1, out T2 p2, T3 p3, T4 p4);
                public delegate void MySpec3<in T1, in T2, T3, in T4>(T1 p1, T2 p2, out T3 p3, T4 p4);
                public delegate void MySpec4<in T1, in T2, in T3, T4>(T1 p1, T2 p2, T3 p3, out T4 p4);
                public delegate void MySpec1_2<T1, T2, in T3, in T4>(out T1 p1, out T2 p2, T3 p3, T4 p4);
                public delegate void MySpec1_3<T1, in T2, T3, in T4>(out T1 p1, T2 p2, out T3 p3, T4 p4);
                public delegate void MySpec1_4<T1, in T2, in T3, T4>(out T1 p1, T2 p2, T3 p3, out T4 p4);
                public delegate void MySpec2_3<in T1, T2, T3, in T4>(T1 p1, out T2 p2, out T3 p3, T4 p4);
                public delegate void MySpec2_4<in T1, T2, in T3, T4>(T1 p1, out T2 p2, T3 p3, out T4 p4);
                public delegate void MySpec3_4<in T1, in T2, T3, T4>(T1 p1, T2 p2, out T3 p3, out T4 p4);
                public delegate void MySpec1_2_3<T1, T2, T3, in T4>(out T1 p1, out T2 p2, out T3 p3, T4 p4);
                public delegate void MySpec1_2_4<T1, T2, in T3, T4>(out T1 p1, out T2 p2, T3 p3, out T4 p4);
                public delegate void MySpec1_3_4<T1, in T2, T3, T4>(out T1 p1, T2 p2, out T3 p3, out T4 p4);
                public delegate void MySpec2_3_4<in T1, T2, T3, T4>(T1 p1, out T2 p2, out T3 p3, out T4 p4);
                public delegate void MySpec<T1, T2, T3, T4>(out T1 p1, out T2 p2, out T3 p3, out T4 p4);                                
             ...
             */

            foreach(var s in Collection.getV1("MySpec", 4, Collection.Modifiers.Out, false)) {
                Console.WriteLine(s);
            }

            Console.ReadKey();
        }
    }
}
