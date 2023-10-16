using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LongQin.Models;
using Dapper;

namespace LongQin.Repository
{
    public class TableDesignerRepository : ITableDesignerRepository
    {
        public TableDesigner GetByIdAsync(int id)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = "select * from [diy_table] where Id = @id and Status=1;";
                var list = conn.Query<TableDesigner>(sql, new { id = id });
                return list != null ? list.FirstOrDefault() : null;
            }
        }

        public PageModel<TableDesigner> GetListAsync(int pageIndex, int pageSize, int organizationId)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string whereSql = "";
                whereSql += organizationId != 0 ? "" : " and n.organizationId = " + organizationId;
                string countSql = "select count(1) from [diy_table] n where n.Status=1" + whereSql;
                int total = conn.ExecuteScalar<int>(countSql);
                if (total == 0)
                {
                    return new PageModel<TableDesigner>();
                }

                string sql = string.Format(@"select * from (select n.*, u.userName as creatorName, f.formName, ROW_NUMBER() over (Order by n.Id desc) as RowNumber from [diy_table] n
                        left join [des_form] f on f.tableName = n.dataSource
                        left join [sys_user] u on u.userId = n.creator
                        where n.Status=1 {0}) as b where RowNumber between {1} and {2};", whereSql, ((pageIndex - 1) * pageSize) + 1, pageIndex * pageSize);
                var list = conn.Query<TableDesigner>(sql);

                return new PageModel<TableDesigner>
                {
                    Total = total,
                    Data = list != null ? list.ToList() : null
                };
            }
        }

        public int InsertAsync(TableDesigner model)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                var fields = model.ToFields(removeFields: new List<string> { "Id", "CreateTime", "Status", "CreatorName", "FormName", "Columns" });
                model.Status = 1;

                string sql = string.Format("insert into [diy_table] ({0}) values ({1});", string.Join(",", fields), string.Join(",", fields.Select(n => "@" + n)));
                sql += ";select @@identity";
                return conn.ExecuteScalar<int>(sql, model);
            }
        }

        public bool UpdateAsync(TableDesigner model)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                var fields = model.ToFields(removeFields: new List<string>
                {
                    "Id",
                    "CreateTime",
                    "Status",
                    "CreatorName",
                    "FormName",
                    "Columns"
                });

                if (fields == null || fields.Count == 0)
                {
                    return false;
                }

                var fieldList = new List<string>();
                foreach (var field in fields)
                {
                    fieldList.Add(string.Format("{0}=@{0}", field));
                }

                string sql = string.Format("update [diy_table] set {0} where Id=@Id;", string.Join(",", fieldList));
                return conn.Execute(sql, model) > 0;
            }
        }

        public bool DeleteAsync(int id)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = "update [diy_table] set Status=0 where Id=@Id;";
                return conn.Execute(sql, new { Id = id }) > 0;
            }
        }

        public List<TableDesignerColumn> GetTableColumnsAsync(int id)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = "select * from [diy_table_columns] where TableId=@id";
                var list = conn.Query<TableDesignerColumn>(sql, new { id = id });
                return list != null ? list.ToList() : null;
            }
        }

        public bool DeleteColumnsAsync(int id)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = "delete from [diy_table_columns] where TableId=@Id;";
                return conn.Execute(sql, new { Id = id }) >= 0;
            }
        }

        public bool InsertColumnsAsync(TableDesignerColumn model)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                var fields = model.ToFields();

                string sql = string.Format("insert into [diy_table_columns] ({0}) values ({1});", string.Join(",", fields), string.Join(",", fields.Select(n => "@" + n)));
                return conn.Execute(sql, model) > 0;
            }
        }

        public PageModel<Dictionary<string, object>> GetTableData(string countSql, string dataSql)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                int total = conn.ExecuteScalar<int>(countSql);
                if (total == 0)
                {
                    return new PageModel<Dictionary<string, object>>();
                }
                var list = conn.Query<dynamic>(dataSql);

                List<Dictionary<string, object>> data = new List<Dictionary<string, object>>();
                if (list != null)
                {
                    foreach (var row in list)
                    {
                        IDictionary<string, object> dic = row as IDictionary<string, object>;
                        Dictionary<string, object> obj = new Dictionary<string, object>();
                        foreach (var item in dic) 
                        {
                            obj.Add(item.Key, item.Value);
                        }
                        data.Add(obj);
                    }
                }
                else 
                {
                    data = null;
                }

                return new PageModel<Dictionary<string, object>>
                {
                    Total = total,
                    Data = data
                };
            }
        }
    }
}
