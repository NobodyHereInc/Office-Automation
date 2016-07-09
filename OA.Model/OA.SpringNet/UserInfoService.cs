using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.SpringNet
{
    class UserInfoService : IUserinfoService
    {
        public string Name { get; set; }
        public string ShowMessage()
        {
            return "HELLO WORLD" + Name;
        }
    }
}
