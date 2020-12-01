using System;
using System.Collections.Generic;
using System.Text;

namespace Allegro.SDK.Models.Products
{
    public class GetProductByIdRequest : BaseRequest<GetProductByIdResponse>
    {
        public GetProductByIdRequest(string productId,string token):base(token)
        {
            productId = productId;
        }
        public string productId { get; set; }
        public override string Url => "/sale/products/"+this.productId;
    }

    public class GetProductByIdResponse
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
