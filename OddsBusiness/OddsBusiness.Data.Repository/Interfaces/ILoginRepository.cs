using OddsBusiness.Core.Entity;
using System;
using System.Collections.Generic;
namespace OddsBusiness.Repository.Interfaces
{
    public interface ILoginRepository
    {
        /// <summary>
        /// Check Login for Admin
        /// </summary>
        /// <param name="username"></param>
        /// <param name="pwd"></param>
        /// <returns>True or False</returns>
        bool CheckLogin(string username, string pwd);

    }
}
