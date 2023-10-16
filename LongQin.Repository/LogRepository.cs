using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LongQin.Models;
using Dapper;

namespace LongQin.Repository
{
    public class LogRepository : ILogRepository
    {
        public Log GetByIdAsync(int id)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = "select * from [sys_log] where logId = @id and Status=1;";
                var list = conn.Query<Log>(sql, new { id = id });
                return list != null ? list.FirstOrDefault() : null;
            }
        }

        public Log GetById(int id)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = "select * from [sys_log] where logId = @id and Status=1;";
                var list = conn.Query<Log>(sql, new { id = id });
                return list != null ? list.FirstOrDefault() : null;
            }
        }

        public PageModel<Log> GetListAsync(string beginDate, string endDate, int pageIndex, int pageSize, int organizationId)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                if (organizationId == 0)
                {
                    string whereSql = "";
                    whereSql += string.IsNullOrEmpty(beginDate) ? "" : " and s.createTime >= '" + beginDate + "'";
                    whereSql += string.IsNullOrEmpty(endDate) ? "" : " and s.createTime <= '" + endDate + "'";
                    string countSql = "select count(1) from [sys_log] s where 1 = 1" + whereSql;
                    int total = conn.ExecuteScalar<int>(countSql);
                    if (total == 0)
                    {
                        return new PageModel<Log>();
                    }

                    string sql = string.Format(@"select * from (select s.*, u.userName, ROW_NUMBER() over (Order by s.logId desc) as RowNumber from [sys_log] s
                        left join [sys_user] u on u.userId = s.userId where 1 = 1 {0}) as b where RowNumber between {1} and {2};", whereSql, ((pageIndex - 1) * pageSize) + 1, pageIndex * pageSize);
                    var list = conn.Query<Log>(sql);

                    return new PageModel<Log>
                    {
                        Total = total,
                        Data = list != null ? list.ToList() : null
                    };
                }
                else
                {
                    string whereSql = "";
                    whereSql += string.IsNullOrEmpty(beginDate) ? "" : " and s.createTime >= '" + beginDate + "'";
                    whereSql += string.IsNullOrEmpty(endDate) ? "" : " and s.createTime <= '" + endDate + "'";
                    string countSql = "select count(1) from [sys_log] s where s.OrganizationId=@organizationId" + whereSql;
                    int total = conn.ExecuteScalar<int>(countSql, new { organizationId = organizationId });
                    if (total == 0)
                    {
                        return new PageModel<Log>();
                    }

                    string sql = string.Format(@"select * from (select s.*, u.userName, ROW_NUMBER() over (Order by s.logId desc) as RowNumber from [sys_log] s
                        left join [sys_user] u on u.userId = s.userId where s.OrganizationId=@organizationId {0}) as b where RowNumber between {1} and {2};", whereSql, ((pageIndex - 1) * pageSize) + 1, pageIndex * pageSize);
                    var list = conn.Query<Log>(sql, new { organizationId = organizationId });

                    return new PageModel<Log>
                    {
                        Total = total,
                        Data = list != null ? list.ToList() : null
                    };
                }
            }
        }

        public bool InsertAsync(Log model)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                var fields = model.ToFields(removeFields: new List<string> { "LogId", "UserName", "CreateTime" });
                if (fields == null || fields.Count == 0)
                {
                    return false;
                }

                string sql = string.Format("insert into [sys_log] ({0}) values ({1});", string.Join(",", fields), string.Join(",", fields.Select(n => "@" + n)));
                return conn.Execute(sql, model) > 0;
            }
        }
    }
}
