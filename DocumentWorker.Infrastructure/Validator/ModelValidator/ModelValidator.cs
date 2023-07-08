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
        private readonly ILogger<WordProcessingService> _logger;
        public ModelValidator(ILogger<WordProcessingService> logger) 
        {
            _logger = logger;
        }
        public bool IsValidModel(T model)
        {
            if (model == null)
            {
                Console.WriteLine("Объект не должен быть равен null");
                return false;
            }

            var context = new ValidationContext(model);
            var results = new List<ValidationResult>();

            if (!System.ComponentModel.DataAnnotations.Validator.TryValidateObject(model, context, results, true))
            {
                string errorMessages = "";
                errorMessages += $"Объекту {model.GetType().Name} не удалось пройти валидацию.\n";
                foreach (var error in results)
                {
                    errorMessages += $"{error.ErrorMessage}\n";
                }
                _logger.LogError(errorMessages);
                return false;
            }
            else
                _logger.LogInformation($"Объект {model.GetType().Name} успешно прошел валидацию.\n");

            return true;
        }
    }
}
