using System;
using System.Collections.Generic;
using System.Text;

namespace Dream.Shop.Models
{
    public class CouponOutput
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
        /// 满足金额
        /// </summary>
        public decimal Meet { get; set; }
        /// <summary>
        ///优惠金额
        /// </summary>
        public decimal DiscountedPrice { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTiem { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }
        /// <summary>
        /// 优惠券状态(0失效，1生效，2满足金额不足)
        /// </summary>
        public bool State { get; set; }
        /// <summary>
        /// 判断是否满足减免金额
        /// </summary>
        public bool JudgeMeet { get; set; }
    }
}
