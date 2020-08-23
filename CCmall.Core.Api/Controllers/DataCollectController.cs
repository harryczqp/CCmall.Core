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
using System.IO;

namespace CCmall.Core.Api.Controllers
{
    public class DataCollectController : BaseController
    {
        private readonly IRedisManager _redisManager;

        public DataCollectController(IRedisManager redisManager)
        {
            _redisManager = redisManager;
        }
        [AllowAnonymous]
        [HttpPost]
        public IActionResult SetMachineProgram([FromBody] RequestMachineInfo request)
        {
            var bytes = SerializeHelper.SerializeToBytes(request);
            var ret = _redisManager.SetHash(RedisConstant.MachineInfo, request.MachineId.ToStr(), bytes);
            return new ObjectResult(ret);
        }
        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetMachineInfo(int mid)
        {
            var machineInfo = _redisManager.GetHash<RequestMachineInfo>(RedisConstant.MachineInfo, mid.ToStr());
            if (machineInfo == null || machineInfo == default(RequestMachineInfo))
            {
                return new ObjectResult(0);
            }
            var path = Path.Combine(machineInfo.FileLocation, $"{DateTime.Now.ToString("yyyyMMdd")}.{machineInfo.FileType}");
            if (!System.IO.File.Exists(path))
            {
                return new ObjectResult($"{path} file not found");
            }
            var helper = new CommonExcelHelper();
            var ret = helper.ReadLastLine(path);
            return new ObjectResult(ret);
        }

    }
}
