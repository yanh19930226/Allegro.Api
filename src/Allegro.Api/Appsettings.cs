using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Allegro.Api
{
    public class Appsettings
    {
        public AllegroConfig Allegro { get; set; }


        public class AllegroConfig
        {
            public bool IsDev { get; set; }
            public string ClientId { get; set; }
            public string ClientSecret { get; set; }
            public string RedirectUri { get; set; }
        }
    }
}
