using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LongQin.Models
{
    public class FlowWorkForm
    {
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 流程实例ID
        /// </summary>
        public int WorkId { get; set; }

        /// <summary>
        /// 节点待办ID
        /// </summary>
        public int ProcessId { get; set; }

        /// <summary>
        /// 节点ID
        /// </summary>
        public int NodeId { get; set; }

        /// <summary>
        /// 表单数据库表名称
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// 表单数据ID
        /// </summary>
        public int FormDataId { get; set; }
    }
}
