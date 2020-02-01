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

namespace net.r_eg.Conari.Types.Action.Out
{
    /*
         One group should be: C1 + C2 + .. Cn
                              Where C = n! / (m! * (n - m)!)
                                                                */

    public delegate void ActionOut<T1>(out T1 p1);


    /* 2 param-group */

    public delegate void ActionOut1<T1, in T2>(out T1 p1, T2 p2);

    public delegate void ActionOut2<in T1, T2>(T1 p1, out T2 p2);

    public delegate void ActionOut<T1, T2>(out T1 p1, out T2 p2);


    /* 3 param-group */

    public delegate void ActionOut1<T1, in T2, in T3>(out T1 p1, T2 p2, T3 p3);

    public delegate void ActionOut2<in T1, T2, in T3>(T1 p1, out T2 p2, T3 p3);

    public delegate void ActionOut3<in T1, in T2, T3>(T1 p1, T2 p2, out T3 p3);

    public delegate void ActionOut1_2<T1, T2, in T3>(out T1 p1, out T2 p2, T3 p3);

    public delegate void ActionOut1_3<T1, in T2, T3>(out T1 p1, T2 p2, out T3 p3);

    public delegate void ActionOut2_3<in T1, T2, T3>(T1 p1, out T2 p2, out T3 p3);

    public delegate void ActionOut<T1, T2, T3>(out T1 p1, out T2 p2, out T3 p3);


    /* 4 param-group */

    public delegate void ActionOut1<T1, in T2, in T3, in T4>(out T1 p1, T2 p2, T3 p3, T4 p4);

    public delegate void ActionOut2<in T1, T2, in T3, in T4>(T1 p1, out T2 p2, T3 p3, T4 p4);

    public delegate void ActionOut3<in T1, in T2, T3, in T4>(T1 p1, T2 p2, out T3 p3, T4 p4);

    public delegate void ActionOut4<in T1, in T2, in T3, T4>(T1 p1, T2 p2, T3 p3, out T4 p4);

    public delegate void ActionOut1_2<T1, T2, in T3, in T4>(out T1 p1, out T2 p2, T3 p3, T4 p4);

    public delegate void ActionOut1_3<T1, in T2, T3, in T4>(out T1 p1, T2 p2, out T3 p3, T4 p4);

    public delegate void ActionOut1_4<T1, in T2, in T3, T4>(out T1 p1, T2 p2, T3 p3, out T4 p4);

    public delegate void ActionOut2_3<in T1, T2, T3, in T4>(T1 p1, out T2 p2, out T3 p3, T4 p4);

    public delegate void ActionOut2_4<in T1, T2, in T3, T4>(T1 p1, out T2 p2, T3 p3, out T4 p4);

    public delegate void ActionOut3_4<in T1, in T2, T3, T4>(T1 p1, T2 p2, out T3 p3, out T4 p4);

    public delegate void ActionOut1_2_3<T1, T2, T3, in T4>(out T1 p1, out T2 p2, out T3 p3, T4 p4);

    public delegate void ActionOut1_2_4<T1, T2, in T3, T4>(out T1 p1, out T2 p2, T3 p3, out T4 p4);

    public delegate void ActionOut1_3_4<T1, in T2, T3, T4>(out T1 p1, T2 p2, out T3 p3, out T4 p4);

    public delegate void ActionOut2_3_4<in T1, T2, T3, T4>(T1 p1, out T2 p2, out T3 p3, out T4 p4);

    public delegate void ActionOut<T1, T2, T3, T4>(out T1 p1, out T2 p2, out T3 p3, out T4 p4);


    /* 5 param-group */

    public delegate void ActionOut1<T1, in T2, in T3, in T4, in T5>(out T1 p1, T2 p2, T3 p3, T4 p4, T5 p5);

    public delegate void ActionOut2<in T1, T2, in T3, in T4, in T5>(T1 p1, out T2 p2, T3 p3, T4 p4, T5 p5);

    public delegate void ActionOut3<in T1, in T2, T3, in T4, in T5>(T1 p1, T2 p2, out T3 p3, T4 p4, T5 p5);

    public delegate void ActionOut4<in T1, in T2, in T3, T4, in T5>(T1 p1, T2 p2, T3 p3, out T4 p4, T5 p5);

    public delegate void ActionOut5<in T1, in T2, in T3, in T4, T5>(T1 p1, T2 p2, T3 p3, T4 p4, out T5 p5);

    public delegate void ActionOut1_2<T1, T2, in T3, in T4, in T5>(out T1 p1, out T2 p2, T3 p3, T4 p4, T5 p5);

    public delegate void ActionOut1_3<T1, in T2, T3, in T4, in T5>(out T1 p1, T2 p2, out T3 p3, T4 p4, T5 p5);

    public delegate void ActionOut1_4<T1, in T2, in T3, T4, in T5>(out T1 p1, T2 p2, T3 p3, out T4 p4, T5 p5);

    public delegate void ActionOut1_5<T1, in T2, in T3, in T4, T5>(out T1 p1, T2 p2, T3 p3, T4 p4, out T5 p5);

    public delegate void ActionOut2_3<in T1, T2, T3, in T4, in T5>(T1 p1, out T2 p2, out T3 p3, T4 p4, T5 p5);

    public delegate void ActionOut2_4<in T1, T2, in T3, T4, in T5>(T1 p1, out T2 p2, T3 p3, out T4 p4, T5 p5);

    public delegate void ActionOut2_5<in T1, T2, in T3, in T4, T5>(T1 p1, out T2 p2, T3 p3, T4 p4, out T5 p5);

    public delegate void ActionOut3_4<in T1, in T2, T3, T4, in T5>(T1 p1, T2 p2, out T3 p3, out T4 p4, T5 p5);

    public delegate void ActionOut3_5<in T1, in T2, T3, in T4, T5>(T1 p1, T2 p2, out T3 p3, T4 p4, out T5 p5);

    public delegate void ActionOut4_5<in T1, in T2, in T3, T4, T5>(T1 p1, T2 p2, T3 p3, out T4 p4, out T5 p5);

