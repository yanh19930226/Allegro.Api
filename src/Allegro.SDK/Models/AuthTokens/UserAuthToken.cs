using System;
using System.Collections.Generic;
using System.Text;

namespace Allegro.SDK.Models.AuthTokens
{
    public class UserAuthTokenRequest : BaseRequest<AuthTokenResponse>
    {
        public UserAuthTokenRequest(string code,string token) : base(token)
        {
            Code = code;
        }
        public string Code { get; set; }

        public override string Url => "/token?grant_type=authorization_code&code="+this.Code;
    }
}
