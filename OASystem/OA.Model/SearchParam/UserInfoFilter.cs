using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Model.SearchParam
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
