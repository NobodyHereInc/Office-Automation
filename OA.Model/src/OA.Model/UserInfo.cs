using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace OA.Model
{
    public class UserInfo
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        [MinLength(2, ErrorMessage = "")]
        [MaxLength(20, ErrorMessage ="")]
        public String UserName { get; set; }
        public String UserGender { get; set; }
        [Range(1, 100)]
        public int UserAge { get; set; }
    }
}
