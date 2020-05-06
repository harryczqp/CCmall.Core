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
using CCmall.Repository.Interface;
using CCmall.Model.Models;

namespace CCmall.Core.Api.Controllers
{
    public class RouterController : BaseController
    {
        private readonly IRouterRepository _routerRepository;

        public RouterController(IRouterRepository routerRepository)
        {
            _routerRepository = routerRepository;
        }
        [AllowAnonymous]
        [HttpGet]
        public IActionResult List()
        {
            var ret = _routerRepository.GetRouterTree(0);
            return new ObjectResult(ret);
        }
    }
}
