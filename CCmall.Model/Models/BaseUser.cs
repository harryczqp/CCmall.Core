using System;
using System.Collections.Generic;
using System.Text;

namespace CCmall.Model.Models
{
    public class base_user : BaseModel
    {
        public string username { get; set; }
        public string nickname { get; set; }
        public string password { get; set; }
        public string mobile { get; set; }

    }
}
