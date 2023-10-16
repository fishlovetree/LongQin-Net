using LongQin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LongQin.Service
{
    public interface IPositionService
    {
        Position GetByIdAsync(int id);

        Position GetById(int id);

        List<Position> GetListAsync(int organizationId);

        object GetTreeAsync(int id, int organizationId);

        PageModel<Position> GetPageListAsync(int pageIndex, int pageSize);

        bool DeleteAsync(int id);

        bool InsertAsync(Position model);

        bool UpdateAsync(Position model);

        List<Position> GetChildrenListAsync(int parentId);
    }
}
