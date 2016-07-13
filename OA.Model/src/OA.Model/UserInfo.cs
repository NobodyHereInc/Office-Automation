using System;
using System.Collections.Generic;

namespace OA.Model
{
    public partial class UserInfo
    {
        public UserInfo()
        {
            RUserInfoActionInfo = new HashSet<RUserInfoActionInfo>();
            UserInfoDepartment = new HashSet<UserInfoDepartment>();
            UserInfoRoleInfo = new HashSet<UserInfoRoleInfo>();
        }

        public int Id { get; set; }
        public string Uname { get; set; }
        public string Upwd { get; set; }
        public DateTime SubTime { get; set; }
        public short DelFlag { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string Remark { get; set; }
        public string Sort { get; set; }

        public virtual ICollection<RUserInfoActionInfo> RUserInfoActionInfo { get; set; }
        public virtual ICollection<UserInfoDepartment> UserInfoDepartment { get; set; }
        public virtual ICollection<UserInfoRoleInfo> UserInfoRoleInfo { get; set; }
    }
}
