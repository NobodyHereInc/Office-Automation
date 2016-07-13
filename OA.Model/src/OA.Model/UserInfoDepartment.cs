using System;
using System.Collections.Generic;

namespace OA.Model
{
    public partial class UserInfoDepartment
    {
        public int DepartmentId { get; set; }
        public int UserInfoId { get; set; }

        public virtual Department Department { get; set; }
        public virtual UserInfo UserInfo { get; set; }
    }
}
