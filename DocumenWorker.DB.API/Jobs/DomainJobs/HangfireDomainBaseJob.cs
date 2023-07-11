using Autofac;
using Hangfire.Server;
using Hangfire.Console;
using DocumentWorker.APIDB.DTO.Models.Interfaces;
using DocumenWorker.DB.API.Jobs.DomainJobs.Interfaces;

namespace DocumenWorker.DB.API.Jobs.DomainJobs
{
    public abstract class HangfireDomainBaseJob<T> : IHangfireDomainJob<T>
        where T : class, IBaseEntity
    {
        protected ILogger _logger;
        public PerformContext Context { get; private set; }

        public void WriteLine(string msg)
        {
            Context?.WriteLine(msg);
        }

        public virtual void ExecuteHangfire(PerformContext context, List<T> baseEntity)
        {
            Context = context;

            try
            {
                WriteLine("Запуск Job");

                DoJob(baseEntity);

                //_logger.Info($"Stop {GetType()}");
            }
            catch (Exception exception)
            {
                WriteLine(exception.ToString());
                //_logger.Error($"Error {GetType()}", exception);
                throw new Exception(exception.Message, exception);
            }
        }

        public abstract void DoJob(List<T> baseEntity);
    }

}