    public delegate void ActionOut1_2_3<T1, T2, T3, in T4, in T5>(out T1 p1, out T2 p2, out T3 p3, T4 p4, T5 p5);

    public delegate void ActionOut1_2_4<T1, T2, in T3, T4, in T5>(out T1 p1, out T2 p2, T3 p3, out T4 p4, T5 p5);

    public delegate void ActionOut1_2_5<T1, T2, in T3, in T4, T5>(out T1 p1, out T2 p2, T3 p3, T4 p4, out T5 p5);

    public delegate void ActionOut1_3_4<T1, in T2, T3, T4, in T5>(out T1 p1, T2 p2, out T3 p3, out T4 p4, T5 p5);

    public delegate void ActionOut1_3_5<T1, in T2, T3, in T4, T5>(out T1 p1, T2 p2, out T3 p3, T4 p4, out T5 p5);

    public delegate void ActionOut1_4_5<T1, in T2, in T3, T4, T5>(out T1 p1, T2 p2, T3 p3, out T4 p4, out T5 p5);

    public delegate void ActionOut2_3_4<in T1, T2, T3, T4, in T5>(T1 p1, out T2 p2, out T3 p3, out T4 p4, T5 p5);

    public delegate void ActionOut2_3_5<in T1, T2, T3, in T4, T5>(T1 p1, out T2 p2, out T3 p3, T4 p4, out T5 p5);

    public delegate void ActionOut2_4_5<in T1, T2, in T3, T4, T5>(T1 p1, out T2 p2, T3 p3, out T4 p4, out T5 p5);

    public delegate void ActionOut3_4_5<in T1, in T2, T3, T4, T5>(T1 p1, T2 p2, out T3 p3, out T4 p4, out T5 p5);

    public delegate void ActionOut1_2_3_4<T1, T2, T3, T4, in T5>(out T1 p1, out T2 p2, out T3 p3, out T4 p4, T5 p5);

    public delegate void ActionOut1_2_3_5<T1, T2, T3, in T4, T5>(out T1 p1, out T2 p2, out T3 p3, T4 p4, out T5 p5);

    public delegate void ActionOut1_2_4_5<T1, T2, in T3, T4, T5>(out T1 p1, out T2 p2, T3 p3, out T4 p4, out T5 p5);

    public delegate void ActionOut1_3_4_5<T1, in T2, T3, T4, T5>(out T1 p1, T2 p2, out T3 p3, out T4 p4, out T5 p5);

    public delegate void ActionOut2_3_4_5<in T1, T2, T3, T4, T5>(T1 p1, out T2 p2, out T3 p3, out T4 p4, out T5 p5);

    public delegate void ActionOut<T1, T2, T3, T4, T5>(out T1 p1, out T2 p2, out T3 p3, out T4 p4, out T5 p5);


    /* 6 param-group */

    public delegate void ActionOut1<T1, in T2, in T3, in T4, in T5, in T6>(out T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6);

    public delegate void ActionOut2<in T1, T2, in T3, in T4, in T5, in T6>(T1 p1, out T2 p2, T3 p3, T4 p4, T5 p5, T6 p6);

    public delegate void ActionOut3<in T1, in T2, T3, in T4, in T5, in T6>(T1 p1, T2 p2, out T3 p3, T4 p4, T5 p5, T6 p6);

    public delegate void ActionOut4<in T1, in T2, in T3, T4, in T5, in T6>(T1 p1, T2 p2, T3 p3, out T4 p4, T5 p5, T6 p6);

    public delegate void ActionOut5<in T1, in T2, in T3, in T4, T5, in T6>(T1 p1, T2 p2, T3 p3, T4 p4, out T5 p5, T6 p6);

    public delegate void ActionOut6<in T1, in T2, in T3, in T4, in T5, T6>(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, out T6 p6);

    public delegate void ActionOut1_2<T1, T2, in T3, in T4, in T5, in T6>(out T1 p1, out T2 p2, T3 p3, T4 p4, T5 p5, T6 p6);

    public delegate void ActionOut1_3<T1, in T2, T3, in T4, in T5, in T6>(out T1 p1, T2 p2, out T3 p3, T4 p4, T5 p5, T6 p6);

    public delegate void ActionOut1_4<T1, in T2, in T3, T4, in T5, in T6>(out T1 p1, T2 p2, T3 p3, out T4 p4, T5 p5, T6 p6);

    public delegate void ActionOut1_5<T1, in T2, in T3, in T4, T5, in T6>(out T1 p1, T2 p2, T3 p3, T4 p4, out T5 p5, T6 p6);

    public delegate void ActionOut1_6<T1, in T2, in T3, in T4, in T5, T6>(out T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, out T6 p6);

    public delegate void ActionOut2_3<in T1, T2, T3, in T4, in T5, in T6>(T1 p1, out T2 p2, out T3 p3, T4 p4, T5 p5, T6 p6);

    public delegate void ActionOut2_4<in T1, T2, in T3, T4, in T5, in T6>(T1 p1, out T2 p2, T3 p3, out T4 p4, T5 p5, T6 p6);

    public delegate void ActionOut2_5<in T1, T2, in T3, in T4, T5, in T6>(T1 p1, out T2 p2, T3 p3, T4 p4, out T5 p5, T6 p6);

    public delegate void ActionOut2_6<in T1, T2, in T3, in T4, in T5, T6>(T1 p1, out T2 p2, T3 p3, T4 p4, T5 p5, out T6 p6);

    public delegate void ActionOut3_4<in T1, in T2, T3, T4, in T5, in T6>(T1 p1, T2 p2, out T3 p3, out T4 p4, T5 p5, T6 p6);

    public delegate void ActionOut3_5<in T1, in T2, T3, in T4, T5, in T6>(T1 p1, T2 p2, out T3 p3, T4 p4, out T5 p5, T6 p6);

    public delegate void ActionOut3_6<in T1, in T2, T3, in T4, in T5, T6>(T1 p1, T2 p2, out T3 p3, T4 p4, T5 p5, out T6 p6);

    public delegate void ActionOut4_5<in T1, in T2, in T3, T4, T5, in T6>(T1 p1, T2 p2, T3 p3, out T4 p4, out T5 p5, T6 p6);

