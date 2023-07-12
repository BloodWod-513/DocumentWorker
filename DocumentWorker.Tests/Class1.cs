using DocumentWorker.Infrastructure.Services;
using DocumentWorker.Infrastructure.Services.Interfaces;
using DocumentWorker.Infrastructure.Validator.FileValidator.Interfaces;
using DocumentWorker.Infrastructure.Validator.FileValidator;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DocumentWorker.Infrastructure.Validator.ModelValidator.Interfaces;
using DocumentWorker.Infrastructure.Validator.ModelValidator;
using DocumentWorker.DTO.Model;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Autofac.Extras.DynamicProxy;
using DocumentWorker.Infrastructure;
using DocumentWorker.Tests.AutofacModules;

namespace DocumentWorker.Tests
{
    [TestClass]
    public class Class1
    {
        private IContainer ApplicationContainer { get; set; }

        private const string TestFolderPath = "TestFiles/";
        private readonly ITxtFileReaderService _fileReaderService;
        private readonly WordProcessingService _wordProcessingService;
        private readonly IStringParserService _stringParserService;
        private readonly ILogger<Class1> _logger;

        public Class1()
        {
            #region Настройка DI
            IServiceCollection services = new ServiceCollection();
            services.AddLogging(opt =>
            {
                opt.AddNLog("nlog.config");
                opt.AddConsole();
                opt.AddDebug();
                opt.SetMinimumLevel(LogLevel.Debug);
            });

            var builder = new ContainerBuilder();
            builder.Populate(services);

            builder.RegisterModule<TestPerDependencyModule>();

            this.ApplicationContainer = builder.Build();
            IServiceProvider serviceProvider = new AutofacServiceProvider(ApplicationContainer);

            _fileReaderService = serviceProvider.GetService<ITxtFileReaderService>();
            //_wordProcessingService = serviceProvider.GetService<WordProcessingService>();
            _wordProcessingService = ApplicationContainer.Resolve<WordProcessingService>();
            _logger = serviceProvider.GetService<ILogger<Class1>>();

            #endregion
        }
        [TestMethod]
        public void Testik()
        {
            string path = TestFolderPath + "ForBadRead.txt";
            FileInfo txtFileInfo = new FileInfo(path);
            var zxc = _wordProcessingService;
            var result = zxc.GetWords(txtFileInfo);
        }
    }
}
