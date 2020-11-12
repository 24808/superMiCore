using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dream.Shop.DataEntity
{
    public class BaseMod
    {
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// 逻辑判断
        /// </summary>
        public bool IsDelect { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
