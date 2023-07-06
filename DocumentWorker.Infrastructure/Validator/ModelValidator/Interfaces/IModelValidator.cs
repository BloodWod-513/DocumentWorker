using DocumentWorker.DTO.Model;
using DocumentWorker.DTO.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentWorker.Infrastructure.Validator.ModelValidator.Interfaces
{
    /// <summary>
    /// Интерфейс для создания валидаторов моделей наследуемых от IModelValidation
    /// </summary>
    /// <typeparam name="T">Тип объекта</typeparam>
    public interface IModelValidator<T> where T : IModelValidation
    {
        public bool IsValidModel(T model);
    }
}
