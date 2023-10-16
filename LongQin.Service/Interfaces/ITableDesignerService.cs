using LongQin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LongQin.Service
{
    public interface ITableDesignerService
    {
        TableDesigner GetByIdAsync(int id);

        PageModel<TableDesigner> GetListAsync(int pageIndex, int pageSize, int organizationId);

        bool DeleteAsync(int id);

        int InsertAsync(TableDesigner model);

        bool UpdateAsync(TableDesigner model);

        List<TableDesignerColumn> GetTableColumnsAsync(int id);

        bool InsertColumnsAsync(int id, List<TableDesignerColumn> list);

        PageModel<Dictionary<string, object>> GetTableData(int page, int limit, int id, string dataSource, int organizationId, Dictionary<string, string> searchDic);
    }
}
