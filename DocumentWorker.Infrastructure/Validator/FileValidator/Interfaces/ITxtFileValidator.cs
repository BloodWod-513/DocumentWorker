using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentWorker.Infrastructure.Validator.FileValidator.Interfaces
{
    public interface ITxtFileValidator : IBaseFileValidator
    {
        bool IsUTF8Encoding(FileInfo file);
    }
}
