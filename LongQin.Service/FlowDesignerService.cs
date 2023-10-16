using LongQin.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LongQin.Models;
using LongQin.Common;
using System.Data;

namespace LongQin.Service
{
    public class FlowDesignerService : ServiceBase, IFlowDesignerService
    {
        IFlowDesignerRepository _flowDesignerRepository = AutofacRepository.Resolve<IFlowDesignerRepository>();

        public FlowDesignerService()
        {
            base.AddDisposableObject(_flowDesignerRepository);
        }

        public PageModel<FlowDesigner> GetFlowListAsync(int organizationId, int pageIndex, int pageSize)
        {
            return _flowDesignerRepository.GetFlowListAsync(organizationId, pageIndex, pageSize);
        }

        public FlowDesigner GetFlowByIdAsync(int flowId)
        {
            if (flowId <= 0)
            {
                throw new ArgumentException("id错误");
            }

            return _flowDesignerRepository.GetFlowByIdAsync(flowId);
        }

        public string GetFlowJson(int flowId)
        {
            var nodes = _flowDesignerRepository.GetFlowNodesAsync(flowId);
            var links = _flowDesignerRepository.GetFlowLinksAsync(flowId);
            StringBuilder sb = new StringBuilder("({rects:{");
            if (nodes != null && nodes.Count != 0)
            {
                int i = 0;
                foreach (FlowNode node in nodes)
                {
                    sb.Append("rect" + i + ":{data:{\"id\":\"" + node.NodeId + "\",\"name\":\"" + node.NodeName + "\",\"rectType\":\"" + node.NodeType
                        + "\",\"formId\":\"" + node.FormId + "\",\"cooperation\":\"" + node.Cooperation
                        + "\",\"virtual\":\"" + node.Virtual + "\",\"departmentId\":\"" + node.DepartmentId
                        + "\",\"positionId\":\"" + node.PositionId + "\",\"userId\":\"" + node.UserId
                        + "\",\"remark\":\"" + node.Description + "\"}, attr:{x:" + node.PositionX + ",y:" + node.PositionY
                        + "}, text:{\"text\":\"" + node.NodeName + "\"}}");
                    sb.Append(",");
                    i++;
                }
                sb = sb.Remove(sb.Length - 1, 1);
            }
            sb.Append("},paths:{");
            if (links != null && links.Count != 0)
            {
                int j = 0;
                foreach (FlowLink link in links)
                {
                    sb.Append("path" + j + ":{from:" + link.FromNodeId + ",to:" + link.ToNodeId + ",data:{\"id\":\"" + link.LinkId + "\",\"name\":\"" + link.LinkName
                        + "\",\"formId\":\"" + link.FormId + "\",\"field\":\"" + link.Field + "\",\"operator\":\"" + link.Operator + "\",\"operatorValue\":\"" + link.OperatorValue + "\",\"remark\":\"" + link.Description
                        + "\"}, text:{\"text\":\"" + link.LinkName + "\"}, textPos:{x:" + link.PositionX + ",y:" + link.PositionY + "}}");
                    sb.Append(",");
                    j++;
                }
                sb = sb.Remove(sb.Length - 1, 1);
            }
            sb.Append("}})");
            return sb.ToString();
        }

        public bool SaveAsync(FlowDesigner model, string nodes, string links)
        {
            bool result = false;
            if (model.FlowId == 0)
            {
                model.FlowId = _flowDesignerRepository.CreateFlowAsync(model);
                if (model.FlowId > 0)
                {
                    result = true;
                }
            }
            else
            {
                result = _flowDesignerRepository.UpdateFlowAsync(model);
            }
            if (result)
            {
                _flowDesignerRepository.DeleteNodeAsync(model.FlowId);
                _flowDesignerRepository.DeleteLinkAsync(model.FlowId);
                Dictionary<string, int> nodeDics = new Dictionary<string, int>();
                string[] nodeArr = nodes.Split(';');
                for (int i = 0; i < nodeArr.Length; i++)
                {
                    string[] node = nodeArr[i].Split(',');
                    FlowNode flowNode = new FlowNode();
                    flowNode.Gid = node[0];
                    flowNode.PositionX = node[1].ToInt32();
                    flowNode.PositionY = node[2].ToInt32();
                    flowNode.NodeId = node[3].ToInt32();
                    flowNode.NodeName = node[4];
                    flowNode.NodeType = node[5].ToInt32();
                    flowNode.FormId = node[6].ToInt32();
                    flowNode.Virtual = node[7].ToInt32();
                    flowNode.Cooperation = node[8].ToInt32();
                    flowNode.DepartmentId = node[9].ToInt32();
                    flowNode.PositionId = node[10].ToInt32();
                    flowNode.UserId = node[11].ToInt32();
                    flowNode.Description = node[12];
                    flowNode.Groupseq = node[13].ToInt32();
                    flowNode.IsApproval = node[14].ToInt32();
                    flowNode.FlowId = model.FlowId;
                    flowNode.Creator = model.Creator;
                    flowNode.OrganizationId = model.OrganizationId;
                    if (flowNode.NodeId == 0)
                    {
                        flowNode.NodeId = _flowDesignerRepository.CreateNodeAsync(flowNode);
                    }
                    else
                    {
                        _flowDesignerRepository.UpdateNodeAsync(flowNode);
                    }
                    nodeDics.Add(flowNode.Gid, flowNode.NodeId);
                }
                string[] linkArr = links.Split(';');
                for (int j = 0; j < linkArr.Length; j++)
                {
                    string[] link = linkArr[j].Split(',');
                    FlowLink flowLink = new FlowLink();
                    int fromNodeId = 0;
                    nodeDics.TryGetValue(link[0], out fromNodeId);
                    flowLink.FromNodeId = fromNodeId;
                    int toNodeId = 0;
                    nodeDics.TryGetValue(link[1], out toNodeId);
                    flowLink.ToNodeId = toNodeId;
                    flowLink.LinkName = link[2];
                    flowLink.PositionX = link[3].ToInt32();
                    flowLink.PositionY = link[4].ToInt32();
                    flowLink.FormId = link[5].ToInt32();
                    flowLink.Field = link[6];
                    flowLink.Operator = link[7];
                    flowLink.OperatorValue = link[8];
                    flowLink.Description = link[9];
                    flowLink.FlowId = model.FlowId;
                    flowLink.Creator = model.Creator;
                    flowLink.OrganizationId = model.OrganizationId;
                    _flowDesignerRepository.CreateLinkAsync(flowLink);
                }
            }
            return result;
        }

        public bool DeleteFlowAsync(int flowId)
        {
            if (flowId <= 0)
            {
                throw new ArgumentException("id错误");
            }

            return _flowDesignerRepository.DeleteFlowAsync(flowId);
        }
    }
}
