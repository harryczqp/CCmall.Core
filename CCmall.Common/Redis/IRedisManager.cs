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
        string GetValue(string key);
        void Remove(string key);
        void Set(string key, object value, TimeSpan cacheTime);
        bool SetValue(string key, byte[] value);
    }
}
