using System;
using System.Collections.Generic;
using System.Text;

namespace Allegro.SDK.Models.Categories
{
    public class GetCategoryRequest : BaseRequest<CategoryResponse, GetCategoryRequestParameter>
    {
        public GetCategoryRequest(GetCategoryRequestParameter data,string token) :base(data,token)
        {
           
        }
        
        public override string Url => "sale/categories/"+this.Data.categoryId;
    }

    public class GetCategoryRequestParameter
    {
        public string categoryId { get; set; }
    }

    public class CategoryResponse
    {
        public string id { get; set; }
        public string name { get; set; }
        public string parent { get; set; }
        public bool leaf { get; set; }
        public Option options { get; set; }
    }

    public class Option
    {
        public bool variantsByColorPatternAllowed { get; set; }
        public bool advertisement { get; set; }
        public bool advertisementPriceOptional { get; set; }
        public bool offersWithProductPublicationEnabled { get; set; }
        public bool productCreationEnabled { get; set; }
        public bool productEANRequired { get; set; }
        public bool customParametersEnabled { get; set; }
    }

}
