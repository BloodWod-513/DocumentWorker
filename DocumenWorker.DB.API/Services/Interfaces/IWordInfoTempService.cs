using DocumenWorker.DB.API.Domains;
using DocumenWorker.DB.API.Models;

namespace DocumenWorker.DB.API.Services.Interfaces
{
    public interface IWordInfoTempService
    {
        public bool Add(WordInfoTempDomain wordInfo);
        public WordInfoTempDomain GetById(int id);
        public bool Update(WordInfoTempDomain wordInfo);
        public bool Remove(int id);
    }
}
