using CCmall.Core.Api.Message;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Net;

namespace CCmall.Core.Api.Filters
{
    public class ResultFilter : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (!IsSkipFilter(context))
            {
                var status = context.HttpContext.Response.StatusCode;
                if (status != (int)HttpStatusCode.OK)
                {
                    context.Result = new ObjectResult(new MessageResult(9999, context.Exception.Message, null));
                    return;
                }
                var originResult = context.Result;

                switch (originResult)
                {
                    case null:
                        if (context.Exception is BadExceptionResult badException)
                        {
                            context.Result = new ObjectResult(new MessageResult(0, badException.Error, null));
                        }
                        break;
                    case ObjectResult objectResult:
                        context.Result = new ObjectResult(new MessageResult(20000, "", objectResult.Value));
                        break;
                    case ContentResult contentResult:
                        context.Result = contentResult;
                        break;
                    default:
                        context.Result = new ObjectResult(new MessageResult(0, "服务器错误！", null));
                        break;
                }
            }
        }
        private static bool IsSkipFilter(ActionExecutedContext context)
        {
            var ret = context.ActionDescriptor.FilterDescriptors.Any(f => f.Filter.GetType().Equals(typeof(NoResultFilter)));
            return ret;
        }
    }
}
