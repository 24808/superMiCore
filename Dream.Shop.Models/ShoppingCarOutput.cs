using System;
using System.Collections.Generic;
using System.Text;

namespace Dream.Shop.Models
{
    public class ShoppingCarOutput
    {
        /// <summary>
        /// 商品编号
        /// </summary>
        public string GoodId { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>
        public string GoodName { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 商品单价
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// 商品数量
        /// </summary>
        public int Amount { get; set; }
        /// <summary>
        /// 商品库存
        /// </summary>
        public int Stock { get; set; }
        /// <summary>
        /// 图片地址(缩略图)
        /// </summary>
        public string ImgUrl { get; set; }
        public Boolean ischeck{ get; set; }
        /// <summary>
        /// 规格值
        /// </summary>
        public string SpecificationName { get; set; }
    }
}
