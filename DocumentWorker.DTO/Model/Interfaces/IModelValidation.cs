using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentWorker.DTO.Model.Interfaces
{
    /// <summary>
    /// Интерфейс необходимый для того, чтобы прогонять модель через валидаторы наследуемые от IModelValidator
    /// </summary>
    public interface IModelValidation
    {
        public enum ModelValidatorsType { Default, WordInfo }
    }
}
