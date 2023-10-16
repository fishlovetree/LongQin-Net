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

namespace LongQin.Areas.FormDesigner.Controllers
{
    [CheckLogin]
    public class FormDesignController : ControllerBase
    {
        IFormDesignerService _formDesignerService = AutofacService.Resolve<IFormDesignerService>();

        public ActionResult Index(int id = 0)
        {
            LongQin.Models.FormDesigner model = new LongQin.Models.FormDesigner();
            if (id != 0)
            {
                model = _formDesignerService.GetByIdAsync(id);
            }
            ViewBag.JsonData = model.JsonData;
            return View(model);
        }

        [HttpPost]
        [Operation("保存表单")]
        public JsonResult Set(LongQin.Models.FormDesigner model)
        {
            if (model == null || model.JsonData == null)
            {
                return Error("参数错误");
            }

            List<TableColumn> list = new List<TableColumn>();
            JArray jArray = (JArray)JsonConvert.DeserializeObject(model.JsonData);
            int length = jArray.Count;
            for (int i = 0; i < jArray.Count; i++)
            {
                string columnType = "";
                string tag = jArray[i]["tag"].ToString();
                switch (tag)
                {
                    case "input":
                        columnType = "[varchar](100)";
                        break;
                    case "textarea":
                        columnType = "[varchar](510)";
                        break;
                    case "editor":
                        columnType = "[text]";
                        break;
                    case "upload":
                        columnType = "[varchar](200)";
                        break;
                    case "tags":
                        columnType = "[varchar](200)";
                        break;
                    case "tips":
                    case "note":
                    case "subtraction":
                    case "tab":
                    case "grid":
                    case "space":
                        continue;
                    default:
                        columnType = "[varchar](50)";
                        break;
                }
                TableColumn tablColumn = new TableColumn();
                tablColumn.ColumnName = jArray[i]["name"].ToString();
                tablColumn.Description = jArray[i]["label"].ToString();
                tablColumn.ColumnType = columnType;
                string isNull = jArray[i]["required"] == null ? "false" : jArray[i]["required"].ToString();
                tablColumn.IsNull = isNull == "false" ? "NULL" : "NOT NULL";
                list.Add(tablColumn);
            }

            model.TableColumns = list;
            model.Creator = LoginUser.UserId;
            model.OrganizationId = LoginUser.OrganizationId;

            var result = new ResultBase();
            if (model.Id > 0)
            {
                result.success = _formDesignerService.UpdateAsync(model);
            }
            else
            {
                result.success = _formDesignerService.CreateAsync(model);
            }

            return Json(result);
        }

        public ActionResult List()
        {
            return View();
        }

        public string GetFormList(int page, int limit)
        {
            var list = _formDesignerService.GetListAsync(LoginUser.OrganizationId, page, limit);
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

        public string GetById(int id)
        {
            var model = _formDesignerService.GetByIdAsync(id);
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.Serialize(new
            {
                data = model
            });
        }

        [HttpPost]
        [Operation("删除表单")]
        public JsonResult Delete(int id)
        {
            try
            {
                return Json(new ResultBase
                {
                    success = _formDesignerService.DeleteAsync(id)
                });
            }
            catch (Exception ex)
            {
                LogHelper.Error("FormDesignController.Delete异常", ex);
                return Error(ex.Message);
            }
        }

        [HttpPost]
        [Operation("测试表单")]
        public JsonResult TestForm(FormCollection collection)
        {
            string tableName = collection[0];
            List<string> columns = new List<string>();
            List<string> values = new List<string>();
            for (int i = 1; i < collection.Count; i++)
            {
                string key = collection.AllKeys[i];
                columns.Add(key);
                string val = collection[i];
                values.Add(val);
            }
            var result = new ResultBase();
            result.success = _formDesignerService.InertFormDataAsync(tableName, columns, values);
            return Json(result);
        }
    }
}