    public delegate void ActionOut4_6<in T1, in T2, in T3, T4, in T5, T6>(T1 p1, T2 p2, T3 p3, out T4 p4, T5 p5, out T6 p6);

    public delegate void ActionOut5_6<in T1, in T2, in T3, in T4, T5, T6>(T1 p1, T2 p2, T3 p3, T4 p4, out T5 p5, out T6 p6);

    public delegate void ActionOut1_2_3<T1, T2, T3, in T4, in T5, in T6>(out T1 p1, out T2 p2, out T3 p3, T4 p4, T5 p5, T6 p6);

    public delegate void ActionOut1_2_4<T1, T2, in T3, T4, in T5, in T6>(out T1 p1, out T2 p2, T3 p3, out T4 p4, T5 p5, T6 p6);

    public delegate void ActionOut1_2_5<T1, T2, in T3, in T4, T5, in T6>(out T1 p1, out T2 p2, T3 p3, T4 p4, out T5 p5, T6 p6);

    public delegate void ActionOut1_2_6<T1, T2, in T3, in T4, in T5, T6>(out T1 p1, out T2 p2, T3 p3, T4 p4, T5 p5, out T6 p6);

    public delegate void ActionOut1_3_4<T1, in T2, T3, T4, in T5, in T6>(out T1 p1, T2 p2, out T3 p3, out T4 p4, T5 p5, T6 p6);

    public delegate void ActionOut1_3_5<T1, in T2, T3, in T4, T5, in T6>(out T1 p1, T2 p2, out T3 p3, T4 p4, out T5 p5, T6 p6);

    public delegate void ActionOut1_3_6<T1, in T2, T3, in T4, in T5, T6>(out T1 p1, T2 p2, out T3 p3, T4 p4, T5 p5, out T6 p6);

    public delegate void ActionOut1_4_5<T1, in T2, in T3, T4, T5, in T6>(out T1 p1, T2 p2, T3 p3, out T4 p4, out T5 p5, T6 p6);

    public delegate void ActionOut1_4_6<T1, in T2, in T3, T4, in T5, T6>(out T1 p1, T2 p2, T3 p3, out T4 p4, T5 p5, out T6 p6);

    public delegate void ActionOut1_5_6<T1, in T2, in T3, in T4, T5, T6>(out T1 p1, T2 p2, T3 p3, T4 p4, out T5 p5, out T6 p6);

    public delegate void ActionOut2_3_4<in T1, T2, T3, T4, in T5, in T6>(T1 p1, out T2 p2, out T3 p3, out T4 p4, T5 p5, T6 p6);

    public delegate void ActionOut2_3_5<in T1, T2, T3, in T4, T5, in T6>(T1 p1, out T2 p2, out T3 p3, T4 p4, out T5 p5, T6 p6);

    public delegate void ActionOut2_3_6<in T1, T2, T3, in T4, in T5, T6>(T1 p1, out T2 p2, out T3 p3, T4 p4, T5 p5, out T6 p6);

    public delegate void ActionOut2_4_5<in T1, T2, in T3, T4, T5, in T6>(T1 p1, out T2 p2, T3 p3, out T4 p4, out T5 p5, T6 p6);

    public delegate void ActionOut2_4_6<in T1, T2, in T3, T4, in T5, T6>(T1 p1, out T2 p2, T3 p3, out T4 p4, T5 p5, out T6 p6);

    public delegate void ActionOut2_5_6<in T1, T2, in T3, in T4, T5, T6>(T1 p1, out T2 p2, T3 p3, T4 p4, out T5 p5, out T6 p6);

    public delegate void ActionOut3_4_5<in T1, in T2, T3, T4, T5, in T6>(T1 p1, T2 p2, out T3 p3, out T4 p4, out T5 p5, T6 p6);

    public delegate void ActionOut3_4_6<in T1, in T2, T3, T4, in T5, T6>(T1 p1, T2 p2, out T3 p3, out T4 p4, T5 p5, out T6 p6);

    public delegate void ActionOut3_5_6<in T1, in T2, T3, in T4, T5, T6>(T1 p1, T2 p2, out T3 p3, T4 p4, out T5 p5, out T6 p6);

    public delegate void ActionOut4_5_6<in T1, in T2, in T3, T4, T5, T6>(T1 p1, T2 p2, T3 p3, out T4 p4, out T5 p5, out T6 p6);

    public delegate void ActionOut1_2_3_4<T1, T2, T3, T4, in T5, in T6>(out T1 p1, out T2 p2, out T3 p3, out T4 p4, T5 p5, T6 p6);

    public delegate void ActionOut1_2_3_5<T1, T2, T3, in T4, T5, in T6>(out T1 p1, out T2 p2, out T3 p3, T4 p4, out T5 p5, T6 p6);

    public delegate void ActionOut1_2_3_6<T1, T2, T3, in T4, in T5, T6>(out T1 p1, out T2 p2, out T3 p3, T4 p4, T5 p5, out T6 p6);

    public delegate void ActionOut1_2_4_5<T1, T2, in T3, T4, T5, in T6>(out T1 p1, out T2 p2, T3 p3, out T4 p4, out T5 p5, T6 p6);

    public delegate void ActionOut1_2_4_6<T1, T2, in T3, T4, in T5, T6>(out T1 p1, out T2 p2, T3 p3, out T4 p4, T5 p5, out T6 p6);

    public delegate void ActionOut1_2_5_6<T1, T2, in T3, in T4, T5, T6>(out T1 p1, out T2 p2, T3 p3, T4 p4, out T5 p5, out T6 p6);

    public delegate void ActionOut1_3_4_5<T1, in T2, T3, T4, T5, in T6>(out T1 p1, T2 p2, out T3 p3, out T4 p4, out T5 p5, T6 p6);

    public delegate void ActionOut1_3_4_6<T1, in T2, T3, T4, in T5, T6>(out T1 p1, T2 p2, out T3 p3, out T4 p4, T5 p5, out T6 p6);

