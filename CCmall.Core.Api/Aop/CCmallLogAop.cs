using Castle.DynamicProxy;
using CCmall.Common.Attributes;
using CCmall.Common.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace CCmall.Core.Api.Aop
{
    public class CCmallLogAop : IInterceptor
    {
        private readonly ILogger _logger;
        private readonly IHttpContextAccessor _accessor;

        public CCmallLogAop(ILogger logger, IHttpContextAccessor accessor)
        {
            _logger = logger;
            _accessor = accessor;
        }
        public void Intercept(IInvocation invocation)
        {
            try
            {
                if (IsCheckLogAop(invocation))
                {
                    string UserName = _accessor.HttpContext.User.Identity.Name;
                    //记录被拦截方法信息的日志信息
                    var dataIntercept = "" +
                        $"【当前操作用户】：{ UserName} \r\n" +
                        $"【当前执行方法】：{ invocation.Method.Name} \r\n" +
                        $"【携带的参数有】：{string.Join(",", invocation.Arguments.Select(s => (s ?? "").ToString()).ToArray())}";
                }
                invocation.Proceed();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }
        private bool IsCheckLogAop(IInvocation invocation)
        {
            var method = invocation.MethodInvocationTarget ?? invocation.Method;
            return method.GetCustomAttributes(true).Any(a => a.GetType().Equals(typeof(LogAopAttribute)));
        }
    }
}
