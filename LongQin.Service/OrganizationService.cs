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
    public class OrganizationService : ServiceBase, IOrganizationService
    {
        IOrganizationRepository _organizationRepository = AutofacRepository.Resolve<IOrganizationRepository>();

        public OrganizationService()
        {
            base.AddDisposableObject(_organizationRepository);
        }

        public Organization GetByIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("id错误");
            }

            return _organizationRepository.GetByIdAsync(id);
        }

        public Organization GetById(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("id错误");
            }

            return _organizationRepository.GetById(id);
        }

        public Organization GetByNameAsync(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("name不能为null");
            }

            return _organizationRepository.GetByNameAsync(name);
        }

        public PageModel<Organization> GetListAsync(int pageIndex, int pageSize)
        {
            return _organizationRepository.GetListAsync(pageIndex, pageSize);
        }

        public List<Organization> GetAllListAsync()
        {
            return _organizationRepository.GetAllListAsync();
        }

        public bool DeleteAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("id错误");
            }

            return _organizationRepository.DeleteAsync(id);
        }

        public int InsertAsync(Organization model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("model不能为null");
            }

            if (string.IsNullOrEmpty(model.OrganizationName))
            {
                throw new ArgumentNullException("OrganizationName错误");
            }
            return _organizationRepository.InsertAsync(model);
        }

        public bool UpdateAsync(Organization model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("model不能为null");
            }

            if (string.IsNullOrEmpty(model.OrganizationName))
            {
                throw new ArgumentNullException("OrganizationName错误");
            }
            return _organizationRepository.UpdateAsync(model);
        }
    }
}
