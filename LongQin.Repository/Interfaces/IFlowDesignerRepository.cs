using LongQin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LongQin.Repository
{
    public interface IFlowDesignerRepository
    {
        int CreateFlowAsync(FlowDesigner model);

        PageModel<FlowDesigner> GetFlowListAsync(int organizationId, int pageIndex, int pageSize);

        FlowDesigner GetFlowByIdAsync(int id);

        bool UpdateFlowAsync(FlowDesigner model);

        bool DeleteFlowAsync(int id);

        int CreateNodeAsync(FlowNode model);

        bool UpdateNodeAsync(FlowNode model);

        bool DeleteNodeAsync(int flowId);

        List<FlowNode> GetFlowNodesAsync(int flowId);

        bool DeleteLinkAsync(int flowId);

        bool CreateLinkAsync(FlowLink model);

        List<FlowLink> GetFlowLinksAsync(int flowId);
    }
}
