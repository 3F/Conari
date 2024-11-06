/*!
 * Copyright (c) 2016  Denis Kuzmin <x-3F@outlook.com> github/3F
 * Copyright (c) Conari contributors https://github.com/3F/Conari/graphs/contributors
 * Licensed under the MIT License (MIT).
 * See accompanying LICENSE.txt file or visit https://github.com/3F/Conari
*/

namespace net.r_eg.Conari.Types.Func.Ref
{
    /*
         One group should be: C1 + C2 + .. Cn
                              Where C = n! / (m! * (n - m)!)
                                                                */

    public delegate TRes FuncRef<T1, out TRes>(ref T1 p1);


    /* 2 param-group */

    public delegate TRes FuncRef1<T1, in T2, out TRes>(ref T1 p1, T2 p2);

    public delegate TRes FuncRef2<in T1, T2, out TRes>(T1 p1, ref T2 p2);

    public delegate TRes FuncRef<T1, T2, out TRes>(ref T1 p1, ref T2 p2);


/* 3 param-group */
#if F_PREDEFINED_FUNC_P3

    public delegate TRes FuncRef1<T1, in T2, in T3, out TRes>(ref T1 p1, T2 p2, T3 p3);

    public delegate TRes FuncRef2<in T1, T2, in T3, out TRes>(T1 p1, ref T2 p2, T3 p3);

    public delegate TRes FuncRef3<in T1, in T2, T3, out TRes>(T1 p1, T2 p2, ref T3 p3);

    public delegate TRes FuncRef1_2<T1, T2, in T3, out TRes>(ref T1 p1, ref T2 p2, T3 p3);

    public delegate TRes FuncRef1_3<T1, in T2, T3, out TRes>(ref T1 p1, T2 p2, ref T3 p3);

    public delegate TRes FuncRef2_3<in T1, T2, T3, out TRes>(T1 p1, ref T2 p2, ref T3 p3);

    public delegate TRes FuncRef<T1, T2, T3, out TRes>(ref T1 p1, ref T2 p2, ref T3 p3);

#endif

/* 4 param-group */
#if F_PREDEFINED_FUNC_P4

    public delegate TRes FuncRef1<T1, in T2, in T3, in T4, out TRes>(ref T1 p1, T2 p2, T3 p3, T4 p4);

    public delegate TRes FuncRef2<in T1, T2, in T3, in T4, out TRes>(T1 p1, ref T2 p2, T3 p3, T4 p4);

    public delegate TRes FuncRef3<in T1, in T2, T3, in T4, out TRes>(T1 p1, T2 p2, ref T3 p3, T4 p4);

    public delegate TRes FuncRef4<in T1, in T2, in T3, T4, out TRes>(T1 p1, T2 p2, T3 p3, ref T4 p4);

    public delegate TRes FuncRef1_2<T1, T2, in T3, in T4, out TRes>(ref T1 p1, ref T2 p2, T3 p3, T4 p4);

    public delegate TRes FuncRef1_3<T1, in T2, T3, in T4, out TRes>(ref T1 p1, T2 p2, ref T3 p3, T4 p4);

    public delegate TRes FuncRef1_4<T1, in T2, in T3, T4, out TRes>(ref T1 p1, T2 p2, T3 p3, ref T4 p4);

    public delegate TRes FuncRef2_3<in T1, T2, T3, in T4, out TRes>(T1 p1, ref T2 p2, ref T3 p3, T4 p4);

    public delegate TRes FuncRef2_4<in T1, T2, in T3, T4, out TRes>(T1 p1, ref T2 p2, T3 p3, ref T4 p4);

    public delegate TRes FuncRef3_4<in T1, in T2, T3, T4, out TRes>(T1 p1, T2 p2, ref T3 p3, ref T4 p4);

    public delegate TRes FuncRef1_2_3<T1, T2, T3, in T4, out TRes>(ref T1 p1, ref T2 p2, ref T3 p3, T4 p4);

    public delegate TRes FuncRef1_2_4<T1, T2, in T3, T4, out TRes>(ref T1 p1, ref T2 p2, T3 p3, ref T4 p4);

    public delegate TRes FuncRef1_3_4<T1, in T2, T3, T4, out TRes>(ref T1 p1, T2 p2, ref T3 p3, ref T4 p4);

    public delegate TRes FuncRef2_3_4<in T1, T2, T3, T4, out TRes>(T1 p1, ref T2 p2, ref T3 p3, ref T4 p4);

    public delegate TRes FuncRef<T1, T2, T3, T4, out TRes>(ref T1 p1, ref T2 p2, ref T3 p3, ref T4 p4);

#endif

/* 5 param-group */
#if F_PREDEFINED_FUNC_P5

    public delegate TRes FuncRef1<T1, in T2, in T3, in T4, in T5, out TRes>(ref T1 p1, T2 p2, T3 p3, T4 p4, T5 p5);

    public delegate TRes FuncRef2<in T1, T2, in T3, in T4, in T5, out TRes>(T1 p1, ref T2 p2, T3 p3, T4 p4, T5 p5);

    public delegate TRes FuncRef3<in T1, in T2, T3, in T4, in T5, out TRes>(T1 p1, T2 p2, ref T3 p3, T4 p4, T5 p5);

    public delegate TRes FuncRef4<in T1, in T2, in T3, T4, in T5, out TRes>(T1 p1, T2 p2, T3 p3, ref T4 p4, T5 p5);

    public delegate TRes FuncRef5<in T1, in T2, in T3, in T4, T5, out TRes>(T1 p1, T2 p2, T3 p3, T4 p4, ref T5 p5);

    public delegate TRes FuncRef1_2<T1, T2, in T3, in T4, in T5, out TRes>(ref T1 p1, ref T2 p2, T3 p3, T4 p4, T5 p5);

    public delegate TRes FuncRef1_3<T1, in T2, T3, in T4, in T5, out TRes>(ref T1 p1, T2 p2, ref T3 p3, T4 p4, T5 p5);

    public delegate TRes FuncRef1_4<T1, in T2, in T3, T4, in T5, out TRes>(ref T1 p1, T2 p2, T3 p3, ref T4 p4, T5 p5);

    public delegate TRes FuncRef1_5<T1, in T2, in T3, in T4, T5, out TRes>(ref T1 p1, T2 p2, T3 p3, T4 p4, ref T5 p5);

    public delegate TRes FuncRef2_3<in T1, T2, T3, in T4, in T5, out TRes>(T1 p1, ref T2 p2, ref T3 p3, T4 p4, T5 p5);

    public delegate TRes FuncRef2_4<in T1, T2, in T3, T4, in T5, out TRes>(T1 p1, ref T2 p2, T3 p3, ref T4 p4, T5 p5);

