using Autofac.Features.AttributeFilters;
using Autofac.Features.Indexed;
using DocumentWorker.DTO.Model;
using DocumentWorker.DTO.Model.Interfaces;
using DocumentWorker.Infrastructure.Services.Interfaces;
using DocumentWorker.Infrastructure.Validator.ModelValidator;
using DocumentWorker.Infrastructure.Validator.ModelValidator.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DocumentWorker.DTO.Model.Interfaces.IModelValidation;

namespace DocumentWorker.Infrastructure.Services
{
    /// <summary>
    /// Класс для парсинга строки через регулярки на отдельные слова.
    /// Слова, которые вернул парсинг проходят валидацию в соотвествии с атрибутами модели WordInfo
    /// </summary>
    public class WordInfoParserWithValidationService<T> : StringIntoSingleWordParserService
        where T : WordInfo
    {
        IIndex<ModelValidatorsType, IModelValidator<T>> _states;
        private readonly IModelValidator<T> _modelValidator;

        public WordInfoParserWithValidationService(IIndex<ModelValidatorsType, IModelValidator<T>> states)
        {
            _states = states;
            _modelValidator = _states[ModelValidatorsType.WordInfo];
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
