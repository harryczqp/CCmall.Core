using System;
using System.Collections.Generic;
using System.Text;

namespace CCmall.Common.Configurations
{
    public class Startup
    {
        public Cors Cors { get; set; }
        public string ApiName { get; set; }
    }
    public class Cors
    {
        public string IPs { get; set; }
    }
}
