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
    public class PositionService : ServiceBase, IPositionService
    {
        IPositionRepository _positionRepository = AutofacRepository.Resolve<IPositionRepository>();

        public PositionService()
        {
            base.AddDisposableObject(_positionRepository);
        }

        public Position GetByIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("id错误");
            }

            return _positionRepository.GetByIdAsync(id);
        }

        public Position GetById(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("id错误");
            }

            return _positionRepository.GetById(id);
        }

        public List<Position> GetListAsync(int organizationId)
        {
            return _positionRepository.GetListAsync(organizationId);
        }

        public object GetTreeAsync(int id, int organizationId)
        {
            List<Position> list = _positionRepository.GetListAsync(organizationId);
            Dictionary<int, Position> dic = new Dictionary<int, Position>();
            foreach (Position item in list)
            {
                dic.Add(item.PositionId, item);
            }
            List<Position> result = new List<Position>();
            foreach (int key in dic.Keys)
            {
                Position child = dic[key];
                int parentId = child.ParentId;
                if (dic.ContainsKey(parentId))
                {
                    Position parent = dic[parentId];
                    if (parent.Children != null)
                    {
                        parent.Children.Add(child);
                    }
                    else
                    {
                        parent.Children = new List<Position>();
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

        private object BuildTree(List<Position> list, int id)
        {
            List<Dictionary<string, object>> result = new List<Dictionary<string, object>>();
            foreach (Position department in list)
            {
                if (department.PositionId != id)
                {    //判断如果是修改的话，修改的节点及下级节点不显示，也就不加载
                    Dictionary<string, object> node = new Dictionary<string, object>();
                    node.Add("id", department.PositionId);    //节点id
                    node.Add("name", department.PositionName);    //节点数据名称
                    node.Add("title", department.PositionName);    //节点数据名称
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

        public PageModel<Position> GetPageListAsync(int pageIndex, int pageSize)
        {
            return _positionRepository.GetPageListAsync(pageIndex, pageSize);
        }

        public bool DeleteAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("id错误");
            }

            return _positionRepository.DeleteAsync(id);
        }

        public bool InsertAsync(Position model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("model不能为null");
            }

            if (string.IsNullOrEmpty(model.PositionName))
            {
                throw new ArgumentNullException("PositionName错误");
            }

            return _positionRepository.InsertAsync(model);
        }

        public bool UpdateAsync(Position model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("model不能为null");
            }

            if (string.IsNullOrEmpty(model.PositionName))
            {
                throw new ArgumentNullException("PositionName错误");
            }

            return _positionRepository.UpdateAsync(model);
        }

        public List<Position> GetChildrenListAsync(int parentId)
        {
            List<Position> result = _positionRepository.GetChildrenListAsync(parentId);
            foreach (Position item in result)
            {
                List<Position> children = _positionRepository.GetChildrenListAsync(item.PositionId);
                item.Children = children;
            }
            return result;
        }
    }
}
