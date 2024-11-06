/*!
 * Copyright (c) 2016  Denis Kuzmin <x-3F@outlook.com> github/3F
 * Copyright (c) Conari contributors https://github.com/3F/Conari/graphs/contributors
 * Licensed under the MIT License (MIT).
 * See accompanying LICENSE.txt file or visit https://github.com/3F/Conari
*/

namespace net.r_eg.Conari.Types.Action.Ref
{
    /*
         One group should be: C1 + C2 + .. Cn
                              Where C = n! / (m! * (n - m)!)
                                                                */

    public delegate void ActionRef<T1>(ref T1 p1);


    /* 2 param-group */

    public delegate void ActionRef1<T1, in T2>(ref T1 p1, T2 p2);

    public delegate void ActionRef2<in T1, T2>(T1 p1, ref T2 p2);

    public delegate void ActionRef<T1, T2>(ref T1 p1, ref T2 p2);


/* 3 param-group */
#if F_PREDEFINED_ACT_P3

    public delegate void ActionRef1<T1, in T2, in T3>(ref T1 p1, T2 p2, T3 p3);

    public delegate void ActionRef2<in T1, T2, in T3>(T1 p1, ref T2 p2, T3 p3);

    public delegate void ActionRef3<in T1, in T2, T3>(T1 p1, T2 p2, ref T3 p3);

    public delegate void ActionRef1_2<T1, T2, in T3>(ref T1 p1, ref T2 p2, T3 p3);

    public delegate void ActionRef1_3<T1, in T2, T3>(ref T1 p1, T2 p2, ref T3 p3);

    public delegate void ActionRef2_3<in T1, T2, T3>(T1 p1, ref T2 p2, ref T3 p3);

    public delegate void ActionRef<T1, T2, T3>(ref T1 p1, ref T2 p2, ref T3 p3);

#endif

/* 4 param-group */
#if F_PREDEFINED_ACT_P4

    public delegate void ActionRef1<T1, in T2, in T3, in T4>(ref T1 p1, T2 p2, T3 p3, T4 p4);

    public delegate void ActionRef2<in T1, T2, in T3, in T4>(T1 p1, ref T2 p2, T3 p3, T4 p4);

    public delegate void ActionRef3<in T1, in T2, T3, in T4>(T1 p1, T2 p2, ref T3 p3, T4 p4);

    public delegate void ActionRef4<in T1, in T2, in T3, T4>(T1 p1, T2 p2, T3 p3, ref T4 p4);

    public delegate void ActionRef1_2<T1, T2, in T3, in T4>(ref T1 p1, ref T2 p2, T3 p3, T4 p4);

    public delegate void ActionRef1_3<T1, in T2, T3, in T4>(ref T1 p1, T2 p2, ref T3 p3, T4 p4);

    public delegate void ActionRef1_4<T1, in T2, in T3, T4>(ref T1 p1, T2 p2, T3 p3, ref T4 p4);

    public delegate void ActionRef2_3<in T1, T2, T3, in T4>(T1 p1, ref T2 p2, ref T3 p3, T4 p4);

    public delegate void ActionRef2_4<in T1, T2, in T3, T4>(T1 p1, ref T2 p2, T3 p3, ref T4 p4);

    public delegate void ActionRef3_4<in T1, in T2, T3, T4>(T1 p1, T2 p2, ref T3 p3, ref T4 p4);

    public delegate void ActionRef1_2_3<T1, T2, T3, in T4>(ref T1 p1, ref T2 p2, ref T3 p3, T4 p4);

    public delegate void ActionRef1_2_4<T1, T2, in T3, T4>(ref T1 p1, ref T2 p2, T3 p3, ref T4 p4);

    public delegate void ActionRef1_3_4<T1, in T2, T3, T4>(ref T1 p1, T2 p2, ref T3 p3, ref T4 p4);

    public delegate void ActionRef2_3_4<in T1, T2, T3, T4>(T1 p1, ref T2 p2, ref T3 p3, ref T4 p4);

    public delegate void ActionRef<T1, T2, T3, T4>(ref T1 p1, ref T2 p2, ref T3 p3, ref T4 p4);

#endif

/* 5 param-group */
#if F_PREDEFINED_ACT_P5

    public delegate void ActionRef1<T1, in T2, in T3, in T4, in T5>(ref T1 p1, T2 p2, T3 p3, T4 p4, T5 p5);

    public delegate void ActionRef2<in T1, T2, in T3, in T4, in T5>(T1 p1, ref T2 p2, T3 p3, T4 p4, T5 p5);

    public delegate void ActionRef3<in T1, in T2, T3, in T4, in T5>(T1 p1, T2 p2, ref T3 p3, T4 p4, T5 p5);

    public delegate void ActionRef4<in T1, in T2, in T3, T4, in T5>(T1 p1, T2 p2, T3 p3, ref T4 p4, T5 p5);

    public delegate void ActionRef5<in T1, in T2, in T3, in T4, T5>(T1 p1, T2 p2, T3 p3, T4 p4, ref T5 p5);

    public delegate void ActionRef1_2<T1, T2, in T3, in T4, in T5>(ref T1 p1, ref T2 p2, T3 p3, T4 p4, T5 p5);

    public delegate void ActionRef1_3<T1, in T2, T3, in T4, in T5>(ref T1 p1, T2 p2, ref T3 p3, T4 p4, T5 p5);

    public delegate void ActionRef1_4<T1, in T2, in T3, T4, in T5>(ref T1 p1, T2 p2, T3 p3, ref T4 p4, T5 p5);

    public delegate void ActionRef1_5<T1, in T2, in T3, in T4, T5>(ref T1 p1, T2 p2, T3 p3, T4 p4, ref T5 p5);

    public delegate void ActionRef2_3<in T1, T2, T3, in T4, in T5>(T1 p1, ref T2 p2, ref T3 p3, T4 p4, T5 p5);

    public delegate void ActionRef2_4<in T1, T2, in T3, T4, in T5>(T1 p1, ref T2 p2, T3 p3, ref T4 p4, T5 p5);

    public delegate void ActionRef2_5<in T1, T2, in T3, in T4, T5>(T1 p1, ref T2 p2, T3 p3, T4 p4, ref T5 p5);

