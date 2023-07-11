using DocumentWorker.APIDB.DTO.Models;

namespace DocumenWorker.DB.API.Services.Interfaces
{
    public interface IWordInfoService
    {
        public bool AddRange(List<WordInfoDomain> wordInfos);
        public bool UpdateRange(List<WordInfoDomain> wordInfos);
        public bool Add(WordInfoDomain wordInfo);
        public WordInfoDomain GetById(int id);
        public bool Update(WordInfoDomain wordInfo);
        public bool Remove(int id);
    }
}