    public delegate void ActionOut1_3_5_6<T1, in T2, T3, in T4, T5, T6>(out T1 p1, T2 p2, out T3 p3, T4 p4, out T5 p5, out T6 p6);

    public delegate void ActionOut1_4_5_6<T1, in T2, in T3, T4, T5, T6>(out T1 p1, T2 p2, T3 p3, out T4 p4, out T5 p5, out T6 p6);

    public delegate void ActionOut2_3_4_5<in T1, T2, T3, T4, T5, in T6>(T1 p1, out T2 p2, out T3 p3, out T4 p4, out T5 p5, T6 p6);

    public delegate void ActionOut2_3_4_6<in T1, T2, T3, T4, in T5, T6>(T1 p1, out T2 p2, out T3 p3, out T4 p4, T5 p5, out T6 p6);

    public delegate void ActionOut2_3_5_6<in T1, T2, T3, in T4, T5, T6>(T1 p1, out T2 p2, out T3 p3, T4 p4, out T5 p5, out T6 p6);

    public delegate void ActionOut2_4_5_6<in T1, T2, in T3, T4, T5, T6>(T1 p1, out T2 p2, T3 p3, out T4 p4, out T5 p5, out T6 p6);

    public delegate void ActionOut3_4_5_6<in T1, in T2, T3, T4, T5, T6>(T1 p1, T2 p2, out T3 p3, out T4 p4, out T5 p5, out T6 p6);

    public delegate void ActionOut1_2_3_4_5<T1, T2, T3, T4, T5, in T6>(out T1 p1, out T2 p2, out T3 p3, out T4 p4, out T5 p5, T6 p6);

    public delegate void ActionOut1_2_3_4_6<T1, T2, T3, T4, in T5, T6>(out T1 p1, out T2 p2, out T3 p3, out T4 p4, T5 p5, out T6 p6);

    public delegate void ActionOut1_2_3_5_6<T1, T2, T3, in T4, T5, T6>(out T1 p1, out T2 p2, out T3 p3, T4 p4, out T5 p5, out T6 p6);

    public delegate void ActionOut1_2_4_5_6<T1, T2, in T3, T4, T5, T6>(out T1 p1, out T2 p2, T3 p3, out T4 p4, out T5 p5, out T6 p6);

    public delegate void ActionOut1_3_4_5_6<T1, in T2, T3, T4, T5, T6>(out T1 p1, T2 p2, out T3 p3, out T4 p4, out T5 p5, out T6 p6);

    public delegate void ActionOut2_3_4_5_6<in T1, T2, T3, T4, T5, T6>(T1 p1, out T2 p2, out T3 p3, out T4 p4, out T5 p5, out T6 p6);

    public delegate void ActionOut<T1, T2, T3, T4, T5, T6>(out T1 p1, out T2 p2, out T3 p3, out T4 p4, out T5 p5, out T6 p6);


    /* 7 param-group */

    public delegate void ActionOut1<T1, in T2, in T3, in T4, in T5, in T6, in T7>(out T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7);

    public delegate void ActionOut2<in T1, T2, in T3, in T4, in T5, in T6, in T7>(T1 p1, out T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7);

    public delegate void ActionOut3<in T1, in T2, T3, in T4, in T5, in T6, in T7>(T1 p1, T2 p2, out T3 p3, T4 p4, T5 p5, T6 p6, T7 p7);

    public delegate void ActionOut4<in T1, in T2, in T3, T4, in T5, in T6, in T7>(T1 p1, T2 p2, T3 p3, out T4 p4, T5 p5, T6 p6, T7 p7);

    public delegate void ActionOut5<in T1, in T2, in T3, in T4, T5, in T6, in T7>(T1 p1, T2 p2, T3 p3, T4 p4, out T5 p5, T6 p6, T7 p7);

    public delegate void ActionOut6<in T1, in T2, in T3, in T4, in T5, T6, in T7>(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, out T6 p6, T7 p7);

    public delegate void ActionOut7<in T1, in T2, in T3, in T4, in T5, in T6, T7>(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, out T7 p7);

    public delegate void ActionOut1_2<T1, T2, in T3, in T4, in T5, in T6, in T7>(out T1 p1, out T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7);

    public delegate void ActionOut1_3<T1, in T2, T3, in T4, in T5, in T6, in T7>(out T1 p1, T2 p2, out T3 p3, T4 p4, T5 p5, T6 p6, T7 p7);

    public delegate void ActionOut1_4<T1, in T2, in T3, T4, in T5, in T6, in T7>(out T1 p1, T2 p2, T3 p3, out T4 p4, T5 p5, T6 p6, T7 p7);

    public delegate void ActionOut1_5<T1, in T2, in T3, in T4, T5, in T6, in T7>(out T1 p1, T2 p2, T3 p3, T4 p4, out T5 p5, T6 p6, T7 p7);

    public delegate void ActionOut1_6<T1, in T2, in T3, in T4, in T5, T6, in T7>(out T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, out T6 p6, T7 p7);

    public delegate void ActionOut1_7<T1, in T2, in T3, in T4, in T5, in T6, T7>(out T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, out T7 p7);

    public delegate void ActionOut2_3<in T1, T2, T3, in T4, in T5, in T6, in T7>(T1 p1, out T2 p2, out T3 p3, T4 p4, T5 p5, T6 p6, T7 p7);

    public delegate void ActionOut2_4<in T1, T2, in T3, T4, in T5, in T6, in T7>(T1 p1, out T2 p2, T3 p3, out T4 p4, T5 p5, T6 p6, T7 p7);

    public delegate void ActionOut2_5<in T1, T2, in T3, in T4, T5, in T6, in T7>(T1 p1, out T2 p2, T3 p3, T4 p4, out T5 p5, T6 p6, T7 p7);

    public delegate void ActionOut2_6<in T1, T2, in T3, in T4, in T5, T6, in T7>(T1 p1, out T2 p2, T3 p3, T4 p4, T5 p5, out T6 p6, T7 p7);

    public delegate void ActionOut2_7<in T1, T2, in T3, in T4, in T5, in T6, T7>(T1 p1, out T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, out T7 p7);

