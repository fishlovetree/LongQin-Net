using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LongQin.Models
{
    public class Log
    {
        /// <summary>
        /// id
        /// </summary>
        public int LogId { get; set; }

        /// <summary>
        /// 方法名称
        /// </summary>
        public string ActionName { get; set; }

        /// <summary>
        /// 控制器名称
        /// </summary>
        public string ControllerName { get; set; }

        /// <summary>
        /// 方法参数
        /// </summary>
        public string ActionParameters { get; set; }

        /// <summary>
        /// 操作内容
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 操作备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 操作人id
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 操作人名称
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 数据权限
        /// </summary>
        public int OrganizationId { get; set; }
    }
}
