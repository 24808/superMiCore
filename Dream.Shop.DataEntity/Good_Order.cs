using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dream.Shop.DataEntity
{
    /// <summary>
    /// 商品订单关联表
    /// </summary>
    public class Good_Order :BaseMod
    {
        /// <summary>
        /// 商品编号
        /// </summary>
        public string GoodId { get; set; }
        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderId { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int Quantity { get; set; }
        /// <summary>
        /// 规格
        /// </summary>
        public string SpecificationName { get; set; }

        /// <summary>
        /// 单价
        /// </summary>
        public decimal Price { get; set; }
        
    }
}
