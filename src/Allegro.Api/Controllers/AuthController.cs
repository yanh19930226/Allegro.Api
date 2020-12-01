using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Allegro.Api.Application.Services;
using Allegro.SDK;
using Allegro.SDK.Models.AuthTokens;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

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
        private readonly Appsettings _settings;

        private readonly IAllegroAuthService _authService;

        public AuthController(IAllegroAuthService authService, IOptions<Appsettings> options)
        {
            _authService = authService;

            _settings = options.Value;
        }
        /// <summary>
        /// 获取应用AccessToken
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("AppAccessToken")]
        public async Task<AllegroResult<AuthTokenResponse>> GetTokenAppAsync()
        {

            return await _authService.GetTokenAppAsync();

        }
        /// <summary>
        /// 获取登入Allegro链接
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAllegroLoginUrl")]
        public AllegroResult<string> GetAllegroLoginUrl()
        {
            var res =new  AllegroResult<string>();

            var uri = "https://allegro.pl.allegrosandbox.pl/auth/oauth/authorize?response_type=code" + "&client_id=" + _settings.Allegro.ClientId + "&redirect_uri=" + _settings.Allegro.RedirectUri;

            res.Success(uri, AllegroResultCode.Succeed.ToString());

            return res;
        }
        /// <summary>
        /// 获取用户Token
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("UserAccessToken")]
        public async Task<AllegroResult<AuthTokenResponse>> GetTokenUserAsync(string code)
        {

            return await _authService.GetTokenUserAsync(code);

        }
    }
}
