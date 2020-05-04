using System;
using System.Collections.Generic;
using System.Text;

namespace CCmall.Common.Configurations
{
    public class RedisConfig
    {
        public string Connection { get; set; }
        public string Password { get; set; }
        public int ConnectTimeout { get; set; }
        public int SyncTimeout { get; set; }
    }
}
