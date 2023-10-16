using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LongQin.Models;
using Dapper;

namespace LongQin.Repository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        public Department GetByIdAsync(int id)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = "select * from [sys_department] where DepartmentId = @id and Status=1;";
                var list = conn.Query<Department>(sql, new { id = id });
                return list != null ? list.FirstOrDefault() : null;
            }
        }

        public Department GetById(int id)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = "select * from [sys_department] where DepartmentId = @id and Status=1;";
                var list = conn.Query<Department>(sql, new { id = id });
                return list != null ? list.FirstOrDefault() : null;
            }
        }

        public List<Department> GetListAsync(int organizationId)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                if (organizationId == 0)
                {
                    string sql = "select * from [sys_department] where Status=1 Order by ParentId, DepartmentId;";
                    var list = conn.Query<Department>(sql);

                    return list != null ? list.ToList() : null;
                }
                else
                {
                    string sql = "select * from [sys_department] where Status=1 and OrganizationId=@organizationId Order by ParentId, DepartmentId;";
                    var list = conn.Query<Department>(sql, new { organizationId = organizationId });

                    return list != null ? list.ToList() : null;
                }
            }
        }

        public PageModel<Department> GetPageListAsync(int pageIndex, int pageSize)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string countSql = "select count(1) from [sys_department] where Status=1;";
                int total = conn.ExecuteScalar<int>(countSql);
                if (total == 0)
                {
                    return new PageModel<Department>();
                }

                string sql = string.Format(@"select * from (select *, ROW_NUMBER() over (Order by ParentId, DepartmentId) as RowNumber from [sys_department] where Status=1) as b where RowNumber between {0} and {1};", ((pageIndex - 1) * pageSize) + 1, pageIndex * pageSize);
                var list = conn.Query<Department>(sql);

                return new PageModel<Department>
                {
                    Total = total,
                    Data = list != null ? list.ToList() : null
                };
            }
        }

        public bool InsertAsync(Department model)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                var fields = model.ToFields(removeFields: new List<string> { "DepartmentId", "CreateTime", "Status", "Children" });
                if (fields == null || fields.Count == 0)
                {
                    return false;
                }

                model.Status = 1;

                string sql = string.Format("insert into [sys_department] ({0}) values ({1});", string.Join(",", fields), string.Join(",", fields.Select(n => "@" + n)));
                return conn.Execute(sql, model) > 0;
            }
        }

        public bool UpdateAsync(Department model)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                var fields = model.ToFields(removeFields: new List<string>
                {
                    "DepartmentId",
                    "CreateTime",
                    "Status",
                    "Children"
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

                string sql = string.Format("update [sys_department] set {0} where DepartmentId=@DepartmentId;", string.Join(",", fieldList));
                return conn.Execute(sql, model) > 0;
            }
        }

        public bool DeleteAsync(int id)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = "update [sys_department] set Status=0 where DepartmentId=@Id;";
                return conn.Execute(sql, new { Id = id }) > 0;
            }
        }

        public List<Department> GetChildrenListAsync(int parentId)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = string.Format(@"select * from [sys_department] where ParentId={0} and Status=1 Order by DepartmentId;", parentId);
                var list = conn.Query<Department>(sql);

                return list != null ? list.ToList() : null;
            }
        }
    }
}
