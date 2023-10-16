using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LongQin.Models
{
    public class FlowProcess
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ProcessId { get; set; }

        /// <summary>
        /// 流程实例ID
        /// </summary>
        public int WorkId { get; set; }

        /// <summary>
        /// 节点ID
        /// </summary>
        public int NodeId { get; set; }

        /// <summary>
        /// 连线ID
        /// </summary>
        public int LinkId { get; set; }

        /// <summary>
        /// 处理人ID
        /// </summary>
        public int SendingTo { get; set; }

        /// <summary>
        /// 类型：1-主送，2-抄送
        /// </summary>
        public int ProcessType { get; set; }

        /// <summary>
        /// 提交人
        /// </summary>
        public int Submitter { get; set; }

        /// <summary>
        /// 组织机构
        /// </summary>
        public int OrganizationId { get; set; }

        /// <summary>
        /// 提交时间
        /// </summary>
        public DateTime SubmitTime { get; set; }

        /// <summary>
        /// 1-未读，2-已读
        /// </summary>
        public int Flag { get; set; }

        /// <summary>
        /// 状态：1-运行中，0-关闭，2-就绪
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 流程ID
        /// </summary>
        public int FlowId { get; set; }

        /// <summary>
        /// 表单数据ID
        /// </summary>
        public int FormDataId { get; set; }
    }
}
