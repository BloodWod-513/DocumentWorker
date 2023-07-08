using Autofac;
using AutofacWrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentWorker.Tests.AutofacModules
{
    public class TestPerDependencyModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var module = new TestAutofacModuleBuilder(builder);
            module.Registers(ModuleType.PerDependency);
        }
    }
}
