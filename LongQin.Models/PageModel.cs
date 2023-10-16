using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LongQin.Models
{
    public class PageModel<T> where T : class
    {
        public List<T> Data { get; set; }

        public int Total { get; set; }
    }
}
