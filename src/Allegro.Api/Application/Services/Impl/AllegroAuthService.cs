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
            var request = new AppAuthTokenRequest();

            request.Request = RequestEnum.App;

            var res= await _client.GetAsync(request);

            _redis.StringSet("AllegroAppToken", res.Result.access_token);

            return res;

        }

        /// <summary>
        /// 获取UserAccessToken
        /// </summary>
        /// <returns></returns>
        public async Task<AllegroResult<AuthTokenResponse>> GetTokenUserAsync(string code)
        {
            var request = new UserAuthTokenRequest(new UserAuthTokenRequestParameter { Code=code});

            request.Request = RequestEnum.User;

            var res = await _client.GetAsync(request);

            //根据用户Id 写入UserToken
            _redis.StringSet("AllegroUserToken", res.Result.access_token);

            return res;

        }

        /// <summary>
        /// 刷新用户令牌
        /// </summary>
        /// <returns></returns>
        public async Task<AllegroResult<AuthTokenResponse>> GetRefreshTokenUserAsync()
        {
           var refreshToken= _redis.StringGet("AllegroRefreshToken");

            var request = new RefreshTokenRequest(refreshToken);

            request.Request = RequestEnum.Refresh;

            var res = await _client.GetAsync(request);

            //根据用户Id 写入RefreshToken
            _redis.StringSet("AllegroRefreshToken", res.Result.refresh_token);

            return res;
        }
    }
}
