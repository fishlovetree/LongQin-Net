using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LongQin.Models;
using Dapper;

namespace LongQin.Repository
{
    public class UserRepository : IUserRepository
    {
        public User GetByIdAsync(int id)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = "select * from [sys_user] where UserId = @id and Status=1;";
                var list = conn.Query<User>(sql, new { id = id });
                return list != null ? list.FirstOrDefault() : null;
            }
        }

        public User GetById(int id)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = "select u.*, s.organizationName, s.logoPath, s.systemName from [sys_user] u left join [sys_organization] s on s.organizationId = u.organizationId where u.UserId = @id and u.Status=1;";
                var list = conn.Query<User>(sql, new { id = id });
                return list != null ? list.FirstOrDefault() : null;
            }
        }

        public User GetByNameAsync(string name)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = "select u.*, s.organizationName, s.logoPath, s.systemName from [sys_user] u left join [sys_organization] s on s.organizationId = u.organizationId where u.UserName = @name and u.Status=1;";
                return conn.QueryFirstOrDefault<User>(sql, new { name = name });
            }
        }

        public int GetCountByNameAsync(string name, int id)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = string.Format("select * from [sys_user] where UserName = '{0}' and Status=1 and UserId != {1};", name, id);
                return conn.ExecuteScalar<int>(sql);
            }
        }

        public PageModel<User> GetListAsync(int pageIndex, int pageSize, string departmentId, string nickName, int organizationId)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string whereSql = "";
                whereSql += string.IsNullOrEmpty(departmentId) ? "" : " and u.departmentId = '" + departmentId + "'";
                whereSql += string.IsNullOrEmpty(nickName) ? "" : " and u.nickName = '" + nickName + "'";
                if (organizationId == 0)
                {
                    string countSql = "select count(1) from [sys_user] u where u.Status=1" + whereSql;
                    int total = conn.ExecuteScalar<int>(countSql);
                    if (total == 0)
                    {
                        return new PageModel<User>();
                    }

                    string sql = string.Format(@"select * from (select u.*, d.departmentName, s.positionName, ROW_NUMBER() over (Order by u.UserId desc) as RowNumber from [sys_user] u
                        left join [sys_department] d on d.departmentId = u.departmentId
                        left join [sys_position] s on s.positionId = u.positionId 
                        where u.Status=1 {0}) as b where RowNumber between {1} and {2};", whereSql, ((pageIndex - 1) * pageSize) + 1, pageIndex * pageSize);
                    var list = conn.Query<User>(sql);

                    return new PageModel<User>
                    {
                        Total = total,
                        Data = list != null ? list.ToList() : null
                    };
                }
                else
                {
                    string countSql = "select count(1) from [sys_user] u where u.Status=1 and u.OrganizationId=@organizationId" + whereSql;
                    int total = conn.ExecuteScalar<int>(countSql, new { organizationId = organizationId });
                    if (total == 0)
                    {
                        return new PageModel<User>();
                    }

                    string sql = string.Format(@"select * from (select u.*, d.departmentName, s.positionName, ROW_NUMBER() over (Order by UserId desc) as RowNumber from [sys_user] u
                        left join [sys_department] d on d.departmentId = u.departmentId
                        left join [sys_position] s on s.positionId = u.positionId
                        where u.Status=1 and u.OrganizationId=@organizationId {0}) as b where RowNumber between {1} and {2};", whereSql, ((pageIndex - 1) * pageSize) + 1, pageIndex * pageSize);
                    var list = conn.Query<User>(sql, new { organizationId = organizationId });

                    return new PageModel<User>
                    {
                        Total = total,
                        Data = list != null ? list.ToList() : null
                    };
                }
            }
        }

        public int InsertAsync(User model)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                var fields = model.ToFields(removeFields: new List<string> { "UserId", "DepartmentName", "PositionName", "CreateTime", "OrganizationName", "LogoPath", "SystemName" });
                model.Status = 1;

                string sql = string.Format("insert into [sys_user] ({0}) values ({1});", string.Join(",", fields), string.Join(",", fields.Select(n => "@" + n)));
                sql += ";select @@identity";
                return conn.ExecuteScalar<int>(sql, model);
            }
        }

        public bool UpdateAsync(User model)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                var fields = model.ToFields(removeFields: new List<string>
                {
                    "UserId",
                    "CreateTime",
                    "Status",
                    "Password",
                    "DepartmentName",
                    "OrganizationName",
                    "PositionName",
                    "LogoPath",
                    "SystemName"
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

                string sql = string.Format("update [sys_user] set {0} where UserId=@UserId;", string.Join(",", fieldList));
                return conn.Execute(sql, model) > 0;
            }
        }

        public bool UpdatePasswordAsync(int id, string password)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = "update [sys_user] set Password=@Password where UserId=@Id;";
                return conn.Execute(sql, new { Id = id, Password = password }) > 0;
            }
        }

        public bool DeleteAsync(int id)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = "update [sys_user] set Status=0 where UserId=@Id;";
                return conn.Execute(sql, new { Id = id }) > 0;
            }
        }

        public List<int> GetUserRoleAsync(int userId)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = "select roleid from sys_userrole where userid=@userid";
                var list = conn.Query<int>(sql, new { userid = userId });
                return list != null ? list.ToList() : null;
            }
        }

        public bool SetRoleAsync(int userId, string roleIds)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = @"delete from sys_userrole where userid=@userid
                  if @roleids is not null
                  begin
                    declare @@value varchar(100)
                    declare @@totalcount int
                    declare @@index int
                    select @@totalcount = count(1) from split(@roleids, ',')
                    set @@index = 1
                    while @@index <= @@totalcount
                    begin
                      set @@value = null
                      select @@value = value1 from split(@roleids, ',') where id = @@index
                      insert into sys_userrole (userid,roleid) values (@userid,@@value)
                      set @@index = @@index + 1
                    end
                  end";
                return conn.Execute(sql, new { userid = userId, roleids = roleIds }) > 0;
            }
        }

        // 获取用户最高职级
        public int GetUserPositionLevelAsync(int userId)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = "select p.positionLevel from sys_user u left join sys_position p on p.positionId = u.positionId where u.userid=@userid";
                return conn.ExecuteScalar<int>(sql, new { userid = userId });
            }
        }

        // 获取用户最高职级
        public List<int> GetUserByDeptAndPositionAsync(int departmentId, int positionId)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = "select u.userId from sys_user u where u.departmentId=@departmentId and u.positionId=@positionId";
                var list = conn.Query<int>(sql, new { departmentId = departmentId, positionId = positionId });
                return list != null ? list.ToList() : null;
            }
        }
    }
}
