using DocumentWorker.APIDB.DTO.Models;
using DocumenWorker.DB.API.Repository.Interfaces;
using DocumenWorker.DB.API.Services.Interfaces;

namespace DocumenWorker.DB.API.Services
{
    /// <summary>
    /// Сервис для работы с второстепенной таблицей
    /// </summary>
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

        public bool UpdateRange(List<WordInfoTempDomain> wordInfoTemps) => _wordInfoTempRepo.UpdateRange(wordInfoTemps);
        public bool AddRange(List<WordInfoTempDomain> wordInfoTemps) => _wordInfoTempRepo.AddRange(wordInfoTemps);
    }
}