    public delegate TRes FuncRef2_5<in T1, T2, in T3, in T4, T5, out TRes>(T1 p1, ref T2 p2, T3 p3, T4 p4, ref T5 p5);

    public delegate TRes FuncRef3_4<in T1, in T2, T3, T4, in T5, out TRes>(T1 p1, T2 p2, ref T3 p3, ref T4 p4, T5 p5);

    public delegate TRes FuncRef3_5<in T1, in T2, T3, in T4, T5, out TRes>(T1 p1, T2 p2, ref T3 p3, T4 p4, ref T5 p5);

    public delegate TRes FuncRef4_5<in T1, in T2, in T3, T4, T5, out TRes>(T1 p1, T2 p2, T3 p3, ref T4 p4, ref T5 p5);

    public delegate TRes FuncRef1_2_3<T1, T2, T3, in T4, in T5, out TRes>(ref T1 p1, ref T2 p2, ref T3 p3, T4 p4, T5 p5);

    public delegate TRes FuncRef1_2_4<T1, T2, in T3, T4, in T5, out TRes>(ref T1 p1, ref T2 p2, T3 p3, ref T4 p4, T5 p5);

    public delegate TRes FuncRef1_2_5<T1, T2, in T3, in T4, T5, out TRes>(ref T1 p1, ref T2 p2, T3 p3, T4 p4, ref T5 p5);

    public delegate TRes FuncRef1_3_4<T1, in T2, T3, T4, in T5, out TRes>(ref T1 p1, T2 p2, ref T3 p3, ref T4 p4, T5 p5);

    public delegate TRes FuncRef1_3_5<T1, in T2, T3, in T4, T5, out TRes>(ref T1 p1, T2 p2, ref T3 p3, T4 p4, ref T5 p5);

    public delegate TRes FuncRef1_4_5<T1, in T2, in T3, T4, T5, out TRes>(ref T1 p1, T2 p2, T3 p3, ref T4 p4, ref T5 p5);

    public delegate TRes FuncRef2_3_4<in T1, T2, T3, T4, in T5, out TRes>(T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, T5 p5);

    public delegate TRes FuncRef2_3_5<in T1, T2, T3, in T4, T5, out TRes>(T1 p1, ref T2 p2, ref T3 p3, T4 p4, ref T5 p5);

    public delegate TRes FuncRef2_4_5<in T1, T2, in T3, T4, T5, out TRes>(T1 p1, ref T2 p2, T3 p3, ref T4 p4, ref T5 p5);

    public delegate TRes FuncRef3_4_5<in T1, in T2, T3, T4, T5, out TRes>(T1 p1, T2 p2, ref T3 p3, ref T4 p4, ref T5 p5);

    public delegate TRes FuncRef1_2_3_4<T1, T2, T3, T4, in T5, out TRes>(ref T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, T5 p5);

    public delegate TRes FuncRef1_2_3_5<T1, T2, T3, in T4, T5, out TRes>(ref T1 p1, ref T2 p2, ref T3 p3, T4 p4, ref T5 p5);

    public delegate TRes FuncRef1_2_4_5<T1, T2, in T3, T4, T5, out TRes>(ref T1 p1, ref T2 p2, T3 p3, ref T4 p4, ref T5 p5);

    public delegate TRes FuncRef1_3_4_5<T1, in T2, T3, T4, T5, out TRes>(ref T1 p1, T2 p2, ref T3 p3, ref T4 p4, ref T5 p5);

    public delegate TRes FuncRef2_3_4_5<in T1, T2, T3, T4, T5, out TRes>(T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, ref T5 p5);

    public delegate TRes FuncRef<T1, T2, T3, T4, T5, out TRes>(ref T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, ref T5 p5);

#endif

/* 6 param-group */
#if F_PREDEFINED_FUNC_P6

    public delegate TRes FuncRef1<T1, in T2, in T3, in T4, in T5, in T6, out TRes>(ref T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6);

    public delegate TRes FuncRef2<in T1, T2, in T3, in T4, in T5, in T6, out TRes>(T1 p1, ref T2 p2, T3 p3, T4 p4, T5 p5, T6 p6);

    public delegate TRes FuncRef3<in T1, in T2, T3, in T4, in T5, in T6, out TRes>(T1 p1, T2 p2, ref T3 p3, T4 p4, T5 p5, T6 p6);

    public delegate TRes FuncRef4<in T1, in T2, in T3, T4, in T5, in T6, out TRes>(T1 p1, T2 p2, T3 p3, ref T4 p4, T5 p5, T6 p6);

    public delegate TRes FuncRef5<in T1, in T2, in T3, in T4, T5, in T6, out TRes>(T1 p1, T2 p2, T3 p3, T4 p4, ref T5 p5, T6 p6);

    public delegate TRes FuncRef6<in T1, in T2, in T3, in T4, in T5, T6, out TRes>(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, ref T6 p6);

    public delegate TRes FuncRef1_2<T1, T2, in T3, in T4, in T5, in T6, out TRes>(ref T1 p1, ref T2 p2, T3 p3, T4 p4, T5 p5, T6 p6);

    public delegate TRes FuncRef1_3<T1, in T2, T3, in T4, in T5, in T6, out TRes>(ref T1 p1, T2 p2, ref T3 p3, T4 p4, T5 p5, T6 p6);

    public delegate TRes FuncRef1_4<T1, in T2, in T3, T4, in T5, in T6, out TRes>(ref T1 p1, T2 p2, T3 p3, ref T4 p4, T5 p5, T6 p6);

    public delegate TRes FuncRef1_5<T1, in T2, in T3, in T4, T5, in T6, out TRes>(ref T1 p1, T2 p2, T3 p3, T4 p4, ref T5 p5, T6 p6);

    public delegate TRes FuncRef1_6<T1, in T2, in T3, in T4, in T5, T6, out TRes>(ref T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, ref T6 p6);

    public delegate TRes FuncRef2_3<in T1, T2, T3, in T4, in T5, in T6, out TRes>(T1 p1, ref T2 p2, ref T3 p3, T4 p4, T5 p5, T6 p6);

    public delegate TRes FuncRef2_4<in T1, T2, in T3, T4, in T5, in T6, out TRes>(T1 p1, ref T2 p2, T3 p3, ref T4 p4, T5 p5, T6 p6);

    public delegate TRes FuncRef2_5<in T1, T2, in T3, in T4, T5, in T6, out TRes>(T1 p1, ref T2 p2, T3 p3, T4 p4, ref T5 p5, T6 p6);

    public delegate TRes FuncRef2_6<in T1, T2, in T3, in T4, in T5, T6, out TRes>(T1 p1, ref T2 p2, T3 p3, T4 p4, T5 p5, ref T6 p6);

