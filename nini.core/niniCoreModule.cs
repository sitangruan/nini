﻿using Autofac;
using nini.core.Common.Session;

namespace nini.core
{
    public class niniCoreModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

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
        }
    }
}
