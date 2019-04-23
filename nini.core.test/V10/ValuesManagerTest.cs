using System;
using Autofac;
using nini.core.V10;
using Xunit;

namespace nini.core.test
{
    public class ValuesManagerTest
    {
        [Fact]
        public void GetValuesTest()
        {
            ContainerBuilder builder = TestHelper.CreateBuilderWithDefaultModules();
            IContainer container = builder.Build();
            using (var scope = container.BeginLifetimeScope())
            {
                var manager = scope.Resolve<V10.IValuesManager>();

                string[] values = manager.GetValues();

                Assert.Equal("Value 1 from mgr", values[0]);
                Assert.Equal("Value 2 from mgr", values[1]);
            }
        }
    }
}
