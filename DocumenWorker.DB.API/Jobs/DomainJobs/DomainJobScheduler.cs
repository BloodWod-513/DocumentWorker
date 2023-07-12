using DocumentWorker.APIDB.DTO.Models;
using DocumentWorker.APIDB.DTO.Models.Interfaces;
using DocumenWorker.DB.API.Jobs.DomainJobs.Interfaces;
using Hangfire;
using Hangfire.Server;
using System;

namespace DocumenWorker.DB.API.Jobs.DomainJobs
{
    /// <summary>
    /// Планироващик для Background джоб
    /// </summary>
    public class DomainJobScheduler
    {       
        public static void InsertIntoWordInfoTempTableJob(PerformContext performContext, List<WordInfoTempDomain> baseEntity)
        {
            var job = new InsertIntoWordInfoTempTableJob<WordInfoTempDomain>();
            job.ExecuteHangfire(performContext, baseEntity);
        }
    }
}
