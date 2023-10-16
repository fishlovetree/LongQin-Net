using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LongQin.Models
{
    public class Menu
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public int MenuId { get; set; }

        /// <summary>
        /// 菜单名称
        /// </summary>
        public string MenuName { get; set; }

        /// <summary>
        /// 菜单链接
        /// </summary>
        public string MenuUrl { get; set; }

        /// <summary>
        /// 上级菜单
        /// </summary>
        public int ParentId { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int GroupSeq { get; set; }

        /// <summary>
        /// 菜单图标
        /// </summary>
        public string MenuIcon { get; set; }

        /// <summary>
        /// 控制器名称
        /// </summary>
        public string Controller { get; set; }

        /// <summary>
        /// 数据权限
        /// </summary>
        public string Action { get; set; } 

        /// <summary>
        /// 状态，1：正常，0：删除
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 子集
        /// </summary>
        public List<Menu> Children { get; set; }

        /// <summary>
        /// 创建者
        /// </summary>
        public int Creator { get; set; }

        /// <summary>
        /// 组织机构ID
        /// </summary>
        public int OrganizationId { get; set; }
    }
}
