using Allegro.Api.Abstractions.Dtos.Commons;
using System;
using System.Collections.Generic;
using System.Text;

namespace Allegro.Api.Abstractions.Dtos.Requests.Products
{
    /// <summary>
    /// 发布产品
    /// </summary>
    public class ProposeProductRequestDto
    {
        /// <summary>
        ///名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 类别
        /// </summary>
        public CategoryDto category { get; set; }
        /// <summary>
        /// 图片
        /// </summary>
        public List<ImagesDto> images { get; set; }
        /// <summary>
        /// 参数
        /// </summary>
        public List<ParameterDto> parameters { get; set; }
    }
}
