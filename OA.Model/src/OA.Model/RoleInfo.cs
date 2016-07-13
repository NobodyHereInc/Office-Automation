using System;
using System.Collections.Generic;

namespace OA.Model
{
    public partial class RoleInfo
    {
        public RoleInfo()
        {
            RoleInfoActionInfo = new HashSet<RoleInfoActionInfo>();
            UserInfoRoleInfo = new HashSet<UserInfoRoleInfo>();
        }

        public int Id { get; set; }
        public string RoleName { get; set; }
        public short DelFlag { get; set; }
        public DateTime SubTime { get; set; }
        public string Remark { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string Sort { get; set; }

        public virtual ICollection<RoleInfoActionInfo> RoleInfoActionInfo { get; set; }
        public virtual ICollection<UserInfoRoleInfo> UserInfoRoleInfo { get; set; }
    }
}
