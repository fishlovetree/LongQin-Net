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
    public class LogService : ServiceBase, ILogService
    {
        ILogRepository _logRepository = AutofacRepository.Resolve<ILogRepository>();

        public LogService()
        {
            base.AddDisposableObject(_logRepository);
        }

        public Log GetByIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("id错误");
            }

            return _logRepository.GetByIdAsync(id);
        }

        public Log GetById(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("id错误");
            }

            return _logRepository.GetById(id);
        }

        public PageModel<Log> GetListAsync(string beginDate, string endDate, int pageIndex, int pageSize, int organizationId)
        {
            return _logRepository.GetListAsync(beginDate, endDate, pageIndex, pageSize, organizationId);
        }

        public bool InsertAsync(Log model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("model不能为null");
            }

            if (string.IsNullOrEmpty(model.Title))
            {
                throw new ArgumentNullException("Title错误");
            }

            return _logRepository.InsertAsync(model);
        }
    }
}
