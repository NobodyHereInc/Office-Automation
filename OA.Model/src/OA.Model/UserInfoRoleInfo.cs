using System;
using System.Collections.Generic;

namespace OA.Model
{
    public partial class UserInfoRoleInfo
    {
        public int RoleInfoId { get; set; }
        public int UserInfoId { get; set; }

        public virtual RoleInfo RoleInfo { get; set; }
        public virtual UserInfo UserInfo { get; set; }
    }
}
