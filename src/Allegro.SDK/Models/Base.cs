using System;
using System.Collections.Generic;
using System.Text;

namespace Allegro.SDK.Models
{
    public abstract class BaseRequest<T>
    {

        public BaseRequest()
        {

        }
        public RequestEnum Request { get; set; } = RequestEnum.Api;
        public abstract string Url { get; }

        public string Token { get; set; }

    }
}
