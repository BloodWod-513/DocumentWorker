using DocumentWorker.DTO.Model;
using DocumentWorker.Infrastructure.Validator.FileValidator;
using DocumentWorker.Infrastructure.Validator.FileValidator.Interfaces;
using DocumentWorker.Infrastructure.Validator.ModelValidator;
using DocumentWorker.Infrastructure.Validator.ModelValidator.Interfaces;
using System.Text.RegularExpressions;

namespace DocumentWorker.Tests
{
    [TestClass]
    public class ValidatorsTest
    {
        private const string TestFolderPath = "TestFiles/";
        private readonly ITxtFileValidator _txtFileValidator;
        private readonly IModelValidator<WordInfo> _wordInfoModelValidator;

        public ValidatorsTest()
        {
            _txtFileValidator = new TxtFileValidator();
            _wordInfoModelValidator = new ModelValidator<WordInfo>();
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