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

namespace DocumentWorker
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; private set; }

        public Startup()
        {

        }

        public void ConfigureServices(IServiceCollection services)
        {
            #region Настройка DI
            services.AddTransient<ITxtFileReaderService, TxtFileReaderWithValidationService>();
            services.AddTransient<ITxtFileValidator, TxtFileValidator>();
            services.AddTransient(typeof(IStringParserService), typeof(WordInfoParserWithValidationService<WordInfo>));
            services.AddTransient(typeof(IModelValidator<>), typeof(ModelValidator<>));
            services.AddTransient<WordProcessingService>();
            services.AddLogging(opt =>
            {
                opt.AddNLog();
                opt.AddConsole();
                opt.AddDebug();
                opt.SetMinimumLevel(LogLevel.Debug);
            });
            IServiceProvider serviceProvider = services.BuildServiceProvider();
            #endregion

            Configuration = CreateConfigurationBuilder();

            services.AddSingleton(Configuration);
        }
        private IConfigurationRoot CreateConfigurationBuilder()
        {
            return new ConfigurationBuilder().SetBasePath(
                Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
                .AddJsonFile("appsettings.json", false, false).Build();
        }
    }
}
