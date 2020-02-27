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
    public class Program
    {
        public static void Main(string[] args)
        {
            ILogger<Program> logger = default;
            try
            {
                var host = CreateHostBuilder(args).Build();
                using (var scope = host.Services.CreateScope())
                {
                    var services = scope.ServiceProvider;
                    var context = services.GetRequiredService<SqlContext>();
                    context.Database.EnsureCreated();
                    logger = services.GetRequiredService<ILogger<Program>>();
                }
                Console.WriteLine($"[{DateTime.Now.ToString()}]服务启动成功");
                host.Run();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred creating the DB.");
                Console.WriteLine($"错误信息：[{ex.StackTrace}]->{ex.Message}");
            }
        }
        /// <summary>
        /// 站点框架相关配置
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseUrls("http://*:8080", "https://*:8081");
                    webBuilder.UseStartup<Startup>();
                });
    }
}
