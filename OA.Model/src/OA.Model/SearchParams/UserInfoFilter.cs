using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OA.Model.SearchParams
{
    /// <summary>
    /// Search conditions.
    /// </summary>
    public class UserInfoFilter : BaseParam
    {
        public String Uname { get; set; }
        public String Uremark { get; set; }
    }
}
