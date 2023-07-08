using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AutofacWrapper.Exceptions
{
    [Serializable]
    public class UnexpectedModuleTypeException : Exception
    {
        private const string EXCEPTION_MSG = "Необработанный тип времени жизни модуля: {0}";

        public UnexpectedModuleTypeException(ModuleType moduleType) : base(string.Format(EXCEPTION_MSG, moduleType))
        {
        }

        public UnexpectedModuleTypeException(string message) : base(message)
        {
        }

        public UnexpectedModuleTypeException(ModuleType moduleType, Exception innerException) : base(string.Format(EXCEPTION_MSG, moduleType), innerException)
        {
        }

        public UnexpectedModuleTypeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }


    }

}
