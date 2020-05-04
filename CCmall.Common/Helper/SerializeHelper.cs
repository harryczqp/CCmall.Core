using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CCmall.Common.Helper
{
    public class SerializeHelper
    {
        public static TEntity DeserializeBytes<TEntity>(byte[] data)
        {
            if (data == null)
            {
                return default(TEntity);
            }
            var jsonString = Encoding.UTF8.GetString(data);
            return Deserialize<TEntity>(jsonString);
        }

        public static TEntity Deserialize<TEntity>(string jsonString)
        {
            if (string.IsNullOrEmpty(jsonString))
            {
                return default(TEntity);
            }
            return JsonConvert.DeserializeObject<TEntity>(jsonString);
        }
        public static string Serialize(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
        public static byte[] SerializeToBytes(object item)
        {
            var jsonString = JsonConvert.SerializeObject(item);

            return Encoding.UTF8.GetBytes(jsonString);
        }
    }
}
