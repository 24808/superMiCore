using System;
using System.Collections.Generic;
using System.Text;

namespace Dream.Shop.Models
{

    public class CollectOutput
    {
        /// <summary>
        /// 用户编号
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 商品编号
        /// </summary>
        public string GoodId { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>
        public string GoodName { get; set; }
        /// <summary>
        /// 商品数量
        /// </summary>
        public int Amount { get; set; }
        /// <summary>
        /// 商品价格
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// 缩略图
        /// </summary>
        public string ThumbnailImg { get; set; }
        /// <summary>
        /// 商品选择规格
        /// </summary>
        public List<SpecificationOutput> SpecificationList { get; set; }
    }
}
