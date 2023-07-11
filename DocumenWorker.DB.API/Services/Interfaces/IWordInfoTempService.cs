
using DocumentWorker.APIDB.DTO.Models;

namespace DocumenWorker.DB.API.Services.Interfaces
{
    public interface IWordInfoTempService
    {
        public bool Add(WordInfoTempDomain wordInfoTemp);
        public bool AddRange(List<WordInfoTempDomain> wordInfoTemps);
        public bool UpdateRange(List<WordInfoTempDomain> wordInfoTemps);
        public WordInfoTempDomain GetById(int id);
        public bool Update(WordInfoTempDomain wordInfoTemp);
        public bool Remove(int id);
    }
}
