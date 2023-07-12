using DocumentWorker.APIDB.DTO.Models;
using DocumenWorker.DB.API.Repository.Interfaces;
using DocumenWorker.DB.API.Services.Interfaces;

namespace DocumenWorker.DB.API.Services
{
    /// <summary>
    /// Сервис для работы с основной таблицей
    /// </summary>
    public class WordInfoService : IWordInfoService
    {
        private readonly IGenericRepository<WordInfoDomain> _wordInfoServiceRepository;

        public WordInfoService(IGenericRepository<WordInfoDomain> wordInfoServiceRepository)
        {
            _wordInfoServiceRepository = wordInfoServiceRepository;
        }
        #region Не используются, для будущих целей
        public bool Add(WordInfoDomain wordInfo) => _wordInfoServiceRepository.Add(wordInfo);

        public bool AddRange(List<WordInfoDomain> wordInfos) => _wordInfoServiceRepository.AddRange(wordInfos);

        public WordInfoDomain GetById(int id) => _wordInfoServiceRepository.FindById(id);
        public bool Remove(int id) => _wordInfoServiceRepository.Remove(id);

        public bool Update(WordInfoDomain wordInfo) => _wordInfoServiceRepository.Update(wordInfo);

        public bool UpdateRange(List<WordInfoDomain> wordInfos) => _wordInfoServiceRepository.UpdateRange(wordInfos);
        #endregion
    }
}
