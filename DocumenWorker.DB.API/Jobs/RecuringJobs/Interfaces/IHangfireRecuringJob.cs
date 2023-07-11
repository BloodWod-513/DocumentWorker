using Hangfire.Server;

namespace DocumenWorker.DB.API.Jobs.RecuringJobs.Interfaces
{
    public interface IHangfireRecuringJob
    {
        void ExecuteHangfire(PerformContext context);
    }
}
