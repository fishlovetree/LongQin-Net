using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using LongQin.Models;
using LongQin.Service;
using LongQin.Service.Base;
using LongQin.Common;
using LongQin.Configs;
using LongQin.Infrastructures;
using System.Web.Security;

namespace LongQin.Controllers
{
    public class AccountController : ControllerBase
    {
        IUserService _userService = AutofacService.Resolve<IUserService>();
        IOrganizationService _organizationService = AutofacService.Resolve<IOrganizationService>();

        public AccountController()
        {
            base.AddDisposableObject(_userService);
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [Operation("登录系统")]
        public JsonResult Login(string username, string password, bool autoLogin = false)
        {
            if (string.IsNullOrEmpty(username))
            {
                return Error("用户名不能为空");
            }

            if (string.IsNullOrEmpty(password))
            {
                return Error("密码不能为空");
            }

            var user = _userService.GetByNameAsync(username.Trim());

            if (user == null)
            {
                return Error("用户不存在");
            }

            if (Md5Helper.Encrypt(password) != user.Password.Trim())
            {
                return Error("密码错误");
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
            Session[WebAppSettings.SessionName] = loginUser;

            if (autoLogin)
            {
                string encryptStr = DesHelper.Encrypt(user.UserId.ToString(), WebAppSettings.DesEncryptKey);
                CookieHelper.Set(WebAppSettings.CookieName, encryptStr, DateTime.Now.AddDays(3));
            }

            return Success();
        }

        [HttpPost]
        [Operation("退出系统")]
        public void Quit()
        {
            Session[WebAppSettings.SessionName] = null;
            CookieHelper.Clear(WebAppSettings.CookieName);
        }

        [HttpPost]
        [Operation("发送注册验证码")]
        public JsonResult SendCode(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return Error("邮箱不能为空");
            }

            Tuple<bool, string> sendResult = VerifyCode.SendCode(email);
            if (sendResult.Item1)
            {
                return Success();
            }
            else 
            {
                return Error();
            }
        }

        [HttpPost]
        [Operation("用户注册")]
        public JsonResult Register(string email, string code, string pwd, string nickName, string company, string phone)
        {
            if (string.IsNullOrEmpty(email))
            {
                return Error("邮箱不能为空");
            }

            string realCode = VerifyCode.GetVerifyCode(email);
            if (realCode != code) 
            {
                return Error("验证码错误");
            }
            int count = _userService.GetCountByNameAsync(email, 0);
            if (count > 0)
            {
                return Error("邮箱已被注册");
            }
            Organization org = new Organization();
            org.OrganizationName = company;
            org.Phone = phone;
            int orgId = _organizationService.InsertAsync(org);
            if (orgId > 0)
            {
                User user = new User();
                user.UserName = email;
                user.NickName = nickName;
                user.Password = pwd;
                user.Email = email;
                user.Phone = phone;
                user.OrganizationId = orgId;
                user.CreateTime = DateTime.Now;
                int userId = _userService.InsertAsync(user);
                if (userId > 0)
                {
                    _userService.SetRoleAsync(userId, "1"); //默认管理员角色
                    return Success();
                }
                else 
                {
                    return Error();
                }
            }
            else 
            {
                return Error();
            }
        }
    }
}