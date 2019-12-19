using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace CCmall.CoreControllers
{

    public class LoginController : BaseController
    {
        public LoginController()
        {
        }
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Logout()
        {
            return new ObjectResult("Bye!");
        }
    }
}
