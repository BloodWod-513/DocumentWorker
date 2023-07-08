using Autofac;
using AutofacWrapper;

namespace DocumentWorker.Infrastructure.AutofacModules
{
    public class DocumentWorkerInfrastructurePerDependencyModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var module = new DocumentWorkerInfrastructureAutofacModuleBuilder(builder);
            module.Registers(ModuleType.PerDependency);
        }
    }
}