    public delegate void ActionOut3_4<in T1, in T2, T3, T4, in T5, in T6, in T7>(T1 p1, T2 p2, out T3 p3, out T4 p4, T5 p5, T6 p6, T7 p7);

    public delegate void ActionOut3_5<in T1, in T2, T3, in T4, T5, in T6, in T7>(T1 p1, T2 p2, out T3 p3, T4 p4, out T5 p5, T6 p6, T7 p7);

    public delegate void ActionOut3_6<in T1, in T2, T3, in T4, in T5, T6, in T7>(T1 p1, T2 p2, out T3 p3, T4 p4, T5 p5, out T6 p6, T7 p7);

    public delegate void ActionOut3_7<in T1, in T2, T3, in T4, in T5, in T6, T7>(T1 p1, T2 p2, out T3 p3, T4 p4, T5 p5, T6 p6, out T7 p7);

    public delegate void ActionOut4_5<in T1, in T2, in T3, T4, T5, in T6, in T7>(T1 p1, T2 p2, T3 p3, out T4 p4, out T5 p5, T6 p6, T7 p7);

    public delegate void ActionOut4_6<in T1, in T2, in T3, T4, in T5, T6, in T7>(T1 p1, T2 p2, T3 p3, out T4 p4, T5 p5, out T6 p6, T7 p7);

    public delegate void ActionOut4_7<in T1, in T2, in T3, T4, in T5, in T6, T7>(T1 p1, T2 p2, T3 p3, out T4 p4, T5 p5, T6 p6, out T7 p7);

    public delegate void ActionOut5_6<in T1, in T2, in T3, in T4, T5, T6, in T7>(T1 p1, T2 p2, T3 p3, T4 p4, out T5 p5, out T6 p6, T7 p7);

    public delegate void ActionOut5_7<in T1, in T2, in T3, in T4, T5, in T6, T7>(T1 p1, T2 p2, T3 p3, T4 p4, out T5 p5, T6 p6, out T7 p7);

    public delegate void ActionOut6_7<in T1, in T2, in T3, in T4, in T5, T6, T7>(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, out T6 p6, out T7 p7);

    public delegate void ActionOut1_2_3<T1, T2, T3, in T4, in T5, in T6, in T7>(out T1 p1, out T2 p2, out T3 p3, T4 p4, T5 p5, T6 p6, T7 p7);

    public delegate void ActionOut1_2_4<T1, T2, in T3, T4, in T5, in T6, in T7>(out T1 p1, out T2 p2, T3 p3, out T4 p4, T5 p5, T6 p6, T7 p7);

    public delegate void ActionOut1_2_5<T1, T2, in T3, in T4, T5, in T6, in T7>(out T1 p1, out T2 p2, T3 p3, T4 p4, out T5 p5, T6 p6, T7 p7);

    public delegate void ActionOut1_2_6<T1, T2, in T3, in T4, in T5, T6, in T7>(out T1 p1, out T2 p2, T3 p3, T4 p4, T5 p5, out T6 p6, T7 p7);

    public delegate void ActionOut1_2_7<T1, T2, in T3, in T4, in T5, in T6, T7>(out T1 p1, out T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, out T7 p7);

    public delegate void ActionOut1_3_4<T1, in T2, T3, T4, in T5, in T6, in T7>(out T1 p1, T2 p2, out T3 p3, out T4 p4, T5 p5, T6 p6, T7 p7);

    public delegate void ActionOut1_3_5<T1, in T2, T3, in T4, T5, in T6, in T7>(out T1 p1, T2 p2, out T3 p3, T4 p4, out T5 p5, T6 p6, T7 p7);

    public delegate void ActionOut1_3_6<T1, in T2, T3, in T4, in T5, T6, in T7>(out T1 p1, T2 p2, out T3 p3, T4 p4, T5 p5, out T6 p6, T7 p7);

    public delegate void ActionOut1_3_7<T1, in T2, T3, in T4, in T5, in T6, T7>(out T1 p1, T2 p2, out T3 p3, T4 p4, T5 p5, T6 p6, out T7 p7);

    public delegate void ActionOut1_4_5<T1, in T2, in T3, T4, T5, in T6, in T7>(out T1 p1, T2 p2, T3 p3, out T4 p4, out T5 p5, T6 p6, T7 p7);

    public delegate void ActionOut1_4_6<T1, in T2, in T3, T4, in T5, T6, in T7>(out T1 p1, T2 p2, T3 p3, out T4 p4, T5 p5, out T6 p6, T7 p7);

    public delegate void ActionOut1_4_7<T1, in T2, in T3, T4, in T5, in T6, T7>(out T1 p1, T2 p2, T3 p3, out T4 p4, T5 p5, T6 p6, out T7 p7);

    public delegate void ActionOut1_5_6<T1, in T2, in T3, in T4, T5, T6, in T7>(out T1 p1, T2 p2, T3 p3, T4 p4, out T5 p5, out T6 p6, T7 p7);

    public delegate void ActionOut1_5_7<T1, in T2, in T3, in T4, T5, in T6, T7>(out T1 p1, T2 p2, T3 p3, T4 p4, out T5 p5, T6 p6, out T7 p7);

    public delegate void ActionOut1_6_7<T1, in T2, in T3, in T4, in T5, T6, T7>(out T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, out T6 p6, out T7 p7);

    public delegate void ActionOut2_3_4<in T1, T2, T3, T4, in T5, in T6, in T7>(T1 p1, out T2 p2, out T3 p3, out T4 p4, T5 p5, T6 p6, T7 p7);

    public delegate void ActionOut2_3_5<in T1, T2, T3, in T4, T5, in T6, in T7>(T1 p1, out T2 p2, out T3 p3, T4 p4, out T5 p5, T6 p6, T7 p7);

    public delegate void ActionOut2_3_6<in T1, T2, T3, in T4, in T5, T6, in T7>(T1 p1, out T2 p2, out T3 p3, T4 p4, T5 p5, out T6 p6, T7 p7);

    public delegate void ActionOut2_3_7<in T1, T2, T3, in T4, in T5, in T6, T7>(T1 p1, out T2 p2, out T3 p3, T4 p4, T5 p5, T6 p6, out T7 p7);

