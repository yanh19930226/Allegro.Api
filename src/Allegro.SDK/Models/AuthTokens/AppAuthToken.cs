using System;
using System.Collections.Generic;
using System.Text;

namespace Allegro.SDK.Models.AuthTokens
{
    public class AppAuthTokenRequest : BaseRequest<AuthTokenResponse>
    {

        public AppAuthTokenRequest(string token):base(token)
        {
        }
        public override string Url => "/token?grant_type=client_credentials";
    }
}