    public delegate TRes FuncRef3_4<in T1, in T2, T3, T4, in T5, in T6, out TRes>(T1 p1, T2 p2, ref T3 p3, ref T4 p4, T5 p5, T6 p6);

    public delegate TRes FuncRef3_5<in T1, in T2, T3, in T4, T5, in T6, out TRes>(T1 p1, T2 p2, ref T3 p3, T4 p4, ref T5 p5, T6 p6);

    public delegate TRes FuncRef3_6<in T1, in T2, T3, in T4, in T5, T6, out TRes>(T1 p1, T2 p2, ref T3 p3, T4 p4, T5 p5, ref T6 p6);

    public delegate TRes FuncRef4_5<in T1, in T2, in T3, T4, T5, in T6, out TRes>(T1 p1, T2 p2, T3 p3, ref T4 p4, ref T5 p5, T6 p6);

    public delegate TRes FuncRef4_6<in T1, in T2, in T3, T4, in T5, T6, out TRes>(T1 p1, T2 p2, T3 p3, ref T4 p4, T5 p5, ref T6 p6);

    public delegate TRes FuncRef5_6<in T1, in T2, in T3, in T4, T5, T6, out TRes>(T1 p1, T2 p2, T3 p3, T4 p4, ref T5 p5, ref T6 p6);

    public delegate TRes FuncRef1_2_3<T1, T2, T3, in T4, in T5, in T6, out TRes>(ref T1 p1, ref T2 p2, ref T3 p3, T4 p4, T5 p5, T6 p6);

    public delegate TRes FuncRef1_2_4<T1, T2, in T3, T4, in T5, in T6, out TRes>(ref T1 p1, ref T2 p2, T3 p3, ref T4 p4, T5 p5, T6 p6);

    public delegate TRes FuncRef1_2_5<T1, T2, in T3, in T4, T5, in T6, out TRes>(ref T1 p1, ref T2 p2, T3 p3, T4 p4, ref T5 p5, T6 p6);

    public delegate TRes FuncRef1_2_6<T1, T2, in T3, in T4, in T5, T6, out TRes>(ref T1 p1, ref T2 p2, T3 p3, T4 p4, T5 p5, ref T6 p6);

    public delegate TRes FuncRef1_3_4<T1, in T2, T3, T4, in T5, in T6, out TRes>(ref T1 p1, T2 p2, ref T3 p3, ref T4 p4, T5 p5, T6 p6);

    public delegate TRes FuncRef1_3_5<T1, in T2, T3, in T4, T5, in T6, out TRes>(ref T1 p1, T2 p2, ref T3 p3, T4 p4, ref T5 p5, T6 p6);

    public delegate TRes FuncRef1_3_6<T1, in T2, T3, in T4, in T5, T6, out TRes>(ref T1 p1, T2 p2, ref T3 p3, T4 p4, T5 p5, ref T6 p6);

    public delegate TRes FuncRef1_4_5<T1, in T2, in T3, T4, T5, in T6, out TRes>(ref T1 p1, T2 p2, T3 p3, ref T4 p4, ref T5 p5, T6 p6);

    public delegate TRes FuncRef1_4_6<T1, in T2, in T3, T4, in T5, T6, out TRes>(ref T1 p1, T2 p2, T3 p3, ref T4 p4, T5 p5, ref T6 p6);

    public delegate TRes FuncRef1_5_6<T1, in T2, in T3, in T4, T5, T6, out TRes>(ref T1 p1, T2 p2, T3 p3, T4 p4, ref T5 p5, ref T6 p6);

    public delegate TRes FuncRef2_3_4<in T1, T2, T3, T4, in T5, in T6, out TRes>(T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, T5 p5, T6 p6);

    public delegate TRes FuncRef2_3_5<in T1, T2, T3, in T4, T5, in T6, out TRes>(T1 p1, ref T2 p2, ref T3 p3, T4 p4, ref T5 p5, T6 p6);

    public delegate TRes FuncRef2_3_6<in T1, T2, T3, in T4, in T5, T6, out TRes>(T1 p1, ref T2 p2, ref T3 p3, T4 p4, T5 p5, ref T6 p6);

    public delegate TRes FuncRef2_4_5<in T1, T2, in T3, T4, T5, in T6, out TRes>(T1 p1, ref T2 p2, T3 p3, ref T4 p4, ref T5 p5, T6 p6);

    public delegate TRes FuncRef2_4_6<in T1, T2, in T3, T4, in T5, T6, out TRes>(T1 p1, ref T2 p2, T3 p3, ref T4 p4, T5 p5, ref T6 p6);

    public delegate TRes FuncRef2_5_6<in T1, T2, in T3, in T4, T5, T6, out TRes>(T1 p1, ref T2 p2, T3 p3, T4 p4, ref T5 p5, ref T6 p6);

    public delegate TRes FuncRef3_4_5<in T1, in T2, T3, T4, T5, in T6, out TRes>(T1 p1, T2 p2, ref T3 p3, ref T4 p4, ref T5 p5, T6 p6);

    public delegate TRes FuncRef3_4_6<in T1, in T2, T3, T4, in T5, T6, out TRes>(T1 p1, T2 p2, ref T3 p3, ref T4 p4, T5 p5, ref T6 p6);

    public delegate TRes FuncRef3_5_6<in T1, in T2, T3, in T4, T5, T6, out TRes>(T1 p1, T2 p2, ref T3 p3, T4 p4, ref T5 p5, ref T6 p6);

    public delegate TRes FuncRef4_5_6<in T1, in T2, in T3, T4, T5, T6, out TRes>(T1 p1, T2 p2, T3 p3, ref T4 p4, ref T5 p5, ref T6 p6);

    public delegate TRes FuncRef1_2_3_4<T1, T2, T3, T4, in T5, in T6, out TRes>(ref T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, T5 p5, T6 p6);

    public delegate TRes FuncRef1_2_3_5<T1, T2, T3, in T4, T5, in T6, out TRes>(ref T1 p1, ref T2 p2, ref T3 p3, T4 p4, ref T5 p5, T6 p6);

    public delegate TRes FuncRef1_2_3_6<T1, T2, T3, in T4, in T5, T6, out TRes>(ref T1 p1, ref T2 p2, ref T3 p3, T4 p4, T5 p5, ref T6 p6);

    public delegate TRes FuncRef1_2_4_5<T1, T2, in T3, T4, T5, in T6, out TRes>(ref T1 p1, ref T2 p2, T3 p3, ref T4 p4, ref T5 p5, T6 p6);

    public delegate TRes FuncRef1_2_4_6<T1, T2, in T3, T4, in T5, T6, out TRes>(ref T1 p1, ref T2 p2, T3 p3, ref T4 p4, T5 p5, ref T6 p6);

