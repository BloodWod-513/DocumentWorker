using Hangfire.Server;
using Hangfire;
using DocumenWorker.DB.API.Jobs.RecuringJobs.Interfaces;
using DocumenWorker.DB.API.Hangfire.Filters;

namespace DocumenWorker.DB.API.Jobs.RecuringJobs
{
    /// <summary>
    /// Класс планировщик повторяющихся джоб
    /// </summary>
    public class RecuringJobScheduler
    {
        /// <summary>
        /// Тут лежат джобы
        /// </summary>
        private static List<Type> _types = new List<Type>();

        /// <summary>
        /// Метод, который должен вызываться при запуске программы для запуска всех повторяющихся джоб
        /// </summary>
        public static void Start()
        {
            RecurringJob.AddOrUpdate(() => UpdateWordInfoTableJob(null), "*/10 * * * * *", TimeZoneInfo.Local);
        }

        /// <summary>
        /// Метод, который проверяет наличие джоб в списке _types, нужен для того,
        /// чтобы одна и таже джоба не задвоилась, затроилась и так далее...
        /// То есть, если в списке есть еще джоба и она еще не запущена, то лишние убираются
        /// </summary>
        /// <typeparam name="TJob">Тип джобы</typeparam>
        /// <param name="pContext"></param>
        /// <exception cref="Exception"></exception>
        public static void Job<TJob>(PerformContext pContext) where TJob : IHangfireRecuringJob, new()
        {
            var job = new TJob();
            var typeJob = typeof(TJob);

            if (_types.Contains(typeJob))
                return;

            _types.Add(typeJob);
            try
            {
                job.ExecuteHangfire(pContext);
                _types.Remove(typeJob);
            }
            catch (Exception e)
            {
                if (_types.Contains(typeJob))
                    _types.Remove(typeJob);

                throw new Exception(e.Message, e);
            }
        }
        /// <summary>
        /// Атрибут SkipWhenPreviousJobIsRunning отвечает за то, чтобы выполнялась только одна джоба,
        /// которая будет апдейтить таблицу, то есть, если джоба в процессе, то новая даже не создатся
        /// </summary>
        /// <param name="performContext"></param>
        [SkipWhenPreviousJobIsRunning]
        public static void UpdateWordInfoTableJob(PerformContext performContext)
        {
            Job<UpdateWordInfoTableJob>(performContext);
        }
    }
}
