using LongQin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LongQin.Repository
{
    public interface IFormDesignerRepository
    {
        bool CreateTableAsync(string tableName, List<TableColumn> list);

        bool CreateFormAsync(FormDesigner model);

        PageModel<FormDesigner> GetListAsync(int organizationId, int pageIndex, int pageSize);

        bool InsertFormDataAsync(string tableName, List<string> columns, List<string> values);

        FormDesigner GetByIdAsync(int id);

        bool UpdateAsync(FormDesigner model);

        bool DeleteAsync(int id);

        bool DeleteTableAsync(string tableName);

        int GetTableCountAsync(string tableName);

        List<TableColumn> GetTableColumnsAsync(string tableName);
    }
}
