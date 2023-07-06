using DocumentWorker.Infrastructure.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentWorker.Infrastructure.Services
{
    /// <summary>
    /// Класс для чтения файла
    /// </summary>
    public class TxtFileReaderService : ITxtFileReaderService
    {
        public virtual IEnumerable<string> ReadLine(FileInfo fileInfo)
        {
            string fullFile = fileInfo.FullName;
            using (StreamReader reader = new StreamReader(fullFile))
            {
                string? line;
                while ((line = reader.ReadLine()) != null)
                {
                    yield return line;
                }
            }
        }
    }
}
