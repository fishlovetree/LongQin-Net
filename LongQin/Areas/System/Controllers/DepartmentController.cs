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
    public class DepartmentController : ControllerBase
    {
        IDepartmentService _departmentService = AutofacService.Resolve<IDepartmentService>();

        // GET: System/Department
        public ActionResult Index()
        {
            return View();
        }

        public string GetDepartmentList(int organizationId = 0)
        {
            organizationId = organizationId == 0 ? LoginUser.OrganizationId : organizationId;
            var list = _departmentService.GetListAsync(organizationId);
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string str = serializer.Serialize(new
            {
                code = 0,
                msg = "",
                data = list
            });
            return str;
        }

        public string GetDepartmentTree(int id = 0, int organizationId = 0)
        {
            organizationId = organizationId == 0 ? LoginUser.OrganizationId : organizationId;
            object obj = _departmentService.GetTreeAsync(id, organizationId);
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string str = serializer.Serialize(obj);
            return str;
        }

        public ActionResult Add(int id)
        {
            Department model = new Department();
            if (id != 0)
            {
                model = _departmentService.GetByIdAsync(id);
            }
            ViewBag.OrganizationId = LoginUser.OrganizationId;
            return View(model);
        }

        [HttpPost]
        [Operation("设置部门")]
        public JsonResult Set(Department model)
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
            if (model.DepartmentId > 0)
            {
                result.success = _departmentService.UpdateAsync(model);
            }
            else
            {
                result.success = _departmentService.InsertAsync(model);
            }

            return Json(result);
        }

        [HttpPost]
        [Operation("删除部门")]
        public JsonResult Delete(int id)
        {
            try
            {
                return Json(new ResultBase
                {
                    success = _departmentService.DeleteAsync(id)
                });
            }
            catch (Exception ex)
            {
                LogHelper.Error("DepartmentController.Delete异常", ex);
                return Error(ex.Message);
            }
        }
    }
}