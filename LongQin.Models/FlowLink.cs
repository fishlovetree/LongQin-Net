using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LongQin.Models
{
    public class FlowLink
    {
        /// <summary>
        /// ID
        /// </summary>
        public int LinkId { get; set; }

        /// <summary>
        /// 连线名称
        /// </summary>
        public string LinkName { get; set; }

        /// <summary>
        /// 起始节点
        /// </summary>
        public int FromNodeId { get; set; }

        /// <summary>
        /// 终到节点
        /// </summary>
        public int ToNodeId { get; set; }

        /// <summary>
        /// 条件表单
        /// </summary>
        public int FormId { get; set; }

        /// <summary>
        /// 条件字段
        /// </summary>
        public string Field { get; set; }

        /// <summary>
        /// 条件符号
        /// </summary>
        public string Operator { get; set; }

        /// <summary>
        /// 条件值
        /// </summary>
        public string OperatorValue { get; set; }

        /// <summary>
        /// 节点定位-x
        /// </summary>
        public int PositionX { get; set; }

        /// <summary>
        /// 节点定位-y
        /// </summary>
        public int PositionY { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 所属流程id
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
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
