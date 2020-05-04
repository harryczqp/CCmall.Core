using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CCmall.Repository.Interface;
using System.Threading.Tasks;
using System.Linq;
using CCmall.Model.Request;
using CCmall.Common.Helper;
using CCmall.Core.Api.Message;
using CCmall.Common.Redis;
using CCmall.Core.Common.Hubs;

namespace CCmall.Core.Api.Controllers
{

    public class LoginController : BaseController
    {
        private readonly IBaseUserRepository _baseUser;
        private readonly IRedisManager _redisManager;

        public LoginController(IBaseUserRepository baseUser,IRedisManager redisManager)
        {
            _baseUser = baseUser;
            _redisManager = redisManager;
        }
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Logout()
        {
            return new ObjectResult("Bye!");
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody]RequestUser model)
        {
            var pwd = MD5Helper.MD5Encrypt32(model.password);
            var query = await _baseUser.Query(f => f.username.Equals(model.username) && f.password.Equals(pwd) && f.status == 1);
            if (query.Count == 0)
            {
                throw new BadExceptionResult("User is valid!");
            }
            var bytes= SerializeHelper.SerializeToBytes(query);
            //TODO根据项目获取数据库
            _redisManager.SetDefaultDatabase(12);
            _redisManager.SetHash(RedisConstant.UserData,query.First().id.ToStr(), bytes);
            var token = "bearer ";
            token += JwtHelper.IssueJwt(new JwtTokenModel { Role = "admin,test", Uid = 1 });
            return new ObjectResult(new { token });
        }
        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetToken(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                throw new BadExceptionResult("error");
            }
            token = token.Replace("bearer ", "");
            var data = JwtHelper.ParsingJwt(token);
            var roles = data.Role.Split(',', StringSplitOptions.RemoveEmptyEntries);
            return new ObjectResult(new { roles, name = data.Role, avatar = "123", introduction = "" });
        }
    }
}
