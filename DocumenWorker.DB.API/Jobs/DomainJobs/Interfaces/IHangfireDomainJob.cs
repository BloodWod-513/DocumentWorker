using DocumentWorker.APIDB.DTO.Models.Interfaces;
using Hangfire.Server;

namespace DocumenWorker.DB.API.Jobs.DomainJobs.Interfaces
{
    public interface IHangfireDomainJob<T>
        where T : class, IBaseEntity
    {
        void ExecuteHangfire(PerformContext context, List<T> baseEntity);
    }
}
