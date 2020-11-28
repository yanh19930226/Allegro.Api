using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Allegro.Api.Application.Services;
using Allegro.SDK;
using Allegro.SDK.Models.AuthTokens;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Allegro.Api.Controllers
{
    /// <summary>
    /// 权限认证管理
    /// </summary>
    [ApiController]
    [AllowAnonymous]
    [Route("Api/Auth")]
    public class AuthController : Controller
    {
        private readonly IAllegroAuthService _authService;
        public AuthController(IAllegroAuthService authService)
        {
            _authService = authService;
        }
        /// <summary>
        /// 获取AccessToken
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("AccessToken")]
        public async Task<AllegroResult<AppAuthTokenResponse>> GetTokenAppAsync()
        {
            return await _authService.GetTokenAppAsync();
        }
    }
}
