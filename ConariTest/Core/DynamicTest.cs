using System;
using System.Reflection;
using net.r_eg.Conari;
using net.r_eg.Conari.Core;
using Xunit;

namespace ConariTest.Core
{
    public class DynamicTest
    {
        [Fact]
        public void CreateEmptyTypeTest1()
        {
            var expRetType = typeof(void);

            Type type   = Dynamic.CreateEmptyType(expRetType);
            var mi      = type.GetMethod(Dynamic.METHOD_NAME);

            Assert.NotNull(mi);
            Assert.Equal(expRetType, mi.ReturnType);
            Assert.Empty(mi.GetParameters());
        }

        [Fact]
        public void CreateEmptyTypeTest2()
        {
            var expRetType  = typeof(bool);
            var expArg0Type = typeof(Int64);
            var expArg1Type = typeof(bool);
            var expArg2Type = typeof(sbyte);

            Type type   = Dynamic.CreateEmptyType("MyFunc", expRetType, expArg0Type, expArg1Type, expArg2Type);
            var mi      = type.GetMethod("MyFunc");

            Assert.Equal(3, mi.GetParameters().Length);
            Assert.Equal(expArg0Type, mi.GetParameters()[0].ParameterType);
            Assert.Equal(expArg1Type, mi.GetParameters()[1].ParameterType);
            Assert.Equal(expArg2Type, mi.GetParameters()[2].ParameterType);
        }

        [Fact]
        public void CreateEmptyTypeTest3()
        {
            Assert.Throws<ArgumentNullException>(() => 
                Dynamic.CreateEmptyType((string)null, typeof(void))
            );
        }

        [Fact]
        public void CreateEmptyTypeTest4()
        {
            Assert.Throws<ArgumentNullException>(() =>
                Dynamic.CreateEmptyType(" ", typeof(void))
            );
        }

        [Fact]
        public void GetMethodInfoTest1()
        {
            var expRetType = typeof(void);

            MethodInfo mi = Dynamic.GetMethodInfo(false, expRetType);

            Assert.NotNull(mi);
            Assert.Equal(Dynamic.METHOD_NAME, mi.Name);
            Assert.Equal(expRetType, mi.ReturnType);
            Assert.Empty(mi.GetParameters());
        }

        [Fact]
        public void GetMethodInfoTest2()
        {
            var expRetType  = typeof(IntPtr);
            var expArg0Type = typeof(int);
            var expArg1Type = typeof(bool);

            MethodInfo mi = Dynamic.GetMethodInfo("MyFunc", expRetType, expArg0Type, expArg1Type);

            Assert.Equal(2, mi.GetParameters().Length);
            Assert.Equal("MyFunc", mi.Name);
            Assert.Equal(expArg0Type, mi.GetParameters()[0].ParameterType);
            Assert.Equal(expArg1Type, mi.GetParameters()[1].ParameterType);
        }

        [Fact]
        public void GetMethodInfoTest3()
        {
            Assert.Equal(Dynamic.METHOD_NAME, Dynamic.GetMethodInfo((string)null, false, typeof(void)).Name);
            Assert.Equal(Dynamic.METHOD_NAME, Dynamic.GetMethodInfo(" ", false, typeof(void)).Name);
        }

        [Fact]
        public void CastTest1()
        {
            Assert.Equal(0x3F, Dynamic.Cast<byte>(0x3F));
            Assert.Equal(3, Dynamic.DCast(typeof(int), 3.14f));

            Assert.Equal(typeof(IntPtr), Dynamic.DCast(typeof(IntPtr), 17).GetType());
            Assert.Equal(typeof(char), Dynamic.DCast(typeof(char), (byte)0x3F).GetType());

            Assert.Equal(null, Dynamic.DCast(typeof(void), 17));
        }

        [Fact]
        public void cacheTest1()
        {
            using(var l = new ConariL(
                                 new Config("") {
                                     LazyLoading = true
                                 }))
            {
                Dynamic._.UseCache = true;

                Assert.Equal("m1", Dynamic.GetMethodInfo("m1", false, typeof(bool), typeof(int)).Name);
                Assert.Equal("m2", Dynamic.GetMethodInfo("m2", typeof(bool), typeof(int)).Name);
                Assert.Equal("m2", Dynamic.GetMethodInfo("m3", typeof(bool), typeof(int)).Name);
                Assert.Equal("m2", Dynamic.GetMethodInfo(typeof(bool), typeof(int)).Name);
                Assert.Equal("m4", Dynamic.GetMethodInfo("m4", false, typeof(bool), typeof(int)).Name);
            }
        }
    }
}
