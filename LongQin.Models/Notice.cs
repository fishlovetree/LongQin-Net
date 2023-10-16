using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LongQin.Models
{
    public class Notice
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public int NoticeId { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 紧急程度：1-普通，2-紧急，3-加急
        /// </summary>
        public int NoticeLevel { get; set; }

        /// <summary>
        /// 保密程度：1-公开，2-内部公开，3-机密
        /// </summary>
        public int Security { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public int Creator { get; set; }

        /// <summary>
        /// 创建人名称
        /// </summary>
        public string CreatorName { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        public string DepartmentName { get; set; }

        /// <summary>
        /// 公司ID
        /// </summary>
        public int OrganizationId { get; set; }

        /// <summary>
        /// 状态，1：正常，0：删除
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 附件
        /// </summary>
        public string Attachments { get; set; }
    }
}
