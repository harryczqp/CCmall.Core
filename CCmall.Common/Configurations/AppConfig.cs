using System;
using System.Collections.Generic;
using System.Text;

namespace CCmall.Common.Configurations
{
    public class AppConfig
    {
        public CCmallLogAop CCmallLogAop { get; set; }
    }
    public class CCmallLogAop
    {
        public bool Enable { get; set; } = false;
    }
}
