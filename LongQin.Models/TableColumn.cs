using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LongQin.Models
{
    public class TableColumn
    {
        /// <summary>
        /// 字段名称
        /// </summary>
        public string ColumnName { get; set; }

        /// <summary>
        /// 字段类型
        /// </summary>
        public string ColumnType { get; set; }

        /// <summary>
        /// 是否为空
        /// </summary>
        public string IsNull { get; set; }

        /// <summary>
        /// 中文描述
        /// </summary>
        public string Description { get; set; }
    }
}
