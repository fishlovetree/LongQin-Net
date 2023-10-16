using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LongQin.Models
{
    public class FlowStep
    {
        /// <summary>
        /// ID
        /// </summary>
        public int StepId { get; set; }

        /// <summary>
        /// 流程实例ID
        /// </summary>
        public int WorkId { get; set; }

        /// <summary>
        /// 节点ID
        /// </summary>
        public int NodeId { get; set; }

        /// <summary>
        /// 代办ID
        /// </summary>
        public int ProcessId { get; set; }

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
        /// 动作：1-前进，2-跳转，3-关闭，4-指派
        /// </summary>
        public int Action { get; set; }

        /// <summary>
        /// 处理意见
        /// </summary>
        public string Reason { get; set; }

        /// <summary>
        /// 节点名称
        /// </summary>
        public string NodeName { get; set; }

        /// <summary>
        /// 处理人姓名
        /// </summary>
        public string SubmitterName { get; set; }

        /// <summary>
        /// 工作开始时间
        /// </summary>
        public DateTime BeginTime { get; set; }

        /// <summary>
        /// 停留时间
        /// </summary>
        public string StayTime { get; set; }
    }
}
