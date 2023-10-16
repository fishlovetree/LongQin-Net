using LongQin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LongQin.Service
{
    public interface IWorkFlowService
    {
        FormDesigner GetFlowBeignNodeFormAsync(int flowId);

        int DealWorkAsync(int flowId, int processId, int action, string tableName,
            List<string> columns, List<string> values, int submitter, int organizationId, bool isSave);

        int ExcuteFlowAsync(int flowId, int workId, int processId, int nodeId, int action, List<string> columns, List<string> values, int submitter, int organizationId);

        PageModel<Backlog> GetProcessListAsync(int userId, string beginDate, string endDate, int status, int pageIndex, int pageSize);

        FormDesigner GetFlowProcessFormAsync(int processId);

        Object GetFlowProcessFormDataAsync(int processId, string tableName);

        List<Dictionary<string, object>> GetWorkProcessFormListAsync(int workId);

        int WorkTransferAsync(int processId, string transferUser, int submitter, int organizationId);

        PageModel<FlowStep> GetWorkStepsAsync(int workId, int pageIndex, int pageSize);

        bool DeleteWorkAsync(int workId);
    }
}