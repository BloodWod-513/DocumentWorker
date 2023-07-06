using DocumentWorker.DTO.Model;
using DocumentWorker.DTO.Model.Interfaces;
using DocumentWorker.Infrastructure.Validator.ModelValidator;
using DocumentWorker.Infrastructure.Validator.ModelValidator.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentWorker.Infrastructure.Services
{
    /// <summary>
    /// Класс для парсинга строки через регулярки на отдельные слова.
    /// Слова, которые вернул парсинг проходят валидацию в соотвествии с атрибутами модели WordInfo
    /// </summary>
    public class WordInfoParserWithValidationService<T> : StringIntoSingleWordParserService
        where T : class, IModelValidation
    {
        private readonly IModelValidator<T> _modelValidator;
        public WordInfoParserWithValidationService()
        {
            _modelValidator = new ModelValidator<T>();
        }

        public override IEnumerable<string> Parse(string str)
        {
            foreach (var wordItem in base.Parse(str))
            {
                WordInfo wordInfo = new WordInfo(wordItem);
                bool isValid = _modelValidator.IsValidModel(wordInfo as T);
                if (!isValid)
                {
                    continue;
                }
                yield return wordInfo.Text;
            }
        }
    }
}
