using LongQin.Models;
using LongQin.Service;
using LongQin.Service.Base;
using LongQin.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace LongQin
{
    public class OperationAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// 方法名称
        /// </summary>
        public string ActionName
        {
            get;
            set;
        }

        /// <summary>
        /// 控制器名称
        /// </summary>
        public string ControllerName
        {
            get;
            set;
        }

        /// <summary>
        /// 方法参数
        /// </summary>
        public string ActionParameters
        {
            get;
            set;
        }

        /// <summary>
        /// 操作内容
        /// </summary>
        public string Title
        {
            get;
            set;
        }

        /// <summary>
        /// 操作备注
        /// </summary>
        public string Remark
        {
            get;
            set;
        }

        /// <summary>
        /// 操作日志
        /// </summary>
        /// <param name="title">操作内容</param>
        /// <param name="remark">备注</param>
        public OperationAttribute(string title, string remark = null)
        {
            this.Title = title;
            this.Remark = remark;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            this.ActionName = filterContext.ActionDescriptor.ActionName;
            this.ControllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            IDictionary<string, object> dic = filterContext.ActionParameters;
            string extra = string.Empty;

            if(dic != null && dic.Count > 0)
            {
                this.ActionParameters = JsonConvert.SerializeObject(dic);
                try
                {
                    string pattern = "\"Id\":(\\d+),";
                    if (Regex.IsMatch(this.ActionParameters, pattern))
                    {
                        var match = Regex.Match(this.ActionParameters, pattern);
                        int id = Int32.Parse(match.Groups[1].Value);
                        if (id > 0)
                        {
                            extra = "[修改]";
                        }
                        else
                        {
                            extra = "[添加]";
                        }
                    }
                }
                catch
                {
                }
            }

            AutofacService.Resolve<ILogService>().InsertAsync(new Log
            {
                Title = this.Title + extra,
                Remark = this.Remark,
                ActionName = this.ActionName,
                ControllerName = this.ControllerName,
                ActionParameters = this.ActionParameters,
                CreateTime = DateTime.Now,
                UserId = CookieData.CurrentUser == null ? 0 : CookieData.CurrentUser.UserId,
                UserName = CookieData.CurrentUser == null ? "" : CookieData.CurrentUser.UserName,
                OrganizationId = CookieData.CurrentUser == null ? 0 : CookieData.CurrentUser.OrganizationId
            });
        }
    }
}