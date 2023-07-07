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

namespace DocumentWorker.Tests
{
    [TestClass]
    public class ValidatorsTest
    {
        private const string TestFolderPath = "TestFiles/";
        private readonly ITxtFileValidator _txtFileValidator;
        private readonly IModelValidator<WordInfo> _wordInfoModelValidator;
        private readonly ITxtFileReaderService _fileReaderService;
        private readonly ILogger<ValidatorsTest> _logger;

        public ValidatorsTest()
        {
            #region Настройка DI
            IServiceCollection services = new ServiceCollection();
            services.AddTransient<ITxtFileReaderService, TxtFileReaderWithValidationService>();
            services.AddTransient<ITxtFileValidator, TxtFileValidator>();
            services.AddTransient(typeof(IStringParserService), typeof(WordInfoParserWithValidationService<WordInfo>));
            services.AddTransient(typeof(IModelValidator<>), typeof(ModelValidator<>));
            services.AddTransient<WordProcessingService>();
            services.AddLogging(opt =>
            {
                opt.AddNLog();
                opt.AddConsole();
                opt.AddDebug();
                opt.SetMinimumLevel(LogLevel.Debug);
            });
            IServiceProvider serviceProvider = services.BuildServiceProvider();
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
            Console.WriteLine(isValid);

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