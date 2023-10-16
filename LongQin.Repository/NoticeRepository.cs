using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LongQin.Models;
using Dapper;

namespace LongQin.Repository
{
    public class NoticeRepository : INoticeRepository
    {
        public Notice GetByIdAsync(int id)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = "select * from [sys_notice] where NoticeId = @id and Status=1;";
                var list = conn.Query<Notice>(sql, new { id = id });
                return list != null ? list.FirstOrDefault() : null;
            }
        }

        public PageModel<Notice> GetListAsync(int pageIndex, int pageSize, string title, string startDate, string endDate, int organizationId)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string whereSql = "";
                whereSql += string.IsNullOrEmpty(title) ? "" : " and n.title = '" + title + "'";
                whereSql += string.IsNullOrEmpty(startDate) ? "" : " and n.noticeDate >= '" + startDate + "'";
                whereSql += string.IsNullOrEmpty(endDate) ? "" : " and n.noticeDate <= '" + endDate + "'";
                whereSql += organizationId != 0 ? "" : " and n.organizationId = " + organizationId;
                string countSql = "select count(1) from [sys_notice] n where n.Status=1" + whereSql;
                int total = conn.ExecuteScalar<int>(countSql);
                if (total == 0)
                {
                    return new PageModel<Notice>();
                }

                string sql = string.Format(@"select * from (select n.*, d.departmentName, u.userName as creatorName, ROW_NUMBER() over (Order by n.NoticeId desc) as RowNumber from [sys_notice] n
                        left join [sys_user] u on u.userId = n.creator
                        left join [sys_department] d on d.departmentId = u.departmentId
                        where n.Status=1 {0}) as b where RowNumber between {1} and {2};", whereSql, ((pageIndex - 1) * pageSize) + 1, pageIndex * pageSize);
                var list = conn.Query<Notice>(sql);

                return new PageModel<Notice>
                {
                    Total = total,
                    Data = list != null ? list.ToList() : null
                };
            }
        }

        public int InsertAsync(Notice model)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                var fields = model.ToFields(removeFields: new List<string> { "NoticeId", "DepartmentName", "CreateTime", "Status", "CreatorName" });
                model.Status = 1;

                string sql = string.Format("insert into [sys_notice] ({0}) values ({1});", string.Join(",", fields), string.Join(",", fields.Select(n => "@" + n)));
                sql += ";select @@identity";
                return conn.ExecuteScalar<int>(sql, model);
            }
        }

        public bool UpdateAsync(Notice model)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                var fields = model.ToFields(removeFields: new List<string>
                {
                    "NoticeId",
                    "CreateTime",
                    "Status",
                    "DepartmentName",
                    "CreatorName"
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

                string sql = string.Format("update [sys_notice] set {0} where NoticeId=@NoticeId;", string.Join(",", fieldList));
                return conn.Execute(sql, model) > 0;
            }
        }

        public bool DeleteAsync(int id)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = "update [sys_notice] set Status=0 where NoticeId=@Id;";
                return conn.Execute(sql, new { Id = id }) > 0;
            }
        }

        public List<string> GetNoticeFilesAsync(int noticeId)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = "select filePath from sys_notice_files where noticeId=@noticeId";
                var list = conn.Query<string>(sql, new { noticeId = noticeId });
                return list != null ? list.ToList() : null;
            }
        }

        public bool SetNoticeFilesAsync(int noticeId, string files)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = @"delete from sys_notice_files where noticeId=@noticeId
                  if @files is not null
                  begin
                    declare @@value varchar(100)
                    declare @@totalcount int
                    declare @@index int
                    select @@totalcount = count(1) from split(@files, ',')
                    set @@index = 1
                    while @@index <= @@totalcount
                    begin
                      set @@value = null
                      select @@value = value1 from split(@files, ',') where id = @@index
                      insert into sys_notice_files (noticeId,filePath) values (@noticeId,@@value)
                      set @@index = @@index + 1
                    end
                  end";
                return conn.Execute(sql, new { noticeId = noticeId, files = files }) > 0;
            }
        }
    }
}
