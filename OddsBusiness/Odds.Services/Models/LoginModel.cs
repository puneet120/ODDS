using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OddsBusiness.Models
{
    public class LoginModel:BaseModel
    {
       
        public string Username { get; set; }

       
        public string Password { get; set; }
       
    }
}