using System;
using System.Collections.Generic;
using System.Text;

namespace Allegro.SDK.Models.AuthTokens
{
    public class RefreshTokenRequest : BaseRequest<AuthTokenResponse>
    {
        public RefreshTokenRequest(string token) : base(token)
        {
            RefreshToken = token;
        }
        public string RefreshToken { get; set; }

        public override string Url => "/token?grant_type=refresh_token&refresh_token="+ RefreshToken;
    }
}
