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

namespace net.r_eg.Conari.Types
{
    public delegate TRes FuncOut2<in T1, T2, out TRes>(T1 p1, out T2 p2);

    // -

    public delegate TRes FuncOut2<in T1, T2, in T3, out TRes>(T1 p1, out T2 p2, T3 p3);

    public delegate TRes FuncOut3<in T1, in T2, T3, out TRes>(T1 p1, T2 p2, out T3 p3);

    public delegate TRes FuncOut<in T1, T2, T3, out TRes>(T1 p1, out T2 p2, out T3 p3);

    // -

    public delegate TRes FuncOut2<in T1, T2, in T3, in T4, out TRes>(T1 p1, out T2 p2, T3 p3, T4 p4);

    public delegate TRes FuncOut3<in T1, in T2, T3, in T4, out TRes>(T1 p1, T2 p2, out T3 p3, T4 p4);

    public delegate TRes FuncOut4<in T1, in T2, in T3, T4, out TRes>(T1 p1, T2 p2, T3 p3, out T4 p4);

    public delegate TRes FuncOut2And3<in T1, T2, T3, in T4, out TRes>(T1 p1, out T2 p2, out T3 p3, T4 p4);

    public delegate TRes FuncOut3And4<in T1, in T2, T3, T4, out TRes>(T1 p1, T2 p2, out T3 p3, out T4 p4);

    public delegate TRes FuncOut<in T1, T2, T3, T4, out TRes>(T1 p1, out T2 p2, out T3 p3, out T4 p4);
}