    public delegate void ActionOut2_4_5<in T1, T2, in T3, T4, T5, in T6, in T7>(T1 p1, out T2 p2, T3 p3, out T4 p4, out T5 p5, T6 p6, T7 p7);

    public delegate void ActionOut2_4_6<in T1, T2, in T3, T4, in T5, T6, in T7>(T1 p1, out T2 p2, T3 p3, out T4 p4, T5 p5, out T6 p6, T7 p7);

    public delegate void ActionOut2_4_7<in T1, T2, in T3, T4, in T5, in T6, T7>(T1 p1, out T2 p2, T3 p3, out T4 p4, T5 p5, T6 p6, out T7 p7);

    public delegate void ActionOut2_5_6<in T1, T2, in T3, in T4, T5, T6, in T7>(T1 p1, out T2 p2, T3 p3, T4 p4, out T5 p5, out T6 p6, T7 p7);

    public delegate void ActionOut2_5_7<in T1, T2, in T3, in T4, T5, in T6, T7>(T1 p1, out T2 p2, T3 p3, T4 p4, out T5 p5, T6 p6, out T7 p7);

    public delegate void ActionOut2_6_7<in T1, T2, in T3, in T4, in T5, T6, T7>(T1 p1, out T2 p2, T3 p3, T4 p4, T5 p5, out T6 p6, out T7 p7);

    public delegate void ActionOut3_4_5<in T1, in T2, T3, T4, T5, in T6, in T7>(T1 p1, T2 p2, out T3 p3, out T4 p4, out T5 p5, T6 p6, T7 p7);

    public delegate void ActionOut3_4_6<in T1, in T2, T3, T4, in T5, T6, in T7>(T1 p1, T2 p2, out T3 p3, out T4 p4, T5 p5, out T6 p6, T7 p7);

    public delegate void ActionOut3_4_7<in T1, in T2, T3, T4, in T5, in T6, T7>(T1 p1, T2 p2, out T3 p3, out T4 p4, T5 p5, T6 p6, out T7 p7);

    public delegate void ActionOut3_5_6<in T1, in T2, T3, in T4, T5, T6, in T7>(T1 p1, T2 p2, out T3 p3, T4 p4, out T5 p5, out T6 p6, T7 p7);

    public delegate void ActionOut3_5_7<in T1, in T2, T3, in T4, T5, in T6, T7>(T1 p1, T2 p2, out T3 p3, T4 p4, out T5 p5, T6 p6, out T7 p7);

    public delegate void ActionOut3_6_7<in T1, in T2, T3, in T4, in T5, T6, T7>(T1 p1, T2 p2, out T3 p3, T4 p4, T5 p5, out T6 p6, out T7 p7);

    public delegate void ActionOut4_5_6<in T1, in T2, in T3, T4, T5, T6, in T7>(T1 p1, T2 p2, T3 p3, out T4 p4, out T5 p5, out T6 p6, T7 p7);

    public delegate void ActionOut4_5_7<in T1, in T2, in T3, T4, T5, in T6, T7>(T1 p1, T2 p2, T3 p3, out T4 p4, out T5 p5, T6 p6, out T7 p7);

    public delegate void ActionOut4_6_7<in T1, in T2, in T3, T4, in T5, T6, T7>(T1 p1, T2 p2, T3 p3, out T4 p4, T5 p5, out T6 p6, out T7 p7);

    public delegate void ActionOut5_6_7<in T1, in T2, in T3, in T4, T5, T6, T7>(T1 p1, T2 p2, T3 p3, T4 p4, out T5 p5, out T6 p6, out T7 p7);

    public delegate void ActionOut1_2_3_4<T1, T2, T3, T4, in T5, in T6, in T7>(out T1 p1, out T2 p2, out T3 p3, out T4 p4, T5 p5, T6 p6, T7 p7);

    public delegate void ActionOut1_2_3_5<T1, T2, T3, in T4, T5, in T6, in T7>(out T1 p1, out T2 p2, out T3 p3, T4 p4, out T5 p5, T6 p6, T7 p7);

    public delegate void ActionOut1_2_3_6<T1, T2, T3, in T4, in T5, T6, in T7>(out T1 p1, out T2 p2, out T3 p3, T4 p4, T5 p5, out T6 p6, T7 p7);

    public delegate void ActionOut1_2_3_7<T1, T2, T3, in T4, in T5, in T6, T7>(out T1 p1, out T2 p2, out T3 p3, T4 p4, T5 p5, T6 p6, out T7 p7);

    public delegate void ActionOut1_2_4_5<T1, T2, in T3, T4, T5, in T6, in T7>(out T1 p1, out T2 p2, T3 p3, out T4 p4, out T5 p5, T6 p6, T7 p7);

    public delegate void ActionOut1_2_4_6<T1, T2, in T3, T4, in T5, T6, in T7>(out T1 p1, out T2 p2, T3 p3, out T4 p4, T5 p5, out T6 p6, T7 p7);

    public delegate void ActionOut1_2_4_7<T1, T2, in T3, T4, in T5, in T6, T7>(out T1 p1, out T2 p2, T3 p3, out T4 p4, T5 p5, T6 p6, out T7 p7);

    public delegate void ActionOut1_2_5_6<T1, T2, in T3, in T4, T5, T6, in T7>(out T1 p1, out T2 p2, T3 p3, T4 p4, out T5 p5, out T6 p6, T7 p7);

    public delegate void ActionOut1_2_5_7<T1, T2, in T3, in T4, T5, in T6, T7>(out T1 p1, out T2 p2, T3 p3, T4 p4, out T5 p5, T6 p6, out T7 p7);

    public delegate void ActionOut1_2_6_7<T1, T2, in T3, in T4, in T5, T6, T7>(out T1 p1, out T2 p2, T3 p3, T4 p4, T5 p5, out T6 p6, out T7 p7);

    public delegate void ActionOut1_3_4_5<T1, in T2, T3, T4, T5, in T6, in T7>(out T1 p1, T2 p2, out T3 p3, out T4 p4, out T5 p5, T6 p6, T7 p7);

