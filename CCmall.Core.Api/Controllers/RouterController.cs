using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CCmall.Common.Helper;
using Microsoft.AspNetCore.SignalR;
using CCmall.Core.Common.Hubs;
using CCmall.Common.Redis;
using CCmall.Model.Request;
using Microsoft.AspNetCore.Authorization;

namespace CCmall.Core.Api.Controllers
{
    public class RouterController : BaseController
    {

        public RouterController()
        {
        }
        [HttpGet]
        public IActionResult List()
        {
            return new ObjectResult(123);
        }
    }
}