    public delegate void ActionRef3_4<in T1, in T2, T3, T4, in T5>(T1 p1, T2 p2, ref T3 p3, ref T4 p4, T5 p5);

    public delegate void ActionRef3_5<in T1, in T2, T3, in T4, T5>(T1 p1, T2 p2, ref T3 p3, T4 p4, ref T5 p5);

    public delegate void ActionRef4_5<in T1, in T2, in T3, T4, T5>(T1 p1, T2 p2, T3 p3, ref T4 p4, ref T5 p5);

    public delegate void ActionRef1_2_3<T1, T2, T3, in T4, in T5>(ref T1 p1, ref T2 p2, ref T3 p3, T4 p4, T5 p5);

    public delegate void ActionRef1_2_4<T1, T2, in T3, T4, in T5>(ref T1 p1, ref T2 p2, T3 p3, ref T4 p4, T5 p5);

    public delegate void ActionRef1_2_5<T1, T2, in T3, in T4, T5>(ref T1 p1, ref T2 p2, T3 p3, T4 p4, ref T5 p5);

    public delegate void ActionRef1_3_4<T1, in T2, T3, T4, in T5>(ref T1 p1, T2 p2, ref T3 p3, ref T4 p4, T5 p5);

    public delegate void ActionRef1_3_5<T1, in T2, T3, in T4, T5>(ref T1 p1, T2 p2, ref T3 p3, T4 p4, ref T5 p5);

    public delegate void ActionRef1_4_5<T1, in T2, in T3, T4, T5>(ref T1 p1, T2 p2, T3 p3, ref T4 p4, ref T5 p5);

    public delegate void ActionRef2_3_4<in T1, T2, T3, T4, in T5>(T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, T5 p5);

    public delegate void ActionRef2_3_5<in T1, T2, T3, in T4, T5>(T1 p1, ref T2 p2, ref T3 p3, T4 p4, ref T5 p5);

    public delegate void ActionRef2_4_5<in T1, T2, in T3, T4, T5>(T1 p1, ref T2 p2, T3 p3, ref T4 p4, ref T5 p5);

    public delegate void ActionRef3_4_5<in T1, in T2, T3, T4, T5>(T1 p1, T2 p2, ref T3 p3, ref T4 p4, ref T5 p5);

    public delegate void ActionRef1_2_3_4<T1, T2, T3, T4, in T5>(ref T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, T5 p5);

    public delegate void ActionRef1_2_3_5<T1, T2, T3, in T4, T5>(ref T1 p1, ref T2 p2, ref T3 p3, T4 p4, ref T5 p5);

    public delegate void ActionRef1_2_4_5<T1, T2, in T3, T4, T5>(ref T1 p1, ref T2 p2, T3 p3, ref T4 p4, ref T5 p5);

    public delegate void ActionRef1_3_4_5<T1, in T2, T3, T4, T5>(ref T1 p1, T2 p2, ref T3 p3, ref T4 p4, ref T5 p5);

    public delegate void ActionRef2_3_4_5<in T1, T2, T3, T4, T5>(T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, ref T5 p5);

    public delegate void ActionRef<T1, T2, T3, T4, T5>(ref T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, ref T5 p5);

#endif

/* 6 param-group */
#if F_PREDEFINED_ACT_P6

    public delegate void ActionRef1<T1, in T2, in T3, in T4, in T5, in T6>(ref T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6);

    public delegate void ActionRef2<in T1, T2, in T3, in T4, in T5, in T6>(T1 p1, ref T2 p2, T3 p3, T4 p4, T5 p5, T6 p6);

    public delegate void ActionRef3<in T1, in T2, T3, in T4, in T5, in T6>(T1 p1, T2 p2, ref T3 p3, T4 p4, T5 p5, T6 p6);

    public delegate void ActionRef4<in T1, in T2, in T3, T4, in T5, in T6>(T1 p1, T2 p2, T3 p3, ref T4 p4, T5 p5, T6 p6);

    public delegate void ActionRef5<in T1, in T2, in T3, in T4, T5, in T6>(T1 p1, T2 p2, T3 p3, T4 p4, ref T5 p5, T6 p6);

    public delegate void ActionRef6<in T1, in T2, in T3, in T4, in T5, T6>(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, ref T6 p6);

    public delegate void ActionRef1_2<T1, T2, in T3, in T4, in T5, in T6>(ref T1 p1, ref T2 p2, T3 p3, T4 p4, T5 p5, T6 p6);

    public delegate void ActionRef1_3<T1, in T2, T3, in T4, in T5, in T6>(ref T1 p1, T2 p2, ref T3 p3, T4 p4, T5 p5, T6 p6);

    public delegate void ActionRef1_4<T1, in T2, in T3, T4, in T5, in T6>(ref T1 p1, T2 p2, T3 p3, ref T4 p4, T5 p5, T6 p6);

    public delegate void ActionRef1_5<T1, in T2, in T3, in T4, T5, in T6>(ref T1 p1, T2 p2, T3 p3, T4 p4, ref T5 p5, T6 p6);

    public delegate void ActionRef1_6<T1, in T2, in T3, in T4, in T5, T6>(ref T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, ref T6 p6);

    public delegate void ActionRef2_3<in T1, T2, T3, in T4, in T5, in T6>(T1 p1, ref T2 p2, ref T3 p3, T4 p4, T5 p5, T6 p6);

    public delegate void ActionRef2_4<in T1, T2, in T3, T4, in T5, in T6>(T1 p1, ref T2 p2, T3 p3, ref T4 p4, T5 p5, T6 p6);

    public delegate void ActionRef2_5<in T1, T2, in T3, in T4, T5, in T6>(T1 p1, ref T2 p2, T3 p3, T4 p4, ref T5 p5, T6 p6);

    public delegate void ActionRef2_6<in T1, T2, in T3, in T4, in T5, T6>(T1 p1, ref T2 p2, T3 p3, T4 p4, T5 p5, ref T6 p6);

    public delegate void ActionRef3_4<in T1, in T2, T3, T4, in T5, in T6>(T1 p1, T2 p2, ref T3 p3, ref T4 p4, T5 p5, T6 p6);

