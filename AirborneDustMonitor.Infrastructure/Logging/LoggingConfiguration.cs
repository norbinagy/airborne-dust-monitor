using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Text;

namespace AirborneDustMonitor.Infrastructure.Logging
{
    // Serilog konfiguráció beállítása a konzol és fájl alapú logoláshoz
    public static class LoggingConfiguration
    {
        public static IServiceCollection AddApplicationLogging(this IServiceCollection services)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Debug()
                .WriteTo.File("logs/airborne-.log", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            services.AddLogging(builder =>
            {
                builder.ClearProviders();
                builder.AddSerilog(dispose: true);
            });

            return services;
        }
    }
}
