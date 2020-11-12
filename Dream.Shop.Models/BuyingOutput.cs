using System;
using System.Collections.Generic;
using System.Text;

namespace Dream.Shop.Models
{
    public class BuyingOutput
    {
        public List<HomeGoodOutput> ChlidGood { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public string StartTiem { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public string EndTime { get; set; }
    }
}