    public delegate TRes FuncRef1_2_5_6<T1, T2, in T3, in T4, T5, T6, out TRes>(ref T1 p1, ref T2 p2, T3 p3, T4 p4, ref T5 p5, ref T6 p6);

    public delegate TRes FuncRef1_3_4_5<T1, in T2, T3, T4, T5, in T6, out TRes>(ref T1 p1, T2 p2, ref T3 p3, ref T4 p4, ref T5 p5, T6 p6);

    public delegate TRes FuncRef1_3_4_6<T1, in T2, T3, T4, in T5, T6, out TRes>(ref T1 p1, T2 p2, ref T3 p3, ref T4 p4, T5 p5, ref T6 p6);

    public delegate TRes FuncRef1_3_5_6<T1, in T2, T3, in T4, T5, T6, out TRes>(ref T1 p1, T2 p2, ref T3 p3, T4 p4, ref T5 p5, ref T6 p6);

    public delegate TRes FuncRef1_4_5_6<T1, in T2, in T3, T4, T5, T6, out TRes>(ref T1 p1, T2 p2, T3 p3, ref T4 p4, ref T5 p5, ref T6 p6);

    public delegate TRes FuncRef2_3_4_5<in T1, T2, T3, T4, T5, in T6, out TRes>(T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, ref T5 p5, T6 p6);

    public delegate TRes FuncRef2_3_4_6<in T1, T2, T3, T4, in T5, T6, out TRes>(T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, T5 p5, ref T6 p6);

    public delegate TRes FuncRef2_3_5_6<in T1, T2, T3, in T4, T5, T6, out TRes>(T1 p1, ref T2 p2, ref T3 p3, T4 p4, ref T5 p5, ref T6 p6);

    public delegate TRes FuncRef2_4_5_6<in T1, T2, in T3, T4, T5, T6, out TRes>(T1 p1, ref T2 p2, T3 p3, ref T4 p4, ref T5 p5, ref T6 p6);

    public delegate TRes FuncRef3_4_5_6<in T1, in T2, T3, T4, T5, T6, out TRes>(T1 p1, T2 p2, ref T3 p3, ref T4 p4, ref T5 p5, ref T6 p6);

    public delegate TRes FuncRef1_2_3_4_5<T1, T2, T3, T4, T5, in T6, out TRes>(ref T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, ref T5 p5, T6 p6);

    public delegate TRes FuncRef1_2_3_4_6<T1, T2, T3, T4, in T5, T6, out TRes>(ref T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, T5 p5, ref T6 p6);

    public delegate TRes FuncRef1_2_3_5_6<T1, T2, T3, in T4, T5, T6, out TRes>(ref T1 p1, ref T2 p2, ref T3 p3, T4 p4, ref T5 p5, ref T6 p6);

    public delegate TRes FuncRef1_2_4_5_6<T1, T2, in T3, T4, T5, T6, out TRes>(ref T1 p1, ref T2 p2, T3 p3, ref T4 p4, ref T5 p5, ref T6 p6);

    public delegate TRes FuncRef1_3_4_5_6<T1, in T2, T3, T4, T5, T6, out TRes>(ref T1 p1, T2 p2, ref T3 p3, ref T4 p4, ref T5 p5, ref T6 p6);

    public delegate TRes FuncRef2_3_4_5_6<in T1, T2, T3, T4, T5, T6, out TRes>(T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, ref T5 p5, ref T6 p6);

    public delegate TRes FuncRef<T1, T2, T3, T4, T5, T6, out TRes>(ref T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, ref T5 p5, ref T6 p6);

#endif

/* 7 param-group */
#if F_PREDEFINED_FUNC_P7

    public delegate TRes FuncRef1<T1, in T2, in T3, in T4, in T5, in T6, in T7, out TRes>(ref T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7);

    public delegate TRes FuncRef2<in T1, T2, in T3, in T4, in T5, in T6, in T7, out TRes>(T1 p1, ref T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7);

    public delegate TRes FuncRef3<in T1, in T2, T3, in T4, in T5, in T6, in T7, out TRes>(T1 p1, T2 p2, ref T3 p3, T4 p4, T5 p5, T6 p6, T7 p7);

    public delegate TRes FuncRef4<in T1, in T2, in T3, T4, in T5, in T6, in T7, out TRes>(T1 p1, T2 p2, T3 p3, ref T4 p4, T5 p5, T6 p6, T7 p7);

    public delegate TRes FuncRef5<in T1, in T2, in T3, in T4, T5, in T6, in T7, out TRes>(T1 p1, T2 p2, T3 p3, T4 p4, ref T5 p5, T6 p6, T7 p7);

    public delegate TRes FuncRef6<in T1, in T2, in T3, in T4, in T5, T6, in T7, out TRes>(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, ref T6 p6, T7 p7);

    public delegate TRes FuncRef7<in T1, in T2, in T3, in T4, in T5, in T6, T7, out TRes>(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, ref T7 p7);

    public delegate TRes FuncRef1_2<T1, T2, in T3, in T4, in T5, in T6, in T7, out TRes>(ref T1 p1, ref T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7);

    public delegate TRes FuncRef1_3<T1, in T2, T3, in T4, in T5, in T6, in T7, out TRes>(ref T1 p1, T2 p2, ref T3 p3, T4 p4, T5 p5, T6 p6, T7 p7);

    public delegate TRes FuncRef1_4<T1, in T2, in T3, T4, in T5, in T6, in T7, out TRes>(ref T1 p1, T2 p2, T3 p3, ref T4 p4, T5 p5, T6 p6, T7 p7);

    public delegate TRes FuncRef1_5<T1, in T2, in T3, in T4, T5, in T6, in T7, out TRes>(ref T1 p1, T2 p2, T3 p3, T4 p4, ref T5 p5, T6 p6, T7 p7);

    public delegate TRes FuncRef1_6<T1, in T2, in T3, in T4, in T5, T6, in T7, out TRes>(ref T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, ref T6 p6, T7 p7);

    public delegate TRes FuncRef1_7<T1, in T2, in T3, in T4, in T5, in T6, T7, out TRes>(ref T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, ref T7 p7);

    public delegate TRes FuncRef2_3<in T1, T2, T3, in T4, in T5, in T6, in T7, out TRes>(T1 p1, ref T2 p2, ref T3 p3, T4 p4, T5 p5, T6 p6, T7 p7);

    public delegate TRes FuncRef2_4<in T1, T2, in T3, T4, in T5, in T6, in T7, out TRes>(T1 p1, ref T2 p2, T3 p3, ref T4 p4, T5 p5, T6 p6, T7 p7);

