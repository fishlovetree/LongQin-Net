using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LongQin.Models;
using Dapper;

namespace LongQin.Repository
{
    public class MenuRepository : IMenuRepository
    {
        public Menu GetByIdAsync(int id)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = "select * from [sys_menu] where MenuId = @id and Status=1;";
                var list = conn.Query<Menu>(sql, new { id = id });
                return list != null ? list.FirstOrDefault() : null;
            }
        }

        public Menu GetById(int id)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = "select * from [sys_menu] where MenuId = @id and Status=1;";
                var list = conn.Query<Menu>(sql, new { id = id });
                return list != null ? list.FirstOrDefault() : null;
            }
        }

        public List<Menu> GetListAsync()
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = "select * from [sys_menu] where Status=1 Order by ParentId, GroupSeq;";
                var list = conn.Query<Menu>(sql);

                return list != null ? list.ToList() : null;
            }
        }

        public PageModel<Menu> GetPageListAsync(int pageIndex, int pageSize)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string countSql = "select count(1) from [sys_menu] where Status=1;";
                int total = conn.ExecuteScalar<int>(countSql);
                if (total == 0)
                {
                    return new PageModel<Menu>();
                }

                string sql = string.Format(@"select * from (select *, ROW_NUMBER() over (Order by ParentId, GroupSeq) as RowNumber from [sys_menu] where Status=1) as b where RowNumber between {0} and {1};", ((pageIndex - 1) * pageSize) + 1, pageIndex * pageSize);
                var list = conn.Query<Menu>(sql);

                return new PageModel<Menu>
                {
                    Total = total,
                    Data = list != null ? list.ToList() : null
                };
            }
        }

        public bool InsertAsync(Menu model)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                var fields = model.ToFields(removeFields: new List<string> { "MenuId", "CreateTime", "Children" });
                if (fields == null || fields.Count == 0)
                {
                    return false;
                }

                model.Status = 1;

                string sql = string.Format("insert into [sys_menu] ({0}) values ({1});", string.Join(",", fields), string.Join(",", fields.Select(n => "@" + n)));
                return conn.Execute(sql, model) > 0;
            }
        }

        public bool UpdateAsync(Menu model)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                var fields = model.ToFields(removeFields: new List<string>
                {
                    "MenuId",
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

                string sql = string.Format("update [sys_menu] set {0} where MenuId=@MenuId;", string.Join(",", fieldList));
                return conn.Execute(sql, model) > 0;
            }
        }

        public bool UpdateByUrlAsync(Menu model)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                var fields = model.ToFields(removeFields: new List<string>
                {
                    "MenuId",
                    "CreateTime",
                    "Status",
                    "Children",
                    "MenuUrl",
                    "ParentId",
                    "GroupSeq",
                    "MenuIcon",
                    "OrganizationId"
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

                string sql = string.Format("update [sys_menu] set {0} where MenuUrl=@MenuUrl;", string.Join(",", fieldList));
                return conn.Execute(sql, model) >= 0;
            }
        }

        public bool DeleteAsync(int id)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = "update [sys_menu] set Status=0 where MenuId=@Id;";
                return conn.Execute(sql, new { Id = id }) > 0;
            }
        }

        public List<Menu> GetChildrenListAsync(int parentId, int userId, int organizationId)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql;
                if (organizationId == 0)
                {
                    sql = string.Format(@"select m.* from sys_menu m where m.parentId={0} and m.status=1 order by m.groupseq;", parentId);
                }
                else
                {
                    sql = string.Format(@"select distinct t.* from (select m.* from sys_userrole ur 
                                left join sys_rolemenu rm on rm.roleid = ur.roleid
                                left join sys_role r on r.roleid = rm.roleid
                                left join sys_menu m on m.menuid = rm.menuid
                                where m.parentId={0} and ur.userid={1} and r.status=1 and m.status=1
                                union select s.* from sys_menu s
                                where s.parentId={0} and s.organizationId={2} and s.status=1) t order by t.groupseq;", parentId, userId, organizationId);
                }
                var list = conn.Query<Menu>(sql);

                return list != null ? list.ToList() : null;
            }
        }

        public List<Menu> GetUserMenuListAsync(int userId, int organizationId)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql;
                if (organizationId == 0)
                {
                    sql = @"select m.* from sys_menu m where m.status=1 order by m.parentId, m.groupseq;";
                }
                else
                {
                    sql = @"select distinct t.* from (select m.* from sys_userrole ur 
                                left join sys_rolemenu rm on rm.roleid = ur.roleid
                                left join sys_role r on r.roleid = rm.roleid
                                left join sys_menu m on m.menuid = rm.menuid
                                where ur.userid=@userId and r.status=1 and m.status=1 
                                union select s.* from sys_menu s
                                where s.organizationId=@organizationId and s.status=1) t
                                order by t.parentId, t.groupseq;";
                }
                var list = conn.Query<Menu>(sql, new { userId = userId, organizationId = organizationId });

                return list != null ? list.ToList() : null;
            }
        }
    }
}
