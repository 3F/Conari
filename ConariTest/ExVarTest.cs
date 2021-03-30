using System;
using net.r_eg.Conari;
using net.r_eg.Conari.Core;
using net.r_eg.Conari.Native;
using net.r_eg.Conari.Types;
using Xunit;

namespace ConariTest
{
    using static _svc.TestHelper;

    public class ExVarTest
    {
        private readonly IConfig gCfgUnlib = new Config(UNLIB_DLL, true);

        [Fact]
        public void exvarTest1()
        {
            using(var l = new ConariL(gCfgUnlib))
            {
                UInt32 expected = 0x00001CE8;

                Assert.Equal(expected, (UInt32)l.ExVar.DLR.ADDR_SPEC);
                Assert.Equal(expected, l.ExVar.DLR.ADDR_SPEC<UInt32>());
                Assert.Equal(expected, (UInt32)l.ExVar.get("ADDR_SPEC"));
                Assert.Equal(expected, l.ExVar.get<UInt32>("ADDR_SPEC"));
                Assert.Equal(expected, (UInt32)l.ExVar.getVar("ADDR_SPEC"));
                Assert.Equal(expected, l.ExVar.getVar<UInt32>("ADDR_SPEC"));

                Assert.Equal(expected, l.ExVar.getField<UInt32>("ADDR_SPEC").value);
                Assert.Equal(expected, l.ExVar.getField(typeof(UInt32), "ADDR_SPEC").value);

                byte[] raw = l.ExVar.getField(typeof(UInt32).NativeSize(), "ADDR_SPEC").value;
                Assert.Equal(expected, BitConverter.ToUInt32(raw, 0));
            }
        }

        [Fact]
        public void exvarTest2()
        {
            using(var l = new ConariL(UNLIB_DLL, true, "apiprefix_"))
            {
                bool expected = false;
                
                Assert.Equal(expected, l.ExVar.DLR.GFlag<bool>());
                Assert.Equal(expected, l.ExVar.get<bool>("GFlag"));
                Assert.Equal(expected, l.ExVar.getVar<bool>("apiprefix_GFlag"));

                Assert.Equal(expected, l.ExVar.getField<bool>("apiprefix_GFlag").value);
                Assert.Equal(expected, l.ExVar.getField(typeof(bool), "apiprefix_GFlag").value);

                byte[] raw = l.ExVar.getField(typeof(bool).NativeSize(), "apiprefix_GFlag").value;
                Assert.Equal(expected, BitConverter.ToBoolean(raw, 0));
            }
        }

        [Fact]
        public void exvarTest3()
        {
            using(var l = new ConariL(gCfgUnlib))
            {
                string expected = "Hello World!";

                string fld;
                if(IntPtr.Size == sizeof(Int64)) {
                    fld = "?eVariableTest@API@UnLib@Conari@r_eg@net@@3PEBDEB";
                }
                else {
                    fld = "?eVariableTest@API@UnLib@Conari@r_eg@net@@3PBDB";
                }

                if(IntPtr.Size == sizeof(Int32))
                {
                    Assert.Equal(expected, (CharPtr)l.ExVar.get(fld));
                    Assert.Equal(expected, (CharPtr)l.ExVar.getVar(fld));
                }

                Assert.Equal(expected, (CharPtr)l.ExVar.get<IntPtr>(fld));
                Assert.Equal(expected, (CharPtr)l.ExVar.getVar<IntPtr>(fld));

                Assert.Equal(expected, (CharPtr)l.ExVar.getField<IntPtr>(fld).value);
                Assert.Equal(expected, (CharPtr)l.ExVar.getField(typeof(IntPtr), fld).value);

                byte[] raw  = l.ExVar.getField(typeof(IntPtr).NativeSize(), fld).value;
                var rawptr  = (IntPtr.Size == sizeof(Int64)) ? BitConverter.ToInt64(raw, 0) : BitConverter.ToInt32(raw, 0);

                Assert.Equal(expected, (CharPtr)rawptr);
            }
        }

    }
}
