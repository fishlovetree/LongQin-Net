using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LongQin.Models
{
    public class TableDesigner
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 列表名称
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// 数据源表名
        /// </summary>
        public string DataSource { get; set; }

        /// <summary>
        /// 数据源表单名称
        /// </summary>
        public string FormName { get; set; }

        /// <summary>
        /// 数据权限
        /// </summary>
        public int OrganizationId { get; set; }

        /// <summary>
        /// 创建人id
        /// </summary>
        public int Creator { get; set; }

        /// <summary>
        /// 创建人名称
        /// </summary>
        public string CreatorName { get; set; }

        /// <summary>
        /// 状态，1：正常，0：删除
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 表头集合
        /// </summary>
        public List<TableDesignerColumn> Columns { get; set; }
    }
}
