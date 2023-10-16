using LongQin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LongQin.Service
{
    public interface IRoleService
    {
        Role GetByIdAsync(int id);

        List<Role> GetListAsync(int organizationId);

        PageModel<Role> GetPageListAsync(int pageIndex, int pageSize);

        bool DeleteAsync(int id);

        bool InsertAsync(Role model);

        bool UpdateAsync(Role model);

        List<int> GetMenus(int roleId);

        bool SetMenuAsync(int roleId, string menuIds);
    }
}
