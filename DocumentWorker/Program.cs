using DocumentWorker.DTO.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Extensions.Logging;
using System;

namespace DocumentWorker
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IServiceCollection services = new ServiceCollection();
            Startup startup = new Startup();
            startup.ConfigureServices(services);
            IServiceProvider serviceProvider = services.BuildServiceProvider();

            var logger = serviceProvider.GetService<ILogger<WordInfo>>();

            logger.LogDebug("Logger is working!");

            // Get Service and call method
            var service = serviceProvider.GetService<IMyService>();
            service.MyServiceMethod();
            //var services = new ServiceCollection();
            //services.AddLogging(opt =>
            //{
            //    opt.AddNLog("nlog.config");
            //    opt.AddConsole();
            //    opt.AddDebug();
            //});

            //var provider = services.BuildServiceProvider();

            ////var logger = provider.GetService<ILogger<WordInfo>>();
            //var logger = provider.GetService<ILoggerFactory>()
            //.CreateLogger<Program>();
            //var service = provider.GetService<IMyService>();
            //service.MyServiceMethod();

            //logger.LogTrace("Trace message");
            //logger.LogDebug("Debug message");
            //logger.LogInformation("Info message");
            //logger.LogWarning("Warning message");
            //logger.LogError("Error message");
            //logger.LogCritical("Critical message");

            //var services = new ServiceCollection();
            //services.AddLogging();
            //var provider = services.BuildServiceProvider();

            //var factory = provider.GetService<ILoggerFactory>();
            //factory.AddNLog();
            //factory.ConfigureNLog("nlog.config");

            //var logger = provider.GetService<ILogger<Program>>();

            //// create a configuration instance
            //var config = new NLog.Config.LoggingConfiguration();

            //// create a console logging target
            //var logConsole = new NLog.Targets.ConsoleTarget();
            //// create a debug output logging target
            //var logDebug = new NLog.Targets.OutputDebugStringTarget();

            //// send logs with levels from Info to Fatal to the console
            //config.AddRule(NLog.LogLevel.Info, NLog.LogLevel.Fatal, logConsole);
            //// send logs with levels from Debug to Fatal to the console
            //config.AddRule(NLog.LogLevel.Debug, NLog.LogLevel.Fatal, logDebug);

            //// apply the configuration
            //NLog.LogManager.Configuration = config;

            //// create a logger
            //var logger = LogManager.GetCurrentClassLogger();

            //// logging
            //logger.Trace("Trace message");
            //logger.Debug("Debug message");
            //logger.Info("Info message");
            //logger.Warn("Warning message");
            //logger.Error("Error message");
            //logger.Fatal("Fatal message");

        }
    }
}