using LongQin.Attributes;
using LongQin.Common;
using LongQin.Configs;
using LongQin.Models;
using LongQin.Service;
using LongQin.Service.Base;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace LongQin.Areas.FlowDesigner.Controllers
{
    [CheckLogin]
    public class FlowDesignController : ControllerBase
    {
        IFlowDesignerService _flowDesignerService = AutofacService.Resolve<IFlowDesignerService>();

        public ActionResult Index(int flowId = 0)
        {
            LongQin.Models.FlowDesigner model = new LongQin.Models.FlowDesigner();
            if (flowId > 0)
            {
                model = _flowDesignerService.GetFlowByIdAsync(flowId);
            }
            return View(model);
        }

        [HttpPost]
        [Operation("保存流程")]
        public JsonResult Save(int flowId, string flowName, int flowSort, string nodes, string links)
        {
            if (String.IsNullOrEmpty(flowName) || String.IsNullOrEmpty(nodes) || String.IsNullOrEmpty(links))
            {
                return Error("参数错误");
            }

            LongQin.Models.FlowDesigner model = new LongQin.Models.FlowDesigner();
            model.FlowId = flowId;
            model.FlowName = flowName;
            model.FlowSort = flowSort;
            model.Creator = LoginUser.UserId;
            model.OrganizationId = LoginUser.OrganizationId;

            var result = new ResultBase();
            result.success = _flowDesignerService.SaveAsync(model, nodes, links);
            return Json(result);
        }

        public ActionResult List()
        {
            return View();
        }

        public string GetFlowList(int page, int limit)
        {
            var list = _flowDesignerService.GetFlowListAsync(LoginUser.OrganizationId, page, limit);
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string str = serializer.Serialize(new
            {
                code = 0,
                msg = "",
                count = list.Total,
                data = list.Data
            });

            str = Regex.Replace(str, @"\\/Date\((\d+)\)\\/", match =>
            {

                DateTime dt = new DateTime(1970, 1, 1);
                dt = dt.AddMilliseconds(long.Parse(match.Groups[1].Value));
                dt = dt.ToLocalTime();
                return dt.ToString("yyyy-MM-dd HH:mm:ss");
            });
            return str;
        }

        public string GetFlowById(int flowId)
        {
            var model = _flowDesignerService.GetFlowByIdAsync(flowId);
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.Serialize(new
            {
                data = model
            });
        }

        public string GetFlowJson(int flowId)
        {
            return _flowDesignerService.GetFlowJson(flowId);
        }

        [HttpPost]
        [Operation("删除流程")]
        public JsonResult DeleteFlow(int flowId)
        {
            try
            {
                return Json(new ResultBase
                {
                    success = _flowDesignerService.DeleteFlowAsync(flowId)
                });
            }
            catch (Exception ex)
            {
                LogHelper.Error("FlowDesignController.Delete异常", ex);
                return Error(ex.Message);
            }
        }

        public ActionResult FormDesigner()
        {
            return View();
        }
    }
}