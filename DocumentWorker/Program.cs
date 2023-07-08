using Autofac;
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
            string path = TxtFilesFolderPath + "ForRead.txt";
            FileInfo txtFileInfo = new FileInfo(path);
            var wordProcessingService = Startup.ApplicationContainer.Resolve<WordProcessingService>();
            var result = wordProcessingService.GetWords(txtFileInfo);
        }
    }
}