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
using System.Collections.Generic;

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

            Run();
        }

        public static void Run()
        {
            string apiDBUrl = Startup.Configuration.GetValue<string>("APIDB:Url");
            var wordProcessingService = Startup.ApplicationContainer.Resolve<WordProcessingService>();
            FileInfo fileInfo = new FileInfo(TxtFilesFolderPath + "ForRead.txt");

            WordInfoTempRequestService<WordInfoTempDomain> wordInfoTempRequestService = new WordInfoTempRequestService<WordInfoTempDomain>(apiDBUrl);

            var dict = wordProcessingService.GetWords(fileInfo);
            List<WordInfoTempDomain> wordInfoTemps = dict.Select(x => new WordInfoTempDomain { Name = x.Key, Count = x.Value.CountWordInText }).ToList();

            var request = wordInfoTempRequestService.AddRangeRequest(wordInfoTemps);
        }
    }
}