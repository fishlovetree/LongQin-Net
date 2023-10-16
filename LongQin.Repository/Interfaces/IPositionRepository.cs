using LongQin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LongQin.Repository
{
    public interface IPositionRepository
    {
        Position GetByIdAsync(int id);

        Position GetById(int id);

        List<Position> GetListAsync(int organizationId);

        PageModel<Position> GetPageListAsync(int pageIndex, int pageSize);

        bool DeleteAsync(int id);

        bool InsertAsync(Position model);

        bool UpdateAsync(Position model);

        List<Position> GetChildrenListAsync(int parentId);
    }
}
