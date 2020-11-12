using System;
using System.Collections.Generic;
using System.Text;

namespace Dream.Shop.Models
{
    public class SettlementOutput
    {
        /// <summary>
        /// 获取商品列表
        /// </summary>
        public List<GoodListOutput> GetGoodLists { get; set; }
        /// <summary>
        /// 获取商品购物券
        /// </summary>
        public List<CouponOutput> GetGoodCoupons { get; set; }
    }
}
