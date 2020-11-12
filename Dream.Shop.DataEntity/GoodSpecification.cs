using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dream.Shop.DataEntity
{
    /// <summary>
    /// 规格表
    /// </summary>
    public class GoodSpecification : BaseMod
    {
        /// <summary>
        /// 规格编号
        /// </summary>
        public string SpecificationId { get; set; }
        /// <summary>
        /// 规格类型(颜色、尺码、包装等)
        /// </summary>
        public string SpecificationName { get; set; }
        


    }
}
