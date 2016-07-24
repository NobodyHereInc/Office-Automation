using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Model.SearchParam
{
    /// <summary>
    /// 
    /// </summary>
    public class BaseParam
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }

    }
}
