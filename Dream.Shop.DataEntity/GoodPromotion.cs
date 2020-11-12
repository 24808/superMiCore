﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dream.Shop.DataEntity
{
    /// <summary>
    /// 促销活动表
    /// </summary>
    public class GoodPromotion : BaseMod
    {
        /// <summary>
        /// 活动编号
        /// </summary>
        public string PromotionId { get; set; }
        /// <summary>
        /// 活动名称
        /// </summary>
        public string PromotionName { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTiem { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }
        /// <summary>
        /// 活动状态(当当前时间超过结束时间时，要把Staate修改成false)
        /// </summary>
        public bool State { get; set; }
    }

}
