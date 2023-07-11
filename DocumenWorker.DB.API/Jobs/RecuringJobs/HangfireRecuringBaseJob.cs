using Autofac;
using DocumenWorker.DB.API.Jobs.RecuringJobs.Interfaces;
using Hangfire.Console;
using Hangfire.Server;

namespace DocumenWorker.DB.API.Jobs.RecuringJobs
{
    public abstract class HangfireRecuringBaseJob : IHangfireRecuringJob
    {
        protected ILogger _logger;
        public PerformContext Context { get; private set; }

        public void WriteLine(string msg)
        {
            Context?.WriteLine(msg);
        }

        public virtual void ExecuteHangfire(PerformContext context)
        {
            Context = context;

            try
            {
                WriteLine("Запуск Job");

                DoJob();

                //_logger.Info($"Stop {GetType()}");
            }
            catch (Exception exception)
            {
                WriteLine(exception.ToString());
                //_logger.Error($"Error {GetType()}", exception);
                throw new Exception(exception.Message, exception);
            }

            //using (var scope = Singleton<SiteDependencyResolver>.Instance.Container.BeginLifetimeScope())
            //{
            //    //_logger = scope.Resolve<ILogger>();
            //    //_logger.Info($"Start {GetType()}");

               
            //}
        }

        public abstract void DoJob();
    }
}
