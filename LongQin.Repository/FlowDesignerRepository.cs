using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LongQin.Models;
using Dapper;

namespace LongQin.Repository
{
    public class FlowDesignerRepository : IFlowDesignerRepository
    {
        public int CreateFlowAsync(FlowDesigner model)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                var fields = model.ToFields(removeFields: new List<string> { "FlowId", "CreateTime" });
                string sql = string.Format("insert into [wf_flow] ({0}) values ({1});", string.Join(",", fields), string.Join(",", fields.Select(n => "@" + n)));
                sql += ";select @@identity";
                return conn.ExecuteScalar<int>(sql, model);
            }
        }

        public PageModel<FlowDesigner> GetFlowListAsync(int organizationId, int pageIndex, int pageSize)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string countSql = string.Format(@"select count(1) from [wf_flow] where OrganizationId = {0} and Status=1;", organizationId);
                int total = conn.ExecuteScalar<int>(countSql);
                if (total == 0)
                {
                    return new PageModel<FlowDesigner>();
                }

                string sql = string.Format(@"select * from (select *, ROW_NUMBER() over (Order by FlowId desc) as RowNumber from [wf_flow] where OrganizationId = {0} and Status=1) as b where RowNumber between {1};", organizationId, ((pageIndex - 1) * pageSize) + 1 + " and " + pageIndex * pageSize);
                var list = conn.Query<FlowDesigner>(sql);

                return new PageModel<FlowDesigner>
                {
                    Total = total,
                    Data = list != null ? list.ToList() : null
                };
            }
        }

        public FlowDesigner GetFlowByIdAsync(int id)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = "select * from [wf_flow] where flowid = @id and Status=1;";
                var list = conn.Query<FlowDesigner>(sql, new { id = id });
                return list != null ? list.FirstOrDefault() : null;
            }
        }

        public bool UpdateFlowAsync(FlowDesigner model)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                var fields = model.ToFields(removeFields: new List<string>
                {
                    "FlowId",
                    "CreateTime",
                    "Creator",
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

                string sql = string.Format("update [wf_flow] set {0} where flowid=@FlowId;", string.Join(",", fieldList));
                return conn.Execute(sql, model) > 0;
            }
        }

        public bool DeleteFlowAsync(int id)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = "update [wf_flow] set Status=0 where flowid=@FlowId;";
                return conn.Execute(sql, new { FlowId = id }) > 0;
            }
        }

        public int CreateNodeAsync(FlowNode model)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                var fields = model.ToFields(removeFields: new List<string> { "NodeId", "FormName", "CreateTime", "Gid" });
                string sql = string.Format("insert into [wf_node] ({0}) values ({1});", string.Join(",", fields), string.Join(",", fields.Select(n => "@" + n)));
                sql += ";select @@identity";
                return conn.ExecuteScalar<int>(sql, model);
            }
        }

        public bool UpdateNodeAsync(FlowNode model)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                var fields = model.ToFields(removeFields: new List<string>
                {
                    "NodeId",
                    "FormName",
                    "CreateTime",
                    "Creator",
                    "OrganizationId",
                    "Gid"
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
                fieldList.Add("Status=1");

                string sql = string.Format("update [wf_node] set {0} where nodeid=@NodeId;", string.Join(",", fieldList));
                return conn.Execute(sql, model) > 0;
            }
        }

        public bool DeleteNodeAsync(int flowId)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = "update [wf_node] set Status=0 where FlowId=@FlowId;";
                return conn.Execute(sql, new { FlowId = flowId }) > 0;
            }
        }

        public List<FlowNode> GetFlowNodesAsync(int flowId)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = string.Format(@"select * from [wf_node] where FlowId = {0} and Status=1;", flowId);
                var list = conn.Query<FlowNode>(sql).ToList();
                return list;
            }
        }

        public bool DeleteLinkAsync(int flowId)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = "delete from [wf_link] where FlowId=@FlowId;";
                return conn.Execute(sql, new { FlowId = flowId }) > 0;
            }
        }

        public bool CreateLinkAsync(FlowLink model)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                var fields = model.ToFields(removeFields: new List<string> { "LinkId", "CreateTime" });
                string sql = string.Format("insert into [wf_link] ({0}) values ({1});", string.Join(",", fields), string.Join(",", fields.Select(n => "@" + n)));
                return conn.Execute(sql, model) > 0;
            }
        }

        public List<FlowLink> GetFlowLinksAsync(int flowId)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = string.Format(@"select * from [wf_link] where FlowId = {0} and Status=1;", flowId);
                var list = conn.Query<FlowLink>(sql).ToList();
                return list;
            }
        }
    }
}
