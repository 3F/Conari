/*!
 * Copyright (c) 2016  Denis Kuzmin <x-3F@outlook.com> github/3F
 * Copyright (c) Conari contributors https://github.com/3F/Conari/graphs/contributors
 * Licensed under the MIT License (MIT).
 * See accompanying LICENSE.txt file or visit https://github.com/3F/Conari
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
