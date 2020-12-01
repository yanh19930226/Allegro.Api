using System;
using System.Collections.Generic;
using System.Text;

namespace Allegro.SDK.Models.Products
{
    public class ProposeProductRequest : BaseRequest<ProposeProductResponse>
    {
        public ProposeProductRequest(string name,Category category,List<Images> images,List<Parameter> parameters,string token):base(token)
        {
            name = name;
            category = category;
            images = images;
            parameters = parameters;
        }
        /// <summary>
        ///名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 类别
        /// </summary>
        public Category category { get; set; }
        /// <summary>
        /// 图片
        /// </summary>
        public List<Images> images { get; set; }
        /// <summary>
        /// 参数
        /// </summary>
        public List<Parameter> parameters { get; set; }
        public override string Url => "/sale/product-proposals";
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
