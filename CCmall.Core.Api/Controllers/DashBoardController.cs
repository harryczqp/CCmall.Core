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
using DotNetCore.CAP;

namespace CCmall.Core.Api.Controllers
{
    public class DashBoardController : BaseController
    {
        private readonly IHubContext<ChatHub> _hubContext;
        private readonly IRedisManager _redisManager;
        private readonly ICapPublisher _capPublisher;

        public DashBoardController(IHubContext<ChatHub> hubContext, IRedisManager redisManager,ICapPublisher capPublisher)
        {
            _hubContext = hubContext;
            _redisManager = redisManager;
            _capPublisher = capPublisher;
        }

        [HttpGet]
        public IActionResult Getlist()
        {
            var rng = new Random();
            var ret = new { data1 = rng.Next(1000, 3000), data2 = rng.Next(3000, 7000), data3 = rng.Next(3000, 5000) };
            return new ObjectResult(ret);
        }
        [HttpPost]
        public IActionResult SetDashData([FromBody]RequestDashData request)
        {
            var model = _redisManager.Get<RequestDashData>(RedisConstant.DashData);
            if (model == null)
            {
                model = request;
            }
            else
            {
                //Mapper Function
                foreach (var prop in model.GetType().GetProperties())
                {
                    prop.SetValue(model, (int)prop.GetValue(request) + (int)prop.GetValue(model));
                }
            }
            _redisManager.Set(RedisConstant.DashData, model, TimeSpan.FromDays(1));
            _hubContext.Clients.All.SendAsync("GetDashData", SerializeHelper.Serialize(model));
            return new ObjectResult(model);
        }
        [HttpGet]
        public IActionResult CapTest()
        {
            return new ObjectResult(null);
        }
    }
}
