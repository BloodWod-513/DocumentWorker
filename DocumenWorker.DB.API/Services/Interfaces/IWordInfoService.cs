using DocumenWorker.DB.API.Domains;
using DocumenWorker.DB.API.Models;

namespace DocumenWorker.DB.API.Services.Interfaces
{
    public interface IWordInfoService
    {
        public bool Add(WordInfoDomain wordInfo);
        public WordInfoDomain GetById(int id);
        public bool Update(WordInfoDomain wordInfo);
        public bool Remove(int id);
    }
}
