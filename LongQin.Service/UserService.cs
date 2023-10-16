using LongQin.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LongQin.Models;
using LongQin.Common;

namespace LongQin.Service
{
    public class UserService : ServiceBase, IUserService
    {
        IUserRepository _userRepository = AutofacRepository.Resolve<IUserRepository>();

        public UserService()
        {
            base.AddDisposableObject(_userRepository);
        }

        public User GetByIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("id错误");
            }

            return _userRepository.GetByIdAsync(id);
        }

        public User GetById(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("id错误");
            }

            return _userRepository.GetById(id);
        }

        public User GetByNameAsync(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("name不能为null");
            }

            return _userRepository.GetByNameAsync(name);
        }

        public int GetCountByNameAsync(string name, int id)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("name不能为null");
            }

            return _userRepository.GetCountByNameAsync(name, id);
        }

        public PageModel<User> GetListAsync(int pageIndex, int pageSize, string departmentId, string nickName, int organizationId)
        {
            return _userRepository.GetListAsync(pageIndex, pageSize, departmentId, nickName, organizationId);
        }

        public bool DeleteAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("id错误");
            }

            return _userRepository.DeleteAsync(id);
        }

        public bool UpdatePasswordAsync(int id, string password)
        {
            if (id <= 0)
            {
                throw new ArgumentException("id错误");
            }

            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentNullException("password不能为空");
            }

            string encryptPwd = Md5Helper.Encrypt(password);

            return _userRepository.UpdatePasswordAsync(id, encryptPwd);
        }

        public int InsertAsync(User model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("model不能为null");
            }

            if (string.IsNullOrEmpty(model.UserName))
            {
                throw new ArgumentNullException("UserName错误");
            }

            model.Password = Md5Helper.Encrypt(model.UserName);
            model.CreateTime = DateTime.Now;

            return _userRepository.InsertAsync(model);
        }

        public bool UpdateAsync(User model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("model不能为null");
            }

            if (string.IsNullOrEmpty(model.UserName))
            {
                throw new ArgumentNullException("UserName错误");
            }

            return _userRepository.UpdateAsync(model);
        }

        public List<int> GetRoles(int userId)
        {
            return _userRepository.GetUserRoleAsync(userId);
        }

        public bool SetRoleAsync(int userId, string roleIds)
        {
            if (userId <= 0)
            {
                throw new ArgumentException("id错误");
            }

            return _userRepository.SetRoleAsync(userId, roleIds);
        }
    }
}
