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
    public class UserController : ControllerBase
    {
        IUserService _userService = AutofacService.Resolve<IUserService>();
        IMenuService _menuService = AutofacService.Resolve<IMenuService>();

        public ActionResult Index()
        {
            return View();
        }

        public string GetUserList(int page, int limit, string departmentId, string nickName, int organizationId = 0)
        {
            organizationId = organizationId == 0 ? LoginUser.OrganizationId : organizationId;
            var list = _userService.GetListAsync(page, limit, departmentId, nickName, organizationId);
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
            User model = new User();
            if (id != 0)
            {
                model = _userService.GetByIdAsync(id);
            }
            ViewBag.OrganizationId = LoginUser.OrganizationId;
            return View(model);
        }

        [HttpPost]
        [Operation("设置用户")]
        public JsonResult Set(User model)
        {
            if (model == null)
            {
                return Error("参数错误");
            }

            if (model.OrganizationId == 0)
            {
                model.OrganizationId = LoginUser.OrganizationId;
            }

            var fileAvatar = Request.Files["fileAvatar"];

            if (fileAvatar != null)
            {
                string uploadResult = UploadHelper.Process(fileAvatar.FileName, fileAvatar.InputStream);
                if (!string.IsNullOrEmpty(uploadResult))
                {
                    model.Avatar = uploadResult;
                }
            }
            if (model.UserId > 0)
            {
                int count = _userService.GetCountByNameAsync(model.UserName, model.UserId);
                if (count > 0)
                {
                    return Error("账号名已存在");
                }
                else 
                {
                    bool result = _userService.UpdateAsync(model);
                    if (result) return Success();
                    else return Error();
                }
            }
            else
            {
                int count = _userService.GetCountByNameAsync(model.UserName, 0);
                if (count > 0)
                {
                    return Error("账号名已存在");
                }
                else
                {
                    bool result = _userService.InsertAsync(model) > 0 ? true : false;
                    if (result) return Success();
                    else return Error();
                }
            }
        }

        [HttpPost]
        [Operation("删除用户")]
        public JsonResult Delete(int id)
        {
            try
            {
                return Json(new ResultBase
                {
                    success = _userService.DeleteAsync(id)
                });
            }
            catch (Exception ex)
            {
                LogHelper.Error("UserController.Delete异常", ex);
                return Error(ex.Message);
            }
        }

        [HttpPost]
        [Operation("修改用户密码")]
        public JsonResult UpdatePassword(int userId, string oldPwd, string newPwd)
        {
            User user = _userService.GetByIdAsync(userId);
            if (user == null || Md5Helper.Encrypt(oldPwd) != user.Password)
            {
                return Error("旧密码错误");
            }
            bool result = _userService.UpdatePasswordAsync(userId, newPwd);

            if (result)
            {
                Session[WebAppSettings.SessionName] = null;
                CookieHelper.Clear(WebAppSettings.CookieName);
            }

            return Json(new ResultBase
            {
                success = result
            });
        }

        public ActionResult UpdatePwd()
        {
            int id = LoginUser.UserId;
            ViewBag.Id = id;
            return View();
        }

        public ActionResult SetRole(int id)
        {
            ViewBag.userId = id;
            List<int> roleIdList = _userService.GetRoles(id);
            ViewBag.roleIds = string.Join(",", roleIdList);
            return View();
        }

        [HttpPost]
        [Operation("设置用户角色")]
        public JsonResult SetRole(int userId, string roleIds)
        {
            return Json(new ResultBase
            {
                success = _userService.SetRoleAsync(userId, roleIds)
            });
        }

        public ActionResult BaseSet()
        {
            int userId = LoginUser.UserId;
            User model = _userService.GetByIdAsync(userId);
            return View(model);
        }
    }
}