    public delegate void ActionOut1_3_4_6<T1, in T2, T3, T4, in T5, T6, in T7>(out T1 p1, T2 p2, out T3 p3, out T4 p4, T5 p5, out T6 p6, T7 p7);

    public delegate void ActionOut1_3_4_7<T1, in T2, T3, T4, in T5, in T6, T7>(out T1 p1, T2 p2, out T3 p3, out T4 p4, T5 p5, T6 p6, out T7 p7);

    public delegate void ActionOut1_3_5_6<T1, in T2, T3, in T4, T5, T6, in T7>(out T1 p1, T2 p2, out T3 p3, T4 p4, out T5 p5, out T6 p6, T7 p7);

    public delegate void ActionOut1_3_5_7<T1, in T2, T3, in T4, T5, in T6, T7>(out T1 p1, T2 p2, out T3 p3, T4 p4, out T5 p5, T6 p6, out T7 p7);

    public delegate void ActionOut1_3_6_7<T1, in T2, T3, in T4, in T5, T6, T7>(out T1 p1, T2 p2, out T3 p3, T4 p4, T5 p5, out T6 p6, out T7 p7);

    public delegate void ActionOut1_4_5_6<T1, in T2, in T3, T4, T5, T6, in T7>(out T1 p1, T2 p2, T3 p3, out T4 p4, out T5 p5, out T6 p6, T7 p7);

    public delegate void ActionOut1_4_5_7<T1, in T2, in T3, T4, T5, in T6, T7>(out T1 p1, T2 p2, T3 p3, out T4 p4, out T5 p5, T6 p6, out T7 p7);

    public delegate void ActionOut1_4_6_7<T1, in T2, in T3, T4, in T5, T6, T7>(out T1 p1, T2 p2, T3 p3, out T4 p4, T5 p5, out T6 p6, out T7 p7);

    public delegate void ActionOut1_5_6_7<T1, in T2, in T3, in T4, T5, T6, T7>(out T1 p1, T2 p2, T3 p3, T4 p4, out T5 p5, out T6 p6, out T7 p7);

    public delegate void ActionOut2_3_4_5<in T1, T2, T3, T4, T5, in T6, in T7>(T1 p1, out T2 p2, out T3 p3, out T4 p4, out T5 p5, T6 p6, T7 p7);

    public delegate void ActionOut2_3_4_6<in T1, T2, T3, T4, in T5, T6, in T7>(T1 p1, out T2 p2, out T3 p3, out T4 p4, T5 p5, out T6 p6, T7 p7);

    public delegate void ActionOut2_3_4_7<in T1, T2, T3, T4, in T5, in T6, T7>(T1 p1, out T2 p2, out T3 p3, out T4 p4, T5 p5, T6 p6, out T7 p7);

    public delegate void ActionOut2_3_5_6<in T1, T2, T3, in T4, T5, T6, in T7>(T1 p1, out T2 p2, out T3 p3, T4 p4, out T5 p5, out T6 p6, T7 p7);

    public delegate void ActionOut2_3_5_7<in T1, T2, T3, in T4, T5, in T6, T7>(T1 p1, out T2 p2, out T3 p3, T4 p4, out T5 p5, T6 p6, out T7 p7);

    public delegate void ActionOut2_3_6_7<in T1, T2, T3, in T4, in T5, T6, T7>(T1 p1, out T2 p2, out T3 p3, T4 p4, T5 p5, out T6 p6, out T7 p7);

    public delegate void ActionOut2_4_5_6<in T1, T2, in T3, T4, T5, T6, in T7>(T1 p1, out T2 p2, T3 p3, out T4 p4, out T5 p5, out T6 p6, T7 p7);

    public delegate void ActionOut2_4_5_7<in T1, T2, in T3, T4, T5, in T6, T7>(T1 p1, out T2 p2, T3 p3, out T4 p4, out T5 p5, T6 p6, out T7 p7);

    public delegate void ActionOut2_4_6_7<in T1, T2, in T3, T4, in T5, T6, T7>(T1 p1, out T2 p2, T3 p3, out T4 p4, T5 p5, out T6 p6, out T7 p7);

    public delegate void ActionOut2_5_6_7<in T1, T2, in T3, in T4, T5, T6, T7>(T1 p1, out T2 p2, T3 p3, T4 p4, out T5 p5, out T6 p6, out T7 p7);

    public delegate void ActionOut3_4_5_6<in T1, in T2, T3, T4, T5, T6, in T7>(T1 p1, T2 p2, out T3 p3, out T4 p4, out T5 p5, out T6 p6, T7 p7);

    public delegate void ActionOut3_4_5_7<in T1, in T2, T3, T4, T5, in T6, T7>(T1 p1, T2 p2, out T3 p3, out T4 p4, out T5 p5, T6 p6, out T7 p7);

    public delegate void ActionOut3_4_6_7<in T1, in T2, T3, T4, in T5, T6, T7>(T1 p1, T2 p2, out T3 p3, out T4 p4, T5 p5, out T6 p6, out T7 p7);

    public delegate void ActionOut3_5_6_7<in T1, in T2, T3, in T4, T5, T6, T7>(T1 p1, T2 p2, out T3 p3, T4 p4, out T5 p5, out T6 p6, out T7 p7);

    public delegate void ActionOut4_5_6_7<in T1, in T2, in T3, T4, T5, T6, T7>(T1 p1, T2 p2, T3 p3, out T4 p4, out T5 p5, out T6 p6, out T7 p7);

    public delegate void ActionOut1_2_3_4_5<T1, T2, T3, T4, T5, in T6, in T7>(out T1 p1, out T2 p2, out T3 p3, out T4 p4, out T5 p5, T6 p6, T7 p7);

    public delegate void ActionOut1_2_3_4_6<T1, T2, T3, T4, in T5, T6, in T7>(out T1 p1, out T2 p2, out T3 p3, out T4 p4, T5 p5, out T6 p6, T7 p7);

    public delegate void ActionOut1_2_3_4_7<T1, T2, T3, T4, in T5, in T6, T7>(out T1 p1, out T2 p2, out T3 p3, out T4 p4, T5 p5, T6 p6, out T7 p7);

