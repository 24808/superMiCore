using System;
using System.Collections.Generic;
using System.Text;

namespace Dream.Shop.Models
{
    public class HomeTopOutput
    {
        /// <summary>
        /// 名字
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 商品列表
        /// </summary>
        public List<HomeGoodOutput> GetGoodList { get; set; }

    }
}
