using System;
using System.Collections.Generic;
using System.Text;
using Autofac;

namespace nini.core
{
    public class niniCoreModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<V10.ValuesManager>()
                .As<V10.IValuesManager>()
                .SingleInstance()
                .ExternallyOwned()
                .IfNotRegistered(typeof(V10.ValuesManager));
        }
    }
}