    public delegate void ActionRef3_5<in T1, in T2, T3, in T4, T5, in T6>(T1 p1, T2 p2, ref T3 p3, T4 p4, ref T5 p5, T6 p6);

    public delegate void ActionRef3_6<in T1, in T2, T3, in T4, in T5, T6>(T1 p1, T2 p2, ref T3 p3, T4 p4, T5 p5, ref T6 p6);

    public delegate void ActionRef4_5<in T1, in T2, in T3, T4, T5, in T6>(T1 p1, T2 p2, T3 p3, ref T4 p4, ref T5 p5, T6 p6);

    public delegate void ActionRef4_6<in T1, in T2, in T3, T4, in T5, T6>(T1 p1, T2 p2, T3 p3, ref T4 p4, T5 p5, ref T6 p6);

    public delegate void ActionRef5_6<in T1, in T2, in T3, in T4, T5, T6>(T1 p1, T2 p2, T3 p3, T4 p4, ref T5 p5, ref T6 p6);

    public delegate void ActionRef1_2_3<T1, T2, T3, in T4, in T5, in T6>(ref T1 p1, ref T2 p2, ref T3 p3, T4 p4, T5 p5, T6 p6);

    public delegate void ActionRef1_2_4<T1, T2, in T3, T4, in T5, in T6>(ref T1 p1, ref T2 p2, T3 p3, ref T4 p4, T5 p5, T6 p6);

    public delegate void ActionRef1_2_5<T1, T2, in T3, in T4, T5, in T6>(ref T1 p1, ref T2 p2, T3 p3, T4 p4, ref T5 p5, T6 p6);

    public delegate void ActionRef1_2_6<T1, T2, in T3, in T4, in T5, T6>(ref T1 p1, ref T2 p2, T3 p3, T4 p4, T5 p5, ref T6 p6);

    public delegate void ActionRef1_3_4<T1, in T2, T3, T4, in T5, in T6>(ref T1 p1, T2 p2, ref T3 p3, ref T4 p4, T5 p5, T6 p6);

    public delegate void ActionRef1_3_5<T1, in T2, T3, in T4, T5, in T6>(ref T1 p1, T2 p2, ref T3 p3, T4 p4, ref T5 p5, T6 p6);

    public delegate void ActionRef1_3_6<T1, in T2, T3, in T4, in T5, T6>(ref T1 p1, T2 p2, ref T3 p3, T4 p4, T5 p5, ref T6 p6);

    public delegate void ActionRef1_4_5<T1, in T2, in T3, T4, T5, in T6>(ref T1 p1, T2 p2, T3 p3, ref T4 p4, ref T5 p5, T6 p6);

    public delegate void ActionRef1_4_6<T1, in T2, in T3, T4, in T5, T6>(ref T1 p1, T2 p2, T3 p3, ref T4 p4, T5 p5, ref T6 p6);

    public delegate void ActionRef1_5_6<T1, in T2, in T3, in T4, T5, T6>(ref T1 p1, T2 p2, T3 p3, T4 p4, ref T5 p5, ref T6 p6);

    public delegate void ActionRef2_3_4<in T1, T2, T3, T4, in T5, in T6>(T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, T5 p5, T6 p6);

    public delegate void ActionRef2_3_5<in T1, T2, T3, in T4, T5, in T6>(T1 p1, ref T2 p2, ref T3 p3, T4 p4, ref T5 p5, T6 p6);

    public delegate void ActionRef2_3_6<in T1, T2, T3, in T4, in T5, T6>(T1 p1, ref T2 p2, ref T3 p3, T4 p4, T5 p5, ref T6 p6);

    public delegate void ActionRef2_4_5<in T1, T2, in T3, T4, T5, in T6>(T1 p1, ref T2 p2, T3 p3, ref T4 p4, ref T5 p5, T6 p6);

    public delegate void ActionRef2_4_6<in T1, T2, in T3, T4, in T5, T6>(T1 p1, ref T2 p2, T3 p3, ref T4 p4, T5 p5, ref T6 p6);

    public delegate void ActionRef2_5_6<in T1, T2, in T3, in T4, T5, T6>(T1 p1, ref T2 p2, T3 p3, T4 p4, ref T5 p5, ref T6 p6);

    public delegate void ActionRef3_4_5<in T1, in T2, T3, T4, T5, in T6>(T1 p1, T2 p2, ref T3 p3, ref T4 p4, ref T5 p5, T6 p6);

    public delegate void ActionRef3_4_6<in T1, in T2, T3, T4, in T5, T6>(T1 p1, T2 p2, ref T3 p3, ref T4 p4, T5 p5, ref T6 p6);

    public delegate void ActionRef3_5_6<in T1, in T2, T3, in T4, T5, T6>(T1 p1, T2 p2, ref T3 p3, T4 p4, ref T5 p5, ref T6 p6);

    public delegate void ActionRef4_5_6<in T1, in T2, in T3, T4, T5, T6>(T1 p1, T2 p2, T3 p3, ref T4 p4, ref T5 p5, ref T6 p6);

    public delegate void ActionRef1_2_3_4<T1, T2, T3, T4, in T5, in T6>(ref T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, T5 p5, T6 p6);

    public delegate void ActionRef1_2_3_5<T1, T2, T3, in T4, T5, in T6>(ref T1 p1, ref T2 p2, ref T3 p3, T4 p4, ref T5 p5, T6 p6);

    public delegate void ActionRef1_2_3_6<T1, T2, T3, in T4, in T5, T6>(ref T1 p1, ref T2 p2, ref T3 p3, T4 p4, T5 p5, ref T6 p6);

    public delegate void ActionRef1_2_4_5<T1, T2, in T3, T4, T5, in T6>(ref T1 p1, ref T2 p2, T3 p3, ref T4 p4, ref T5 p5, T6 p6);

    public delegate void ActionRef1_2_4_6<T1, T2, in T3, T4, in T5, T6>(ref T1 p1, ref T2 p2, T3 p3, ref T4 p4, T5 p5, ref T6 p6);

    public delegate void ActionRef1_2_5_6<T1, T2, in T3, in T4, T5, T6>(ref T1 p1, ref T2 p2, T3 p3, T4 p4, ref T5 p5, ref T6 p6);

