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
using System.Web.ApplicationServices;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace LongQin.Areas.System.Controllers
{
    [CheckLogin]
    public class NoticeController : ControllerBase
    {
        INoticeService _noticeService = AutofacService.Resolve<INoticeService>();

        public ActionResult Index()
        {
            return View();
        }

        public string GetNoticeList(int page, int limit, string title, string startDate, string endDate)
        {
            var list = _noticeService.GetListAsync(page, limit, title, startDate, endDate, LoginUser.OrganizationId);
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

        public ActionResult Add(int id)
        {
            Notice model = new Notice();
            if (id != 0)
            {
                model = _noticeService.GetByIdAsync(id);
            }
            return View(model);
        }

        [HttpPost]
        [Operation("发布公告")]
        [ValidateInput(false)]
        public JsonResult Set(Notice model)
        {
            if (model == null)
            {
                return Error("参数错误");
            }

            if (model.OrganizationId == 0)
            {
                model.OrganizationId = LoginUser.OrganizationId;
            }

            if (model.NoticeId > 0)
            {
                bool result = _noticeService.UpdateAsync(model);
                if (result) 
                {
                    if (!String.IsNullOrEmpty(model.Attachments)) 
                    {
                        _noticeService.SetNoticeFilesAsync(model.NoticeId, model.Attachments);
                    }
                    return Success();
                } 
                else return Error();
            }
            else
            {
                int noticeId = _noticeService.InsertAsync(model);
                if (noticeId > 0)
                {
                    if (!String.IsNullOrEmpty(model.Attachments))
                    {
                        _noticeService.SetNoticeFilesAsync(noticeId, model.Attachments);
                    }
                    return Success();
                }
                else return Error();
            }
        }

        [HttpPost]
        [Operation("删除公告")]
        public JsonResult Delete(int id)
        {
            try
            {
                return Json(new ResultBase
                {
                    success = _noticeService.DeleteAsync(id)
                });
            }
            catch (Exception ex)
            {
                LogHelper.Error("NoticeController.Delete异常", ex);
                return Error(ex.Message);
            }
        }

        [HttpPost]
        [Operation("上传文件")]
        public JsonResult UploadFiles()
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

        public ActionResult Details(int id)
        {
            Notice model = _noticeService.GetByIdAsync(id);
            return View(model);
        }
    }
}