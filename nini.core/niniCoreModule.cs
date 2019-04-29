using Autofac;
using nini.core.Common.Session;

namespace nini.core
{
    public class niniCoreModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<SessionFactory>()
                .As<ISessionFactory>()
                .SingleInstance()
                .ExternallyOwned()
                .IfNotRegistered(typeof(SessionFactory));

            builder.RegisterType<SessionManager>()
                .As<ISessionManager>()
                .SingleInstance()
                .ExternallyOwned()
                .IfNotRegistered(typeof(SessionManager));

            builder.RegisterType<dal.V10.ValuesProvider>()
                .As<dal.V10.IValuesProvider>()
                .SingleInstance()
                .ExternallyOwned()
                .IfNotRegistered(typeof(dal.V10.ValuesProvider));

            builder.RegisterType<V10.ValuesManager>()
                .As<V10.IValuesManager>()
                .SingleInstance()
                .ExternallyOwned()
                .IfNotRegistered(typeof(V10.ValuesManager));

            builder.RegisterType<dal.V10.LoginProvider>()
                .As<dal.V10.ILoginProvider>()
                .SingleInstance()
                .ExternallyOwned()
                .IfNotRegistered(typeof(dal.V10.LoginProvider));

            builder.RegisterType<V10.LoginManager>()
                .As<V10.ILoginManager>()
                .SingleInstance()
                .ExternallyOwned()
                .IfNotRegistered(typeof(V10.LoginManager));
        }
    }
}
