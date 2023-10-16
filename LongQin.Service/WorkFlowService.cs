using LongQin.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LongQin.Models;
using LongQin.Common;
using System.Data;
using System.Transactions;
using System.Diagnostics;

namespace LongQin.Service
{
    public class WorkFlowService : ServiceBase, IWorkFlowService
    {
        IWorkFlowRepository _workFlowRepository = AutofacRepository.Resolve<IWorkFlowRepository>();
        IUserRepository _userRepository = AutofacRepository.Resolve<IUserRepository>();
        IDepartmentRepository _departmentRepository = AutofacRepository.Resolve<IDepartmentRepository>();
        IPositionRepository _positionRepository = AutofacRepository.Resolve<IPositionRepository>();
        IFormDesignerRepository _formDesignerRepository = AutofacRepository.Resolve<IFormDesignerRepository>();

        public WorkFlowService()
        {
            base.AddDisposableObject(_workFlowRepository);
        }

        public FormDesigner GetFlowBeignNodeFormAsync(int flowId)
        {
            if (flowId <= 0)
            {
                throw new ArgumentException("id错误");
            }

            return _workFlowRepository.GetFlowBeignNodeFormAsync(flowId);
        }

        // 处理用户提交的工作
        public int DealWorkAsync(int flowId, int processId, int action, string tableName,
            List<string> columns, List<string> values, int submitter, int organizationId, bool isSave)
        {
            using (TransactionScope scope = new TransactionScope()) // 开启事务
            {
                // 先处理表单数据，增加创建人、创建时间、组织机构、表单状态
                columns.Add("creator");
                values.Add(submitter.ToString());
                columns.Add("createTime");
                string format = "yyyy-MM-dd HH:mm:ss";
                values.Add(DateTime.Now.ToString(format));
                columns.Add("organizationId");
                values.Add(organizationId.ToString());
                columns.Add("status");
                values.Add("1");

                bool isSucceed = false;
                int result = 1;
                if (processId == 0)
                {
                    // 工作还没创建
                    // 先创建工作实例以及生成一条代办
                    FlowNode node = _workFlowRepository.GetFlowBeignNodeAsync(flowId);
                    int nodeId = node.NodeId;
                    FlowWork work = new FlowWork();
                    work.FlowId = flowId;
                    work.Creator = submitter;
                    work.OrganizationId = organizationId;
                    int workId = _workFlowRepository.CreateFlowWorkAsync(work);
                    if (workId == 0) return -1;
                    FlowProcess process = new FlowProcess();
                    process.WorkId = workId;
                    process.NodeId = node.NodeId;
                    process.LinkId = 0;
                    process.SendingTo = submitter;
                    process.ProcessType = 1;
                    process.Submitter = submitter;
                    process.Status = 1;
                    process.OrganizationId = organizationId;
                    processId = _workFlowRepository.CreateFlowProcessAsync(process);
                    if (processId == 0) return -1;
                    // 插入表单数据
                    isSucceed = DealFormData(workId, processId, nodeId, tableName, columns, values, 0);
                    if (!isSucceed) return -1;
                    if (!isSave)
                    {
                        // 流程流转
                        result = ExcuteFlowAsync(flowId, workId, processId, nodeId, action, columns, values, submitter, organizationId);
                    }
                    else
                    {
                        // 返回代办工作ID
                        result = processId;
                    }
                }
                else
                {
                    FlowProcess flowProcess = _workFlowRepository.GetProcessByIdAsync(processId);
                    // 插入表单数据
                    isSucceed = DealFormData(flowProcess.WorkId, processId, flowProcess.NodeId, tableName, columns, values, flowProcess.FormDataId);
                    if (!isSucceed) return -1;
                    if (!isSave)
                    {
                        // 流程流转
                        result = ExcuteFlowAsync(flowProcess.FlowId, flowProcess.WorkId, processId, flowProcess.NodeId, action, columns, values, submitter, organizationId);
                    }
                }
                if (result > 0) 
                {
                    scope.Complete();
                }
                return result;
            }
        }

