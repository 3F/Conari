using System;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using net.r_eg.Conari;
using net.r_eg.Conari.Core;
using net.r_eg.Conari.Exceptions;
using net.r_eg.Conari.Native;
using net.r_eg.Conari.Native.Core;
using net.r_eg.Conari.Types;

namespace net.r_eg.ConariTest
{
    [TestClass]
    public class ExVarTest
    {
        private const string UNLIB_DLL = "UnLib.dll";

        [TestMethod]
        public void exvarTest1()
        {
            using(var l = new ConariL(UNLIB_DLL))
            {
                UInt32 expected = 0x00001CE8;

                Assert.AreEqual(expected, (UInt32)l.ExVar.DLR.ADDR_SPEC, "1");
                Assert.AreEqual(expected, l.ExVar.DLR.ADDR_SPEC<UInt32>(), "2");
                Assert.AreEqual(expected, (UInt32)l.ExVar.get("ADDR_SPEC"), "3");
                Assert.AreEqual(expected, l.ExVar.get<UInt32>("ADDR_SPEC"), "4");
                Assert.AreEqual(expected, (UInt32)l.ExVar.getVar("ADDR_SPEC"), "5");
                Assert.AreEqual(expected, l.ExVar.getVar<UInt32>("ADDR_SPEC"), "6");

                Assert.AreEqual(expected, l.ExVar.getField<UInt32>("ADDR_SPEC").value, "7");
                Assert.AreEqual(expected, l.ExVar.getField(typeof(UInt32), "ADDR_SPEC").value, "8");

                byte[] raw = l.ExVar.getField(typeof(UInt32).NativeSize(), "ADDR_SPEC").value;
                Assert.AreEqual(expected, BitConverter.ToUInt32(raw, 0), "9");
            }
        }

        [TestMethod]
        public void exvarTest2()
        {
            using(var l = new ConariL(UNLIB_DLL, "apiprefix_"))
            {
                bool expected = false;
                
                Assert.AreEqual(expected, l.ExVar.DLR.GFlag<bool>(), "1");
                Assert.AreEqual(expected, l.ExVar.get<bool>("GFlag"), "2");
                Assert.AreEqual(expected, l.ExVar.getVar<bool>("apiprefix_GFlag"), "3");

                Assert.AreEqual(expected, l.ExVar.getField<bool>("apiprefix_GFlag").value, "4");
                Assert.AreEqual(expected, l.ExVar.getField(typeof(bool), "apiprefix_GFlag").value, "5");

                byte[] raw = l.ExVar.getField(typeof(bool).NativeSize(), "apiprefix_GFlag").value;
                Assert.AreEqual(expected, BitConverter.ToBoolean(raw, 0), "6");
            }
        }

        [TestMethod]
        public void exvarTest3()
        {
            using(var l = new ConariL(UNLIB_DLL))
            {
                string expected = "Hello World!";
                string fld      = "?eVariableTest@API@UnLib@Conari@r_eg@net@@3PBDB";

                Assert.AreEqual(expected, (CharPtr)l.ExVar.get(fld), "1");
                Assert.AreEqual(expected, (CharPtr)l.ExVar.get<IntPtr>(fld), "2");
                Assert.AreEqual(expected, (CharPtr)l.ExVar.getVar(fld), "3");
                Assert.AreEqual(expected, (CharPtr)l.ExVar.getVar<IntPtr>(fld), "4");

                Assert.AreEqual(expected, (CharPtr)l.ExVar.getField<IntPtr>(fld).value, "5");
                Assert.AreEqual(expected, (CharPtr)l.ExVar.getField(typeof(IntPtr), fld).value, "6");

                byte[] raw      = l.ExVar.getField(typeof(IntPtr).NativeSize(), fld).value;
                CharPtr rawstr  = (IntPtr.Size == sizeof(Int64)) ? (CharPtr)BitConverter.ToInt64(raw, 0) : (CharPtr)BitConverter.ToInt32(raw, 0);
                Assert.AreEqual(expected, rawstr, "7");
            }
        }

    }
}