    public delegate TRes FuncRef2_5<in T1, T2, in T3, in T4, T5, in T6, in T7, out TRes>(T1 p1, ref T2 p2, T3 p3, T4 p4, ref T5 p5, T6 p6, T7 p7);

    public delegate TRes FuncRef2_6<in T1, T2, in T3, in T4, in T5, T6, in T7, out TRes>(T1 p1, ref T2 p2, T3 p3, T4 p4, T5 p5, ref T6 p6, T7 p7);

    public delegate TRes FuncRef2_7<in T1, T2, in T3, in T4, in T5, in T6, T7, out TRes>(T1 p1, ref T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, ref T7 p7);

    public delegate TRes FuncRef3_4<in T1, in T2, T3, T4, in T5, in T6, in T7, out TRes>(T1 p1, T2 p2, ref T3 p3, ref T4 p4, T5 p5, T6 p6, T7 p7);

    public delegate TRes FuncRef3_5<in T1, in T2, T3, in T4, T5, in T6, in T7, out TRes>(T1 p1, T2 p2, ref T3 p3, T4 p4, ref T5 p5, T6 p6, T7 p7);

    public delegate TRes FuncRef3_6<in T1, in T2, T3, in T4, in T5, T6, in T7, out TRes>(T1 p1, T2 p2, ref T3 p3, T4 p4, T5 p5, ref T6 p6, T7 p7);

    public delegate TRes FuncRef3_7<in T1, in T2, T3, in T4, in T5, in T6, T7, out TRes>(T1 p1, T2 p2, ref T3 p3, T4 p4, T5 p5, T6 p6, ref T7 p7);

    public delegate TRes FuncRef4_5<in T1, in T2, in T3, T4, T5, in T6, in T7, out TRes>(T1 p1, T2 p2, T3 p3, ref T4 p4, ref T5 p5, T6 p6, T7 p7);

    public delegate TRes FuncRef4_6<in T1, in T2, in T3, T4, in T5, T6, in T7, out TRes>(T1 p1, T2 p2, T3 p3, ref T4 p4, T5 p5, ref T6 p6, T7 p7);

    public delegate TRes FuncRef4_7<in T1, in T2, in T3, T4, in T5, in T6, T7, out TRes>(T1 p1, T2 p2, T3 p3, ref T4 p4, T5 p5, T6 p6, ref T7 p7);

    public delegate TRes FuncRef5_6<in T1, in T2, in T3, in T4, T5, T6, in T7, out TRes>(T1 p1, T2 p2, T3 p3, T4 p4, ref T5 p5, ref T6 p6, T7 p7);

    public delegate TRes FuncRef5_7<in T1, in T2, in T3, in T4, T5, in T6, T7, out TRes>(T1 p1, T2 p2, T3 p3, T4 p4, ref T5 p5, T6 p6, ref T7 p7);

    public delegate TRes FuncRef6_7<in T1, in T2, in T3, in T4, in T5, T6, T7, out TRes>(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, ref T6 p6, ref T7 p7);

    public delegate TRes FuncRef1_2_3<T1, T2, T3, in T4, in T5, in T6, in T7, out TRes>(ref T1 p1, ref T2 p2, ref T3 p3, T4 p4, T5 p5, T6 p6, T7 p7);

    public delegate TRes FuncRef1_2_4<T1, T2, in T3, T4, in T5, in T6, in T7, out TRes>(ref T1 p1, ref T2 p2, T3 p3, ref T4 p4, T5 p5, T6 p6, T7 p7);

    public delegate TRes FuncRef1_2_5<T1, T2, in T3, in T4, T5, in T6, in T7, out TRes>(ref T1 p1, ref T2 p2, T3 p3, T4 p4, ref T5 p5, T6 p6, T7 p7);

    public delegate TRes FuncRef1_2_6<T1, T2, in T3, in T4, in T5, T6, in T7, out TRes>(ref T1 p1, ref T2 p2, T3 p3, T4 p4, T5 p5, ref T6 p6, T7 p7);

    public delegate TRes FuncRef1_2_7<T1, T2, in T3, in T4, in T5, in T6, T7, out TRes>(ref T1 p1, ref T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, ref T7 p7);

    public delegate TRes FuncRef1_3_4<T1, in T2, T3, T4, in T5, in T6, in T7, out TRes>(ref T1 p1, T2 p2, ref T3 p3, ref T4 p4, T5 p5, T6 p6, T7 p7);

    public delegate TRes FuncRef1_3_5<T1, in T2, T3, in T4, T5, in T6, in T7, out TRes>(ref T1 p1, T2 p2, ref T3 p3, T4 p4, ref T5 p5, T6 p6, T7 p7);

    public delegate TRes FuncRef1_3_6<T1, in T2, T3, in T4, in T5, T6, in T7, out TRes>(ref T1 p1, T2 p2, ref T3 p3, T4 p4, T5 p5, ref T6 p6, T7 p7);

    public delegate TRes FuncRef1_3_7<T1, in T2, T3, in T4, in T5, in T6, T7, out TRes>(ref T1 p1, T2 p2, ref T3 p3, T4 p4, T5 p5, T6 p6, ref T7 p7);

    public delegate TRes FuncRef1_4_5<T1, in T2, in T3, T4, T5, in T6, in T7, out TRes>(ref T1 p1, T2 p2, T3 p3, ref T4 p4, ref T5 p5, T6 p6, T7 p7);

    public delegate TRes FuncRef1_4_6<T1, in T2, in T3, T4, in T5, T6, in T7, out TRes>(ref T1 p1, T2 p2, T3 p3, ref T4 p4, T5 p5, ref T6 p6, T7 p7);

    public delegate TRes FuncRef1_4_7<T1, in T2, in T3, T4, in T5, in T6, T7, out TRes>(ref T1 p1, T2 p2, T3 p3, ref T4 p4, T5 p5, T6 p6, ref T7 p7);

    public delegate TRes FuncRef1_5_6<T1, in T2, in T3, in T4, T5, T6, in T7, out TRes>(ref T1 p1, T2 p2, T3 p3, T4 p4, ref T5 p5, ref T6 p6, T7 p7);

    public delegate TRes FuncRef1_5_7<T1, in T2, in T3, in T4, T5, in T6, T7, out TRes>(ref T1 p1, T2 p2, T3 p3, T4 p4, ref T5 p5, T6 p6, ref T7 p7);

    public delegate TRes FuncRef1_6_7<T1, in T2, in T3, in T4, in T5, T6, T7, out TRes>(ref T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, ref T6 p6, ref T7 p7);

    public delegate TRes FuncRef2_3_4<in T1, T2, T3, T4, in T5, in T6, in T7, out TRes>(T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, T5 p5, T6 p6, T7 p7);

