using System;
using System.Collections.Generic;
using System.Text;

namespace CCmall.Common.Configurations
{
    public class BaseDB
    {
        public string ConnId { get; set; }
        public string Connection { get; set; }
        public DataBaseType DbType { get; set; }
        public bool Enabled { get; set; }
    }
    public enum DataBaseType
    {
        MySql = 0,
        SqlServer = 1,
        Sqlite = 2,
        Oracle = 3,
        PostgreSQL = 4
    }
}
