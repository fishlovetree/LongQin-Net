using LongQin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LongQin.Repository
{
    public interface IOrganizationRepository
    {
        Organization GetByIdAsync(int id);

        Organization GetById(int id);

        Organization GetByNameAsync(string name);

        PageModel<Organization> GetListAsync(int pageIndex, int pageSize);

        List<Organization> GetAllListAsync();

        bool DeleteAsync(int id);

        int InsertAsync(Organization model);

        bool UpdateAsync(Organization model);
    }
}
