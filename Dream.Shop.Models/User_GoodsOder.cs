﻿using Dream.Shop.DataEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dream.Shop.Models
{
    /// <summary>
    /// 订单信息
    /// </summary>
    public class User_GoodsOder
    {
        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderId { get; set; }
        /// <summary>
        /// 用户编号
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 用户名字
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 商品编号
        /// </summary>
        //public string GoodId { get; set; }
        ///// <summary>
        ///// 商品名称
        ///// </summary>
        //public string GoodName { get; set; }
        ///// <summary>
        ///// 数量
        ///// </summary>
        //public int Quantity { get; set; }

        public string Address { get; set; }
        /// <summary>
        /// 订单总价
        /// </summary>
        public decimal? Price { get; set; }
        ///// <summary>
        ///// 规格类型(颜色、尺码、包装等)
        ///// </summary>
        //public string SpecificationName { get; set; }

        //public string ImgUrl { get; set; }
        /// <summary>
        /// 规格值
        /// </summary>
        public string SpecificationNum { get; set; }
        /// <summary>
        /// 订单类型(1待付款，2待发货，3待收货，4待评价，5已评价)
        /// </summary>
        public int OrderType { get; set; }
        /// <summary>
        /// 订单里的所有商品
        /// </summary>
        public List<GoodOrderedDTO> ChlidClass { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        // <summary>
        /// 逻辑判断
        /// </summary>
        public bool IsDelect { get; set; }
    }
}
