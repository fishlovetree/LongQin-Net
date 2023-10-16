using LongQin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LongQin.Service
{
    public interface IFlowDesignerService
    {
        PageModel<FlowDesigner> GetFlowListAsync(int organizationId, int pageIndex, int pageSize);

        FlowDesigner GetFlowByIdAsync(int flowId);

        string GetFlowJson(int flowId);

        bool SaveAsync(FlowDesigner model, string nodes, string links);

        bool DeleteFlowAsync(int flowId);
    }
}
