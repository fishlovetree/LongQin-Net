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
    public class PositionController : ControllerBase
    {
        IPositionService _positionService = AutofacService.Resolve<IPositionService>();

        // GET: System/Position
        public ActionResult Index()
        {
            return View();
        }

        public string GetPositionList(int organizationId = 0)
        {
            organizationId = organizationId == 0 ? LoginUser.OrganizationId : organizationId;
            var list = _positionService.GetListAsync(organizationId);
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string str = serializer.Serialize(new
            {
                code = 0,
                msg = "",
                data = list
            });
            return str;
        }

        public string GetPositionTree(int id = 0, int organizationId = 0)
        {
            organizationId = organizationId == 0 ? LoginUser.OrganizationId : organizationId;
            object obj = _positionService.GetTreeAsync(id, organizationId);
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string str = serializer.Serialize(obj);
            return str;
        }

        public ActionResult Add(int id)
        {
            Position model = new Position();
            if (id != 0)
            {
                model = _positionService.GetByIdAsync(id);
            }
            ViewBag.OrganizationId = LoginUser.OrganizationId;
            return View(model);
        }

        [HttpPost]
        [Operation("设置职位")]
        public JsonResult Set(Position model)
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
            if (model.PositionId > 0)
            {
                result.success = _positionService.UpdateAsync(model);
            }
            else
            {
                result.success = _positionService.InsertAsync(model);
            }

            return Json(result);
        }

        [HttpPost]
        [Operation("删除职位")]
        public JsonResult Delete(int id)
        {
            try
            {
                return Json(new ResultBase
                {
                    success = _positionService.DeleteAsync(id)
                });
            }
            catch (Exception ex)
            {
                LogHelper.Error("PositionController.Delete异常", ex);
                return Error(ex.Message);
            }
        }
    }
}