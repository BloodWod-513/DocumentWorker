using DocumentWorker.Infrastructure.Validator.FileValidator.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DocumentWorker.Infrastructure.Validator.FileValidator
{
    /// <summary>
    /// Валидатор для текстовых файлов
    /// </summary>
    public class TxtFileValidator : ITxtFileValidator
    {
        private const int OneHundredMegaBytes = 100 * 1024 * 1024;
        public bool IsUTF8Encoding(FileInfo file)
        {
            string fullFile = file.FullName;  
            using (var reader = new StreamReader(fullFile, true))
            {
                reader.Peek();
                return reader.CurrentEncoding == Encoding.UTF8;
            }
        }

        public bool IsValidFileSize(FileInfo file) => file.Length <= OneHundredMegaBytes;
    }
}
