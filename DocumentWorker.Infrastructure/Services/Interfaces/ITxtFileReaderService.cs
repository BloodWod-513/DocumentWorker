using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentWorker.Infrastructure.Services.Interfaces
{
    public interface ITxtFileReaderService
    {
        public IEnumerable<string> ReadLine(FileInfo fileInfo);
    }
}
