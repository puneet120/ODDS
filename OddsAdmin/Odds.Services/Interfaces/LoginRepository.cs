using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Odds.Core.Entity;
using Odds.Data.Context;

namespace Odds.Services.Interfaces
{
    public  class LoginRepository :  ILoginRepository
    {
        private readonly IRepository<Login> _login;       


        public LoginRepository(IRepository<Login> login
            )
        {
            _login = login;
            
        }


        /// <summary>
        /// Check Login for Admin
        /// </summary>
        /// <param name="username"></param>
        /// <param name="pwd"></param>
        /// <returns>True or False</returns>
        public bool CheckLogin(string username, string pwd)        
        {

            var empployee = _login.Table.Where(m => m.Username == username && m.Password == pwd).FirstOrDefault();
            if (empployee != null)
            {
                return true;
            }
            return false;
        }
        
    }
}
