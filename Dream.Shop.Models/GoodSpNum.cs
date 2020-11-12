using Dream.Shop.DataEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dream.Shop.Models
{
    public class GoodSpNum
    {
        /// <summary>
        /// 商品编号
        /// </summary>
        public string GoodId { get; set; }
        /// <summary>
        /// 规格编号
        /// </summary>
        public string GSId { get; set; }
        /// <summary>
        /// 规格值
        /// </summary>
        public string Num { get; set; }
        /// <summary>
        /// 父编号
        /// </summary>
        public string ParentId { get; set; }
        /// <summary>
        /// 价格
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// 子类
        /// </summary>
        public List<SpecificationOutput> Chlid { get; set; }
    }
}
