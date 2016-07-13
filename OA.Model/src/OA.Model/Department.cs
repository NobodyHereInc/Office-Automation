using System;
using System.Collections.Generic;

namespace OA.Model
{
    public partial class Department
    {
        public Department()
        {
            DepartmentActionInfo = new HashSet<DepartmentActionInfo>();
            UserInfoDepartment = new HashSet<UserInfoDepartment>();
        }

        public int Id { get; set; }
        public string DepName { get; set; }
        public int ParentId { get; set; }
        public string TreePath { get; set; }
        public int Level { get; set; }
        public bool IsLeaf { get; set; }

        public virtual ICollection<DepartmentActionInfo> DepartmentActionInfo { get; set; }
        public virtual ICollection<UserInfoDepartment> UserInfoDepartment { get; set; }
    }
}
