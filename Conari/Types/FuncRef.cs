/*
 * The MIT License (MIT)
 * 
 * Copyright (c) 2016  Denis Kuzmin <entry.reg@gmail.com>
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

    public delegate TRes FuncRef1<T1, in T2, in T3, out TRes>(ref T1 p1, T2 p2, T3 p3);

    public delegate TRes FuncRef2<in T1, T2, in T3, out TRes>(T1 p1, ref T2 p2, T3 p3);

    public delegate TRes FuncRef3<in T1, in T2, T3, out TRes>(T1 p1, T2 p2, ref T3 p3);

    public delegate TRes FuncRef1_2<T1, T2, in T3, out TRes>(ref T1 p1, ref T2 p2, T3 p3);

    public delegate TRes FuncRef1_3<T1, in T2, T3, out TRes>(ref T1 p1, T2 p2, ref T3 p3);

    public delegate TRes FuncRef2_3<in T1, T2, T3, out TRes>(T1 p1, ref T2 p2, ref T3 p3);

    public delegate TRes FuncRef<T1, T2, T3, out TRes>(ref T1 p1, ref T2 p2, ref T3 p3);


    /* 4 param-group */

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


    /* 5 param-group */

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


    /* 6 param-group */

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


    /* 7 param-group */

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


    /* 8 param-group */

    public delegate TRes FuncRef1<T1, in T2, in T3, in T4, in T5, in T6, in T7, in T8, out TRes>(ref T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8);

    public delegate TRes FuncRef2<in T1, T2, in T3, in T4, in T5, in T6, in T7, in T8, out TRes>(T1 p1, ref T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8);

    public delegate TRes FuncRef3<in T1, in T2, T3, in T4, in T5, in T6, in T7, in T8, out TRes>(T1 p1, T2 p2, ref T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8);

    public delegate TRes FuncRef4<in T1, in T2, in T3, T4, in T5, in T6, in T7, in T8, out TRes>(T1 p1, T2 p2, T3 p3, ref T4 p4, T5 p5, T6 p6, T7 p7, T8 p8);

    public delegate TRes FuncRef5<in T1, in T2, in T3, in T4, T5, in T6, in T7, in T8, out TRes>(T1 p1, T2 p2, T3 p3, T4 p4, ref T5 p5, T6 p6, T7 p7, T8 p8);

    public delegate TRes FuncRef6<in T1, in T2, in T3, in T4, in T5, T6, in T7, in T8, out TRes>(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, ref T6 p6, T7 p7, T8 p8);

    public delegate TRes FuncRef7<in T1, in T2, in T3, in T4, in T5, in T6, T7, in T8, out TRes>(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, ref T7 p7, T8 p8);

    public delegate TRes FuncRef8<in T1, in T2, in T3, in T4, in T5, in T6, in T7, T8, out TRes>(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, ref T8 p8);

    public delegate TRes FuncRef1_2<T1, T2, in T3, in T4, in T5, in T6, in T7, in T8, out TRes>(ref T1 p1, ref T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8);

    public delegate TRes FuncRef1_3<T1, in T2, T3, in T4, in T5, in T6, in T7, in T8, out TRes>(ref T1 p1, T2 p2, ref T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8);

    public delegate TRes FuncRef1_4<T1, in T2, in T3, T4, in T5, in T6, in T7, in T8, out TRes>(ref T1 p1, T2 p2, T3 p3, ref T4 p4, T5 p5, T6 p6, T7 p7, T8 p8);

    public delegate TRes FuncRef1_5<T1, in T2, in T3, in T4, T5, in T6, in T7, in T8, out TRes>(ref T1 p1, T2 p2, T3 p3, T4 p4, ref T5 p5, T6 p6, T7 p7, T8 p8);

    public delegate TRes FuncRef1_6<T1, in T2, in T3, in T4, in T5, T6, in T7, in T8, out TRes>(ref T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, ref T6 p6, T7 p7, T8 p8);

    public delegate TRes FuncRef1_7<T1, in T2, in T3, in T4, in T5, in T6, T7, in T8, out TRes>(ref T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, ref T7 p7, T8 p8);

    public delegate TRes FuncRef1_8<T1, in T2, in T3, in T4, in T5, in T6, in T7, T8, out TRes>(ref T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, ref T8 p8);

    public delegate TRes FuncRef2_3<in T1, T2, T3, in T4, in T5, in T6, in T7, in T8, out TRes>(T1 p1, ref T2 p2, ref T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8);

    public delegate TRes FuncRef2_4<in T1, T2, in T3, T4, in T5, in T6, in T7, in T8, out TRes>(T1 p1, ref T2 p2, T3 p3, ref T4 p4, T5 p5, T6 p6, T7 p7, T8 p8);

    public delegate TRes FuncRef2_5<in T1, T2, in T3, in T4, T5, in T6, in T7, in T8, out TRes>(T1 p1, ref T2 p2, T3 p3, T4 p4, ref T5 p5, T6 p6, T7 p7, T8 p8);

    public delegate TRes FuncRef2_6<in T1, T2, in T3, in T4, in T5, T6, in T7, in T8, out TRes>(T1 p1, ref T2 p2, T3 p3, T4 p4, T5 p5, ref T6 p6, T7 p7, T8 p8);

    public delegate TRes FuncRef2_7<in T1, T2, in T3, in T4, in T5, in T6, T7, in T8, out TRes>(T1 p1, ref T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, ref T7 p7, T8 p8);

    public delegate TRes FuncRef2_8<in T1, T2, in T3, in T4, in T5, in T6, in T7, T8, out TRes>(T1 p1, ref T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, ref T8 p8);

    public delegate TRes FuncRef3_4<in T1, in T2, T3, T4, in T5, in T6, in T7, in T8, out TRes>(T1 p1, T2 p2, ref T3 p3, ref T4 p4, T5 p5, T6 p6, T7 p7, T8 p8);

    public delegate TRes FuncRef3_5<in T1, in T2, T3, in T4, T5, in T6, in T7, in T8, out TRes>(T1 p1, T2 p2, ref T3 p3, T4 p4, ref T5 p5, T6 p6, T7 p7, T8 p8);

    public delegate TRes FuncRef3_6<in T1, in T2, T3, in T4, in T5, T6, in T7, in T8, out TRes>(T1 p1, T2 p2, ref T3 p3, T4 p4, T5 p5, ref T6 p6, T7 p7, T8 p8);

    public delegate TRes FuncRef3_7<in T1, in T2, T3, in T4, in T5, in T6, T7, in T8, out TRes>(T1 p1, T2 p2, ref T3 p3, T4 p4, T5 p5, T6 p6, ref T7 p7, T8 p8);

    public delegate TRes FuncRef3_8<in T1, in T2, T3, in T4, in T5, in T6, in T7, T8, out TRes>(T1 p1, T2 p2, ref T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, ref T8 p8);

    public delegate TRes FuncRef4_5<in T1, in T2, in T3, T4, T5, in T6, in T7, in T8, out TRes>(T1 p1, T2 p2, T3 p3, ref T4 p4, ref T5 p5, T6 p6, T7 p7, T8 p8);

    public delegate TRes FuncRef4_6<in T1, in T2, in T3, T4, in T5, T6, in T7, in T8, out TRes>(T1 p1, T2 p2, T3 p3, ref T4 p4, T5 p5, ref T6 p6, T7 p7, T8 p8);

    public delegate TRes FuncRef4_7<in T1, in T2, in T3, T4, in T5, in T6, T7, in T8, out TRes>(T1 p1, T2 p2, T3 p3, ref T4 p4, T5 p5, T6 p6, ref T7 p7, T8 p8);

    public delegate TRes FuncRef4_8<in T1, in T2, in T3, T4, in T5, in T6, in T7, T8, out TRes>(T1 p1, T2 p2, T3 p3, ref T4 p4, T5 p5, T6 p6, T7 p7, ref T8 p8);

    public delegate TRes FuncRef5_6<in T1, in T2, in T3, in T4, T5, T6, in T7, in T8, out TRes>(T1 p1, T2 p2, T3 p3, T4 p4, ref T5 p5, ref T6 p6, T7 p7, T8 p8);

    public delegate TRes FuncRef5_7<in T1, in T2, in T3, in T4, T5, in T6, T7, in T8, out TRes>(T1 p1, T2 p2, T3 p3, T4 p4, ref T5 p5, T6 p6, ref T7 p7, T8 p8);

    public delegate TRes FuncRef5_8<in T1, in T2, in T3, in T4, T5, in T6, in T7, T8, out TRes>(T1 p1, T2 p2, T3 p3, T4 p4, ref T5 p5, T6 p6, T7 p7, ref T8 p8);

    public delegate TRes FuncRef6_7<in T1, in T2, in T3, in T4, in T5, T6, T7, in T8, out TRes>(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, ref T6 p6, ref T7 p7, T8 p8);

    public delegate TRes FuncRef6_8<in T1, in T2, in T3, in T4, in T5, T6, in T7, T8, out TRes>(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, ref T6 p6, T7 p7, ref T8 p8);

    public delegate TRes FuncRef7_8<in T1, in T2, in T3, in T4, in T5, in T6, T7, T8, out TRes>(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, ref T7 p7, ref T8 p8);

    public delegate TRes FuncRef1_2_3<T1, T2, T3, in T4, in T5, in T6, in T7, in T8, out TRes>(ref T1 p1, ref T2 p2, ref T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8);

    public delegate TRes FuncRef1_2_4<T1, T2, in T3, T4, in T5, in T6, in T7, in T8, out TRes>(ref T1 p1, ref T2 p2, T3 p3, ref T4 p4, T5 p5, T6 p6, T7 p7, T8 p8);

    public delegate TRes FuncRef1_2_5<T1, T2, in T3, in T4, T5, in T6, in T7, in T8, out TRes>(ref T1 p1, ref T2 p2, T3 p3, T4 p4, ref T5 p5, T6 p6, T7 p7, T8 p8);

    public delegate TRes FuncRef1_2_6<T1, T2, in T3, in T4, in T5, T6, in T7, in T8, out TRes>(ref T1 p1, ref T2 p2, T3 p3, T4 p4, T5 p5, ref T6 p6, T7 p7, T8 p8);

    public delegate TRes FuncRef1_2_7<T1, T2, in T3, in T4, in T5, in T6, T7, in T8, out TRes>(ref T1 p1, ref T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, ref T7 p7, T8 p8);

    public delegate TRes FuncRef1_2_8<T1, T2, in T3, in T4, in T5, in T6, in T7, T8, out TRes>(ref T1 p1, ref T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, ref T8 p8);

    public delegate TRes FuncRef1_3_4<T1, in T2, T3, T4, in T5, in T6, in T7, in T8, out TRes>(ref T1 p1, T2 p2, ref T3 p3, ref T4 p4, T5 p5, T6 p6, T7 p7, T8 p8);

    public delegate TRes FuncRef1_3_5<T1, in T2, T3, in T4, T5, in T6, in T7, in T8, out TRes>(ref T1 p1, T2 p2, ref T3 p3, T4 p4, ref T5 p5, T6 p6, T7 p7, T8 p8);

    public delegate TRes FuncRef1_3_6<T1, in T2, T3, in T4, in T5, T6, in T7, in T8, out TRes>(ref T1 p1, T2 p2, ref T3 p3, T4 p4, T5 p5, ref T6 p6, T7 p7, T8 p8);

    public delegate TRes FuncRef1_3_7<T1, in T2, T3, in T4, in T5, in T6, T7, in T8, out TRes>(ref T1 p1, T2 p2, ref T3 p3, T4 p4, T5 p5, T6 p6, ref T7 p7, T8 p8);

    public delegate TRes FuncRef1_3_8<T1, in T2, T3, in T4, in T5, in T6, in T7, T8, out TRes>(ref T1 p1, T2 p2, ref T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, ref T8 p8);

    public delegate TRes FuncRef1_4_5<T1, in T2, in T3, T4, T5, in T6, in T7, in T8, out TRes>(ref T1 p1, T2 p2, T3 p3, ref T4 p4, ref T5 p5, T6 p6, T7 p7, T8 p8);

    public delegate TRes FuncRef1_4_6<T1, in T2, in T3, T4, in T5, T6, in T7, in T8, out TRes>(ref T1 p1, T2 p2, T3 p3, ref T4 p4, T5 p5, ref T6 p6, T7 p7, T8 p8);

    public delegate TRes FuncRef1_4_7<T1, in T2, in T3, T4, in T5, in T6, T7, in T8, out TRes>(ref T1 p1, T2 p2, T3 p3, ref T4 p4, T5 p5, T6 p6, ref T7 p7, T8 p8);

    public delegate TRes FuncRef1_4_8<T1, in T2, in T3, T4, in T5, in T6, in T7, T8, out TRes>(ref T1 p1, T2 p2, T3 p3, ref T4 p4, T5 p5, T6 p6, T7 p7, ref T8 p8);

    public delegate TRes FuncRef1_5_6<T1, in T2, in T3, in T4, T5, T6, in T7, in T8, out TRes>(ref T1 p1, T2 p2, T3 p3, T4 p4, ref T5 p5, ref T6 p6, T7 p7, T8 p8);

    public delegate TRes FuncRef1_5_7<T1, in T2, in T3, in T4, T5, in T6, T7, in T8, out TRes>(ref T1 p1, T2 p2, T3 p3, T4 p4, ref T5 p5, T6 p6, ref T7 p7, T8 p8);

    public delegate TRes FuncRef1_5_8<T1, in T2, in T3, in T4, T5, in T6, in T7, T8, out TRes>(ref T1 p1, T2 p2, T3 p3, T4 p4, ref T5 p5, T6 p6, T7 p7, ref T8 p8);

    public delegate TRes FuncRef1_6_7<T1, in T2, in T3, in T4, in T5, T6, T7, in T8, out TRes>(ref T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, ref T6 p6, ref T7 p7, T8 p8);

    public delegate TRes FuncRef1_6_8<T1, in T2, in T3, in T4, in T5, T6, in T7, T8, out TRes>(ref T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, ref T6 p6, T7 p7, ref T8 p8);

    public delegate TRes FuncRef1_7_8<T1, in T2, in T3, in T4, in T5, in T6, T7, T8, out TRes>(ref T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, ref T7 p7, ref T8 p8);

    public delegate TRes FuncRef2_3_4<in T1, T2, T3, T4, in T5, in T6, in T7, in T8, out TRes>(T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, T5 p5, T6 p6, T7 p7, T8 p8);

    public delegate TRes FuncRef2_3_5<in T1, T2, T3, in T4, T5, in T6, in T7, in T8, out TRes>(T1 p1, ref T2 p2, ref T3 p3, T4 p4, ref T5 p5, T6 p6, T7 p7, T8 p8);

    public delegate TRes FuncRef2_3_6<in T1, T2, T3, in T4, in T5, T6, in T7, in T8, out TRes>(T1 p1, ref T2 p2, ref T3 p3, T4 p4, T5 p5, ref T6 p6, T7 p7, T8 p8);

    public delegate TRes FuncRef2_3_7<in T1, T2, T3, in T4, in T5, in T6, T7, in T8, out TRes>(T1 p1, ref T2 p2, ref T3 p3, T4 p4, T5 p5, T6 p6, ref T7 p7, T8 p8);

    public delegate TRes FuncRef2_3_8<in T1, T2, T3, in T4, in T5, in T6, in T7, T8, out TRes>(T1 p1, ref T2 p2, ref T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, ref T8 p8);

    public delegate TRes FuncRef2_4_5<in T1, T2, in T3, T4, T5, in T6, in T7, in T8, out TRes>(T1 p1, ref T2 p2, T3 p3, ref T4 p4, ref T5 p5, T6 p6, T7 p7, T8 p8);

    public delegate TRes FuncRef2_4_6<in T1, T2, in T3, T4, in T5, T6, in T7, in T8, out TRes>(T1 p1, ref T2 p2, T3 p3, ref T4 p4, T5 p5, ref T6 p6, T7 p7, T8 p8);

    public delegate TRes FuncRef2_4_7<in T1, T2, in T3, T4, in T5, in T6, T7, in T8, out TRes>(T1 p1, ref T2 p2, T3 p3, ref T4 p4, T5 p5, T6 p6, ref T7 p7, T8 p8);

    public delegate TRes FuncRef2_4_8<in T1, T2, in T3, T4, in T5, in T6, in T7, T8, out TRes>(T1 p1, ref T2 p2, T3 p3, ref T4 p4, T5 p5, T6 p6, T7 p7, ref T8 p8);

    public delegate TRes FuncRef2_5_6<in T1, T2, in T3, in T4, T5, T6, in T7, in T8, out TRes>(T1 p1, ref T2 p2, T3 p3, T4 p4, ref T5 p5, ref T6 p6, T7 p7, T8 p8);

    public delegate TRes FuncRef2_5_7<in T1, T2, in T3, in T4, T5, in T6, T7, in T8, out TRes>(T1 p1, ref T2 p2, T3 p3, T4 p4, ref T5 p5, T6 p6, ref T7 p7, T8 p8);

    public delegate TRes FuncRef2_5_8<in T1, T2, in T3, in T4, T5, in T6, in T7, T8, out TRes>(T1 p1, ref T2 p2, T3 p3, T4 p4, ref T5 p5, T6 p6, T7 p7, ref T8 p8);

    public delegate TRes FuncRef2_6_7<in T1, T2, in T3, in T4, in T5, T6, T7, in T8, out TRes>(T1 p1, ref T2 p2, T3 p3, T4 p4, T5 p5, ref T6 p6, ref T7 p7, T8 p8);

    public delegate TRes FuncRef2_6_8<in T1, T2, in T3, in T4, in T5, T6, in T7, T8, out TRes>(T1 p1, ref T2 p2, T3 p3, T4 p4, T5 p5, ref T6 p6, T7 p7, ref T8 p8);

    public delegate TRes FuncRef2_7_8<in T1, T2, in T3, in T4, in T5, in T6, T7, T8, out TRes>(T1 p1, ref T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, ref T7 p7, ref T8 p8);

    public delegate TRes FuncRef3_4_5<in T1, in T2, T3, T4, T5, in T6, in T7, in T8, out TRes>(T1 p1, T2 p2, ref T3 p3, ref T4 p4, ref T5 p5, T6 p6, T7 p7, T8 p8);

    public delegate TRes FuncRef3_4_6<in T1, in T2, T3, T4, in T5, T6, in T7, in T8, out TRes>(T1 p1, T2 p2, ref T3 p3, ref T4 p4, T5 p5, ref T6 p6, T7 p7, T8 p8);

    public delegate TRes FuncRef3_4_7<in T1, in T2, T3, T4, in T5, in T6, T7, in T8, out TRes>(T1 p1, T2 p2, ref T3 p3, ref T4 p4, T5 p5, T6 p6, ref T7 p7, T8 p8);

    public delegate TRes FuncRef3_4_8<in T1, in T2, T3, T4, in T5, in T6, in T7, T8, out TRes>(T1 p1, T2 p2, ref T3 p3, ref T4 p4, T5 p5, T6 p6, T7 p7, ref T8 p8);

    public delegate TRes FuncRef3_5_6<in T1, in T2, T3, in T4, T5, T6, in T7, in T8, out TRes>(T1 p1, T2 p2, ref T3 p3, T4 p4, ref T5 p5, ref T6 p6, T7 p7, T8 p8);

    public delegate TRes FuncRef3_5_7<in T1, in T2, T3, in T4, T5, in T6, T7, in T8, out TRes>(T1 p1, T2 p2, ref T3 p3, T4 p4, ref T5 p5, T6 p6, ref T7 p7, T8 p8);

    public delegate TRes FuncRef3_5_8<in T1, in T2, T3, in T4, T5, in T6, in T7, T8, out TRes>(T1 p1, T2 p2, ref T3 p3, T4 p4, ref T5 p5, T6 p6, T7 p7, ref T8 p8);

    public delegate TRes FuncRef3_6_7<in T1, in T2, T3, in T4, in T5, T6, T7, in T8, out TRes>(T1 p1, T2 p2, ref T3 p3, T4 p4, T5 p5, ref T6 p6, ref T7 p7, T8 p8);

    public delegate TRes FuncRef3_6_8<in T1, in T2, T3, in T4, in T5, T6, in T7, T8, out TRes>(T1 p1, T2 p2, ref T3 p3, T4 p4, T5 p5, ref T6 p6, T7 p7, ref T8 p8);

    public delegate TRes FuncRef3_7_8<in T1, in T2, T3, in T4, in T5, in T6, T7, T8, out TRes>(T1 p1, T2 p2, ref T3 p3, T4 p4, T5 p5, T6 p6, ref T7 p7, ref T8 p8);

    public delegate TRes FuncRef4_5_6<in T1, in T2, in T3, T4, T5, T6, in T7, in T8, out TRes>(T1 p1, T2 p2, T3 p3, ref T4 p4, ref T5 p5, ref T6 p6, T7 p7, T8 p8);

    public delegate TRes FuncRef4_5_7<in T1, in T2, in T3, T4, T5, in T6, T7, in T8, out TRes>(T1 p1, T2 p2, T3 p3, ref T4 p4, ref T5 p5, T6 p6, ref T7 p7, T8 p8);

    public delegate TRes FuncRef4_5_8<in T1, in T2, in T3, T4, T5, in T6, in T7, T8, out TRes>(T1 p1, T2 p2, T3 p3, ref T4 p4, ref T5 p5, T6 p6, T7 p7, ref T8 p8);

    public delegate TRes FuncRef4_6_7<in T1, in T2, in T3, T4, in T5, T6, T7, in T8, out TRes>(T1 p1, T2 p2, T3 p3, ref T4 p4, T5 p5, ref T6 p6, ref T7 p7, T8 p8);

    public delegate TRes FuncRef4_6_8<in T1, in T2, in T3, T4, in T5, T6, in T7, T8, out TRes>(T1 p1, T2 p2, T3 p3, ref T4 p4, T5 p5, ref T6 p6, T7 p7, ref T8 p8);

    public delegate TRes FuncRef4_7_8<in T1, in T2, in T3, T4, in T5, in T6, T7, T8, out TRes>(T1 p1, T2 p2, T3 p3, ref T4 p4, T5 p5, T6 p6, ref T7 p7, ref T8 p8);

    public delegate TRes FuncRef5_6_7<in T1, in T2, in T3, in T4, T5, T6, T7, in T8, out TRes>(T1 p1, T2 p2, T3 p3, T4 p4, ref T5 p5, ref T6 p6, ref T7 p7, T8 p8);

    public delegate TRes FuncRef5_6_8<in T1, in T2, in T3, in T4, T5, T6, in T7, T8, out TRes>(T1 p1, T2 p2, T3 p3, T4 p4, ref T5 p5, ref T6 p6, T7 p7, ref T8 p8);

    public delegate TRes FuncRef5_7_8<in T1, in T2, in T3, in T4, T5, in T6, T7, T8, out TRes>(T1 p1, T2 p2, T3 p3, T4 p4, ref T5 p5, T6 p6, ref T7 p7, ref T8 p8);

    public delegate TRes FuncRef6_7_8<in T1, in T2, in T3, in T4, in T5, T6, T7, T8, out TRes>(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, ref T6 p6, ref T7 p7, ref T8 p8);

    public delegate TRes FuncRef1_2_3_4<T1, T2, T3, T4, in T5, in T6, in T7, in T8, out TRes>(ref T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, T5 p5, T6 p6, T7 p7, T8 p8);

    public delegate TRes FuncRef1_2_3_5<T1, T2, T3, in T4, T5, in T6, in T7, in T8, out TRes>(ref T1 p1, ref T2 p2, ref T3 p3, T4 p4, ref T5 p5, T6 p6, T7 p7, T8 p8);

    public delegate TRes FuncRef1_2_3_6<T1, T2, T3, in T4, in T5, T6, in T7, in T8, out TRes>(ref T1 p1, ref T2 p2, ref T3 p3, T4 p4, T5 p5, ref T6 p6, T7 p7, T8 p8);

    public delegate TRes FuncRef1_2_3_7<T1, T2, T3, in T4, in T5, in T6, T7, in T8, out TRes>(ref T1 p1, ref T2 p2, ref T3 p3, T4 p4, T5 p5, T6 p6, ref T7 p7, T8 p8);

    public delegate TRes FuncRef1_2_3_8<T1, T2, T3, in T4, in T5, in T6, in T7, T8, out TRes>(ref T1 p1, ref T2 p2, ref T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, ref T8 p8);

    public delegate TRes FuncRef1_2_4_5<T1, T2, in T3, T4, T5, in T6, in T7, in T8, out TRes>(ref T1 p1, ref T2 p2, T3 p3, ref T4 p4, ref T5 p5, T6 p6, T7 p7, T8 p8);

    public delegate TRes FuncRef1_2_4_6<T1, T2, in T3, T4, in T5, T6, in T7, in T8, out TRes>(ref T1 p1, ref T2 p2, T3 p3, ref T4 p4, T5 p5, ref T6 p6, T7 p7, T8 p8);

    public delegate TRes FuncRef1_2_4_7<T1, T2, in T3, T4, in T5, in T6, T7, in T8, out TRes>(ref T1 p1, ref T2 p2, T3 p3, ref T4 p4, T5 p5, T6 p6, ref T7 p7, T8 p8);

    public delegate TRes FuncRef1_2_4_8<T1, T2, in T3, T4, in T5, in T6, in T7, T8, out TRes>(ref T1 p1, ref T2 p2, T3 p3, ref T4 p4, T5 p5, T6 p6, T7 p7, ref T8 p8);

    public delegate TRes FuncRef1_2_5_6<T1, T2, in T3, in T4, T5, T6, in T7, in T8, out TRes>(ref T1 p1, ref T2 p2, T3 p3, T4 p4, ref T5 p5, ref T6 p6, T7 p7, T8 p8);

    public delegate TRes FuncRef1_2_5_7<T1, T2, in T3, in T4, T5, in T6, T7, in T8, out TRes>(ref T1 p1, ref T2 p2, T3 p3, T4 p4, ref T5 p5, T6 p6, ref T7 p7, T8 p8);

    public delegate TRes FuncRef1_2_5_8<T1, T2, in T3, in T4, T5, in T6, in T7, T8, out TRes>(ref T1 p1, ref T2 p2, T3 p3, T4 p4, ref T5 p5, T6 p6, T7 p7, ref T8 p8);

    public delegate TRes FuncRef1_2_6_7<T1, T2, in T3, in T4, in T5, T6, T7, in T8, out TRes>(ref T1 p1, ref T2 p2, T3 p3, T4 p4, T5 p5, ref T6 p6, ref T7 p7, T8 p8);

    public delegate TRes FuncRef1_2_6_8<T1, T2, in T3, in T4, in T5, T6, in T7, T8, out TRes>(ref T1 p1, ref T2 p2, T3 p3, T4 p4, T5 p5, ref T6 p6, T7 p7, ref T8 p8);

    public delegate TRes FuncRef1_2_7_8<T1, T2, in T3, in T4, in T5, in T6, T7, T8, out TRes>(ref T1 p1, ref T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, ref T7 p7, ref T8 p8);

    public delegate TRes FuncRef1_3_4_5<T1, in T2, T3, T4, T5, in T6, in T7, in T8, out TRes>(ref T1 p1, T2 p2, ref T3 p3, ref T4 p4, ref T5 p5, T6 p6, T7 p7, T8 p8);

    public delegate TRes FuncRef1_3_4_6<T1, in T2, T3, T4, in T5, T6, in T7, in T8, out TRes>(ref T1 p1, T2 p2, ref T3 p3, ref T4 p4, T5 p5, ref T6 p6, T7 p7, T8 p8);

    public delegate TRes FuncRef1_3_4_7<T1, in T2, T3, T4, in T5, in T6, T7, in T8, out TRes>(ref T1 p1, T2 p2, ref T3 p3, ref T4 p4, T5 p5, T6 p6, ref T7 p7, T8 p8);

    public delegate TRes FuncRef1_3_4_8<T1, in T2, T3, T4, in T5, in T6, in T7, T8, out TRes>(ref T1 p1, T2 p2, ref T3 p3, ref T4 p4, T5 p5, T6 p6, T7 p7, ref T8 p8);

    public delegate TRes FuncRef1_3_5_6<T1, in T2, T3, in T4, T5, T6, in T7, in T8, out TRes>(ref T1 p1, T2 p2, ref T3 p3, T4 p4, ref T5 p5, ref T6 p6, T7 p7, T8 p8);

    public delegate TRes FuncRef1_3_5_7<T1, in T2, T3, in T4, T5, in T6, T7, in T8, out TRes>(ref T1 p1, T2 p2, ref T3 p3, T4 p4, ref T5 p5, T6 p6, ref T7 p7, T8 p8);

    public delegate TRes FuncRef1_3_5_8<T1, in T2, T3, in T4, T5, in T6, in T7, T8, out TRes>(ref T1 p1, T2 p2, ref T3 p3, T4 p4, ref T5 p5, T6 p6, T7 p7, ref T8 p8);

    public delegate TRes FuncRef1_3_6_7<T1, in T2, T3, in T4, in T5, T6, T7, in T8, out TRes>(ref T1 p1, T2 p2, ref T3 p3, T4 p4, T5 p5, ref T6 p6, ref T7 p7, T8 p8);

    public delegate TRes FuncRef1_3_6_8<T1, in T2, T3, in T4, in T5, T6, in T7, T8, out TRes>(ref T1 p1, T2 p2, ref T3 p3, T4 p4, T5 p5, ref T6 p6, T7 p7, ref T8 p8);

    public delegate TRes FuncRef1_3_7_8<T1, in T2, T3, in T4, in T5, in T6, T7, T8, out TRes>(ref T1 p1, T2 p2, ref T3 p3, T4 p4, T5 p5, T6 p6, ref T7 p7, ref T8 p8);

    public delegate TRes FuncRef1_4_5_6<T1, in T2, in T3, T4, T5, T6, in T7, in T8, out TRes>(ref T1 p1, T2 p2, T3 p3, ref T4 p4, ref T5 p5, ref T6 p6, T7 p7, T8 p8);

    public delegate TRes FuncRef1_4_5_7<T1, in T2, in T3, T4, T5, in T6, T7, in T8, out TRes>(ref T1 p1, T2 p2, T3 p3, ref T4 p4, ref T5 p5, T6 p6, ref T7 p7, T8 p8);

    public delegate TRes FuncRef1_4_5_8<T1, in T2, in T3, T4, T5, in T6, in T7, T8, out TRes>(ref T1 p1, T2 p2, T3 p3, ref T4 p4, ref T5 p5, T6 p6, T7 p7, ref T8 p8);

    public delegate TRes FuncRef1_4_6_7<T1, in T2, in T3, T4, in T5, T6, T7, in T8, out TRes>(ref T1 p1, T2 p2, T3 p3, ref T4 p4, T5 p5, ref T6 p6, ref T7 p7, T8 p8);

    public delegate TRes FuncRef1_4_6_8<T1, in T2, in T3, T4, in T5, T6, in T7, T8, out TRes>(ref T1 p1, T2 p2, T3 p3, ref T4 p4, T5 p5, ref T6 p6, T7 p7, ref T8 p8);

    public delegate TRes FuncRef1_4_7_8<T1, in T2, in T3, T4, in T5, in T6, T7, T8, out TRes>(ref T1 p1, T2 p2, T3 p3, ref T4 p4, T5 p5, T6 p6, ref T7 p7, ref T8 p8);

    public delegate TRes FuncRef1_5_6_7<T1, in T2, in T3, in T4, T5, T6, T7, in T8, out TRes>(ref T1 p1, T2 p2, T3 p3, T4 p4, ref T5 p5, ref T6 p6, ref T7 p7, T8 p8);

    public delegate TRes FuncRef1_5_6_8<T1, in T2, in T3, in T4, T5, T6, in T7, T8, out TRes>(ref T1 p1, T2 p2, T3 p3, T4 p4, ref T5 p5, ref T6 p6, T7 p7, ref T8 p8);

    public delegate TRes FuncRef1_5_7_8<T1, in T2, in T3, in T4, T5, in T6, T7, T8, out TRes>(ref T1 p1, T2 p2, T3 p3, T4 p4, ref T5 p5, T6 p6, ref T7 p7, ref T8 p8);

    public delegate TRes FuncRef1_6_7_8<T1, in T2, in T3, in T4, in T5, T6, T7, T8, out TRes>(ref T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, ref T6 p6, ref T7 p7, ref T8 p8);

    public delegate TRes FuncRef2_3_4_5<in T1, T2, T3, T4, T5, in T6, in T7, in T8, out TRes>(T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, ref T5 p5, T6 p6, T7 p7, T8 p8);

    public delegate TRes FuncRef2_3_4_6<in T1, T2, T3, T4, in T5, T6, in T7, in T8, out TRes>(T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, T5 p5, ref T6 p6, T7 p7, T8 p8);

    public delegate TRes FuncRef2_3_4_7<in T1, T2, T3, T4, in T5, in T6, T7, in T8, out TRes>(T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, T5 p5, T6 p6, ref T7 p7, T8 p8);

    public delegate TRes FuncRef2_3_4_8<in T1, T2, T3, T4, in T5, in T6, in T7, T8, out TRes>(T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, T5 p5, T6 p6, T7 p7, ref T8 p8);

    public delegate TRes FuncRef2_3_5_6<in T1, T2, T3, in T4, T5, T6, in T7, in T8, out TRes>(T1 p1, ref T2 p2, ref T3 p3, T4 p4, ref T5 p5, ref T6 p6, T7 p7, T8 p8);

    public delegate TRes FuncRef2_3_5_7<in T1, T2, T3, in T4, T5, in T6, T7, in T8, out TRes>(T1 p1, ref T2 p2, ref T3 p3, T4 p4, ref T5 p5, T6 p6, ref T7 p7, T8 p8);

    public delegate TRes FuncRef2_3_5_8<in T1, T2, T3, in T4, T5, in T6, in T7, T8, out TRes>(T1 p1, ref T2 p2, ref T3 p3, T4 p4, ref T5 p5, T6 p6, T7 p7, ref T8 p8);

    public delegate TRes FuncRef2_3_6_7<in T1, T2, T3, in T4, in T5, T6, T7, in T8, out TRes>(T1 p1, ref T2 p2, ref T3 p3, T4 p4, T5 p5, ref T6 p6, ref T7 p7, T8 p8);

    public delegate TRes FuncRef2_3_6_8<in T1, T2, T3, in T4, in T5, T6, in T7, T8, out TRes>(T1 p1, ref T2 p2, ref T3 p3, T4 p4, T5 p5, ref T6 p6, T7 p7, ref T8 p8);

    public delegate TRes FuncRef2_3_7_8<in T1, T2, T3, in T4, in T5, in T6, T7, T8, out TRes>(T1 p1, ref T2 p2, ref T3 p3, T4 p4, T5 p5, T6 p6, ref T7 p7, ref T8 p8);

    public delegate TRes FuncRef2_4_5_6<in T1, T2, in T3, T4, T5, T6, in T7, in T8, out TRes>(T1 p1, ref T2 p2, T3 p3, ref T4 p4, ref T5 p5, ref T6 p6, T7 p7, T8 p8);

    public delegate TRes FuncRef2_4_5_7<in T1, T2, in T3, T4, T5, in T6, T7, in T8, out TRes>(T1 p1, ref T2 p2, T3 p3, ref T4 p4, ref T5 p5, T6 p6, ref T7 p7, T8 p8);

    public delegate TRes FuncRef2_4_5_8<in T1, T2, in T3, T4, T5, in T6, in T7, T8, out TRes>(T1 p1, ref T2 p2, T3 p3, ref T4 p4, ref T5 p5, T6 p6, T7 p7, ref T8 p8);

    public delegate TRes FuncRef2_4_6_7<in T1, T2, in T3, T4, in T5, T6, T7, in T8, out TRes>(T1 p1, ref T2 p2, T3 p3, ref T4 p4, T5 p5, ref T6 p6, ref T7 p7, T8 p8);

    public delegate TRes FuncRef2_4_6_8<in T1, T2, in T3, T4, in T5, T6, in T7, T8, out TRes>(T1 p1, ref T2 p2, T3 p3, ref T4 p4, T5 p5, ref T6 p6, T7 p7, ref T8 p8);

    public delegate TRes FuncRef2_4_7_8<in T1, T2, in T3, T4, in T5, in T6, T7, T8, out TRes>(T1 p1, ref T2 p2, T3 p3, ref T4 p4, T5 p5, T6 p6, ref T7 p7, ref T8 p8);

    public delegate TRes FuncRef2_5_6_7<in T1, T2, in T3, in T4, T5, T6, T7, in T8, out TRes>(T1 p1, ref T2 p2, T3 p3, T4 p4, ref T5 p5, ref T6 p6, ref T7 p7, T8 p8);

    public delegate TRes FuncRef2_5_6_8<in T1, T2, in T3, in T4, T5, T6, in T7, T8, out TRes>(T1 p1, ref T2 p2, T3 p3, T4 p4, ref T5 p5, ref T6 p6, T7 p7, ref T8 p8);

    public delegate TRes FuncRef2_5_7_8<in T1, T2, in T3, in T4, T5, in T6, T7, T8, out TRes>(T1 p1, ref T2 p2, T3 p3, T4 p4, ref T5 p5, T6 p6, ref T7 p7, ref T8 p8);

    public delegate TRes FuncRef2_6_7_8<in T1, T2, in T3, in T4, in T5, T6, T7, T8, out TRes>(T1 p1, ref T2 p2, T3 p3, T4 p4, T5 p5, ref T6 p6, ref T7 p7, ref T8 p8);

    public delegate TRes FuncRef3_4_5_6<in T1, in T2, T3, T4, T5, T6, in T7, in T8, out TRes>(T1 p1, T2 p2, ref T3 p3, ref T4 p4, ref T5 p5, ref T6 p6, T7 p7, T8 p8);

    public delegate TRes FuncRef3_4_5_7<in T1, in T2, T3, T4, T5, in T6, T7, in T8, out TRes>(T1 p1, T2 p2, ref T3 p3, ref T4 p4, ref T5 p5, T6 p6, ref T7 p7, T8 p8);

    public delegate TRes FuncRef3_4_5_8<in T1, in T2, T3, T4, T5, in T6, in T7, T8, out TRes>(T1 p1, T2 p2, ref T3 p3, ref T4 p4, ref T5 p5, T6 p6, T7 p7, ref T8 p8);

    public delegate TRes FuncRef3_4_6_7<in T1, in T2, T3, T4, in T5, T6, T7, in T8, out TRes>(T1 p1, T2 p2, ref T3 p3, ref T4 p4, T5 p5, ref T6 p6, ref T7 p7, T8 p8);

    public delegate TRes FuncRef3_4_6_8<in T1, in T2, T3, T4, in T5, T6, in T7, T8, out TRes>(T1 p1, T2 p2, ref T3 p3, ref T4 p4, T5 p5, ref T6 p6, T7 p7, ref T8 p8);

    public delegate TRes FuncRef3_4_7_8<in T1, in T2, T3, T4, in T5, in T6, T7, T8, out TRes>(T1 p1, T2 p2, ref T3 p3, ref T4 p4, T5 p5, T6 p6, ref T7 p7, ref T8 p8);

    public delegate TRes FuncRef3_5_6_7<in T1, in T2, T3, in T4, T5, T6, T7, in T8, out TRes>(T1 p1, T2 p2, ref T3 p3, T4 p4, ref T5 p5, ref T6 p6, ref T7 p7, T8 p8);

    public delegate TRes FuncRef3_5_6_8<in T1, in T2, T3, in T4, T5, T6, in T7, T8, out TRes>(T1 p1, T2 p2, ref T3 p3, T4 p4, ref T5 p5, ref T6 p6, T7 p7, ref T8 p8);

    public delegate TRes FuncRef3_5_7_8<in T1, in T2, T3, in T4, T5, in T6, T7, T8, out TRes>(T1 p1, T2 p2, ref T3 p3, T4 p4, ref T5 p5, T6 p6, ref T7 p7, ref T8 p8);

    public delegate TRes FuncRef3_6_7_8<in T1, in T2, T3, in T4, in T5, T6, T7, T8, out TRes>(T1 p1, T2 p2, ref T3 p3, T4 p4, T5 p5, ref T6 p6, ref T7 p7, ref T8 p8);

    public delegate TRes FuncRef4_5_6_7<in T1, in T2, in T3, T4, T5, T6, T7, in T8, out TRes>(T1 p1, T2 p2, T3 p3, ref T4 p4, ref T5 p5, ref T6 p6, ref T7 p7, T8 p8);

    public delegate TRes FuncRef4_5_6_8<in T1, in T2, in T3, T4, T5, T6, in T7, T8, out TRes>(T1 p1, T2 p2, T3 p3, ref T4 p4, ref T5 p5, ref T6 p6, T7 p7, ref T8 p8);

    public delegate TRes FuncRef4_5_7_8<in T1, in T2, in T3, T4, T5, in T6, T7, T8, out TRes>(T1 p1, T2 p2, T3 p3, ref T4 p4, ref T5 p5, T6 p6, ref T7 p7, ref T8 p8);

    public delegate TRes FuncRef4_6_7_8<in T1, in T2, in T3, T4, in T5, T6, T7, T8, out TRes>(T1 p1, T2 p2, T3 p3, ref T4 p4, T5 p5, ref T6 p6, ref T7 p7, ref T8 p8);

    public delegate TRes FuncRef5_6_7_8<in T1, in T2, in T3, in T4, T5, T6, T7, T8, out TRes>(T1 p1, T2 p2, T3 p3, T4 p4, ref T5 p5, ref T6 p6, ref T7 p7, ref T8 p8);

    public delegate TRes FuncRef1_2_3_4_5<T1, T2, T3, T4, T5, in T6, in T7, in T8, out TRes>(ref T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, ref T5 p5, T6 p6, T7 p7, T8 p8);

    public delegate TRes FuncRef1_2_3_4_6<T1, T2, T3, T4, in T5, T6, in T7, in T8, out TRes>(ref T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, T5 p5, ref T6 p6, T7 p7, T8 p8);

    public delegate TRes FuncRef1_2_3_4_7<T1, T2, T3, T4, in T5, in T6, T7, in T8, out TRes>(ref T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, T5 p5, T6 p6, ref T7 p7, T8 p8);

    public delegate TRes FuncRef1_2_3_4_8<T1, T2, T3, T4, in T5, in T6, in T7, T8, out TRes>(ref T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, T5 p5, T6 p6, T7 p7, ref T8 p8);

    public delegate TRes FuncRef1_2_3_5_6<T1, T2, T3, in T4, T5, T6, in T7, in T8, out TRes>(ref T1 p1, ref T2 p2, ref T3 p3, T4 p4, ref T5 p5, ref T6 p6, T7 p7, T8 p8);

    public delegate TRes FuncRef1_2_3_5_7<T1, T2, T3, in T4, T5, in T6, T7, in T8, out TRes>(ref T1 p1, ref T2 p2, ref T3 p3, T4 p4, ref T5 p5, T6 p6, ref T7 p7, T8 p8);

    public delegate TRes FuncRef1_2_3_5_8<T1, T2, T3, in T4, T5, in T6, in T7, T8, out TRes>(ref T1 p1, ref T2 p2, ref T3 p3, T4 p4, ref T5 p5, T6 p6, T7 p7, ref T8 p8);

    public delegate TRes FuncRef1_2_3_6_7<T1, T2, T3, in T4, in T5, T6, T7, in T8, out TRes>(ref T1 p1, ref T2 p2, ref T3 p3, T4 p4, T5 p5, ref T6 p6, ref T7 p7, T8 p8);

    public delegate TRes FuncRef1_2_3_6_8<T1, T2, T3, in T4, in T5, T6, in T7, T8, out TRes>(ref T1 p1, ref T2 p2, ref T3 p3, T4 p4, T5 p5, ref T6 p6, T7 p7, ref T8 p8);

    public delegate TRes FuncRef1_2_3_7_8<T1, T2, T3, in T4, in T5, in T6, T7, T8, out TRes>(ref T1 p1, ref T2 p2, ref T3 p3, T4 p4, T5 p5, T6 p6, ref T7 p7, ref T8 p8);

    public delegate TRes FuncRef1_2_4_5_6<T1, T2, in T3, T4, T5, T6, in T7, in T8, out TRes>(ref T1 p1, ref T2 p2, T3 p3, ref T4 p4, ref T5 p5, ref T6 p6, T7 p7, T8 p8);

    public delegate TRes FuncRef1_2_4_5_7<T1, T2, in T3, T4, T5, in T6, T7, in T8, out TRes>(ref T1 p1, ref T2 p2, T3 p3, ref T4 p4, ref T5 p5, T6 p6, ref T7 p7, T8 p8);

    public delegate TRes FuncRef1_2_4_5_8<T1, T2, in T3, T4, T5, in T6, in T7, T8, out TRes>(ref T1 p1, ref T2 p2, T3 p3, ref T4 p4, ref T5 p5, T6 p6, T7 p7, ref T8 p8);

    public delegate TRes FuncRef1_2_4_6_7<T1, T2, in T3, T4, in T5, T6, T7, in T8, out TRes>(ref T1 p1, ref T2 p2, T3 p3, ref T4 p4, T5 p5, ref T6 p6, ref T7 p7, T8 p8);

    public delegate TRes FuncRef1_2_4_6_8<T1, T2, in T3, T4, in T5, T6, in T7, T8, out TRes>(ref T1 p1, ref T2 p2, T3 p3, ref T4 p4, T5 p5, ref T6 p6, T7 p7, ref T8 p8);

    public delegate TRes FuncRef1_2_4_7_8<T1, T2, in T3, T4, in T5, in T6, T7, T8, out TRes>(ref T1 p1, ref T2 p2, T3 p3, ref T4 p4, T5 p5, T6 p6, ref T7 p7, ref T8 p8);

    public delegate TRes FuncRef1_2_5_6_7<T1, T2, in T3, in T4, T5, T6, T7, in T8, out TRes>(ref T1 p1, ref T2 p2, T3 p3, T4 p4, ref T5 p5, ref T6 p6, ref T7 p7, T8 p8);

    public delegate TRes FuncRef1_2_5_6_8<T1, T2, in T3, in T4, T5, T6, in T7, T8, out TRes>(ref T1 p1, ref T2 p2, T3 p3, T4 p4, ref T5 p5, ref T6 p6, T7 p7, ref T8 p8);

    public delegate TRes FuncRef1_2_5_7_8<T1, T2, in T3, in T4, T5, in T6, T7, T8, out TRes>(ref T1 p1, ref T2 p2, T3 p3, T4 p4, ref T5 p5, T6 p6, ref T7 p7, ref T8 p8);

    public delegate TRes FuncRef1_2_6_7_8<T1, T2, in T3, in T4, in T5, T6, T7, T8, out TRes>(ref T1 p1, ref T2 p2, T3 p3, T4 p4, T5 p5, ref T6 p6, ref T7 p7, ref T8 p8);

    public delegate TRes FuncRef1_3_4_5_6<T1, in T2, T3, T4, T5, T6, in T7, in T8, out TRes>(ref T1 p1, T2 p2, ref T3 p3, ref T4 p4, ref T5 p5, ref T6 p6, T7 p7, T8 p8);

    public delegate TRes FuncRef1_3_4_5_7<T1, in T2, T3, T4, T5, in T6, T7, in T8, out TRes>(ref T1 p1, T2 p2, ref T3 p3, ref T4 p4, ref T5 p5, T6 p6, ref T7 p7, T8 p8);

    public delegate TRes FuncRef1_3_4_5_8<T1, in T2, T3, T4, T5, in T6, in T7, T8, out TRes>(ref T1 p1, T2 p2, ref T3 p3, ref T4 p4, ref T5 p5, T6 p6, T7 p7, ref T8 p8);

    public delegate TRes FuncRef1_3_4_6_7<T1, in T2, T3, T4, in T5, T6, T7, in T8, out TRes>(ref T1 p1, T2 p2, ref T3 p3, ref T4 p4, T5 p5, ref T6 p6, ref T7 p7, T8 p8);

    public delegate TRes FuncRef1_3_4_6_8<T1, in T2, T3, T4, in T5, T6, in T7, T8, out TRes>(ref T1 p1, T2 p2, ref T3 p3, ref T4 p4, T5 p5, ref T6 p6, T7 p7, ref T8 p8);

    public delegate TRes FuncRef1_3_4_7_8<T1, in T2, T3, T4, in T5, in T6, T7, T8, out TRes>(ref T1 p1, T2 p2, ref T3 p3, ref T4 p4, T5 p5, T6 p6, ref T7 p7, ref T8 p8);

    public delegate TRes FuncRef1_3_5_6_7<T1, in T2, T3, in T4, T5, T6, T7, in T8, out TRes>(ref T1 p1, T2 p2, ref T3 p3, T4 p4, ref T5 p5, ref T6 p6, ref T7 p7, T8 p8);

    public delegate TRes FuncRef1_3_5_6_8<T1, in T2, T3, in T4, T5, T6, in T7, T8, out TRes>(ref T1 p1, T2 p2, ref T3 p3, T4 p4, ref T5 p5, ref T6 p6, T7 p7, ref T8 p8);

    public delegate TRes FuncRef1_3_5_7_8<T1, in T2, T3, in T4, T5, in T6, T7, T8, out TRes>(ref T1 p1, T2 p2, ref T3 p3, T4 p4, ref T5 p5, T6 p6, ref T7 p7, ref T8 p8);

    public delegate TRes FuncRef1_3_6_7_8<T1, in T2, T3, in T4, in T5, T6, T7, T8, out TRes>(ref T1 p1, T2 p2, ref T3 p3, T4 p4, T5 p5, ref T6 p6, ref T7 p7, ref T8 p8);

    public delegate TRes FuncRef1_4_5_6_7<T1, in T2, in T3, T4, T5, T6, T7, in T8, out TRes>(ref T1 p1, T2 p2, T3 p3, ref T4 p4, ref T5 p5, ref T6 p6, ref T7 p7, T8 p8);

    public delegate TRes FuncRef1_4_5_6_8<T1, in T2, in T3, T4, T5, T6, in T7, T8, out TRes>(ref T1 p1, T2 p2, T3 p3, ref T4 p4, ref T5 p5, ref T6 p6, T7 p7, ref T8 p8);

    public delegate TRes FuncRef1_4_5_7_8<T1, in T2, in T3, T4, T5, in T6, T7, T8, out TRes>(ref T1 p1, T2 p2, T3 p3, ref T4 p4, ref T5 p5, T6 p6, ref T7 p7, ref T8 p8);

    public delegate TRes FuncRef1_4_6_7_8<T1, in T2, in T3, T4, in T5, T6, T7, T8, out TRes>(ref T1 p1, T2 p2, T3 p3, ref T4 p4, T5 p5, ref T6 p6, ref T7 p7, ref T8 p8);

    public delegate TRes FuncRef1_5_6_7_8<T1, in T2, in T3, in T4, T5, T6, T7, T8, out TRes>(ref T1 p1, T2 p2, T3 p3, T4 p4, ref T5 p5, ref T6 p6, ref T7 p7, ref T8 p8);

    public delegate TRes FuncRef2_3_4_5_6<in T1, T2, T3, T4, T5, T6, in T7, in T8, out TRes>(T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, ref T5 p5, ref T6 p6, T7 p7, T8 p8);

    public delegate TRes FuncRef2_3_4_5_7<in T1, T2, T3, T4, T5, in T6, T7, in T8, out TRes>(T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, ref T5 p5, T6 p6, ref T7 p7, T8 p8);

    public delegate TRes FuncRef2_3_4_5_8<in T1, T2, T3, T4, T5, in T6, in T7, T8, out TRes>(T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, ref T5 p5, T6 p6, T7 p7, ref T8 p8);

    public delegate TRes FuncRef2_3_4_6_7<in T1, T2, T3, T4, in T5, T6, T7, in T8, out TRes>(T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, T5 p5, ref T6 p6, ref T7 p7, T8 p8);

    public delegate TRes FuncRef2_3_4_6_8<in T1, T2, T3, T4, in T5, T6, in T7, T8, out TRes>(T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, T5 p5, ref T6 p6, T7 p7, ref T8 p8);

    public delegate TRes FuncRef2_3_4_7_8<in T1, T2, T3, T4, in T5, in T6, T7, T8, out TRes>(T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, T5 p5, T6 p6, ref T7 p7, ref T8 p8);

    public delegate TRes FuncRef2_3_5_6_7<in T1, T2, T3, in T4, T5, T6, T7, in T8, out TRes>(T1 p1, ref T2 p2, ref T3 p3, T4 p4, ref T5 p5, ref T6 p6, ref T7 p7, T8 p8);

    public delegate TRes FuncRef2_3_5_6_8<in T1, T2, T3, in T4, T5, T6, in T7, T8, out TRes>(T1 p1, ref T2 p2, ref T3 p3, T4 p4, ref T5 p5, ref T6 p6, T7 p7, ref T8 p8);

    public delegate TRes FuncRef2_3_5_7_8<in T1, T2, T3, in T4, T5, in T6, T7, T8, out TRes>(T1 p1, ref T2 p2, ref T3 p3, T4 p4, ref T5 p5, T6 p6, ref T7 p7, ref T8 p8);

    public delegate TRes FuncRef2_3_6_7_8<in T1, T2, T3, in T4, in T5, T6, T7, T8, out TRes>(T1 p1, ref T2 p2, ref T3 p3, T4 p4, T5 p5, ref T6 p6, ref T7 p7, ref T8 p8);

    public delegate TRes FuncRef2_4_5_6_7<in T1, T2, in T3, T4, T5, T6, T7, in T8, out TRes>(T1 p1, ref T2 p2, T3 p3, ref T4 p4, ref T5 p5, ref T6 p6, ref T7 p7, T8 p8);

    public delegate TRes FuncRef2_4_5_6_8<in T1, T2, in T3, T4, T5, T6, in T7, T8, out TRes>(T1 p1, ref T2 p2, T3 p3, ref T4 p4, ref T5 p5, ref T6 p6, T7 p7, ref T8 p8);

    public delegate TRes FuncRef2_4_5_7_8<in T1, T2, in T3, T4, T5, in T6, T7, T8, out TRes>(T1 p1, ref T2 p2, T3 p3, ref T4 p4, ref T5 p5, T6 p6, ref T7 p7, ref T8 p8);

    public delegate TRes FuncRef2_4_6_7_8<in T1, T2, in T3, T4, in T5, T6, T7, T8, out TRes>(T1 p1, ref T2 p2, T3 p3, ref T4 p4, T5 p5, ref T6 p6, ref T7 p7, ref T8 p8);

    public delegate TRes FuncRef2_5_6_7_8<in T1, T2, in T3, in T4, T5, T6, T7, T8, out TRes>(T1 p1, ref T2 p2, T3 p3, T4 p4, ref T5 p5, ref T6 p6, ref T7 p7, ref T8 p8);

    public delegate TRes FuncRef3_4_5_6_7<in T1, in T2, T3, T4, T5, T6, T7, in T8, out TRes>(T1 p1, T2 p2, ref T3 p3, ref T4 p4, ref T5 p5, ref T6 p6, ref T7 p7, T8 p8);

    public delegate TRes FuncRef3_4_5_6_8<in T1, in T2, T3, T4, T5, T6, in T7, T8, out TRes>(T1 p1, T2 p2, ref T3 p3, ref T4 p4, ref T5 p5, ref T6 p6, T7 p7, ref T8 p8);

    public delegate TRes FuncRef3_4_5_7_8<in T1, in T2, T3, T4, T5, in T6, T7, T8, out TRes>(T1 p1, T2 p2, ref T3 p3, ref T4 p4, ref T5 p5, T6 p6, ref T7 p7, ref T8 p8);

    public delegate TRes FuncRef3_4_6_7_8<in T1, in T2, T3, T4, in T5, T6, T7, T8, out TRes>(T1 p1, T2 p2, ref T3 p3, ref T4 p4, T5 p5, ref T6 p6, ref T7 p7, ref T8 p8);

    public delegate TRes FuncRef3_5_6_7_8<in T1, in T2, T3, in T4, T5, T6, T7, T8, out TRes>(T1 p1, T2 p2, ref T3 p3, T4 p4, ref T5 p5, ref T6 p6, ref T7 p7, ref T8 p8);

    public delegate TRes FuncRef4_5_6_7_8<in T1, in T2, in T3, T4, T5, T6, T7, T8, out TRes>(T1 p1, T2 p2, T3 p3, ref T4 p4, ref T5 p5, ref T6 p6, ref T7 p7, ref T8 p8);

    public delegate TRes FuncRef1_2_3_4_5_6<T1, T2, T3, T4, T5, T6, in T7, in T8, out TRes>(ref T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, ref T5 p5, ref T6 p6, T7 p7, T8 p8);

    public delegate TRes FuncRef1_2_3_4_5_7<T1, T2, T3, T4, T5, in T6, T7, in T8, out TRes>(ref T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, ref T5 p5, T6 p6, ref T7 p7, T8 p8);

    public delegate TRes FuncRef1_2_3_4_5_8<T1, T2, T3, T4, T5, in T6, in T7, T8, out TRes>(ref T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, ref T5 p5, T6 p6, T7 p7, ref T8 p8);

    public delegate TRes FuncRef1_2_3_4_6_7<T1, T2, T3, T4, in T5, T6, T7, in T8, out TRes>(ref T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, T5 p5, ref T6 p6, ref T7 p7, T8 p8);

    public delegate TRes FuncRef1_2_3_4_6_8<T1, T2, T3, T4, in T5, T6, in T7, T8, out TRes>(ref T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, T5 p5, ref T6 p6, T7 p7, ref T8 p8);

    public delegate TRes FuncRef1_2_3_4_7_8<T1, T2, T3, T4, in T5, in T6, T7, T8, out TRes>(ref T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, T5 p5, T6 p6, ref T7 p7, ref T8 p8);

    public delegate TRes FuncRef1_2_3_5_6_7<T1, T2, T3, in T4, T5, T6, T7, in T8, out TRes>(ref T1 p1, ref T2 p2, ref T3 p3, T4 p4, ref T5 p5, ref T6 p6, ref T7 p7, T8 p8);

    public delegate TRes FuncRef1_2_3_5_6_8<T1, T2, T3, in T4, T5, T6, in T7, T8, out TRes>(ref T1 p1, ref T2 p2, ref T3 p3, T4 p4, ref T5 p5, ref T6 p6, T7 p7, ref T8 p8);

    public delegate TRes FuncRef1_2_3_5_7_8<T1, T2, T3, in T4, T5, in T6, T7, T8, out TRes>(ref T1 p1, ref T2 p2, ref T3 p3, T4 p4, ref T5 p5, T6 p6, ref T7 p7, ref T8 p8);

    public delegate TRes FuncRef1_2_3_6_7_8<T1, T2, T3, in T4, in T5, T6, T7, T8, out TRes>(ref T1 p1, ref T2 p2, ref T3 p3, T4 p4, T5 p5, ref T6 p6, ref T7 p7, ref T8 p8);

    public delegate TRes FuncRef1_2_4_5_6_7<T1, T2, in T3, T4, T5, T6, T7, in T8, out TRes>(ref T1 p1, ref T2 p2, T3 p3, ref T4 p4, ref T5 p5, ref T6 p6, ref T7 p7, T8 p8);

    public delegate TRes FuncRef1_2_4_5_6_8<T1, T2, in T3, T4, T5, T6, in T7, T8, out TRes>(ref T1 p1, ref T2 p2, T3 p3, ref T4 p4, ref T5 p5, ref T6 p6, T7 p7, ref T8 p8);

    public delegate TRes FuncRef1_2_4_5_7_8<T1, T2, in T3, T4, T5, in T6, T7, T8, out TRes>(ref T1 p1, ref T2 p2, T3 p3, ref T4 p4, ref T5 p5, T6 p6, ref T7 p7, ref T8 p8);

    public delegate TRes FuncRef1_2_4_6_7_8<T1, T2, in T3, T4, in T5, T6, T7, T8, out TRes>(ref T1 p1, ref T2 p2, T3 p3, ref T4 p4, T5 p5, ref T6 p6, ref T7 p7, ref T8 p8);

    public delegate TRes FuncRef1_2_5_6_7_8<T1, T2, in T3, in T4, T5, T6, T7, T8, out TRes>(ref T1 p1, ref T2 p2, T3 p3, T4 p4, ref T5 p5, ref T6 p6, ref T7 p7, ref T8 p8);

    public delegate TRes FuncRef1_3_4_5_6_7<T1, in T2, T3, T4, T5, T6, T7, in T8, out TRes>(ref T1 p1, T2 p2, ref T3 p3, ref T4 p4, ref T5 p5, ref T6 p6, ref T7 p7, T8 p8);

    public delegate TRes FuncRef1_3_4_5_6_8<T1, in T2, T3, T4, T5, T6, in T7, T8, out TRes>(ref T1 p1, T2 p2, ref T3 p3, ref T4 p4, ref T5 p5, ref T6 p6, T7 p7, ref T8 p8);

    public delegate TRes FuncRef1_3_4_5_7_8<T1, in T2, T3, T4, T5, in T6, T7, T8, out TRes>(ref T1 p1, T2 p2, ref T3 p3, ref T4 p4, ref T5 p5, T6 p6, ref T7 p7, ref T8 p8);

    public delegate TRes FuncRef1_3_4_6_7_8<T1, in T2, T3, T4, in T5, T6, T7, T8, out TRes>(ref T1 p1, T2 p2, ref T3 p3, ref T4 p4, T5 p5, ref T6 p6, ref T7 p7, ref T8 p8);

    public delegate TRes FuncRef1_3_5_6_7_8<T1, in T2, T3, in T4, T5, T6, T7, T8, out TRes>(ref T1 p1, T2 p2, ref T3 p3, T4 p4, ref T5 p5, ref T6 p6, ref T7 p7, ref T8 p8);

    public delegate TRes FuncRef1_4_5_6_7_8<T1, in T2, in T3, T4, T5, T6, T7, T8, out TRes>(ref T1 p1, T2 p2, T3 p3, ref T4 p4, ref T5 p5, ref T6 p6, ref T7 p7, ref T8 p8);

    public delegate TRes FuncRef2_3_4_5_6_7<in T1, T2, T3, T4, T5, T6, T7, in T8, out TRes>(T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, ref T5 p5, ref T6 p6, ref T7 p7, T8 p8);

    public delegate TRes FuncRef2_3_4_5_6_8<in T1, T2, T3, T4, T5, T6, in T7, T8, out TRes>(T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, ref T5 p5, ref T6 p6, T7 p7, ref T8 p8);

    public delegate TRes FuncRef2_3_4_5_7_8<in T1, T2, T3, T4, T5, in T6, T7, T8, out TRes>(T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, ref T5 p5, T6 p6, ref T7 p7, ref T8 p8);

    public delegate TRes FuncRef2_3_4_6_7_8<in T1, T2, T3, T4, in T5, T6, T7, T8, out TRes>(T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, T5 p5, ref T6 p6, ref T7 p7, ref T8 p8);

    public delegate TRes FuncRef2_3_5_6_7_8<in T1, T2, T3, in T4, T5, T6, T7, T8, out TRes>(T1 p1, ref T2 p2, ref T3 p3, T4 p4, ref T5 p5, ref T6 p6, ref T7 p7, ref T8 p8);

    public delegate TRes FuncRef2_4_5_6_7_8<in T1, T2, in T3, T4, T5, T6, T7, T8, out TRes>(T1 p1, ref T2 p2, T3 p3, ref T4 p4, ref T5 p5, ref T6 p6, ref T7 p7, ref T8 p8);

    public delegate TRes FuncRef3_4_5_6_7_8<in T1, in T2, T3, T4, T5, T6, T7, T8, out TRes>(T1 p1, T2 p2, ref T3 p3, ref T4 p4, ref T5 p5, ref T6 p6, ref T7 p7, ref T8 p8);

    public delegate TRes FuncRef1_2_3_4_5_6_7<T1, T2, T3, T4, T5, T6, T7, in T8, out TRes>(ref T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, ref T5 p5, ref T6 p6, ref T7 p7, T8 p8);

    public delegate TRes FuncRef1_2_3_4_5_6_8<T1, T2, T3, T4, T5, T6, in T7, T8, out TRes>(ref T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, ref T5 p5, ref T6 p6, T7 p7, ref T8 p8);

    public delegate TRes FuncRef1_2_3_4_5_7_8<T1, T2, T3, T4, T5, in T6, T7, T8, out TRes>(ref T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, ref T5 p5, T6 p6, ref T7 p7, ref T8 p8);

    public delegate TRes FuncRef1_2_3_4_6_7_8<T1, T2, T3, T4, in T5, T6, T7, T8, out TRes>(ref T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, T5 p5, ref T6 p6, ref T7 p7, ref T8 p8);

    public delegate TRes FuncRef1_2_3_5_6_7_8<T1, T2, T3, in T4, T5, T6, T7, T8, out TRes>(ref T1 p1, ref T2 p2, ref T3 p3, T4 p4, ref T5 p5, ref T6 p6, ref T7 p7, ref T8 p8);

    public delegate TRes FuncRef1_2_4_5_6_7_8<T1, T2, in T3, T4, T5, T6, T7, T8, out TRes>(ref T1 p1, ref T2 p2, T3 p3, ref T4 p4, ref T5 p5, ref T6 p6, ref T7 p7, ref T8 p8);

    public delegate TRes FuncRef1_3_4_5_6_7_8<T1, in T2, T3, T4, T5, T6, T7, T8, out TRes>(ref T1 p1, T2 p2, ref T3 p3, ref T4 p4, ref T5 p5, ref T6 p6, ref T7 p7, ref T8 p8);

    public delegate TRes FuncRef2_3_4_5_6_7_8<in T1, T2, T3, T4, T5, T6, T7, T8, out TRes>(T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, ref T5 p5, ref T6 p6, ref T7 p7, ref T8 p8);

    public delegate TRes FuncRef<T1, T2, T3, T4, T5, T6, T7, T8, out TRes>(ref T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, ref T5 p5, ref T6 p6, ref T7 p7, ref T8 p8);


    /* 9 param-group */

    public delegate TRes FuncRef1<T1, in T2, in T3, in T4, in T5, in T6, in T7, in T8, in T9, out TRes>(ref T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9);

    public delegate TRes FuncRef2<in T1, T2, in T3, in T4, in T5, in T6, in T7, in T8, in T9, out TRes>(T1 p1, ref T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9);

    public delegate TRes FuncRef3<in T1, in T2, T3, in T4, in T5, in T6, in T7, in T8, in T9, out TRes>(T1 p1, T2 p2, ref T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9);

    public delegate TRes FuncRef4<in T1, in T2, in T3, T4, in T5, in T6, in T7, in T8, in T9, out TRes>(T1 p1, T2 p2, T3 p3, ref T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9);

    public delegate TRes FuncRef5<in T1, in T2, in T3, in T4, T5, in T6, in T7, in T8, in T9, out TRes>(T1 p1, T2 p2, T3 p3, T4 p4, ref T5 p5, T6 p6, T7 p7, T8 p8, T9 p9);

    public delegate TRes FuncRef6<in T1, in T2, in T3, in T4, in T5, T6, in T7, in T8, in T9, out TRes>(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, ref T6 p6, T7 p7, T8 p8, T9 p9);

    public delegate TRes FuncRef7<in T1, in T2, in T3, in T4, in T5, in T6, T7, in T8, in T9, out TRes>(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, ref T7 p7, T8 p8, T9 p9);

    public delegate TRes FuncRef8<in T1, in T2, in T3, in T4, in T5, in T6, in T7, T8, in T9, out TRes>(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, ref T8 p8, T9 p9);

    public delegate TRes FuncRef9<in T1, in T2, in T3, in T4, in T5, in T6, in T7, in T8, T9, out TRes>(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, ref T9 p9);

    public delegate TRes FuncRef1_2<T1, T2, in T3, in T4, in T5, in T6, in T7, in T8, in T9, out TRes>(ref T1 p1, ref T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9);

    public delegate TRes FuncRef1_3<T1, in T2, T3, in T4, in T5, in T6, in T7, in T8, in T9, out TRes>(ref T1 p1, T2 p2, ref T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9);

    public delegate TRes FuncRef1_4<T1, in T2, in T3, T4, in T5, in T6, in T7, in T8, in T9, out TRes>(ref T1 p1, T2 p2, T3 p3, ref T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9);

    public delegate TRes FuncRef1_5<T1, in T2, in T3, in T4, T5, in T6, in T7, in T8, in T9, out TRes>(ref T1 p1, T2 p2, T3 p3, T4 p4, ref T5 p5, T6 p6, T7 p7, T8 p8, T9 p9);

    public delegate TRes FuncRef1_6<T1, in T2, in T3, in T4, in T5, T6, in T7, in T8, in T9, out TRes>(ref T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, ref T6 p6, T7 p7, T8 p8, T9 p9);

    public delegate TRes FuncRef1_7<T1, in T2, in T3, in T4, in T5, in T6, T7, in T8, in T9, out TRes>(ref T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, ref T7 p7, T8 p8, T9 p9);

    public delegate TRes FuncRef1_8<T1, in T2, in T3, in T4, in T5, in T6, in T7, T8, in T9, out TRes>(ref T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, ref T8 p8, T9 p9);

    public delegate TRes FuncRef1_9<T1, in T2, in T3, in T4, in T5, in T6, in T7, in T8, T9, out TRes>(ref T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, ref T9 p9);

    public delegate TRes FuncRef2_3<in T1, T2, T3, in T4, in T5, in T6, in T7, in T8, in T9, out TRes>(T1 p1, ref T2 p2, ref T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9);

    public delegate TRes FuncRef2_4<in T1, T2, in T3, T4, in T5, in T6, in T7, in T8, in T9, out TRes>(T1 p1, ref T2 p2, T3 p3, ref T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9);

    public delegate TRes FuncRef2_5<in T1, T2, in T3, in T4, T5, in T6, in T7, in T8, in T9, out TRes>(T1 p1, ref T2 p2, T3 p3, T4 p4, ref T5 p5, T6 p6, T7 p7, T8 p8, T9 p9);

    public delegate TRes FuncRef2_6<in T1, T2, in T3, in T4, in T5, T6, in T7, in T8, in T9, out TRes>(T1 p1, ref T2 p2, T3 p3, T4 p4, T5 p5, ref T6 p6, T7 p7, T8 p8, T9 p9);

    public delegate TRes FuncRef2_7<in T1, T2, in T3, in T4, in T5, in T6, T7, in T8, in T9, out TRes>(T1 p1, ref T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, ref T7 p7, T8 p8, T9 p9);

    public delegate TRes FuncRef2_8<in T1, T2, in T3, in T4, in T5, in T6, in T7, T8, in T9, out TRes>(T1 p1, ref T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, ref T8 p8, T9 p9);

    public delegate TRes FuncRef2_9<in T1, T2, in T3, in T4, in T5, in T6, in T7, in T8, T9, out TRes>(T1 p1, ref T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, ref T9 p9);

    public delegate TRes FuncRef3_4<in T1, in T2, T3, T4, in T5, in T6, in T7, in T8, in T9, out TRes>(T1 p1, T2 p2, ref T3 p3, ref T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9);

    public delegate TRes FuncRef3_5<in T1, in T2, T3, in T4, T5, in T6, in T7, in T8, in T9, out TRes>(T1 p1, T2 p2, ref T3 p3, T4 p4, ref T5 p5, T6 p6, T7 p7, T8 p8, T9 p9);

    public delegate TRes FuncRef3_6<in T1, in T2, T3, in T4, in T5, T6, in T7, in T8, in T9, out TRes>(T1 p1, T2 p2, ref T3 p3, T4 p4, T5 p5, ref T6 p6, T7 p7, T8 p8, T9 p9);

    public delegate TRes FuncRef3_7<in T1, in T2, T3, in T4, in T5, in T6, T7, in T8, in T9, out TRes>(T1 p1, T2 p2, ref T3 p3, T4 p4, T5 p5, T6 p6, ref T7 p7, T8 p8, T9 p9);

    public delegate TRes FuncRef3_8<in T1, in T2, T3, in T4, in T5, in T6, in T7, T8, in T9, out TRes>(T1 p1, T2 p2, ref T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, ref T8 p8, T9 p9);

    public delegate TRes FuncRef3_9<in T1, in T2, T3, in T4, in T5, in T6, in T7, in T8, T9, out TRes>(T1 p1, T2 p2, ref T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, ref T9 p9);

    public delegate TRes FuncRef4_5<in T1, in T2, in T3, T4, T5, in T6, in T7, in T8, in T9, out TRes>(T1 p1, T2 p2, T3 p3, ref T4 p4, ref T5 p5, T6 p6, T7 p7, T8 p8, T9 p9);

    public delegate TRes FuncRef4_6<in T1, in T2, in T3, T4, in T5, T6, in T7, in T8, in T9, out TRes>(T1 p1, T2 p2, T3 p3, ref T4 p4, T5 p5, ref T6 p6, T7 p7, T8 p8, T9 p9);

    public delegate TRes FuncRef4_7<in T1, in T2, in T3, T4, in T5, in T6, T7, in T8, in T9, out TRes>(T1 p1, T2 p2, T3 p3, ref T4 p4, T5 p5, T6 p6, ref T7 p7, T8 p8, T9 p9);

    public delegate TRes FuncRef4_8<in T1, in T2, in T3, T4, in T5, in T6, in T7, T8, in T9, out TRes>(T1 p1, T2 p2, T3 p3, ref T4 p4, T5 p5, T6 p6, T7 p7, ref T8 p8, T9 p9);

    public delegate TRes FuncRef4_9<in T1, in T2, in T3, T4, in T5, in T6, in T7, in T8, T9, out TRes>(T1 p1, T2 p2, T3 p3, ref T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, ref T9 p9);

    public delegate TRes FuncRef5_6<in T1, in T2, in T3, in T4, T5, T6, in T7, in T8, in T9, out TRes>(T1 p1, T2 p2, T3 p3, T4 p4, ref T5 p5, ref T6 p6, T7 p7, T8 p8, T9 p9);

    public delegate TRes FuncRef5_7<in T1, in T2, in T3, in T4, T5, in T6, T7, in T8, in T9, out TRes>(T1 p1, T2 p2, T3 p3, T4 p4, ref T5 p5, T6 p6, ref T7 p7, T8 p8, T9 p9);

    public delegate TRes FuncRef5_8<in T1, in T2, in T3, in T4, T5, in T6, in T7, T8, in T9, out TRes>(T1 p1, T2 p2, T3 p3, T4 p4, ref T5 p5, T6 p6, T7 p7, ref T8 p8, T9 p9);

    public delegate TRes FuncRef5_9<in T1, in T2, in T3, in T4, T5, in T6, in T7, in T8, T9, out TRes>(T1 p1, T2 p2, T3 p3, T4 p4, ref T5 p5, T6 p6, T7 p7, T8 p8, ref T9 p9);

    public delegate TRes FuncRef6_7<in T1, in T2, in T3, in T4, in T5, T6, T7, in T8, in T9, out TRes>(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, ref T6 p6, ref T7 p7, T8 p8, T9 p9);

    public delegate TRes FuncRef6_8<in T1, in T2, in T3, in T4, in T5, T6, in T7, T8, in T9, out TRes>(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, ref T6 p6, T7 p7, ref T8 p8, T9 p9);

    public delegate TRes FuncRef6_9<in T1, in T2, in T3, in T4, in T5, T6, in T7, in T8, T9, out TRes>(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, ref T6 p6, T7 p7, T8 p8, ref T9 p9);

    public delegate TRes FuncRef7_8<in T1, in T2, in T3, in T4, in T5, in T6, T7, T8, in T9, out TRes>(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, ref T7 p7, ref T8 p8, T9 p9);

    public delegate TRes FuncRef7_9<in T1, in T2, in T3, in T4, in T5, in T6, T7, in T8, T9, out TRes>(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, ref T7 p7, T8 p8, ref T9 p9);

    public delegate TRes FuncRef8_9<in T1, in T2, in T3, in T4, in T5, in T6, in T7, T8, T9, out TRes>(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, ref T8 p8, ref T9 p9);

    public delegate TRes FuncRef1_2_3<T1, T2, T3, in T4, in T5, in T6, in T7, in T8, in T9, out TRes>(ref T1 p1, ref T2 p2, ref T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9);

    public delegate TRes FuncRef1_2_4<T1, T2, in T3, T4, in T5, in T6, in T7, in T8, in T9, out TRes>(ref T1 p1, ref T2 p2, T3 p3, ref T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9);

    public delegate TRes FuncRef1_2_5<T1, T2, in T3, in T4, T5, in T6, in T7, in T8, in T9, out TRes>(ref T1 p1, ref T2 p2, T3 p3, T4 p4, ref T5 p5, T6 p6, T7 p7, T8 p8, T9 p9);

    public delegate TRes FuncRef1_2_6<T1, T2, in T3, in T4, in T5, T6, in T7, in T8, in T9, out TRes>(ref T1 p1, ref T2 p2, T3 p3, T4 p4, T5 p5, ref T6 p6, T7 p7, T8 p8, T9 p9);

    public delegate TRes FuncRef1_2_7<T1, T2, in T3, in T4, in T5, in T6, T7, in T8, in T9, out TRes>(ref T1 p1, ref T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, ref T7 p7, T8 p8, T9 p9);

    public delegate TRes FuncRef1_2_8<T1, T2, in T3, in T4, in T5, in T6, in T7, T8, in T9, out TRes>(ref T1 p1, ref T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, ref T8 p8, T9 p9);

    public delegate TRes FuncRef1_2_9<T1, T2, in T3, in T4, in T5, in T6, in T7, in T8, T9, out TRes>(ref T1 p1, ref T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, ref T9 p9);

    public delegate TRes FuncRef1_3_4<T1, in T2, T3, T4, in T5, in T6, in T7, in T8, in T9, out TRes>(ref T1 p1, T2 p2, ref T3 p3, ref T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9);

    public delegate TRes FuncRef1_3_5<T1, in T2, T3, in T4, T5, in T6, in T7, in T8, in T9, out TRes>(ref T1 p1, T2 p2, ref T3 p3, T4 p4, ref T5 p5, T6 p6, T7 p7, T8 p8, T9 p9);

    public delegate TRes FuncRef1_3_6<T1, in T2, T3, in T4, in T5, T6, in T7, in T8, in T9, out TRes>(ref T1 p1, T2 p2, ref T3 p3, T4 p4, T5 p5, ref T6 p6, T7 p7, T8 p8, T9 p9);

    public delegate TRes FuncRef1_3_7<T1, in T2, T3, in T4, in T5, in T6, T7, in T8, in T9, out TRes>(ref T1 p1, T2 p2, ref T3 p3, T4 p4, T5 p5, T6 p6, ref T7 p7, T8 p8, T9 p9);

    public delegate TRes FuncRef1_3_8<T1, in T2, T3, in T4, in T5, in T6, in T7, T8, in T9, out TRes>(ref T1 p1, T2 p2, ref T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, ref T8 p8, T9 p9);

    public delegate TRes FuncRef1_3_9<T1, in T2, T3, in T4, in T5, in T6, in T7, in T8, T9, out TRes>(ref T1 p1, T2 p2, ref T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, ref T9 p9);

    public delegate TRes FuncRef1_4_5<T1, in T2, in T3, T4, T5, in T6, in T7, in T8, in T9, out TRes>(ref T1 p1, T2 p2, T3 p3, ref T4 p4, ref T5 p5, T6 p6, T7 p7, T8 p8, T9 p9);

    public delegate TRes FuncRef1_4_6<T1, in T2, in T3, T4, in T5, T6, in T7, in T8, in T9, out TRes>(ref T1 p1, T2 p2, T3 p3, ref T4 p4, T5 p5, ref T6 p6, T7 p7, T8 p8, T9 p9);

    public delegate TRes FuncRef1_4_7<T1, in T2, in T3, T4, in T5, in T6, T7, in T8, in T9, out TRes>(ref T1 p1, T2 p2, T3 p3, ref T4 p4, T5 p5, T6 p6, ref T7 p7, T8 p8, T9 p9);

    public delegate TRes FuncRef1_4_8<T1, in T2, in T3, T4, in T5, in T6, in T7, T8, in T9, out TRes>(ref T1 p1, T2 p2, T3 p3, ref T4 p4, T5 p5, T6 p6, T7 p7, ref T8 p8, T9 p9);

    public delegate TRes FuncRef1_4_9<T1, in T2, in T3, T4, in T5, in T6, in T7, in T8, T9, out TRes>(ref T1 p1, T2 p2, T3 p3, ref T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, ref T9 p9);

    public delegate TRes FuncRef1_5_6<T1, in T2, in T3, in T4, T5, T6, in T7, in T8, in T9, out TRes>(ref T1 p1, T2 p2, T3 p3, T4 p4, ref T5 p5, ref T6 p6, T7 p7, T8 p8, T9 p9);

    public delegate TRes FuncRef1_5_7<T1, in T2, in T3, in T4, T5, in T6, T7, in T8, in T9, out TRes>(ref T1 p1, T2 p2, T3 p3, T4 p4, ref T5 p5, T6 p6, ref T7 p7, T8 p8, T9 p9);

    public delegate TRes FuncRef1_5_8<T1, in T2, in T3, in T4, T5, in T6, in T7, T8, in T9, out TRes>(ref T1 p1, T2 p2, T3 p3, T4 p4, ref T5 p5, T6 p6, T7 p7, ref T8 p8, T9 p9);

    public delegate TRes FuncRef1_5_9<T1, in T2, in T3, in T4, T5, in T6, in T7, in T8, T9, out TRes>(ref T1 p1, T2 p2, T3 p3, T4 p4, ref T5 p5, T6 p6, T7 p7, T8 p8, ref T9 p9);

    public delegate TRes FuncRef1_6_7<T1, in T2, in T3, in T4, in T5, T6, T7, in T8, in T9, out TRes>(ref T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, ref T6 p6, ref T7 p7, T8 p8, T9 p9);

    public delegate TRes FuncRef1_6_8<T1, in T2, in T3, in T4, in T5, T6, in T7, T8, in T9, out TRes>(ref T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, ref T6 p6, T7 p7, ref T8 p8, T9 p9);

    public delegate TRes FuncRef1_6_9<T1, in T2, in T3, in T4, in T5, T6, in T7, in T8, T9, out TRes>(ref T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, ref T6 p6, T7 p7, T8 p8, ref T9 p9);

    public delegate TRes FuncRef1_7_8<T1, in T2, in T3, in T4, in T5, in T6, T7, T8, in T9, out TRes>(ref T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, ref T7 p7, ref T8 p8, T9 p9);

    public delegate TRes FuncRef1_7_9<T1, in T2, in T3, in T4, in T5, in T6, T7, in T8, T9, out TRes>(ref T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, ref T7 p7, T8 p8, ref T9 p9);

    public delegate TRes FuncRef1_8_9<T1, in T2, in T3, in T4, in T5, in T6, in T7, T8, T9, out TRes>(ref T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, ref T8 p8, ref T9 p9);

    public delegate TRes FuncRef2_3_4<in T1, T2, T3, T4, in T5, in T6, in T7, in T8, in T9, out TRes>(T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9);

    public delegate TRes FuncRef2_3_5<in T1, T2, T3, in T4, T5, in T6, in T7, in T8, in T9, out TRes>(T1 p1, ref T2 p2, ref T3 p3, T4 p4, ref T5 p5, T6 p6, T7 p7, T8 p8, T9 p9);

    public delegate TRes FuncRef2_3_6<in T1, T2, T3, in T4, in T5, T6, in T7, in T8, in T9, out TRes>(T1 p1, ref T2 p2, ref T3 p3, T4 p4, T5 p5, ref T6 p6, T7 p7, T8 p8, T9 p9);

    public delegate TRes FuncRef2_3_7<in T1, T2, T3, in T4, in T5, in T6, T7, in T8, in T9, out TRes>(T1 p1, ref T2 p2, ref T3 p3, T4 p4, T5 p5, T6 p6, ref T7 p7, T8 p8, T9 p9);

    public delegate TRes FuncRef2_3_8<in T1, T2, T3, in T4, in T5, in T6, in T7, T8, in T9, out TRes>(T1 p1, ref T2 p2, ref T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, ref T8 p8, T9 p9);

    public delegate TRes FuncRef2_3_9<in T1, T2, T3, in T4, in T5, in T6, in T7, in T8, T9, out TRes>(T1 p1, ref T2 p2, ref T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, ref T9 p9);

    public delegate TRes FuncRef2_4_5<in T1, T2, in T3, T4, T5, in T6, in T7, in T8, in T9, out TRes>(T1 p1, ref T2 p2, T3 p3, ref T4 p4, ref T5 p5, T6 p6, T7 p7, T8 p8, T9 p9);

    public delegate TRes FuncRef2_4_6<in T1, T2, in T3, T4, in T5, T6, in T7, in T8, in T9, out TRes>(T1 p1, ref T2 p2, T3 p3, ref T4 p4, T5 p5, ref T6 p6, T7 p7, T8 p8, T9 p9);

    public delegate TRes FuncRef2_4_7<in T1, T2, in T3, T4, in T5, in T6, T7, in T8, in T9, out TRes>(T1 p1, ref T2 p2, T3 p3, ref T4 p4, T5 p5, T6 p6, ref T7 p7, T8 p8, T9 p9);

    public delegate TRes FuncRef2_4_8<in T1, T2, in T3, T4, in T5, in T6, in T7, T8, in T9, out TRes>(T1 p1, ref T2 p2, T3 p3, ref T4 p4, T5 p5, T6 p6, T7 p7, ref T8 p8, T9 p9);

    public delegate TRes FuncRef2_4_9<in T1, T2, in T3, T4, in T5, in T6, in T7, in T8, T9, out TRes>(T1 p1, ref T2 p2, T3 p3, ref T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, ref T9 p9);

    public delegate TRes FuncRef2_5_6<in T1, T2, in T3, in T4, T5, T6, in T7, in T8, in T9, out TRes>(T1 p1, ref T2 p2, T3 p3, T4 p4, ref T5 p5, ref T6 p6, T7 p7, T8 p8, T9 p9);

    public delegate TRes FuncRef2_5_7<in T1, T2, in T3, in T4, T5, in T6, T7, in T8, in T9, out TRes>(T1 p1, ref T2 p2, T3 p3, T4 p4, ref T5 p5, T6 p6, ref T7 p7, T8 p8, T9 p9);

    public delegate TRes FuncRef2_5_8<in T1, T2, in T3, in T4, T5, in T6, in T7, T8, in T9, out TRes>(T1 p1, ref T2 p2, T3 p3, T4 p4, ref T5 p5, T6 p6, T7 p7, ref T8 p8, T9 p9);

    public delegate TRes FuncRef2_5_9<in T1, T2, in T3, in T4, T5, in T6, in T7, in T8, T9, out TRes>(T1 p1, ref T2 p2, T3 p3, T4 p4, ref T5 p5, T6 p6, T7 p7, T8 p8, ref T9 p9);

    public delegate TRes FuncRef2_6_7<in T1, T2, in T3, in T4, in T5, T6, T7, in T8, in T9, out TRes>(T1 p1, ref T2 p2, T3 p3, T4 p4, T5 p5, ref T6 p6, ref T7 p7, T8 p8, T9 p9);

    public delegate TRes FuncRef2_6_8<in T1, T2, in T3, in T4, in T5, T6, in T7, T8, in T9, out TRes>(T1 p1, ref T2 p2, T3 p3, T4 p4, T5 p5, ref T6 p6, T7 p7, ref T8 p8, T9 p9);

    public delegate TRes FuncRef2_6_9<in T1, T2, in T3, in T4, in T5, T6, in T7, in T8, T9, out TRes>(T1 p1, ref T2 p2, T3 p3, T4 p4, T5 p5, ref T6 p6, T7 p7, T8 p8, ref T9 p9);

    public delegate TRes FuncRef2_7_8<in T1, T2, in T3, in T4, in T5, in T6, T7, T8, in T9, out TRes>(T1 p1, ref T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, ref T7 p7, ref T8 p8, T9 p9);

    public delegate TRes FuncRef2_7_9<in T1, T2, in T3, in T4, in T5, in T6, T7, in T8, T9, out TRes>(T1 p1, ref T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, ref T7 p7, T8 p8, ref T9 p9);

    public delegate TRes FuncRef2_8_9<in T1, T2, in T3, in T4, in T5, in T6, in T7, T8, T9, out TRes>(T1 p1, ref T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, ref T8 p8, ref T9 p9);

    public delegate TRes FuncRef3_4_5<in T1, in T2, T3, T4, T5, in T6, in T7, in T8, in T9, out TRes>(T1 p1, T2 p2, ref T3 p3, ref T4 p4, ref T5 p5, T6 p6, T7 p7, T8 p8, T9 p9);

    public delegate TRes FuncRef3_4_6<in T1, in T2, T3, T4, in T5, T6, in T7, in T8, in T9, out TRes>(T1 p1, T2 p2, ref T3 p3, ref T4 p4, T5 p5, ref T6 p6, T7 p7, T8 p8, T9 p9);

    public delegate TRes FuncRef3_4_7<in T1, in T2, T3, T4, in T5, in T6, T7, in T8, in T9, out TRes>(T1 p1, T2 p2, ref T3 p3, ref T4 p4, T5 p5, T6 p6, ref T7 p7, T8 p8, T9 p9);

    public delegate TRes FuncRef3_4_8<in T1, in T2, T3, T4, in T5, in T6, in T7, T8, in T9, out TRes>(T1 p1, T2 p2, ref T3 p3, ref T4 p4, T5 p5, T6 p6, T7 p7, ref T8 p8, T9 p9);

    public delegate TRes FuncRef3_4_9<in T1, in T2, T3, T4, in T5, in T6, in T7, in T8, T9, out TRes>(T1 p1, T2 p2, ref T3 p3, ref T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, ref T9 p9);

    public delegate TRes FuncRef3_5_6<in T1, in T2, T3, in T4, T5, T6, in T7, in T8, in T9, out TRes>(T1 p1, T2 p2, ref T3 p3, T4 p4, ref T5 p5, ref T6 p6, T7 p7, T8 p8, T9 p9);

    public delegate TRes FuncRef3_5_7<in T1, in T2, T3, in T4, T5, in T6, T7, in T8, in T9, out TRes>(T1 p1, T2 p2, ref T3 p3, T4 p4, ref T5 p5, T6 p6, ref T7 p7, T8 p8, T9 p9);

    public delegate TRes FuncRef3_5_8<in T1, in T2, T3, in T4, T5, in T6, in T7, T8, in T9, out TRes>(T1 p1, T2 p2, ref T3 p3, T4 p4, ref T5 p5, T6 p6, T7 p7, ref T8 p8, T9 p9);

    public delegate TRes FuncRef3_5_9<in T1, in T2, T3, in T4, T5, in T6, in T7, in T8, T9, out TRes>(T1 p1, T2 p2, ref T3 p3, T4 p4, ref T5 p5, T6 p6, T7 p7, T8 p8, ref T9 p9);

    public delegate TRes FuncRef3_6_7<in T1, in T2, T3, in T4, in T5, T6, T7, in T8, in T9, out TRes>(T1 p1, T2 p2, ref T3 p3, T4 p4, T5 p5, ref T6 p6, ref T7 p7, T8 p8, T9 p9);

    public delegate TRes FuncRef3_6_8<in T1, in T2, T3, in T4, in T5, T6, in T7, T8, in T9, out TRes>(T1 p1, T2 p2, ref T3 p3, T4 p4, T5 p5, ref T6 p6, T7 p7, ref T8 p8, T9 p9);

    public delegate TRes FuncRef3_6_9<in T1, in T2, T3, in T4, in T5, T6, in T7, in T8, T9, out TRes>(T1 p1, T2 p2, ref T3 p3, T4 p4, T5 p5, ref T6 p6, T7 p7, T8 p8, ref T9 p9);

    public delegate TRes FuncRef3_7_8<in T1, in T2, T3, in T4, in T5, in T6, T7, T8, in T9, out TRes>(T1 p1, T2 p2, ref T3 p3, T4 p4, T5 p5, T6 p6, ref T7 p7, ref T8 p8, T9 p9);

    public delegate TRes FuncRef3_7_9<in T1, in T2, T3, in T4, in T5, in T6, T7, in T8, T9, out TRes>(T1 p1, T2 p2, ref T3 p3, T4 p4, T5 p5, T6 p6, ref T7 p7, T8 p8, ref T9 p9);

    public delegate TRes FuncRef3_8_9<in T1, in T2, T3, in T4, in T5, in T6, in T7, T8, T9, out TRes>(T1 p1, T2 p2, ref T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, ref T8 p8, ref T9 p9);

    public delegate TRes FuncRef4_5_6<in T1, in T2, in T3, T4, T5, T6, in T7, in T8, in T9, out TRes>(T1 p1, T2 p2, T3 p3, ref T4 p4, ref T5 p5, ref T6 p6, T7 p7, T8 p8, T9 p9);

    public delegate TRes FuncRef4_5_7<in T1, in T2, in T3, T4, T5, in T6, T7, in T8, in T9, out TRes>(T1 p1, T2 p2, T3 p3, ref T4 p4, ref T5 p5, T6 p6, ref T7 p7, T8 p8, T9 p9);

    public delegate TRes FuncRef4_5_8<in T1, in T2, in T3, T4, T5, in T6, in T7, T8, in T9, out TRes>(T1 p1, T2 p2, T3 p3, ref T4 p4, ref T5 p5, T6 p6, T7 p7, ref T8 p8, T9 p9);

    public delegate TRes FuncRef4_5_9<in T1, in T2, in T3, T4, T5, in T6, in T7, in T8, T9, out TRes>(T1 p1, T2 p2, T3 p3, ref T4 p4, ref T5 p5, T6 p6, T7 p7, T8 p8, ref T9 p9);

    public delegate TRes FuncRef4_6_7<in T1, in T2, in T3, T4, in T5, T6, T7, in T8, in T9, out TRes>(T1 p1, T2 p2, T3 p3, ref T4 p4, T5 p5, ref T6 p6, ref T7 p7, T8 p8, T9 p9);

    public delegate TRes FuncRef4_6_8<in T1, in T2, in T3, T4, in T5, T6, in T7, T8, in T9, out TRes>(T1 p1, T2 p2, T3 p3, ref T4 p4, T5 p5, ref T6 p6, T7 p7, ref T8 p8, T9 p9);

    public delegate TRes FuncRef4_6_9<in T1, in T2, in T3, T4, in T5, T6, in T7, in T8, T9, out TRes>(T1 p1, T2 p2, T3 p3, ref T4 p4, T5 p5, ref T6 p6, T7 p7, T8 p8, ref T9 p9);

    public delegate TRes FuncRef4_7_8<in T1, in T2, in T3, T4, in T5, in T6, T7, T8, in T9, out TRes>(T1 p1, T2 p2, T3 p3, ref T4 p4, T5 p5, T6 p6, ref T7 p7, ref T8 p8, T9 p9);

    public delegate TRes FuncRef4_7_9<in T1, in T2, in T3, T4, in T5, in T6, T7, in T8, T9, out TRes>(T1 p1, T2 p2, T3 p3, ref T4 p4, T5 p5, T6 p6, ref T7 p7, T8 p8, ref T9 p9);

    public delegate TRes FuncRef4_8_9<in T1, in T2, in T3, T4, in T5, in T6, in T7, T8, T9, out TRes>(T1 p1, T2 p2, T3 p3, ref T4 p4, T5 p5, T6 p6, T7 p7, ref T8 p8, ref T9 p9);

    public delegate TRes FuncRef5_6_7<in T1, in T2, in T3, in T4, T5, T6, T7, in T8, in T9, out TRes>(T1 p1, T2 p2, T3 p3, T4 p4, ref T5 p5, ref T6 p6, ref T7 p7, T8 p8, T9 p9);

    public delegate TRes FuncRef5_6_8<in T1, in T2, in T3, in T4, T5, T6, in T7, T8, in T9, out TRes>(T1 p1, T2 p2, T3 p3, T4 p4, ref T5 p5, ref T6 p6, T7 p7, ref T8 p8, T9 p9);

    public delegate TRes FuncRef5_6_9<in T1, in T2, in T3, in T4, T5, T6, in T7, in T8, T9, out TRes>(T1 p1, T2 p2, T3 p3, T4 p4, ref T5 p5, ref T6 p6, T7 p7, T8 p8, ref T9 p9);

    public delegate TRes FuncRef5_7_8<in T1, in T2, in T3, in T4, T5, in T6, T7, T8, in T9, out TRes>(T1 p1, T2 p2, T3 p3, T4 p4, ref T5 p5, T6 p6, ref T7 p7, ref T8 p8, T9 p9);

    public delegate TRes FuncRef5_7_9<in T1, in T2, in T3, in T4, T5, in T6, T7, in T8, T9, out TRes>(T1 p1, T2 p2, T3 p3, T4 p4, ref T5 p5, T6 p6, ref T7 p7, T8 p8, ref T9 p9);

    public delegate TRes FuncRef5_8_9<in T1, in T2, in T3, in T4, T5, in T6, in T7, T8, T9, out TRes>(T1 p1, T2 p2, T3 p3, T4 p4, ref T5 p5, T6 p6, T7 p7, ref T8 p8, ref T9 p9);

    public delegate TRes FuncRef6_7_8<in T1, in T2, in T3, in T4, in T5, T6, T7, T8, in T9, out TRes>(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, ref T6 p6, ref T7 p7, ref T8 p8, T9 p9);

    public delegate TRes FuncRef6_7_9<in T1, in T2, in T3, in T4, in T5, T6, T7, in T8, T9, out TRes>(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, ref T6 p6, ref T7 p7, T8 p8, ref T9 p9);

    public delegate TRes FuncRef6_8_9<in T1, in T2, in T3, in T4, in T5, T6, in T7, T8, T9, out TRes>(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, ref T6 p6, T7 p7, ref T8 p8, ref T9 p9);

    public delegate TRes FuncRef7_8_9<in T1, in T2, in T3, in T4, in T5, in T6, T7, T8, T9, out TRes>(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, ref T7 p7, ref T8 p8, ref T9 p9);

    public delegate TRes FuncRef1_2_3_4<T1, T2, T3, T4, in T5, in T6, in T7, in T8, in T9, out TRes>(ref T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9);

    public delegate TRes FuncRef1_2_3_5<T1, T2, T3, in T4, T5, in T6, in T7, in T8, in T9, out TRes>(ref T1 p1, ref T2 p2, ref T3 p3, T4 p4, ref T5 p5, T6 p6, T7 p7, T8 p8, T9 p9);

    public delegate TRes FuncRef1_2_3_6<T1, T2, T3, in T4, in T5, T6, in T7, in T8, in T9, out TRes>(ref T1 p1, ref T2 p2, ref T3 p3, T4 p4, T5 p5, ref T6 p6, T7 p7, T8 p8, T9 p9);

    public delegate TRes FuncRef1_2_3_7<T1, T2, T3, in T4, in T5, in T6, T7, in T8, in T9, out TRes>(ref T1 p1, ref T2 p2, ref T3 p3, T4 p4, T5 p5, T6 p6, ref T7 p7, T8 p8, T9 p9);

    public delegate TRes FuncRef1_2_3_8<T1, T2, T3, in T4, in T5, in T6, in T7, T8, in T9, out TRes>(ref T1 p1, ref T2 p2, ref T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, ref T8 p8, T9 p9);

    public delegate TRes FuncRef1_2_3_9<T1, T2, T3, in T4, in T5, in T6, in T7, in T8, T9, out TRes>(ref T1 p1, ref T2 p2, ref T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, ref T9 p9);

    public delegate TRes FuncRef1_2_4_5<T1, T2, in T3, T4, T5, in T6, in T7, in T8, in T9, out TRes>(ref T1 p1, ref T2 p2, T3 p3, ref T4 p4, ref T5 p5, T6 p6, T7 p7, T8 p8, T9 p9);

    public delegate TRes FuncRef1_2_4_6<T1, T2, in T3, T4, in T5, T6, in T7, in T8, in T9, out TRes>(ref T1 p1, ref T2 p2, T3 p3, ref T4 p4, T5 p5, ref T6 p6, T7 p7, T8 p8, T9 p9);

    public delegate TRes FuncRef1_2_4_7<T1, T2, in T3, T4, in T5, in T6, T7, in T8, in T9, out TRes>(ref T1 p1, ref T2 p2, T3 p3, ref T4 p4, T5 p5, T6 p6, ref T7 p7, T8 p8, T9 p9);

    public delegate TRes FuncRef1_2_4_8<T1, T2, in T3, T4, in T5, in T6, in T7, T8, in T9, out TRes>(ref T1 p1, ref T2 p2, T3 p3, ref T4 p4, T5 p5, T6 p6, T7 p7, ref T8 p8, T9 p9);

    public delegate TRes FuncRef1_2_4_9<T1, T2, in T3, T4, in T5, in T6, in T7, in T8, T9, out TRes>(ref T1 p1, ref T2 p2, T3 p3, ref T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, ref T9 p9);

    public delegate TRes FuncRef1_2_5_6<T1, T2, in T3, in T4, T5, T6, in T7, in T8, in T9, out TRes>(ref T1 p1, ref T2 p2, T3 p3, T4 p4, ref T5 p5, ref T6 p6, T7 p7, T8 p8, T9 p9);

    public delegate TRes FuncRef1_2_5_7<T1, T2, in T3, in T4, T5, in T6, T7, in T8, in T9, out TRes>(ref T1 p1, ref T2 p2, T3 p3, T4 p4, ref T5 p5, T6 p6, ref T7 p7, T8 p8, T9 p9);

    public delegate TRes FuncRef1_2_5_8<T1, T2, in T3, in T4, T5, in T6, in T7, T8, in T9, out TRes>(ref T1 p1, ref T2 p2, T3 p3, T4 p4, ref T5 p5, T6 p6, T7 p7, ref T8 p8, T9 p9);

    public delegate TRes FuncRef1_2_5_9<T1, T2, in T3, in T4, T5, in T6, in T7, in T8, T9, out TRes>(ref T1 p1, ref T2 p2, T3 p3, T4 p4, ref T5 p5, T6 p6, T7 p7, T8 p8, ref T9 p9);

    public delegate TRes FuncRef1_2_6_7<T1, T2, in T3, in T4, in T5, T6, T7, in T8, in T9, out TRes>(ref T1 p1, ref T2 p2, T3 p3, T4 p4, T5 p5, ref T6 p6, ref T7 p7, T8 p8, T9 p9);

    public delegate TRes FuncRef1_2_6_8<T1, T2, in T3, in T4, in T5, T6, in T7, T8, in T9, out TRes>(ref T1 p1, ref T2 p2, T3 p3, T4 p4, T5 p5, ref T6 p6, T7 p7, ref T8 p8, T9 p9);

    public delegate TRes FuncRef1_2_6_9<T1, T2, in T3, in T4, in T5, T6, in T7, in T8, T9, out TRes>(ref T1 p1, ref T2 p2, T3 p3, T4 p4, T5 p5, ref T6 p6, T7 p7, T8 p8, ref T9 p9);

    public delegate TRes FuncRef1_2_7_8<T1, T2, in T3, in T4, in T5, in T6, T7, T8, in T9, out TRes>(ref T1 p1, ref T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, ref T7 p7, ref T8 p8, T9 p9);

    public delegate TRes FuncRef1_2_7_9<T1, T2, in T3, in T4, in T5, in T6, T7, in T8, T9, out TRes>(ref T1 p1, ref T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, ref T7 p7, T8 p8, ref T9 p9);

    public delegate TRes FuncRef1_2_8_9<T1, T2, in T3, in T4, in T5, in T6, in T7, T8, T9, out TRes>(ref T1 p1, ref T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, ref T8 p8, ref T9 p9);

    public delegate TRes FuncRef1_3_4_5<T1, in T2, T3, T4, T5, in T6, in T7, in T8, in T9, out TRes>(ref T1 p1, T2 p2, ref T3 p3, ref T4 p4, ref T5 p5, T6 p6, T7 p7, T8 p8, T9 p9);

    public delegate TRes FuncRef1_3_4_6<T1, in T2, T3, T4, in T5, T6, in T7, in T8, in T9, out TRes>(ref T1 p1, T2 p2, ref T3 p3, ref T4 p4, T5 p5, ref T6 p6, T7 p7, T8 p8, T9 p9);

    public delegate TRes FuncRef1_3_4_7<T1, in T2, T3, T4, in T5, in T6, T7, in T8, in T9, out TRes>(ref T1 p1, T2 p2, ref T3 p3, ref T4 p4, T5 p5, T6 p6, ref T7 p7, T8 p8, T9 p9);

    public delegate TRes FuncRef1_3_4_8<T1, in T2, T3, T4, in T5, in T6, in T7, T8, in T9, out TRes>(ref T1 p1, T2 p2, ref T3 p3, ref T4 p4, T5 p5, T6 p6, T7 p7, ref T8 p8, T9 p9);

    public delegate TRes FuncRef1_3_4_9<T1, in T2, T3, T4, in T5, in T6, in T7, in T8, T9, out TRes>(ref T1 p1, T2 p2, ref T3 p3, ref T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, ref T9 p9);

    public delegate TRes FuncRef1_3_5_6<T1, in T2, T3, in T4, T5, T6, in T7, in T8, in T9, out TRes>(ref T1 p1, T2 p2, ref T3 p3, T4 p4, ref T5 p5, ref T6 p6, T7 p7, T8 p8, T9 p9);

    public delegate TRes FuncRef1_3_5_7<T1, in T2, T3, in T4, T5, in T6, T7, in T8, in T9, out TRes>(ref T1 p1, T2 p2, ref T3 p3, T4 p4, ref T5 p5, T6 p6, ref T7 p7, T8 p8, T9 p9);

    public delegate TRes FuncRef1_3_5_8<T1, in T2, T3, in T4, T5, in T6, in T7, T8, in T9, out TRes>(ref T1 p1, T2 p2, ref T3 p3, T4 p4, ref T5 p5, T6 p6, T7 p7, ref T8 p8, T9 p9);

    public delegate TRes FuncRef1_3_5_9<T1, in T2, T3, in T4, T5, in T6, in T7, in T8, T9, out TRes>(ref T1 p1, T2 p2, ref T3 p3, T4 p4, ref T5 p5, T6 p6, T7 p7, T8 p8, ref T9 p9);

    public delegate TRes FuncRef1_3_6_7<T1, in T2, T3, in T4, in T5, T6, T7, in T8, in T9, out TRes>(ref T1 p1, T2 p2, ref T3 p3, T4 p4, T5 p5, ref T6 p6, ref T7 p7, T8 p8, T9 p9);

    public delegate TRes FuncRef1_3_6_8<T1, in T2, T3, in T4, in T5, T6, in T7, T8, in T9, out TRes>(ref T1 p1, T2 p2, ref T3 p3, T4 p4, T5 p5, ref T6 p6, T7 p7, ref T8 p8, T9 p9);

    public delegate TRes FuncRef1_3_6_9<T1, in T2, T3, in T4, in T5, T6, in T7, in T8, T9, out TRes>(ref T1 p1, T2 p2, ref T3 p3, T4 p4, T5 p5, ref T6 p6, T7 p7, T8 p8, ref T9 p9);

    public delegate TRes FuncRef1_3_7_8<T1, in T2, T3, in T4, in T5, in T6, T7, T8, in T9, out TRes>(ref T1 p1, T2 p2, ref T3 p3, T4 p4, T5 p5, T6 p6, ref T7 p7, ref T8 p8, T9 p9);

    public delegate TRes FuncRef1_3_7_9<T1, in T2, T3, in T4, in T5, in T6, T7, in T8, T9, out TRes>(ref T1 p1, T2 p2, ref T3 p3, T4 p4, T5 p5, T6 p6, ref T7 p7, T8 p8, ref T9 p9);

    public delegate TRes FuncRef1_3_8_9<T1, in T2, T3, in T4, in T5, in T6, in T7, T8, T9, out TRes>(ref T1 p1, T2 p2, ref T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, ref T8 p8, ref T9 p9);

    public delegate TRes FuncRef1_4_5_6<T1, in T2, in T3, T4, T5, T6, in T7, in T8, in T9, out TRes>(ref T1 p1, T2 p2, T3 p3, ref T4 p4, ref T5 p5, ref T6 p6, T7 p7, T8 p8, T9 p9);

    public delegate TRes FuncRef1_4_5_7<T1, in T2, in T3, T4, T5, in T6, T7, in T8, in T9, out TRes>(ref T1 p1, T2 p2, T3 p3, ref T4 p4, ref T5 p5, T6 p6, ref T7 p7, T8 p8, T9 p9);

    public delegate TRes FuncRef1_4_5_8<T1, in T2, in T3, T4, T5, in T6, in T7, T8, in T9, out TRes>(ref T1 p1, T2 p2, T3 p3, ref T4 p4, ref T5 p5, T6 p6, T7 p7, ref T8 p8, T9 p9);

    public delegate TRes FuncRef1_4_5_9<T1, in T2, in T3, T4, T5, in T6, in T7, in T8, T9, out TRes>(ref T1 p1, T2 p2, T3 p3, ref T4 p4, ref T5 p5, T6 p6, T7 p7, T8 p8, ref T9 p9);

    public delegate TRes FuncRef1_4_6_7<T1, in T2, in T3, T4, in T5, T6, T7, in T8, in T9, out TRes>(ref T1 p1, T2 p2, T3 p3, ref T4 p4, T5 p5, ref T6 p6, ref T7 p7, T8 p8, T9 p9);

    public delegate TRes FuncRef1_4_6_8<T1, in T2, in T3, T4, in T5, T6, in T7, T8, in T9, out TRes>(ref T1 p1, T2 p2, T3 p3, ref T4 p4, T5 p5, ref T6 p6, T7 p7, ref T8 p8, T9 p9);

    public delegate TRes FuncRef1_4_6_9<T1, in T2, in T3, T4, in T5, T6, in T7, in T8, T9, out TRes>(ref T1 p1, T2 p2, T3 p3, ref T4 p4, T5 p5, ref T6 p6, T7 p7, T8 p8, ref T9 p9);

    public delegate TRes FuncRef1_4_7_8<T1, in T2, in T3, T4, in T5, in T6, T7, T8, in T9, out TRes>(ref T1 p1, T2 p2, T3 p3, ref T4 p4, T5 p5, T6 p6, ref T7 p7, ref T8 p8, T9 p9);

    public delegate TRes FuncRef1_4_7_9<T1, in T2, in T3, T4, in T5, in T6, T7, in T8, T9, out TRes>(ref T1 p1, T2 p2, T3 p3, ref T4 p4, T5 p5, T6 p6, ref T7 p7, T8 p8, ref T9 p9);

    public delegate TRes FuncRef1_4_8_9<T1, in T2, in T3, T4, in T5, in T6, in T7, T8, T9, out TRes>(ref T1 p1, T2 p2, T3 p3, ref T4 p4, T5 p5, T6 p6, T7 p7, ref T8 p8, ref T9 p9);

    public delegate TRes FuncRef1_5_6_7<T1, in T2, in T3, in T4, T5, T6, T7, in T8, in T9, out TRes>(ref T1 p1, T2 p2, T3 p3, T4 p4, ref T5 p5, ref T6 p6, ref T7 p7, T8 p8, T9 p9);

    public delegate TRes FuncRef1_5_6_8<T1, in T2, in T3, in T4, T5, T6, in T7, T8, in T9, out TRes>(ref T1 p1, T2 p2, T3 p3, T4 p4, ref T5 p5, ref T6 p6, T7 p7, ref T8 p8, T9 p9);

    public delegate TRes FuncRef1_5_6_9<T1, in T2, in T3, in T4, T5, T6, in T7, in T8, T9, out TRes>(ref T1 p1, T2 p2, T3 p3, T4 p4, ref T5 p5, ref T6 p6, T7 p7, T8 p8, ref T9 p9);

    public delegate TRes FuncRef1_5_7_8<T1, in T2, in T3, in T4, T5, in T6, T7, T8, in T9, out TRes>(ref T1 p1, T2 p2, T3 p3, T4 p4, ref T5 p5, T6 p6, ref T7 p7, ref T8 p8, T9 p9);

    public delegate TRes FuncRef1_5_7_9<T1, in T2, in T3, in T4, T5, in T6, T7, in T8, T9, out TRes>(ref T1 p1, T2 p2, T3 p3, T4 p4, ref T5 p5, T6 p6, ref T7 p7, T8 p8, ref T9 p9);

    public delegate TRes FuncRef1_5_8_9<T1, in T2, in T3, in T4, T5, in T6, in T7, T8, T9, out TRes>(ref T1 p1, T2 p2, T3 p3, T4 p4, ref T5 p5, T6 p6, T7 p7, ref T8 p8, ref T9 p9);

    public delegate TRes FuncRef1_6_7_8<T1, in T2, in T3, in T4, in T5, T6, T7, T8, in T9, out TRes>(ref T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, ref T6 p6, ref T7 p7, ref T8 p8, T9 p9);

    public delegate TRes FuncRef1_6_7_9<T1, in T2, in T3, in T4, in T5, T6, T7, in T8, T9, out TRes>(ref T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, ref T6 p6, ref T7 p7, T8 p8, ref T9 p9);

    public delegate TRes FuncRef1_6_8_9<T1, in T2, in T3, in T4, in T5, T6, in T7, T8, T9, out TRes>(ref T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, ref T6 p6, T7 p7, ref T8 p8, ref T9 p9);

    public delegate TRes FuncRef1_7_8_9<T1, in T2, in T3, in T4, in T5, in T6, T7, T8, T9, out TRes>(ref T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, ref T7 p7, ref T8 p8, ref T9 p9);

    public delegate TRes FuncRef2_3_4_5<in T1, T2, T3, T4, T5, in T6, in T7, in T8, in T9, out TRes>(T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, ref T5 p5, T6 p6, T7 p7, T8 p8, T9 p9);

    public delegate TRes FuncRef2_3_4_6<in T1, T2, T3, T4, in T5, T6, in T7, in T8, in T9, out TRes>(T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, T5 p5, ref T6 p6, T7 p7, T8 p8, T9 p9);

    public delegate TRes FuncRef2_3_4_7<in T1, T2, T3, T4, in T5, in T6, T7, in T8, in T9, out TRes>(T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, T5 p5, T6 p6, ref T7 p7, T8 p8, T9 p9);

    public delegate TRes FuncRef2_3_4_8<in T1, T2, T3, T4, in T5, in T6, in T7, T8, in T9, out TRes>(T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, T5 p5, T6 p6, T7 p7, ref T8 p8, T9 p9);

    public delegate TRes FuncRef2_3_4_9<in T1, T2, T3, T4, in T5, in T6, in T7, in T8, T9, out TRes>(T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, ref T9 p9);

    public delegate TRes FuncRef2_3_5_6<in T1, T2, T3, in T4, T5, T6, in T7, in T8, in T9, out TRes>(T1 p1, ref T2 p2, ref T3 p3, T4 p4, ref T5 p5, ref T6 p6, T7 p7, T8 p8, T9 p9);

    public delegate TRes FuncRef2_3_5_7<in T1, T2, T3, in T4, T5, in T6, T7, in T8, in T9, out TRes>(T1 p1, ref T2 p2, ref T3 p3, T4 p4, ref T5 p5, T6 p6, ref T7 p7, T8 p8, T9 p9);

    public delegate TRes FuncRef2_3_5_8<in T1, T2, T3, in T4, T5, in T6, in T7, T8, in T9, out TRes>(T1 p1, ref T2 p2, ref T3 p3, T4 p4, ref T5 p5, T6 p6, T7 p7, ref T8 p8, T9 p9);

    public delegate TRes FuncRef2_3_5_9<in T1, T2, T3, in T4, T5, in T6, in T7, in T8, T9, out TRes>(T1 p1, ref T2 p2, ref T3 p3, T4 p4, ref T5 p5, T6 p6, T7 p7, T8 p8, ref T9 p9);

    public delegate TRes FuncRef2_3_6_7<in T1, T2, T3, in T4, in T5, T6, T7, in T8, in T9, out TRes>(T1 p1, ref T2 p2, ref T3 p3, T4 p4, T5 p5, ref T6 p6, ref T7 p7, T8 p8, T9 p9);

    public delegate TRes FuncRef2_3_6_8<in T1, T2, T3, in T4, in T5, T6, in T7, T8, in T9, out TRes>(T1 p1, ref T2 p2, ref T3 p3, T4 p4, T5 p5, ref T6 p6, T7 p7, ref T8 p8, T9 p9);

    public delegate TRes FuncRef2_3_6_9<in T1, T2, T3, in T4, in T5, T6, in T7, in T8, T9, out TRes>(T1 p1, ref T2 p2, ref T3 p3, T4 p4, T5 p5, ref T6 p6, T7 p7, T8 p8, ref T9 p9);

    public delegate TRes FuncRef2_3_7_8<in T1, T2, T3, in T4, in T5, in T6, T7, T8, in T9, out TRes>(T1 p1, ref T2 p2, ref T3 p3, T4 p4, T5 p5, T6 p6, ref T7 p7, ref T8 p8, T9 p9);

    public delegate TRes FuncRef2_3_7_9<in T1, T2, T3, in T4, in T5, in T6, T7, in T8, T9, out TRes>(T1 p1, ref T2 p2, ref T3 p3, T4 p4, T5 p5, T6 p6, ref T7 p7, T8 p8, ref T9 p9);

    public delegate TRes FuncRef2_3_8_9<in T1, T2, T3, in T4, in T5, in T6, in T7, T8, T9, out TRes>(T1 p1, ref T2 p2, ref T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, ref T8 p8, ref T9 p9);

    public delegate TRes FuncRef2_4_5_6<in T1, T2, in T3, T4, T5, T6, in T7, in T8, in T9, out TRes>(T1 p1, ref T2 p2, T3 p3, ref T4 p4, ref T5 p5, ref T6 p6, T7 p7, T8 p8, T9 p9);

    public delegate TRes FuncRef2_4_5_7<in T1, T2, in T3, T4, T5, in T6, T7, in T8, in T9, out TRes>(T1 p1, ref T2 p2, T3 p3, ref T4 p4, ref T5 p5, T6 p6, ref T7 p7, T8 p8, T9 p9);

    public delegate TRes FuncRef2_4_5_8<in T1, T2, in T3, T4, T5, in T6, in T7, T8, in T9, out TRes>(T1 p1, ref T2 p2, T3 p3, ref T4 p4, ref T5 p5, T6 p6, T7 p7, ref T8 p8, T9 p9);

    public delegate TRes FuncRef2_4_5_9<in T1, T2, in T3, T4, T5, in T6, in T7, in T8, T9, out TRes>(T1 p1, ref T2 p2, T3 p3, ref T4 p4, ref T5 p5, T6 p6, T7 p7, T8 p8, ref T9 p9);

    public delegate TRes FuncRef2_4_6_7<in T1, T2, in T3, T4, in T5, T6, T7, in T8, in T9, out TRes>(T1 p1, ref T2 p2, T3 p3, ref T4 p4, T5 p5, ref T6 p6, ref T7 p7, T8 p8, T9 p9);

    public delegate TRes FuncRef2_4_6_8<in T1, T2, in T3, T4, in T5, T6, in T7, T8, in T9, out TRes>(T1 p1, ref T2 p2, T3 p3, ref T4 p4, T5 p5, ref T6 p6, T7 p7, ref T8 p8, T9 p9);

    public delegate TRes FuncRef2_4_6_9<in T1, T2, in T3, T4, in T5, T6, in T7, in T8, T9, out TRes>(T1 p1, ref T2 p2, T3 p3, ref T4 p4, T5 p5, ref T6 p6, T7 p7, T8 p8, ref T9 p9);

    public delegate TRes FuncRef2_4_7_8<in T1, T2, in T3, T4, in T5, in T6, T7, T8, in T9, out TRes>(T1 p1, ref T2 p2, T3 p3, ref T4 p4, T5 p5, T6 p6, ref T7 p7, ref T8 p8, T9 p9);

    public delegate TRes FuncRef2_4_7_9<in T1, T2, in T3, T4, in T5, in T6, T7, in T8, T9, out TRes>(T1 p1, ref T2 p2, T3 p3, ref T4 p4, T5 p5, T6 p6, ref T7 p7, T8 p8, ref T9 p9);

    public delegate TRes FuncRef2_4_8_9<in T1, T2, in T3, T4, in T5, in T6, in T7, T8, T9, out TRes>(T1 p1, ref T2 p2, T3 p3, ref T4 p4, T5 p5, T6 p6, T7 p7, ref T8 p8, ref T9 p9);

    public delegate TRes FuncRef2_5_6_7<in T1, T2, in T3, in T4, T5, T6, T7, in T8, in T9, out TRes>(T1 p1, ref T2 p2, T3 p3, T4 p4, ref T5 p5, ref T6 p6, ref T7 p7, T8 p8, T9 p9);

    public delegate TRes FuncRef2_5_6_8<in T1, T2, in T3, in T4, T5, T6, in T7, T8, in T9, out TRes>(T1 p1, ref T2 p2, T3 p3, T4 p4, ref T5 p5, ref T6 p6, T7 p7, ref T8 p8, T9 p9);

    public delegate TRes FuncRef2_5_6_9<in T1, T2, in T3, in T4, T5, T6, in T7, in T8, T9, out TRes>(T1 p1, ref T2 p2, T3 p3, T4 p4, ref T5 p5, ref T6 p6, T7 p7, T8 p8, ref T9 p9);

    public delegate TRes FuncRef2_5_7_8<in T1, T2, in T3, in T4, T5, in T6, T7, T8, in T9, out TRes>(T1 p1, ref T2 p2, T3 p3, T4 p4, ref T5 p5, T6 p6, ref T7 p7, ref T8 p8, T9 p9);

    public delegate TRes FuncRef2_5_7_9<in T1, T2, in T3, in T4, T5, in T6, T7, in T8, T9, out TRes>(T1 p1, ref T2 p2, T3 p3, T4 p4, ref T5 p5, T6 p6, ref T7 p7, T8 p8, ref T9 p9);

    public delegate TRes FuncRef2_5_8_9<in T1, T2, in T3, in T4, T5, in T6, in T7, T8, T9, out TRes>(T1 p1, ref T2 p2, T3 p3, T4 p4, ref T5 p5, T6 p6, T7 p7, ref T8 p8, ref T9 p9);

    public delegate TRes FuncRef2_6_7_8<in T1, T2, in T3, in T4, in T5, T6, T7, T8, in T9, out TRes>(T1 p1, ref T2 p2, T3 p3, T4 p4, T5 p5, ref T6 p6, ref T7 p7, ref T8 p8, T9 p9);

    public delegate TRes FuncRef2_6_7_9<in T1, T2, in T3, in T4, in T5, T6, T7, in T8, T9, out TRes>(T1 p1, ref T2 p2, T3 p3, T4 p4, T5 p5, ref T6 p6, ref T7 p7, T8 p8, ref T9 p9);

    public delegate TRes FuncRef2_6_8_9<in T1, T2, in T3, in T4, in T5, T6, in T7, T8, T9, out TRes>(T1 p1, ref T2 p2, T3 p3, T4 p4, T5 p5, ref T6 p6, T7 p7, ref T8 p8, ref T9 p9);

    public delegate TRes FuncRef2_7_8_9<in T1, T2, in T3, in T4, in T5, in T6, T7, T8, T9, out TRes>(T1 p1, ref T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, ref T7 p7, ref T8 p8, ref T9 p9);

    public delegate TRes FuncRef3_4_5_6<in T1, in T2, T3, T4, T5, T6, in T7, in T8, in T9, out TRes>(T1 p1, T2 p2, ref T3 p3, ref T4 p4, ref T5 p5, ref T6 p6, T7 p7, T8 p8, T9 p9);

    public delegate TRes FuncRef3_4_5_7<in T1, in T2, T3, T4, T5, in T6, T7, in T8, in T9, out TRes>(T1 p1, T2 p2, ref T3 p3, ref T4 p4, ref T5 p5, T6 p6, ref T7 p7, T8 p8, T9 p9);

    public delegate TRes FuncRef3_4_5_8<in T1, in T2, T3, T4, T5, in T6, in T7, T8, in T9, out TRes>(T1 p1, T2 p2, ref T3 p3, ref T4 p4, ref T5 p5, T6 p6, T7 p7, ref T8 p8, T9 p9);

    public delegate TRes FuncRef3_4_5_9<in T1, in T2, T3, T4, T5, in T6, in T7, in T8, T9, out TRes>(T1 p1, T2 p2, ref T3 p3, ref T4 p4, ref T5 p5, T6 p6, T7 p7, T8 p8, ref T9 p9);

    public delegate TRes FuncRef3_4_6_7<in T1, in T2, T3, T4, in T5, T6, T7, in T8, in T9, out TRes>(T1 p1, T2 p2, ref T3 p3, ref T4 p4, T5 p5, ref T6 p6, ref T7 p7, T8 p8, T9 p9);

    public delegate TRes FuncRef3_4_6_8<in T1, in T2, T3, T4, in T5, T6, in T7, T8, in T9, out TRes>(T1 p1, T2 p2, ref T3 p3, ref T4 p4, T5 p5, ref T6 p6, T7 p7, ref T8 p8, T9 p9);

    public delegate TRes FuncRef3_4_6_9<in T1, in T2, T3, T4, in T5, T6, in T7, in T8, T9, out TRes>(T1 p1, T2 p2, ref T3 p3, ref T4 p4, T5 p5, ref T6 p6, T7 p7, T8 p8, ref T9 p9);

    public delegate TRes FuncRef3_4_7_8<in T1, in T2, T3, T4, in T5, in T6, T7, T8, in T9, out TRes>(T1 p1, T2 p2, ref T3 p3, ref T4 p4, T5 p5, T6 p6, ref T7 p7, ref T8 p8, T9 p9);

    public delegate TRes FuncRef3_4_7_9<in T1, in T2, T3, T4, in T5, in T6, T7, in T8, T9, out TRes>(T1 p1, T2 p2, ref T3 p3, ref T4 p4, T5 p5, T6 p6, ref T7 p7, T8 p8, ref T9 p9);

    public delegate TRes FuncRef3_4_8_9<in T1, in T2, T3, T4, in T5, in T6, in T7, T8, T9, out TRes>(T1 p1, T2 p2, ref T3 p3, ref T4 p4, T5 p5, T6 p6, T7 p7, ref T8 p8, ref T9 p9);

    public delegate TRes FuncRef3_5_6_7<in T1, in T2, T3, in T4, T5, T6, T7, in T8, in T9, out TRes>(T1 p1, T2 p2, ref T3 p3, T4 p4, ref T5 p5, ref T6 p6, ref T7 p7, T8 p8, T9 p9);

    public delegate TRes FuncRef3_5_6_8<in T1, in T2, T3, in T4, T5, T6, in T7, T8, in T9, out TRes>(T1 p1, T2 p2, ref T3 p3, T4 p4, ref T5 p5, ref T6 p6, T7 p7, ref T8 p8, T9 p9);

    public delegate TRes FuncRef3_5_6_9<in T1, in T2, T3, in T4, T5, T6, in T7, in T8, T9, out TRes>(T1 p1, T2 p2, ref T3 p3, T4 p4, ref T5 p5, ref T6 p6, T7 p7, T8 p8, ref T9 p9);

    public delegate TRes FuncRef3_5_7_8<in T1, in T2, T3, in T4, T5, in T6, T7, T8, in T9, out TRes>(T1 p1, T2 p2, ref T3 p3, T4 p4, ref T5 p5, T6 p6, ref T7 p7, ref T8 p8, T9 p9);

    public delegate TRes FuncRef3_5_7_9<in T1, in T2, T3, in T4, T5, in T6, T7, in T8, T9, out TRes>(T1 p1, T2 p2, ref T3 p3, T4 p4, ref T5 p5, T6 p6, ref T7 p7, T8 p8, ref T9 p9);

    public delegate TRes FuncRef3_5_8_9<in T1, in T2, T3, in T4, T5, in T6, in T7, T8, T9, out TRes>(T1 p1, T2 p2, ref T3 p3, T4 p4, ref T5 p5, T6 p6, T7 p7, ref T8 p8, ref T9 p9);

    public delegate TRes FuncRef3_6_7_8<in T1, in T2, T3, in T4, in T5, T6, T7, T8, in T9, out TRes>(T1 p1, T2 p2, ref T3 p3, T4 p4, T5 p5, ref T6 p6, ref T7 p7, ref T8 p8, T9 p9);

    public delegate TRes FuncRef3_6_7_9<in T1, in T2, T3, in T4, in T5, T6, T7, in T8, T9, out TRes>(T1 p1, T2 p2, ref T3 p3, T4 p4, T5 p5, ref T6 p6, ref T7 p7, T8 p8, ref T9 p9);

    public delegate TRes FuncRef3_6_8_9<in T1, in T2, T3, in T4, in T5, T6, in T7, T8, T9, out TRes>(T1 p1, T2 p2, ref T3 p3, T4 p4, T5 p5, ref T6 p6, T7 p7, ref T8 p8, ref T9 p9);

    public delegate TRes FuncRef3_7_8_9<in T1, in T2, T3, in T4, in T5, in T6, T7, T8, T9, out TRes>(T1 p1, T2 p2, ref T3 p3, T4 p4, T5 p5, T6 p6, ref T7 p7, ref T8 p8, ref T9 p9);

    public delegate TRes FuncRef4_5_6_7<in T1, in T2, in T3, T4, T5, T6, T7, in T8, in T9, out TRes>(T1 p1, T2 p2, T3 p3, ref T4 p4, ref T5 p5, ref T6 p6, ref T7 p7, T8 p8, T9 p9);

    public delegate TRes FuncRef4_5_6_8<in T1, in T2, in T3, T4, T5, T6, in T7, T8, in T9, out TRes>(T1 p1, T2 p2, T3 p3, ref T4 p4, ref T5 p5, ref T6 p6, T7 p7, ref T8 p8, T9 p9);

    public delegate TRes FuncRef4_5_6_9<in T1, in T2, in T3, T4, T5, T6, in T7, in T8, T9, out TRes>(T1 p1, T2 p2, T3 p3, ref T4 p4, ref T5 p5, ref T6 p6, T7 p7, T8 p8, ref T9 p9);

    public delegate TRes FuncRef4_5_7_8<in T1, in T2, in T3, T4, T5, in T6, T7, T8, in T9, out TRes>(T1 p1, T2 p2, T3 p3, ref T4 p4, ref T5 p5, T6 p6, ref T7 p7, ref T8 p8, T9 p9);

    public delegate TRes FuncRef4_5_7_9<in T1, in T2, in T3, T4, T5, in T6, T7, in T8, T9, out TRes>(T1 p1, T2 p2, T3 p3, ref T4 p4, ref T5 p5, T6 p6, ref T7 p7, T8 p8, ref T9 p9);

    public delegate TRes FuncRef4_5_8_9<in T1, in T2, in T3, T4, T5, in T6, in T7, T8, T9, out TRes>(T1 p1, T2 p2, T3 p3, ref T4 p4, ref T5 p5, T6 p6, T7 p7, ref T8 p8, ref T9 p9);

    public delegate TRes FuncRef4_6_7_8<in T1, in T2, in T3, T4, in T5, T6, T7, T8, in T9, out TRes>(T1 p1, T2 p2, T3 p3, ref T4 p4, T5 p5, ref T6 p6, ref T7 p7, ref T8 p8, T9 p9);

    public delegate TRes FuncRef4_6_7_9<in T1, in T2, in T3, T4, in T5, T6, T7, in T8, T9, out TRes>(T1 p1, T2 p2, T3 p3, ref T4 p4, T5 p5, ref T6 p6, ref T7 p7, T8 p8, ref T9 p9);

    public delegate TRes FuncRef4_6_8_9<in T1, in T2, in T3, T4, in T5, T6, in T7, T8, T9, out TRes>(T1 p1, T2 p2, T3 p3, ref T4 p4, T5 p5, ref T6 p6, T7 p7, ref T8 p8, ref T9 p9);

    public delegate TRes FuncRef4_7_8_9<in T1, in T2, in T3, T4, in T5, in T6, T7, T8, T9, out TRes>(T1 p1, T2 p2, T3 p3, ref T4 p4, T5 p5, T6 p6, ref T7 p7, ref T8 p8, ref T9 p9);

    public delegate TRes FuncRef5_6_7_8<in T1, in T2, in T3, in T4, T5, T6, T7, T8, in T9, out TRes>(T1 p1, T2 p2, T3 p3, T4 p4, ref T5 p5, ref T6 p6, ref T7 p7, ref T8 p8, T9 p9);

    public delegate TRes FuncRef5_6_7_9<in T1, in T2, in T3, in T4, T5, T6, T7, in T8, T9, out TRes>(T1 p1, T2 p2, T3 p3, T4 p4, ref T5 p5, ref T6 p6, ref T7 p7, T8 p8, ref T9 p9);

    public delegate TRes FuncRef5_6_8_9<in T1, in T2, in T3, in T4, T5, T6, in T7, T8, T9, out TRes>(T1 p1, T2 p2, T3 p3, T4 p4, ref T5 p5, ref T6 p6, T7 p7, ref T8 p8, ref T9 p9);

    public delegate TRes FuncRef5_7_8_9<in T1, in T2, in T3, in T4, T5, in T6, T7, T8, T9, out TRes>(T1 p1, T2 p2, T3 p3, T4 p4, ref T5 p5, T6 p6, ref T7 p7, ref T8 p8, ref T9 p9);

    public delegate TRes FuncRef6_7_8_9<in T1, in T2, in T3, in T4, in T5, T6, T7, T8, T9, out TRes>(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, ref T6 p6, ref T7 p7, ref T8 p8, ref T9 p9);

    public delegate TRes FuncRef1_2_3_4_5<T1, T2, T3, T4, T5, in T6, in T7, in T8, in T9, out TRes>(ref T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, ref T5 p5, T6 p6, T7 p7, T8 p8, T9 p9);

    public delegate TRes FuncRef1_2_3_4_6<T1, T2, T3, T4, in T5, T6, in T7, in T8, in T9, out TRes>(ref T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, T5 p5, ref T6 p6, T7 p7, T8 p8, T9 p9);

    public delegate TRes FuncRef1_2_3_4_7<T1, T2, T3, T4, in T5, in T6, T7, in T8, in T9, out TRes>(ref T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, T5 p5, T6 p6, ref T7 p7, T8 p8, T9 p9);

    public delegate TRes FuncRef1_2_3_4_8<T1, T2, T3, T4, in T5, in T6, in T7, T8, in T9, out TRes>(ref T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, T5 p5, T6 p6, T7 p7, ref T8 p8, T9 p9);

    public delegate TRes FuncRef1_2_3_4_9<T1, T2, T3, T4, in T5, in T6, in T7, in T8, T9, out TRes>(ref T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, ref T9 p9);

    public delegate TRes FuncRef1_2_3_5_6<T1, T2, T3, in T4, T5, T6, in T7, in T8, in T9, out TRes>(ref T1 p1, ref T2 p2, ref T3 p3, T4 p4, ref T5 p5, ref T6 p6, T7 p7, T8 p8, T9 p9);

    public delegate TRes FuncRef1_2_3_5_7<T1, T2, T3, in T4, T5, in T6, T7, in T8, in T9, out TRes>(ref T1 p1, ref T2 p2, ref T3 p3, T4 p4, ref T5 p5, T6 p6, ref T7 p7, T8 p8, T9 p9);

    public delegate TRes FuncRef1_2_3_5_8<T1, T2, T3, in T4, T5, in T6, in T7, T8, in T9, out TRes>(ref T1 p1, ref T2 p2, ref T3 p3, T4 p4, ref T5 p5, T6 p6, T7 p7, ref T8 p8, T9 p9);

    public delegate TRes FuncRef1_2_3_5_9<T1, T2, T3, in T4, T5, in T6, in T7, in T8, T9, out TRes>(ref T1 p1, ref T2 p2, ref T3 p3, T4 p4, ref T5 p5, T6 p6, T7 p7, T8 p8, ref T9 p9);

    public delegate TRes FuncRef1_2_3_6_7<T1, T2, T3, in T4, in T5, T6, T7, in T8, in T9, out TRes>(ref T1 p1, ref T2 p2, ref T3 p3, T4 p4, T5 p5, ref T6 p6, ref T7 p7, T8 p8, T9 p9);

    public delegate TRes FuncRef1_2_3_6_8<T1, T2, T3, in T4, in T5, T6, in T7, T8, in T9, out TRes>(ref T1 p1, ref T2 p2, ref T3 p3, T4 p4, T5 p5, ref T6 p6, T7 p7, ref T8 p8, T9 p9);

    public delegate TRes FuncRef1_2_3_6_9<T1, T2, T3, in T4, in T5, T6, in T7, in T8, T9, out TRes>(ref T1 p1, ref T2 p2, ref T3 p3, T4 p4, T5 p5, ref T6 p6, T7 p7, T8 p8, ref T9 p9);

    public delegate TRes FuncRef1_2_3_7_8<T1, T2, T3, in T4, in T5, in T6, T7, T8, in T9, out TRes>(ref T1 p1, ref T2 p2, ref T3 p3, T4 p4, T5 p5, T6 p6, ref T7 p7, ref T8 p8, T9 p9);

    public delegate TRes FuncRef1_2_3_7_9<T1, T2, T3, in T4, in T5, in T6, T7, in T8, T9, out TRes>(ref T1 p1, ref T2 p2, ref T3 p3, T4 p4, T5 p5, T6 p6, ref T7 p7, T8 p8, ref T9 p9);

    public delegate TRes FuncRef1_2_3_8_9<T1, T2, T3, in T4, in T5, in T6, in T7, T8, T9, out TRes>(ref T1 p1, ref T2 p2, ref T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, ref T8 p8, ref T9 p9);

    public delegate TRes FuncRef1_2_4_5_6<T1, T2, in T3, T4, T5, T6, in T7, in T8, in T9, out TRes>(ref T1 p1, ref T2 p2, T3 p3, ref T4 p4, ref T5 p5, ref T6 p6, T7 p7, T8 p8, T9 p9);

    public delegate TRes FuncRef1_2_4_5_7<T1, T2, in T3, T4, T5, in T6, T7, in T8, in T9, out TRes>(ref T1 p1, ref T2 p2, T3 p3, ref T4 p4, ref T5 p5, T6 p6, ref T7 p7, T8 p8, T9 p9);

    public delegate TRes FuncRef1_2_4_5_8<T1, T2, in T3, T4, T5, in T6, in T7, T8, in T9, out TRes>(ref T1 p1, ref T2 p2, T3 p3, ref T4 p4, ref T5 p5, T6 p6, T7 p7, ref T8 p8, T9 p9);

    public delegate TRes FuncRef1_2_4_5_9<T1, T2, in T3, T4, T5, in T6, in T7, in T8, T9, out TRes>(ref T1 p1, ref T2 p2, T3 p3, ref T4 p4, ref T5 p5, T6 p6, T7 p7, T8 p8, ref T9 p9);

    public delegate TRes FuncRef1_2_4_6_7<T1, T2, in T3, T4, in T5, T6, T7, in T8, in T9, out TRes>(ref T1 p1, ref T2 p2, T3 p3, ref T4 p4, T5 p5, ref T6 p6, ref T7 p7, T8 p8, T9 p9);

    public delegate TRes FuncRef1_2_4_6_8<T1, T2, in T3, T4, in T5, T6, in T7, T8, in T9, out TRes>(ref T1 p1, ref T2 p2, T3 p3, ref T4 p4, T5 p5, ref T6 p6, T7 p7, ref T8 p8, T9 p9);

    public delegate TRes FuncRef1_2_4_6_9<T1, T2, in T3, T4, in T5, T6, in T7, in T8, T9, out TRes>(ref T1 p1, ref T2 p2, T3 p3, ref T4 p4, T5 p5, ref T6 p6, T7 p7, T8 p8, ref T9 p9);

    public delegate TRes FuncRef1_2_4_7_8<T1, T2, in T3, T4, in T5, in T6, T7, T8, in T9, out TRes>(ref T1 p1, ref T2 p2, T3 p3, ref T4 p4, T5 p5, T6 p6, ref T7 p7, ref T8 p8, T9 p9);

    public delegate TRes FuncRef1_2_4_7_9<T1, T2, in T3, T4, in T5, in T6, T7, in T8, T9, out TRes>(ref T1 p1, ref T2 p2, T3 p3, ref T4 p4, T5 p5, T6 p6, ref T7 p7, T8 p8, ref T9 p9);

    public delegate TRes FuncRef1_2_4_8_9<T1, T2, in T3, T4, in T5, in T6, in T7, T8, T9, out TRes>(ref T1 p1, ref T2 p2, T3 p3, ref T4 p4, T5 p5, T6 p6, T7 p7, ref T8 p8, ref T9 p9);

    public delegate TRes FuncRef1_2_5_6_7<T1, T2, in T3, in T4, T5, T6, T7, in T8, in T9, out TRes>(ref T1 p1, ref T2 p2, T3 p3, T4 p4, ref T5 p5, ref T6 p6, ref T7 p7, T8 p8, T9 p9);

    public delegate TRes FuncRef1_2_5_6_8<T1, T2, in T3, in T4, T5, T6, in T7, T8, in T9, out TRes>(ref T1 p1, ref T2 p2, T3 p3, T4 p4, ref T5 p5, ref T6 p6, T7 p7, ref T8 p8, T9 p9);

    public delegate TRes FuncRef1_2_5_6_9<T1, T2, in T3, in T4, T5, T6, in T7, in T8, T9, out TRes>(ref T1 p1, ref T2 p2, T3 p3, T4 p4, ref T5 p5, ref T6 p6, T7 p7, T8 p8, ref T9 p9);

    public delegate TRes FuncRef1_2_5_7_8<T1, T2, in T3, in T4, T5, in T6, T7, T8, in T9, out TRes>(ref T1 p1, ref T2 p2, T3 p3, T4 p4, ref T5 p5, T6 p6, ref T7 p7, ref T8 p8, T9 p9);

    public delegate TRes FuncRef1_2_5_7_9<T1, T2, in T3, in T4, T5, in T6, T7, in T8, T9, out TRes>(ref T1 p1, ref T2 p2, T3 p3, T4 p4, ref T5 p5, T6 p6, ref T7 p7, T8 p8, ref T9 p9);

    public delegate TRes FuncRef1_2_5_8_9<T1, T2, in T3, in T4, T5, in T6, in T7, T8, T9, out TRes>(ref T1 p1, ref T2 p2, T3 p3, T4 p4, ref T5 p5, T6 p6, T7 p7, ref T8 p8, ref T9 p9);

    public delegate TRes FuncRef1_2_6_7_8<T1, T2, in T3, in T4, in T5, T6, T7, T8, in T9, out TRes>(ref T1 p1, ref T2 p2, T3 p3, T4 p4, T5 p5, ref T6 p6, ref T7 p7, ref T8 p8, T9 p9);

    public delegate TRes FuncRef1_2_6_7_9<T1, T2, in T3, in T4, in T5, T6, T7, in T8, T9, out TRes>(ref T1 p1, ref T2 p2, T3 p3, T4 p4, T5 p5, ref T6 p6, ref T7 p7, T8 p8, ref T9 p9);

    public delegate TRes FuncRef1_2_6_8_9<T1, T2, in T3, in T4, in T5, T6, in T7, T8, T9, out TRes>(ref T1 p1, ref T2 p2, T3 p3, T4 p4, T5 p5, ref T6 p6, T7 p7, ref T8 p8, ref T9 p9);

    public delegate TRes FuncRef1_2_7_8_9<T1, T2, in T3, in T4, in T5, in T6, T7, T8, T9, out TRes>(ref T1 p1, ref T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, ref T7 p7, ref T8 p8, ref T9 p9);

    public delegate TRes FuncRef1_3_4_5_6<T1, in T2, T3, T4, T5, T6, in T7, in T8, in T9, out TRes>(ref T1 p1, T2 p2, ref T3 p3, ref T4 p4, ref T5 p5, ref T6 p6, T7 p7, T8 p8, T9 p9);

    public delegate TRes FuncRef1_3_4_5_7<T1, in T2, T3, T4, T5, in T6, T7, in T8, in T9, out TRes>(ref T1 p1, T2 p2, ref T3 p3, ref T4 p4, ref T5 p5, T6 p6, ref T7 p7, T8 p8, T9 p9);

    public delegate TRes FuncRef1_3_4_5_8<T1, in T2, T3, T4, T5, in T6, in T7, T8, in T9, out TRes>(ref T1 p1, T2 p2, ref T3 p3, ref T4 p4, ref T5 p5, T6 p6, T7 p7, ref T8 p8, T9 p9);

    public delegate TRes FuncRef1_3_4_5_9<T1, in T2, T3, T4, T5, in T6, in T7, in T8, T9, out TRes>(ref T1 p1, T2 p2, ref T3 p3, ref T4 p4, ref T5 p5, T6 p6, T7 p7, T8 p8, ref T9 p9);

    public delegate TRes FuncRef1_3_4_6_7<T1, in T2, T3, T4, in T5, T6, T7, in T8, in T9, out TRes>(ref T1 p1, T2 p2, ref T3 p3, ref T4 p4, T5 p5, ref T6 p6, ref T7 p7, T8 p8, T9 p9);

    public delegate TRes FuncRef1_3_4_6_8<T1, in T2, T3, T4, in T5, T6, in T7, T8, in T9, out TRes>(ref T1 p1, T2 p2, ref T3 p3, ref T4 p4, T5 p5, ref T6 p6, T7 p7, ref T8 p8, T9 p9);

    public delegate TRes FuncRef1_3_4_6_9<T1, in T2, T3, T4, in T5, T6, in T7, in T8, T9, out TRes>(ref T1 p1, T2 p2, ref T3 p3, ref T4 p4, T5 p5, ref T6 p6, T7 p7, T8 p8, ref T9 p9);

    public delegate TRes FuncRef1_3_4_7_8<T1, in T2, T3, T4, in T5, in T6, T7, T8, in T9, out TRes>(ref T1 p1, T2 p2, ref T3 p3, ref T4 p4, T5 p5, T6 p6, ref T7 p7, ref T8 p8, T9 p9);

    public delegate TRes FuncRef1_3_4_7_9<T1, in T2, T3, T4, in T5, in T6, T7, in T8, T9, out TRes>(ref T1 p1, T2 p2, ref T3 p3, ref T4 p4, T5 p5, T6 p6, ref T7 p7, T8 p8, ref T9 p9);

    public delegate TRes FuncRef1_3_4_8_9<T1, in T2, T3, T4, in T5, in T6, in T7, T8, T9, out TRes>(ref T1 p1, T2 p2, ref T3 p3, ref T4 p4, T5 p5, T6 p6, T7 p7, ref T8 p8, ref T9 p9);

    public delegate TRes FuncRef1_3_5_6_7<T1, in T2, T3, in T4, T5, T6, T7, in T8, in T9, out TRes>(ref T1 p1, T2 p2, ref T3 p3, T4 p4, ref T5 p5, ref T6 p6, ref T7 p7, T8 p8, T9 p9);

    public delegate TRes FuncRef1_3_5_6_8<T1, in T2, T3, in T4, T5, T6, in T7, T8, in T9, out TRes>(ref T1 p1, T2 p2, ref T3 p3, T4 p4, ref T5 p5, ref T6 p6, T7 p7, ref T8 p8, T9 p9);

    public delegate TRes FuncRef1_3_5_6_9<T1, in T2, T3, in T4, T5, T6, in T7, in T8, T9, out TRes>(ref T1 p1, T2 p2, ref T3 p3, T4 p4, ref T5 p5, ref T6 p6, T7 p7, T8 p8, ref T9 p9);

    public delegate TRes FuncRef1_3_5_7_8<T1, in T2, T3, in T4, T5, in T6, T7, T8, in T9, out TRes>(ref T1 p1, T2 p2, ref T3 p3, T4 p4, ref T5 p5, T6 p6, ref T7 p7, ref T8 p8, T9 p9);

    public delegate TRes FuncRef1_3_5_7_9<T1, in T2, T3, in T4, T5, in T6, T7, in T8, T9, out TRes>(ref T1 p1, T2 p2, ref T3 p3, T4 p4, ref T5 p5, T6 p6, ref T7 p7, T8 p8, ref T9 p9);

    public delegate TRes FuncRef1_3_5_8_9<T1, in T2, T3, in T4, T5, in T6, in T7, T8, T9, out TRes>(ref T1 p1, T2 p2, ref T3 p3, T4 p4, ref T5 p5, T6 p6, T7 p7, ref T8 p8, ref T9 p9);

    public delegate TRes FuncRef1_3_6_7_8<T1, in T2, T3, in T4, in T5, T6, T7, T8, in T9, out TRes>(ref T1 p1, T2 p2, ref T3 p3, T4 p4, T5 p5, ref T6 p6, ref T7 p7, ref T8 p8, T9 p9);

    public delegate TRes FuncRef1_3_6_7_9<T1, in T2, T3, in T4, in T5, T6, T7, in T8, T9, out TRes>(ref T1 p1, T2 p2, ref T3 p3, T4 p4, T5 p5, ref T6 p6, ref T7 p7, T8 p8, ref T9 p9);

    public delegate TRes FuncRef1_3_6_8_9<T1, in T2, T3, in T4, in T5, T6, in T7, T8, T9, out TRes>(ref T1 p1, T2 p2, ref T3 p3, T4 p4, T5 p5, ref T6 p6, T7 p7, ref T8 p8, ref T9 p9);

    public delegate TRes FuncRef1_3_7_8_9<T1, in T2, T3, in T4, in T5, in T6, T7, T8, T9, out TRes>(ref T1 p1, T2 p2, ref T3 p3, T4 p4, T5 p5, T6 p6, ref T7 p7, ref T8 p8, ref T9 p9);

    public delegate TRes FuncRef1_4_5_6_7<T1, in T2, in T3, T4, T5, T6, T7, in T8, in T9, out TRes>(ref T1 p1, T2 p2, T3 p3, ref T4 p4, ref T5 p5, ref T6 p6, ref T7 p7, T8 p8, T9 p9);

    public delegate TRes FuncRef1_4_5_6_8<T1, in T2, in T3, T4, T5, T6, in T7, T8, in T9, out TRes>(ref T1 p1, T2 p2, T3 p3, ref T4 p4, ref T5 p5, ref T6 p6, T7 p7, ref T8 p8, T9 p9);

    public delegate TRes FuncRef1_4_5_6_9<T1, in T2, in T3, T4, T5, T6, in T7, in T8, T9, out TRes>(ref T1 p1, T2 p2, T3 p3, ref T4 p4, ref T5 p5, ref T6 p6, T7 p7, T8 p8, ref T9 p9);

    public delegate TRes FuncRef1_4_5_7_8<T1, in T2, in T3, T4, T5, in T6, T7, T8, in T9, out TRes>(ref T1 p1, T2 p2, T3 p3, ref T4 p4, ref T5 p5, T6 p6, ref T7 p7, ref T8 p8, T9 p9);

    public delegate TRes FuncRef1_4_5_7_9<T1, in T2, in T3, T4, T5, in T6, T7, in T8, T9, out TRes>(ref T1 p1, T2 p2, T3 p3, ref T4 p4, ref T5 p5, T6 p6, ref T7 p7, T8 p8, ref T9 p9);

    public delegate TRes FuncRef1_4_5_8_9<T1, in T2, in T3, T4, T5, in T6, in T7, T8, T9, out TRes>(ref T1 p1, T2 p2, T3 p3, ref T4 p4, ref T5 p5, T6 p6, T7 p7, ref T8 p8, ref T9 p9);

    public delegate TRes FuncRef1_4_6_7_8<T1, in T2, in T3, T4, in T5, T6, T7, T8, in T9, out TRes>(ref T1 p1, T2 p2, T3 p3, ref T4 p4, T5 p5, ref T6 p6, ref T7 p7, ref T8 p8, T9 p9);

    public delegate TRes FuncRef1_4_6_7_9<T1, in T2, in T3, T4, in T5, T6, T7, in T8, T9, out TRes>(ref T1 p1, T2 p2, T3 p3, ref T4 p4, T5 p5, ref T6 p6, ref T7 p7, T8 p8, ref T9 p9);

    public delegate TRes FuncRef1_4_6_8_9<T1, in T2, in T3, T4, in T5, T6, in T7, T8, T9, out TRes>(ref T1 p1, T2 p2, T3 p3, ref T4 p4, T5 p5, ref T6 p6, T7 p7, ref T8 p8, ref T9 p9);

    public delegate TRes FuncRef1_4_7_8_9<T1, in T2, in T3, T4, in T5, in T6, T7, T8, T9, out TRes>(ref T1 p1, T2 p2, T3 p3, ref T4 p4, T5 p5, T6 p6, ref T7 p7, ref T8 p8, ref T9 p9);

    public delegate TRes FuncRef1_5_6_7_8<T1, in T2, in T3, in T4, T5, T6, T7, T8, in T9, out TRes>(ref T1 p1, T2 p2, T3 p3, T4 p4, ref T5 p5, ref T6 p6, ref T7 p7, ref T8 p8, T9 p9);

    public delegate TRes FuncRef1_5_6_7_9<T1, in T2, in T3, in T4, T5, T6, T7, in T8, T9, out TRes>(ref T1 p1, T2 p2, T3 p3, T4 p4, ref T5 p5, ref T6 p6, ref T7 p7, T8 p8, ref T9 p9);

    public delegate TRes FuncRef1_5_6_8_9<T1, in T2, in T3, in T4, T5, T6, in T7, T8, T9, out TRes>(ref T1 p1, T2 p2, T3 p3, T4 p4, ref T5 p5, ref T6 p6, T7 p7, ref T8 p8, ref T9 p9);

    public delegate TRes FuncRef1_5_7_8_9<T1, in T2, in T3, in T4, T5, in T6, T7, T8, T9, out TRes>(ref T1 p1, T2 p2, T3 p3, T4 p4, ref T5 p5, T6 p6, ref T7 p7, ref T8 p8, ref T9 p9);

    public delegate TRes FuncRef1_6_7_8_9<T1, in T2, in T3, in T4, in T5, T6, T7, T8, T9, out TRes>(ref T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, ref T6 p6, ref T7 p7, ref T8 p8, ref T9 p9);

    public delegate TRes FuncRef2_3_4_5_6<in T1, T2, T3, T4, T5, T6, in T7, in T8, in T9, out TRes>(T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, ref T5 p5, ref T6 p6, T7 p7, T8 p8, T9 p9);

    public delegate TRes FuncRef2_3_4_5_7<in T1, T2, T3, T4, T5, in T6, T7, in T8, in T9, out TRes>(T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, ref T5 p5, T6 p6, ref T7 p7, T8 p8, T9 p9);

    public delegate TRes FuncRef2_3_4_5_8<in T1, T2, T3, T4, T5, in T6, in T7, T8, in T9, out TRes>(T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, ref T5 p5, T6 p6, T7 p7, ref T8 p8, T9 p9);

    public delegate TRes FuncRef2_3_4_5_9<in T1, T2, T3, T4, T5, in T6, in T7, in T8, T9, out TRes>(T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, ref T5 p5, T6 p6, T7 p7, T8 p8, ref T9 p9);

    public delegate TRes FuncRef2_3_4_6_7<in T1, T2, T3, T4, in T5, T6, T7, in T8, in T9, out TRes>(T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, T5 p5, ref T6 p6, ref T7 p7, T8 p8, T9 p9);

    public delegate TRes FuncRef2_3_4_6_8<in T1, T2, T3, T4, in T5, T6, in T7, T8, in T9, out TRes>(T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, T5 p5, ref T6 p6, T7 p7, ref T8 p8, T9 p9);

    public delegate TRes FuncRef2_3_4_6_9<in T1, T2, T3, T4, in T5, T6, in T7, in T8, T9, out TRes>(T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, T5 p5, ref T6 p6, T7 p7, T8 p8, ref T9 p9);

    public delegate TRes FuncRef2_3_4_7_8<in T1, T2, T3, T4, in T5, in T6, T7, T8, in T9, out TRes>(T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, T5 p5, T6 p6, ref T7 p7, ref T8 p8, T9 p9);

    public delegate TRes FuncRef2_3_4_7_9<in T1, T2, T3, T4, in T5, in T6, T7, in T8, T9, out TRes>(T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, T5 p5, T6 p6, ref T7 p7, T8 p8, ref T9 p9);

    public delegate TRes FuncRef2_3_4_8_9<in T1, T2, T3, T4, in T5, in T6, in T7, T8, T9, out TRes>(T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, T5 p5, T6 p6, T7 p7, ref T8 p8, ref T9 p9);

    public delegate TRes FuncRef2_3_5_6_7<in T1, T2, T3, in T4, T5, T6, T7, in T8, in T9, out TRes>(T1 p1, ref T2 p2, ref T3 p3, T4 p4, ref T5 p5, ref T6 p6, ref T7 p7, T8 p8, T9 p9);

    public delegate TRes FuncRef2_3_5_6_8<in T1, T2, T3, in T4, T5, T6, in T7, T8, in T9, out TRes>(T1 p1, ref T2 p2, ref T3 p3, T4 p4, ref T5 p5, ref T6 p6, T7 p7, ref T8 p8, T9 p9);

    public delegate TRes FuncRef2_3_5_6_9<in T1, T2, T3, in T4, T5, T6, in T7, in T8, T9, out TRes>(T1 p1, ref T2 p2, ref T3 p3, T4 p4, ref T5 p5, ref T6 p6, T7 p7, T8 p8, ref T9 p9);

    public delegate TRes FuncRef2_3_5_7_8<in T1, T2, T3, in T4, T5, in T6, T7, T8, in T9, out TRes>(T1 p1, ref T2 p2, ref T3 p3, T4 p4, ref T5 p5, T6 p6, ref T7 p7, ref T8 p8, T9 p9);

    public delegate TRes FuncRef2_3_5_7_9<in T1, T2, T3, in T4, T5, in T6, T7, in T8, T9, out TRes>(T1 p1, ref T2 p2, ref T3 p3, T4 p4, ref T5 p5, T6 p6, ref T7 p7, T8 p8, ref T9 p9);

    public delegate TRes FuncRef2_3_5_8_9<in T1, T2, T3, in T4, T5, in T6, in T7, T8, T9, out TRes>(T1 p1, ref T2 p2, ref T3 p3, T4 p4, ref T5 p5, T6 p6, T7 p7, ref T8 p8, ref T9 p9);

    public delegate TRes FuncRef2_3_6_7_8<in T1, T2, T3, in T4, in T5, T6, T7, T8, in T9, out TRes>(T1 p1, ref T2 p2, ref T3 p3, T4 p4, T5 p5, ref T6 p6, ref T7 p7, ref T8 p8, T9 p9);

    public delegate TRes FuncRef2_3_6_7_9<in T1, T2, T3, in T4, in T5, T6, T7, in T8, T9, out TRes>(T1 p1, ref T2 p2, ref T3 p3, T4 p4, T5 p5, ref T6 p6, ref T7 p7, T8 p8, ref T9 p9);

    public delegate TRes FuncRef2_3_6_8_9<in T1, T2, T3, in T4, in T5, T6, in T7, T8, T9, out TRes>(T1 p1, ref T2 p2, ref T3 p3, T4 p4, T5 p5, ref T6 p6, T7 p7, ref T8 p8, ref T9 p9);

    public delegate TRes FuncRef2_3_7_8_9<in T1, T2, T3, in T4, in T5, in T6, T7, T8, T9, out TRes>(T1 p1, ref T2 p2, ref T3 p3, T4 p4, T5 p5, T6 p6, ref T7 p7, ref T8 p8, ref T9 p9);

    public delegate TRes FuncRef2_4_5_6_7<in T1, T2, in T3, T4, T5, T6, T7, in T8, in T9, out TRes>(T1 p1, ref T2 p2, T3 p3, ref T4 p4, ref T5 p5, ref T6 p6, ref T7 p7, T8 p8, T9 p9);

    public delegate TRes FuncRef2_4_5_6_8<in T1, T2, in T3, T4, T5, T6, in T7, T8, in T9, out TRes>(T1 p1, ref T2 p2, T3 p3, ref T4 p4, ref T5 p5, ref T6 p6, T7 p7, ref T8 p8, T9 p9);

    public delegate TRes FuncRef2_4_5_6_9<in T1, T2, in T3, T4, T5, T6, in T7, in T8, T9, out TRes>(T1 p1, ref T2 p2, T3 p3, ref T4 p4, ref T5 p5, ref T6 p6, T7 p7, T8 p8, ref T9 p9);

    public delegate TRes FuncRef2_4_5_7_8<in T1, T2, in T3, T4, T5, in T6, T7, T8, in T9, out TRes>(T1 p1, ref T2 p2, T3 p3, ref T4 p4, ref T5 p5, T6 p6, ref T7 p7, ref T8 p8, T9 p9);

    public delegate TRes FuncRef2_4_5_7_9<in T1, T2, in T3, T4, T5, in T6, T7, in T8, T9, out TRes>(T1 p1, ref T2 p2, T3 p3, ref T4 p4, ref T5 p5, T6 p6, ref T7 p7, T8 p8, ref T9 p9);

    public delegate TRes FuncRef2_4_5_8_9<in T1, T2, in T3, T4, T5, in T6, in T7, T8, T9, out TRes>(T1 p1, ref T2 p2, T3 p3, ref T4 p4, ref T5 p5, T6 p6, T7 p7, ref T8 p8, ref T9 p9);

    public delegate TRes FuncRef2_4_6_7_8<in T1, T2, in T3, T4, in T5, T6, T7, T8, in T9, out TRes>(T1 p1, ref T2 p2, T3 p3, ref T4 p4, T5 p5, ref T6 p6, ref T7 p7, ref T8 p8, T9 p9);

    public delegate TRes FuncRef2_4_6_7_9<in T1, T2, in T3, T4, in T5, T6, T7, in T8, T9, out TRes>(T1 p1, ref T2 p2, T3 p3, ref T4 p4, T5 p5, ref T6 p6, ref T7 p7, T8 p8, ref T9 p9);

    public delegate TRes FuncRef2_4_6_8_9<in T1, T2, in T3, T4, in T5, T6, in T7, T8, T9, out TRes>(T1 p1, ref T2 p2, T3 p3, ref T4 p4, T5 p5, ref T6 p6, T7 p7, ref T8 p8, ref T9 p9);

    public delegate TRes FuncRef2_4_7_8_9<in T1, T2, in T3, T4, in T5, in T6, T7, T8, T9, out TRes>(T1 p1, ref T2 p2, T3 p3, ref T4 p4, T5 p5, T6 p6, ref T7 p7, ref T8 p8, ref T9 p9);

    public delegate TRes FuncRef2_5_6_7_8<in T1, T2, in T3, in T4, T5, T6, T7, T8, in T9, out TRes>(T1 p1, ref T2 p2, T3 p3, T4 p4, ref T5 p5, ref T6 p6, ref T7 p7, ref T8 p8, T9 p9);

    public delegate TRes FuncRef2_5_6_7_9<in T1, T2, in T3, in T4, T5, T6, T7, in T8, T9, out TRes>(T1 p1, ref T2 p2, T3 p3, T4 p4, ref T5 p5, ref T6 p6, ref T7 p7, T8 p8, ref T9 p9);

    public delegate TRes FuncRef2_5_6_8_9<in T1, T2, in T3, in T4, T5, T6, in T7, T8, T9, out TRes>(T1 p1, ref T2 p2, T3 p3, T4 p4, ref T5 p5, ref T6 p6, T7 p7, ref T8 p8, ref T9 p9);

    public delegate TRes FuncRef2_5_7_8_9<in T1, T2, in T3, in T4, T5, in T6, T7, T8, T9, out TRes>(T1 p1, ref T2 p2, T3 p3, T4 p4, ref T5 p5, T6 p6, ref T7 p7, ref T8 p8, ref T9 p9);

    public delegate TRes FuncRef2_6_7_8_9<in T1, T2, in T3, in T4, in T5, T6, T7, T8, T9, out TRes>(T1 p1, ref T2 p2, T3 p3, T4 p4, T5 p5, ref T6 p6, ref T7 p7, ref T8 p8, ref T9 p9);

    public delegate TRes FuncRef3_4_5_6_7<in T1, in T2, T3, T4, T5, T6, T7, in T8, in T9, out TRes>(T1 p1, T2 p2, ref T3 p3, ref T4 p4, ref T5 p5, ref T6 p6, ref T7 p7, T8 p8, T9 p9);

    public delegate TRes FuncRef3_4_5_6_8<in T1, in T2, T3, T4, T5, T6, in T7, T8, in T9, out TRes>(T1 p1, T2 p2, ref T3 p3, ref T4 p4, ref T5 p5, ref T6 p6, T7 p7, ref T8 p8, T9 p9);

    public delegate TRes FuncRef3_4_5_6_9<in T1, in T2, T3, T4, T5, T6, in T7, in T8, T9, out TRes>(T1 p1, T2 p2, ref T3 p3, ref T4 p4, ref T5 p5, ref T6 p6, T7 p7, T8 p8, ref T9 p9);

    public delegate TRes FuncRef3_4_5_7_8<in T1, in T2, T3, T4, T5, in T6, T7, T8, in T9, out TRes>(T1 p1, T2 p2, ref T3 p3, ref T4 p4, ref T5 p5, T6 p6, ref T7 p7, ref T8 p8, T9 p9);

    public delegate TRes FuncRef3_4_5_7_9<in T1, in T2, T3, T4, T5, in T6, T7, in T8, T9, out TRes>(T1 p1, T2 p2, ref T3 p3, ref T4 p4, ref T5 p5, T6 p6, ref T7 p7, T8 p8, ref T9 p9);

    public delegate TRes FuncRef3_4_5_8_9<in T1, in T2, T3, T4, T5, in T6, in T7, T8, T9, out TRes>(T1 p1, T2 p2, ref T3 p3, ref T4 p4, ref T5 p5, T6 p6, T7 p7, ref T8 p8, ref T9 p9);

    public delegate TRes FuncRef3_4_6_7_8<in T1, in T2, T3, T4, in T5, T6, T7, T8, in T9, out TRes>(T1 p1, T2 p2, ref T3 p3, ref T4 p4, T5 p5, ref T6 p6, ref T7 p7, ref T8 p8, T9 p9);

    public delegate TRes FuncRef3_4_6_7_9<in T1, in T2, T3, T4, in T5, T6, T7, in T8, T9, out TRes>(T1 p1, T2 p2, ref T3 p3, ref T4 p4, T5 p5, ref T6 p6, ref T7 p7, T8 p8, ref T9 p9);

    public delegate TRes FuncRef3_4_6_8_9<in T1, in T2, T3, T4, in T5, T6, in T7, T8, T9, out TRes>(T1 p1, T2 p2, ref T3 p3, ref T4 p4, T5 p5, ref T6 p6, T7 p7, ref T8 p8, ref T9 p9);

    public delegate TRes FuncRef3_4_7_8_9<in T1, in T2, T3, T4, in T5, in T6, T7, T8, T9, out TRes>(T1 p1, T2 p2, ref T3 p3, ref T4 p4, T5 p5, T6 p6, ref T7 p7, ref T8 p8, ref T9 p9);

    public delegate TRes FuncRef3_5_6_7_8<in T1, in T2, T3, in T4, T5, T6, T7, T8, in T9, out TRes>(T1 p1, T2 p2, ref T3 p3, T4 p4, ref T5 p5, ref T6 p6, ref T7 p7, ref T8 p8, T9 p9);

    public delegate TRes FuncRef3_5_6_7_9<in T1, in T2, T3, in T4, T5, T6, T7, in T8, T9, out TRes>(T1 p1, T2 p2, ref T3 p3, T4 p4, ref T5 p5, ref T6 p6, ref T7 p7, T8 p8, ref T9 p9);

    public delegate TRes FuncRef3_5_6_8_9<in T1, in T2, T3, in T4, T5, T6, in T7, T8, T9, out TRes>(T1 p1, T2 p2, ref T3 p3, T4 p4, ref T5 p5, ref T6 p6, T7 p7, ref T8 p8, ref T9 p9);

    public delegate TRes FuncRef3_5_7_8_9<in T1, in T2, T3, in T4, T5, in T6, T7, T8, T9, out TRes>(T1 p1, T2 p2, ref T3 p3, T4 p4, ref T5 p5, T6 p6, ref T7 p7, ref T8 p8, ref T9 p9);

    public delegate TRes FuncRef3_6_7_8_9<in T1, in T2, T3, in T4, in T5, T6, T7, T8, T9, out TRes>(T1 p1, T2 p2, ref T3 p3, T4 p4, T5 p5, ref T6 p6, ref T7 p7, ref T8 p8, ref T9 p9);

    public delegate TRes FuncRef4_5_6_7_8<in T1, in T2, in T3, T4, T5, T6, T7, T8, in T9, out TRes>(T1 p1, T2 p2, T3 p3, ref T4 p4, ref T5 p5, ref T6 p6, ref T7 p7, ref T8 p8, T9 p9);

    public delegate TRes FuncRef4_5_6_7_9<in T1, in T2, in T3, T4, T5, T6, T7, in T8, T9, out TRes>(T1 p1, T2 p2, T3 p3, ref T4 p4, ref T5 p5, ref T6 p6, ref T7 p7, T8 p8, ref T9 p9);

    public delegate TRes FuncRef4_5_6_8_9<in T1, in T2, in T3, T4, T5, T6, in T7, T8, T9, out TRes>(T1 p1, T2 p2, T3 p3, ref T4 p4, ref T5 p5, ref T6 p6, T7 p7, ref T8 p8, ref T9 p9);

    public delegate TRes FuncRef4_5_7_8_9<in T1, in T2, in T3, T4, T5, in T6, T7, T8, T9, out TRes>(T1 p1, T2 p2, T3 p3, ref T4 p4, ref T5 p5, T6 p6, ref T7 p7, ref T8 p8, ref T9 p9);

    public delegate TRes FuncRef4_6_7_8_9<in T1, in T2, in T3, T4, in T5, T6, T7, T8, T9, out TRes>(T1 p1, T2 p2, T3 p3, ref T4 p4, T5 p5, ref T6 p6, ref T7 p7, ref T8 p8, ref T9 p9);

    public delegate TRes FuncRef5_6_7_8_9<in T1, in T2, in T3, in T4, T5, T6, T7, T8, T9, out TRes>(T1 p1, T2 p2, T3 p3, T4 p4, ref T5 p5, ref T6 p6, ref T7 p7, ref T8 p8, ref T9 p9);

    public delegate TRes FuncRef1_2_3_4_5_6<T1, T2, T3, T4, T5, T6, in T7, in T8, in T9, out TRes>(ref T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, ref T5 p5, ref T6 p6, T7 p7, T8 p8, T9 p9);

    public delegate TRes FuncRef1_2_3_4_5_7<T1, T2, T3, T4, T5, in T6, T7, in T8, in T9, out TRes>(ref T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, ref T5 p5, T6 p6, ref T7 p7, T8 p8, T9 p9);

    public delegate TRes FuncRef1_2_3_4_5_8<T1, T2, T3, T4, T5, in T6, in T7, T8, in T9, out TRes>(ref T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, ref T5 p5, T6 p6, T7 p7, ref T8 p8, T9 p9);

    public delegate TRes FuncRef1_2_3_4_5_9<T1, T2, T3, T4, T5, in T6, in T7, in T8, T9, out TRes>(ref T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, ref T5 p5, T6 p6, T7 p7, T8 p8, ref T9 p9);

    public delegate TRes FuncRef1_2_3_4_6_7<T1, T2, T3, T4, in T5, T6, T7, in T8, in T9, out TRes>(ref T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, T5 p5, ref T6 p6, ref T7 p7, T8 p8, T9 p9);

    public delegate TRes FuncRef1_2_3_4_6_8<T1, T2, T3, T4, in T5, T6, in T7, T8, in T9, out TRes>(ref T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, T5 p5, ref T6 p6, T7 p7, ref T8 p8, T9 p9);

    public delegate TRes FuncRef1_2_3_4_6_9<T1, T2, T3, T4, in T5, T6, in T7, in T8, T9, out TRes>(ref T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, T5 p5, ref T6 p6, T7 p7, T8 p8, ref T9 p9);

    public delegate TRes FuncRef1_2_3_4_7_8<T1, T2, T3, T4, in T5, in T6, T7, T8, in T9, out TRes>(ref T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, T5 p5, T6 p6, ref T7 p7, ref T8 p8, T9 p9);

    public delegate TRes FuncRef1_2_3_4_7_9<T1, T2, T3, T4, in T5, in T6, T7, in T8, T9, out TRes>(ref T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, T5 p5, T6 p6, ref T7 p7, T8 p8, ref T9 p9);

    public delegate TRes FuncRef1_2_3_4_8_9<T1, T2, T3, T4, in T5, in T6, in T7, T8, T9, out TRes>(ref T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, T5 p5, T6 p6, T7 p7, ref T8 p8, ref T9 p9);

    public delegate TRes FuncRef1_2_3_5_6_7<T1, T2, T3, in T4, T5, T6, T7, in T8, in T9, out TRes>(ref T1 p1, ref T2 p2, ref T3 p3, T4 p4, ref T5 p5, ref T6 p6, ref T7 p7, T8 p8, T9 p9);

    public delegate TRes FuncRef1_2_3_5_6_8<T1, T2, T3, in T4, T5, T6, in T7, T8, in T9, out TRes>(ref T1 p1, ref T2 p2, ref T3 p3, T4 p4, ref T5 p5, ref T6 p6, T7 p7, ref T8 p8, T9 p9);

    public delegate TRes FuncRef1_2_3_5_6_9<T1, T2, T3, in T4, T5, T6, in T7, in T8, T9, out TRes>(ref T1 p1, ref T2 p2, ref T3 p3, T4 p4, ref T5 p5, ref T6 p6, T7 p7, T8 p8, ref T9 p9);

    public delegate TRes FuncRef1_2_3_5_7_8<T1, T2, T3, in T4, T5, in T6, T7, T8, in T9, out TRes>(ref T1 p1, ref T2 p2, ref T3 p3, T4 p4, ref T5 p5, T6 p6, ref T7 p7, ref T8 p8, T9 p9);

    public delegate TRes FuncRef1_2_3_5_7_9<T1, T2, T3, in T4, T5, in T6, T7, in T8, T9, out TRes>(ref T1 p1, ref T2 p2, ref T3 p3, T4 p4, ref T5 p5, T6 p6, ref T7 p7, T8 p8, ref T9 p9);

    public delegate TRes FuncRef1_2_3_5_8_9<T1, T2, T3, in T4, T5, in T6, in T7, T8, T9, out TRes>(ref T1 p1, ref T2 p2, ref T3 p3, T4 p4, ref T5 p5, T6 p6, T7 p7, ref T8 p8, ref T9 p9);

    public delegate TRes FuncRef1_2_3_6_7_8<T1, T2, T3, in T4, in T5, T6, T7, T8, in T9, out TRes>(ref T1 p1, ref T2 p2, ref T3 p3, T4 p4, T5 p5, ref T6 p6, ref T7 p7, ref T8 p8, T9 p9);

    public delegate TRes FuncRef1_2_3_6_7_9<T1, T2, T3, in T4, in T5, T6, T7, in T8, T9, out TRes>(ref T1 p1, ref T2 p2, ref T3 p3, T4 p4, T5 p5, ref T6 p6, ref T7 p7, T8 p8, ref T9 p9);

    public delegate TRes FuncRef1_2_3_6_8_9<T1, T2, T3, in T4, in T5, T6, in T7, T8, T9, out TRes>(ref T1 p1, ref T2 p2, ref T3 p3, T4 p4, T5 p5, ref T6 p6, T7 p7, ref T8 p8, ref T9 p9);

    public delegate TRes FuncRef1_2_3_7_8_9<T1, T2, T3, in T4, in T5, in T6, T7, T8, T9, out TRes>(ref T1 p1, ref T2 p2, ref T3 p3, T4 p4, T5 p5, T6 p6, ref T7 p7, ref T8 p8, ref T9 p9);

    public delegate TRes FuncRef1_2_4_5_6_7<T1, T2, in T3, T4, T5, T6, T7, in T8, in T9, out TRes>(ref T1 p1, ref T2 p2, T3 p3, ref T4 p4, ref T5 p5, ref T6 p6, ref T7 p7, T8 p8, T9 p9);

    public delegate TRes FuncRef1_2_4_5_6_8<T1, T2, in T3, T4, T5, T6, in T7, T8, in T9, out TRes>(ref T1 p1, ref T2 p2, T3 p3, ref T4 p4, ref T5 p5, ref T6 p6, T7 p7, ref T8 p8, T9 p9);

    public delegate TRes FuncRef1_2_4_5_6_9<T1, T2, in T3, T4, T5, T6, in T7, in T8, T9, out TRes>(ref T1 p1, ref T2 p2, T3 p3, ref T4 p4, ref T5 p5, ref T6 p6, T7 p7, T8 p8, ref T9 p9);

    public delegate TRes FuncRef1_2_4_5_7_8<T1, T2, in T3, T4, T5, in T6, T7, T8, in T9, out TRes>(ref T1 p1, ref T2 p2, T3 p3, ref T4 p4, ref T5 p5, T6 p6, ref T7 p7, ref T8 p8, T9 p9);

    public delegate TRes FuncRef1_2_4_5_7_9<T1, T2, in T3, T4, T5, in T6, T7, in T8, T9, out TRes>(ref T1 p1, ref T2 p2, T3 p3, ref T4 p4, ref T5 p5, T6 p6, ref T7 p7, T8 p8, ref T9 p9);

    public delegate TRes FuncRef1_2_4_5_8_9<T1, T2, in T3, T4, T5, in T6, in T7, T8, T9, out TRes>(ref T1 p1, ref T2 p2, T3 p3, ref T4 p4, ref T5 p5, T6 p6, T7 p7, ref T8 p8, ref T9 p9);

    public delegate TRes FuncRef1_2_4_6_7_8<T1, T2, in T3, T4, in T5, T6, T7, T8, in T9, out TRes>(ref T1 p1, ref T2 p2, T3 p3, ref T4 p4, T5 p5, ref T6 p6, ref T7 p7, ref T8 p8, T9 p9);

    public delegate TRes FuncRef1_2_4_6_7_9<T1, T2, in T3, T4, in T5, T6, T7, in T8, T9, out TRes>(ref T1 p1, ref T2 p2, T3 p3, ref T4 p4, T5 p5, ref T6 p6, ref T7 p7, T8 p8, ref T9 p9);

    public delegate TRes FuncRef1_2_4_6_8_9<T1, T2, in T3, T4, in T5, T6, in T7, T8, T9, out TRes>(ref T1 p1, ref T2 p2, T3 p3, ref T4 p4, T5 p5, ref T6 p6, T7 p7, ref T8 p8, ref T9 p9);

    public delegate TRes FuncRef1_2_4_7_8_9<T1, T2, in T3, T4, in T5, in T6, T7, T8, T9, out TRes>(ref T1 p1, ref T2 p2, T3 p3, ref T4 p4, T5 p5, T6 p6, ref T7 p7, ref T8 p8, ref T9 p9);

    public delegate TRes FuncRef1_2_5_6_7_8<T1, T2, in T3, in T4, T5, T6, T7, T8, in T9, out TRes>(ref T1 p1, ref T2 p2, T3 p3, T4 p4, ref T5 p5, ref T6 p6, ref T7 p7, ref T8 p8, T9 p9);

    public delegate TRes FuncRef1_2_5_6_7_9<T1, T2, in T3, in T4, T5, T6, T7, in T8, T9, out TRes>(ref T1 p1, ref T2 p2, T3 p3, T4 p4, ref T5 p5, ref T6 p6, ref T7 p7, T8 p8, ref T9 p9);

    public delegate TRes FuncRef1_2_5_6_8_9<T1, T2, in T3, in T4, T5, T6, in T7, T8, T9, out TRes>(ref T1 p1, ref T2 p2, T3 p3, T4 p4, ref T5 p5, ref T6 p6, T7 p7, ref T8 p8, ref T9 p9);

    public delegate TRes FuncRef1_2_5_7_8_9<T1, T2, in T3, in T4, T5, in T6, T7, T8, T9, out TRes>(ref T1 p1, ref T2 p2, T3 p3, T4 p4, ref T5 p5, T6 p6, ref T7 p7, ref T8 p8, ref T9 p9);

    public delegate TRes FuncRef1_2_6_7_8_9<T1, T2, in T3, in T4, in T5, T6, T7, T8, T9, out TRes>(ref T1 p1, ref T2 p2, T3 p3, T4 p4, T5 p5, ref T6 p6, ref T7 p7, ref T8 p8, ref T9 p9);

    public delegate TRes FuncRef1_3_4_5_6_7<T1, in T2, T3, T4, T5, T6, T7, in T8, in T9, out TRes>(ref T1 p1, T2 p2, ref T3 p3, ref T4 p4, ref T5 p5, ref T6 p6, ref T7 p7, T8 p8, T9 p9);

    public delegate TRes FuncRef1_3_4_5_6_8<T1, in T2, T3, T4, T5, T6, in T7, T8, in T9, out TRes>(ref T1 p1, T2 p2, ref T3 p3, ref T4 p4, ref T5 p5, ref T6 p6, T7 p7, ref T8 p8, T9 p9);

    public delegate TRes FuncRef1_3_4_5_6_9<T1, in T2, T3, T4, T5, T6, in T7, in T8, T9, out TRes>(ref T1 p1, T2 p2, ref T3 p3, ref T4 p4, ref T5 p5, ref T6 p6, T7 p7, T8 p8, ref T9 p9);

    public delegate TRes FuncRef1_3_4_5_7_8<T1, in T2, T3, T4, T5, in T6, T7, T8, in T9, out TRes>(ref T1 p1, T2 p2, ref T3 p3, ref T4 p4, ref T5 p5, T6 p6, ref T7 p7, ref T8 p8, T9 p9);

    public delegate TRes FuncRef1_3_4_5_7_9<T1, in T2, T3, T4, T5, in T6, T7, in T8, T9, out TRes>(ref T1 p1, T2 p2, ref T3 p3, ref T4 p4, ref T5 p5, T6 p6, ref T7 p7, T8 p8, ref T9 p9);

    public delegate TRes FuncRef1_3_4_5_8_9<T1, in T2, T3, T4, T5, in T6, in T7, T8, T9, out TRes>(ref T1 p1, T2 p2, ref T3 p3, ref T4 p4, ref T5 p5, T6 p6, T7 p7, ref T8 p8, ref T9 p9);

    public delegate TRes FuncRef1_3_4_6_7_8<T1, in T2, T3, T4, in T5, T6, T7, T8, in T9, out TRes>(ref T1 p1, T2 p2, ref T3 p3, ref T4 p4, T5 p5, ref T6 p6, ref T7 p7, ref T8 p8, T9 p9);

    public delegate TRes FuncRef1_3_4_6_7_9<T1, in T2, T3, T4, in T5, T6, T7, in T8, T9, out TRes>(ref T1 p1, T2 p2, ref T3 p3, ref T4 p4, T5 p5, ref T6 p6, ref T7 p7, T8 p8, ref T9 p9);

    public delegate TRes FuncRef1_3_4_6_8_9<T1, in T2, T3, T4, in T5, T6, in T7, T8, T9, out TRes>(ref T1 p1, T2 p2, ref T3 p3, ref T4 p4, T5 p5, ref T6 p6, T7 p7, ref T8 p8, ref T9 p9);

    public delegate TRes FuncRef1_3_4_7_8_9<T1, in T2, T3, T4, in T5, in T6, T7, T8, T9, out TRes>(ref T1 p1, T2 p2, ref T3 p3, ref T4 p4, T5 p5, T6 p6, ref T7 p7, ref T8 p8, ref T9 p9);

    public delegate TRes FuncRef1_3_5_6_7_8<T1, in T2, T3, in T4, T5, T6, T7, T8, in T9, out TRes>(ref T1 p1, T2 p2, ref T3 p3, T4 p4, ref T5 p5, ref T6 p6, ref T7 p7, ref T8 p8, T9 p9);

    public delegate TRes FuncRef1_3_5_6_7_9<T1, in T2, T3, in T4, T5, T6, T7, in T8, T9, out TRes>(ref T1 p1, T2 p2, ref T3 p3, T4 p4, ref T5 p5, ref T6 p6, ref T7 p7, T8 p8, ref T9 p9);

    public delegate TRes FuncRef1_3_5_6_8_9<T1, in T2, T3, in T4, T5, T6, in T7, T8, T9, out TRes>(ref T1 p1, T2 p2, ref T3 p3, T4 p4, ref T5 p5, ref T6 p6, T7 p7, ref T8 p8, ref T9 p9);

    public delegate TRes FuncRef1_3_5_7_8_9<T1, in T2, T3, in T4, T5, in T6, T7, T8, T9, out TRes>(ref T1 p1, T2 p2, ref T3 p3, T4 p4, ref T5 p5, T6 p6, ref T7 p7, ref T8 p8, ref T9 p9);

    public delegate TRes FuncRef1_3_6_7_8_9<T1, in T2, T3, in T4, in T5, T6, T7, T8, T9, out TRes>(ref T1 p1, T2 p2, ref T3 p3, T4 p4, T5 p5, ref T6 p6, ref T7 p7, ref T8 p8, ref T9 p9);

    public delegate TRes FuncRef1_4_5_6_7_8<T1, in T2, in T3, T4, T5, T6, T7, T8, in T9, out TRes>(ref T1 p1, T2 p2, T3 p3, ref T4 p4, ref T5 p5, ref T6 p6, ref T7 p7, ref T8 p8, T9 p9);

    public delegate TRes FuncRef1_4_5_6_7_9<T1, in T2, in T3, T4, T5, T6, T7, in T8, T9, out TRes>(ref T1 p1, T2 p2, T3 p3, ref T4 p4, ref T5 p5, ref T6 p6, ref T7 p7, T8 p8, ref T9 p9);

    public delegate TRes FuncRef1_4_5_6_8_9<T1, in T2, in T3, T4, T5, T6, in T7, T8, T9, out TRes>(ref T1 p1, T2 p2, T3 p3, ref T4 p4, ref T5 p5, ref T6 p6, T7 p7, ref T8 p8, ref T9 p9);

    public delegate TRes FuncRef1_4_5_7_8_9<T1, in T2, in T3, T4, T5, in T6, T7, T8, T9, out TRes>(ref T1 p1, T2 p2, T3 p3, ref T4 p4, ref T5 p5, T6 p6, ref T7 p7, ref T8 p8, ref T9 p9);

    public delegate TRes FuncRef1_4_6_7_8_9<T1, in T2, in T3, T4, in T5, T6, T7, T8, T9, out TRes>(ref T1 p1, T2 p2, T3 p3, ref T4 p4, T5 p5, ref T6 p6, ref T7 p7, ref T8 p8, ref T9 p9);

    public delegate TRes FuncRef1_5_6_7_8_9<T1, in T2, in T3, in T4, T5, T6, T7, T8, T9, out TRes>(ref T1 p1, T2 p2, T3 p3, T4 p4, ref T5 p5, ref T6 p6, ref T7 p7, ref T8 p8, ref T9 p9);

    public delegate TRes FuncRef2_3_4_5_6_7<in T1, T2, T3, T4, T5, T6, T7, in T8, in T9, out TRes>(T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, ref T5 p5, ref T6 p6, ref T7 p7, T8 p8, T9 p9);

    public delegate TRes FuncRef2_3_4_5_6_8<in T1, T2, T3, T4, T5, T6, in T7, T8, in T9, out TRes>(T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, ref T5 p5, ref T6 p6, T7 p7, ref T8 p8, T9 p9);

    public delegate TRes FuncRef2_3_4_5_6_9<in T1, T2, T3, T4, T5, T6, in T7, in T8, T9, out TRes>(T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, ref T5 p5, ref T6 p6, T7 p7, T8 p8, ref T9 p9);

    public delegate TRes FuncRef2_3_4_5_7_8<in T1, T2, T3, T4, T5, in T6, T7, T8, in T9, out TRes>(T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, ref T5 p5, T6 p6, ref T7 p7, ref T8 p8, T9 p9);

    public delegate TRes FuncRef2_3_4_5_7_9<in T1, T2, T3, T4, T5, in T6, T7, in T8, T9, out TRes>(T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, ref T5 p5, T6 p6, ref T7 p7, T8 p8, ref T9 p9);

    public delegate TRes FuncRef2_3_4_5_8_9<in T1, T2, T3, T4, T5, in T6, in T7, T8, T9, out TRes>(T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, ref T5 p5, T6 p6, T7 p7, ref T8 p8, ref T9 p9);

    public delegate TRes FuncRef2_3_4_6_7_8<in T1, T2, T3, T4, in T5, T6, T7, T8, in T9, out TRes>(T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, T5 p5, ref T6 p6, ref T7 p7, ref T8 p8, T9 p9);

    public delegate TRes FuncRef2_3_4_6_7_9<in T1, T2, T3, T4, in T5, T6, T7, in T8, T9, out TRes>(T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, T5 p5, ref T6 p6, ref T7 p7, T8 p8, ref T9 p9);

    public delegate TRes FuncRef2_3_4_6_8_9<in T1, T2, T3, T4, in T5, T6, in T7, T8, T9, out TRes>(T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, T5 p5, ref T6 p6, T7 p7, ref T8 p8, ref T9 p9);

    public delegate TRes FuncRef2_3_4_7_8_9<in T1, T2, T3, T4, in T5, in T6, T7, T8, T9, out TRes>(T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, T5 p5, T6 p6, ref T7 p7, ref T8 p8, ref T9 p9);

    public delegate TRes FuncRef2_3_5_6_7_8<in T1, T2, T3, in T4, T5, T6, T7, T8, in T9, out TRes>(T1 p1, ref T2 p2, ref T3 p3, T4 p4, ref T5 p5, ref T6 p6, ref T7 p7, ref T8 p8, T9 p9);

    public delegate TRes FuncRef2_3_5_6_7_9<in T1, T2, T3, in T4, T5, T6, T7, in T8, T9, out TRes>(T1 p1, ref T2 p2, ref T3 p3, T4 p4, ref T5 p5, ref T6 p6, ref T7 p7, T8 p8, ref T9 p9);

    public delegate TRes FuncRef2_3_5_6_8_9<in T1, T2, T3, in T4, T5, T6, in T7, T8, T9, out TRes>(T1 p1, ref T2 p2, ref T3 p3, T4 p4, ref T5 p5, ref T6 p6, T7 p7, ref T8 p8, ref T9 p9);

    public delegate TRes FuncRef2_3_5_7_8_9<in T1, T2, T3, in T4, T5, in T6, T7, T8, T9, out TRes>(T1 p1, ref T2 p2, ref T3 p3, T4 p4, ref T5 p5, T6 p6, ref T7 p7, ref T8 p8, ref T9 p9);

    public delegate TRes FuncRef2_3_6_7_8_9<in T1, T2, T3, in T4, in T5, T6, T7, T8, T9, out TRes>(T1 p1, ref T2 p2, ref T3 p3, T4 p4, T5 p5, ref T6 p6, ref T7 p7, ref T8 p8, ref T9 p9);

    public delegate TRes FuncRef2_4_5_6_7_8<in T1, T2, in T3, T4, T5, T6, T7, T8, in T9, out TRes>(T1 p1, ref T2 p2, T3 p3, ref T4 p4, ref T5 p5, ref T6 p6, ref T7 p7, ref T8 p8, T9 p9);

    public delegate TRes FuncRef2_4_5_6_7_9<in T1, T2, in T3, T4, T5, T6, T7, in T8, T9, out TRes>(T1 p1, ref T2 p2, T3 p3, ref T4 p4, ref T5 p5, ref T6 p6, ref T7 p7, T8 p8, ref T9 p9);

    public delegate TRes FuncRef2_4_5_6_8_9<in T1, T2, in T3, T4, T5, T6, in T7, T8, T9, out TRes>(T1 p1, ref T2 p2, T3 p3, ref T4 p4, ref T5 p5, ref T6 p6, T7 p7, ref T8 p8, ref T9 p9);

    public delegate TRes FuncRef2_4_5_7_8_9<in T1, T2, in T3, T4, T5, in T6, T7, T8, T9, out TRes>(T1 p1, ref T2 p2, T3 p3, ref T4 p4, ref T5 p5, T6 p6, ref T7 p7, ref T8 p8, ref T9 p9);

    public delegate TRes FuncRef2_4_6_7_8_9<in T1, T2, in T3, T4, in T5, T6, T7, T8, T9, out TRes>(T1 p1, ref T2 p2, T3 p3, ref T4 p4, T5 p5, ref T6 p6, ref T7 p7, ref T8 p8, ref T9 p9);

    public delegate TRes FuncRef2_5_6_7_8_9<in T1, T2, in T3, in T4, T5, T6, T7, T8, T9, out TRes>(T1 p1, ref T2 p2, T3 p3, T4 p4, ref T5 p5, ref T6 p6, ref T7 p7, ref T8 p8, ref T9 p9);

    public delegate TRes FuncRef3_4_5_6_7_8<in T1, in T2, T3, T4, T5, T6, T7, T8, in T9, out TRes>(T1 p1, T2 p2, ref T3 p3, ref T4 p4, ref T5 p5, ref T6 p6, ref T7 p7, ref T8 p8, T9 p9);

    public delegate TRes FuncRef3_4_5_6_7_9<in T1, in T2, T3, T4, T5, T6, T7, in T8, T9, out TRes>(T1 p1, T2 p2, ref T3 p3, ref T4 p4, ref T5 p5, ref T6 p6, ref T7 p7, T8 p8, ref T9 p9);

    public delegate TRes FuncRef3_4_5_6_8_9<in T1, in T2, T3, T4, T5, T6, in T7, T8, T9, out TRes>(T1 p1, T2 p2, ref T3 p3, ref T4 p4, ref T5 p5, ref T6 p6, T7 p7, ref T8 p8, ref T9 p9);

    public delegate TRes FuncRef3_4_5_7_8_9<in T1, in T2, T3, T4, T5, in T6, T7, T8, T9, out TRes>(T1 p1, T2 p2, ref T3 p3, ref T4 p4, ref T5 p5, T6 p6, ref T7 p7, ref T8 p8, ref T9 p9);

    public delegate TRes FuncRef3_4_6_7_8_9<in T1, in T2, T3, T4, in T5, T6, T7, T8, T9, out TRes>(T1 p1, T2 p2, ref T3 p3, ref T4 p4, T5 p5, ref T6 p6, ref T7 p7, ref T8 p8, ref T9 p9);

    public delegate TRes FuncRef3_5_6_7_8_9<in T1, in T2, T3, in T4, T5, T6, T7, T8, T9, out TRes>(T1 p1, T2 p2, ref T3 p3, T4 p4, ref T5 p5, ref T6 p6, ref T7 p7, ref T8 p8, ref T9 p9);

    public delegate TRes FuncRef4_5_6_7_8_9<in T1, in T2, in T3, T4, T5, T6, T7, T8, T9, out TRes>(T1 p1, T2 p2, T3 p3, ref T4 p4, ref T5 p5, ref T6 p6, ref T7 p7, ref T8 p8, ref T9 p9);

    public delegate TRes FuncRef1_2_3_4_5_6_7<T1, T2, T3, T4, T5, T6, T7, in T8, in T9, out TRes>(ref T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, ref T5 p5, ref T6 p6, ref T7 p7, T8 p8, T9 p9);

    public delegate TRes FuncRef1_2_3_4_5_6_8<T1, T2, T3, T4, T5, T6, in T7, T8, in T9, out TRes>(ref T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, ref T5 p5, ref T6 p6, T7 p7, ref T8 p8, T9 p9);

    public delegate TRes FuncRef1_2_3_4_5_6_9<T1, T2, T3, T4, T5, T6, in T7, in T8, T9, out TRes>(ref T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, ref T5 p5, ref T6 p6, T7 p7, T8 p8, ref T9 p9);

    public delegate TRes FuncRef1_2_3_4_5_7_8<T1, T2, T3, T4, T5, in T6, T7, T8, in T9, out TRes>(ref T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, ref T5 p5, T6 p6, ref T7 p7, ref T8 p8, T9 p9);

    public delegate TRes FuncRef1_2_3_4_5_7_9<T1, T2, T3, T4, T5, in T6, T7, in T8, T9, out TRes>(ref T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, ref T5 p5, T6 p6, ref T7 p7, T8 p8, ref T9 p9);

    public delegate TRes FuncRef1_2_3_4_5_8_9<T1, T2, T3, T4, T5, in T6, in T7, T8, T9, out TRes>(ref T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, ref T5 p5, T6 p6, T7 p7, ref T8 p8, ref T9 p9);

    public delegate TRes FuncRef1_2_3_4_6_7_8<T1, T2, T3, T4, in T5, T6, T7, T8, in T9, out TRes>(ref T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, T5 p5, ref T6 p6, ref T7 p7, ref T8 p8, T9 p9);

    public delegate TRes FuncRef1_2_3_4_6_7_9<T1, T2, T3, T4, in T5, T6, T7, in T8, T9, out TRes>(ref T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, T5 p5, ref T6 p6, ref T7 p7, T8 p8, ref T9 p9);

    public delegate TRes FuncRef1_2_3_4_6_8_9<T1, T2, T3, T4, in T5, T6, in T7, T8, T9, out TRes>(ref T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, T5 p5, ref T6 p6, T7 p7, ref T8 p8, ref T9 p9);

    public delegate TRes FuncRef1_2_3_4_7_8_9<T1, T2, T3, T4, in T5, in T6, T7, T8, T9, out TRes>(ref T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, T5 p5, T6 p6, ref T7 p7, ref T8 p8, ref T9 p9);

    public delegate TRes FuncRef1_2_3_5_6_7_8<T1, T2, T3, in T4, T5, T6, T7, T8, in T9, out TRes>(ref T1 p1, ref T2 p2, ref T3 p3, T4 p4, ref T5 p5, ref T6 p6, ref T7 p7, ref T8 p8, T9 p9);

    public delegate TRes FuncRef1_2_3_5_6_7_9<T1, T2, T3, in T4, T5, T6, T7, in T8, T9, out TRes>(ref T1 p1, ref T2 p2, ref T3 p3, T4 p4, ref T5 p5, ref T6 p6, ref T7 p7, T8 p8, ref T9 p9);

    public delegate TRes FuncRef1_2_3_5_6_8_9<T1, T2, T3, in T4, T5, T6, in T7, T8, T9, out TRes>(ref T1 p1, ref T2 p2, ref T3 p3, T4 p4, ref T5 p5, ref T6 p6, T7 p7, ref T8 p8, ref T9 p9);

    public delegate TRes FuncRef1_2_3_5_7_8_9<T1, T2, T3, in T4, T5, in T6, T7, T8, T9, out TRes>(ref T1 p1, ref T2 p2, ref T3 p3, T4 p4, ref T5 p5, T6 p6, ref T7 p7, ref T8 p8, ref T9 p9);

    public delegate TRes FuncRef1_2_3_6_7_8_9<T1, T2, T3, in T4, in T5, T6, T7, T8, T9, out TRes>(ref T1 p1, ref T2 p2, ref T3 p3, T4 p4, T5 p5, ref T6 p6, ref T7 p7, ref T8 p8, ref T9 p9);

    public delegate TRes FuncRef1_2_4_5_6_7_8<T1, T2, in T3, T4, T5, T6, T7, T8, in T9, out TRes>(ref T1 p1, ref T2 p2, T3 p3, ref T4 p4, ref T5 p5, ref T6 p6, ref T7 p7, ref T8 p8, T9 p9);

    public delegate TRes FuncRef1_2_4_5_6_7_9<T1, T2, in T3, T4, T5, T6, T7, in T8, T9, out TRes>(ref T1 p1, ref T2 p2, T3 p3, ref T4 p4, ref T5 p5, ref T6 p6, ref T7 p7, T8 p8, ref T9 p9);

    public delegate TRes FuncRef1_2_4_5_6_8_9<T1, T2, in T3, T4, T5, T6, in T7, T8, T9, out TRes>(ref T1 p1, ref T2 p2, T3 p3, ref T4 p4, ref T5 p5, ref T6 p6, T7 p7, ref T8 p8, ref T9 p9);

    public delegate TRes FuncRef1_2_4_5_7_8_9<T1, T2, in T3, T4, T5, in T6, T7, T8, T9, out TRes>(ref T1 p1, ref T2 p2, T3 p3, ref T4 p4, ref T5 p5, T6 p6, ref T7 p7, ref T8 p8, ref T9 p9);

    public delegate TRes FuncRef1_2_4_6_7_8_9<T1, T2, in T3, T4, in T5, T6, T7, T8, T9, out TRes>(ref T1 p1, ref T2 p2, T3 p3, ref T4 p4, T5 p5, ref T6 p6, ref T7 p7, ref T8 p8, ref T9 p9);

    public delegate TRes FuncRef1_2_5_6_7_8_9<T1, T2, in T3, in T4, T5, T6, T7, T8, T9, out TRes>(ref T1 p1, ref T2 p2, T3 p3, T4 p4, ref T5 p5, ref T6 p6, ref T7 p7, ref T8 p8, ref T9 p9);

    public delegate TRes FuncRef1_3_4_5_6_7_8<T1, in T2, T3, T4, T5, T6, T7, T8, in T9, out TRes>(ref T1 p1, T2 p2, ref T3 p3, ref T4 p4, ref T5 p5, ref T6 p6, ref T7 p7, ref T8 p8, T9 p9);

    public delegate TRes FuncRef1_3_4_5_6_7_9<T1, in T2, T3, T4, T5, T6, T7, in T8, T9, out TRes>(ref T1 p1, T2 p2, ref T3 p3, ref T4 p4, ref T5 p5, ref T6 p6, ref T7 p7, T8 p8, ref T9 p9);

    public delegate TRes FuncRef1_3_4_5_6_8_9<T1, in T2, T3, T4, T5, T6, in T7, T8, T9, out TRes>(ref T1 p1, T2 p2, ref T3 p3, ref T4 p4, ref T5 p5, ref T6 p6, T7 p7, ref T8 p8, ref T9 p9);

    public delegate TRes FuncRef1_3_4_5_7_8_9<T1, in T2, T3, T4, T5, in T6, T7, T8, T9, out TRes>(ref T1 p1, T2 p2, ref T3 p3, ref T4 p4, ref T5 p5, T6 p6, ref T7 p7, ref T8 p8, ref T9 p9);

    public delegate TRes FuncRef1_3_4_6_7_8_9<T1, in T2, T3, T4, in T5, T6, T7, T8, T9, out TRes>(ref T1 p1, T2 p2, ref T3 p3, ref T4 p4, T5 p5, ref T6 p6, ref T7 p7, ref T8 p8, ref T9 p9);

    public delegate TRes FuncRef1_3_5_6_7_8_9<T1, in T2, T3, in T4, T5, T6, T7, T8, T9, out TRes>(ref T1 p1, T2 p2, ref T3 p3, T4 p4, ref T5 p5, ref T6 p6, ref T7 p7, ref T8 p8, ref T9 p9);

    public delegate TRes FuncRef1_4_5_6_7_8_9<T1, in T2, in T3, T4, T5, T6, T7, T8, T9, out TRes>(ref T1 p1, T2 p2, T3 p3, ref T4 p4, ref T5 p5, ref T6 p6, ref T7 p7, ref T8 p8, ref T9 p9);

    public delegate TRes FuncRef2_3_4_5_6_7_8<in T1, T2, T3, T4, T5, T6, T7, T8, in T9, out TRes>(T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, ref T5 p5, ref T6 p6, ref T7 p7, ref T8 p8, T9 p9);

    public delegate TRes FuncRef2_3_4_5_6_7_9<in T1, T2, T3, T4, T5, T6, T7, in T8, T9, out TRes>(T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, ref T5 p5, ref T6 p6, ref T7 p7, T8 p8, ref T9 p9);

    public delegate TRes FuncRef2_3_4_5_6_8_9<in T1, T2, T3, T4, T5, T6, in T7, T8, T9, out TRes>(T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, ref T5 p5, ref T6 p6, T7 p7, ref T8 p8, ref T9 p9);

    public delegate TRes FuncRef2_3_4_5_7_8_9<in T1, T2, T3, T4, T5, in T6, T7, T8, T9, out TRes>(T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, ref T5 p5, T6 p6, ref T7 p7, ref T8 p8, ref T9 p9);

    public delegate TRes FuncRef2_3_4_6_7_8_9<in T1, T2, T3, T4, in T5, T6, T7, T8, T9, out TRes>(T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, T5 p5, ref T6 p6, ref T7 p7, ref T8 p8, ref T9 p9);

    public delegate TRes FuncRef2_3_5_6_7_8_9<in T1, T2, T3, in T4, T5, T6, T7, T8, T9, out TRes>(T1 p1, ref T2 p2, ref T3 p3, T4 p4, ref T5 p5, ref T6 p6, ref T7 p7, ref T8 p8, ref T9 p9);

    public delegate TRes FuncRef2_4_5_6_7_8_9<in T1, T2, in T3, T4, T5, T6, T7, T8, T9, out TRes>(T1 p1, ref T2 p2, T3 p3, ref T4 p4, ref T5 p5, ref T6 p6, ref T7 p7, ref T8 p8, ref T9 p9);

    public delegate TRes FuncRef3_4_5_6_7_8_9<in T1, in T2, T3, T4, T5, T6, T7, T8, T9, out TRes>(T1 p1, T2 p2, ref T3 p3, ref T4 p4, ref T5 p5, ref T6 p6, ref T7 p7, ref T8 p8, ref T9 p9);

    public delegate TRes FuncRef1_2_3_4_5_6_7_8<T1, T2, T3, T4, T5, T6, T7, T8, in T9, out TRes>(ref T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, ref T5 p5, ref T6 p6, ref T7 p7, ref T8 p8, T9 p9);

    public delegate TRes FuncRef1_2_3_4_5_6_7_9<T1, T2, T3, T4, T5, T6, T7, in T8, T9, out TRes>(ref T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, ref T5 p5, ref T6 p6, ref T7 p7, T8 p8, ref T9 p9);

    public delegate TRes FuncRef1_2_3_4_5_6_8_9<T1, T2, T3, T4, T5, T6, in T7, T8, T9, out TRes>(ref T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, ref T5 p5, ref T6 p6, T7 p7, ref T8 p8, ref T9 p9);

    public delegate TRes FuncRef1_2_3_4_5_7_8_9<T1, T2, T3, T4, T5, in T6, T7, T8, T9, out TRes>(ref T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, ref T5 p5, T6 p6, ref T7 p7, ref T8 p8, ref T9 p9);

    public delegate TRes FuncRef1_2_3_4_6_7_8_9<T1, T2, T3, T4, in T5, T6, T7, T8, T9, out TRes>(ref T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, T5 p5, ref T6 p6, ref T7 p7, ref T8 p8, ref T9 p9);

    public delegate TRes FuncRef1_2_3_5_6_7_8_9<T1, T2, T3, in T4, T5, T6, T7, T8, T9, out TRes>(ref T1 p1, ref T2 p2, ref T3 p3, T4 p4, ref T5 p5, ref T6 p6, ref T7 p7, ref T8 p8, ref T9 p9);

    public delegate TRes FuncRef1_2_4_5_6_7_8_9<T1, T2, in T3, T4, T5, T6, T7, T8, T9, out TRes>(ref T1 p1, ref T2 p2, T3 p3, ref T4 p4, ref T5 p5, ref T6 p6, ref T7 p7, ref T8 p8, ref T9 p9);

    public delegate TRes FuncRef1_3_4_5_6_7_8_9<T1, in T2, T3, T4, T5, T6, T7, T8, T9, out TRes>(ref T1 p1, T2 p2, ref T3 p3, ref T4 p4, ref T5 p5, ref T6 p6, ref T7 p7, ref T8 p8, ref T9 p9);

    public delegate TRes FuncRef2_3_4_5_6_7_8_9<in T1, T2, T3, T4, T5, T6, T7, T8, T9, out TRes>(T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, ref T5 p5, ref T6 p6, ref T7 p7, ref T8 p8, ref T9 p9);

    public delegate TRes FuncRef<T1, T2, T3, T4, T5, T6, T7, T8, T9, out TRes>(ref T1 p1, ref T2 p2, ref T3 p3, ref T4 p4, ref T5 p5, ref T6 p6, ref T7 p7, ref T8 p8, ref T9 p9);

}
