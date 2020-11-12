using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dream.Shop.DataEntity
{
    /// <summary>
    /// 订单表
    /// </summary>
    public class Order : BaseMod
    {
        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderId { get; set; }
        /// <summary>
        /// 用户编号
        /// </summary>
        public string UserID { get; set; }
        /// <summary>
        /// 收货人名字
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 收货人手机号
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 订单类型(1待付款，2待发货，3待收货，4待评价，5已评价)
        /// </summary>
        public int OrderType { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 优惠券编号
        /// </summary>
        public string CouponId { get; set; }
        /// <summary>
        /// 订单总价
        /// </summary>
        public decimal? TotalPrice { get; set; }



    }
}
