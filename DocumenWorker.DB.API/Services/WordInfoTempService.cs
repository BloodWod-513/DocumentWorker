using DocumenWorker.DB.API.Domains;
using DocumenWorker.DB.API.Models;
using DocumenWorker.DB.API.Repository.Interfaces;
using DocumenWorker.DB.API.Services.Interfaces;

namespace DocumenWorker.DB.API.Services
{
    public class WordInfoTempService : IWordInfoTempService
    {
        private readonly IGenericRepository<WordInfoTempDomain> _wordInfoTempRepo;

        public WordInfoTempService(IGenericRepository<WordInfoTempDomain> wordInfoTempRepo)
        {
            _wordInfoTempRepo = wordInfoTempRepo;
        }

        public bool Add(WordInfoTempDomain wordInfo)
        {
            wordInfo.IsNew = true;
            return _wordInfoTempRepo.Add(wordInfo);
        }
        public WordInfoTempDomain GetById(int id) => _wordInfoTempRepo.FindById(id);
        public bool Remove(int id) => _wordInfoTempRepo.Remove(id);

        public bool Update(WordInfoTempDomain wordInfo) => _wordInfoTempRepo.Update(wordInfo);
    }
}