        // 流程流转
        // 返回值：1-成功，-1失败，-2没找到处理人
        public int ExcuteFlowAsync(int flowId, int workId, int processId, int nodeId, int action, List<string> columns, List<string> values, int submitter, int organizationId)
        {
            bool isSucceed = false;
            int sendingTo = 0;
            // 插入操作步骤
            FlowStep step = new FlowStep();
            step.WorkId = workId;
            step.NodeId = nodeId;
            step.ProcessId = processId;
            step.Submitter = submitter;
            step.OrganizationId = organizationId;
            step.Action = action;
            isSucceed = _workFlowRepository.CreateFlowStepAsync(step);
            if (!isSucceed) return -1;
            if (action == 1) // 前进
            {
                // 获取当前节点
                FlowNode fromNode = _workFlowRepository.GetFlowNodeByIdAsync(nodeId);
                bool needCooperation = NeedCooperation(workId, fromNode);
                if (!needCooperation)
                {
                    // 无需多人协作，关闭当前节点所有待办
                    isSucceed = _workFlowRepository.CloseNodeProcessAsync(workId, nodeId);
                    if (!isSucceed) return -1;
                    // 获取节点连线
                    List<FlowLink> links = _workFlowRepository.GetFlowNodeLinksAsync(nodeId);
                    if (links.Count == 0)
                    {
                        // 没有后继节点，结束流程
                        isSucceed = _workFlowRepository.CloseWorkAsync(workId);
                        if (!isSucceed) return -1;
                        else return 1;
                    }
                    // 创建下个节点待办
                    // 判断是否是普通节点或者合流点（只有普通节点或合流点存在条件走向，或后继节点可能是合流点）
                    if (fromNode.NodeType == 0 || fromNode.NodeType == 2)
                    {
                        // 判断是否有条件
                        int toNodeId = 0;
                        int linkId = 0;
                        foreach (FlowLink link in links)
                        {
                            string field = link.Field;
                            string operatorName = link.Operator;
                            string operatorValue = link.OperatorValue;
                            if (!String.IsNullOrEmpty(field) && !String.IsNullOrEmpty(operatorName) && !String.IsNullOrEmpty(operatorValue))
                            {
                                int submitterCondition = submitter;
                                // 判断条件表单，不为0表示取其他节点表单
                                if (link.FormId != 0) 
                                {
                                    FormDesigner formDesigner = _formDesignerRepository.GetByIdAsync(link.FormId);
                                    columns = _workFlowRepository.GetTableColumnsAsync(formDesigner.TableName);
                                    values = _workFlowRepository.GetTableValueAsync(workId, formDesigner.TableName);
                                }
                                // 有条件，判断满足哪个条件
                                if (field == "userId")
                                {
                                    // 提交人
                                    switch (operatorName)
                                    {
                                        case "=":
                                            if (operatorValue == submitter.ToString())
                                            {
                                                toNodeId = link.ToNodeId;
                                                linkId = link.LinkId;
                                            }
                                            break;
                                        case "!=":
                                            if (operatorValue != submitter.ToString())
                                            {
                                                toNodeId = link.ToNodeId;
                                                linkId = link.LinkId;
                                            }
                                            break;
                                    }
                                }
                                else if (field == "positionLevel")
                                {
                                    // 提交人职级
                                    // 获取提交人最高职级
                                    int positionLevel = _userRepository.GetUserPositionLevelAsync(submitter);
                                    int val = operatorValue.ToInt32();
                                    switch (operatorName)
                                    {
                                        case "=":
                                            if (val == positionLevel)
                                            {
                                                toNodeId = link.ToNodeId;
                                                linkId = link.LinkId;
                                            }
                                            break;
                                        case "!=":
                                            if (val != positionLevel)
                                            {
                                                toNodeId = link.ToNodeId;
                                                linkId = link.LinkId;
                                            }
                                            break;
                                        case ">":
                                            if (positionLevel > val)
                                            {
                                                toNodeId = link.ToNodeId;
                                                linkId = link.LinkId;
                                            }
                                            break;
                                        case "<":
                                            if (positionLevel < val)
                                            {
                                                toNodeId = link.ToNodeId;
                                                linkId = link.LinkId;
                                            }
                                            break;
                                        case ">=":
                                            if (positionLevel >= val)
                                            {
                                                toNodeId = link.ToNodeId;
                                                linkId = link.LinkId;
                                            }
                                            break;
                                        case "<=":
                                            if (positionLevel <= val)
                                            {
                                                toNodeId = link.ToNodeId;
                                                linkId = link.LinkId;
                                            }
                                            break;
                                    }
                                }
                                else
                                {
                                    if (columns == null || values == null || columns.Count != values.Count) continue;
                                    for (int i = 0; i < columns.Count; i++)
                                    {
                                        string column = columns[i];
                                        string value = values[i];
                                        if (column == field)
                                        {
                                            switch (operatorName)
                                            {
                                                case "=":
                                                    if (operatorValue == value)
                                                    {
                                                        toNodeId = link.ToNodeId;
                                                        linkId = link.LinkId;
                                                    }
                                                    break;
                                                case "!=":
                                                    if (operatorValue != value)
                                                    {
                                                        toNodeId = link.ToNodeId;
                                                        linkId = link.LinkId;
                                                    }
                                                    break;
                                                case ">":
                                                    int intFormValue = value.ToInt32();
                                                    int intOperatorValue = operatorValue.ToInt32();
                                                    if (intFormValue > intOperatorValue)
                                                    {
                                                        toNodeId = link.ToNodeId;
                                                        linkId = link.LinkId;
                                                    }
                                                    break;
                                                case "<":
                                                    int intFormValue1 = value.ToInt32();
                                                    int intOperatorValue1 = operatorValue.ToInt32();
                                                    if (intFormValue1 < intOperatorValue1)
                                                    {
                                                        toNodeId = link.ToNodeId;
                                                        linkId = link.LinkId;
                                                    }
                                                    break;
                                                case ">=":
                                                    int intFormValue2 = value.ToInt32();
                                                    int intOperatorValue2 = operatorValue.ToInt32();
                                                    if (intFormValue2 >= intOperatorValue2)
                                                    {
                                                        toNodeId = link.ToNodeId;
                                                        linkId = link.LinkId;
                                                    }
                                                    break;
                                                case "<=":
                                                    int intFormValue3 = value.ToInt32();
                                                    int intOperatorValue3 = operatorValue.ToInt32();
                                                    if (intFormValue3 <= intOperatorValue3)
                                                    {
                                                        toNodeId = link.ToNodeId;
                                                        linkId = link.LinkId;
                                                    }
                                                    break;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        if (toNodeId == 0)
                        {
                            // 没有条件，则默认取第一个线路
                            FlowLink link = links[0];
                            FlowNode toNode = _workFlowRepository.GetFlowNodeByIdAsync(link.ToNodeId);
                            if (toNode.NodeType == 2 || toNode.NodeType == 3)
                            {
                                // 后继节点是合流点或者分合流点，则需判断前置节点待办是否都已处理
                                int preNodeProcess = _workFlowRepository.GetPreNodeProcessCountAsync(workId, link.ToNodeId);
                                if (preNodeProcess == 0)
                                {
                                    // 前驱节点都已关闭，创建后继节点的待办，否则不做处理
                                    List<int> handlers = GetHandlerAsync(link.ToNodeId, submitter);
                                    if (handlers.Count == 0) 
                                    {
                                        return -2;
                                    }
                                    // 默认返回第一个处理人
                                    sendingTo = handlers[0];
                                    foreach (int handler in handlers)
                                    {
                                        // 创建代办工作
                                        FlowProcess process = new FlowProcess();
                                        process.WorkId = workId;
                                        process.NodeId = link.ToNodeId;
                                        process.LinkId = link.LinkId;
                                        process.SendingTo = handler;
                                        process.ProcessType = 1;
                                        process.Submitter = submitter;
                                        process.OrganizationId = organizationId;
                                        process.Status = 1;
                                        int newProcessId = _workFlowRepository.CreateFlowProcessAsync(process);
                                        if (newProcessId <= 0) return -1;
                                    }
                                }
                            }
                            else
                            {
                                // 后继节点是普通或者分流点，则直接生成待办工作
                                List<int> handlers = GetHandlerAsync(link.ToNodeId, submitter);
                                if (handlers.Count == 0)
                                {
                                    return -2;
                                }
                                // 默认返回第一个处理人
                                sendingTo = handlers[0];
                                foreach (int handler in handlers)
                                {
                                    // 创建代办工作
                                    FlowProcess process = new FlowProcess();
                                    process.WorkId = workId;
                                    process.NodeId = link.ToNodeId;
                                    process.LinkId = link.LinkId;
                                    process.SendingTo = handler;
                                    process.ProcessType = 1;
                                    process.Submitter = submitter;
                                    process.OrganizationId = organizationId;
                                    process.Status = 1;
                                    int newProcessId = _workFlowRepository.CreateFlowProcessAsync(process);
                                    if (newProcessId <= 0) return -1;
                                }
                            }
                        }
                        else
                        {
                            // 没有条件，则直接生成待办工作
                            List<int> handlers = GetHandlerAsync(toNodeId, submitter);
                            if (handlers.Count == 0)
                            {
                                return -2;
                            }
                            // 默认返回第一个处理人
                            sendingTo = handlers[0];
                            foreach (int handler in handlers)
                            {
                                // 创建代办工作
                                FlowProcess process = new FlowProcess();
                                process.WorkId = workId;
                                process.NodeId = toNodeId;
                                process.LinkId = linkId;
                                process.SendingTo = handler;
                                process.ProcessType = 1;
                                process.Submitter = submitter;
                                process.OrganizationId = organizationId;
                                process.Status = 1;
                                int newProcessId = _workFlowRepository.CreateFlowProcessAsync(process);
                                if (newProcessId <= 0) return -1;
                            }
                        }
                    }
                    else if (fromNode.NodeType == 1 || fromNode.NodeType == 3)
                    {
                        // 分流节点或者分合流点
                        // 遍历后继节点分别创建待办工作
                        foreach (FlowLink link in links)
                        {
                            List<int> handlers = GetHandlerAsync(link.ToNodeId, submitter);
                            if (handlers.Count == 0)
                            {
                                return -2;
                            }
                            // 默认返回第一个处理人
                            sendingTo = handlers[0];
                            foreach (int handler in handlers)
                            {
                                // 创建代办工作
                                FlowProcess process = new FlowProcess();
                                process.WorkId = workId;
                                process.NodeId = link.ToNodeId;
                                process.LinkId = link.LinkId;
                                process.SendingTo = handler;
                                process.ProcessType = 1;
                                process.Submitter = submitter;
                                process.OrganizationId = organizationId;
                                process.Status = 1;
                                int newProcessId = _workFlowRepository.CreateFlowProcessAsync(process);
                                if (newProcessId <= 0) return -1;
                            }
                        }
                    }
                }
                else
                {
                    // 需多人协作，只关闭当前处理人待办
                    isSucceed = _workFlowRepository.CloseUserProcessAsync(processId);
                    if (!isSucceed) return -1;
                }
            }
            else
            {
                // 作废所有已办和未办工作
                _workFlowRepository.DisableProcessAsync(workId);
                // 后退到开始节点
                FlowNode beginNode = _workFlowRepository.GetFlowBeignNodeAsync(flowId);
                FlowProcess beginProcess = _workFlowRepository.GetFlowNodeProcessAsync(workId, beginNode.NodeId);
                // 默认返回第一个处理人
                sendingTo = beginProcess.SendingTo;
                beginProcess.Status = 1;
                int newProcessId = _workFlowRepository.CreateFlowProcessAsync(beginProcess);
                if (newProcessId <= 0) return -1;
                FlowWorkForm form = _workFlowRepository.GetWorkFormAsync(beginProcess.ProcessId);
                // 删除原有关联
                _workFlowRepository.DeleteWorkFormAsync(form.Id);
                form.ProcessId = newProcessId;
                isSucceed = _workFlowRepository.InsertWorkFormAsync(form);
                if (!isSucceed) return -1;
            }
            return sendingTo;
        }

        // 处理业务数据
        public bool DealFormData(int workId, int processId, int nodeId, string tableName, List<string> columns, List<string> values, int formDataId) 
        {
            if (formDataId == 0)
            {
                formDataId = _workFlowRepository.InsertFormDataAsync(tableName, columns, values);
                if (formDataId > 0)
                {
                    FlowWorkForm flowWorkForm = new FlowWorkForm();
                    flowWorkForm.WorkId = workId;
                    flowWorkForm.ProcessId = processId;
                    flowWorkForm.NodeId = nodeId;
                    flowWorkForm.TableName = tableName;
                    flowWorkForm.FormDataId = formDataId;
                    return _workFlowRepository.InsertWorkFormAsync(flowWorkForm);
                }
                else 
                {
                    return false;
                }
            }
            else 
            {
                return _workFlowRepository.UpdateFormDataAsync(tableName, columns, values, formDataId);
            }
        }

        // 是否需要多人协作（false表示不需要或者是最后一人）
        private bool NeedCooperation(int workId, FlowNode node) 
        {
            if (node.Cooperation == 1)
            {
                int processCount = _workFlowRepository.GetNodeProcessCountAsync(workId, node.NodeId);
                if (processCount > 1) 
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else 
            {
                return false;
            }
        }

        // 获取处理人
        public List<int> GetHandlerAsync(int toNodeId, int submitterId)
        {
            List<int> handlers = new List<int>();
            User submitter = _userRepository.GetByIdAsync(submitterId);
            FlowNode toNode = _workFlowRepository.GetFlowNodeByIdAsync(toNodeId);
            // 先判断节点是否配置指定用户
            if (toNode.UserId != 0)
            {
                handlers.Add(toNode.UserId);
            }
            else if (toNode.PositionId != 0)
            {
                if (toNode.DepartmentId != 0)
                {
                    // 指定部门和职位
                    handlers = _userRepository.GetUserByDeptAndPositionAsync(toNode.DepartmentId, toNode.PositionId);
                }
                else
                {
                    // 只指定了职位,根据用户所属部门向上逐级查找
                    GetUserByPosition(toNode.PositionId, submitter.DepartmentId, ref handlers);
                }
            }
            else if (toNode.DepartmentId != 0)
            {
                // 只指定了部门,根据用户职位向上逐级查找
                GetUserByDepartment(submitter.PositionId, toNode.DepartmentId, ref  handlers);
            }
            else
            {
                // 啥都没指定
                GetUserRecursion(submitter.PositionId, submitter.DepartmentId, ref handlers);
            }
            return handlers;
        }

        // 指定职位，根据用户所属部门向上逐级查找
        private void GetUserByPosition(int positionId, int departmentId, ref List<int> handlers)
        {
            handlers = _userRepository.GetUserByDeptAndPositionAsync(departmentId, positionId);
            if (handlers == null || handlers.Count == 0)
            {
                Department dept = _departmentRepository.GetByIdAsync(departmentId);
                int parentId = dept.ParentId;
                if (parentId != 0)
                {
                    // 递归获取处理人
                    GetUserByPosition(positionId, parentId, ref handlers);
                }
            }
        }

        // 指定部门，根据用户所属职位向上逐级查找
        private void GetUserByDepartment(int positionId, int departmentId, ref List<int> handlers)
        {
            Position position = _positionRepository.GetByIdAsync(positionId);
            int parentId = position.ParentId;
            if (parentId != 0)
            {
                handlers = _userRepository.GetUserByDeptAndPositionAsync(departmentId, parentId);
                if (handlers == null || handlers.Count == 0)
                {
                    // 递归获取处理人
                    GetUserByDepartment(parentId, departmentId, ref handlers);
                }
            }
        }

        // 未指定部门和职位，根据用户所属职位和部门向上逐级查找
        private void GetUserRecursion(int positionId, int departmentId, ref List<int> handlers)
        {
            Position position = _positionRepository.GetByIdAsync(positionId);
            if (position != null && position.ParentId != 0)
            {
                int parentId = position.ParentId;
                handlers = _userRepository.GetUserByDeptAndPositionAsync(departmentId, parentId);
                if (handlers == null || handlers.Count == 0)
                {
                    // 递归部门获取处理人
                    GetUserByPosition(parentId, departmentId, ref handlers);
                    if (handlers == null || handlers.Count == 0)
                    {
                        // 递归职位和部门获取处理人
                        GetUserRecursion(parentId, departmentId, ref handlers);
                    }
                }
            }
        }

        // 获取待办工作列表
        public PageModel<Backlog> GetProcessListAsync(int userId, string beginDate, string endDate, int status, int pageIndex, int pageSize)
        {
            return _workFlowRepository.GetProcessListAsync(userId, beginDate, endDate, status, pageIndex, pageSize);
        }

        public FormDesigner GetFlowProcessFormAsync(int processId)
        {
            if (processId <= 0)
            {
                throw new ArgumentException("processId错误");
            }

            return _workFlowRepository.GetFlowProcessFormAsync(processId);
        }

        public Object GetFlowProcessFormDataAsync(int processId, string tableName)
        {
            if (processId <= 0)
            {
                throw new ArgumentException("processId错误");
            }

            return _workFlowRepository.GetFlowProcessFormDataAsync(processId, tableName);
        }

        // 获取已处理的表单列表
        public List<Dictionary<string, object>> GetWorkProcessFormListAsync(int workId)
        {
            if (workId <= 0)
            {
                throw new ArgumentException("workId错误");
            }

            List<Dictionary<string, object>> result = new List<Dictionary<string, object>>();
            List<ProcessForm> list = _workFlowRepository.GetWorkProcessFormListAsync(workId);
            if (list != null) 
            {
                foreach(ProcessForm item in list)
                {
                    item.SubmitTimeStr = item.SubmitTime == null ? "" : item.SubmitTime.ToString("yyyy-MM-dd HH:mm:ss");
                    object formData = _workFlowRepository.GetFlowProcessFormDataAsync(item.ProcessId, item.TableName);
                    Dictionary<string, object> dic = new Dictionary<string, object>();
                    dic.Add("form", item);
                    dic.Add("formData", formData);
                    result.Add(dic);
                }
            }

            return result;
        }

        // 处理用户提交的工作
        public int WorkTransferAsync(int processId, string transferUser, int submitter, int organizationId)
        {
            using (TransactionScope scope = new TransactionScope()) // 开启事务
            {
                bool isSucceed = false;
                int result = 1;
                FlowProcess flowProcess = _workFlowRepository.GetProcessByIdAsync(processId);
                // 插入操作步骤
                FlowStep step = new FlowStep();
                step.WorkId = flowProcess.WorkId;
                step.NodeId = flowProcess.NodeId;
                step.ProcessId = processId;
                step.Submitter = submitter;
                step.OrganizationId = organizationId;
                step.Action = 3; //转办
                isSucceed = _workFlowRepository.CreateFlowStepAsync(step);
                if (!isSucceed) return -1;
                isSucceed = _workFlowRepository.CloseUserProcessAsync(processId);
                if (!isSucceed) return -1;
                flowProcess.Status = 1;
                string[] users = transferUser.Split(',');
                for(int i = 0; i < users.Length; i++)
                {
                    flowProcess.SendingTo = users[i].ToInt32();
                    result = _workFlowRepository.CreateFlowProcessAsync(flowProcess);
                }
                if (result > 0)
                {
                    scope.Complete();
                }
                return result;
            }
        }

        // 获取待办工作列表
        public PageModel<FlowStep> GetWorkStepsAsync(int workId, int pageIndex, int pageSize)
        {
            PageModel<FlowStep> result = _workFlowRepository.GetWorkStepsAsync(workId, pageIndex, pageSize);
            for (int i = 0; i < result.Data.Count; i++)
            {
                TimeSpan ts = result.Data[i].SubmitTime - result.Data[i].BeginTime;
                result.Data[i].StayTime = ts.Days + "天" + ts.Hours + "小时" + ts.Minutes + "分" + ts.Seconds + "秒";
            }
            return result;
        }

        public bool DeleteWorkAsync(int workId)
        {
            return _workFlowRepository.DeleteWorkAsync(workId);
        }
    }
}
