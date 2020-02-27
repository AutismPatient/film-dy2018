using StackExchange.Redis;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Macrocosm.Tool
{
    /// <summary>
    /// redis 配置类
    /// </summary>
    public class RedisHelper : IDisposable
    {
        private string _connectionString; //连接字符串
        private string _instanceName; //实例名称
        private int _defaultDB; //默认数据库
        private ConcurrentDictionary<string, ConnectionMultiplexer> _connections;
        public IDatabase Database { get; set; }
        public RedisHelper(string connectionString, string instanceName, int defaultDB = 0)
        {
            _connectionString = connectionString;
            _instanceName = instanceName;
            _defaultDB = defaultDB;
            _connections = new ConcurrentDictionary<string, ConnectionMultiplexer>();
            Database = GetDatabase();
        }

        /// <summary>
        /// 获取ConnectionMultiplexer
        /// </summary>
        /// <returns></returns>
        private ConnectionMultiplexer GetConnect()
        {
            return _connections.GetOrAdd(_instanceName, p => ConnectionMultiplexer.Connect(_connectionString));
        }

        /// <summary>
        /// 获取数据库
        /// </summary>
        /// <param name="configName"></param>
        /// <param name="db">默认为0：优先代码的db配置，其次config中的配置</param>
        /// <returns></returns>
        public IDatabase GetDatabase()
        {
            return GetConnect().GetDatabase(_defaultDB);
        }

        public IServer GetServer(string configName = null, int endPointsIndex = 0)
        {
            var confOption = ConfigurationOptions.Parse(_connectionString);
            return GetConnect().GetServer(confOption.EndPoints[endPointsIndex]);
        }
        /// <summary>
        /// 获取库中所有键
        /// </summary>
        /// <param name="page"></param>
        /// <param name="page_size"></param>
        /// <returns></returns>
        public ICollection<RedisKey> GetAllKeys(int offset = 0, int page_size = 10)
        {
            return GetServer().Keys(_defaultDB, pageSize: page_size,pageOffset: offset).ToList();
        }
        public ISubscriber GetSubscriber(string configName = null)
        {
            return GetConnect().GetSubscriber();
        }
        public void Dispose()
        {
            if (_connections != null && _connections.Count > 0)
            {
                foreach (var item in _connections.Values)
                {
                    item.Close();
                }
            }
        }
        /// <summary>
        /// 设置List
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task ListSet<T>(string key, List<T> value)
        {
            foreach (var single in value)
            {
                var s = JsonConvert.SerializeObject(single); //序列化
                await Database.ListLeftPushAsync(key, s);
            }
        }
        /// <summary>
        /// 设置List 过期时间
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="span"></param>
        /// <returns></returns>
        public async Task ListSetExp<T>(string key,List<T> value,TimeSpan span)
        {
            var model = JsonConvert.SerializeObject(value);
            _ = await Database.StringSetAsync(key, model, span);
        }
        /// <summary>
        /// 单个对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="span"></param>
        /// <returns></returns>
        public async Task ObjSetExp<T>(string key, T value, TimeSpan span)
        {
            var model = JsonConvert.SerializeObject(value);
            _ = await Database.StringSetAsync(key, model, span);
        }
        /// <summary>
        /// 获取集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<List<T>> GetTAsync<T>(string key) 
        {
            var vList =await Database.ListRangeAsync(key);
            List<T> result = new List<T>();
            foreach (var item in vList)
            {
                var model = JsonConvert.DeserializeObject<T>(item); //反序列化
                result.Add(model);
            }
            return result;
        }
        /// <summary>
        /// 获取集合 string --> json
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<List<T>> GetStringAsync<T>(string key)
        {
            string val = await Database.StringGetAsync(key);
            var model = JsonConvert.DeserializeObject<List<T>>(val); //反序列化
            return model;
        }
    }
}
