using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LongQin.Models
{
    public class Organization
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public int OrganizationId { get; set; }

        /// <summary>
        /// 公司名称
        /// </summary>
        public string OrganizationName { get; set; }

        /// <summary>
        /// 组织机构代码
        /// </summary>
        public string OrganizationCode { get; set; }

        /// <summary>
        /// 上级
        /// </summary>
        public int ParentId { get; set; }

        /// <summary>
        /// 公司地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 联系方式
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// LOGO
        /// </summary>
        public string LogoPath { get; set; }

        /// <summary>
        /// 系统名称
        /// </summary>
        public String SystemName { get; set; }

        /// <summary>
        /// 简介
        /// </summary>
        public String Introduction { get; set; }

        /// <summary>
        /// 状态，1：正常，0：删除
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
