using LongQin.Attributes;
using LongQin.Common;
using LongQin.Configs;
using LongQin.Models;
using LongQin.Service;
using LongQin.Service.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace LongQin.Areas.System.Controllers
{
    [CheckLogin]
    public class MenuController : ControllerBase
    {
        IMenuService _menuService = AutofacService.Resolve<IMenuService>();

        // GET: System/Menu
        public ActionResult Index()
        {
            return View();
        }

        public string GetMenuList()
        {
            var list = _menuService.GetListAsync();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string str = serializer.Serialize(new
            {
                code = 0,
                msg = "",
                data = list
            });
            return str;
        }

        public string GetMenuTree(int id = 0)
        {
            object obj = _menuService.GetTreeAsync(id);
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string str = serializer.Serialize(obj);
            return str;
        }

        public ActionResult Add(int id)
        {
            Menu model = new Menu();
            if (id != 0)
            {
                model = _menuService.GetByIdAsync(id);
            }
            return View(model);
        }

        [HttpPost]
        [Operation("设置菜单")]
        public JsonResult Set(Menu model)
        {
            if (model == null)
            {
                return Error("参数错误");
            }

            model.Creator = 1;
            model.OrganizationId = LoginUser.OrganizationId;
            var result = new ResultBase();
            if (model.MenuId > 0)
            {
                result.success = _menuService.UpdateAsync(model);
            }
            else
            {
                result.success = _menuService.InsertAsync(model);
            }

            return Json(result);
        }

        [HttpPost]
        [Operation("删除菜单")]
        public JsonResult Delete(int id)
        {
            try
            {
                return Json(new ResultBase
                {
                    success = _menuService.DeleteAsync(id)
                });
            }
            catch (Exception ex)
            {
                LogHelper.Error("MenuController.Delete异常", ex);
                return Error(ex.Message);
            }
        }

        public string GetUserMenuTree()
        {
            object obj = _menuService.GetUserMenuTreeAsync(LoginUser.UserId, LoginUser.OrganizationId);
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string str = serializer.Serialize(obj);
            return str;
        }
    }
}