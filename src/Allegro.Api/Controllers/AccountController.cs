using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Allegro.Api.Controllers
{
    /// <summary>
    /// 登入管理
    /// </summary>
    [ApiController]
    [AllowAnonymous]
    [Route("Api/Account")]
    public class AccountController : Controller
    {
        /// <summary>
        /// 注册
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("Register")]
        public IActionResult Register()
        {
            return Ok();
        }
        /// <summary>
        /// 登入
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("Login")]
        public IActionResult Login()
        {
            return Ok();
        }
    }
}