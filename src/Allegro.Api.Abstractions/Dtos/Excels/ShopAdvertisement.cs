using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Allegro.Api.Abstractions.Dtos.Excels
{
    public class ShopAdvertisement
    {
        [Description("日期")]
        public DateTime Date { get; set; }
        [Description("店铺名称")]
        public String ShopName { get; set; } = "";
        [Description("广告费（USD）")]
        public decimal Fee { get; set; }
    }
}
