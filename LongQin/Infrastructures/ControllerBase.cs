using LongQin.Attributes;
using LongQin.Models;
using LongQin.Service;
using LongQin.Service.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Security;
using System.Web;
using System.Web.Mvc;

namespace LongQin
{
    [Exception]
    public class ControllerBase : Controller
    {
        IMenuService _menuService = AutofacService.Resolve<IMenuService>();

        public LoginUser LoginUser
        {
            get
            {
                return CookieData.CurrentUser;
            }
        }

        public IList<IDisposable> DisposableObjects { get; private set; }

        public ControllerBase()
        {
            this.DisposableObjects = new List<IDisposable>();
            int userId = 0;
            int organizationId = 0;
            string userName = "";
            string avatar = "";
            string organizationName = "";
            string logoPath = "";
            string systemName = "";
            if (CookieData.CurrentUser != null)
            {
                userId = CookieData.CurrentUser.UserId;
                organizationId = CookieData.CurrentUser.OrganizationId;
                userName = CookieData.CurrentUser.NickName;
                avatar = CookieData.CurrentUser.Avatar;
                organizationName = CookieData.CurrentUser.OrganizationName;
                logoPath = CookieData.CurrentUser.LogoPath;
                systemName = CookieData.CurrentUser.SystemName;
            }
            ViewBag.MenuList = _menuService.GetChildrenListAsync(0, userId, organizationId);
            ViewBag.UserName = userName;
            ViewBag.Avatar = avatar;
            ViewBag.OrganizationName = organizationName;
            ViewBag.LogoPath = String.IsNullOrEmpty(logoPath) ? "/lq.png" : logoPath;
            ViewBag.SystemName = String.IsNullOrEmpty(systemName) ? "龙琴工作流" : systemName;
        }

        protected void AddDisposableObject(object obj)
        {
            var disposable = obj as IDisposable;
            if (disposable != null)
            {
                this.DisposableObjects.Add(disposable);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                foreach (IDisposable obj in this.DisposableObjects)
                {
                    if (obj != null)
                    {
                        obj.Dispose();
                    }
                }
            }
            base.Dispose(disposing);
        }

        protected JsonResult Success(string msg = null, object data = null)
        {
            return new JsonResult
            {
                Data = new ResultBase
                {
                    success = true,
                    message = msg,
                    data = data
                }
            };
        }

        protected JsonResult Error(string msg = null, object data = null)
        {
            return new JsonResult
            {
                Data = new ResultBase
                {
                    success = false,
                    message = msg,
                    data = data
                }
            };
        }
    }
}