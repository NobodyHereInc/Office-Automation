using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace OA.Model
{
    public class UserInfo
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        public String UserName { get; set; }
        public String UPwd { get; set; }
        public DateTime SubTime { get; set; }
        public short DelFlag { get; set; }
        public DateTime ModifiedOn { get; set; }
        public String Remark { get; set; }
        public String Sort { get; set; }
    }
}
