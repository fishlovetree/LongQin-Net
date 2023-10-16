using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LongQin.Models;
using Dapper;

namespace LongQin.Repository
{
    public class OrganizationRepository : IOrganizationRepository
    {
        public Organization GetByIdAsync(int id)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = "select * from [sys_organization] where OrganizationId = @id and Status=1;";
                var list = conn.Query<Organization>(sql, new { id = id });
                return list != null ? list.FirstOrDefault() : null;
            }
        }

        public Organization GetById(int id)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = "select * from [sys_organization] where OrganizationId = @id and Status=1;";
                var list = conn.Query<Organization>(sql, new { id = id });
                return list != null ? list.FirstOrDefault() : null;
            }
        }

        public Organization GetByNameAsync(string name)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = "select * from [sys_organization] where OrganizationName = @name and Status=1;";
                return conn.QueryFirstOrDefault<Organization>(sql, new { name = name });
            }
        }

        public PageModel<Organization> GetListAsync(int pageIndex, int pageSize)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string countSql = "select count(1) from [sys_organization] where Status=1;";
                int total = conn.ExecuteScalar<int>(countSql);
                if (total == 0)
                {
                    return new PageModel<Organization>();
                }

                string sql = string.Format(@"select * from (select *, ROW_NUMBER() over (Order by OrganizationId desc) as RowNumber from [sys_organization] where Status=1) as b where RowNumber between {0} and {1};", ((pageIndex - 1) * pageSize) + 1, pageIndex * pageSize);
                var list = conn.Query<Organization>(sql);

                return new PageModel<Organization>
                {
                    Total = total,
                    Data = list != null ? list.ToList() : null
                };
            }
        }

        public List<Organization> GetAllListAsync()
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = string.Format(@"select *, ROW_NUMBER() over (Order by OrganizationId desc) as RowNumber from [sys_organization] where Status=1;");
                var list = conn.Query<Organization>(sql);

                return list != null ? list.ToList() : null;
            }
        }

        public int InsertAsync(Organization model)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                var fields = model.ToFields(removeFields: new List<string> { "OrganizationId", "ParentId", "Status", "CreateTime" });
                model.Status = 1;
                string sql = string.Format("insert into [sys_organization] ({0}) values ({1});", string.Join(",", fields), string.Join(",", fields.Select(n => "@" + n)));
                sql += ";select @@identity";
                return conn.ExecuteScalar<int>(sql, model);
            }
        }

        public bool UpdateAsync(Organization model)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                var fields = model.ToFields(removeFields: new List<string>
                {
                    "OrganizationId",
                    "CreateTime",
                    "Status",
                    "ParentId"
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

                string sql = string.Format("update [sys_organization] set {0} where OrganizationId=@OrganizationId;", string.Join(",", fieldList));
                return conn.Execute(sql, model) > 0;
            }
        }

        public bool DeleteAsync(int id)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = "update [sys_organization] set Status=0 where OrganizationId=@Id;";
                return conn.Execute(sql, new { Id = id }) > 0;
            }
        }
    }
}
