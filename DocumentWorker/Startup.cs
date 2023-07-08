using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using DocumentWorker.Infrastructure.Services.Interfaces;
using DocumentWorker.Infrastructure.Services;
using DocumentWorker.Infrastructure.Validator.FileValidator.Interfaces;
using DocumentWorker.Infrastructure.Validator.FileValidator;
using DocumentWorker.DTO.Model.Interfaces;
using DocumentWorker.Infrastructure.Validator.ModelValidator;
using DocumentWorker.Infrastructure.Validator.ModelValidator.Interfaces;
using DocumentWorker.DTO.Model;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using DocumentWorker.Infrastructure.AutofacModules;

namespace DocumentWorker
{
    public class Startup
    {
        public static IConfigurationRoot Configuration { get; private set; }
        public static IContainer ApplicationContainer { get; set; }
        public Startup()
        {

        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            #region Настройка DI
            services = new ServiceCollection();
            Configuration = CreateConfigurationBuilder();

            services.AddSingleton(Configuration);

            string nlogConfigName = Configuration.GetValue<string>("NLogerConfig:DefaultConfig");

            services.AddLogging(opt =>
            {
                opt.AddNLog(nlogConfigName);
                opt.AddConsole();
                opt.AddDebug();
                opt.SetMinimumLevel(LogLevel.Debug);
            });

            var builder = new ContainerBuilder();
            builder.Populate(services);
            builder.RegisterModule<DocumentWorkerInfrastructurePerDependencyModule>();

            ApplicationContainer = builder.Build();
            IServiceProvider serviceProvider = new AutofacServiceProvider(ApplicationContainer);

            return serviceProvider;
            #endregion
        }
        private IConfigurationRoot CreateConfigurationBuilder()
        {
            return new ConfigurationBuilder().SetBasePath(
                Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
                .AddJsonFile("appsettings.json", false, false).Build();
        }
    }
}
