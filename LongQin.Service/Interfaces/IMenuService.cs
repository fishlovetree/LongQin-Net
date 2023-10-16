using LongQin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LongQin.Service
{
    public interface IMenuService
    {
        Menu GetByIdAsync(int id);

        Menu GetById(int id);

        List<Menu> GetListAsync();

        object GetTreeAsync(int id);

        PageModel<Menu> GetPageListAsync(int pageIndex, int pageSize);

        bool DeleteAsync(int id);

        bool InsertAsync(Menu model);

        bool UpdateAsync(Menu model);

        bool UpdateByUrlAsync(Menu model);

        List<Menu> GetChildrenListAsync(int parentId, int userId, int organizationId);

        object GetUserMenuTreeAsync(int userId, int organizationId);
    }
}
