using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Macrocosm.Filter;
using Macrocosm.Service;
using Macrocosm.Tool;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Macrocosm
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            var config = Configuration.GetConnectionString("MySQLConnection");
            services.AddDbContext<SqlContext>(option => {
                option.UseMySql(config); // 使用mysql
            });
            //注入
            services.AddScoped<IMovieService, MovieService>();
            services.AddScoped<ISystemService, SystemService>();
            services.AddScoped(typeof(IRepository<>), typeof(MysqlHelper<>));
            services.AddScoped<CommonFilter>();
            services.AddScoped<SystemFiltercs>();

            services.AddMvc()
                .AddNewtonsoftJson()
                .SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_3_0);

            // 使用redis缓存
            var section = Configuration.GetSection("Redis:Default");
            string _connectionString = section.GetSection("Connection").Value;//连接字符串
            string _instanceName = section.GetSection("InstanceName").Value; //实例名称
            int _defaultDB = int.Parse(section.GetSection("DefaultDB").Value ?? "0"); //默认redis数据库
            
            services.AddSingleton(new RedisHelper(_connectionString, _instanceName, _defaultDB));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseStatusCodePagesWithReExecute("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=List}/{id?}");
            });
        }
    }
}
