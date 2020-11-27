using AngleSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Dapper;
using Quartz.Logging;
using Quartz;
using System.Collections.Specialized;
using Quartz.Impl;
using MySql.Data.MySqlClient;

namespace CrawlerTask
{
    class Program
    {
        private static readonly string tiggerName = "CrawlerJobTrigger";
        private static readonly string gropName = "CrawlerJobTriggerGrop";
        private static readonly string jobName = "CrawlerJob";
        private static MySqlConnection MySQL = new MySQLHelper().MySql;
        static void Main(string[] args)
        {
            Run().GetAwaiter().GetResult();
            while (Console.ReadKey().Key != ConsoleKey.Escape) { }
        }
        /// <summary>
        /// 运行定时任务
        /// </summary>
        /// <returns></returns>
        private static async Task Run()
        {
            try
            {
                // 从工厂获取调度程序实例
                NameValueCollection props = new NameValueCollection
                {
                    { "quartz.serializer.type", "binary" }
                };
                // 创建工厂
                StdSchedulerFactory factory = new StdSchedulerFactory(props);
                IScheduler scheduler = await factory.GetScheduler();

                //处于待命状态
                await scheduler.Start();

                // 绑定定时任务
                IJobDetail job = JobBuilder.Create<CrawlerJob>()
                    .WithIdentity(jobName, gropName)
                    .Build();
                var Config = await MySQL.QueryFirstAsync<SystemConfig>("SELECT * FROM System_Config WHERE `Key`='timing'", null);
                int time = int.Parse(Config.Value);
                // 定义触发器 
                ITrigger trigger = TriggerBuilder.Create()
                    .WithIdentity(tiggerName, gropName)
                    .StartNow()
                    .WithSimpleSchedule(x => x
                        .WithIntervalInHours(time)
                        .RepeatForever())
                    .Build();
                await scheduler.ScheduleJob(job, trigger);
            }
            catch (Exception se)
            {
                Console.WriteLine(se);
            }
        }
        /// <summary>
        /// 任务
        /// </summary>
        public class CrawlerJob : IJob
        {
            public async Task Execute(IJobExecutionContext context)
            {
                Console.WriteLine($"[{DateTime.Now.ToString()}] 定时任务开始执行");
                var task = await new CrawlerTask().Dy2018Task(isNext: false);
                Console.WriteLine(task.Message);
            }
        }
    }

}
