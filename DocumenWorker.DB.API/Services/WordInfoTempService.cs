using DocumentWorker.APIDB.DTO.Models;
using DocumenWorker.DB.API.Jobs.DomainJobs;
using DocumenWorker.DB.API.Repository.Interfaces;
using DocumenWorker.DB.API.Services.Interfaces;
using Hangfire;
using Microsoft.EntityFrameworkCore;

namespace DocumenWorker.DB.API.Services
{
    /// <summary>
    /// Сервис для работы с второстепенной таблицей
    /// </summary>
    public class WordInfoTempService : IWordInfoTempService
    {
        private readonly IGenericRepository<WordInfoTempDomain> _wordInfoTempRepo;
        private readonly ILogger<WordInfoTempService> _logger;

        public WordInfoTempService(IGenericRepository<WordInfoTempDomain> wordInfoTempRepo,
            ILogger<WordInfoTempService> logger)
        {
            _wordInfoTempRepo = wordInfoTempRepo;
            _logger = logger;
        }
        #region Не используются, для будущих целей
        public bool Add(WordInfoTempDomain wordInfo)
        {
            wordInfo.IsNew = true;
            return _wordInfoTempRepo.Add(wordInfo);
        }
        public WordInfoTempDomain GetById(int id) => _wordInfoTempRepo.FindById(id);
        public bool Remove(int id) => _wordInfoTempRepo.Remove(id);

        public bool Update(WordInfoTempDomain wordInfo) => _wordInfoTempRepo.Update(wordInfo);

        public bool UpdateRange(List<WordInfoTempDomain> wordInfoTemps) => _wordInfoTempRepo.UpdateRange(wordInfoTemps);
        public bool RemoveRange(List<WordInfoTempDomain> items)
        {
            return _wordInfoTempRepo.RemoveRange(items);
        }
        #endregion
        public bool AddRange(List<WordInfoTempDomain> wordInfoTemps)
        {
            _logger.LogDebug("Пробуем создать BackgroundJob InsertIntoWordInfoTempTableJob..");
            try
            {
                BackgroundJob.Enqueue(() => DomainJobScheduler.InsertIntoWordInfoTempTableJob(null, wordInfoTemps));
                _logger.LogDebug("BackgroundJob InsertIntoWordInfoTempTableJob создана.");
                return true;
            }
            catch (Exception)
            {
                _logger.LogDebug("При создании BackgroundJob InsertIntoWordInfoTempTableJob возникла ошибка.");
                return false;
            }
        }     
    }
}
