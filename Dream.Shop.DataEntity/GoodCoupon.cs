using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dream.Shop.DataEntity
{
    /// <summary>
    /// 优惠券
    /// </summary>
    public class GoodCoupon : BaseMod
    {
        /// <summary>
        /// 优惠券编号
        /// </summary>
        public string CouponId { get; set; }
        /// <summary>
        /// 优惠券名称
        /// </summary>
        public string CouponName { get; set; }
        /// <summary>
        /// 用户编号
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTiem { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }
        /// <summary>
        /// 优惠券状态(当当前时间超过结束时间时，要把Staate修改成false)
        /// </summary>
        public bool State { get; set; }
        /// <summary>
        /// 条件说明
        /// </summary>
        public string ConditionDesc { get; set; }
        /// <summary>
        /// 满足金额
        /// </summary>
        public decimal Meet { get; set; }
        /// <summary>
        /// 优惠金额
        /// </summary>
        public decimal DiscountedPrice { get; set; }

    }
}
