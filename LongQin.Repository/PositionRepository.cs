using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LongQin.Models;
using Dapper;

namespace LongQin.Repository
{
    public class PositionRepository : IPositionRepository
    {
        public Position GetByIdAsync(int id)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = "select * from [sys_position] where PositionId = @id and Status=1;";
                var list = conn.Query<Position>(sql, new { id = id });
                return list != null ? list.FirstOrDefault() : null;
            }
        }

        public Position GetById(int id)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = "select * from [sys_position] where PositionId = @id and Status=1;";
                var list = conn.Query<Position>(sql, new { id = id });
                return list != null ? list.FirstOrDefault() : null;
            }
        }

        public List<Position> GetListAsync(int organizationId)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                if (organizationId == 0)
                {
                    string sql = "select * from [sys_position] where Status=1 Order by ParentId, PositionId;";
                    var list = conn.Query<Position>(sql);

                    return list != null ? list.ToList() : null;
                }
                else
                {
                    string sql = "select * from [sys_position] where Status=1 and OrganizationId=@organizationId Order by ParentId, PositionId;";
                    var list = conn.Query<Position>(sql, new { organizationId = organizationId });

                    return list != null ? list.ToList() : null;
                }
            }
        }

        public PageModel<Position> GetPageListAsync(int pageIndex, int pageSize)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string countSql = "select count(1) from [sys_position] where Status=1;";
                int total = conn.ExecuteScalar<int>(countSql);
                if (total == 0)
                {
                    return new PageModel<Position>();
                }

                string sql = string.Format(@"select * from (select *, ROW_NUMBER() over (Order by ParentId, PositionId) as RowNumber from [sys_position] where Status=1) as b where RowNumber between {0} and {1};", ((pageIndex - 1) * pageSize) + 1, pageIndex * pageSize);
                var list = conn.Query<Position>(sql);

                return new PageModel<Position>
                {
                    Total = total,
                    Data = list != null ? list.ToList() : null
                };
            }
        }

        public bool InsertAsync(Position model)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                var fields = model.ToFields(removeFields: new List<string> { "PositionId", "CreateTime", "Status", "Children" });
                if (fields == null || fields.Count == 0)
                {
                    return false;
                }

                model.Status = 1;

                string sql = string.Format("insert into [sys_position] ({0}) values ({1});", string.Join(",", fields), string.Join(",", fields.Select(n => "@" + n)));
                return conn.Execute(sql, model) > 0;
            }
        }

        public bool UpdateAsync(Position model)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                var fields = model.ToFields(removeFields: new List<string>
                {
                    "PositionId",
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

                string sql = string.Format("update [sys_position] set {0} where PositionId=@PositionId;", string.Join(",", fieldList));
                return conn.Execute(sql, model) > 0;
            }
        }

        public bool DeleteAsync(int id)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = "update [sys_position] set Status=0 where PositionId=@Id;";
                return conn.Execute(sql, new { Id = id }) > 0;
            }
        }

        public List<Position> GetChildrenListAsync(int parentId)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = string.Format(@"select * from [sys_position] where ParentId={0} and Status=1 Order by PositionId;", parentId);
                var list = conn.Query<Position>(sql);

                return list != null ? list.ToList() : null;
            }
        }
    }
}
