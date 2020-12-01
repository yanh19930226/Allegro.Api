using System;
using System.Collections.Generic;
using System.Text;

namespace Allegro.SDK.Models.AuthTokens
{
    public class AppAuthTokenRequest : BaseRequest<AuthTokenResponse,string>
    {

        public AppAuthTokenRequest(string data = null,string token=null) :base(data,token)
        {
        }
        public override string Url => "/token?grant_type=client_credentials";
    }
}