    public delegate void ActionOut1_2_3_5_6<T1, T2, T3, in T4, T5, T6, in T7>(out T1 p1, out T2 p2, out T3 p3, T4 p4, out T5 p5, out T6 p6, T7 p7);

    public delegate void ActionOut1_2_3_5_7<T1, T2, T3, in T4, T5, in T6, T7>(out T1 p1, out T2 p2, out T3 p3, T4 p4, out T5 p5, T6 p6, out T7 p7);

    public delegate void ActionOut1_2_3_6_7<T1, T2, T3, in T4, in T5, T6, T7>(out T1 p1, out T2 p2, out T3 p3, T4 p4, T5 p5, out T6 p6, out T7 p7);

    public delegate void ActionOut1_2_4_5_6<T1, T2, in T3, T4, T5, T6, in T7>(out T1 p1, out T2 p2, T3 p3, out T4 p4, out T5 p5, out T6 p6, T7 p7);

    public delegate void ActionOut1_2_4_5_7<T1, T2, in T3, T4, T5, in T6, T7>(out T1 p1, out T2 p2, T3 p3, out T4 p4, out T5 p5, T6 p6, out T7 p7);

    public delegate void ActionOut1_2_4_6_7<T1, T2, in T3, T4, in T5, T6, T7>(out T1 p1, out T2 p2, T3 p3, out T4 p4, T5 p5, out T6 p6, out T7 p7);

    public delegate void ActionOut1_2_5_6_7<T1, T2, in T3, in T4, T5, T6, T7>(out T1 p1, out T2 p2, T3 p3, T4 p4, out T5 p5, out T6 p6, out T7 p7);

    public delegate void ActionOut1_3_4_5_6<T1, in T2, T3, T4, T5, T6, in T7>(out T1 p1, T2 p2, out T3 p3, out T4 p4, out T5 p5, out T6 p6, T7 p7);

    public delegate void ActionOut1_3_4_5_7<T1, in T2, T3, T4, T5, in T6, T7>(out T1 p1, T2 p2, out T3 p3, out T4 p4, out T5 p5, T6 p6, out T7 p7);

    public delegate void ActionOut1_3_4_6_7<T1, in T2, T3, T4, in T5, T6, T7>(out T1 p1, T2 p2, out T3 p3, out T4 p4, T5 p5, out T6 p6, out T7 p7);

    public delegate void ActionOut1_3_5_6_7<T1, in T2, T3, in T4, T5, T6, T7>(out T1 p1, T2 p2, out T3 p3, T4 p4, out T5 p5, out T6 p6, out T7 p7);

    public delegate void ActionOut1_4_5_6_7<T1, in T2, in T3, T4, T5, T6, T7>(out T1 p1, T2 p2, T3 p3, out T4 p4, out T5 p5, out T6 p6, out T7 p7);

    public delegate void ActionOut2_3_4_5_6<in T1, T2, T3, T4, T5, T6, in T7>(T1 p1, out T2 p2, out T3 p3, out T4 p4, out T5 p5, out T6 p6, T7 p7);

    public delegate void ActionOut2_3_4_5_7<in T1, T2, T3, T4, T5, in T6, T7>(T1 p1, out T2 p2, out T3 p3, out T4 p4, out T5 p5, T6 p6, out T7 p7);

    public delegate void ActionOut2_3_4_6_7<in T1, T2, T3, T4, in T5, T6, T7>(T1 p1, out T2 p2, out T3 p3, out T4 p4, T5 p5, out T6 p6, out T7 p7);

    public delegate void ActionOut2_3_5_6_7<in T1, T2, T3, in T4, T5, T6, T7>(T1 p1, out T2 p2, out T3 p3, T4 p4, out T5 p5, out T6 p6, out T7 p7);

    public delegate void ActionOut2_4_5_6_7<in T1, T2, in T3, T4, T5, T6, T7>(T1 p1, out T2 p2, T3 p3, out T4 p4, out T5 p5, out T6 p6, out T7 p7);

    public delegate void ActionOut3_4_5_6_7<in T1, in T2, T3, T4, T5, T6, T7>(T1 p1, T2 p2, out T3 p3, out T4 p4, out T5 p5, out T6 p6, out T7 p7);

    public delegate void ActionOut1_2_3_4_5_6<T1, T2, T3, T4, T5, T6, in T7>(out T1 p1, out T2 p2, out T3 p3, out T4 p4, out T5 p5, out T6 p6, T7 p7);

    public delegate void ActionOut1_2_3_4_5_7<T1, T2, T3, T4, T5, in T6, T7>(out T1 p1, out T2 p2, out T3 p3, out T4 p4, out T5 p5, T6 p6, out T7 p7);

    public delegate void ActionOut1_2_3_4_6_7<T1, T2, T3, T4, in T5, T6, T7>(out T1 p1, out T2 p2, out T3 p3, out T4 p4, T5 p5, out T6 p6, out T7 p7);

    public delegate void ActionOut1_2_3_5_6_7<T1, T2, T3, in T4, T5, T6, T7>(out T1 p1, out T2 p2, out T3 p3, T4 p4, out T5 p5, out T6 p6, out T7 p7);

    public delegate void ActionOut1_2_4_5_6_7<T1, T2, in T3, T4, T5, T6, T7>(out T1 p1, out T2 p2, T3 p3, out T4 p4, out T5 p5, out T6 p6, out T7 p7);

    public delegate void ActionOut1_3_4_5_6_7<T1, in T2, T3, T4, T5, T6, T7>(out T1 p1, T2 p2, out T3 p3, out T4 p4, out T5 p5, out T6 p6, out T7 p7);

    public delegate void ActionOut2_3_4_5_6_7<in T1, T2, T3, T4, T5, T6, T7>(T1 p1, out T2 p2, out T3 p3, out T4 p4, out T5 p5, out T6 p6, out T7 p7);

    public delegate void ActionOut<T1, T2, T3, T4, T5, T6, T7>(out T1 p1, out T2 p2, out T3 p3, out T4 p4, out T5 p5, out T6 p6, out T7 p7);

    /* end-of-group */
}
