using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dream.Shop.Utility
{
    /// <summary>
    /// 通用数据结果类
    /// </summary>
    public class DataResult<T>
    {
        /// <summary>
        /// 接口状态
        /// </summary>
        public int code { get; set; }

        /// <summary>
        /// 提示文本
        /// </summary>
        public string msg { get; set; }

        /// <summary>
        /// 数据长度
        /// </summary>
        public int count { get; set; }

        /// <summary>
        /// 当前页索引
        /// </summary>
        public int pageNum { get; set; }

        /// <summary>
        /// 每页记录条数
        /// </summary>
        public int pageSize { get; set; }

        /// <summary>
        /// 数据结果
        /// </summary>
        public T list { get; set; }
    }

}
