using System;
using System.Collections.Generic;
using System.Text;

namespace CCmall.Common.Configurations
{
    public class Jwt
    {
        public string Secret { get; set; }
        public string SecretFile { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
}
