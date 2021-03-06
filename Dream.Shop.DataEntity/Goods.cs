﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dream.Shop.DataEntity
{
    /// <summary>
    /// 商品表
    /// </summary>
    public class Goods : BaseMod
    {
        /// <summary>
        /// 商品编号
        /// </summary>
        public string GoodId { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>
        public string GoodName { get; set; }
        /// <summary>
        /// 类别编号
        /// </summary>
        public string CategoryID { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 库存
        /// </summary>
        public int Stock { get; set; }
        /// <summary>
        /// 警告库存(达到警告库存时警告用户或商家)
        /// </summary>
        public int WarningStock { get; set; }
        /// <summary>
        /// 商品状态(1上架，2下架，3预售)
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 浏览次数统计
        /// </summary>
        public int VisitCount { get; set; }
        /// <summary>
        /// 缩略图
        /// </summary>
        public string ThumbnailImg { get; set; }
        /// <summary>
        /// 概述编号
        /// </summary>
        public string OutLineId { get; set; }


    }
}
