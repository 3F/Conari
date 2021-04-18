using System;
using net.r_eg.Conari;
using Xunit;

namespace ConariTest
{
    using static _svc.TestHelper;

    public class AddressesTest
    {
        [Fact]
        public void addrTest1()
        {
            using(var l = new ConariL(UNLIB_DLL))
            {
                Assert.NotEqual(IntPtr.Zero, (IntPtr)l);
                Assert.Equal
                (
                    l.PE.Export.getAddressOf("get_GPtrVal", (IntPtr)l), 
                    l.addr("get_GPtrVal")
                );

                Assert.Equal
                (
                    l.PE.Export.getAddressOf("?getD_True@API@UnLib@Conari@r_eg@net@@YA_N_N@Z", (IntPtr)l),
                    l.addr("?getD_True@API@UnLib@Conari@r_eg@net@@YA_N_N@Z")
                );
            }
        }

        [Fact]
        public void addrTest2()
        {
            using(var l = new ConariL(UNLIB_DLL))
            {
                Assert.Throws<ArgumentException>(() => l.addr(string.Empty));
                Assert.Throws<ArgumentException>(() => l.addr(null));
                Assert.Throws<EntryPointNotFoundException>(() => l.addr($"__not_real_proc_{nameof(addrTest2)}"));

                Assert.Equal(0u, l.PE.Export.getAddressOf(string.Empty));
                Assert.Equal(0u, l.PE.Export.getAddressOf(null));
                Assert.Equal(0u, l.PE.Export.getAddressOf($"__not_real_proc_{nameof(addrTest2)}"));

                Assert.Equal((IntPtr)l, l.PE.Export.getAddressOf(string.Empty, (IntPtr)l));
                Assert.Equal((IntPtr)l, l.PE.Export.getAddressOf(null, (IntPtr)l));
                Assert.Equal((IntPtr)l, l.PE.Export.getAddressOf($"__not_real_proc_{nameof(addrTest2)}", (IntPtr)l));
            }
        }

        [Fact]
        public void addrTest3()
        {
            const string proc = "get_GPtrVal";
            using(var l = new ConariL(UNLIB_DLL))
            {
                Assert.NotEqual(IntPtr.Zero, l.Svc.getProcAddr(proc));
                Assert.Equal
                (
                    l.Svc.getProcAddr(proc),
                    l.addr(proc)
                );

                Assert.Equal
                (
                    l.Svc.native(proc),
                    l.addr(proc)
                );
            }
        }
    }
}
