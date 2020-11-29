using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Redis;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;

namespace Allegro.Api.Controllers
{
    [Route("api/Redis")]
    [ApiController]
    public class TestController : Controller
    {
        private readonly IDatabase _redis;
        public TestController(RedisClient client)
        {
            _redis = client.GetDatabase();
        }

        [HttpGet]
        public string Get()
        {
            // 往Redis里面存入数据
            _redis.StringSet("Name", "Tom");
            // 从Redis里面取数据
            string name = _redis.StringGet("Name");
            return name;
        }
    }
}