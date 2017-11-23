using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OddsAdmin.Models
{
    public class LoginModel:BaseModel
    {
        [Required]
        [StringLength(50)]
       [AllowHtml]
        public string Username { get; set; }

        [Required]
        [StringLength(50)]
        [AllowHtml]
        public string Password { get; set; }
       
    }
}