    public delegate void ActionRef1_3_4_5<T1, in T2, T3, T4, T5, in T6>(ref T1 p1, T2 p2, ref T3 p3, ref T4 p4, ref T5 p5, T6 p6);

    public delegate void ActionRef1_3_4_6<T1, in T2, T3, T4, in T5, T6>(ref T1 p1, T2 p2, ref T3 p3, ref T4 p4, T5 p5, ref T6 p6);

    public delegate void ActionRef1_3_5_6<T1, in T2, T3, in T4, T5, T6>(ref T1 p1, T2 p2, ref T3 p3, T4 p4, ref T5 p5, ref T6 p6);

    public delegate void ActionRef1_4_5_6<T1, in T2, in T3, T4, T5, T6>(ref T1 p1, T2 p2, T3 p3, ref T4 p4, ref T5 p5, ref T6 p6);

    public delegate void ActionRef2_3_4_5<in T1, T2, T3, T4, T5, in T6>(T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, ref T5 p5, T6 p6);

    public delegate void ActionRef2_3_4_6<in T1, T2, T3, T4, in T5, T6>(T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, T5 p5, ref T6 p6);

    public delegate void ActionRef2_3_5_6<in T1, T2, T3, in T4, T5, T6>(T1 p1, ref T2 p2, ref T3 p3, T4 p4, ref T5 p5, ref T6 p6);

    public delegate void ActionRef2_4_5_6<in T1, T2, in T3, T4, T5, T6>(T1 p1, ref T2 p2, T3 p3, ref T4 p4, ref T5 p5, ref T6 p6);

    public delegate void ActionRef3_4_5_6<in T1, in T2, T3, T4, T5, T6>(T1 p1, T2 p2, ref T3 p3, ref T4 p4, ref T5 p5, ref T6 p6);

    public delegate void ActionRef1_2_3_4_5<T1, T2, T3, T4, T5, in T6>(ref T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, ref T5 p5, T6 p6);

    public delegate void ActionRef1_2_3_4_6<T1, T2, T3, T4, in T5, T6>(ref T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, T5 p5, ref T6 p6);

    public delegate void ActionRef1_2_3_5_6<T1, T2, T3, in T4, T5, T6>(ref T1 p1, ref T2 p2, ref T3 p3, T4 p4, ref T5 p5, ref T6 p6);

    public delegate void ActionRef1_2_4_5_6<T1, T2, in T3, T4, T5, T6>(ref T1 p1, ref T2 p2, T3 p3, ref T4 p4, ref T5 p5, ref T6 p6);

    public delegate void ActionRef1_3_4_5_6<T1, in T2, T3, T4, T5, T6>(ref T1 p1, T2 p2, ref T3 p3, ref T4 p4, ref T5 p5, ref T6 p6);

    public delegate void ActionRef2_3_4_5_6<in T1, T2, T3, T4, T5, T6>(T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, ref T5 p5, ref T6 p6);

    public delegate void ActionRef<T1, T2, T3, T4, T5, T6>(ref T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, ref T5 p5, ref T6 p6);

#endif

/* 7 param-group */
#if F_PREDEFINED_ACT_P7

    public delegate void ActionRef1<T1, in T2, in T3, in T4, in T5, in T6, in T7>(ref T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7);

    public delegate void ActionRef2<in T1, T2, in T3, in T4, in T5, in T6, in T7>(T1 p1, ref T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7);

    public delegate void ActionRef3<in T1, in T2, T3, in T4, in T5, in T6, in T7>(T1 p1, T2 p2, ref T3 p3, T4 p4, T5 p5, T6 p6, T7 p7);

    public delegate void ActionRef4<in T1, in T2, in T3, T4, in T5, in T6, in T7>(T1 p1, T2 p2, T3 p3, ref T4 p4, T5 p5, T6 p6, T7 p7);

    public delegate void ActionRef5<in T1, in T2, in T3, in T4, T5, in T6, in T7>(T1 p1, T2 p2, T3 p3, T4 p4, ref T5 p5, T6 p6, T7 p7);

    public delegate void ActionRef6<in T1, in T2, in T3, in T4, in T5, T6, in T7>(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, ref T6 p6, T7 p7);

    public delegate void ActionRef7<in T1, in T2, in T3, in T4, in T5, in T6, T7>(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, ref T7 p7);

    public delegate void ActionRef1_2<T1, T2, in T3, in T4, in T5, in T6, in T7>(ref T1 p1, ref T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7);

    public delegate void ActionRef1_3<T1, in T2, T3, in T4, in T5, in T6, in T7>(ref T1 p1, T2 p2, ref T3 p3, T4 p4, T5 p5, T6 p6, T7 p7);

    public delegate void ActionRef1_4<T1, in T2, in T3, T4, in T5, in T6, in T7>(ref T1 p1, T2 p2, T3 p3, ref T4 p4, T5 p5, T6 p6, T7 p7);

    public delegate void ActionRef1_5<T1, in T2, in T3, in T4, T5, in T6, in T7>(ref T1 p1, T2 p2, T3 p3, T4 p4, ref T5 p5, T6 p6, T7 p7);

    public delegate void ActionRef1_6<T1, in T2, in T3, in T4, in T5, T6, in T7>(ref T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, ref T6 p6, T7 p7);

    public delegate void ActionRef1_7<T1, in T2, in T3, in T4, in T5, in T6, T7>(ref T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, ref T7 p7);

    public delegate void ActionRef2_3<in T1, T2, T3, in T4, in T5, in T6, in T7>(T1 p1, ref T2 p2, ref T3 p3, T4 p4, T5 p5, T6 p6, T7 p7);

    public delegate void ActionRef2_4<in T1, T2, in T3, T4, in T5, in T6, in T7>(T1 p1, ref T2 p2, T3 p3, ref T4 p4, T5 p5, T6 p6, T7 p7);

    public delegate void ActionRef2_5<in T1, T2, in T3, in T4, T5, in T6, in T7>(T1 p1, ref T2 p2, T3 p3, T4 p4, ref T5 p5, T6 p6, T7 p7);

