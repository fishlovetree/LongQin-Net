using LongQin.Attributes;
using LongQin.Models;
using LongQin.Service;
using LongQin.Service.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LongQin.Controllers
{
    [CheckLogin]
    public class HomeController : ControllerBase
    {
        IUserService _userService = AutofacService.Resolve<IUserService>();
        IMenuService _menuService = AutofacService.Resolve<IMenuService>();

        public ActionResult Index()
        {
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
            return View();
        }
    }
}