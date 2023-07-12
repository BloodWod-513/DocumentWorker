using DocumentWorker.DTO.Model;
using DocumentWorker.DTO.Model.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentWorker.Infrastructure.Validator.ModelValidator
{
    public class WordInfoModelValidator<T> : ModelValidator<T>
        where T : WordInfo
    {
        public WordInfoModelValidator(ILogger<T> logger) : base(logger)
        {
        }
        protected override void PrintErrors(T model, List<ValidationResult> validationResults, bool isValid)
        {
            WordInfo tempModel = model as WordInfo;
            if (isValid)
            {
                _logger.LogDebug($"Объект {tempModel.GetType().Name} успешно прошел валидацию. Слово: {tempModel.Text} \n");
            }
            else
            {
                string errorMessages = "";
                errorMessages += $"Объекту {model.GetType().Name} не удалось пройти валидацию. Слово: {tempModel.Text} \n";
                foreach (var error in validationResults)
                {
                    errorMessages += $"{error.ErrorMessage}\n";
                }
                _logger.LogDebug(errorMessages);
            }
        }
    }
}
