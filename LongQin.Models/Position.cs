using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LongQin.Models
{
    public class Position
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public int PositionId { get; set; }

        /// <summary>
        /// 职位名称
        /// </summary>
        public string PositionName { get; set; }

        /// <summary>
        /// 职级
        /// </summary>
        public int PositionLevel { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 上级职位
        /// </summary>
        public int ParentId { get; set; }

        /// <summary>
        /// 数据权限
        /// </summary>
        public int OrganizationId { get; set; }

        /// <summary>
        /// 状态，1：正常，0：删除
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 子集
        /// </summary>
        public List<Position> Children { get; set; }
    }
}
