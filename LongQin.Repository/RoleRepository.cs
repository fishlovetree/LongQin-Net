using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LongQin.Models;
using Dapper;

namespace LongQin.Repository
{
    public class RoleRepository : IRoleRepository
    {
        public Role GetByIdAsync(int id)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = "select * from [sys_role] where RoleId = @id and Status=1;";
                var list = conn.Query<Role>(sql, new { id = id });
                return list != null ? list.FirstOrDefault() : null;
            }
        }

        public List<Role> GetListAsync(int organizationId)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                if (organizationId == 0)
                {
                    string sql = "select * from [sys_role] where Status=1 Order by RoleId desc;";
                    var list = conn.Query<Role>(sql);

                    return list != null ? list.ToList() : null;
                }
                else
                {
                    string sql = "select * from [sys_role] where Status=1 and OrganizationId=@organizationId Order by RoleId desc;";
                    var list = conn.Query<Role>(sql, new { organizationId = organizationId });

                    return list != null ? list.ToList() : null;
                }
            }
        }

        public PageModel<Role> GetPageListAsync(int pageIndex, int pageSize)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string countSql = "select count(1) from [sys_role] where Status=1;";
                int total = conn.ExecuteScalar<int>(countSql);
                if (total == 0)
                {
                    return new PageModel<Role>();
                }

                string sql = string.Format(@"select * from (select *, ROW_NUMBER() over (Order by RoleId desc) as RowNumber from [sys_role] where Status=1) as b where RowNumber between {0} and {1};", ((pageIndex - 1) * pageSize) + 1, pageIndex * pageSize);
                var list = conn.Query<Role>(sql);

                return new PageModel<Role>
                {
                    Total = total,
                    Data = list != null ? list.ToList() : null
                };
            }
        }

        public bool InsertAsync(Role model)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                var fields = model.ToFields(removeFields: new List<string> { "RoleId", "Status", "CreaterTime", "MenuIds" });
                if (fields == null || fields.Count == 0)
                {
                    return false;
                }

                model.Status = 1;

                string sql = string.Format("insert into [sys_role] ({0}) values ({1});", string.Join(",", fields), string.Join(",", fields.Select(n => "@" + n)));
                return conn.Execute(sql, model) > 0;
            }
        }

        public bool UpdateAsync(Role model)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                var fields = model.ToFields(removeFields: new List<string>
                {
                    "RoleId",
                    "Status",
                    "CreaterTime",
                    "MenuIds"
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

                string sql = string.Format("update [sys_role] set {0} where RoleId=@RoleId;", string.Join(",", fieldList));
                return conn.Execute(sql, model) > 0;
            }
        }

        public bool DeleteAsync(int id)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = "update [sys_role] set Status=0 where RoleId=@Id;";
                return conn.Execute(sql, new { Id = id }) > 0;
            }
        }

        public List<int> GetRoleMenuAsync(int roleId)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = "select menuid from sys_rolemenu where roleid=@roleid";
                var list = conn.Query<int>(sql, new { roleid = roleId });
                return list != null ? list.ToList() : null;
            }
        }

        public bool SetMenuAsync(int roleId, string menuIds)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = @"delete from sys_rolemenu where roleid=@roleid
                  if @menuids is not null
                  begin
                    declare @@value varchar(100)
                    declare @@totalcount int
                    declare @@index int
                    select @@totalcount = count(1) from split(@menuids, ',')
                    set @@index = 1
                    while @@index <= @@totalcount
                    begin
                      set @@value = null
                      select @@value = value1 from split(@menuids, ',') where id = @@index
                      insert into sys_rolemenu (roleid,menuid) values (@roleid,@@value)
                      set @@index = @@index + 1
                    end
                  end";
                return conn.Execute(sql, new { roleid = roleId, menuids = menuIds }) > 0;
            }
        }
    }
}
