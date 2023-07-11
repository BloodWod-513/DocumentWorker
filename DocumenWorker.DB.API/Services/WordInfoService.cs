using DocumenWorker.DB.API.Domains;
using DocumenWorker.DB.API.Models;
using DocumenWorker.DB.API.Repository.Interfaces;
using DocumenWorker.DB.API.Services.Interfaces;

namespace DocumenWorker.DB.API.Services
{
    public class WordInfoService : IWordInfoService
    {
        private readonly IGenericRepository<WordInfoDomain> _wordInfoServiceRepository;

        public WordInfoService(IGenericRepository<WordInfoDomain> wordInfoServiceRepository)
        {
            _wordInfoServiceRepository = wordInfoServiceRepository;
        }

        public bool Add(WordInfoDomain wordInfo) => _wordInfoServiceRepository.Add(wordInfo);
        public WordInfoDomain GetById(int id) => _wordInfoServiceRepository.FindById(id);
        public bool Remove(int id) => _wordInfoServiceRepository.Remove(id);

        public bool Update(WordInfoDomain wordInfo) => _wordInfoServiceRepository.Update(wordInfo);
    }
}
