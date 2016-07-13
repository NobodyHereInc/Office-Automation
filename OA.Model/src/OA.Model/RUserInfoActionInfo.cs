using System;
using System.Collections.Generic;

namespace OA.Model
{
    public partial class RUserInfoActionInfo
    {
        public int Id { get; set; }
        public int UserInfoId { get; set; }
        public int ActionInfoId { get; set; }
        public bool IsPass { get; set; }

        public virtual ActionInfo ActionInfo { get; set; }
        public virtual UserInfo UserInfo { get; set; }
    }
}
