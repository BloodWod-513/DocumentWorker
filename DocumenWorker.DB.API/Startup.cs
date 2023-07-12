using DocumenWorker.DB.API.Repository.Interfaces;
using DocumenWorker.DB.API.Repository;
using System.Reflection;
using DocumenWorker.DB.API.Services.Interfaces;
using DocumenWorker.DB.API.Services;
using Microsoft.EntityFrameworkCore;
using DocumenWorker.DB.API.Context;
using DocumenWorker.DB.API.Context.Interfaces;
using Autofac.Core;
using Hangfire;
using DocumenWorker.DB.API.Jobs;
using DocumenWorker.DB.API.Jobs.DomainJobs;
using DocumenWorker.DB.API.Jobs.RecuringJobs;
using NLog.Extensions.Logging;
using Hangfire.Console;

namespace DocumenWorker.DB.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public static IConfigurationRoot ConfigurationRoot { get; private set; }
        public static IServiceCollection ServiceCollection { get; private set; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            ConfigurationRoot = CreateConfigurationBuilder();
        }
        public void ConfigureServices(IServiceCollection services)
        {
            var builder = WebApplication.CreateBuilder();

            ServiceSettings(builder);
            RegisterDepencidies(builder);

            ServiceCollection = builder.Services;

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }


            app.UseHangfireDashboard("/dashboard");
            RecuringJobScheduler.Start();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger()
               .UseSwaggerUI(c =>
               {
                   c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
               });

            app.UseMvc(
                routes =>
                {
                    routes.MapRoute(
                        name: "default",
                        template: "/{controller=Home}/{action=Index}/{id?}");
                }
            );
        }
        private IConfigurationRoot CreateConfigurationBuilder()
        {
            return new ConfigurationBuilder().SetBasePath(
                Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
                .AddJsonFile("appsettings.json", false, false).Build();
        }
        private void ServiceSettings(WebApplicationBuilder builder)
        {
            string nlogConfigName = Configuration.GetValue<string>("NLogerConfig:DefaultConfig");
            builder.Services.AddLogging(opt =>
            {
                opt.AddNLog(nlogConfigName);
                opt.AddConsole();
                opt.AddDebug();
                opt.SetMinimumLevel(LogLevel.Debug);
            });
            builder.Services.AddHangfire(x =>
            {
                x.UseSqlServerStorage(Configuration.GetConnectionString("DefaultConnection"));
                x.UseConsole();
            });
            builder.Services.AddHangfireServer();
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddMvc(options => options.EnableEndpointRouting = false);
            builder.Services.AddSwaggerGen();
        }

        private void RegisterDepencidies(WebApplicationBuilder builder)
        {
            //Add services to the container.
            builder.Services.AddSingleton(ConfigurationRoot);

            builder.Services.AddDbContext<ApplicationContext>(options =>
              options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));


            builder.Services.AddScoped<IWordInfoService, WordInfoService>();
            builder.Services.AddScoped<IWordInfoTempService, WordInfoTempService>();
            builder.Services.AddScoped<IDBContext, ApplicationContext>();
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(EFGenericRepository<>));
        }
    }
}
