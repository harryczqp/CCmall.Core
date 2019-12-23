using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CCmall.Core.Message
{
    public class MessageResult
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
        public MessageResult(int code, string msg, Object data)
        {
            Code = code;
            Message = msg;
            Data = data;
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
