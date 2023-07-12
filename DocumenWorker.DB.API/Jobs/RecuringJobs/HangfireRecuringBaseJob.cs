using Autofac;
using DocumentWorker.APIDB.DTO.Models.Interfaces;
using DocumenWorker.DB.API.Jobs.DomainJobs;
using DocumenWorker.DB.API.Jobs.RecuringJobs.Interfaces;
using Hangfire.Console;
using Hangfire.Server;

namespace DocumenWorker.DB.API.Jobs.RecuringJobs
{
    /// <summary>
    /// Базовый класс для повторяющихся джоб
    /// </summary>
    public abstract class HangfireRecuringBaseJob : IHangfireRecuringJob
    {
        protected ILogger<HangfireRecuringBaseJob> _logger;
        public PerformContext Context { get; private set; }

        public void WriteLine(string msg)
        {
            Context?.WriteLine(msg);
        }

        public virtual void ExecuteHangfire(PerformContext context)
        {
            Context = context;

            var type = GetType();
            using (var serviceProvider = Startup.ServiceCollection.BuildServiceProvider())
            {
                _logger = serviceProvider.GetService<ILogger<HangfireRecuringBaseJob>>();
                _logger.LogInformation($"Запуск работы {type}");
                try
                {
                    DoJob();

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

        public abstract void DoJob();
    }
}
