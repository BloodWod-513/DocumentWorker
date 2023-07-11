using Autofac;
using Castle.Core.Configuration;
using DocumentWorker.APIDB.Client.Services;
using DocumentWorker.APIDB.DTO.Models;
using DocumentWorker.DTO.Model;
using DocumentWorker.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Extensions.Logging;
using System;

namespace DocumentWorker
{
    public class Program
    {
        private const string TxtFilesFolderPath = "TxtFiles/";
        static void Main(string[] args)
        {
            IServiceCollection services = new ServiceCollection();
            Startup startup = new Startup();
            startup.ConfigureServices(services);
            IServiceProvider serviceProvider = startup.ConfigureServices(services);

            Run();
        }

        public static void Run()
        {
            string apiDBUrl = Startup.Configuration.GetValue<string>("APIDB:Url");
            WordInfoTempRequestService<WordInfoTempDomain> wordInfoTempRequestService = new WordInfoTempRequestService<WordInfoTempDomain>(apiDBUrl);
            var zxc = new List<WordInfoTempDomain>()
            {
                new WordInfoTempDomain()
                {
                    Name = "testim2",
                    Count = 1,
                },
                new WordInfoTempDomain()
                {
                    Name = "testim3",
                    Count = 1,
                },
            };
            var sss = wordInfoTempRequestService.AddRangeRequest(zxc);
            var ssget = wordInfoTempRequestService.GetRequest(1);
        }
    }
}