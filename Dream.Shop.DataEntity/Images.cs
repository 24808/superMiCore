using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dream.Shop.DataEntity
{
    /// <summary>
    /// 图片表
    /// </summary>
    public class Images : BaseMod
    {
        /// <summary>
        /// 编号(通过编号定义图片地址)
        /// </summary>
        public string Number { get; set; }
        /// <summary>
        /// 图片地址
        /// </summary>
        public string ImgUrl { get; set; }
       /// <summary>
        /// 排序
        /// </summary>
        public int Rank { get; set; }

    }
}
