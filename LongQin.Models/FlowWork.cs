using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LongQin.Models
{
    public class FlowWork
    {
        /// <summary>
        /// ID
        /// </summary>
        public int WorkId { get; set; }

        /// <summary>
        /// 流程ID
        /// </summary>
        public int FlowId { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public int Creator { get; set; }

        /// <summary>
        /// 组织机构
        /// </summary>
        public int OrganizationId { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime CloseTime { get; set; }

        /// <summary>
        /// 状态：1-运行中，0-关闭，9-作废
        /// </summary>
        public int Status { get; set; }
    }
}