    public delegate void ActionRef2_6<in T1, T2, in T3, in T4, in T5, T6, in T7>(T1 p1, ref T2 p2, T3 p3, T4 p4, T5 p5, ref T6 p6, T7 p7);

    public delegate void ActionRef2_7<in T1, T2, in T3, in T4, in T5, in T6, T7>(T1 p1, ref T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, ref T7 p7);

    public delegate void ActionRef3_4<in T1, in T2, T3, T4, in T5, in T6, in T7>(T1 p1, T2 p2, ref T3 p3, ref T4 p4, T5 p5, T6 p6, T7 p7);

    public delegate void ActionRef3_5<in T1, in T2, T3, in T4, T5, in T6, in T7>(T1 p1, T2 p2, ref T3 p3, T4 p4, ref T5 p5, T6 p6, T7 p7);

    public delegate void ActionRef3_6<in T1, in T2, T3, in T4, in T5, T6, in T7>(T1 p1, T2 p2, ref T3 p3, T4 p4, T5 p5, ref T6 p6, T7 p7);

    public delegate void ActionRef3_7<in T1, in T2, T3, in T4, in T5, in T6, T7>(T1 p1, T2 p2, ref T3 p3, T4 p4, T5 p5, T6 p6, ref T7 p7);

    public delegate void ActionRef4_5<in T1, in T2, in T3, T4, T5, in T6, in T7>(T1 p1, T2 p2, T3 p3, ref T4 p4, ref T5 p5, T6 p6, T7 p7);

    public delegate void ActionRef4_6<in T1, in T2, in T3, T4, in T5, T6, in T7>(T1 p1, T2 p2, T3 p3, ref T4 p4, T5 p5, ref T6 p6, T7 p7);

    public delegate void ActionRef4_7<in T1, in T2, in T3, T4, in T5, in T6, T7>(T1 p1, T2 p2, T3 p3, ref T4 p4, T5 p5, T6 p6, ref T7 p7);

    public delegate void ActionRef5_6<in T1, in T2, in T3, in T4, T5, T6, in T7>(T1 p1, T2 p2, T3 p3, T4 p4, ref T5 p5, ref T6 p6, T7 p7);

    public delegate void ActionRef5_7<in T1, in T2, in T3, in T4, T5, in T6, T7>(T1 p1, T2 p2, T3 p3, T4 p4, ref T5 p5, T6 p6, ref T7 p7);

    public delegate void ActionRef6_7<in T1, in T2, in T3, in T4, in T5, T6, T7>(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, ref T6 p6, ref T7 p7);

    public delegate void ActionRef1_2_3<T1, T2, T3, in T4, in T5, in T6, in T7>(ref T1 p1, ref T2 p2, ref T3 p3, T4 p4, T5 p5, T6 p6, T7 p7);

    public delegate void ActionRef1_2_4<T1, T2, in T3, T4, in T5, in T6, in T7>(ref T1 p1, ref T2 p2, T3 p3, ref T4 p4, T5 p5, T6 p6, T7 p7);

    public delegate void ActionRef1_2_5<T1, T2, in T3, in T4, T5, in T6, in T7>(ref T1 p1, ref T2 p2, T3 p3, T4 p4, ref T5 p5, T6 p6, T7 p7);

    public delegate void ActionRef1_2_6<T1, T2, in T3, in T4, in T5, T6, in T7>(ref T1 p1, ref T2 p2, T3 p3, T4 p4, T5 p5, ref T6 p6, T7 p7);

    public delegate void ActionRef1_2_7<T1, T2, in T3, in T4, in T5, in T6, T7>(ref T1 p1, ref T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, ref T7 p7);

    public delegate void ActionRef1_3_4<T1, in T2, T3, T4, in T5, in T6, in T7>(ref T1 p1, T2 p2, ref T3 p3, ref T4 p4, T5 p5, T6 p6, T7 p7);

    public delegate void ActionRef1_3_5<T1, in T2, T3, in T4, T5, in T6, in T7>(ref T1 p1, T2 p2, ref T3 p3, T4 p4, ref T5 p5, T6 p6, T7 p7);

    public delegate void ActionRef1_3_6<T1, in T2, T3, in T4, in T5, T6, in T7>(ref T1 p1, T2 p2, ref T3 p3, T4 p4, T5 p5, ref T6 p6, T7 p7);

    public delegate void ActionRef1_3_7<T1, in T2, T3, in T4, in T5, in T6, T7>(ref T1 p1, T2 p2, ref T3 p3, T4 p4, T5 p5, T6 p6, ref T7 p7);

    public delegate void ActionRef1_4_5<T1, in T2, in T3, T4, T5, in T6, in T7>(ref T1 p1, T2 p2, T3 p3, ref T4 p4, ref T5 p5, T6 p6, T7 p7);

    public delegate void ActionRef1_4_6<T1, in T2, in T3, T4, in T5, T6, in T7>(ref T1 p1, T2 p2, T3 p3, ref T4 p4, T5 p5, ref T6 p6, T7 p7);

    public delegate void ActionRef1_4_7<T1, in T2, in T3, T4, in T5, in T6, T7>(ref T1 p1, T2 p2, T3 p3, ref T4 p4, T5 p5, T6 p6, ref T7 p7);

    public delegate void ActionRef1_5_6<T1, in T2, in T3, in T4, T5, T6, in T7>(ref T1 p1, T2 p2, T3 p3, T4 p4, ref T5 p5, ref T6 p6, T7 p7);

    public delegate void ActionRef1_5_7<T1, in T2, in T3, in T4, T5, in T6, T7>(ref T1 p1, T2 p2, T3 p3, T4 p4, ref T5 p5, T6 p6, ref T7 p7);

    public delegate void ActionRef1_6_7<T1, in T2, in T3, in T4, in T5, T6, T7>(ref T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, ref T6 p6, ref T7 p7);

    public delegate void ActionRef2_3_4<in T1, T2, T3, T4, in T5, in T6, in T7>(T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, T5 p5, T6 p6, T7 p7);

    public delegate void ActionRef2_3_5<in T1, T2, T3, in T4, T5, in T6, in T7>(T1 p1, ref T2 p2, ref T3 p3, T4 p4, ref T5 p5, T6 p6, T7 p7);

