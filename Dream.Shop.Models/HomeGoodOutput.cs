using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dream.Shop.Models
{
    public class HomeGoodOutput
    {
        /// <summary>
        /// 头部类别编号
        /// </summary>
        public string HardId { get; set; }
        /// <summary>
        ///  头部类别名称
        /// </summary>
        public string HardName { get; set; }
        /// <summary>
        /// 商品编号
        /// </summary>
        public string GoodId { get; set; }
        /// <summary>
        /// 商品类别名称
        /// </summary>
        public string GoodName { get; set; }
        /// <summary>
        /// 图片地址
        /// </summary>
        public string ImgUrl { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 最低价格
        /// </summary>
        public decimal FloorPrice { get; set; }
        /// <summary>
        /// 优惠金额
        /// </summary>
        public decimal Discounts { get; set; }

    }
}
