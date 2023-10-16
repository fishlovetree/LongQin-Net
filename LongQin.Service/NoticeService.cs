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
    public class NoticeService : ServiceBase, INoticeService
    {
        INoticeRepository _noticeRepository = AutofacRepository.Resolve<INoticeRepository>();

        public NoticeService()
        {
            base.AddDisposableObject(_noticeRepository);
        }

        public Notice GetByIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("id错误");
            }

            return _noticeRepository.GetByIdAsync(id);
        }

        public PageModel<Notice> GetListAsync(int pageIndex, int pageSize, string title, string startDate, string endDate, int organizationId)
        {
            return _noticeRepository.GetListAsync(pageIndex, pageSize, title, startDate, endDate, organizationId);
        }

        public bool DeleteAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("id错误");
            }

            return _noticeRepository.DeleteAsync(id);
        }

        public int InsertAsync(Notice model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("model不能为null");
            }

            if (string.IsNullOrEmpty(model.Title))
            {
                throw new ArgumentNullException("Title错误");
            }

            return _noticeRepository.InsertAsync(model);
        }

        public bool UpdateAsync(Notice model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("model不能为null");
            }

            if (string.IsNullOrEmpty(model.Title))
            {
                throw new ArgumentNullException("Title错误");
            }

            return _noticeRepository.UpdateAsync(model);
        }

        public List<string> GetNoticeFilesAsync(int noticeId)
        {
            return _noticeRepository.GetNoticeFilesAsync(noticeId);
        }

        public bool SetNoticeFilesAsync(int noticeId, string files)
        {
            if (noticeId <= 0)
            {
                throw new ArgumentException("id错误");
            }

            return _noticeRepository.SetNoticeFilesAsync(noticeId, files);
        }
    }
}
