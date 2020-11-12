using System;
using System.Collections.Generic;
using System.Text;
using Dream.Shop.DataEntity;

namespace Dream.Shop.Models
{
    public class SpecificationOutput
    {
        /// <summary>
        /// 规格编号
        /// </summary>
        public string SpecificationId { get; set; }
        /// <summary>
        /// 规格名
        /// </summary>
        public string SpecificationName { get; set; }
        /// <summary>
        /// 规格值
        /// </summary>
        public List<GoodSpNum> SpecificationNum { get; set; }


    }
}
