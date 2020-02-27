using System;
using System.Collections.Generic;
using System.Text;
using log4net;
using log4net.Config;
using log4net.Repository;

namespace CrawlerTask
{
    /// <summary>
    /// log4net 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Logger<T>
    {
        public ILog Log;
        public Logger()
        {
            ILoggerRepository loggerRepository = LogManager.CreateRepository(typeof(T).Name);
            BasicConfigurator.Configure(loggerRepository);
            Log = LogManager.GetLogger(loggerRepository.Name, $"log_{loggerRepository.Name}");
        }
    }
}
