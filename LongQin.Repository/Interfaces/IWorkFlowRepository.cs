using LongQin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LongQin.Repository
{
    public interface IWorkFlowRepository
    {
        FormDesigner GetFlowBeignNodeFormAsync(int flowId);

        FlowNode GetFlowBeignNodeAsync(int flowId);

        int CreateFlowWorkAsync(FlowWork model);

        int CreateFlowProcessAsync(FlowProcess model);

        bool CreateFlowStepAsync(FlowStep model);

        int InsertFormDataAsync(string tableName, List<string> columns, List<string> values);

        bool UpdateFormDataAsync(string tableName, List<string> columns, List<string> values, int formId);

        bool InsertWorkFormAsync(FlowWorkForm form);

        List<FlowLink> GetFlowNodeLinksAsync(int nodeId);

        FlowNode GetFlowNodeByIdAsync(int nodeId);

        bool CloseWorkAsync(int workId);

        FlowProcess GetFlowNodeProcessAsync(int workId, int nodeId);

        FlowWorkForm GetWorkFormAsync(int processId);

        bool DeleteWorkFormAsync(int id);

        FlowProcess GetProcessByIdAsync(int processId);

        int GetNodeProcessCountAsync(int workId, int nodeId);

        int GetPreNodeProcessCountAsync(int workId, int nodeId);

        bool CloseNodeProcessAsync(int workId, int nodeId);

        bool CloseUserProcessAsync(int processId);

        bool DisableProcessAsync(int workId);

        PageModel<Backlog> GetProcessListAsync(int userId, string beginDate, string endDate, int status, int pageIndex, int pageSize);

        FormDesigner GetFlowProcessFormAsync(int processId);

        Object GetFlowProcessFormDataAsync(int processId, string tableName);

        List<ProcessForm> GetWorkProcessFormListAsync(int workId);

        PageModel<FlowStep> GetWorkStepsAsync(int workId, int pageIndex, int pageSize);

        List<string> GetTableColumnsAsync(string tableName);

        List<string> GetTableValueAsync(int workId, string tableName);

        bool DeleteWorkAsync(int workId);
    }
}
