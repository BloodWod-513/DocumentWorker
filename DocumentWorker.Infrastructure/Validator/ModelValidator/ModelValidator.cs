using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using DocumentWorker.DTO.Model;
using DocumentWorker.DTO.Model.Interfaces;
using DocumentWorker.Infrastructure.Validator.ModelValidator.Interfaces;

namespace DocumentWorker.Infrastructure.Validator.ModelValidator
{
    /// <summary>
    /// Валидатор моделей, который описан в соотвествии с IModelValidator
    /// </summary>
    /// <typeparam name="T">Тип объекта</typeparam>
    public class ModelValidator<T> : IModelValidator<T>
        where T : IModelValidation
    {
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
                Console.WriteLine($"Объекту {model.GetType().Name} не удалось пройти валидацию.");
                foreach (var error in results)
                {
                    Console.WriteLine(error.ErrorMessage);
                }
                Console.WriteLine();
                return false;
            }
            else
                Console.WriteLine($"Объект {model.GetType().Name} успешно прошел валидацию.");

            return true;
        }
    }
}