    public delegate void ActionRef2_3_6<in T1, T2, T3, in T4, in T5, T6, in T7>(T1 p1, ref T2 p2, ref T3 p3, T4 p4, T5 p5, ref T6 p6, T7 p7);

    public delegate void ActionRef2_3_7<in T1, T2, T3, in T4, in T5, in T6, T7>(T1 p1, ref T2 p2, ref T3 p3, T4 p4, T5 p5, T6 p6, ref T7 p7);

    public delegate void ActionRef2_4_5<in T1, T2, in T3, T4, T5, in T6, in T7>(T1 p1, ref T2 p2, T3 p3, ref T4 p4, ref T5 p5, T6 p6, T7 p7);

    public delegate void ActionRef2_4_6<in T1, T2, in T3, T4, in T5, T6, in T7>(T1 p1, ref T2 p2, T3 p3, ref T4 p4, T5 p5, ref T6 p6, T7 p7);

    public delegate void ActionRef2_4_7<in T1, T2, in T3, T4, in T5, in T6, T7>(T1 p1, ref T2 p2, T3 p3, ref T4 p4, T5 p5, T6 p6, ref T7 p7);

    public delegate void ActionRef2_5_6<in T1, T2, in T3, in T4, T5, T6, in T7>(T1 p1, ref T2 p2, T3 p3, T4 p4, ref T5 p5, ref T6 p6, T7 p7);

    public delegate void ActionRef2_5_7<in T1, T2, in T3, in T4, T5, in T6, T7>(T1 p1, ref T2 p2, T3 p3, T4 p4, ref T5 p5, T6 p6, ref T7 p7);

    public delegate void ActionRef2_6_7<in T1, T2, in T3, in T4, in T5, T6, T7>(T1 p1, ref T2 p2, T3 p3, T4 p4, T5 p5, ref T6 p6, ref T7 p7);

    public delegate void ActionRef3_4_5<in T1, in T2, T3, T4, T5, in T6, in T7>(T1 p1, T2 p2, ref T3 p3, ref T4 p4, ref T5 p5, T6 p6, T7 p7);

    public delegate void ActionRef3_4_6<in T1, in T2, T3, T4, in T5, T6, in T7>(T1 p1, T2 p2, ref T3 p3, ref T4 p4, T5 p5, ref T6 p6, T7 p7);

    public delegate void ActionRef3_4_7<in T1, in T2, T3, T4, in T5, in T6, T7>(T1 p1, T2 p2, ref T3 p3, ref T4 p4, T5 p5, T6 p6, ref T7 p7);

    public delegate void ActionRef3_5_6<in T1, in T2, T3, in T4, T5, T6, in T7>(T1 p1, T2 p2, ref T3 p3, T4 p4, ref T5 p5, ref T6 p6, T7 p7);

    public delegate void ActionRef3_5_7<in T1, in T2, T3, in T4, T5, in T6, T7>(T1 p1, T2 p2, ref T3 p3, T4 p4, ref T5 p5, T6 p6, ref T7 p7);

    public delegate void ActionRef3_6_7<in T1, in T2, T3, in T4, in T5, T6, T7>(T1 p1, T2 p2, ref T3 p3, T4 p4, T5 p5, ref T6 p6, ref T7 p7);

    public delegate void ActionRef4_5_6<in T1, in T2, in T3, T4, T5, T6, in T7>(T1 p1, T2 p2, T3 p3, ref T4 p4, ref T5 p5, ref T6 p6, T7 p7);

    public delegate void ActionRef4_5_7<in T1, in T2, in T3, T4, T5, in T6, T7>(T1 p1, T2 p2, T3 p3, ref T4 p4, ref T5 p5, T6 p6, ref T7 p7);

    public delegate void ActionRef4_6_7<in T1, in T2, in T3, T4, in T5, T6, T7>(T1 p1, T2 p2, T3 p3, ref T4 p4, T5 p5, ref T6 p6, ref T7 p7);

    public delegate void ActionRef5_6_7<in T1, in T2, in T3, in T4, T5, T6, T7>(T1 p1, T2 p2, T3 p3, T4 p4, ref T5 p5, ref T6 p6, ref T7 p7);

    public delegate void ActionRef1_2_3_4<T1, T2, T3, T4, in T5, in T6, in T7>(ref T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, T5 p5, T6 p6, T7 p7);

    public delegate void ActionRef1_2_3_5<T1, T2, T3, in T4, T5, in T6, in T7>(ref T1 p1, ref T2 p2, ref T3 p3, T4 p4, ref T5 p5, T6 p6, T7 p7);

    public delegate void ActionRef1_2_3_6<T1, T2, T3, in T4, in T5, T6, in T7>(ref T1 p1, ref T2 p2, ref T3 p3, T4 p4, T5 p5, ref T6 p6, T7 p7);

    public delegate void ActionRef1_2_3_7<T1, T2, T3, in T4, in T5, in T6, T7>(ref T1 p1, ref T2 p2, ref T3 p3, T4 p4, T5 p5, T6 p6, ref T7 p7);

    public delegate void ActionRef1_2_4_5<T1, T2, in T3, T4, T5, in T6, in T7>(ref T1 p1, ref T2 p2, T3 p3, ref T4 p4, ref T5 p5, T6 p6, T7 p7);

    public delegate void ActionRef1_2_4_6<T1, T2, in T3, T4, in T5, T6, in T7>(ref T1 p1, ref T2 p2, T3 p3, ref T4 p4, T5 p5, ref T6 p6, T7 p7);

    public delegate void ActionRef1_2_4_7<T1, T2, in T3, T4, in T5, in T6, T7>(ref T1 p1, ref T2 p2, T3 p3, ref T4 p4, T5 p5, T6 p6, ref T7 p7);

    public delegate void ActionRef1_2_5_6<T1, T2, in T3, in T4, T5, T6, in T7>(ref T1 p1, ref T2 p2, T3 p3, T4 p4, ref T5 p5, ref T6 p6, T7 p7);

    public delegate void ActionRef1_2_5_7<T1, T2, in T3, in T4, T5, in T6, T7>(ref T1 p1, ref T2 p2, T3 p3, T4 p4, ref T5 p5, T6 p6, ref T7 p7);

