using System;
using System.Collections.Generic;
using System.Text;
using CoreProject.Common.Helper;
using CoreProject.Common.Extensions;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;

namespace CoreProject.Common.Redis
{
    public class RedisCacheManager : IRedisCacheManager
    {
        private readonly string redisConnectionString;
        public volatile ConnectionMultiplexer redisConnection;
        private readonly object redisConnectionLock = new object();
        public RedisCacheManager()
        {

            IConfiguration configuration = new ConfigurationBuilder() 
            .AddJsonFile("appsettings.json")
            .Build();
            string redisConfiguration = configuration.GetSections(new string[] { "AppSettings", "RedisCaching", "ConnectionString" });
            //string redisConfiguration = Appsettings.app(new string[] { "AppSettings", "RedisCaching", "ConnectionString" });//获取连接字符串
            if (string.IsNullOrWhiteSpace(redisConfiguration))
            {
                throw new ArgumentException("Redis配置文件为空", nameof(redisConfiguration));
            }
            this.redisConnectionString = redisConfiguration;
            this.redisConnection = GetRedisConnection();
        }
        /// <summary>
        /// 核心代码，获取连接实例
        /// 通过双if夹lock的方式，实现单例模式
        /// </summary>
        /// <returns></returns>
        private ConnectionMultiplexer GetRedisConnection()
        {
            //如果已经连接实例，直接返回
            if (this.redisConnection!=null&&this.redisConnection.IsConnected)
            {
                return this.redisConnection;
            }
            //加锁，防止异步编程中，出现单例无效的问题
            lock (redisConnectionLock)
            {
                if (this.redisConnection!=null)
                {
                    //释放redis连接
                    this.redisConnection.Dispose();
                }
                try
                {
                    this.redisConnection = ConnectionMultiplexer.Connect(redisConnectionString);
                }
                catch (Exception)
                {

                    throw new Exception("Redis服务未启用，请开启服务");
                }
            }
            return this.redisConnection;
        }

        public void Clear()
        {
            foreach (var endPoint in this.GetRedisConnection().GetEndPoints())
            {
                var server = this.GetRedisConnection().GetServer(endPoint);
                foreach (var key in server.Keys())
                {
                    redisConnection.GetDatabase().KeyDelete(key);
                }
            }
        }

        public TEntity Get<TEntity>(string key)
        {
            var value = redisConnection.GetDatabase().StringGet(key);
            if (value.HasValue)
            {
                return SerializeHelper.Deserialize<TEntity>(value);
            }
            else
            {
                return default(TEntity);    
            }
        }

        public bool Get(string key)
        {
            return redisConnection.GetDatabase().KeyExists(key);
        }

        public string GetValue(string key)
        {
            return redisConnection.GetDatabase().StringGet(key);
        }

        public void Remove(string key)
        {
            redisConnection.GetDatabase().KeyDelete(key);
        }

        public void Set(string key, object value, TimeSpan cacheTime)
        {
            if (value != null)
            {
                redisConnection.GetDatabase().StringSet(key, SerializeHelper.Serialize(value), cacheTime);
            }
        }

        public bool SetValue(string key,byte[] value)
        {
          return  redisConnection.GetDatabase().StringSet(key, value, TimeSpan.FromSeconds(120));
        }
    }
}
