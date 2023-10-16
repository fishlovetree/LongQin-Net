using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LongQin.Models
{
    public class FlowNode
    {
        /// <summary>
        /// ID
        /// </summary>
        public int NodeId { get; set; }

        /// <summary>
        /// 节点名称
        /// </summary>
        public string NodeName { get; set; }

        /// <summary>
        /// 节点类别
        /// </summary>
        public int NodeType { get; set; }

        /// <summary>
        /// 表单id
        /// </summary>
        public int FormId { get; set; }

        /// <summary>
        /// 表单名称
        /// </summary>
        public string FormName { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        public int Groupseq { get; set; }

        /// <summary>
        /// 是否虚拟
        /// </summary>
        public int Virtual { get; set; }

        /// <summary>
        /// 是否多人协作
        /// </summary>
        public int Cooperation { get; set; }

        /// <summary>
        /// 处理部门ID
        /// </summary>
        public int DepartmentId { get; set; }

        /// <summary>
        /// 处理职位ID
        /// </summary>
        public int PositionId { get; set; }

        /// <summary>
        /// 处理人ID
        /// </summary>
        public int UserId { get; set; }

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

        /// <summary>
        /// 前端页面id
        /// </summary>
        public string Gid { get; set; }

        /// <summary>
        /// 是否审批，1-是，0-否
        /// </summary>
        public int IsApproval { get; set; }
    }
}
