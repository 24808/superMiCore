using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dream.Shop.DataEntity
{
    /// <summary>
    /// 商品活动关联表
    /// </summary>
    public class Good_Promotion : BaseMod
    {
        /// <summary>
        /// 商品编号
        /// </summary>
        public string GoodId { get; set; }
        /// <summary>
        /// 活动编号
        /// </summary>
        public string PromotionId{ get; set; }
        /// <summary>
        /// 优惠价格
        /// </summary>
        public decimal Discounts { get; set; }
    }
}
