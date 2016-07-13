using System;
using System.Collections.Generic;

namespace OA.Model
{
    public partial class RoleInfoActionInfo
    {
        public int ActionInfoId { get; set; }
        public int RoleInfoId { get; set; }

        public virtual ActionInfo ActionInfo { get; set; }
        public virtual RoleInfo RoleInfo { get; set; }
    }
}
