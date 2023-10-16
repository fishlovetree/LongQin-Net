using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LongQin.Models;
using Dapper;
using LongQin.Common;

namespace LongQin.Repository
{
    public class WorkFlowRepository : IWorkFlowRepository
    {
        public FormDesigner GetFlowBeignNodeFormAsync(int flowId)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = "select d.* from [wf_node] s left join [des_form] d on d.id = s.formId where s.flowId = @id and s.status=1 and s.groupseq = 1;";
                var list = conn.Query<FormDesigner>(sql, new { id = flowId });
                return list != null ? list.FirstOrDefault() : null;
            }
        }

        public FlowNode GetFlowBeignNodeAsync(int flowId)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = "select s.* from [wf_node] s where s.flowId = @id and s.status=1 and s.groupseq = 1;";
                var list = conn.Query<FlowNode>(sql, new { id = flowId });
                return list != null ? list.FirstOrDefault() : null;
            }
        }

        public int CreateFlowWorkAsync(FlowWork model)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                var fields = model.ToFields(removeFields: new List<string> { "WorkId", "CreateTime", "CloseTime", "Status" });
                string sql = string.Format("insert into [wf_work] ({0}) values ({1});", string.Join(",", fields), string.Join(",", fields.Select(n => "@" + n)));
                sql += ";select @@identity";
                return conn.ExecuteScalar<int>(sql, model);
            }
        }

        public int CreateFlowProcessAsync(FlowProcess model)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                var fields = model.ToFields(removeFields: new List<string> { "ProcessId", "ProcessType", "SubmitTime", "Flag", "FlowId", "FormDataId" });
                string sql = string.Format("insert into [wf_process] ({0}) values ({1});", string.Join(",", fields), string.Join(",", fields.Select(n => "@" + n)));
                sql += ";select @@identity";
                return conn.ExecuteScalar<int>(sql, model);
            }
        }

        public bool CreateFlowStepAsync(FlowStep model)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                var fields = model.ToFields(removeFields: new List<string> { "StepId", "SubmitTime", "NodeName", "SubmitterName", "BeginTime", "StayTime" });
                string sql = string.Format("insert into [wf_step] ({0}) values ({1});", string.Join(",", fields), string.Join(",", fields.Select(n => "@" + n)));
                return conn.Execute(sql, model) > 0;
            }
        }

        public int InsertFormDataAsync(string tableName, List<string> columns, List<string> values)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                for (int i = 0; i < values.Count; i++)
                {
                    if (values[i].Contains(","))
                    {
                        string[] arr = values[i].Split(",");
                        values[i] = "'" + string.Join("|", arr) + "'";
                    }
                    else
                    {
                        values[i] = "'" + values[i] + "'";
                    }
                }
                string sql = string.Format("insert into [" + tableName + "] ({0}) values ({1});", string.Join(",", columns), string.Join(",", values));
                sql += ";select @@identity";
                return conn.ExecuteScalar<int>(sql);
            }
        }

        public bool UpdateFormDataAsync(string tableName, List<string> columns, List<string> values, int formId)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                var fieldList = new List<string>();
                for (int i = 0;i < columns.Count; i++)
                {
                    fieldList.Add(string.Format("{0}='{1}'", columns[i], values[i]));
                }

                string sql = string.Format("update [" + tableName + "] set {0} where id = {1}; ", string.Join(",", fieldList), formId);
                return conn.Execute(sql) > 0;
            }
        }

        // 流程表单关联
        public bool InsertWorkFormAsync(FlowWorkForm model)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                var fields = model.ToFields(removeFields: new List<string> { "Id" });
                string sql = string.Format("insert into [wf_workform] ({0}) values ({1});", string.Join(",", fields), string.Join(",", fields.Select(n => "@" + n)));
                return conn.Execute(sql, model) > 0;
            }
        }

        public List<FlowLink> GetFlowNodeLinksAsync(int nodeId)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = "select * from wf_link where fromnodeid = @nodeId and status = 1;";
                var list = conn.Query<FlowLink>(sql, new { nodeId = nodeId });
                return list != null ? list.ToList() : null;
            }
        }

        public FlowNode GetFlowNodeByIdAsync(int nodeId)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = "select * from wf_node where nodeId = @nodeId and status = 1;";
                var list = conn.Query<FlowNode>(sql, new { nodeId = nodeId });
                return list != null ? list.FirstOrDefault() : null;
            }
        }

        public bool CloseWorkAsync(int workId)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = string.Format("update [wf_work] set status = 0, closeTime = getdate() where workId = {0};", workId);
                return conn.Execute(sql) > 0;
            }
        }

        public FlowProcess GetFlowNodeProcessAsync(int workId, int nodeId)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = "select top 1 * from [wf_process] where workId = @workId and nodeId = @nodeId order by processId desc;";
                var list = conn.Query<FlowProcess>(sql, new { workId = workId, nodeId = nodeId });
                return list != null ? list.FirstOrDefault() : null;
            }
        }

        public FlowWorkForm GetWorkFormAsync(int processId)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = "select * from [wf_workform] where processId = @processId;";
                var list = conn.Query<FlowWorkForm>(sql, new { processId = processId });
                return list != null ? list.FirstOrDefault() : null;
            }
        }

        public bool DeleteWorkFormAsync(int id)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = "delete from [wf_workform] where id = @id;";
                return conn.Execute(sql, new { Id = id }) > 0;
            }
        }

        public FlowProcess GetProcessByIdAsync(int processId)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = "select p.*, w.formDataId, k.flowId from [wf_process] p left join [wf_workform] w on w.processId = p.processId left join [wf_work] k on k.workId = p.workId where p.processId = @processId;";
                var list = conn.Query<FlowProcess>(sql, new { processId = processId });
                return list != null ? list.FirstOrDefault() : null;
            }
        }

        // 获取当前节点待办工作数量
        public int GetNodeProcessCountAsync(int workId, int nodeId)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = "select COUNT(0) from [wf_process] where workId = @workId and status = 1 and nodeId = @nodeId;";
                var result = conn.ExecuteScalar<int>(sql, new { workId = workId, nodeId = nodeId });
                return result;
            }
        }

        // 获取前置节点待办工作数量
        public int GetPreNodeProcessCountAsync(int workId, int nodeId) 
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = "select COUNT(0) from [wf_process] where workId = @workId and status = 1 and nodeId in (select fromNodeId from wf_link where toNodeId = @nodeId and status = 1);";
                var result = conn.ExecuteScalar<int>(sql, new { workId = workId, nodeId = nodeId });
                return result;
            }
        }

        public bool CloseNodeProcessAsync(int workId, int nodeId)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = string.Format("update [wf_process] set status = 0 where status = 1 and workId = {0} and nodeId = {1};", workId, nodeId);
                return conn.Execute(sql) > 0;
            }
        }

        public bool CloseUserProcessAsync(int processId)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = string.Format("update [wf_process] set status = 0 where processId = {0};", processId);
                return conn.Execute(sql) > 0;
            }
        }

        // 回退时禁用待办的工作
        public bool DisableProcessAsync(int workId)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = string.Format("update [wf_process] set status = 9 where workId = {0};", workId);
                return conn.Execute(sql) > 0;
            }
        }

        // 获取用户待办/已办工作列表
        public PageModel<Backlog> GetProcessListAsync(int userId, string beginDate, string endDate, int status, int pageIndex, int pageSize)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string whereSql = " and p.status = " + status;
                whereSql += string.IsNullOrEmpty(beginDate) ? "" : " and p.submitTime >= '" + beginDate + "'";
                whereSql += string.IsNullOrEmpty(endDate) ? "" : " and p.submitTime <= '" + endDate + "'";
                string countSql = @"select count(1) from [wf_process] p where p.sendingTo=@userId" + whereSql;
                int total = conn.ExecuteScalar<int>(countSql, new { userId = userId });
                if (total == 0)
                {
                    return new PageModel<Backlog>();
                }

                string sql = string.Format(@"select * from (select p.*, n.nodeName, n.formId, u1.nickName as submitterName, w.creator, u2.nickName as creatorName, w.createTime, d.departmentName,
                    f.flowId, f.flowName, ROW_NUMBER() over (Order by p.processId desc) as RowNumber from [wf_process] p
                    left join [wf_node] n on n.nodeId = p.nodeId
                    left join [sys_user] u1 on u1.userId = p.submitter
                    left join [wf_work] w on w.workId = p.workId
                    left join [wf_flow] f on f.flowId = w.flowId
                    left join [sys_user] u2 on u2.userId = w.creator 
                    left join [sys_department] d on d.departmentId = u2.departmentId
                    where p.sendingTo=@userId {0}) as b where RowNumber between {1} and {2};", whereSql, ((pageIndex - 1) * pageSize) + 1, pageIndex * pageSize);
                var list = conn.Query<Backlog>(sql, new { userId = userId });

                return new PageModel<Backlog>
                {
                    Total = total,
                    Data = list != null ? list.ToList() : null
                };
            }
        }

        public FormDesigner GetFlowProcessFormAsync(int processId)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = "select d.* from [wf_process] p left join [wf_node] s on s.nodeId= p.nodeId left join [des_form] d on d.id = s.formId where p.processId = @processId;";
                var list = conn.Query<FormDesigner>(sql, new { processId = processId });
                return list != null ? list.FirstOrDefault() : null;
            }
        }

        public Object GetFlowProcessFormDataAsync(int processId, string tableName)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = string.Format("select s.* from [{0}] s left join wf_workform f on f.formDataId = s.id where f.processId = @processId;", tableName);
                var list = conn.Query<Object>(sql, new { processId = processId });
                return list != null ? list.FirstOrDefault() : null;
            }
        }

        // 获取流程工作表单集合
        public List<ProcessForm> GetWorkProcessFormListAsync(int workId)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = @"select p.processId, u.nickName as submitterName, t.submitTime, d.* from [wf_process] p left join [wf_node] s on s.nodeId= p.nodeId left join [des_form] d on d.id = s.formId
                    left join [wf_workform] w on w.processId = p.processId 
                    left join [wf_step] t on t.processId = p.processId
                    left join [sys_user] u on t.submitter = u.userId
                    where p.workId = @workId and p.status = 0 and w.id is not null;";
                var list = conn.Query<ProcessForm>(sql, new { workId = workId });
                return list != null ? list.ToList() : null;
            }
        }

        // 获取工作历史记录
        public PageModel<FlowStep> GetWorkStepsAsync(int workId, int pageIndex, int pageSize)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string countSql = @"select count(1) from [wf_step] s where s.workId=@workId";
                int total = conn.ExecuteScalar<int>(countSql, new { workId = workId });
                if (total == 0)
                {
                    return new PageModel<FlowStep>
                    {
                        Total = total,
                        Data = new List<FlowStep>()
                    };
                }

                string sql = string.Format(@"select * from (select s.*, n.nodeName, u.nickName as submitterName, p.submitTime as beginTime,
                    ROW_NUMBER() over (Order by s.stepId desc) as RowNumber from [wf_step] s
                    left join [wf_node] n on n.nodeId = s.nodeId
                    left join [sys_user] u on u.userId = s.submitter
                    left join [wf_process] p on s.processId = p.processId
                    where s.workId=@workId) as b where RowNumber between {0} and {1};", ((pageIndex - 1) * pageSize) + 1, pageIndex * pageSize);
                var list = conn.Query<FlowStep>(sql, new { workId = workId });

                return new PageModel<FlowStep>
                {
                    Total = total,
                    Data = list != null ? list.ToList() : null
                };
            }
        }

        // 获取表列集合
        public List<string> GetTableColumnsAsync(string tableName)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = string.Format("select name from syscolumns where id = object_id('{0}');", tableName);
                var list = conn.Query<string>(sql);
                return list != null ? list.ToList() : null;
            }
        }

        // 获取表数据集合
        public List<string> GetTableValueAsync(int workId, string tableName)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = string.Format("select top 1 s.* from [{0}] s left join wf_workform f on f.formDataId = s.id where f.workId = {1} and f.tableName = '{0}' order by id desc;", tableName, workId);
                var data = conn.QueryFirst(sql);
                var fields = data as IDictionary<string, object>;
                List<string> result = new List<string>();
                fields.ForEach(item => result.Add(item.Value.ToStringOrDefault()));
                return result;
            }
        }

        public bool DeleteWorkAsync(int workId)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = string.Format("update [wf_work] set status = 9 where workId = {0}; update [wf_process] set status = 9 where workId = {0}", workId);
                return conn.Execute(sql) > 0;
            }
        }
    }
}
