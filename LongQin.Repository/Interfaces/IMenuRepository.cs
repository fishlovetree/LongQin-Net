using LongQin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LongQin.Repository
{
    public interface IMenuRepository
    {
        Menu GetByIdAsync(int id);

        Menu GetById(int id);

        List<Menu> GetListAsync();

        PageModel<Menu> GetPageListAsync(int pageIndex, int pageSize);

        bool DeleteAsync(int id);

        bool InsertAsync(Menu model);

        bool UpdateAsync(Menu model);

        bool UpdateByUrlAsync(Menu model);

        List<Menu> GetChildrenListAsync(int parentId, int userId, int organizationId);

        List<Menu> GetUserMenuListAsync(int userId, int organizationId);
    }
}
