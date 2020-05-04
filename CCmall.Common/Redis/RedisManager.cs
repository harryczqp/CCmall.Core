using CCmall.Common.Configurations;
using CCmall.Common.Helper;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace CCmall.Common.Redis
{
    public class RedisManager : IRedisManager
    {
        private readonly RedisConfig _redisConnectionModel;
        private volatile ConnectionMultiplexer _redisConnection;
        private readonly object redisConnectionLock = new object();
        public int DefaultDatabase { get; set; } = 0;


        public RedisManager()
        {
            var redisConfig = Appsettings.RedisConfig;
            if (string.IsNullOrEmpty(redisConfig.Connection) || redisConfig.ConnectTimeout == 0 || redisConfig.SyncTimeout == 0)
            {
                throw new ArgumentException("RedisConfig error", nameof(redisConfig));
            }
            _redisConnectionModel = redisConfig;
            _redisConnection = GetRedisConnection();
        }

        private ConnectionMultiplexer GetRedisConnection()
        {
            if (_redisConnection != null && _redisConnection.IsConnected && _redisConnection.GetDatabase().Database == DefaultDatabase)
            {
                return _redisConnection;
            }
            lock (redisConnectionLock)
            {
                if (_redisConnection != null)
                {
                    _redisConnection.Dispose();
                }
                try
                {
                    var config = new ConfigurationOptions
                    {
                        AbortOnConnectFail = false,
                        AllowAdmin = true,
                        ConnectTimeout = _redisConnectionModel.ConnectTimeout,
                        SyncTimeout = _redisConnectionModel.SyncTimeout,
                        Password = _redisConnectionModel.Password,
                        EndPoints = { _redisConnectionModel.Connection },
                        DefaultDatabase = DefaultDatabase//可配
                    };
                    _redisConnection = ConnectionMultiplexer.Connect(config);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return _redisConnection;
        }

        public void SetDefaultDatabase(int db)
        {
            if (db >= 0 && db <= 14)
            {
                DefaultDatabase = db;
            }
        }

        /// <summary>
        /// 清除
        /// </summary>
        public void Clear()
        {
            foreach (var endPoint in this.GetRedisConnection().GetEndPoints())
            {
                var server = _redisConnection.GetServer(endPoint);
                foreach (var key in server.Keys())
                {
                    _redisConnection.GetDatabase().KeyDelete(key);
                }
            }
        }

        /// <summary>
        /// 判断是否存在
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Get(string key)
        {
            return _redisConnection.GetDatabase().KeyExists(key);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetValue(string key)
        {
            return _redisConnection.GetDatabase().StringGet(key);
        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public TEntity Get<TEntity>(string key)
        {
            var value = _redisConnection.GetDatabase().StringGet(key);
            if (value.HasValue)
            {
                //需要用的反序列化，将Redis存储的Byte[]，进行反序列化
                return SerializeHelper.DeserializeBytes<TEntity>(value);
            }
            else
            {
                return default(TEntity);
            }
        }

        /// <summary>
        /// 移除
        /// </summary>
        /// <param name="key"></param>
        public void Remove(string key)
        {
            _redisConnection.GetDatabase().KeyDelete(key);
        }

        /// <summary>
        /// 设置
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="cacheTime"></param>
        public void Set(string key, object value, TimeSpan cacheTime)
        {
            if (value != null)
            {
                //序列化，将object值生成RedisValue
                _redisConnection.GetDatabase().StringSet(key, SerializeHelper.Serialize(value), cacheTime);
            }
        }

        /// <summary>
        /// 增加/修改
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool SetValue(string key, byte[] value)
        {
            return _redisConnection.GetDatabase().StringSet(key, value, TimeSpan.FromSeconds(120));
        }

        /// <summary>
        /// 增加/修改
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool SetHash(string key, string field, byte[] value, bool isReconnected = false)
        {
            if (isReconnected)
            {
                _redisConnection = GetRedisConnection();
            }
            return _redisConnection.GetDatabase().HashSet(key, field, value);
        }
    }
}
