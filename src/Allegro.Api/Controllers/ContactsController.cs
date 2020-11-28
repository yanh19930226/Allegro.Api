using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Allegro.Api.Controllers
{
    /// <summary>
    /// 联系人
    /// </summary>
    [Route("Api/Contacts")]
    [ApiController]
    public class ContactsController : Controller
    {
        /// <summary>
        /// 联系人测试
        /// </summary>
        /// <returns></returns>
        [Route("Test")]
        [HttpGet]
        public IActionResult Test()
        {
            return Ok();
        }

        /// <summary>
        /// 获取联系人详细
        /// </summary>
        /// <returns></returns>
        [Route("GetContactDetails")]
        [HttpGet]
        public IActionResult GetContactDetails()
        {
            return Ok();
        }
    }
}
