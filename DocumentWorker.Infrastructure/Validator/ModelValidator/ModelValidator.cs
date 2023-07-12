using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using DocumentWorker.DTO.Model;
using DocumentWorker.DTO.Model.Interfaces;
using DocumentWorker.Infrastructure.Services;
using DocumentWorker.Infrastructure.Validator.ModelValidator.Interfaces;
using Microsoft.Extensions.Logging;

namespace DocumentWorker.Infrastructure.Validator.ModelValidator
{
    /// <summary>
    /// Валидатор моделей, который описан в соотвествии с IModelValidator
    /// </summary>
    /// <typeparam name="T">Тип объекта</typeparam>
    public class ModelValidator<T> : IModelValidator<T>
        where T : IModelValidation
    {
        protected readonly ILogger<T> _logger;
        public ModelValidator(ILogger<T> logger) 
        {
            _logger = logger;
        }
        public bool IsValidModel(T model)
        {
            if (model == null)
            {
                _logger.LogError("Объект не должен быть равен null");
                return false;
            }

            var context = new ValidationContext(model);
            var results = new List<ValidationResult>();

            bool isValid = System.ComponentModel.DataAnnotations.Validator.TryValidateObject(model, context, results, true);

            PrintErrors(model, results, isValid);

            return isValid;
        }
        protected virtual void PrintErrors(T model, List<ValidationResult> validationResults, bool isValid)
        {
            if (isValid)
            {
                _logger.LogDebug($"Объект {model.GetType().Name} успешно прошел валидацию.\n");
            }
            else
            {
                string errorMessages = "";
                errorMessages += $"Объекту {model.GetType().Name} не удалось пройти валидацию.\n";
                foreach (var error in validationResults)
                {
                    errorMessages += $"{error.ErrorMessage}\n";
                }
                _logger.LogDebug(errorMessages);
            }
        }
    }
}
