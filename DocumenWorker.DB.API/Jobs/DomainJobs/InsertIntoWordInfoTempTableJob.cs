using Autofac.Core;
using DocumentWorker.APIDB.DTO.Models;
using DocumentWorker.APIDB.DTO.Models.Interfaces;
using DocumenWorker.DB.API.Repository.Interfaces;

namespace DocumenWorker.DB.API.Jobs.DomainJobs
{
    public class InsertIntoWordInfoTempTableJob<T> : HangfireDomainBaseJob<T>
        where T : class, IBaseEntity
    {
        private IGenericRepository<WordInfoTempDomain> _wordInfoTempRepo;
        public InsertIntoWordInfoTempTableJob()
        {
        }
        public override void DoJob(List<T> baseEntity)
        {
            using (var serviceProvider = Startup.ServiceCollection.BuildServiceProvider())
            {
                _wordInfoTempRepo = serviceProvider.GetService<IGenericRepository<WordInfoTempDomain>>();
                try
                {
                    WriteLine("Добавление массива данных в БД..");
                    List<WordInfoTempDomain> wordInfos = baseEntity as List<WordInfoTempDomain>;
                    _wordInfoTempRepo.AddRange(wordInfos);
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
    }
}
