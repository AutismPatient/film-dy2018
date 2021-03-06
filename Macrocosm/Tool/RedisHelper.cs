﻿using StackExchange.Redis;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;

namespace Macrocosm.Tool
{
    /// <summary>
    /// redis 配置类
    /// </summary>
    public class RedisHelper : IDisposable
    {
        private readonly string _connectionString; //连接字符串
        private readonly string _instanceName; //实例名称
        private readonly int _defaultDB; //默认数据库
        private readonly ConcurrentDictionary<string, ConnectionMultiplexer> _connections;
        private ILogger<RedisHelper> logger;
        public IDatabase Database { get; private set; }
        public RedisHelper(ILogger<RedisHelper> _logger) : base()
        {
            logger = _logger;
        }
        public RedisHelper(string connectionString, string instanceName, int defaultDb = 0)
        {
            
            _connectionString = connectionString;
            _instanceName = instanceName;
            _defaultDB = defaultDb;
            _connections = new ConcurrentDictionary<string, ConnectionMultiplexer>();
            Database = GetDatabase();
        }

        /// <summary>
        /// 获取ConnectionMultiplexer
        /// </summary>
        /// <returns></returns>
        private ConnectionMultiplexer GetConnect()
        {
            try
            {
                return _connections.GetOrAdd(_instanceName, p => ConnectionMultiplexer.Connect(_connectionString));
            }
            catch (Exception ex)
            {
                logger.LogError("Redis Client {instanceName} remote connection error,{ex},at {stack}", _instanceName, ex.Message, ex.StackTrace);
                throw;
            }
        }

        /// <summary>
        /// 获取数据库
        /// </summary>
        /// <returns></returns>
        private IDatabase GetDatabase()
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
        /// <param name="offset"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ICollection<RedisKey> GetAllKeys(int offset = 0, int pageSize = 10)
        {
            return GetServer().Keys(_defaultDB, pageSize: pageSize, pageOffset: offset).ToList();
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
        public async Task ListSetExp<T>(string key, List<T> value, TimeSpan span)
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
            var vList = await Database.ListRangeAsync(key);
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