    public delegate TRes FuncRef2_3_5<in T1, T2, T3, in T4, T5, in T6, in T7, out TRes>(T1 p1, ref T2 p2, ref T3 p3, T4 p4, ref T5 p5, T6 p6, T7 p7);

    public delegate TRes FuncRef2_3_6<in T1, T2, T3, in T4, in T5, T6, in T7, out TRes>(T1 p1, ref T2 p2, ref T3 p3, T4 p4, T5 p5, ref T6 p6, T7 p7);

    public delegate TRes FuncRef2_3_7<in T1, T2, T3, in T4, in T5, in T6, T7, out TRes>(T1 p1, ref T2 p2, ref T3 p3, T4 p4, T5 p5, T6 p6, ref T7 p7);

    public delegate TRes FuncRef2_4_5<in T1, T2, in T3, T4, T5, in T6, in T7, out TRes>(T1 p1, ref T2 p2, T3 p3, ref T4 p4, ref T5 p5, T6 p6, T7 p7);

    public delegate TRes FuncRef2_4_6<in T1, T2, in T3, T4, in T5, T6, in T7, out TRes>(T1 p1, ref T2 p2, T3 p3, ref T4 p4, T5 p5, ref T6 p6, T7 p7);

    public delegate TRes FuncRef2_4_7<in T1, T2, in T3, T4, in T5, in T6, T7, out TRes>(T1 p1, ref T2 p2, T3 p3, ref T4 p4, T5 p5, T6 p6, ref T7 p7);

    public delegate TRes FuncRef2_5_6<in T1, T2, in T3, in T4, T5, T6, in T7, out TRes>(T1 p1, ref T2 p2, T3 p3, T4 p4, ref T5 p5, ref T6 p6, T7 p7);

    public delegate TRes FuncRef2_5_7<in T1, T2, in T3, in T4, T5, in T6, T7, out TRes>(T1 p1, ref T2 p2, T3 p3, T4 p4, ref T5 p5, T6 p6, ref T7 p7);

    public delegate TRes FuncRef2_6_7<in T1, T2, in T3, in T4, in T5, T6, T7, out TRes>(T1 p1, ref T2 p2, T3 p3, T4 p4, T5 p5, ref T6 p6, ref T7 p7);

    public delegate TRes FuncRef3_4_5<in T1, in T2, T3, T4, T5, in T6, in T7, out TRes>(T1 p1, T2 p2, ref T3 p3, ref T4 p4, ref T5 p5, T6 p6, T7 p7);

    public delegate TRes FuncRef3_4_6<in T1, in T2, T3, T4, in T5, T6, in T7, out TRes>(T1 p1, T2 p2, ref T3 p3, ref T4 p4, T5 p5, ref T6 p6, T7 p7);

    public delegate TRes FuncRef3_4_7<in T1, in T2, T3, T4, in T5, in T6, T7, out TRes>(T1 p1, T2 p2, ref T3 p3, ref T4 p4, T5 p5, T6 p6, ref T7 p7);

    public delegate TRes FuncRef3_5_6<in T1, in T2, T3, in T4, T5, T6, in T7, out TRes>(T1 p1, T2 p2, ref T3 p3, T4 p4, ref T5 p5, ref T6 p6, T7 p7);

    public delegate TRes FuncRef3_5_7<in T1, in T2, T3, in T4, T5, in T6, T7, out TRes>(T1 p1, T2 p2, ref T3 p3, T4 p4, ref T5 p5, T6 p6, ref T7 p7);

    public delegate TRes FuncRef3_6_7<in T1, in T2, T3, in T4, in T5, T6, T7, out TRes>(T1 p1, T2 p2, ref T3 p3, T4 p4, T5 p5, ref T6 p6, ref T7 p7);

    public delegate TRes FuncRef4_5_6<in T1, in T2, in T3, T4, T5, T6, in T7, out TRes>(T1 p1, T2 p2, T3 p3, ref T4 p4, ref T5 p5, ref T6 p6, T7 p7);

    public delegate TRes FuncRef4_5_7<in T1, in T2, in T3, T4, T5, in T6, T7, out TRes>(T1 p1, T2 p2, T3 p3, ref T4 p4, ref T5 p5, T6 p6, ref T7 p7);

    public delegate TRes FuncRef4_6_7<in T1, in T2, in T3, T4, in T5, T6, T7, out TRes>(T1 p1, T2 p2, T3 p3, ref T4 p4, T5 p5, ref T6 p6, ref T7 p7);

    public delegate TRes FuncRef5_6_7<in T1, in T2, in T3, in T4, T5, T6, T7, out TRes>(T1 p1, T2 p2, T3 p3, T4 p4, ref T5 p5, ref T6 p6, ref T7 p7);

    public delegate TRes FuncRef1_2_3_4<T1, T2, T3, T4, in T5, in T6, in T7, out TRes>(ref T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, T5 p5, T6 p6, T7 p7);

    public delegate TRes FuncRef1_2_3_5<T1, T2, T3, in T4, T5, in T6, in T7, out TRes>(ref T1 p1, ref T2 p2, ref T3 p3, T4 p4, ref T5 p5, T6 p6, T7 p7);

    public delegate TRes FuncRef1_2_3_6<T1, T2, T3, in T4, in T5, T6, in T7, out TRes>(ref T1 p1, ref T2 p2, ref T3 p3, T4 p4, T5 p5, ref T6 p6, T7 p7);

    public delegate TRes FuncRef1_2_3_7<T1, T2, T3, in T4, in T5, in T6, T7, out TRes>(ref T1 p1, ref T2 p2, ref T3 p3, T4 p4, T5 p5, T6 p6, ref T7 p7);

    public delegate TRes FuncRef1_2_4_5<T1, T2, in T3, T4, T5, in T6, in T7, out TRes>(ref T1 p1, ref T2 p2, T3 p3, ref T4 p4, ref T5 p5, T6 p6, T7 p7);

    public delegate TRes FuncRef1_2_4_6<T1, T2, in T3, T4, in T5, T6, in T7, out TRes>(ref T1 p1, ref T2 p2, T3 p3, ref T4 p4, T5 p5, ref T6 p6, T7 p7);

    public delegate TRes FuncRef1_2_4_7<T1, T2, in T3, T4, in T5, in T6, T7, out TRes>(ref T1 p1, ref T2 p2, T3 p3, ref T4 p4, T5 p5, T6 p6, ref T7 p7);

    public delegate TRes FuncRef1_2_5_6<T1, T2, in T3, in T4, T5, T6, in T7, out TRes>(ref T1 p1, ref T2 p2, T3 p3, T4 p4, ref T5 p5, ref T6 p6, T7 p7);

    public delegate TRes FuncRef1_2_5_7<T1, T2, in T3, in T4, T5, in T6, T7, out TRes>(ref T1 p1, ref T2 p2, T3 p3, T4 p4, ref T5 p5, T6 p6, ref T7 p7);