    public delegate void ActionRef1_2_6_7<T1, T2, in T3, in T4, in T5, T6, T7>(ref T1 p1, ref T2 p2, T3 p3, T4 p4, T5 p5, ref T6 p6, ref T7 p7);

    public delegate void ActionRef1_3_4_5<T1, in T2, T3, T4, T5, in T6, in T7>(ref T1 p1, T2 p2, ref T3 p3, ref T4 p4, ref T5 p5, T6 p6, T7 p7);

    public delegate void ActionRef1_3_4_6<T1, in T2, T3, T4, in T5, T6, in T7>(ref T1 p1, T2 p2, ref T3 p3, ref T4 p4, T5 p5, ref T6 p6, T7 p7);

    public delegate void ActionRef1_3_4_7<T1, in T2, T3, T4, in T5, in T6, T7>(ref T1 p1, T2 p2, ref T3 p3, ref T4 p4, T5 p5, T6 p6, ref T7 p7);

    public delegate void ActionRef1_3_5_6<T1, in T2, T3, in T4, T5, T6, in T7>(ref T1 p1, T2 p2, ref T3 p3, T4 p4, ref T5 p5, ref T6 p6, T7 p7);

    public delegate void ActionRef1_3_5_7<T1, in T2, T3, in T4, T5, in T6, T7>(ref T1 p1, T2 p2, ref T3 p3, T4 p4, ref T5 p5, T6 p6, ref T7 p7);

    public delegate void ActionRef1_3_6_7<T1, in T2, T3, in T4, in T5, T6, T7>(ref T1 p1, T2 p2, ref T3 p3, T4 p4, T5 p5, ref T6 p6, ref T7 p7);

    public delegate void ActionRef1_4_5_6<T1, in T2, in T3, T4, T5, T6, in T7>(ref T1 p1, T2 p2, T3 p3, ref T4 p4, ref T5 p5, ref T6 p6, T7 p7);

    public delegate void ActionRef1_4_5_7<T1, in T2, in T3, T4, T5, in T6, T7>(ref T1 p1, T2 p2, T3 p3, ref T4 p4, ref T5 p5, T6 p6, ref T7 p7);

    public delegate void ActionRef1_4_6_7<T1, in T2, in T3, T4, in T5, T6, T7>(ref T1 p1, T2 p2, T3 p3, ref T4 p4, T5 p5, ref T6 p6, ref T7 p7);

    public delegate void ActionRef1_5_6_7<T1, in T2, in T3, in T4, T5, T6, T7>(ref T1 p1, T2 p2, T3 p3, T4 p4, ref T5 p5, ref T6 p6, ref T7 p7);

    public delegate void ActionRef2_3_4_5<in T1, T2, T3, T4, T5, in T6, in T7>(T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, ref T5 p5, T6 p6, T7 p7);

    public delegate void ActionRef2_3_4_6<in T1, T2, T3, T4, in T5, T6, in T7>(T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, T5 p5, ref T6 p6, T7 p7);

    public delegate void ActionRef2_3_4_7<in T1, T2, T3, T4, in T5, in T6, T7>(T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, T5 p5, T6 p6, ref T7 p7);

    public delegate void ActionRef2_3_5_6<in T1, T2, T3, in T4, T5, T6, in T7>(T1 p1, ref T2 p2, ref T3 p3, T4 p4, ref T5 p5, ref T6 p6, T7 p7);

    public delegate void ActionRef2_3_5_7<in T1, T2, T3, in T4, T5, in T6, T7>(T1 p1, ref T2 p2, ref T3 p3, T4 p4, ref T5 p5, T6 p6, ref T7 p7);

    public delegate void ActionRef2_3_6_7<in T1, T2, T3, in T4, in T5, T6, T7>(T1 p1, ref T2 p2, ref T3 p3, T4 p4, T5 p5, ref T6 p6, ref T7 p7);

    public delegate void ActionRef2_4_5_6<in T1, T2, in T3, T4, T5, T6, in T7>(T1 p1, ref T2 p2, T3 p3, ref T4 p4, ref T5 p5, ref T6 p6, T7 p7);

    public delegate void ActionRef2_4_5_7<in T1, T2, in T3, T4, T5, in T6, T7>(T1 p1, ref T2 p2, T3 p3, ref T4 p4, ref T5 p5, T6 p6, ref T7 p7);

    public delegate void ActionRef2_4_6_7<in T1, T2, in T3, T4, in T5, T6, T7>(T1 p1, ref T2 p2, T3 p3, ref T4 p4, T5 p5, ref T6 p6, ref T7 p7);

    public delegate void ActionRef2_5_6_7<in T1, T2, in T3, in T4, T5, T6, T7>(T1 p1, ref T2 p2, T3 p3, T4 p4, ref T5 p5, ref T6 p6, ref T7 p7);

    public delegate void ActionRef3_4_5_6<in T1, in T2, T3, T4, T5, T6, in T7>(T1 p1, T2 p2, ref T3 p3, ref T4 p4, ref T5 p5, ref T6 p6, T7 p7);

    public delegate void ActionRef3_4_5_7<in T1, in T2, T3, T4, T5, in T6, T7>(T1 p1, T2 p2, ref T3 p3, ref T4 p4, ref T5 p5, T6 p6, ref T7 p7);

    public delegate void ActionRef3_4_6_7<in T1, in T2, T3, T4, in T5, T6, T7>(T1 p1, T2 p2, ref T3 p3, ref T4 p4, T5 p5, ref T6 p6, ref T7 p7);

    public delegate void ActionRef3_5_6_7<in T1, in T2, T3, in T4, T5, T6, T7>(T1 p1, T2 p2, ref T3 p3, T4 p4, ref T5 p5, ref T6 p6, ref T7 p7);

    public delegate void ActionRef4_5_6_7<in T1, in T2, in T3, T4, T5, T6, T7>(T1 p1, T2 p2, T3 p3, ref T4 p4, ref T5 p5, ref T6 p6, ref T7 p7);

    public delegate void ActionRef1_2_3_4_5<T1, T2, T3, T4, T5, in T6, in T7>(ref T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, ref T5 p5, T6 p6, T7 p7);

