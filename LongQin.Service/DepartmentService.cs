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
    public class DepartmentService : ServiceBase, IDepartmentService
    {
        IDepartmentRepository _departmentRepository = AutofacRepository.Resolve<IDepartmentRepository>();

        public DepartmentService()
        {
            base.AddDisposableObject(_departmentRepository);
        }

        public Department GetByIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("id错误");
            }

            return _departmentRepository.GetByIdAsync(id);
        }

        public Department GetById(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("id错误");
            }

            return _departmentRepository.GetById(id);
        }

        public List<Department> GetListAsync(int organizationId)
        {
            return _departmentRepository.GetListAsync(organizationId);
        }

        public object GetTreeAsync(int id, int organizationId)
        {
            List<Department> list = _departmentRepository.GetListAsync(organizationId);
            Dictionary<int, Department> dic = new Dictionary<int, Department>();
            foreach (Department item in list)
            {
                dic.Add(item.DepartmentId, item);
            }
            List<Department> result = new List<Department>();
            foreach (int key in dic.Keys)
            {
                Department child = dic[key];
                int parentId = child.ParentId;
                if (dic.ContainsKey(parentId))
                {
                    Department parent = dic[parentId];
                    if (parent.Children != null)
                    {
                        parent.Children.Add(child);
                    }
                    else
                    {
                        parent.Children = new List<Department>();
                        parent.Children.Add(child);
                    }
                }
                else
                {
                    result.Add(child);
                }
            }
            List<Dictionary<string, object>> r = new List<Dictionary<string, object>>();
            Dictionary<string, object> first = new Dictionary<string, object>();
            first.Add("id", "");    //节点id
            first.Add("name", "请选择");    //节点数据名称
            first.Add("title", "请选择");    //节点数据名称
            first.Add("spread", true);        //是否展开
            first.Add("open", true);        //是否展开
            first.Add("checked", false);    //是否选中，前台也可以设置是否选中
            r.Add(first);
            r.AddRange(BuildTree(result, id));
            return r;
        }

        private List<Dictionary<string, object>> BuildTree(List<Department> list, int id)
        {
            List<Dictionary<string, object>> result = new List<Dictionary<string, object>>();
            foreach (Department department in list)
            {
                if (department.DepartmentId != id)
                {    //判断如果是修改的话，修改的节点及下级节点不显示，也就不加载
                    Dictionary<string, object> node = new Dictionary<string, object>();
                    node.Add("id", department.DepartmentId);    //节点id
                    node.Add("name", department.DepartmentName);    //节点数据名称
                    node.Add("title", department.DepartmentName);    //节点数据名称
                    node.Add("spread", true);        //是否展开
                    node.Add("open", true);        //是否展开
                    node.Add("checked", false);    //是否选中，前台也可以设置是否选中
                    if (department.Children != null && department.Children.Count != 0)
                    {    //如果下级节点不为空，
                        node.Add("children", BuildTree(department.Children, id));    //递归加载下级节点
                    }
                    result.Add(node);
                }
            }
            return result;
        }

        public PageModel<Department> GetPageListAsync(int pageIndex, int pageSize)
        {
            return _departmentRepository.GetPageListAsync(pageIndex, pageSize);
        }

        public bool DeleteAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("id错误");
            }

            return _departmentRepository.DeleteAsync(id);
        }

        public bool InsertAsync(Department model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("model不能为null");
            }

            if (string.IsNullOrEmpty(model.DepartmentName))
            {
                throw new ArgumentNullException("DepartmentName错误");
            }

            return _departmentRepository.InsertAsync(model);
        }

        public bool UpdateAsync(Department model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("model不能为null");
            }

            if (string.IsNullOrEmpty(model.DepartmentName))
            {
                throw new ArgumentNullException("DepartmentName错误");
            }

            return _departmentRepository.UpdateAsync(model);
        }

        public List<Department> GetChildrenListAsync(int parentId)
        {
            List<Department> result = _departmentRepository.GetChildrenListAsync(parentId);
            foreach (Department item in result)
            {
                List<Department> children = _departmentRepository.GetChildrenListAsync(item.DepartmentId);
                item.Children = children;
            }
            return result;
        }
    }
}
