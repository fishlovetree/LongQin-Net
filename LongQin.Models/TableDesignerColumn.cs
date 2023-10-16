using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LongQin.Models
{
    public class TableDesignerColumn
    {
        /// <summary>
        /// 列表主键ID
        /// </summary>
        public int TableId { get; set; }

        /// <summary>
        /// 列表字段
        /// </summary>
        public string TableColumn { get; set; }

        /// <summary>
        /// 表头名称
        /// </summary>
        public string ColumnName { get; set; }

        /// <summary>
        /// 字段顺序
        /// </summary>
        public int ColumnIndex { get; set; }

        /// <summary>
        /// 列宽度
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// 是否排序：0-不排序，1-升序，2-降序
        /// </summary>
        public int Orderby { get; set; }

        /// <summary>
        /// 是否搜索：0-否，1-等于，2-模糊查询，3-介于
        /// </summary>
        public int SearchType { get; set; }

        /// <summary>
        /// 公式：0-否，1-加，2-减，3-乘，4-除，5-拼接
        /// </summary>
        public int Formula { get; set; }

        /// <summary>
        /// 公式值
        /// </summary>
        public string FormulaValue { get; set; }

        /// <summary>
        /// 字段类型
        /// </summary>
        public string ColumnType { get; set; }
    }
}
