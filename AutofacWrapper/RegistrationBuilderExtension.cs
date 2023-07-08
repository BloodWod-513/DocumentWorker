using Autofac.Builder;
using AutofacWrapper.Exceptions;

namespace AutofacWrapper
{
    public static class RegistrationBuilderExtension
    {
        public static IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle> SetLifeTime<TLimit, TActivatorData, TRegistrationStyle>(
            this IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle> regBuilder, ModuleType lfType)
        {
            switch (lfType)
            {
                case ModuleType.PerLifetimeScope:
                    return regBuilder.InstancePerLifetimeScope();
                case ModuleType.PerDependency:
                    return regBuilder.InstancePerDependency();
                case ModuleType.SingleInstance:
                    return regBuilder.SingleInstance();
                default:
                    throw new UnexpectedModuleTypeException(lfType);
            }
        }
    }

}
