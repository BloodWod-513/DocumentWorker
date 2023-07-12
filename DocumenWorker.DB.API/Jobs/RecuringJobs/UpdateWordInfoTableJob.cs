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
                    WriteLine("Получение данных из таблицы WordInfoTemps..");
                    wordInfoTempDomains = _wordInfoTempRepo.Get();

                    if (!wordInfoTempDomains.Any())
                    {
                        WriteLine("Таблица WordInfoTemp пустая..");
                        return;
                    }    

                    WriteLine("Запуск параллельной простановки флага IsNew в значение false..");
                    Parallel.ForEach(wordInfoTempDomains, x => x.IsNew = false);

                    WriteLine("Обновление данных в таблицу WordTempInfo..");
                    _wordInfoTempRepo.UpdateRange(wordInfoTempDomains);

                    WriteLine("Получаем актуальные данные из таблицы WordInfos..");
                    var actualWordInfos = _wordInfoRepo.Get(x => wordInfoTempDomains.Any(y => y.Name == x.Name));

                    WriteLine("Обратываем все полученные данные..");
                    foreach (var word in wordInfoTempDomains)
                    {
                        var searchWord = actualWordInfos.SingleOrDefault(x => x.Name == word.Name);
                        if (searchWord == null)
                        {
                            actualWordInfos.Add(new WordInfoDomain()
                            { 
                                Name = word.Name, Count = word.Count
                            });
                        }
                        else
                        {
                            searchWord.Count += word.Count;
                        }
                    }                      

                    WriteLine("Обновляем таблицу WordInfos..");
                    _wordInfoRepo.UpdateRange(actualWordInfos);
                    WriteLine("Удаляем лишние данные из таблицы WordInfoTemps..");
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
