using LongQin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LongQin.Service
{
    public interface ILogService
    {
        Log GetByIdAsync(int id);

        Log GetById(int id);

        PageModel<Log> GetListAsync(string beginDate, string endDate, int pageIndex, int pageSize, int organizationId);

        bool InsertAsync(Log model);
    }
}
