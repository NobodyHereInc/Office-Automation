using System;
using System.Collections.Generic;

namespace OA.Model
{
    public partial class DepartmentActionInfo
    {
        public int ActionInfoId { get; set; }
        public int DepartmentId { get; set; }

        public virtual ActionInfo ActionInfo { get; set; }
        public virtual Department Department { get; set; }
    }
}