    public delegate TRes FuncRef1_2_6_7<T1, T2, in T3, in T4, in T5, T6, T7, out TRes>(ref T1 p1, ref T2 p2, T3 p3, T4 p4, T5 p5, ref T6 p6, ref T7 p7);

    public delegate TRes FuncRef1_3_4_5<T1, in T2, T3, T4, T5, in T6, in T7, out TRes>(ref T1 p1, T2 p2, ref T3 p3, ref T4 p4, ref T5 p5, T6 p6, T7 p7);

    public delegate TRes FuncRef1_3_4_6<T1, in T2, T3, T4, in T5, T6, in T7, out TRes>(ref T1 p1, T2 p2, ref T3 p3, ref T4 p4, T5 p5, ref T6 p6, T7 p7);

    public delegate TRes FuncRef1_3_4_7<T1, in T2, T3, T4, in T5, in T6, T7, out TRes>(ref T1 p1, T2 p2, ref T3 p3, ref T4 p4, T5 p5, T6 p6, ref T7 p7);

    public delegate TRes FuncRef1_3_5_6<T1, in T2, T3, in T4, T5, T6, in T7, out TRes>(ref T1 p1, T2 p2, ref T3 p3, T4 p4, ref T5 p5, ref T6 p6, T7 p7);

    public delegate TRes FuncRef1_3_5_7<T1, in T2, T3, in T4, T5, in T6, T7, out TRes>(ref T1 p1, T2 p2, ref T3 p3, T4 p4, ref T5 p5, T6 p6, ref T7 p7);

    public delegate TRes FuncRef1_3_6_7<T1, in T2, T3, in T4, in T5, T6, T7, out TRes>(ref T1 p1, T2 p2, ref T3 p3, T4 p4, T5 p5, ref T6 p6, ref T7 p7);

    public delegate TRes FuncRef1_4_5_6<T1, in T2, in T3, T4, T5, T6, in T7, out TRes>(ref T1 p1, T2 p2, T3 p3, ref T4 p4, ref T5 p5, ref T6 p6, T7 p7);

    public delegate TRes FuncRef1_4_5_7<T1, in T2, in T3, T4, T5, in T6, T7, out TRes>(ref T1 p1, T2 p2, T3 p3, ref T4 p4, ref T5 p5, T6 p6, ref T7 p7);

    public delegate TRes FuncRef1_4_6_7<T1, in T2, in T3, T4, in T5, T6, T7, out TRes>(ref T1 p1, T2 p2, T3 p3, ref T4 p4, T5 p5, ref T6 p6, ref T7 p7);

    public delegate TRes FuncRef1_5_6_7<T1, in T2, in T3, in T4, T5, T6, T7, out TRes>(ref T1 p1, T2 p2, T3 p3, T4 p4, ref T5 p5, ref T6 p6, ref T7 p7);

    public delegate TRes FuncRef2_3_4_5<in T1, T2, T3, T4, T5, in T6, in T7, out TRes>(T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, ref T5 p5, T6 p6, T7 p7);

    public delegate TRes FuncRef2_3_4_6<in T1, T2, T3, T4, in T5, T6, in T7, out TRes>(T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, T5 p5, ref T6 p6, T7 p7);

    public delegate TRes FuncRef2_3_4_7<in T1, T2, T3, T4, in T5, in T6, T7, out TRes>(T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, T5 p5, T6 p6, ref T7 p7);

    public delegate TRes FuncRef2_3_5_6<in T1, T2, T3, in T4, T5, T6, in T7, out TRes>(T1 p1, ref T2 p2, ref T3 p3, T4 p4, ref T5 p5, ref T6 p6, T7 p7);

    public delegate TRes FuncRef2_3_5_7<in T1, T2, T3, in T4, T5, in T6, T7, out TRes>(T1 p1, ref T2 p2, ref T3 p3, T4 p4, ref T5 p5, T6 p6, ref T7 p7);

    public delegate TRes FuncRef2_3_6_7<in T1, T2, T3, in T4, in T5, T6, T7, out TRes>(T1 p1, ref T2 p2, ref T3 p3, T4 p4, T5 p5, ref T6 p6, ref T7 p7);

    public delegate TRes FuncRef2_4_5_6<in T1, T2, in T3, T4, T5, T6, in T7, out TRes>(T1 p1, ref T2 p2, T3 p3, ref T4 p4, ref T5 p5, ref T6 p6, T7 p7);

    public delegate TRes FuncRef2_4_5_7<in T1, T2, in T3, T4, T5, in T6, T7, out TRes>(T1 p1, ref T2 p2, T3 p3, ref T4 p4, ref T5 p5, T6 p6, ref T7 p7);

    public delegate TRes FuncRef2_4_6_7<in T1, T2, in T3, T4, in T5, T6, T7, out TRes>(T1 p1, ref T2 p2, T3 p3, ref T4 p4, T5 p5, ref T6 p6, ref T7 p7);

    public delegate TRes FuncRef2_5_6_7<in T1, T2, in T3, in T4, T5, T6, T7, out TRes>(T1 p1, ref T2 p2, T3 p3, T4 p4, ref T5 p5, ref T6 p6, ref T7 p7);

    public delegate TRes FuncRef3_4_5_6<in T1, in T2, T3, T4, T5, T6, in T7, out TRes>(T1 p1, T2 p2, ref T3 p3, ref T4 p4, ref T5 p5, ref T6 p6, T7 p7);

    public delegate TRes FuncRef3_4_5_7<in T1, in T2, T3, T4, T5, in T6, T7, out TRes>(T1 p1, T2 p2, ref T3 p3, ref T4 p4, ref T5 p5, T6 p6, ref T7 p7);

    public delegate TRes FuncRef3_4_6_7<in T1, in T2, T3, T4, in T5, T6, T7, out TRes>(T1 p1, T2 p2, ref T3 p3, ref T4 p4, T5 p5, ref T6 p6, ref T7 p7);

    public delegate TRes FuncRef3_5_6_7<in T1, in T2, T3, in T4, T5, T6, T7, out TRes>(T1 p1, T2 p2, ref T3 p3, T4 p4, ref T5 p5, ref T6 p6, ref T7 p7);

    public delegate TRes FuncRef4_5_6_7<in T1, in T2, in T3, T4, T5, T6, T7, out TRes>(T1 p1, T2 p2, T3 p3, ref T4 p4, ref T5 p5, ref T6 p6, ref T7 p7);

    public delegate TRes FuncRef1_2_3_4_5<T1, T2, T3, T4, T5, in T6, in T7, out TRes>(ref T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, ref T5 p5, T6 p6, T7 p7);

