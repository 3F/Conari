using System.Collections.Generic;
using net.r_eg.Conari.Extension;
using Xunit;

namespace ConariTest.Extensions
{
    public class ObjectExtensionTest
    {
        [Fact]
        public void IfTest1()
        {
            //strings.ForEach(s => s.Value.If(s => !s.Disposed, s => s.Dispose()));
            //strings.Where(s => s.Value.Disposed == false).ForEach(s => s.Value.Dispose());

            Dictionary<int, _KV<bool, bool?>> data = new()
            {
                { 0, new _KV<bool, bool?>(false, true) },
                { 1, new _KV<bool, bool?>(true, true) },
                { 2, new _KV<bool, bool?>(true, true) },
                { 3, new _KV<bool, bool?>(false, true) },
            };

            data.ForEach(d => d.Value.If(d => d.v1, d => d.v2 = false));

            Assert.False(data[0].v1);
            Assert.True(data[1].v1);
            Assert.True(data[2].v1);
            Assert.False(data[3].v1);

            Assert.True(data[0].v2);
            Assert.False(data[1].v2);
            Assert.False(data[2].v2);
            Assert.True(data[3].v2);

            data.ForEach(d => d.Value.If(d => !d.v1, d => d.v2 = null));

            Assert.False(data[0].v1);
            Assert.True(data[1].v1);
            Assert.True(data[2].v1);
            Assert.False(data[3].v1);

            Assert.Null(data[0].v2);
            Assert.False(data[1].v2);
            Assert.False(data[2].v2);
            Assert.Null(data[3].v2);
        }

        private sealed class _KV<T1, T2>
        {
            public T1 v1;
            public T2 v2;

            public _KV(T1 v1, T2 v2) { this.v1 = v1; this.v2 = v2; }
        }
    }
}
