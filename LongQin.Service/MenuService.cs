using LongQin.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LongQin.Models;
using LongQin.Common;

namespace LongQin.Service
{
    public class MenuService : ServiceBase, IMenuService
    {
        IMenuRepository _menuRepository = AutofacRepository.Resolve<IMenuRepository>();

        public MenuService()
        {
            base.AddDisposableObject(_menuRepository);
        }

        public Menu GetByIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("id错误");
            }

            return _menuRepository.GetByIdAsync(id);
        }

        public Menu GetById(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("id错误");
            }

            return _menuRepository.GetById(id);
        }

        public List<Menu> GetListAsync()
        {
            return _menuRepository.GetListAsync();
        }

        public object GetTreeAsync(int id)
        {
            List<Menu> list = _menuRepository.GetListAsync();
            Dictionary<int, Menu> dic = new Dictionary<int, Menu>();
            foreach (Menu item in list)
            {
                dic.Add(item.MenuId, item);
            }
            List<Menu> result = new List<Menu>();
            foreach (int key in dic.Keys)
            {
                Menu child = dic[key];
                int parentId = child.ParentId;
                if (dic.ContainsKey(parentId))
                {
                    Menu parent = dic[parentId];
                    if (parent.Children != null)
                    {
                        parent.Children.Add(child);
                    }
                    else
                    {
                        parent.Children = new List<Menu>();
                        parent.Children.Add(child);
                    }
                }
                else
                {
                    result.Add(child);
                }
            }
            return BuildTree(result, id);
        }

        private object BuildTree(List<Menu> list, int id)
        {
            List<Dictionary<string, object>> result = new List<Dictionary<string, object>>();
            foreach (Menu menu in list)
            {
                if (menu.MenuId != id)
                {    //判断如果是修改的话，修改的节点及下级节点不显示，也就不加载
                    Dictionary<string, object> node = new Dictionary<string, object>();
                    node.Add("id", menu.MenuId);    //节点id
                    node.Add("name", menu.MenuName);    //节点数据名称
                    node.Add("title", menu.MenuName);    //节点数据名称
                    node.Add("spread", true);        //是否展开
                    node.Add("open", true);        //是否展开
                    node.Add("checked", false);    //是否选中，前台也可以设置是否选中
                    if (menu.Children != null && menu.Children.Count != 0)
                    {    //如果下级节点不为空，
                        node.Add("children", BuildTree(menu.Children, id));    //递归加载下级节点
                    }
                    result.Add(node);
                }
            }
            return result;
        }

        public PageModel<Menu> GetPageListAsync(int pageIndex, int pageSize)
        {
            return _menuRepository.GetPageListAsync(pageIndex, pageSize);
        }

        public bool DeleteAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("id错误");
            }

            return _menuRepository.DeleteAsync(id);
        }

        public bool InsertAsync(Menu model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("model不能为null");
            }

            if (string.IsNullOrEmpty(model.MenuName))
            {
                throw new ArgumentNullException("MenuName错误");
            }

            return _menuRepository.InsertAsync(model);
        }

        public bool UpdateAsync(Menu model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("model不能为null");
            }

            if (string.IsNullOrEmpty(model.MenuName))
            {
                throw new ArgumentNullException("MenuName错误");
            }

            return _menuRepository.UpdateAsync(model);
        }

        public bool UpdateByUrlAsync(Menu model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("model不能为null");
            }

            if (string.IsNullOrEmpty(model.MenuName))
            {
                throw new ArgumentNullException("MenuName错误");
            }

            return _menuRepository.UpdateByUrlAsync(model);
        }

        public List<Menu> GetChildrenListAsync(int parentId, int userId, int organizationId)
        {
            List<Menu> result = _menuRepository.GetChildrenListAsync(parentId, userId, organizationId);
            foreach (Menu item in result)
            {
                List<Menu> children = _menuRepository.GetChildrenListAsync(item.MenuId, userId, organizationId);
                item.Children = children;
            }
            return result;
        }

        public object GetUserMenuTreeAsync(int userId, int organizationId)
        {
            List<Menu> list = _menuRepository.GetUserMenuListAsync(userId, organizationId);
            Dictionary<int, Menu> dic = new Dictionary<int, Menu>();
            foreach (Menu item in list)
            {
                dic.Add(item.MenuId, item);
            }
            List<Menu> result = new List<Menu>();
            foreach (int key in dic.Keys)
            {
                Menu child = dic[key];
                int parentId = child.ParentId;
                if (dic.ContainsKey(parentId))
                {
                    Menu parent = dic[parentId];
                    if (parent.Children != null)
                    {
                        parent.Children.Add(child);
                    }
                    else
                    {
                        parent.Children = new List<Menu>();
                        parent.Children.Add(child);
                    }
                }
                else
                {
                    result.Add(child);
                }
            }
            return BuildTree(result, 0);
        }
    }
}
