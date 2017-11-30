using System;
using System.Collections.Generic;

namespace OddsBusiness.Core.Entity
{
    public class Login : BaseEntity
    {
        public string Username { get; set; }
        public string Password { get; set; }      
    }
}
