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

namespace LongQin.Areas.Work.Controllers
{
    [CheckLogin]
    public class WorkFlowController : ControllerBase
    {
        IWorkFlowService _workFlowService = AutofacService.Resolve<IWorkFlowService>();
        IUserService _userService = AutofacService.Resolve<IUserService>();

        public ActionResult Start(int flowId = 0, string flowName = "")
        {
            ViewBag.flowId = flowId;
            ViewBag.flowName = flowName;
            return View();
        }

        [HttpGet]
        public string GetFlowBeginNodeForm(int flowId)
        {
            LongQin.Models.FormDesigner form = _workFlowService.GetFlowBeignNodeFormAsync(flowId);
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.Serialize(form);
        }

        [HttpPost]
        [Operation("流程处理")]
        [ValidateInput(false)]
        public JsonResult Deal(FormCollection collection)
        {
            int flowId = collection[0].ToInt32();
            int processId = collection[1].ToInt32();
            string tableName = collection[2];
            int isApproval = collection[3].ToInt32();
            bool isSave = collection[4].ToInt32() == 1 ? true : false; //是否暂存
            List<string> columns = new List<string>();
            List<string> values = new List<string>();
            for (int i = 5; i < collection.Count; i++)
            {
                string key = collection.AllKeys[i];
                if (key == "file") continue; //文件上传有两个input，屏蔽一个
                columns.Add(key);
                string val = collection[i];
                values.Add(val);
            }
            int direction = isApproval == 1 && collection[5] == "0" ? 2 : 1; // 前进-1还是后退-2
            var result = new ResultBase();
            int data = _workFlowService.DealWorkAsync(flowId, processId, direction, tableName, columns, values, LoginUser.UserId, LoginUser.OrganizationId, isSave);
            result.success = data > 0 ? true : false;
            if (!isSave && data > 1)
            {
                User user = _userService.GetByIdAsync(data);
                if (user != null)
                {
                    result.data = user.NickName;
                }
                else 
                {
                    result.data = data;
                }
            }
            else {
                result.data = data;
            }
            return Json(result);
        }

        // 待办工作列表
        public ActionResult Backlog()
        {
            return View();
        }

        public string GetProcessList(string beginDate, string endDate, int status, int page, int limit)
        {
            int userId = LoginUser.UserId;
            var list = _workFlowService.GetProcessListAsync(userId, beginDate, endDate, status, page, limit);
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

        public ActionResult DealWork(int workId, int processId, int flowId)
        {
            ViewBag.workId = workId;
            ViewBag.processId = processId;
            ViewBag.flowId = flowId;
            return View();
        }

        [HttpGet]
        public string GetFlowProcessForm(int processId)
        {
            LongQin.Models.FormDesigner form = _workFlowService.GetFlowProcessFormAsync(processId);
            if (form == null) return "";
            Object formData = _workFlowService.GetFlowProcessFormDataAsync(processId, form.TableName);
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("form", form);
            dic.Add("formData", formData);
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.Serialize(dic);
        }

        [HttpGet]
        public string GetFlowProcessFormData(int processId, string tableName)
        {
            Object formData = _workFlowService.GetFlowProcessFormDataAsync(processId, tableName);
            if (formData == null) return "";
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.Serialize(formData);
        }

        [HttpGet]
        public string GetWorkProcessFormListAsync(int workId)
        {
            List<Dictionary<string, object>> list = _workFlowService.GetWorkProcessFormListAsync(workId);
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.Serialize(list);
        }

        [HttpPost]
        [Operation("工作转办")]
        public JsonResult WorkTranfer(int processId, string transferUser)
        {
            var result = new ResultBase();
            int data = _workFlowService.WorkTransferAsync(processId, transferUser, LoginUser.UserId, LoginUser.OrganizationId);
            result.success = data > 0 ? true : false;
            result.data = data;
            return Json(result);
        }

        // 已办工作列表
        public ActionResult Completed()
        {
            return View();
        }

        // 工作明细
        public ActionResult Details(int workId, int processId, int flowId)
        {
            ViewBag.workId = workId;
            ViewBag.processId = processId;
            ViewBag.flowId = flowId;
            return View();
        }

        public string GetWorkSteps(int workId, int page, int limit)
        {
            var list = _workFlowService.GetWorkStepsAsync(workId, page, limit);
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

        [HttpPost]
        [Operation("上传文件")]
        public JsonResult UploadFile(string processId)
        {
            string fileName = "";
            var files = Request.Files;
            foreach (var key in files.AllKeys)
            {
                var file = Request.Files[key];
                string uploadResult = UploadHelper.Process(file.FileName, file.InputStream);
                if (!string.IsNullOrEmpty(uploadResult))
                {
                    fileName = uploadResult;
                }
            }
            var result = new ResultBase();
            result.success = String.IsNullOrEmpty(fileName) ? false : true;
            result.data = fileName;
            return Json(result);
        }

        public ActionResult FlowList()
        {
            return View();
        }

        [HttpPost]
        [Operation("工作作废")]
        public JsonResult DeleteWork(int workId)
        {
            var result = new ResultBase();
            result.success = _workFlowService.DeleteWorkAsync(workId);
            return Json(result);
        }
    }
}