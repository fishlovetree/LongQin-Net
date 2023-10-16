using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LongQin.Models
{
    public class FormDesigner
    {
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 表单名称
        /// </summary>
        public string FormName { get; set; }

        /// <summary>
        /// 数据库表名称
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// 表单json数据
        /// </summary>
        public string JsonData { get; set; }

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
        /// 数据库表列
        /// </summary>
        public List<TableColumn> TableColumns { get; set; }

        /// <summary>
        /// 是否审批，1-是，0-否
        /// </summary>
        public int IsApproval { get; set; }
    }
}
