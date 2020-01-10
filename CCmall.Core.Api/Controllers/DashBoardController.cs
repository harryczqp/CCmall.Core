using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CCmall.Common.Helper;

namespace CCmall.Core.Api.Controllers
{
    public class DashBoardController : BaseController
    {
        public DashBoardController()
        {
        }

        [HttpGet]
        public IActionResult Getlist()
        {
            var rng = new Random();
            var ret = new { data1 = rng.Next(1000, 3000), data2 = rng.Next(3000, 7000), data3 = rng.Next(3000, 5000) };
            return new ObjectResult(ret);
        }
    }
}
