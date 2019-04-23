using Autofac;

namespace nini.core.test
{
    public static class TestHelper
    {
        public static ContainerBuilder CreateBuilderWithDefaultModules()
        {
            var myBuilder = new ContainerBuilder();
            myBuilder.RegisterModule<niniCoreModule>();

            return myBuilder;
        }
    }
}
