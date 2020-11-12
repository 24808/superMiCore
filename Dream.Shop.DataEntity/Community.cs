using System;
using System.Collections.Generic;
using System.Text;

namespace Dream.Shop.DataEntity
{
    /// <summary>
    /// 社区表
    /// </summary>
    public class Community
    {
        public string CommunityId { get; set; }
        /// <summary>
        /// 类别编号
        /// </summary>
        public string CategoryId { get; set; }
        /// <summary>
        /// 用户编号
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }
    }
}
