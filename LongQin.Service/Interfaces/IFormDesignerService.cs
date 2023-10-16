using LongQin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LongQin.Service
{
    public interface IFormDesignerService
    {
        bool CreateAsync(FormDesigner model);

        PageModel<FormDesigner> GetListAsync(int organizationId, int pageIndex, int pageSize);

        FormDesigner GetByIdAsync(int id);

        bool DeleteAsync(int id);

        bool UpdateAsync(FormDesigner model);

        bool InertFormDataAsync(string tableName, List<string> columns, List<string> values);

        List<TableColumn> GetTableColumnsAsync(string tableName);
    }
}
