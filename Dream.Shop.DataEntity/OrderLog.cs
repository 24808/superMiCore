using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dream.Shop.DataEntity
{
    /// <summary>
    /// 订单日志
    /// </summary>
    public class OrderLog : BaseMod
    {
        /// <summary>
        /// 订单日志内容(谁在什么时候修改了订单的什么状态)
        /// </summary>
        public string Content { get; set; }
    }
}
