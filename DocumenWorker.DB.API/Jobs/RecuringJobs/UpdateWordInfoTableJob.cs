using DocumentWorker.APIDB.DTO.Models.Interfaces;
using DocumentWorker.APIDB.DTO.Models;
using DocumenWorker.DB.API.Repository.Interfaces;
using DocumenWorker.DB.API.Jobs.DomainJobs;

namespace DocumenWorker.DB.API.Jobs.RecuringJobs
{
    public class UpdateWordInfoTableJob : HangfireRecuringBaseJob
    {
        private IGenericRepository<WordInfoDomain> _wordInfoRepo;
        private IGenericRepository<WordInfoTempDomain> _wordInfoTempRepo;
        public UpdateWordInfoTableJob()
        {
        }
        public override void DoJob()
        {
            using (var serviceProvider = Startup.ServiceCollection.BuildServiceProvider())
            {
                _wordInfoRepo = serviceProvider.GetService<IGenericRepository<WordInfoDomain>>();
                _wordInfoTempRepo = serviceProvider.GetService<IGenericRepository<WordInfoTempDomain>>();
                try
                {
                    List<WordInfoTempDomain> wordInfoTempDomains = new List<WordInfoTempDomain>();

                    wordInfoTempDomains = _wordInfoTempRepo.Get();
                    Parallel.ForEach(wordInfoTempDomains, x => x.IsNew = false);
                    _wordInfoTempRepo.UpdateRange(wordInfoTempDomains);

                    Dictionary<string, int> wordDict = new Dictionary<string, int>();

                    foreach (var word in wordInfoTempDomains)
                    {
                        if (wordDict.ContainsKey(word.Name))
                        {
                            wordDict[word.Name]+= word.Count;
                        }
                        else
                        {
                            wordDict.Add(word.Name, word.Count);
                        }
                    }

                    List<WordInfoDomain> wordInfoDomains = wordDict.Select(x => new WordInfoDomain { Name = x.Key, Count = x.Value }).ToList();

                    _wordInfoRepo.UpdateRange(wordInfoDomains);
                    _wordInfoTempRepo.RemoveRange(wordInfoTempDomains);
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
    }
}
