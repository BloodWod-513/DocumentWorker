using Autofac;
using Hangfire.Server;
using Hangfire.Console;
using DocumentWorker.APIDB.DTO.Models.Interfaces;
using DocumenWorker.DB.API.Jobs.DomainJobs.Interfaces;

namespace DocumenWorker.DB.API.Jobs.DomainJobs
{
    /// <summary>
    /// Базовый класс для background джоб, который работает с моделями от интерфейса IBaseEntity.
    /// То есть, эти джобы можно использовать для CRUD операций
    /// </summary>
    /// <typeparam name="T">Тип модели</typeparam>
    public abstract class HangfireDomainBaseJob<T> : IHangfireDomainJob<T>
        where T : class, IBaseEntity
    {
        protected ILogger<HangfireDomainBaseJob<T>> _logger;
        public PerformContext Context { get; private set; }

        public void WriteLine(string msg)
        {
            Context?.WriteLine(msg);
        }

        public virtual void ExecuteHangfire(PerformContext context, List<T> baseEntity)
        {
            Context = context;
            var type = GetType();
            using (var serviceProvider = Startup.ServiceCollection.BuildServiceProvider())
            {
                _logger = serviceProvider.GetService<ILogger<HangfireDomainBaseJob<T>>>();
                _logger.LogInformation($"Запуск работы {type}");
                try
                {
                    DoJob(baseEntity);

                    _logger.LogInformation($"Конец рабты {type}");
                }
                catch (Exception exception)
                {
                    WriteLine(exception.ToString());
                    _logger.LogError($"Ошибка при работе {type}", exception);
                    throw new Exception(exception.Message, exception);
                }
            }
        }

        public abstract void DoJob(List<T> baseEntity);
    }

}
