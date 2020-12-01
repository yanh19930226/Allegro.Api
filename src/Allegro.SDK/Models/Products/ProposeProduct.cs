using System;
using System.Collections.Generic;
using System.Text;

namespace Allegro.SDK.Models.Products
{
    public class ProposeProductRequest : BaseRequest<ProposeProductResponse, ProposeProductRequestParameter>
    {
        public ProposeProductRequest(ProposeProductRequestParameter data, string token):base(data,token)
        {

        }
        public override string Url => "/sale/product-proposals";
    }

    public class ProposeProductRequestParameter
    {
        public string name { get; set; }
        public Category category { get; set; }
        public List<Images> images { get; set; }
        public List<Parameter> parameters { get; set; }
    }

    public class ProposeProductResponse
    {
        public string id { get; set; }
        public string name { get; set; }
        public Category category { get; set; }
        public List<Images> images { get; set; }
        public List<Parameter> parameters { get; set; }
        public tecdocSpecification tecdocSpecification { get; set; }
        public description description { get; set; }
    }
}
