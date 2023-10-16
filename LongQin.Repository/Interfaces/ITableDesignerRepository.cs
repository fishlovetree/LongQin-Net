using LongQin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LongQin.Repository
{
    public interface ITableDesignerRepository
    {
        TableDesigner GetByIdAsync(int id);

        PageModel<TableDesigner> GetListAsync(int pageIndex, int pageSize, int organizationId);

        bool DeleteAsync(int id);

        int InsertAsync(TableDesigner model);

        bool UpdateAsync(TableDesigner model);

        List<TableDesignerColumn> GetTableColumnsAsync(int id);

        bool DeleteColumnsAsync(int id);

        bool InsertColumnsAsync(TableDesignerColumn model);

        PageModel<Dictionary<string, object>> GetTableData(string countSql, string dataSql);
    }
}
