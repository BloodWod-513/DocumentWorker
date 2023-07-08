using Autofac;
using AutofacWrapper;

namespace DocumentWorker.Infrastructure.AutofacModules
{
    public class DocumentWorkerInfrastructurePerLifetimeScopeModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var module = new DocumentWorkerInfrastructureAutofacModuleBuilder(builder);
            module.Registers(ModuleType.PerLifetimeScope);
        }
    }
}
