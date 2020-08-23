using System;
using System.Collections.Generic;
using System.Text;

namespace CCmall.Common.Redis
{
    public interface IRedisManager
    {
        void Clear();
        bool Get(string key);
        TEntity Get<TEntity>(string key);
        TEntity GetHash<TEntity>(string key, string field, bool isReconnected = false);
        string GetValue(string key);
        void Remove(string key);
        void Set(string key, object value, TimeSpan cacheTime);
        void SetDefaultDatabase(int db);
        bool SetHash(string key, string field, byte[] value, bool isReconnected = false);
        bool SetValue(string key, byte[] value);
    }
}
