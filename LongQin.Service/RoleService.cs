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
    public class RoleService : ServiceBase, IRoleService
    {
        IRoleRepository _roleRepository = AutofacRepository.Resolve<IRoleRepository>();

        public RoleService()
        {
            base.AddDisposableObject(_roleRepository);
        }

        public Role GetByIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("id错误");
            }

            return _roleRepository.GetByIdAsync(id);
        }

        public List<Role> GetListAsync(int organizationId)
        {
            return _roleRepository.GetListAsync(organizationId);
        }

        public PageModel<Role> GetPageListAsync(int pageIndex, int pageSize)
        {
            return _roleRepository.GetPageListAsync(pageIndex, pageSize);
        }

        public bool DeleteAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("id错误");
            }

            return _roleRepository.DeleteAsync(id);
        }

        public bool InsertAsync(Role model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("model不能为null");
            }

            if (string.IsNullOrEmpty(model.RoleName))
            {
                throw new ArgumentNullException("RoleName错误");
            }

            return _roleRepository.InsertAsync(model);
        }

        public bool UpdateAsync(Role model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("model不能为null");
            }

            if (string.IsNullOrEmpty(model.RoleName))
            {
                throw new ArgumentNullException("RoleName错误");
            }

            return _roleRepository.UpdateAsync(model);
        }

        public List<int> GetMenus(int roleId)
        {
            return _roleRepository.GetRoleMenuAsync(roleId);
        }

        public bool SetMenuAsync(int roleId, string menuIds)
        {
            if (roleId <= 0)
            {
                throw new ArgumentException("id错误");
            }

            return _roleRepository.SetMenuAsync(roleId, menuIds);
        }
    }
}
