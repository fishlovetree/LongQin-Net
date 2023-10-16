using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LongQin.Models
{
    public class Role
    {
        /// <summary>
        /// 角色ID
        /// </summary>
        public int RoleId { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName { get; set; }

        /// <summary>
        /// 角色描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 角色状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 所拥有的菜单id集合,逗号分隔
        /// </summary>
        public string MenuIds { get; set; }

        /// <summary>
        /// 数据权限
        /// </summary>
        public int OrganizationId { get; set; }
    }
}
