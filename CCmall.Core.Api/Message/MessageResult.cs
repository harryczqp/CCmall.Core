using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CCmall.Core.Api.Message
{
    public class MessageResult
    {
        public int code { get; set; }
        public string message { get; set; }
        public object data { get; set; }
        public MessageResult(int code, string msg, Object data)
        {
            this.code = code;
            message = msg;
            this.data = data;
        }
    }
    public class BadExceptionResult : Exception
    {
        public string Error { get; set; }
        public BadExceptionResult(string error)
        {
            Error = error;
        }
    }
}
