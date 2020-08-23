using System;
using System.Collections.Generic;
using System.Text;

namespace CCmall.Model.Request
{
    public class RequestMachineInfo
    {
        public int MachineId { get; set; }
        public string FileLocation { get; set; }
        /// <summary>
        /// 文件名后缀
        /// </summary>
        public string FileType { get; set; }
    }
}
