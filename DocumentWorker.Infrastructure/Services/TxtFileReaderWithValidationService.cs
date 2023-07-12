using DocumentWorker.Infrastructure.Validator.FileValidator;
using DocumentWorker.Infrastructure.Validator.FileValidator.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentWorker.Infrastructure.Services
{
    /// <summary>
    /// Класс для чтения файла.
    /// Предварительно проходит валидацию.
    /// </summary>
    public class TxtFileReaderWithValidationService : TxtFileReaderService
    {
        private readonly ITxtFileValidator _txtFileValidator;
        private readonly ILogger<TxtFileReaderWithValidationService> _logger;

        public TxtFileReaderWithValidationService(ILogger<TxtFileReaderWithValidationService> logger,
            ITxtFileValidator txtFileValidator)
        {
            _logger = logger;
            _txtFileValidator = txtFileValidator;
        }

        public override IEnumerable<string> ReadLine(FileInfo fileInfo)
        {
            bool isValid = _txtFileValidator.IsValidFileSize(fileInfo) && _txtFileValidator.IsUTF8Encoding(fileInfo);
            
            if (isValid)
            {
                return base.ReadLine(fileInfo);
            }
            else
            {
                throw new Exception("Ошибка при открытии файла");
            }
        }
    }
}
