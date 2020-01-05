using CCmall.Core.Api.Message;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace CCmall.Core.Api.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is BadExceptionResult badRequestException)
            {
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.OK;
                context.Result = new ObjectResult(new MessageResult(0, badRequestException.Error, badRequestException.Message));
            }
            else
            {
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Result = new ObjectResult(new MessageResult(0, "服务器错误！", context.Exception.Message));
            }
            context.ExceptionHandled = true;
        }
    }
}
