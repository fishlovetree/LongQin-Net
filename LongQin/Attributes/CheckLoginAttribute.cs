using LongQin.Configs;
using LongQin.Models;
using LongQin.Service;
using LongQin.Service.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace LongQin.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class CheckLoginAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var session = filterContext.HttpContext.Session[WebAppSettings.SessionName];
            if (session == null)
            {
                var cookie = filterContext.HttpContext.Request.Cookies[WebAppSettings.CookieName];
                if (cookie == null)
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                    {
                        controller = "account",
                        action = "login",
                        area = ""
                    }));
                    return;
                }

                var userId = Int32.Parse(Common.DesHelper.Decrypt(cookie.Value, WebAppSettings.DesEncryptKey));
                if (userId == 0)
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                    {
                        controller = "account",
                        action = "login",
                        area = ""
                    }));
                    return;
                }

                var user = AutofacService.Resolve<IUserService>().GetById(userId);
                if (user == null)
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                    {
                        controller = "account",
                        action = "login",
                        area = ""
                    }));
                    return;
                }

                var loginUser = new LoginUser
                {
                    UserId = user.UserId,
                    UserName = user.UserName,
                    NickName = user.NickName,
                    Avatar = user.Avatar,
                    OrganizationId = user.OrganizationId,
                    OrganizationName = user.OrganizationName,
                    LogoPath = user.LogoPath,
                    SystemName = string.IsNullOrEmpty(user.SystemName) ? user.OrganizationName : user.SystemName
                };

                CookieData.CurrentUser = loginUser;
                filterContext.HttpContext.Session[WebAppSettings.SessionName] = loginUser;
            }
        }
    }
}