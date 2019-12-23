using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CCmall.Core.Filters
{
    [AttributeUsage(AttributeTargets.Class|AttributeTargets.Method,Inherited =true)]
    public class NoResultFilter: ActionFilterAttribute
    {
    }
}
