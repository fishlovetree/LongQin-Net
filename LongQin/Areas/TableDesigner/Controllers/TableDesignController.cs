using LongQin.Attributes;
using LongQin.Common;
using LongQin.Configs;
using LongQin.Models;
using LongQin.Service;
using LongQin.Service.Base;
using Microsoft.Owin;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace LongQin.Areas.TableDesigner.Controllers
{
    [CheckLogin]
    public class TableDesignController : ControllerBase
    {
        ITableDesignerService _tableDesignerService = AutofacService.Resolve<ITableDesignerService>();
        IFormDesignerService _formDesignerService = AutofacService.Resolve<IFormDesignerService>();
        IMenuService _menuService = AutofacService.Resolve<IMenuService>();

        public ActionResult Index(int id = 0)
        {
            LongQin.Models.TableDesigner model = new LongQin.Models.TableDesigner();
            if (id != 0) 
            {
                model = _tableDesignerService.GetByIdAsync(id);
                List<TableDesignerColumn> columns = _tableDesignerService.GetTableColumnsAsync(id);
                model.Columns = columns;
            }
            return View(model);
        }



        // 获取数据库业务表集合
        public string GetFormTableList(int page, int limit)
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

        // 获取数据库表字段集合
        public string GetTableColumnList(string tableName)
        {
            var list = _formDesignerService.GetTableColumnsAsync(tableName);
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string str = serializer.Serialize(new
            {
                code = 0,
                msg = "",
                data = list
            });
            return str;
        }

        public string GetTablePreview(int page, int limit)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string str = serializer.Serialize(new
            {
                code = 0,
                msg = "",
                count = 0,
                data = new List<string>()
            });
            return str;
        }

        [HttpPost]
        [Operation("保存列表")]
        public JsonResult Save(int id, string tableName, string dataSource, string data)
        {
            if (String.IsNullOrEmpty(tableName) || String.IsNullOrEmpty(dataSource))
            {
                return Error("参数错误");
            }
            LongQin.Models.TableDesigner model = new LongQin.Models.TableDesigner();
            model.Id = id;
            model.TableName = tableName;
            model.DataSource = dataSource;
            model.Creator = LoginUser.UserId;
            model.OrganizationId = LoginUser.OrganizationId;

            var result = new ResultBase();
            using (TransactionScope scope = new TransactionScope()) // 开启事务
            {
                if (model.Id == 0)
                {
                    model.Id = _tableDesignerService.InsertAsync(model);
                    result.success = model.Id > 0 ? true : false;
                }
                else
                {
                    result.success = _tableDesignerService.UpdateAsync(model);
                }
                if (result.success)
                {
                    // 插入列表列属性
                    List<TableDesignerColumn> list = new List<TableDesignerColumn>();
                    JArray jArray = (JArray)JsonConvert.DeserializeObject(data);
                    int length = jArray.Count;
                    for (int i = 0; i < jArray.Count; i++)
                    {
                        TableDesignerColumn column = new TableDesignerColumn();
                        column.TableColumn = jArray[i]["TableColumn"].ToString();
                        column.ColumnName = jArray[i]["ColumnName"].ToString();
                        column.ColumnIndex = i;
                        column.Width = jArray[i]["Width"].ToInt32();
                        column.Orderby = jArray[i]["Orderby"].ToInt32();
                        column.SearchType = jArray[i]["SearchType"].ToInt32();
                        column.Formula = jArray[i]["Formula"].ToInt32();
                        column.FormulaValue = jArray[i]["FormulaValue"].ToString();
                        column.ColumnType = jArray[i]["ColumnType"].ToString();
                        list.Add(column);
                    }
                    result.success = _tableDesignerService.InsertColumnsAsync(model.Id, list);

                    // 更新菜单
                    Menu menu = new Menu();
                    menu.MenuName = tableName;
                    menu.ParentId = 23; // 父级为自定义列表
                    menu.MenuUrl = "/tabledesigner/tabledesign/customerview/" + model.Id;
                    menu.Controller = "tabledesign";
                    menu.Action = "customerview";
                    menu.GroupSeq = model.Id + 2;
                    menu.Creator = LoginUser.UserId;
                    menu.OrganizationId = LoginUser.OrganizationId;
                    if (id == 0)
                    {
                        result.success = _menuService.InsertAsync(menu);
                    }
                    else
                    {
                        result.success = _menuService.UpdateByUrlAsync(menu);
                    }
                }
                if (result.success)
                {
                    scope.Complete();
                }
            }
            return Json(result);
        }

        public ActionResult CustomerView(int id = 0)
        {
            LongQin.Models.TableDesigner model = new LongQin.Models.TableDesigner();
            if (id != 0)
            {
                model = _tableDesignerService.GetByIdAsync(id);
            }
            return View(model);
        }

        // 获取自定义列表表头
        public string GetTableDesignerColumnsAsync(int id)
        {
            var list = _tableDesignerService.GetTableColumnsAsync(id);
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string str = serializer.Serialize(new
            {
                code = 0,
                msg = "",
                data = list
            });
            return str;
        }

        public string GetTableData(int page, int limit, int id, string dataSource)
        {
            // 获取检索条件
            var queryString = Request.QueryString;
            Dictionary<string, string> dic = new Dictionary<string, string>();
            for (int i = 0; i < queryString.Count; i++)
            {
                dic.Add(queryString.Keys[i].ToString(), queryString[i]);
            }
            var list = _tableDesignerService.GetTableData(page, limit, id, dataSource, LoginUser.OrganizationId, dic);
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

        public ActionResult List()
        {
            return View();
        }

        // 列表清单
        public string GetList(int page, int limit)
        {
            var list = _tableDesignerService.GetListAsync(page, limit, LoginUser.OrganizationId);
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
        [Operation("删除列表")]
        public JsonResult Delete(int id)
        {
            try
            {
                return Json(new ResultBase
                {
                    success = _tableDesignerService.DeleteAsync(id)
                });
            }
            catch (Exception ex)
            {
                LogHelper.Error("TableDesignController.Delete异常", ex);
                return Error(ex.Message);
            }
        }
    }
}