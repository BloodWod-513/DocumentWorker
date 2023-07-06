using DocumentWorker.Infrastructure.Services;
using DocumentWorker.Infrastructure.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DocumentWorker.Tests
{
    [TestClass]
    public class Class1
    {
        private const string TestFolderPath = "TestFiles/";
        private readonly ITxtFileReaderService _fileReaderService;
        public Class1()
        {
            _fileReaderService = new TxtFileReaderService();
        }

        [TestMethod]
        public void Testik()
        {
            string path = TestFolderPath + "testless100mb.txt";
            FileInfo txtFileInfo = new FileInfo(path);
            var zxc = new WordProcessingService();
            var result = zxc.GetWords(txtFileInfo);
        }
    }
}
