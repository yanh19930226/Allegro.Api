using Allegro.SDK;
using Allegro.SDK.Models.AuthTokens;
using Core.Redis;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace Allegro.Api.Application.Services.Impl
{
    public class AllegroAuthService : IAllegroAuthService
    {

        private readonly IDatabase _redis;

        private AllegroClient _client;

        public AllegroAuthService(AllegroClient client, RedisClient redisclient)
        {
            _client = client;

            _redis = redisclient.GetDatabase();
        }
        /// <summary>
        /// 获取AppAccessToken
        /// </summary>
        /// <returns></returns>
        public async Task<AllegroResult<AuthTokenResponse>> GetTokenAppAsync()
        {
            var request = new AppAuthTokenRequest(_redis.StringGet("AllegroAppToken"));

            request.Request = RequestEnum.App;

            #region 写入Allegro认证信息
            //var claims = new[] {
            //        new Claim(ClaimTypes.Name, user.Login),
            //        new Claim(ClaimTypes.Email, user.Login),
            //        new Claim(JwtRegisteredClaimNames.Exp, $"{new DateTimeOffset(DateTime.Now.AddMinutes(_settings.JWT.Expires)).ToUnixTimeSeconds()}"),
            //        new Claim(JwtRegisteredClaimNames.Nbf, $"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}")
            //    };
            //var key = new SymmetricSecurityKey(_settings.JWT.SecurityKey.SerializeUtf8());
            //var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            //var securityToken = new JwtSecurityToken(
            //    issuer: _settings.JWT.Domain,
            //    audience: _settings.JWT.Domain,
            //    claims: claims,
            //    expires: DateTime.Now.AddMinutes(_settings.JWT.Expires),
            //    signingCredentials: creds);
            //var token = new JwtSecurityTokenHandler().WriteToken(securityToken); 
            //
            #endregion

            var res= await _client.GetAsync<AuthTokenResponse>(request);

            _redis.StringSet("AllegroAppToken", res.Result.access_token);

            return res;

        }
        /// <summary>
        /// 获取UserAccessToken
        /// </summary>
        /// <returns></returns>
        public async Task<AllegroResult<AuthTokenResponse>> GetTokenUserAsync(string code)
        {
            var request = new UserAuthTokenRequest(code,null);

            request.Request = RequestEnum.User;

            var res = await _client.GetAsync<AuthTokenResponse>(request);

            _redis.StringSet("AllegroUserToken", res.Result.access_token);

            return res;

        }
    }
}