    public delegate TRes FuncRef1_2_3_4_6<T1, T2, T3, T4, in T5, T6, in T7, out TRes>(ref T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, T5 p5, ref T6 p6, T7 p7);

    public delegate TRes FuncRef1_2_3_4_7<T1, T2, T3, T4, in T5, in T6, T7, out TRes>(ref T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, T5 p5, T6 p6, ref T7 p7);

    public delegate TRes FuncRef1_2_3_5_6<T1, T2, T3, in T4, T5, T6, in T7, out TRes>(ref T1 p1, ref T2 p2, ref T3 p3, T4 p4, ref T5 p5, ref T6 p6, T7 p7);

    public delegate TRes FuncRef1_2_3_5_7<T1, T2, T3, in T4, T5, in T6, T7, out TRes>(ref T1 p1, ref T2 p2, ref T3 p3, T4 p4, ref T5 p5, T6 p6, ref T7 p7);

    public delegate TRes FuncRef1_2_3_6_7<T1, T2, T3, in T4, in T5, T6, T7, out TRes>(ref T1 p1, ref T2 p2, ref T3 p3, T4 p4, T5 p5, ref T6 p6, ref T7 p7);

    public delegate TRes FuncRef1_2_4_5_6<T1, T2, in T3, T4, T5, T6, in T7, out TRes>(ref T1 p1, ref T2 p2, T3 p3, ref T4 p4, ref T5 p5, ref T6 p6, T7 p7);

    public delegate TRes FuncRef1_2_4_5_7<T1, T2, in T3, T4, T5, in T6, T7, out TRes>(ref T1 p1, ref T2 p2, T3 p3, ref T4 p4, ref T5 p5, T6 p6, ref T7 p7);

    public delegate TRes FuncRef1_2_4_6_7<T1, T2, in T3, T4, in T5, T6, T7, out TRes>(ref T1 p1, ref T2 p2, T3 p3, ref T4 p4, T5 p5, ref T6 p6, ref T7 p7);

    public delegate TRes FuncRef1_2_5_6_7<T1, T2, in T3, in T4, T5, T6, T7, out TRes>(ref T1 p1, ref T2 p2, T3 p3, T4 p4, ref T5 p5, ref T6 p6, ref T7 p7);

    public delegate TRes FuncRef1_3_4_5_6<T1, in T2, T3, T4, T5, T6, in T7, out TRes>(ref T1 p1, T2 p2, ref T3 p3, ref T4 p4, ref T5 p5, ref T6 p6, T7 p7);

    public delegate TRes FuncRef1_3_4_5_7<T1, in T2, T3, T4, T5, in T6, T7, out TRes>(ref T1 p1, T2 p2, ref T3 p3, ref T4 p4, ref T5 p5, T6 p6, ref T7 p7);

    public delegate TRes FuncRef1_3_4_6_7<T1, in T2, T3, T4, in T5, T6, T7, out TRes>(ref T1 p1, T2 p2, ref T3 p3, ref T4 p4, T5 p5, ref T6 p6, ref T7 p7);

    public delegate TRes FuncRef1_3_5_6_7<T1, in T2, T3, in T4, T5, T6, T7, out TRes>(ref T1 p1, T2 p2, ref T3 p3, T4 p4, ref T5 p5, ref T6 p6, ref T7 p7);

    public delegate TRes FuncRef1_4_5_6_7<T1, in T2, in T3, T4, T5, T6, T7, out TRes>(ref T1 p1, T2 p2, T3 p3, ref T4 p4, ref T5 p5, ref T6 p6, ref T7 p7);

    public delegate TRes FuncRef2_3_4_5_6<in T1, T2, T3, T4, T5, T6, in T7, out TRes>(T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, ref T5 p5, ref T6 p6, T7 p7);

    public delegate TRes FuncRef2_3_4_5_7<in T1, T2, T3, T4, T5, in T6, T7, out TRes>(T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, ref T5 p5, T6 p6, ref T7 p7);

    public delegate TRes FuncRef2_3_4_6_7<in T1, T2, T3, T4, in T5, T6, T7, out TRes>(T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, T5 p5, ref T6 p6, ref T7 p7);

    public delegate TRes FuncRef2_3_5_6_7<in T1, T2, T3, in T4, T5, T6, T7, out TRes>(T1 p1, ref T2 p2, ref T3 p3, T4 p4, ref T5 p5, ref T6 p6, ref T7 p7);

    public delegate TRes FuncRef2_4_5_6_7<in T1, T2, in T3, T4, T5, T6, T7, out TRes>(T1 p1, ref T2 p2, T3 p3, ref T4 p4, ref T5 p5, ref T6 p6, ref T7 p7);

    public delegate TRes FuncRef3_4_5_6_7<in T1, in T2, T3, T4, T5, T6, T7, out TRes>(T1 p1, T2 p2, ref T3 p3, ref T4 p4, ref T5 p5, ref T6 p6, ref T7 p7);

    public delegate TRes FuncRef1_2_3_4_5_6<T1, T2, T3, T4, T5, T6, in T7, out TRes>(ref T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, ref T5 p5, ref T6 p6, T7 p7);

    public delegate TRes FuncRef1_2_3_4_5_7<T1, T2, T3, T4, T5, in T6, T7, out TRes>(ref T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, ref T5 p5, T6 p6, ref T7 p7);

    public delegate TRes FuncRef1_2_3_4_6_7<T1, T2, T3, T4, in T5, T6, T7, out TRes>(ref T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, T5 p5, ref T6 p6, ref T7 p7);

    public delegate TRes FuncRef1_2_3_5_6_7<T1, T2, T3, in T4, T5, T6, T7, out TRes>(ref T1 p1, ref T2 p2, ref T3 p3, T4 p4, ref T5 p5, ref T6 p6, ref T7 p7);

    public delegate TRes FuncRef1_2_4_5_6_7<T1, T2, in T3, T4, T5, T6, T7, out TRes>(ref T1 p1, ref T2 p2, T3 p3, ref T4 p4, ref T5 p5, ref T6 p6, ref T7 p7);

    public delegate TRes FuncRef1_3_4_5_6_7<T1, in T2, T3, T4, T5, T6, T7, out TRes>(ref T1 p1, T2 p2, ref T3 p3, ref T4 p4, ref T5 p5, ref T6 p6, ref T7 p7);

    public delegate TRes FuncRef2_3_4_5_6_7<in T1, T2, T3, T4, T5, T6, T7, out TRes>(T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, ref T5 p5, ref T6 p6, ref T7 p7);

    public delegate TRes FuncRef<T1, T2, T3, T4, T5, T6, T7, out TRes>(ref T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, ref T5 p5, ref T6 p6, ref T7 p7);

#endif

}
