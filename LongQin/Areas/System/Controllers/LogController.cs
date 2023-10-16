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
    public class LogController : ControllerBase
    {
        ILogService _logService = AutofacService.Resolve<ILogService>();

        public ActionResult Index()
        {
            return View();
        }

        public string GetLogList(string beginDate, string endDate, int page, int limit, int organizationId = 0)
        {
            organizationId = organizationId == 0 ? LoginUser.OrganizationId : organizationId;
            var list = _logService.GetListAsync(beginDate, endDate, page, limit, organizationId);
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
    }
}