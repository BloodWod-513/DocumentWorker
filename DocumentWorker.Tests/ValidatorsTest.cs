using DocumentWorker.DTO.Model;
using DocumentWorker.Infrastructure.Services.Interfaces;
using DocumentWorker.Infrastructure.Services;
using DocumentWorker.Infrastructure.Validator.FileValidator;
using DocumentWorker.Infrastructure.Validator.FileValidator.Interfaces;
using DocumentWorker.Infrastructure.Validator.ModelValidator;
using DocumentWorker.Infrastructure.Validator.ModelValidator.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Text.RegularExpressions;
using NLog.Extensions.Logging;
using Autofac;
using DocumentWorker.Infrastructure;
using Autofac.Extensions.DependencyInjection;
using Autofac.Extras.DynamicProxy;
using Castle.DynamicProxy;
using DocumentWorker.DTO.Model.Interfaces;
using DocumentWorker.Tests.AutofacModules;

namespace DocumentWorker.Tests
{
    [TestClass]
    public class ValidatorsTest
    {
        private IContainer ApplicationContainer { get; set; }

        private const string TestFolderPath = "TestFiles/";
        private readonly ITxtFileValidator _txtFileValidator;
        private readonly IModelValidator<WordInfo> _wordInfoModelValidator;
        private readonly ITxtFileReaderService _fileReaderService;
        private readonly ILogger<ValidatorsTest> _logger;

        public ValidatorsTest()
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

            _wordInfoModelValidator = serviceProvider.GetService<IModelValidator<WordInfo>>();
            _txtFileValidator = serviceProvider.GetService<ITxtFileValidator>();
            _fileReaderService = serviceProvider.GetService<ITxtFileReaderService>();
            _logger = serviceProvider.GetService<ILogger<ValidatorsTest>>();
            #endregion
        }

        [TestMethod]
        public void TxtFileOver100MBSizeTest()
        {
            string path = TestFolderPath + "testover100mb";
            FileInfo txtFileInfo = new FileInfo(path);
            var isValid = _txtFileValidator.IsValidFileSize(txtFileInfo);

            Assert.IsFalse(isValid);
        }
        [TestMethod]
        public void TxtFileLess100MBSizeTest()
        {
            string path = TestFolderPath + "testless100mb.txt";
            FileInfo txtFileInfo = new FileInfo(path);
            var isValid = _txtFileValidator.IsValidFileSize(txtFileInfo);

            Assert.IsTrue(isValid);
        }
        [TestMethod]
        public void IsUTF8EncodingFile()
        {
            string path = TestFolderPath + "UTF-8 file.txt";
            FileInfo txtFileInfo = new FileInfo(path);

            var isValid = _txtFileValidator.IsUTF8Encoding(txtFileInfo);

            Assert.IsTrue(isValid);
        }
        [TestMethod]
        public void IsNotUTF8EncodingFile()
        {
            string path = TestFolderPath + "Not UTF-8 file.txt";
            FileInfo txtFileInfo = new FileInfo(path);

            var isValid = _txtFileValidator.IsUTF8Encoding(txtFileInfo);

            Assert.IsFalse(isValid);
        }
        [TestMethod]
        public void IsValidWord()
        {           
            var isValid = _wordInfoModelValidator.IsValidModel(new WordInfo("своём"));

            Assert.IsTrue(isValid);
        }
        [TestMethod]
        public void IsNotValidWord()
        {
            var isValid = _wordInfoModelValidator.IsValidModel(new WordInfo("Б1"));

            Assert.IsFalse(isValid);
        }
    }
}