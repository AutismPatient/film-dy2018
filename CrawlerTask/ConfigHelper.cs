using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace CrawlerTask
{
    /// <summary>
    /// read json for APPConfig
    /// </summary>
    public class ConfigHelper
    {
        public IConfiguration Configuration { get; set; }

        public ConfigHelper()
        {
            ReadFile();
        }
        public void ReadFile()
        {
            var fileName = "consolesetting.json";
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile(fileName, false, true);
            Configuration = builder.Build();
        }
        public IConfigurationSection GetConfig(string key)
        {
            var root = Configuration.GetSection(key);
            if (root.Exists())
            {
                return root;
            }
            return null;
        }
        /// <summary>
        /// 返回配置项
        /// </summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        public string GetDefaultConfigurationValue(string key= "Default")
        {
            var _iconfig = GetConfig(key);
            if (_iconfig.Exists())
            {
                var children = _iconfig.GetChildren();
                foreach(var v in children)
                {
                    return v?.Value;
                }
            }
            return "";
        }
        /// <summary>
        /// 获取配置项下的所有下级值
        /// </summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        public IEnumerable<IConfigurationSection> GetConfigurationSections(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return null;
            }
            var _iconfig = GetConfig(key);
            if (_iconfig.Exists())
            {
                var children = _iconfig.GetChildren();

                return children;
            }
            return null;
        }
    }
}
