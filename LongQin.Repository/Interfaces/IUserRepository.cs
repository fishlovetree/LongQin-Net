using LongQin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LongQin.Repository
{
    public interface IUserRepository
    {
        User GetByIdAsync(int id);

        User GetById(int id);

        User GetByNameAsync(string name);

        int GetCountByNameAsync(string name, int id);

        PageModel<User> GetListAsync(int pageIndex, int pageSize, string departmentId, string nickName, int organizationId);

        bool DeleteAsync(int id);

        bool UpdatePasswordAsync(int id, string password);

        int InsertAsync(User model);

        bool UpdateAsync(User model);

        List<int> GetUserRoleAsync(int userId);

        bool SetRoleAsync(int userId, string roleIds);

        int GetUserPositionLevelAsync(int userId);

        List<int> GetUserByDeptAndPositionAsync(int departmentId, int positionId);
    }
}
