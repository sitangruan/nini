using System;
using Autofac;
using Moq;
using nini.core.Common.Session;
using nini.core.dal.V10;
using nini.core.V10;
using Xunit;

namespace nini.core.test
{
    public class ValuesManagerTest
    {
        [Fact]
        public void GetValuesTest()
        {
            //Test with default module
            ContainerBuilder builder = TestHelper.CreateBuilderWithDefaultModules();
            IContainer container = builder.Build();
            using (var scope = container.BeginLifetimeScope())
            {
                var manager = scope.Resolve<V10.IValuesManager>();

                string[] values = manager.GetValues();

                Assert.Equal("Value 1 from DAL", values[0]);
                Assert.Equal("Value 2 from DAL", values[1]);
            }
        }

        [Fact]
        public void GetValuesTestWithMock()
        {
            ContainerBuilder builder = TestHelper.CreateBuilderWithDefaultModules();
            IContainer container = builder.Build();
            using (var scope = container.BeginLifetimeScope())
            {
                var mock = new Mock<IValuesProvider>();
                mock.Setup(m => m.ReadValues()).Returns(new string[] { "Value 1 from DAL mock", "Value 2 from DAL mock" });
                var sessionManager = scope.Resolve<ISessionManager>();

                IValuesManager manager = new ValuesManager(mock.Object, sessionManager);
                string[] values = manager.GetValues();

                Assert.Equal("Value 1 from DAL mock", values[0]);
                Assert.Equal("Value 2 from DAL mock", values[1]);
            }
        }

        [Theory]
        [InlineData(3)]
        [InlineData(15)]
        public void GetValueTest(int id)
        {
            ContainerBuilder builder = TestHelper.CreateBuilderWithDefaultModules();
            IContainer container = builder.Build();
            using (var scope = container.BeginLifetimeScope())
            {
                var manager = scope.Resolve<V10.IValuesManager>();

                string value = manager.GetValue(id);

                Assert.Equal(value, $"Value {id} from DAL");
            }
        }

        [Theory]
        [InlineData(3)]
        [InlineData(15)]
        public void GetValueTestWithMock(int id)
        {
            ContainerBuilder builder = TestHelper.CreateBuilderWithDefaultModules();
            IContainer container = builder.Build();
            using (var scope = container.BeginLifetimeScope())
            {
                var mock = new Mock<IValuesProvider>();
                mock.Setup(m => m.ReadValue(It.IsAny<int>())).Returns($"Value {id} from DAL mock");
                var sessionManager = scope.Resolve<ISessionManager>();

                IValuesManager manager = new ValuesManager(mock.Object, sessionManager);
                string value = manager.GetValue(id);

                Assert.Equal(value, $"Value {id} from DAL mock");
            }
        }
    }
}
