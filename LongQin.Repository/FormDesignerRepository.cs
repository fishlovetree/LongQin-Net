using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LongQin.Models;
using Dapper;

namespace LongQin.Repository
{
    public class FormDesignerRepository : IFormDesignerRepository
    {
        public bool CreateTableAsync(string tableName, List<TableColumn> list)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string columns = "";
                string descriptions = "";
                for (int i = 0; i < list.Count; i++)
                {
                    TableColumn column = list[i];
                    columns += "[" + column.ColumnName + "] " + column.ColumnType + " " + column.IsNull + ",";
                    descriptions += "EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'" + column.Description + "' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'" + tableName + "', @level2type=N'COLUMN',@level2name=N'" + column.ColumnName + "'";
                }
                columns += "[creator] " + "[int]" + " " + "NULL" + ",";
                columns += "[createTime] " + "[datetime]" + " " + "NULL" + ",";
                columns += "[organizationId] " + "[int]" + " " + "NULL" + ",";
                columns += "[status] " + "[tinyint]" + " " + "NULL" + ",";
                string sql = string.Format("CREATE TABLE [{0}]([id][int] IDENTITY(1, 1) NOT NULL,{1}CONSTRAINT[PK_{0}] PRIMARY KEY CLUSTERED([id] ASC)WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY]) ON[PRIMARY]", tableName, columns);
                sql += descriptions;
                return conn.Execute(sql) > 0;
            }
        }

        public bool CreateFormAsync(FormDesigner model)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                var fields = model.ToFields(removeFields: new List<string> { "Id", "CreateTime", "TableColumns" });
                if (fields == null || fields.Count == 0)
                {
                    return false;
                }

                string sql = string.Format("insert into [des_form] ({0}) values ({1});", string.Join(",", fields), string.Join(",", fields.Select(n => "@" + n)));
                return conn.Execute(sql, model) > 0;
            }
        }

        public PageModel<FormDesigner> GetListAsync(int organizationId, int pageIndex, int pageSize)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string countSql = string.Format(@"select count(1) from [des_form] where OrganizationId = {0} and Status=1;", organizationId);
                int total = conn.ExecuteScalar<int>(countSql);
                if (total == 0)
                {
                    return new PageModel<FormDesigner>();
                }

                string sql = string.Format(@"select * from (select *, ROW_NUMBER() over (Order by Id desc) as RowNumber from [des_form] where OrganizationId = {0} and Status=1) as b where RowNumber between {1};", organizationId, ((pageIndex - 1) * pageSize) + 1 + " and " + pageIndex * pageSize);
                var list = conn.Query<FormDesigner>(sql);

                return new PageModel<FormDesigner>
                {
                    Total = total,
                    Data = list != null ? list.ToList() : null
                };
            }
        }

        public FormDesigner GetByIdAsync(int id)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = "select * from [des_form] where id = @id and Status=1;";
                var list = conn.Query<FormDesigner>(sql, new { id = id });
                return list != null ? list.FirstOrDefault() : null;
            }
        }

        public bool UpdateAsync(FormDesigner model)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                var fields = model.ToFields(removeFields: new List<string>
                {
                    "id",
                    "CreateTime",
                    "Status",
                    "Creator",
                    "OrganizationId",
                    "TableColumns"
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

                string sql = string.Format("update [des_form] set {0} where id=@Id;", string.Join(",", fieldList));
                return conn.Execute(sql, model) > 0;
            }
        }

        public bool DeleteAsync(int id)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = "update [des_form] set Status=0 where id=@Id;";
                return conn.Execute(sql, new { Id = id }) > 0;
            }
        }

        public bool DeleteTableAsync(string tableName)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = string.Format("drop table {0};", tableName);
                return conn.Execute(sql) > 0;
            }
        }

        public bool InsertFormDataAsync(string tableName, List<string> columns, List<string> values)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = string.Format("insert into [" + tableName + "] ({0}) values ({1});", string.Join(",", columns), string.Join(",", values));
                return conn.Execute(sql) > 0;
            }
        }

        public int GetTableCountAsync(string tableName)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string countSql = string.Format("select count(1) from sysobjects where id = object_id('{0}');", tableName);
                return conn.ExecuteScalar<int>(countSql);
            }
        }

        // 获取表列集合
        public List<TableColumn> GetTableColumnsAsync(string tableName)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = string.Format("select a.name as ColumnName, b.name as ColumnType, g.value as Description from syscolumns a left join systypes b on a.xtype = b.xusertype " +
                    "left join sys.extended_properties g on a.id = G.major_id and a.colid = g.minor_id where id = object_id('{0}');", tableName);
                var list = conn.Query<TableColumn>(sql);
                return list != null ? list.ToList() : null;
            }
        }
    }
}
