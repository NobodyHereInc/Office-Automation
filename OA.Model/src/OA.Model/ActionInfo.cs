using System;
using System.Collections.Generic;

namespace OA.Model
{
    public partial class ActionInfo
    {
        public ActionInfo()
        {
            DepartmentActionInfo = new HashSet<DepartmentActionInfo>();
            RoleInfoActionInfo = new HashSet<RoleInfoActionInfo>();
            RUserInfoActionInfo = new HashSet<RUserInfoActionInfo>();
        }

        public int Id { get; set; }
        public DateTime SubTime { get; set; }
        public short DelFlag { get; set; }
        public string ModifiedOn { get; set; }
        public string Remark { get; set; }
        public string Url { get; set; }
        public string HttpMethod { get; set; }
        public string ActionMethodName { get; set; }
        public string ControllerName { get; set; }
        public string ActionInfoName { get; set; }
        public string Sort { get; set; }
        public short ActionTypeEnum { get; set; }
        public string MenuIcon { get; set; }
        public int IconWidth { get; set; }
        public int IconHeight { get; set; }

        public virtual ICollection<DepartmentActionInfo> DepartmentActionInfo { get; set; }
        public virtual ICollection<RoleInfoActionInfo> RoleInfoActionInfo { get; set; }
        public virtual ICollection<RUserInfoActionInfo> RUserInfoActionInfo { get; set; }
    }
}
