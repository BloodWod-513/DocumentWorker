using Hangfire.Server;
using Hangfire;
using DocumenWorker.DB.API.Jobs.RecuringJobs.Interfaces;

namespace DocumenWorker.DB.API.Jobs.RecuringJobs
{
    public class RecuringJobScheduler
    {
        private static List<Type> _types = new List<Type>();
        public static void Start()
        {
            RecurringJob.AddOrUpdate(() => UpdateWordInfoTableJob(null), "*/10 * * * * *", TimeZoneInfo.Local);
        }
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
        public static void UpdateWordInfoTableJob(PerformContext performContext)
        {
            Job<UpdateWordInfoTableJob>(performContext);
        }
    }
}
