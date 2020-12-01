using System;
using System.Collections.Generic;
using System.Text;

namespace Allegro.SDK.Models.Pricings
{
    public class GetUserQuotesRequest : BaseRequest<GetUserQuotesResponse, GetUserQuotesRequestParameter>
    {
        public GetUserQuotesRequest(GetUserQuotesRequestParameter data,string token) : base(data,token)
        {
         
        }
       
        ///pricing/offer-quotes? offer.id=<string>&offer.id=< string >

        public override string Url => "/pricing/offer-quotes?"+ ConnecStr(this.Data.OfferIds);

        private string ConnecStr(List<string> str)
        {
            return "";
        }
    }

    public class GetUserQuotesRequestParameter
    {
        public List<string> OfferIds { get; set; }
    }

    public class GetUserQuotesResponse
    {
        public int count { get; set; }
        public List<Quotes> quotes { get; set; }

    }
    public class Quotes
    {
        public bool enabled { get; set; }
        public Fee fee { get; set; }
        public Offer offer { get; set; }
        public string name { get; set; }
        public string nextDate { get; set; }
        public string type { get; set; }
    }

    public class Fee
    {
        public string amount { get; set; }
        public string currency { get; set; }
    }
    public class Offer
    {
        public string id { get; set; }
    }
}
