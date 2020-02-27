using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrawlerTask
{
    /// <summary>
    /// sql 连接帮助类
    /// </summary>
    public class MySQLHelper
    {
        //private static string ConnString = "server=192.168.1.2;userid=root;pwd=123456;port=3306;database=redenvelopes;SslMode=none";
        public static ConfigHelper configHelper = new ConfigHelper();
        public MySqlConnection MySql { get; set; }
        public MySQLHelper()
        {
            try
            {
                var children = configHelper.GetConfigurationSections("MySQL");
                var md = new ConfigModel { };
                foreach (var v in children)
                {
                    switch (v.Key){
                        case "Server":
                            md.Server = v.Value;
                            break;
                        case "UserId":
                            md.UserId = v.Value;
                            break;
                        case "PassWord":
                            md.PassWord = v.Value;
                            break;
                        case "Port":
                            md.Port = int.Parse(v.Value);
                            break;
                        case "DataBase":
                            md.DataBase = v.Value;
                            break;
                        case "SslMode":
                            md.SslMode = v.Value;
                            break;
                        default:
                            break;
                    }
                }
                string ConnString = $"server={md.Server};userid={md.UserId};pwd={md.PassWord};port={md.Port};database={md.DataBase};SslMode={md.SslMode};charset=utf8;";
                
                MySqlConnection mySqlConnection = new MySqlConnection(ConnString);
                MySql = mySqlConnection;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public class ConfigModel
        {
            public virtual string Server { get; set; }
            public virtual string UserId { get; set; }
            public virtual string PassWord { get; set; }
            public virtual int Port { get; set; }
            public virtual string DataBase { get; set; }
            public virtual string SslMode { get; set; }
        }
    }
}
