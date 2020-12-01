using System;
using System.Collections.Generic;
using System.Text;

namespace Allegro.SDK.Models.AuthTokens
{
    public class RefreshTokenRequest : BaseRequest<AuthTokenResponse, string>
    {
        public RefreshTokenRequest(string refreshToken, string token=null) : base(refreshToken, token)
        {
            //this.Data.RefreshToken = base.Token;
        }
        public override string Url => "/token?grant_type=refresh_token&refresh_token="+ this.Data;
    }
}
