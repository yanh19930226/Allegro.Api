using System;
using System.Collections.Generic;
using System.Text;

namespace Allegro.SDK.Models.AuthTokens
{
    public class UserAuthTokenRequest : BaseRequest<AuthTokenResponse, UserAuthTokenRequestParameter>
    {
        public UserAuthTokenRequest(UserAuthTokenRequestParameter data,string token=null) : base(data,token)
        {
        }

        public override string Url => "/token?grant_type=authorization_code&code="+this.Data.Code;
    }
    public class UserAuthTokenRequestParameter
    {
        public string Code { get; set; }
    }
}
