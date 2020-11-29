using System;
using System.Collections.Generic;
using System.Text;

namespace Allegro.SDK.Models.AuthTokens
{
    public class AuthTokenResponse
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public string refresh_token { get; set; }
        public string expires_in { get; set; }
        public string scope { get; set; }
        public string allegro_api { get; set; }
        public string jti { get; set; }
    }
}
