using Allegro.SDK;
using Allegro.SDK.Models.AuthTokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace Allegro.Api.Application.Services.Impl
{
    public class AllegroAuthService : IAllegroAuthService
    {
        private AllegroClient _client;
        public AllegroAuthService(AllegroClient client)
        {
            _client = client;
        }

        public async Task<AllegroResult<AppAuthTokenResponse>> GetTokenAppAsync()
        {
            var request = new AppAuthTokenRequest();
            request.Request = RequestEnum.Auth;

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

            var res= await _client.GetAsync<AppAuthTokenResponse>(request);
            //var token = new JwtSecurityTokenHandler().WriteToken(res.Result.access_token);
            return res;

        }
    }
}