    public delegate void ActionRef1_2_3_4_6<T1, T2, T3, T4, in T5, T6, in T7>(ref T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, T5 p5, ref T6 p6, T7 p7);

    public delegate void ActionRef1_2_3_4_7<T1, T2, T3, T4, in T5, in T6, T7>(ref T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, T5 p5, T6 p6, ref T7 p7);

    public delegate void ActionRef1_2_3_5_6<T1, T2, T3, in T4, T5, T6, in T7>(ref T1 p1, ref T2 p2, ref T3 p3, T4 p4, ref T5 p5, ref T6 p6, T7 p7);

    public delegate void ActionRef1_2_3_5_7<T1, T2, T3, in T4, T5, in T6, T7>(ref T1 p1, ref T2 p2, ref T3 p3, T4 p4, ref T5 p5, T6 p6, ref T7 p7);

    public delegate void ActionRef1_2_3_6_7<T1, T2, T3, in T4, in T5, T6, T7>(ref T1 p1, ref T2 p2, ref T3 p3, T4 p4, T5 p5, ref T6 p6, ref T7 p7);

    public delegate void ActionRef1_2_4_5_6<T1, T2, in T3, T4, T5, T6, in T7>(ref T1 p1, ref T2 p2, T3 p3, ref T4 p4, ref T5 p5, ref T6 p6, T7 p7);

    public delegate void ActionRef1_2_4_5_7<T1, T2, in T3, T4, T5, in T6, T7>(ref T1 p1, ref T2 p2, T3 p3, ref T4 p4, ref T5 p5, T6 p6, ref T7 p7);

    public delegate void ActionRef1_2_4_6_7<T1, T2, in T3, T4, in T5, T6, T7>(ref T1 p1, ref T2 p2, T3 p3, ref T4 p4, T5 p5, ref T6 p6, ref T7 p7);

    public delegate void ActionRef1_2_5_6_7<T1, T2, in T3, in T4, T5, T6, T7>(ref T1 p1, ref T2 p2, T3 p3, T4 p4, ref T5 p5, ref T6 p6, ref T7 p7);

    public delegate void ActionRef1_3_4_5_6<T1, in T2, T3, T4, T5, T6, in T7>(ref T1 p1, T2 p2, ref T3 p3, ref T4 p4, ref T5 p5, ref T6 p6, T7 p7);

    public delegate void ActionRef1_3_4_5_7<T1, in T2, T3, T4, T5, in T6, T7>(ref T1 p1, T2 p2, ref T3 p3, ref T4 p4, ref T5 p5, T6 p6, ref T7 p7);

    public delegate void ActionRef1_3_4_6_7<T1, in T2, T3, T4, in T5, T6, T7>(ref T1 p1, T2 p2, ref T3 p3, ref T4 p4, T5 p5, ref T6 p6, ref T7 p7);

    public delegate void ActionRef1_3_5_6_7<T1, in T2, T3, in T4, T5, T6, T7>(ref T1 p1, T2 p2, ref T3 p3, T4 p4, ref T5 p5, ref T6 p6, ref T7 p7);

    public delegate void ActionRef1_4_5_6_7<T1, in T2, in T3, T4, T5, T6, T7>(ref T1 p1, T2 p2, T3 p3, ref T4 p4, ref T5 p5, ref T6 p6, ref T7 p7);

    public delegate void ActionRef2_3_4_5_6<in T1, T2, T3, T4, T5, T6, in T7>(T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, ref T5 p5, ref T6 p6, T7 p7);

    public delegate void ActionRef2_3_4_5_7<in T1, T2, T3, T4, T5, in T6, T7>(T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, ref T5 p5, T6 p6, ref T7 p7);

    public delegate void ActionRef2_3_4_6_7<in T1, T2, T3, T4, in T5, T6, T7>(T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, T5 p5, ref T6 p6, ref T7 p7);

    public delegate void ActionRef2_3_5_6_7<in T1, T2, T3, in T4, T5, T6, T7>(T1 p1, ref T2 p2, ref T3 p3, T4 p4, ref T5 p5, ref T6 p6, ref T7 p7);

    public delegate void ActionRef2_4_5_6_7<in T1, T2, in T3, T4, T5, T6, T7>(T1 p1, ref T2 p2, T3 p3, ref T4 p4, ref T5 p5, ref T6 p6, ref T7 p7);

    public delegate void ActionRef3_4_5_6_7<in T1, in T2, T3, T4, T5, T6, T7>(T1 p1, T2 p2, ref T3 p3, ref T4 p4, ref T5 p5, ref T6 p6, ref T7 p7);

    public delegate void ActionRef1_2_3_4_5_6<T1, T2, T3, T4, T5, T6, in T7>(ref T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, ref T5 p5, ref T6 p6, T7 p7);

    public delegate void ActionRef1_2_3_4_5_7<T1, T2, T3, T4, T5, in T6, T7>(ref T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, ref T5 p5, T6 p6, ref T7 p7);

    public delegate void ActionRef1_2_3_4_6_7<T1, T2, T3, T4, in T5, T6, T7>(ref T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, T5 p5, ref T6 p6, ref T7 p7);

    public delegate void ActionRef1_2_3_5_6_7<T1, T2, T3, in T4, T5, T6, T7>(ref T1 p1, ref T2 p2, ref T3 p3, T4 p4, ref T5 p5, ref T6 p6, ref T7 p7);

    public delegate void ActionRef1_2_4_5_6_7<T1, T2, in T3, T4, T5, T6, T7>(ref T1 p1, ref T2 p2, T3 p3, ref T4 p4, ref T5 p5, ref T6 p6, ref T7 p7);

    public delegate void ActionRef1_3_4_5_6_7<T1, in T2, T3, T4, T5, T6, T7>(ref T1 p1, T2 p2, ref T3 p3, ref T4 p4, ref T5 p5, ref T6 p6, ref T7 p7);

    public delegate void ActionRef2_3_4_5_6_7<in T1, T2, T3, T4, T5, T6, T7>(T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, ref T5 p5, ref T6 p6, ref T7 p7);

    public delegate void ActionRef<T1, T2, T3, T4, T5, T6, T7>(ref T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, ref T5 p5, ref T6 p6, ref T7 p7);

#endif

}
