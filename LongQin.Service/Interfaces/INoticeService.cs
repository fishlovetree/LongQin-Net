using LongQin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LongQin.Service
{
    public interface INoticeService
    {
        Notice GetByIdAsync(int id);

        PageModel<Notice> GetListAsync(int pageIndex, int pageSize, string title, string startDate, string endDate, int organizationId);

        bool DeleteAsync(int id);

        int InsertAsync(Notice model);

        bool UpdateAsync(Notice model);

        List<string> GetNoticeFilesAsync(int noticeId);

        bool SetNoticeFilesAsync(int noticeId, string files);
    }
}
