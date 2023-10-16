using LongQin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LongQin.Repository
{
    public interface IDepartmentRepository
    {
        Department GetByIdAsync(int id);

        Department GetById(int id);

        List<Department> GetListAsync(int organizationId);

        PageModel<Department> GetPageListAsync(int pageIndex, int pageSize);

        bool DeleteAsync(int id);

        bool InsertAsync(Department model);

        bool UpdateAsync(Department model);

        List<Department> GetChildrenListAsync(int parentId);
    }
}
