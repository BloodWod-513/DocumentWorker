using Castle.DynamicProxy;
using DocumentWorker.Infrastructure.Services;
using Microsoft.Extensions.Logging;
using NLog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentWorker.Infrastructure
{
    public class LoggerInterceptor : IInterceptor
    {
        private readonly ILogger<LoggerInterceptor> _logger;
        public LoggerInterceptor(ILogger<LoggerInterceptor> logger)
        {
            _logger = logger;
        }

        public void Intercept(IInvocation invocation)
        {
            _logger.LogDebug($"Запуск метода: {invocation.Method.Name}\n");
            invocation.Proceed();
            _logger.LogDebug($"Конец выполнения метода: {invocation.Method.Name}\n");
        }
    }
}
