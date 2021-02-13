using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Macrocosm.Tool;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Macrocosm
{
    class Program
    {
        private static ILogger logger;
        
        public static void Main(string[] args)
        {
            try
            {
                var loggerFactory = LoggerFactory.Create(builder =>
                {
                    builder
                        .AddFilter("Microsoft", LogLevel.Warning)
                        .AddFilter("System", LogLevel.Warning)
                        .AddFilter("LoggingConsoleApp.Program", LogLevel.Debug)
                        .AddConsole()
                        .AddEventLog();
                });
                logger = loggerFactory.CreateLogger<Program>();

                var host = CreateHostBuilder(args).Build();
                using (var scope = host.Services.CreateScope())
                {
                    var services = scope.ServiceProvider;
                    var context = services.GetRequiredService<SqlContext>();
                    context.Database.EnsureCreated();
                }
                logger.LogCritical($"[{DateTime.Now:s}] --- Macrocosm Service started successfully");
                host.Run();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "[{date}] --- An error occurred creating the DB.",DateTime.Now.ToShortTimeString());
            }
        }
        /// <summary>
        /// ’æµ„øÚº‹œ‡πÿ≈‰÷√
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureLogging(logger => {
                    logger.AddConsole(configure => {
                        configure.TimestampFormat = "YYYY/MM/DD hh:ss";
                    });
                    logger.ClearProviders();
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseUrls("http://*:8080", "https://*:8081");
                    webBuilder.UseStartup<Startup>();
                });
    }
}
