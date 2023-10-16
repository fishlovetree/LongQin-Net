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
using System.Web.Security;

namespace LongQin.Areas.System.Controllers
{
    [CheckLogin]
    public class RoleController : ControllerBase
    {
        IRoleService _roleService = AutofacService.Resolve<IRoleService>();

        // GET: System/Menu
        public ActionResult Index()
        {
            return View();
        }

        public string GetRoleList(int organizationId = 0)
        {
            organizationId = organizationId == 0 ? LoginUser.OrganizationId : organizationId;
            var list = _roleService.GetListAsync(organizationId);
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string str = serializer.Serialize(new
            {
                code = 0,
                msg = "",
                data = list
            });
            return str;
        }

        public ActionResult Add(int id)
        {
            Role model = new Role();
            if (id != 0)
            {
                model = _roleService.GetByIdAsync(id);
            }
            ViewBag.OrganizationId = LoginUser.OrganizationId;
            return View(model);
        }

        [HttpPost]
        [Operation("设置角色")]
        public JsonResult Set(Role model)
        {
            if (model == null)
            {
                return Error("参数错误");
            }

            if (model.OrganizationId == 0)
            {
                model.OrganizationId = LoginUser.OrganizationId;
            }

            var result = new ResultBase();
            if (model.RoleId > 0)
            {
                result.success = _roleService.UpdateAsync(model);
            }
            else
            {
                result.success = _roleService.InsertAsync(model);
            }

            return Json(result);
        }

        [HttpPost]
        [Operation("删除角色")]
        public JsonResult Delete(int id)
        {
            try
            {
                return Json(new ResultBase
                {
                    success = _roleService.DeleteAsync(id)
                });
            }
            catch (Exception ex)
            {
                LogHelper.Error("RoleController.Delete异常", ex);
                return Error(ex.Message);
            }
        }

        public ActionResult SetMenu(int id)
        {
            ViewBag.roleId = id;
            List<int> menuIdList = _roleService.GetMenus(id);
            ViewBag.menuIds = string.Join(",", menuIdList);
            return View();
        }

        public string GetMenuButtons(int roleId)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.Serialize(_roleService.GetMenus(roleId));
        }

        [HttpPost]
        [Operation("设置角色功能菜单")]
        public JsonResult SetMenu(int roleId, string menuIds)
        {
            return Json(new ResultBase
            {
                success = _roleService.SetMenuAsync(roleId, menuIds)
            });
        }
    }
}