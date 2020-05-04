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
        private readonly Logger<RedisManager> _logger;
        private volatile ConnectionMultiplexer _redisConnection;

        public RedisManager(Logger<RedisManager> logger)
        {
            var redisConfig = Appsettings.RedisConfig;
            if (string.IsNullOrEmpty(redisConfig.Connection) || redisConfig.ConnectTimeout == 0 || redisConfig.SyncTimeout == 0)
            {
                throw new ArgumentException("RedisConfig error", nameof(redisConfig));
            }
            _redisConnectionModel = redisConfig;
            _logger = logger;
            _redisConnection = GetRedisConnection();
        }

        private ConnectionMultiplexer GetRedisConnection()
        {
            if (_redisConnection != null && _redisConnection.IsConnected)
            {
                return _redisConnection;
            }
            lock (_redisConnection)
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
                        EndPoints = { _redisConnectionModel.Connection }
                    };
                    _redisConnection = ConnectionMultiplexer.Connect(config);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                }
            }
            return _redisConnection;
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

    }
}
