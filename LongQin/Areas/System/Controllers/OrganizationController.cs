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
    public class OrganizationController : ControllerBase
    {
        IOrganizationService _organizationService = AutofacService.Resolve<IOrganizationService>();

        public ActionResult Index()
        {
            return View();
        }

        public string GetOrganizationList(int page, int limit)
        {
            var list = _organizationService.GetListAsync(page, limit);
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

        public string GetAllOrganization()
        {
            var list = _organizationService.GetAllListAsync();
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
            Organization model = new Organization();
            if (id != 0)
            {
                model = _organizationService.GetByIdAsync(id);
            }
            return View(model);
        }

        [HttpPost]
        [Operation("设置公司")]
        public JsonResult Set(Organization model)
        {
            if (model == null)
            {
                return Error("参数错误");
            }

            var fileLogo = Request.Files["fileLogo"];

            if (fileLogo != null)
            {
                string uploadResult = UploadHelper.Process(fileLogo.FileName, fileLogo.InputStream);
                if (!string.IsNullOrEmpty(uploadResult))
                {
                    model.LogoPath = uploadResult;
                }
            }
            var result = new ResultBase();
            if (model.OrganizationId > 0)
            {
                result.success = _organizationService.UpdateAsync(model);
            }
            else
            {
                result.success = _organizationService.InsertAsync(model) > 0 ? true : false;
            }

            return Json(result);
        }

        [HttpPost]
        [Operation("删除公司")]
        public JsonResult Delete(int id)
        {
            try
            {
                return Json(new ResultBase
                {
                    success = _organizationService.DeleteAsync(id)
                });
            }
            catch (Exception ex)
            {
                LogHelper.Error("OrganizationController.Delete异常", ex);
                return Error(ex.Message);
            }
        }

        public ActionResult BaseSet()
        {
            Organization model;
            int organizationId = LoginUser.OrganizationId;
            if (organizationId == 0)
            {
                model = new Organization();
            }
            else
            {
                model = _organizationService.GetByIdAsync(organizationId);
            }
            return View(model);
        }
    }
}