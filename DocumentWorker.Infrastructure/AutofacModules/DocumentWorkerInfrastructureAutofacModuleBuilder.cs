﻿using Autofac;
using Autofac.Extras.DynamicProxy;
using AutofacWrapper;
using DocumentWorker.DTO.Model;
using DocumentWorker.Infrastructure.Services;
using DocumentWorker.Infrastructure.Services.Interfaces;
using DocumentWorker.Infrastructure.Validator.FileValidator;
using DocumentWorker.Infrastructure.Validator.FileValidator.Interfaces;
using DocumentWorker.Infrastructure.Validator.ModelValidator;
using DocumentWorker.Infrastructure.Validator.ModelValidator.Interfaces;
using static DocumentWorker.DTO.Model.Interfaces.IModelValidation;

namespace DocumentWorker.Infrastructure.AutofacModules
{
    public class DocumentWorkerInfrastructureAutofacModuleBuilder : AutofacModuleBuilder
    {
        private readonly ContainerBuilder _builder;

        public DocumentWorkerInfrastructureAutofacModuleBuilder(ContainerBuilder builder)
            : base(builder)
        {
            _builder = builder;
        }
        public override void Registers(ModuleType moduleType)
        {
            RegisterValidatorDependencies(moduleType);
            RegisterServiceDependencies(moduleType);
            SimpleRegister<LoggerInterceptor>(moduleType);
        }

        private void RegisterValidatorDependencies(ModuleType moduleType)
        {
            SimpleRegisterWithInterfaceInterceptor<TxtFileValidator, ITxtFileValidator, LoggerInterceptor>(moduleType);
            RegisterGenericWithInterfaceInterceptor(typeof(ModelValidator<>), typeof(IModelValidator<>), moduleType, typeof(LoggerInterceptor), ModelValidatorsType.Default);
            RegisterGenericWithInterfaceInterceptor(typeof(WordInfoModelValidator<>), typeof(IModelValidator<>), moduleType, typeof(LoggerInterceptor), ModelValidatorsType.WordInfo);
        }
        private void RegisterServiceDependencies(ModuleType moduleType)
        {
            SimpleRegisterWithInterfaceInterceptor<TxtFileReaderWithValidationService, ITxtFileReaderService, LoggerInterceptor>(moduleType);
            SimpleRegisterWithClassInterceptor<WordProcessingService, LoggerInterceptor>(moduleType);
            _builder.RegisterType<WordInfoParserWithValidationService<WordInfo>>()
                .As<IStringParserService>()
                .SetLifeTime(moduleType)
                .EnableInterfaceInterceptors()
                .InterceptedBy(typeof(LoggerInterceptor));
        }
    }
}
